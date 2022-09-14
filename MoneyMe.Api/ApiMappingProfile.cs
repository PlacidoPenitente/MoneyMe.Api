using AutoMapper;
using MoneyMe.Api.Responses;
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
                    fee.Amount.Value));

            CreateMap<RuleDto, RuleResponse>()
                .ConstructUsing(fee => new RuleResponse(
                    fee.Id.Value,
                    fee.DateCreated.Value,
                    fee.DateModified,
                    fee.Name));

            CreateMap<ProductDto, ProductResponse>()
                .ConstructUsing(product => new ProductResponse(
                    product.Id.Value,
                    product.DateCreated.Value,
                    product.DateModified,
                    product.Name,
                    product.InterestRate.Value,
                    product.MaximumDuration.Value,
                    product.MinimumDuration.Value,
                    product.Rule));
        }
    }
}