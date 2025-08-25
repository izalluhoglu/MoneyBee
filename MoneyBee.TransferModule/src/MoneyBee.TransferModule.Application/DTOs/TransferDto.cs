namespace MoneyBee.TransferModule.Application.DTOs
{
    /// <summary>Lightweight transfer data transfer object.</summary>
    public class TransferDto
    {
        public Guid Id { get; set; }
        
        public Guid SenderCustomerId { get; set; }
        
        public Guid ReceiverCustomerId { get; set; }
        
        public decimal Amount { get; set; }
        
        public decimal Fee { get; set; }
        
        public string Code { get; set; }
        
        public int Status { get; set; }
        
        public DateTime CreatedAtUtc { get; set; }
        
        public DateTime? CancelledAtUtc { get; set; }
    }
}