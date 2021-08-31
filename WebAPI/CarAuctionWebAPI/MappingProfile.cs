using System.Linq;
using AutoMapper;
using DTO;
using Entity.Models;

namespace CarAuctionWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();

            CreateMap<Car, CarDtoForGet>()
                .ForMember(opt=>opt.ModelName, 
                    mn=>mn.MapFrom(x=>x.Model.Name))
                .ForMember(opt=>opt.BrandName,
                    bn=>bn.MapFrom(x=>x.Model.Brand.BrandName));


            CreateMap<CarDtoForCreation, Car>();
            CreateMap<Bid, BidsDtoForGet>();
            CreateMap<CarDtoForCreation, Lot>();
            CreateMap<CarDtoForCreation, Car>();
        }
    }
}
