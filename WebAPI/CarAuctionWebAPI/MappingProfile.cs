using System;
using AutoMapper;
using DTO;
using Entity.Models;
using Entity.RequestFeatures;

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
                    bn => bn.MapFrom(x => x.Model.Brand.BrandName))
                .ForMember(opt => opt.Image,
                    bn => bn.MapFrom(x => Convert.ToBase64String(x.Image)));

            CreateMap<PagedList<Car>, PagedList<CarDto>>();


            CreateMap<Car, GetOneCarDto>()
                .ForMember(opt => opt.ModelName,
                    mn => mn.MapFrom(x => x.Model.Name))
                .ForMember(opt => opt.BrandName,
                    bn => bn.MapFrom(x => x.Model.Brand.BrandName))
                .ForMember(opt => opt.Image,
                    bn => bn.MapFrom(x => Convert.ToBase64String(x.Image)));

            CreateMap<Lot, GetOneCarDto>();




            CreateMap<LotCreationDto, Lot>()
                .ForMember(m => m.CurrentCost,
                    c => c.MapFrom(x => x.StartingPrice));


            CreateMap<LotCreationDto, Model>();
            CreateMap<LotCreationDto, Brand>();

            CreateMap<LotCreationDto, Car>();


            CreateMap<Bid, BidsDto>()
                .ForMember(opt => opt.StartDate,
                    mn => mn.MapFrom(x => x.Lot.StartDate))
                .ForMember(opt => opt.EndDate,
                    mn => mn.MapFrom(x => x.Lot.EndDate))
                .ForMember(opt => opt.MinimalStep,
                    mn => mn.MapFrom(x => x.Lot.MinimalStep))
                .ForMember(opt => opt.StartingPrice,
                    mn => mn.MapFrom(x => x.Lot.StartingPrice))
                .ForMember(opt => opt.CurrentCost,
                    mn => mn.MapFrom(x => x.Lot.CurrentCost))
                .ForMember(opt => opt.RedemptionPrice,
                    mn => mn.MapFrom(x => x.Lot.RedemptionPrice))
                .ForMember(opt => opt.Year,
                    mn => mn.MapFrom(x => x.Lot.Car.Year))
                .ForMember(opt => opt.Image,
                    bn => bn.MapFrom(x => Convert.ToBase64String(x.Lot.Car.Image)))
                .ForMember(opt => opt.Fuel,
                    mn => mn.MapFrom(x => x.Lot.Car.Fuel))
                .ForMember(opt => opt.CarBody,
                    mn => mn.MapFrom(x => x.Lot.Car.CarBody))
                .ForMember(opt => opt.DriveUnit,
                    mn => mn.MapFrom(x => x.Lot.Car.DriveUnit))
                .ForMember(opt => opt.ModelName,
                    mn => mn.MapFrom(x => x.Lot.Car.Model.Name))
                .ForMember(opt => opt.BrandName,
                    mn => mn.MapFrom(x => x.Lot.Car.Model.Brand.BrandName));




            CreateMap<LotCreationDto, Car>();
            CreateMap<Brand, BrandModelDto>().ForMember(opt => opt.BrandNames,
                mn => mn.MapFrom(x => x.BrandName));
            CreateMap<Model, BrandModelDto>().ForMember(opt => opt.ModelNames,
                mn => mn.MapFrom(x => x.Name));
        }
    }
}
