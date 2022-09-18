using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MoneyMe.Application.Contracts;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain;
using MoneyMe.Domain.Repositories;
using MoneyMe.Application.Contracts.Dtos;

namespace MoneyMe.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductFactory _productFactory;
        private readonly IProductRepository _productRepository;
        private readonly IFeeRepository _feeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IProductFactory productFactory,
            IProductRepository productRepository,
            IFeeRepository feeRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
            _feeRepository = feeRepository;
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
                productDto.RuleId,
                productDto.Fees.Select(x => x.Id.Value).ToList());

            using (_unitOfWork)
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    await _productRepository.AddAsync(product);

                    var createdProductDto = _mapper.Map<ProductDto>(product);

                    foreach (var feeId in product.FeeIds)
                    {
                        var fee = await _feeRepository.GetAsync(feeId);
                        createdProductDto.Fees.Add(_mapper.Map<FeeDto>(fee));
                    }

                    return createdProductDto;
                });
            }
        }

        public async Task<ProductDto> ReadProductAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            foreach (var feeId in product.FeeIds)
            {
                var fee = await _feeRepository.GetAsync(feeId);
                productDto.Fees.Add(_mapper.Map<FeeDto>(fee));
            }

            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> ReadAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = _mapper.Map<ProductDto>(product);

                foreach (var feeId in product.FeeIds)
                {
                    var fee = await _feeRepository.GetAsync(feeId);
                    productDto.Fees.Add(_mapper.Map<FeeDto>(fee));
                }

                productDtos.Add(productDto);
            }

            return productDtos;
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    var product = await _productRepository.GetAsync(productDto.Id.Value);

                    product.ChangeName(productDto.Name);
                    product.ChangeInterestRate(productDto.InterestRate);
                    product.ChangeDurationRange(productDto.MinimumDuration, productDto.MaximumDuration);
                    product.ChangeRule(productDto.RuleId);
                    product.ChangeFees(productDto.Fees.Select(fee => fee.Id.Value).ToList());

                    _productRepository.Update(product);

                    var a = _mapper.Map<ProductDto>(product);
                    return a;
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

        public async Task AddFeeToProductAsync(Guid id, Guid feeId)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () =>
                {
                    var product = await _productRepository.GetAsync(id);
                    var fee = await _feeRepository.GetAsync(feeId);

                    if (product != null && fee != null)
                    {
                        product.AddFee(fee.Id);
                        _productRepository.Update(product);
                    }
                });
            }
        }

        public async Task RemoveFeeFromProductAsync(Guid id, Guid feeId)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () =>
                {
                    var product = await _productRepository.GetAsync(id);
                    var fee = await _feeRepository.GetAsync(feeId);

                    if (product != null && fee != null)
                    {
                        product.RemoveFee(fee.Id);
                        _productRepository.Update(product);
                    }
                });
            }
        }
    }
}