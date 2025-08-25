using MediatR;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Application.DTOs;

namespace MoneyBee.TransferModule.Application.Transfers.Queries.GetDailyStatsBySender
{
    /// <summary>Handles daily stats retrieval for a sender.</summary>
    public class GetDailyStatsBySenderQueryHandler : IRequestHandler<GetDailyStatsBySenderQuery, TransferDailyStatsDto>
    {
        private readonly ITransferReadRepository _readRepo;
        
        public GetDailyStatsBySenderQueryHandler(ITransferReadRepository readRepo) => _readRepo = readRepo;

        public async Task<TransferDailyStatsDto> Handle(GetDailyStatsBySenderQuery request, CancellationToken cancellationToken)
        {
            var (count, totalAmount, totalFee) = await _readRepo.GetDailyStatsBySenderAsync(request.SenderCustomerId);
            
            return new TransferDailyStatsDto
            {
                SenderCustomerId = request.SenderCustomerId,
                Count = count,
                TotalAmount = totalAmount,
                TotalFee = totalFee,
                DayUtc = DateTime.UtcNow.Date
            };
        }
    }
}