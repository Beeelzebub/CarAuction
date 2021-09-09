using AutoMapper;
using DTO;
using Entity.Models;

namespace CarAuctionWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, User>();

            CreateMap<Car, CarDto>()
                .ForMember(opt => opt.ModelName,
                    mn => mn.MapFrom(x => x.Model.Name))
                .ForMember(opt => opt.BrandName,
                    bn => bn.MapFrom(x => x.Model.Brand.BrandName));

            CreateMap<LotCreationDto, Lot>()
                .ForMember(m => m.CurrentCost,
                    c => c.MapFrom(x => x.StartingPrice));

            CreateMap<LotCreationDto, Car>();
            CreateMap<Bid, BidsDto>();
            CreateMap<LotCreationDto, Car>();
        }
    }
}
