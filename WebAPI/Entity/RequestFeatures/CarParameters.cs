
namespace Entity.RequestFeatures
{
    public class CarParameters: RequestParameters
    {
        public uint MinYear { get; set; }
        public uint MaxYear { get; set; } = int.MaxValue;
        public bool ValidYearRange => MaxYear > MinYear;
        public string ModelName { get; set; } 
        public string BrandName { get; set; } 
    }
}
