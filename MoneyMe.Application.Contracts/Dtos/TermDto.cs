namespace MoneyMe.Application.Contracts.Dtos
{
    public class TermDto
    {
        public int Period { get; set; }
        public decimal Interest { get; set; }
        public decimal Principal { get; set; }
    }
}