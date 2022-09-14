﻿using AutoMapper;
using MoneyMe.Infrastructure.Database.Models;

namespace MoneyMe.Infrastructure
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<Customer, Domain.CustomerAggregate.Customer>().ReverseMap();

            CreateMap<Product, Domain.ProductAggregate.Product>()
                .ConstructUsing(product => new Domain.ProductAggregate.Product(
                    product.Id,
                    product.DateCreated,
                    product.DateModified,
                    product.Name,
                    product.InterestRate,
                    product.MaximumDuration,
                    product.MinimumDuration,
                    product.Rule)).ReverseMap();

            CreateMap<Fee, Domain.FeeAggregate.Fee>()
                .ConstructUsing(fee => new Domain.FeeAggregate.Fee(
                    fee.Id,
                    fee.DateCreated,
                    fee.DateModified,
                    fee.Name,
                    fee.Amount)).ReverseMap();

            CreateMap<Rule, Domain.RuleAggregate.Rule>()
                .ConstructUsing(rule => new Domain.RuleAggregate.Rule(
                    rule.Id,
                    rule.DateCreated,
                    rule.DateModified,
                    rule.Name)).ReverseMap();

            CreateMap<Quote, Domain.QuoteAggregate.Quote>()
                .ConstructUsing(quote => new Domain.QuoteAggregate.Quote(
                    quote.Id,
                    quote.DateCreated,
                    quote.DateModified,
                    quote.CustomerId,
                    quote.LoanAmount,
                    quote.Term)).ReverseMap();

            CreateMap<Loan, Domain.LoanAggregate.Loan>().ReverseMap();
            CreateMap<Payment, Domain.Shared.Payment>().ReverseMap();
        }
    }
}