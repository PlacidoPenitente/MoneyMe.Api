using AutoMapper;
using MoneyMe.Infrastructure.Database.Models;

namespace MoneyMe.Infrastructure
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<Customer, Domain.CustomerAggregate.Customer>().ReverseMap();
            CreateMap<Product, Domain.ProductAggregate.Product>().ReverseMap();
            CreateMap<Fee, Domain.FeeAggregate.Fee>().ReverseMap();
            CreateMap<Loan, Domain.LoanAggregate.Loan>().ReverseMap();
            CreateMap<Payment, Domain.LoanAggregate.Payment>().ReverseMap();
            CreateMap<Quote, Domain.QuoteAggregate.Quote>().ReverseMap();
        }
    }
}