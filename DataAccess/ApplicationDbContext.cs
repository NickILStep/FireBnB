using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Amenity> Amenities { get; set; } 
        public DbSet<BedConfiguration> BedConfigurations { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<City> Cities { get; set; }

		public DbSet<County> Counties { get; set; }

		public DbSet<Discount> Discounts { get; set; }

		public DbSet<Fee> Fees { get; set; }

		public DbSet<Image> Images { get; set; }

		//public DbSet<Location> Locations { get; set; }

		//public DbSet<PriceRange> PriceRanges { get; set; }

		public DbSet<Property> Properties { get; set; }

		public DbSet<PropertyAmenity> PropertyAmenities { get; set; }

		public DbSet<PropertyBedConfiguration> PropertyBedConfigurations { get; set; }

		public DbSet<PropertyDiscount> PropertyDiscounts { get; set; }

		public DbSet<PropertyFee> PropertyFees { get; set; }

		public DbSet<PropertyNightlyPrice> PropertyNightlyPrices { get; set; }

		public DbSet<PropertyType> PropertyTypes { get; set; }

		public DbSet<Review> Reviews { get; set; }

		public DbSet<State> States { get; set; }

		public DbSet<Status> Statuses { get; set; }


		//continue adding models



	}
}

