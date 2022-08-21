﻿using System;

namespace MoneyMe.Api.Requests
{
    public class QuoteRequest
    {
        public decimal AmountRequired { get; set; }
        public int Terms { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}