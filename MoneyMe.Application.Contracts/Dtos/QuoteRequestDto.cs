﻿using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class QuoteRequestDto
    {
        public decimal AmountRequired { get; set; }
        public int Term { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}