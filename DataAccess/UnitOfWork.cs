
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    //This is where we will be doing the actual committing to the db, Centralize the process.
    public class UnitofWork : IUnitofWork
    {
        //dependency injection
        private readonly ApplicationDbContext _dbContext;

        public UnitofWork(ApplicationDbContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IGenericRepository<ApplicationUser> _ApplicationUser;

        public IGenericRepository<Amenity> _Amenity;
        public IGenericRepository<BedConfiguration> _BedConfiguration;
        public IGenericRepository<Booking> _Booking;
        public IGenericRepository<City> _City;
        public IGenericRepository<County> _County;
        public IGenericRepository<Discount> _Discount;
        public IGenericRepository<Fee> _Fee;
        public IGenericRepository<Image> _Image;
        //public IGenericRepository<Location> _Location;
        public IGenericRepository<PriceRange> _PriceRange;
        public IGenericRepository<Property> _Property;
        public IGenericRepository<PropertyAmenity> _PropertyAmenity;
        public IGenericRepository<PropertyBedConfiguration> _PropertyBedConfiguration;
        public IGenericRepository<PropertyDiscount> _PropertyDiscount;
        public IGenericRepository<PropertyFee> _PropertyFee;
        public IGenericRepository<PropertyNightlyPrice> _PropertyNightlyPrice;
        public IGenericRepository<PropertyType> _PropertyType;
        public IGenericRepository<Review> _Review;
        public IGenericRepository<State> _State;
        public IGenericRepository<Status> _Status;

        

        public IGenericRepository<ApplicationUser> ApplicationUser   //local copy Category, readonly copy
        {
            get
            {
                if (_ApplicationUser == null)
                {
                    _ApplicationUser = new GenericRepository<ApplicationUser>(_dbContext);
                }
                return _ApplicationUser;
            }
        }


        public IGenericRepository<Amenity> Amenity   //local copy Category, readonly copy
        {
            get
            {
                if (_Amenity == null)
                {
                    _Amenity = new GenericRepository<Amenity>(_dbContext);
                }
                return _Amenity;
            }
        }

        public IGenericRepository<BedConfiguration> BedConfiguration   //local copy Category, readonly copy
        {
            get
            {
                if (_BedConfiguration == null)
                {
                    _BedConfiguration = new GenericRepository<BedConfiguration>(_dbContext);
                }
                return _BedConfiguration;
            }
        }

        public IGenericRepository<Booking> Booking   //local copy Category, readonly copy
        {
            get
            {
                if (_Booking == null)
                {
                    _Booking = new GenericRepository<Booking>(_dbContext);
                }
                return _Booking;
            }
        }

        public IGenericRepository<City> City   //local copy Category, readonly copy
        {
            get
            {
                if (_City == null)
                {
                    _City = new GenericRepository<City>(_dbContext);
                }
                return _City;
            }
        }

        public IGenericRepository<County> County   //local copy Category, readonly copy
        {
            get
            {
                if (_County == null)
                {
                    _County = new GenericRepository<County>(_dbContext);
                }
                return _County;
            }
        }

        public IGenericRepository<Discount> Discount   //local copy Category, readonly copy
        {
            get
            {
                if (_Discount == null)
                {
                    _Discount = new GenericRepository<Discount>(_dbContext);
                }
                return _Discount;
            }
        }

        public IGenericRepository<Fee> Fee   //local copy Category, readonly copy
        {
            get
            {
                if (_Fee == null)
                {
                    _Fee = new GenericRepository<Fee>(_dbContext);
                }
                return _Fee;
            }
        }       

        public IGenericRepository<Image> Image
        {
            get
            {
                if (_Image == null)
                {
                    _Image = new GenericRepository<Image>(_dbContext);
                }
                return _Image;
            }
        }

        //public IGenericRepository<Location> Location
        //{
        //    get
        //    {
        //        if (_Location == null)
        //        {
        //            _Location = new GenericRepository<Location>(_dbContext);
        //        }
        //        return _Location;
        //    }
        //}

        public IGenericRepository<PriceRange> PriceRange
        {
            get
            {
                if (_PriceRange == null)
                {
                    _PriceRange = new GenericRepository<PriceRange>(_dbContext);
                }
                return _PriceRange;
            }
        }

        public IGenericRepository<Property> Property
        {
            get
            {
                if (_Property == null)
                {
                    _Property = new GenericRepository<Property>(_dbContext);
                }
                return _Property;
            }
        }
        public IEnumerable<Property> GetAllWithLocationsCitiesCountiesStates()
        {
            //    return _dbContext.Properties
            //                       .Include(p => p.Location)
            //                           .ThenInclude(l => l.City)
            //                       .Include(p => p.Location)
            //                           .ThenInclude(l => l.County)
            //                       .Include(p => p.Location)
            //                           .ThenInclude(l => l.State)
            //                       .ToList();

            return _dbContext.Properties
                .Include(p => p.City)
                .Include(p => p.County)
                .Include(p => p.State);
        }

    public IGenericRepository<PropertyAmenity> PropertyAmenity
        {
            get
            {
                if (_PropertyAmenity == null)
                {
                    _PropertyAmenity = new GenericRepository<PropertyAmenity>(_dbContext);
                }
                return _PropertyAmenity;
            }
        }

        public IGenericRepository<PropertyBedConfiguration> PropertyBedConfiguration
        {
            get
            {
                if (_PropertyBedConfiguration == null)
                {
                    _PropertyBedConfiguration = new GenericRepository<PropertyBedConfiguration>(_dbContext);
                }
                return _PropertyBedConfiguration;
            }
        }

        public IGenericRepository<PropertyDiscount> PropertyDiscount
        {
            get
            {
                if (_PropertyDiscount == null)
                {
                    _PropertyDiscount = new GenericRepository<PropertyDiscount>(_dbContext);
                }
                return _PropertyDiscount;
            }
        }

        public IGenericRepository<PropertyFee> PropertyFee
        {
            get
            {
                if (_PropertyFee == null)
                {
                    _PropertyFee = new GenericRepository<PropertyFee>(_dbContext);
                }
                return _PropertyFee;
            }
        }

        public IGenericRepository<PropertyNightlyPrice> PropertyNightlyPrice
        {
            get
            {
                if (_PropertyNightlyPrice == null)
                {
                    _PropertyNightlyPrice = new GenericRepository<PropertyNightlyPrice>(_dbContext);
                }
                return _PropertyNightlyPrice;
            }
        }

        public IGenericRepository<PropertyType> PropertyType
        {
            get
            {
                if (_PropertyType == null)
                {
                    _PropertyType = new GenericRepository<PropertyType>(_dbContext);
                }
                return _PropertyType;
            }
        }

        public IGenericRepository<Review> Review
        {
            get
            {
                if (_Review == null)
                {
                    _Review = new GenericRepository<Review>(_dbContext);
                }
                return _Review;
            }
        }

        public IGenericRepository<State> State
        {
            get
            {
                if (_State == null)
                {
                    _State = new GenericRepository<State>(_dbContext);
                }
                return _State;
            }
        }

        public IGenericRepository<Status> Status
        {
            get
            {
                if (_Status == null)
                {
                    _Status = new GenericRepository<Status>(_dbContext);
                }
                return _Status;
            }
        }

        IGenericRepository<Property> IUnitofWork.GetAllWithLocationsCitiesCountiesStates => throw new NotImplementedException();

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        //Will dispose of open db connection
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
