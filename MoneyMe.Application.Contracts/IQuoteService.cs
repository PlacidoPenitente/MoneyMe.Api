﻿using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IQuoteService
    {
        public Task<string> RequestQuote(QuoteRequestDto quoteDto);
        public Task<string> RequestQuote(Guid quoteId);
    }
}