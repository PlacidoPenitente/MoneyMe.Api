using System.Linq;
using AutoMapper;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.RuleAggregate;

namespace MoneyMe.Application.Contracts
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ConstructUsing(x => new CustomerDto(x.Id));

            CreateMap<Quote, QuoteDto>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ConstructUsing(x => new ProductDto(x.Id, x.DateCreated, x.DateModified)
                {
                    Fees = new System.Collections.Generic.List<FeeDto>()
                })
                .ReverseMap()
                .ConstructUsing(product => new Product(
                    product.Id.Value,
                    product.DateCreated.Value,
                    product.DateModified,
                    product.Name,
                    product.InterestRate.Value,
                    product.MinimumDuration.Value,
                    product.MaximumDuration.Value,
                    product.RuleId,
                    product.Fees.Select(fee => fee.Id.Value).ToList()));

            CreateMap<Fee, FeeDto>().ConstructUsing(x => new FeeDto(x.Id, x.DateCreated, x.DateModified));
            CreateMap<Rule, RuleDto>().ConstructUsing(x => new RuleDto(x.Id, x.DateCreated, x.DateModified));
        }
    }
}