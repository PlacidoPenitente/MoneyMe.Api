using System;
using System.Linq;
using AutoMapper;
using MoneyMe.Infrastructure.Database.Models;

namespace MoneyMe.Infrastructure
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<Customer, Domain.CustomerAggregate.Customer>().ConstructUsing(x => new Domain.CustomerAggregate.Customer(
                x.Id,
                x.DateCreated,
                x.DateModified,
                Enum.Parse<Domain.Shared.Title>(x.Title),
                x.FirstName,
                x.LastName,
                x.DateOfBirth,
                x.MobileNumber,
                x.EmailAddress)).ReverseMap();

            CreateMap<Product, Domain.ProductAggregate.Product>()
                .ConstructUsing(product => new Domain.ProductAggregate.Product(
                    product.Id,
                    product.DateCreated,
                    product.DateModified,
                    product.Name,
                    product.InterestRate,
                    product.MaximumDuration,
                    product.MinimumDuration,
                    product.RuleId,
                    product.ProductFees.Select(fee => fee.Id).ToList()))
                .ReverseMap()
                .ConstructUsing(product => new Product()
                {
                    Id = product.Id,
                    DateCreated = product.DateCreated,
                    DateModified = product.DateModified,
                    Name = product.Name,
                    InterestRate = product.InterestRate,
                    MaximumDuration = product.MaximumDuration,
                    MinimumDuration = product.MinimumDuration,
                    RuleId = product.RuleId,
                    ProductFees = product.FeeIds.Select(x => new ProductFee { Id = Guid.NewGuid(), FeeId = x }).ToList()
                });

            CreateMap<Fee, Domain.FeeAggregate.Fee>()
                .ConstructUsing(fee => new Domain.FeeAggregate.Fee(
                    fee.Id,
                    fee.DateCreated,
                    fee.DateModified,
                    fee.Name,
                    fee.Amount,
                    fee.IsPercentage))
                .ReverseMap();

            CreateMap<Rule, Domain.RuleAggregate.Rule>()
                .ConstructUsing(rule => new Domain.RuleAggregate.Rule(
                    rule.Id,
                    rule.DateCreated,
                    rule.DateModified,
                    rule.Name))
                .ReverseMap();

            CreateMap<Quote, Domain.QuoteAggregate.Quote>()
                .ConstructUsing(quote => new Domain.QuoteAggregate.Quote(
                    quote.Id,
                    quote.DateCreated,
                    quote.DateModified,
                    quote.CustomerId,
                    quote.LoanAmount,
                    quote.Term,
                    quote.ProductId))
                .ReverseMap()
                .ConstructUsing(x=> new Quote()
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    LoanAmount = x.LoanAmount,
                    Term = x.Term,
                    ProductId = x.ProductId
                });

            CreateMap<Loan, Domain.LoanAggregate.Loan>().ReverseMap();
            CreateMap<Payment, Domain.Shared.Payment>().ReverseMap();
        }
    }
}