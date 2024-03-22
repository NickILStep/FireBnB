using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace DataAccess
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public void Initialize()
        {
            _db.Database.EnsureCreated();

            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (_db.Amenities.Any())
            {
                return;
            }


            //create roles if they are not created

            _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.ListerRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.RenterRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.ListerRole)).GetAwaiter().GetResult();


            var Amenities = new List<Amenity>
            {
                new Amenity { AmenityName = "Pool" },
                new Amenity { AmenityName = "Home Theater" },
                new Amenity { AmenityName = "Billiards Table" },
                new Amenity { AmenityName = "Trampoline" },
                new Amenity { AmenityName = "Dishwasher" },
                new Amenity { AmenityName = "Washer and Dryer" }
            };

            foreach (var a in Amenities)
            {
                _db.Amenities.Add(a);
            }
            _db.SaveChanges();

            var ApplicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Tom", LastName = "Riddle", DisplayName = "Voldemort", Birthdate = Convert.ToDateTime("12/31/1926"), SignupDate = Convert.ToDateTime("9/1/1938"), UserName = "tomriddle@mail.com", NormalizedUserName = "TOMRIDDLE@MAIL.COM", Email = "tomriddle@mail.com", NormalizedEmail = "TOMRIDDLE@MAIL.COM", EmailConfirmed = true, PhoneNumber = "801-555-1234", PhoneNumberConfirmed = false},
                new ApplicationUser { FirstName = "Harry", LastName = "Potter", DisplayName = "The Boy Who Lived", Birthdate = Convert.ToDateTime("7/31/1980"), SignupDate = Convert.ToDateTime("9/1/1991"), UserName = "hpotter@mail.com", NormalizedUserName = "HPOTTER@MAIL.COM", Email = "hpotter@mail.com", NormalizedEmail = "HPOTTER@MAIL.COM", EmailConfirmed = true, PhoneNumber = "801-555-2345", PhoneNumberConfirmed = false},
                new ApplicationUser { FirstName = "Hermione", LastName = "Granger", DisplayName = "It's LeviOsa, not levioSA!", Birthdate = Convert.ToDateTime("9/19/1979"), SignupDate = Convert.ToDateTime("9/1/1991"), UserName = "hermioneg@mail.com", NormalizedUserName = "HERMIONEG@MAIL.COM", Email = "hermioneg@mail.com", NormalizedEmail = "HERMIONEG@MAIL.COM", EmailConfirmed = true, PhoneNumber = "801-555-3456", PhoneNumberConfirmed = false},
                new ApplicationUser { FirstName = "Ron", LastName = "Weasley", DisplayName = "Grand Theft Auto", Birthdate = Convert.ToDateTime("3/1/1980"), SignupDate = Convert.ToDateTime("9/1/1991"), UserName = "ronweasley@mail.com", NormalizedUserName = "RONWEASLEY@MAIL.COM", Email = "ronweasley@mail.com", NormalizedEmail = "RONWEASLEY@MAIL.COM", EmailConfirmed = true, PhoneNumber = "801-555-4567", PhoneNumberConfirmed = false}
            };

            foreach (var a in ApplicationUsers)
            {
                _db.ApplicationUsers.Add(a);
            }
            _db.SaveChanges();

            var BedConfigurations = new List<BedConfiguration>
            {
                new BedConfiguration { Quantity = 1, Configuration = "1 King"},
                new BedConfiguration { Quantity = 1, Configuration = "1 Queen" },
                new BedConfiguration { Quantity = 1, Configuration = "1 Twin" },
                new BedConfiguration { Quantity = 2, Configuration = "2 King" },
                new BedConfiguration { Quantity = 2, Configuration = "2 Queen" },
                new BedConfiguration { Quantity = 2, Configuration = "2 Twin" },
                new BedConfiguration { Quantity = 2, Configuration = "1 King, 1 Queen" },
                new BedConfiguration { Quantity = 2, Configuration = "1 King, 1 Twin" },
                new BedConfiguration { Quantity = 2, Configuration = "1 Queen, 1 Twin" },
                new BedConfiguration { Quantity = 2, Configuration = "2 Twin (Bunks)" }
            };

            foreach (var b in BedConfigurations)
            {
                _db.BedConfigurations.Add(b);
            }
            _db.SaveChanges();

            var PropertyTypes = new List<PropertyType>
            {
                new PropertyType { Title = "House"  },
                new PropertyType { Title = "Apartment" },
                new PropertyType { Title = "Cabin" },
                new PropertyType { Title = "Duplex" },
                new PropertyType { Title = "Camper" },
                new PropertyType { Title = "Mansion" }
            };

            foreach (var p in PropertyTypes)
            {
                _db.PropertyTypes.Add(p);
            }
            _db.SaveChanges();

            var Cities = new List<City>
            {
                new City { CityName = "Ogden", TaxRate = 7.25f },
                new City { CityName = "Logan", TaxRate = 1.0f },
                new City { CityName = "Salt Lake City", TaxRate = 7.75f }
            };

            foreach (var c in Cities)
            {
                _db.Cities.Add(c);
            }
            _db.SaveChanges();

            var Counties = new List<County>
            {
                new County { CountyName = "Cache", TaxRate = 0.1f },
                new County { CountyName = "Box Elder", TaxRate = 0.35f },
                new County { CountyName = "Weber", TaxRate = 0.2f }
            };

            foreach (var c in Counties)
            {
                _db.Counties.Add(c);
            }
            _db.SaveChanges();

            var States = new List<State>
            {
                new State { StateName = "Utah", TaxRate = 4.65f },
                new State { StateName = "California", TaxRate = 7.25f },
                new State { StateName = "Colorado" , TaxRate = 4.4f },
                new State { StateName = "Washington", TaxRate = 6.5f }
            };

            foreach (var s in States)
            {
                _db.States.Add(s);
            }
            _db.SaveChanges();

            var Locations = new List<Location>
            {
                new Location { CityId = 1, CountyId = 2, StateId = 4, Address = "123 Somewhere St.", Zipcode = "84321"},
                new Location { CityId = 3, CountyId = 3, StateId = 2, Address = "456 Anywhere Blvd.", Zipcode = "84654"},
                new Location { CityId = 2, CountyId = 1, StateId = 1, Address = "789 Nowhere Ave.", Zipcode = "84987"}
            };

            foreach (var l in Locations)
            {
                _db.Locations.Add(l);
            }
            _db.SaveChanges();

            var Statuses = new List<Status>
            {
                new Status { StatusName = "Available" },
                new Status { StatusName = "Hidden" },
                new Status { StatusName = "Partially Available" }
            };

            foreach (var s in Statuses)
            {
                _db.Statuses.Add(s);
            }
            _db.SaveChanges();

            var Properties = new List<Property>
            {
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 2, LocationId = 1, StatusId = 2, Description = "A nice home", LastUpdated = Convert.ToDateTime(DateTime.Now), GuestSharing = false, GuestMax = 16, BedroomNum = 8, BathroomNum = 3 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyTypeId = 1, LocationId = 2, StatusId = 3, Description = "A nice place", LastUpdated = Convert.ToDateTime(DateTime.Now), GuestSharing = true, GuestMax = 4, BedroomNum = 2, BathroomNum = 1 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 5, LocationId = 3, StatusId = 1, Description = "A nice area", LastUpdated = Convert.ToDateTime(DateTime.Now), GuestSharing = false, GuestMax = 8, BedroomNum = 4, BathroomNum = 2 }
            };

            foreach (var p in Properties)
            {
                _db.Properties.Add(p);
            }
            _db.SaveChanges();

            var Bookings = new List<Booking>
            {
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 2, Checkin = DateTime.Now.Date, Checkout = DateTime.Now.Date.AddDays(5), Tax = 16.23f, TotalPrice = 133.56f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 1, Checkin = DateTime.Now.Date, Checkout = DateTime.Now.Date.AddDays(10), Tax = 14.74f, TotalPrice = 56.17f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 3, Checkin = DateTime.Now.Date, Checkout = DateTime.Now.Date.AddDays(2), Tax = 25.98f, TotalPrice = 384.18f }

            };

            foreach (var b in Bookings)
            {
                _db.Bookings.Add(b);
            }
            _db.SaveChanges();

            var Discounts = new List<Discount>
            {
                new Discount { Value = 10.00f, Code = "10Off", Expiration = DateTime.Now.Date.AddDays(31) },
                new Discount { Value = 15.00f, Code = "NewUser", Expiration = DateTime.Now.Date.AddDays(10) },
                new Discount { Value = 20.00f, Code = "20forNewYears", Expiration = Convert.ToDateTime("1/10/2025") }
            };

            foreach (var d in Discounts)
            {
                _db.Discounts.Add(d);
            }
            _db.SaveChanges();

            var Fees = new List<Fee>
            {
                new Fee { Type = "Cleaning", Price = 150.00f },
                new Fee { Type = "Service", Price = 50.00f },
                new Fee { Type = "Parking", Price = 25.00f }
            };

            foreach (var f in Fees)
            {
                _db.Fees.Add(f);
            }
            _db.SaveChanges();

            var Images = new List<Image>
            {
                new Image { PropertyId = 1, Url = "https://www.martinhomemanagement.com/images/blog/martin%20property%20management_1.jpg", IsPrimary = true },
                new Image { PropertyId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUfpa6xo-6HwkTWkoA8wRXOseMC0UaMR6gnw&usqp=CAU", IsPrimary = false },
                new Image { PropertyId = 2, Url = "https://upload.wikimedia.org/wikipedia/commons/2/26/NRHP_Contributing_property_-Pollack-Krasner_house_11.jpg", IsPrimary = true },
                new Image { PropertyId = 2, Url = "https://nh.rdcpix.com/91788798c5a894ce12af0654fa248b98e-f1910218106od-w480_h360.jpg", IsPrimary = false },
                new Image { PropertyId = 3, Url = "https://www.apmutah.com/user/pages/06.areas-we-serve/brigham-city-property-management/brigham%20city%20house.jpeg", IsPrimary = true },
                new Image { PropertyId = 3, Url = "https://www.beycome.com/blog/wp-content/uploads/2023/11/Prepare-Your-Property-For-Sale-by-owner.jpg", IsPrimary = false }
            };

            foreach (var i in Images)
            {
                _db.Images.Add(i);
            }
            _db.SaveChanges();

            var PriceRanges = new List<PriceRange>
            {
                new PriceRange { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(10) },
                new PriceRange { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(20) },
                new PriceRange { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(25) },
            };

            foreach (var p in PriceRanges)
            {
                _db.PriceRanges.Add(p);
            }
            _db.SaveChanges();

            var PropertyAmenities = new List<PropertyAmenity>
            {
                new PropertyAmenity { PropertyId = 1, AmenityId = 1 },
                new PropertyAmenity { PropertyId = 1, AmenityId = 2 },
                new PropertyAmenity { PropertyId = 2, AmenityId = 5 },
                new PropertyAmenity { PropertyId = 2, AmenityId = 4 },
                new PropertyAmenity { PropertyId = 3, AmenityId = 6 },
                new PropertyAmenity { PropertyId = 3, AmenityId = 3 }
            };

            foreach (var p in PropertyAmenities)
            {
                _db.PropertyAmenities.Add(p);
            }
            _db.SaveChanges();

            var PropertyBedConfigurations = new List<PropertyBedConfiguration>
            {
                new PropertyBedConfiguration { PropertyId = 1, BedConfigurationId = 3 },
                new PropertyBedConfiguration { PropertyId = 1, BedConfigurationId = 2 },
                new PropertyBedConfiguration { PropertyId = 2, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 2, BedConfigurationId = 3 },
                new PropertyBedConfiguration { PropertyId = 3, BedConfigurationId = 3 },
                new PropertyBedConfiguration { PropertyId = 3, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 3, BedConfigurationId = 3 },
                new PropertyBedConfiguration { PropertyId = 3, BedConfigurationId = 2 }
            };

            foreach (var p in PropertyBedConfigurations)
            {
                _db.PropertyBedConfigurations.Add(p);
            }
            _db.SaveChanges();

            var PropertyDiscounts = new List<PropertyDiscount>
            {
                new PropertyDiscount { PropertyId = 1, DiscountId = 3 },
                new PropertyDiscount { PropertyId = 1, DiscountId = 2 },
                new PropertyDiscount { PropertyId = 3, DiscountId = 1 }
            };

            foreach (var p in PropertyDiscounts)
            {
                _db.PropertyDiscounts.Add(p);
            }
            _db.SaveChanges();

            var PropertyFees = new List<PropertyFee>
            {
                new PropertyFee { PropertyId = 1, FeeId = 3 },
                new PropertyFee { PropertyId = 2, FeeId = 3 },
                new PropertyFee { PropertyId = 2, FeeId = 2 },
                new PropertyFee { PropertyId = 3, FeeId = 1 }
            };

            foreach (var p in PropertyFees)
            {
                _db.PropertyFees.Add(p);
            }
            _db.SaveChanges();

            var PropertyNightlyPrices = new List<PropertyNightlyPrice>
            {
                new PropertyNightlyPrice { PropertyId = 1, PriceRangeId = 1, Rate = 23.29f },
                new PropertyNightlyPrice { PropertyId = 2, PriceRangeId = 2, Rate = 103.64f },
                new PropertyNightlyPrice { PropertyId = 3, PriceRangeId = 3, Rate = 45.50f }
            };

            foreach (var p in PropertyNightlyPrices)
            {
                _db.PropertyNightlyPrices.Add(p);
            }
            _db.SaveChanges();

            var Reviews = new List<Review>
            {
                new Review { BookingId = 1, Rating = 7, Comment = "Good experience", Timestamp = DateTime.Now, ReviewType = 0 },
                new Review { BookingId = 1, Rating = 10, Comment = "Great guests", Timestamp = DateTime.Now.AddHours(-2), ReviewType = 1},
                new Review { BookingId = 3, Rating = 2, Comment = "Rats", Timestamp = DateTime.Now.AddMonths(-3), ReviewType = 0 }
            };

            foreach (var r in Reviews)
            {
                _db.Reviews.Add(r);
            }
            _db.SaveChanges();
        }

    }
}

