namespace API
{
    public class CalculateResultDto
    {
        public int TotalHours { get; set; }
        public int NumberOfEvents { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithVAT { get; set; }
        public string Month { get; set; }
    }
}
