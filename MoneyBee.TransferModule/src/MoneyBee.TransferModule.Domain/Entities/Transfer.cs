using System.Transactions;

namespace MoneyBee.TransferModule.Domain.Entities
{
    /// <summary>Represents a money transfer transaction.</summary>
    public class Transfer
    {
        /// <summary>Primary key.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>Sender customer unique Id.</summary>
        public Guid SenderCustomerId { get; set; }

        /// <summary>Receiver customer unique Id.</summary>
        public Guid ReceiverCustomerId { get; set; }

        /// <summary>Amount being sent.</summary>
        public decimal Amount { get; set; }

        /// <summary>Transaction fee amount.</summary>
        public decimal Fee { get; set; }

        /// <summary>Code the receiver will use to withdraw money.</summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>Transaction status.</summary>
        public int Status { get; set; }

        /// <summary>UTC creation time.</summary>
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        /// <summary>UTC cancellation time.</summary>
        public DateTime? CancelledAtUtc { get; set; }
    }
}