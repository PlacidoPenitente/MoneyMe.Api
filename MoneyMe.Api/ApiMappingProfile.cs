using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoneyMe.Api.Responses;
using MoneyMe.Api.Responses.Product;
using MoneyMe.Api.Responses.ProductResponse;
using MoneyMe.Application.Contracts.Dtos;

namespace MoneyMe.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<FeeDto, FeeResponse>()
                .ConstructUsing(fee => new FeeResponse(
                    fee.Id.Value,
                    fee.DateCreated.Value,
                    fee.DateModified,
                    fee.Name,
                    fee.Amount.Value,
                    fee.IsPercentage.Value));

            CreateMap<RuleDto, RuleResponse>()
                .ConstructUsing(rule => new RuleResponse(
                    rule.Id.Value,
                    rule.DateCreated.Value,
                    rule.DateModified,
                    rule.Name));

            CreateMap<FeeDto, Fee>()
                .ConstructUsing(fee => new Fee(
                    fee.Id.Value,
                    fee.Name,
                    fee.Amount.Value));

            CreateMap<ProductDto, Product>()
                .ConstructUsing(product => new Product(
                    product.Id.Value,
                    product.DateCreated.Value,
                    product.DateModified,
                    product.Name,
                    product.InterestRate.Value,
                    product.MaximumDuration.Value,
                    product.MinimumDuration.Value,
                    product.RuleId,
                    new List<Fee>()));
        }
    }
}