using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUnitofWork
    {
        //Add to as new models are developed, so unit of work has access to them
        public IGenericRepository<Amenity> Amenity { get; }
        public IGenericRepository<ApplicationUser> ApplicationUser { get; }
        public IGenericRepository<BedConfiguration> BedConfiguration { get; }
        public IGenericRepository<Booking> Booking { get; }
        public IGenericRepository<City> City { get; }
        public IGenericRepository<County> County { get; }
        public IGenericRepository<Discount> Discount { get; }
        public IGenericRepository<Fee> Fee { get; }
        public IGenericRepository<Image> Image { get; }
        public IGenericRepository<Location> Location { get; }
        public IGenericRepository<PriceRange> PriceRange{ get; }
        public IGenericRepository<Property> Property { get; }
        public IGenericRepository<Property> GetAllWithLocationsCitiesCountiesStates { get; }
        public IGenericRepository<PropertyAmenity> PropertyAmenity { get; }
        public IGenericRepository<PropertyBedConfiguration> PropertyBedConfiguration { get; }
        public IGenericRepository<PropertyDiscount> PropertyDiscount { get; }
        public IGenericRepository<PropertyFee> PropertyFee { get; }
        public IGenericRepository<PropertyNightlyPrice> PropertyNightlyPrice { get; }
        public IGenericRepository<PropertyType> PropertyType { get; }
        public IGenericRepository<Review> Review { get; }
        public IGenericRepository<State> State { get; }
        public IGenericRepository<Status> Status { get; }

        //func: save changes to the data source

        int Commit();

        Task<int> CommitAsync();
    }
}
