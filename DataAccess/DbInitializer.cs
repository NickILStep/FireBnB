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
                new Amenity { AmenityName = "Hot Tub" },
                new Amenity { AmenityName = "Home Theater" },
                new Amenity { AmenityName = "Billiards Table" },
                new Amenity { AmenityName = "Trampoline" },
                new Amenity { AmenityName = "Dishwasher" },
                new Amenity { AmenityName = "Washer and Dryer" },
                new Amenity { AmenityName = "Wifi" },
                new Amenity { AmenityName = "Full Kitchen" }
            };

            foreach (var a in Amenities)
            {
                _db.Amenities.Add(a);
            }
            _db.SaveChanges();

            var ApplicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Tom", LastName = "Riddle", DisplayName = "Voldemort", Birthdate = Convert.ToDateTime("12/31/1926"), SignupDate = Convert.ToDateTime("9/1/1938"), UserName = "tomriddle@mail.com", NormalizedUserName = "TOMRIDDLE@MAIL.COM", Email = "tomriddle@mail.com", NormalizedEmail = "TOMRIDDLE@MAIL.COM", EmailConfirmed = true, PhoneNumber = "801-555-1234", PhoneNumberConfirmed = false, ProfilePictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/aa/Sin_cara.png"},
                new ApplicationUser { FirstName = "Harry", LastName = "Potter", DisplayName = "The Boy Who Lived", Birthdate = Convert.ToDateTime("7/31/1980"), SignupDate = Convert.ToDateTime("9/1/1991"), UserName = "hpotter@mail.com", NormalizedUserName = "HPOTTER@MAIL.COM", Email = "hpotter@mail.com", NormalizedEmail = "HPOTTER@MAIL.COM", EmailConfirmed = true, PhoneNumber = "801-555-2345", PhoneNumberConfirmed = false, ProfilePictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/aa/Sin_cara.png"},
                new ApplicationUser { FirstName = "Hermione", LastName = "Granger", DisplayName = "It's LeviOsa, not levioSA!", Birthdate = Convert.ToDateTime("9/19/1979"), SignupDate = Convert.ToDateTime("9/1/1991"), UserName = "hermioneg@mail.com", NormalizedUserName = "HERMIONEG@MAIL.COM", Email = "hermioneg@mail.com", NormalizedEmail = "HERMIONEG@MAIL.COM", EmailConfirmed = true, PhoneNumber = "801-555-3456", PhoneNumberConfirmed = false, ProfilePictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/aa/Sin_cara.png"},
                new ApplicationUser { FirstName = "Ron", LastName = "Weasley", DisplayName = "Grand Theft Auto", Birthdate = Convert.ToDateTime("3/1/1980"), SignupDate = Convert.ToDateTime("9/1/1991"), UserName = "ronweasley@mail.com", NormalizedUserName = "RONWEASLEY@MAIL.COM", Email = "ronweasley@mail.com", NormalizedEmail = "RONWEASLEY@MAIL.COM", EmailConfirmed = true, PhoneNumber = "801-555-4567", PhoneNumberConfirmed = false, ProfilePictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/aa/Sin_cara.png"}
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
                new PropertyType { Title = "Mansion" },
                new PropertyType { Title = "Igloo" }
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
                new City { CityName = "Salt Lake City", TaxRate = 7.75f },
                new City { CityName = "Brigham City", TaxRate = 3.21f },
                new City { CityName = "Lehi", TaxRate = 7.26f },
                new City { CityName = "Nephi", TaxRate = 4.63f },
                new City { CityName = "Denver", TaxRate = 6.27f },
                new City { CityName = "San Francisco", TaxRate = 8.14f },
                new City { CityName = "Los Angeles", TaxRate = 6.18f },
                new City { CityName = "Hollywood", TaxRate = 3.52f },
                new City { CityName = "San Diego", TaxRate = 6.42f },
                new City { CityName = "Denver", TaxRate = 1.37f },
                new City { CityName = "Boulder", TaxRate = 2.43f},
                new City { CityName = "Colorado Springs", TaxRate = 9.42f},
                new City { CityName = "Aurora", TaxRate = 6.42f},
                new City { CityName = "Fort Collins", TaxRate = 8.34f},
                new City { CityName = "Leavenworth", TaxRate = 6.48f },
                new City { CityName = "Seattle", TaxRate = 5.32f},
                new City { CityName = "Spokane", TaxRate = 2.45f},
                new City { CityName = "Bellvue", TaxRate = 9.42f}
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
                new County { CountyName = "Weber", TaxRate = 0.2f },
                new County { CountyName = "Juab", TaxRate = 0.32f },
                new County { CountyName = "Utah", TaxRate = 0.12f },
                new County { CountyName = "Iron", TaxRate = 0.23f },
                new County { CountyName = "San Juan", TaxRate = 0.14f },
                new County { CountyName = "Los Angeles", TaxRate = 0.2f },
                new County { CountyName = "San Diego", TaxRate = 0.42f },
                new County { CountyName = "Santa Ana", TaxRate = 0.36f },
                new County { CountyName = "Orange", TaxRate = 0.12f },
                new County { CountyName = "San Bernardino", TaxRate = 0.15f },
                new County { CountyName = "King", TaxRate = 0.2f },
                new County { CountyName = "Pierce", TaxRate = 0.23f },
                new County { CountyName = "Snohomish", TaxRate = 0.24f },
                new County { CountyName = "Spokane", TaxRate = 0.14f },
                new County { CountyName = "Clark", TaxRate = 0.26f },
                new County { CountyName = "Thurston", TaxRate = 0.32f },
                new County { CountyName = "El Paso", TaxRate = 0.14f },
                new County { CountyName = "Denver", TaxRate = 0.26f },
                new County { CountyName = "Arapahoe", TaxRate = 0.3f },
                new County { CountyName = "Jefferson", TaxRate = 0.41f },
                new County { CountyName = "Adams", TaxRate = 0.24f },
                new County { CountyName = "Douglas", TaxRate = 0.28f },
                new County { CountyName = "Larimer", TaxRate = 0.17f }
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
                new State { StateName = "Washington", TaxRate = 6.5f },
                new State { StateName = "Missouri", TaxRate = 4.21f },
                new State { StateName = "Maine", TaxRate = 3.51f },
                new State { StateName = "North Dakota", TaxRate = 1.86f },
                new State { StateName = "Hawaii", TaxRate = 6.21f },
                new State { StateName = "Texas", TaxRate = 5.34f },
                new State { StateName = "Idaho", TaxRate = 1.42f },
                new State { StateName = "Oregon", TaxRate = 7.21f },
                new State { StateName = "Florida", TaxRate = 3.52f },
                new State { StateName = "Kansas", TaxRate = 3.18f },
                new State { StateName = "Wyoming", TaxRate = 1.43f },
                new State { StateName = "Georgia", TaxRate = 9.42f },
                new State { StateName = "Maryland", TaxRate = 4.32f },
                new State { StateName = "New York", TaxRate = 3.16f },
                new State { StateName = "Rhode Island", TaxRate = 2.72f },
                new State { StateName = "New Mexico", TaxRate = 2.64f }
            };

            foreach (var s in States)
            {
                _db.States.Add(s);
            }
            _db.SaveChanges();

            //var Locations = new List<Location>
            //{
            //    new Location { CityId = 1, CountyId = 2, StateId = 4, Address = "123 Somewhere St.", Zipcode = "84321"},
            //    new Location { CityId = 3, CountyId = 3, StateId = 2, Address = "456 Anywhere Blvd.", Zipcode = "84654"},
            //    new Location { CityId = 2, CountyId = 1, StateId = 1, Address = "789 Nowhere Ave.", Zipcode = "84987"}
            //};

            //foreach (var l in Locations)
            //{
            //    _db.Locations.Add(l);
            //}
            //_db.SaveChanges();

            var Statuses = new List<Status>
            {
                new Status { StatusName = "Available" },
                new Status { StatusName = "Hidden" },
                new Status { StatusName = "Partially Available" },
                new Status { StatusName = "Suspended" },
                new Status { StatusName = "Owner Use" }
            };

            foreach (var s in Statuses)
            {
                _db.Statuses.Add(s);
            }
            _db.SaveChanges();

            var Properties = new List<Property>
            {
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 2, CityId = 1, CountyId = 2, StateId = 4, Address = "123 Somewhere St.", Zipcode = "84321", StatusId = 2, Title = "Dingus", Description = "A nice home", LastUpdated = DateTime.Now.AddDays(0), GuestSharing = false, GuestMax = 16, BedroomNum = 8, BathroomNum = 3 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyTypeId = 1, CityId = 3, CountyId = 3, StateId = 2, Address = "456 Anywhere Blvd.", Zipcode = "84654", StatusId = 3, Title = "Chungus", Description = "A nice place", LastUpdated = DateTime.Now.AddDays(-10), GuestSharing = true, GuestMax = 4, BedroomNum = 2, BathroomNum = 1 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 5, CityId = 7, CountyId = 1, StateId = 5, Address = "789 Nowhere Ave.", Zipcode = "84987", StatusId = 1, Title = "Larry's Lair", Description = "A nice area", LastUpdated = DateTime.Now.AddDays(2), GuestSharing = false, GuestMax = 8, BedroomNum = 4, BathroomNum = 2 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 4, CityId = 13, CountyId = 25, StateId = 10, Address = "156 YouKnowIt Pl.", Zipcode = "48915", StatusId = 1, Title = "Fun House", Description = "Come have a good time!", LastUpdated = DateTime.Now.AddDays(-1), GuestSharing = false, GuestMax = 8, BedroomNum = 4, BathroomNum = 1 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyTypeId = 2, CityId = 5, CountyId = 18, StateId = 1, Address = "76 RightHere Rd.", Zipcode = "86745", StatusId = 4, Title = "Island Paradise", Description = "A small private island just for you!", LastUpdated = DateTime.Now.AddDays(-200), GuestSharing = false, GuestMax = 2, BedroomNum = 1, BathroomNum = 1 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyTypeId = 7, CityId = 6, CountyId = 3, StateId = 3, Address = "1 WhereIt'sAt St.", Zipcode = "32158", StatusId = 1, Title = "Great View", Description = "Surrounded by beautiful scenery", LastUpdated = DateTime.Now.AddDays(3), GuestSharing = true, GuestMax = 32, BedroomNum = 8, BathroomNum = 3 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 4, CityId = 12, CountyId = 9, StateId = 4, Address = "12 HomeTown Ave.", Zipcode = "15645", StatusId = 1, Title = "Mountain Property", Description = "Perfect for skiing trips!", LastUpdated = DateTime.Now.AddDays(-150), GuestSharing = true, GuestMax = 8, BedroomNum = 4, BathroomNum = 3 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyTypeId = 6, CityId = 20, CountyId = 14, StateId = 4, Address = "862 Street Rd.", Zipcode = "89415", StatusId = 1, Title = "Beautiful Sunsets", Description = "A great place to sit and watch the sunset", LastUpdated = DateTime.Now.AddDays(7), GuestSharing = false, GuestMax = 4, BedroomNum = 1, BathroomNum = 1 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 1, CityId = 8, CountyId = 21, StateId = 6, Address = "321 Heere Cir.", Zipcode = "12348", StatusId = 2, Title = "Book This One Please :`(", Description = "I'm broke :`(", LastUpdated = DateTime.Now.AddDays(-30), GuestSharing = false, GuestMax = 8, BedroomNum = 6, BathroomNum = 3 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyTypeId = 2, CityId = 10, CountyId = 24, StateId = 16, Address = "1600 Pennsylvania Av.", Zipcode = "20500", StatusId = 1, Title = "Perfect Family Getaway", Description = "Great for larger groups!", LastUpdated = DateTime.Now.AddDays(10), GuestSharing = true, GuestMax = 50, BedroomNum = 25, BathroomNum = 10 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyTypeId = 1, CityId = 9, CountyId = 16, StateId = 14, Address = "987 Place Pl.", Zipcode = "74135", StatusId = 1, Title = "Great Wedding Spot", Description = "Perfect for medium wedding ceremonies on the beach", LastUpdated = DateTime.Now.AddDays(5), GuestSharing = false, GuestMax = 6, BedroomNum = 3, BathroomNum = 1 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 1, CityId = 2, CountyId = 7, StateId = 17, Address = "741 Long Ave.", Zipcode = "31588", StatusId = 1, Title = "Your Mom's House", Description = "Stocked with fresh cookies!", LastUpdated = DateTime.Now.AddDays(-7), GuestSharing = false, GuestMax = 2, BedroomNum = 1, BathroomNum = 0 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 3, CityId = 20, CountyId = 6, StateId = 3, Address = "852 West St.", Zipcode = "45321", StatusId = 3, Title = "Where Dreams Come True", Description = "It's just a room with a bed", LastUpdated = DateTime.Now.AddDays(-20), GuestSharing = true, GuestMax = 15, BedroomNum = 5, BathroomNum = 3 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyTypeId = 6, CityId = 2, CountyId = 19, StateId = 1, Address = "963 Mother Rd.", Zipcode = "98265", StatusId = 2, Title = "Fox Garden", Description = "Come play with some furry friends!", LastUpdated = DateTime.Now.AddDays(0), GuestSharing = true, GuestMax = 5, BedroomNum = 2, BathroomNum = 1 },
                new Property {ListerId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyTypeId = 5, CityId = 4, CountyId = 10, StateId = 19, Address = "951 Wall Ave.", Zipcode = "12783", StatusId = 1, Title = "World's Largest Potato", Description = "This is your chance to sleep INSIDE the world's largest potato! It has been hollowed out to make space for a living area. Don't miss this once in a lifetime experience!", LastUpdated = DateTime.Now.AddDays(-1500), GuestSharing = false, GuestMax = 1, BedroomNum = 1, BathroomNum = 0 }
            };

            foreach (var p in Properties)
            {
                _db.Properties.Add(p);
            }
            _db.SaveChanges();

            var Bookings = new List<Booking>
            {
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 2, Checkin = DateTime.Now.Date.AddDays(0), Checkout = DateTime.Now.Date.AddDays(5), Tax = 16.23f, TotalPrice = 133.56f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 1, Checkin = DateTime.Now.Date.AddDays(4), Checkout = DateTime.Now.Date.AddDays(10), Tax = 14.74f, TotalPrice = 56.17f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 3, Checkin = DateTime.Now.Date.AddDays(7), Checkout = DateTime.Now.Date.AddDays(10), Tax = 25.98f, TotalPrice = 384.18f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 13, Checkin = DateTime.Now.Date.AddDays(30), Checkout = DateTime.Now.Date.AddDays(33), Tax = 62.15f, TotalPrice = 5612.04f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 7, Checkin = DateTime.Now.Date.AddDays(-5), Checkout = DateTime.Now.Date.AddDays(0), Tax = 85.21f, TotalPrice = 231.25f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 12, Checkin = DateTime.Now.Date.AddDays(-30), Checkout = DateTime.Now.Date.AddDays(-26), Tax = 12.36f, TotalPrice = 84.12f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 8, Checkin = DateTime.Now.Date.AddDays(-15), Checkout = DateTime.Now.Date.AddDays(-11), Tax = 84.15f, TotalPrice = 163.20f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 14, Checkin = DateTime.Now.Date.AddDays(-3), Checkout = DateTime.Now.Date.AddDays(1), Tax = 95.21f, TotalPrice = 746.18f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 10, Checkin = DateTime.Now.Date.AddDays(4), Checkout = DateTime.Now.Date.AddDays(9), Tax = 48.21f, TotalPrice = 62.30f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 9, Checkin = DateTime.Now.Date.AddDays(1), Checkout = DateTime.Now.Date.AddDays(4), Tax = 5.81f, TotalPrice = 17.21f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 5, Checkin = DateTime.Now.Date.AddDays(10), Checkout = DateTime.Now.Date.AddDays(14), Tax = 17.23f, TotalPrice = 56.34f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 15, Checkin = DateTime.Now.Date.AddDays(-25), Checkout = DateTime.Now.Date.AddDays(-21), Tax = 15.84f, TotalPrice = 32.76f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 2, Checkin = DateTime.Now.Date.AddDays(6), Checkout = DateTime.Now.Date.AddDays(8), Tax = 14.43f, TotalPrice = 53.18f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).First().Id, PropertyId = 11, Checkin = DateTime.Now.Date.AddDays(2), Checkout = DateTime.Now.Date.AddDays(7), Tax = 71.32f, TotalPrice = 203.52f },
                new Booking { GuestId = _db.ApplicationUsers.OrderBy(user => user.SignupDate).Last().Id, PropertyId = 6, Checkin = DateTime.Now.Date.AddDays(0), Checkout = DateTime.Now.Date.AddDays(4), Tax = 25.81f, TotalPrice = 84.27f }

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
                new Discount { Value = 20.00f, Code = "20forNewYears", Expiration = Convert.ToDateTime("1/10/2025") },
                new Discount { Value = 5.00f, Code = "5ForYou", Expiration = DateTime.Now.Date.AddDays(5) }
            };

            foreach (var d in Discounts)
            {
                _db.Discounts.Add(d);
            }
            _db.SaveChanges();

            var Fees = new List<Fee>
            {
                new Fee { Type = "Cleaning", Percentage = 1.5f },
                new Fee { Type = "Service", Percentage = .05f },
                new Fee { Type = "Parking", Percentage = .01f }
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
                new Image { PropertyId = 3, Url = "https://www.beycome.com/blog/wp-content/uploads/2023/11/Prepare-Your-Property-For-Sale-by-owner.jpg", IsPrimary = false },
                new Image { PropertyId = 4, Url = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTA1L3B4MTE3OTM5My1pbWFnZS1rd3Z5MG94eC5qcGc.jpg", IsPrimary = true },
                new Image { PropertyId = 5, Url = "https://live.staticflickr.com/2277/2110457116_21e3589734_b.jpg", IsPrimary = true },
                new Image { PropertyId = 6, Url = "https://upload.wikimedia.org/wikipedia/commons/a/ad/A_house_with_modern_design_and_aesthetic%2C_which_taken_in_Mihu_on_18_September_2021.jpg", IsPrimary = true },
                new Image { PropertyId = 7, Url = "https://upload.wikimedia.org/wikipedia/commons/6/61/Lockhart_House_house.jpg", IsPrimary = true },
                new Image { PropertyId = 8, Url = "https://upload.wikimedia.org/wikipedia/commons/a/a9/Carton_House_IE20100403_007_maynooth.jpg", IsPrimary = true },
                new Image { PropertyId = 9, Url = "https://joshdotoligroup.com/wp-content/uploads/2023/02/How-To-Sell-Your-House-In-5-Days-825x510-1.jpg", IsPrimary = true },
                new Image { PropertyId = 10, Url = "https://www.aclu.org/wp-content/uploads/2019/09/web18_whitehouseprotest_1160x768.jpg", IsPrimary = true },
                new Image { PropertyId = 11, Url = "https://www.worldhistory.org/uploads/images/11015.jpg", IsPrimary = true },
                new Image { PropertyId = 12, Url = "https://images.pexels.com/photos/18273285/pexels-photo-18273285/free-photo-of-modern-house-building.jpeg", IsPrimary = true },
                new Image { PropertyId = 13, Url = "https://upload.wikimedia.org/wikipedia/commons/d/d6/Pope_House_%28Portland%2C_Oregon%29.jpg", IsPrimary = true },
                new Image { PropertyId = 14, Url = "https://upload.wikimedia.org/wikipedia/commons/9/94/Pickering_House%2C_October_2021.jpg", IsPrimary = true },
                new Image { PropertyId = 15, Url = "https://live.staticflickr.com/65535/53338328076_5520203fd3_b.jpg", IsPrimary = true }
            };

            foreach (var i in Images)
            {
                _db.Images.Add(i);
            }
            _db.SaveChanges();

            //var PriceRanges = new List<PriceRange>
            //{
            //    new PriceRange { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(10) },
            //    new PriceRange { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(20) },
            //    new PriceRange { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(25) },
            //};

            //foreach (var p in PriceRanges)
            //{
            //    _db.PriceRanges.Add(p);
            //}
            //_db.SaveChanges();

            var PropertyAmenities = new List<PropertyAmenity>
            {
                new PropertyAmenity { PropertyId = 1, AmenityId = 1 },
                new PropertyAmenity { PropertyId = 1, AmenityId = 2 },
                new PropertyAmenity { PropertyId = 2, AmenityId = 5 },
                new PropertyAmenity { PropertyId = 2, AmenityId = 4 },
                new PropertyAmenity { PropertyId = 3, AmenityId = 6 },
                new PropertyAmenity { PropertyId = 3, AmenityId = 9 },
                new PropertyAmenity { PropertyId = 4, AmenityId = 7 },
                new PropertyAmenity { PropertyId = 5, AmenityId = 2 },
                new PropertyAmenity { PropertyId = 6, AmenityId = 5 },
                new PropertyAmenity { PropertyId = 6, AmenityId = 9 },
                new PropertyAmenity { PropertyId = 7, AmenityId = 6 },
                new PropertyAmenity { PropertyId = 8, AmenityId = 8 },
                new PropertyAmenity { PropertyId = 9, AmenityId = 3 },
                new PropertyAmenity { PropertyId = 10, AmenityId = 2 },
                new PropertyAmenity { PropertyId = 11, AmenityId = 6 },
                new PropertyAmenity { PropertyId = 12, AmenityId = 1 },
                new PropertyAmenity { PropertyId = 13, AmenityId = 9 },
                new PropertyAmenity { PropertyId = 14, AmenityId = 4 },
                new PropertyAmenity { PropertyId = 15, AmenityId = 5 },
                new PropertyAmenity { PropertyId = 15, AmenityId = 2 }
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
                new PropertyBedConfiguration { PropertyId = 3, BedConfigurationId = 2 },
                new PropertyBedConfiguration { PropertyId = 4, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 5, BedConfigurationId = 3 },
                new PropertyBedConfiguration { PropertyId = 6, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 6, BedConfigurationId = 3 },
                new PropertyBedConfiguration { PropertyId = 7, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 8, BedConfigurationId = 2 },
                new PropertyBedConfiguration { PropertyId = 9, BedConfigurationId = 3 },
                new PropertyBedConfiguration { PropertyId = 10, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 11, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 12, BedConfigurationId = 3 },
                new PropertyBedConfiguration { PropertyId = 13, BedConfigurationId = 2 },
                new PropertyBedConfiguration { PropertyId = 13, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 14, BedConfigurationId = 1 },
                new PropertyBedConfiguration { PropertyId = 15, BedConfigurationId = 2 }
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
                new PropertyNightlyPrice { PropertyId = 1, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(10), Rate = 23.29f },
                new PropertyNightlyPrice { PropertyId = 2, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(20), Rate = 103.64f },
                new PropertyNightlyPrice { PropertyId = 3, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(25), Rate = 45.50f },
                new PropertyNightlyPrice { PropertyId = 3, StartDate = DateTime.Now.Date.AddDays(26), EndDate = DateTime.Now.Date.AddDays(50), Rate = 12.25f },
                new PropertyNightlyPrice { PropertyId = 4, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(12), Rate = 50.50f },
                new PropertyNightlyPrice { PropertyId = 5, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(7), Rate = 32.51f },
                new PropertyNightlyPrice { PropertyId = 6, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(10), Rate = 10.23f },
                new PropertyNightlyPrice { PropertyId = 7, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(17), Rate = 1001.10f },
                new PropertyNightlyPrice { PropertyId = 8, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(20), Rate = 502.30f },
                new PropertyNightlyPrice { PropertyId = 9, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(50), Rate = 45.23f },
                new PropertyNightlyPrice { PropertyId = 10, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(11), Rate = 94.12f },
                new PropertyNightlyPrice { PropertyId = 11, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(13), Rate = 63.52f },
                new PropertyNightlyPrice { PropertyId = 12, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(80), Rate = 14.23f },
                new PropertyNightlyPrice { PropertyId = 13, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(120), Rate = 71.32f },
                new PropertyNightlyPrice { PropertyId = 14, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(45), Rate = 15.00f },
                new PropertyNightlyPrice { PropertyId = 15, StartDate = DateTime.Now.Date.AddDays(0), EndDate = DateTime.Now.Date.AddDays(10), Rate = 500.01f }
            };

            foreach (var p in PropertyNightlyPrices)
            {
                _db.PropertyNightlyPrices.Add(p);
            }
            _db.SaveChanges();

            var Reviews = new List<Review>
            {
                new Review { BookingId = 1, Rating = 7, Comment = "Good experience", Timestamp = DateTime.Now, ReviewType = true },
                new Review { BookingId = 1, Rating = 10, Comment = "Great guests", Timestamp = DateTime.Now.AddHours(-2), ReviewType = false },
                new Review { BookingId = 3, Rating = 2, Comment = "Rats", Timestamp = DateTime.Now.AddMonths(-3), ReviewType = true }
            };

            foreach (var r in Reviews)
            {
                _db.Reviews.Add(r);
            }
            _db.SaveChanges();
        }

    }
}

