using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;

namespace MoneyMe.Infrastructure.Repositories
{
    public sealed class ProductRepository : Repository<Product, Database.Models.Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductFactory _productFactory;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IProductFactory productFactory, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _productFactory = productFactory;
            _mapper = mapper;
        }

        public override async Task<Product> GetAsync(Guid id)
        {
            var product = await _context.Products.Include(x => x.ProductFees).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            var productWithFeeIds = _productFactory.Create(
                product.Id,
                product.DateCreated,
                product.DateModified,
                product.Name,
                product.InterestRate,
                product.MinimumDuration,
                product.MaximumDuration,
                product.RuleId,
                product.ProductFees.Select(x => x.FeeId).ToList());

            return _mapper.Map<Product>(productWithFeeIds);
        }

        public override async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            var products = await _context.Products.Include(x => x.ProductFees).ToListAsync();

            var productWithFeeIds = products.Select(product => _productFactory.Create(
                product.Id,
                product.DateCreated,
                product.DateModified,
                product.Name,
                product.InterestRate,
                product.MinimumDuration,
                product.MaximumDuration,
                product.RuleId,
                product.ProductFees.Select(x => x.FeeId).ToList())).Select(_mapper.Map<Product>).ToList();

            return productWithFeeIds;
        }

        public override void Remove(Product t)
        {
            var entity = _mapper.Map<Database.Models.Product>(t);

            var feesToBeRemoved = new List<Database.Models.ProductFee>();

            foreach (var item in entity.ProductFees)
            {
                var productFee = _context.ProductFees.AsNoTracking().Include(x => x.Product).Include(x => x.Fee).Single(x => x.FeeId == item.FeeId && x.ProductId == t.Id);
                feesToBeRemoved.Add(productFee);
            }

            entity.ProductFees = feesToBeRemoved;
            _context.Set<Database.Models.Product>().Remove(entity);
        }

        public override void Update(Product t)
        {
            var entity = _mapper.Map<Database.Models.Product>(t);

            var newFees = new List<Database.Models.ProductFee>();

            var oldProductFees = _context.ProductFees.AsNoTracking().Include(x => x.Product).Include(x => x.Fee).Where(x => x.ProductId == t.Id);

            foreach (var oldProductFee in oldProductFees)
            {
                _context.Entry(oldProductFee).State = EntityState.Deleted;
            }

            foreach (var item in entity.ProductFees)
            {
                var productFee = new Database.Models.ProductFee
                {
                    Id = Guid.NewGuid(),
                    FeeId = item.FeeId,
                    Fee = _context.Fees.AsNoTracking().Single(x => x.Id == item.FeeId),
                    ProductId = entity.Id,
                    Product = entity
                };

                newFees.Add(productFee);
            }

            entity.ProductFees = newFees;

            if (entity.ProductFees.Any())
            {
                foreach (var item in entity.ProductFees)
                {
                    _context.Entry(item).State = EntityState.Added;
                }
            }

            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}