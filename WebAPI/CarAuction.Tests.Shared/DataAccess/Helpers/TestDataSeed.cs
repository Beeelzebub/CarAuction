using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;
using Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CarAuction.Tests.Shared.DataAccess.Helpers
{
    public class TestDataSeed
    {
        public static void Seed(TestCarAuctionDbContext context)
        {
            AddBrandsWithModels(context);

            context.SaveChanges();
        }

        private static void AddBrandsWithModels(TestCarAuctionDbContext context)
        {
            var brands = new List<Brand>
            {
                new Brand()
                {
                    BrandName = "Audi", Models =
                        new List<Model>()
                        {
                            new Model() {Name = "A6"},
                            new Model() {Name = "A7"},
                            new Model() {Name = "A8"},
                        },
                },
                new Brand()
                {
                    BrandName = "BMW", Models =
                        new List<Model>()
                        {
                            new Model() {Name = "M1"},
                            new Model() {Name = "I3"},
                            new Model() {Name = "X5"},
                        },
                },
                new Brand()
                {
                    BrandName = "MERCEDES-BENZ", Models =
                        new List<Model>()
                        {
                            new Model() {Name = "S-CLASS MAYBACH PULLMAN"},
                            new Model() {Name = "S-CLASS CABRIOLET"},
                            new Model() {Name = "A-CLASS AMG"},
                        },
                },
            };

            context.Brands.AddRange(brands);
            context.SaveChanges();
        }

        private static void AddLotsWithCars(TestCarAuctionDbContext context)
        {
            var lots = new List<Lot>
            {
                new Lot()
                {
                    Id = 1,
                    Car = new Car()
                    {
                        CarBody = CarBody.Coupe,
                        DriveUnit = DriveUnit.FourWheelDrive,
                        Fuel = Fuel.Diesel,
                        ModelId = 1
                    },  
                    StartDate = DateTime.Now,
                    Status = LotStatus.Pending,
                    MinimalStep = 2000,
                    StartingPrice = 10000,
                    RedemptionPrice = 20000
                },
                new Lot()
                {
                    Id = 2,
                    Car = new Car()
                    {
                        CarBody = CarBody.Coupe,
                        DriveUnit = DriveUnit.FourWheelDrive,
                        Fuel = Fuel.Diesel,
                        ModelId = 2
                    },
                    StartDate = DateTime.Now,
                    Status = LotStatus.Approved,
                    MinimalStep = 2000,
                    StartingPrice = 10000,
                    RedemptionPrice = 20000,
                    CurrentCost = 10000,
                    Bids = new List<Bid>
                    {
                        new Bid()
                        {
                            Id = 1,
                            BidStatus = BidStatus.Active
                        }
                    }
                },
                new Lot()
                {
                    Id = 3,
                    Car = new Car()
                    {
                        CarBody = CarBody.Coupe,
                        DriveUnit = DriveUnit.FourWheelDrive,
                        Fuel = Fuel.Diesel,
                        ModelId = 3
                    },
                    StartDate = DateTime.Now,
                    Status = LotStatus.Ended,
                    CurrentCost = 10000,
                    MinimalStep = 2000,
                    StartingPrice = 10000,
                    RedemptionPrice = 20000
                }
            };

            context.Lots.AddRange(lots);
            context.SaveChanges();
        }
    }
}
