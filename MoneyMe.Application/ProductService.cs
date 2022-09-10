using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain;
using MoneyMe.Domain.Repositories;

namespace MoneyMe.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductFactory _productFactory;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IProductFactory productFactory, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = _productFactory.Create(
                productDto.Name,
                productDto.InterestRate.Value,
                productDto.MinimumDuration.Value,
                productDto.MaximumDuration.Value,
                productDto.Rule);

            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () => await _productRepository.AddAsync(product));
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> ReadProductAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> ReadAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(_mapper.Map<ProductDto>);
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    var product = await _productRepository.GetAsync(productDto.Id.Value);

                    product.ChangeName(productDto.Name);
                    product.ChangeInterestRate(productDto.InterestRate.Value);
                    product.ChangeDurationRange(productDto.MinimumDuration.Value, productDto.MaximumDuration.Value);
                    product.ChangeRule(productDto.Rule);

                    _productRepository.Update(product);

                    return _mapper.Map<ProductDto>(product);
                });
            }
        }

        public async Task DeleteProductAsync(Guid id)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () =>
                {
                    var product = await _productRepository.GetAsync(id);

                    _productRepository.Remove(product);
                });
            }
        }
    }
}