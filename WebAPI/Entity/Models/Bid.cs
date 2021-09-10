using Enums;

namespace Entity.Models
{
    public class Bid : IEntity
    {
        public int Id { get; set; }
        
        public BidStatus BidStatus { get; set; }
        public  Lot Lot { get; set; }
        
        public int LotId { get; set; }
        
        public  User Buyer { get; set; }

        public string BuyerId { get; set; }
    }
}
