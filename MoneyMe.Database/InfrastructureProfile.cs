using AutoMapper;
using MoneyMe.Infrastructure.Database.Models;

namespace MoneyMe.Infrastructure
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Customer, Domain.CustomerAggregate.Customer>().ReverseMap();
            CreateMap<Product, Domain.ProductAggregate.Product>().ReverseMap();
            CreateMap<Fee, Domain.ProductAggregate.Fee>().ReverseMap();
            CreateMap<Loan, Domain.LoanAggregate.Loan>().ReverseMap();
            CreateMap<Payment, Domain.LoanAggregate.Payment>().ReverseMap();
            CreateMap<Quote, Domain.QuoteAggregate.Quote>().ReverseMap();
        }
    }
}