using AutoMapper;
using Entity.DTO;
using Entity.Models;

namespace CarAuctionWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<Car, CarDtoForGet>();
            CreateMap< CarDtoForCreation, Car>();
        }
    }
}
