using Entity.Models;

namespace DTO
{
    public class GetBidsDto
    {
        public BidStatus BidStatus { get; set; }
        public int LotId { get; set; }
    }
}
