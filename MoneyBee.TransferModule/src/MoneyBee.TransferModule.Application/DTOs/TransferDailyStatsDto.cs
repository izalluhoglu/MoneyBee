namespace MoneyBee.TransferModule.Application.DTOs
{
    /// <summary>Daily totals for a sender.</summary>
    public class TransferDailyStatsDto
    {
        public Guid SenderCustomerId { get; set; }
        
        public int Count { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TotalFee { get; set; }
        
        public DateTime DayUtc { get; set; }
    }
}