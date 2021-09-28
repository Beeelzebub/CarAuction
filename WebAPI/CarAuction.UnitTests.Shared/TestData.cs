using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;
using Enums;

namespace CarAuction.UnitTests.Shared
{
    public static class TestData
    {
        public const string TestUserId = "TestUserId";

        public static Car GetTestCar() =>
            new Car()
            {
                Id = 1,
                CarBody = CarBody.Coupe,
                DriveUnit = DriveUnit.FourWheelDrive,
                Fuel = Fuel.Diesel,
                Image = Encoding.ASCII.GetBytes("hello"),
                Model = new Model()
                {
                    Id = 1,
                    Name = "Audi",
                    BrandId = 1,
                    Brand = new Brand()
                    {
                        Id = 1,
                        BrandName = "A6"
                    }
                },
                ModelId = 1,
                LotId = 1,
                Year = 2018,

                Lot = new Lot()
                {
                    Id = 1,
                    StartDate = DateTime.Now,
                    Status = LotStatus.Pending,
                    MinimalStep = 2000,
                    StartingPrice = 10000,
                    RedemptionPrice = 20000,
                    CurrentCost = 10000,
                    SellerId = TestUserId,
                    
                    Bids = new List<Bid>
                    {
                    new Bid()
                        {
                        Id = 1,
                        BidStatus = BidStatus.Active
                        }
                    }
                }
            };

        public static Lot GetTestLot() =>
            new Lot()
            {
                Id = 1,
                Car = new Car()
                {
                    CarBody = CarBody.Coupe,
                    DriveUnit = DriveUnit.FourWheelDrive,
                    Fuel = Fuel.Diesel,
                    Image = Encoding.ASCII.GetBytes("hello"),
                    ModelId = 1
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
                },
                SellerId = TestUserId
            };

        public static IEnumerable<Car> GetTestCarsList() =>
            new List<Car>()
            {
                new Car()
                {
                    CarBody = CarBody.Coupe,
                    DriveUnit = DriveUnit.FourWheelDrive,
                    Fuel = Fuel.Diesel,
                    Image = Encoding.ASCII.GetBytes("hello"),
                    ModelId = 1
                },
                new Car()
                {
                    CarBody = CarBody.Coupe,
                    DriveUnit = DriveUnit.FourWheelDrive,
                    Fuel = Fuel.Diesel,
                    Image = Encoding.ASCII.GetBytes("hello"),
                    ModelId = 2
                },
                new Car()
                {
                    CarBody = CarBody.Coupe,
                    DriveUnit = DriveUnit.FourWheelDrive,
                    Fuel = Fuel.Diesel,
                    Image = Encoding.ASCII.GetBytes("hello"),
                    ModelId = 3
                }
            };

        public static IEnumerable<Lot> GetTestLotsList() => 
            new List<Lot>
            {
                new Lot()
                {
                    Id = 1,
                    Car = new Car()
                    {
                        CarBody = CarBody.Coupe,
                        DriveUnit = DriveUnit.FourWheelDrive,
                        Fuel = Fuel.Diesel,
                        Image = Encoding.ASCII.GetBytes("hello"),
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
                        Image = Encoding.ASCII.GetBytes("hello"),
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
                        Image = Encoding.ASCII.GetBytes("hello"),
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

        public static IEnumerable<Bid> GetTestBidsList() =>
            new List<Bid>()
            {
                new Bid()
                {
                    Id = 1,
                    BidStatus = BidStatus.Active,
                    BuyerId = TestUserId
                }
            };
    }
}
