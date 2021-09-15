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
            CreateMap<Car, GetOneCarDto>()
                .ForMember(opt => opt.ModelName,
                    mn => mn.MapFrom(x => x.Model.Name))
                .ForMember(opt => opt.BrandName,
                    bn => bn.MapFrom(x => x.Model.Brand.BrandName))
                .ForMember(opt => opt.CurrentCost,
                    bn => bn.MapFrom(x => x.Lot.CurrentCost))
                .ForMember(opt => opt.EndDate,
                bn => bn.MapFrom(x => x.Lot.EndDate))
                .ForMember(opt => opt.StartDate,
                    bn => bn.MapFrom(x => x.Lot.StartDate))
                .ForMember(opt => opt.MinimalStep,
                    bn => bn.MapFrom(x => x.Lot.MinimalStep))
                .ForMember(opt => opt.RedemptionPrice,
                    bn => bn.MapFrom(x => x.Lot.RedemptionPrice))
                .ForMember(opt => opt.StartingPrice,
                    bn => bn.MapFrom(x => x.Lot.StartingPrice));

            CreateMap<LotCreationDto, Lot>()
                .ForMember(m => m.CurrentCost,
                    c => c.MapFrom(x => x.StartingPrice));

            CreateMap<LotCreationDto, Car>();
            CreateMap<Bid, BidsDto>();
            CreateMap<LotCreationDto, Car>();
            CreateMap<Brand, BrandModelDto>().ForMember(opt => opt.BrandNames,
                mn => mn.MapFrom(x => x.BrandName));
            CreateMap<Model, BrandModelDto>().ForMember(opt => opt.ModelNames,
                mn => mn.MapFrom(x => x.Name));
        }
    }
}
