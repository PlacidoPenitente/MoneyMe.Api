using AutoMapper;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.ProductAggregate;

namespace MoneyMe.Application.Contracts
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ConstructUsing(x => new CustomerDto(x.Id));
            CreateMap<Product, ProductDto>().ConstructUsing(x => new ProductDto(x.Id, x.DateCreated, x.DateModified));
            CreateMap<Fee, FeeDto>().ConstructUsing(x => new FeeDto(x.Id, x.DateCreated, x.DateModified));
        }
    }
}