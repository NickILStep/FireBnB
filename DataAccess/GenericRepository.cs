
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly ApplicationDbContext _dbContext;
        //comments weren't given and I don't want to type them out
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);

        }

        public virtual T Get(Expression<Func<T, bool>> predicate, bool trackChanges = false, string? includes = null)
        {
            if (includes == null) //we are not joining other objects
            {
                if (!trackChanges) //is false
                {
                    return _dbContext.Set<T>()
                        .Where(predicate)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
                else //we are tracking changes (which EF does by default)
                {
                    return _dbContext.Set<T>()
                        .Where(predicate)
                        .FirstOrDefault();
                }
            }

            else //we have includes to deal with
            {
                //includes = "Comma,Separate,Objects,Without,Spaces"
                IQueryable<T> queryable = _dbContext.Set<T>();
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }

                if (!trackChanges) //is false
                {
                    return queryable
                        .Where(predicate)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
                else //we are tracking changes (which EF does by default)
                {
                    return queryable
                        .Where(predicate)
                        .FirstOrDefault();
                }
            }
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, int>>? orderBy = null, string? includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            if (predicate != null && includes == null)
            {
                return _dbContext.Set<T>()
                    .Where(predicate)
                    .AsEnumerable();
            }

            //has includes
            else if (includes != null)
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queryable.AsEnumerable();
                }

                else
                {
                    return queryable.OrderBy(orderBy).ToList();
                }
            }
            else
            {
                if (orderBy == null)
                {
                    return queryable
                        .Where(predicate)
                        .ToList();
                }
                else
                {
                    return queryable
                        .Where(predicate)
                        .OrderBy(orderBy).ToList();
                }
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, int>>? orderBy = null, string? includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            if (predicate != null && includes == null)
            {
                return _dbContext.Set<T>()
                    .Where(predicate)
                    .AsEnumerable();
            }

            //has includes
            else if (includes != null)
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queryable.AsEnumerable();
                }

                else
                {
                    return await queryable.OrderBy(orderBy).ToListAsync();
                }
            }
            else
            {
                if (orderBy == null)
                {
                    return await queryable
                        .Where(predicate)
                        .ToListAsync();
                }
                else
                {
                    return await queryable
                        .Where(predicate)
                        .OrderBy(orderBy).ToListAsync();
                }
            }
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, string? includes = null)
        {
            if (includes == null) //we are not joining other objects
            {
                if (!trackChanges) //is false
                {
                    return await _dbContext.Set<T>()
                        .Where(predicate)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
                }
                else //we are tracking changes (which EF does by default)
                {
                    return await _dbContext.Set<T>()
                        .Where(predicate)
                        .FirstOrDefaultAsync();
                }
            }

            else //we have includes to deal with
            {
                //includes = "Comma,Separate,Objects,Without,Spaces"
                IQueryable<T> queryable = _dbContext.Set<T>();
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }

                if (!trackChanges) //is false
                {
                    return queryable
                        .Where(predicate)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
                else //we are tracking changes (which EF does by default)
                {
                    return queryable
                        .Where(predicate)
                        .FirstOrDefault();
                }
            }
        }

        // Increment and Decrement Shopping Cart
        

        //virtual used to modify method, property, indexer, and allows for it to be overridden
        public virtual T GetById(int? id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public ApplicationUser GetById(string id)
        {
            return _dbContext.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        }
        public IEnumerable<Amenity> GetAmenitiesByPropertyId(int propertyId)
        {
            return _dbContext.PropertyAmenities
                           .Where(pa => pa.PropertyId == propertyId)
                           .Select(pa => pa.Amenity)
                           .ToList();
        }

        public DateTime? GetEarliestCheckinDate(int propertyId)
        {
            return _dbContext.Bookings
                           .Where(b => b.PropertyId == propertyId)
                           .OrderBy(b => b.Checkin)
                           .Select(b => b.Checkin)
                           .FirstOrDefault();
        }



        public IEnumerable<Property> SearchProperties(
    string searchQuery,
    DateTime? checkIn,
    DateTime? checkOut,
    int? guestNumber,
    decimal? costPerNight,
    List<int>? selectedAmenities,
    int? bedroomCount,
    int? bathroomCount,
    int? selectedCityId,
    int? selectedCountyId,
    int? selectedStateId)
        {
            // Start with querying all properties
            IQueryable<Property> query = _dbContext.Properties;

            // Filter by search query (if provided)
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.City.CityName.Contains(searchQuery)
                                      || p.County.CountyName.Contains(searchQuery)
                                      || p.State.StateName.Contains(searchQuery)
                                      || p.Address.Contains(searchQuery)
                                      || p.Zipcode.Contains(searchQuery));
            }

            // Filter by check-in date and check-out date (if provided)
            if (checkIn.HasValue && checkOut.HasValue)
            {
                // Get the list of property IDs that have bookings overlapping with the specified dates
                var bookedPropertyIds = _dbContext.Bookings
                    .Where(b =>
                        (b.Checkin < checkOut && b.Checkout > checkIn)
                        || (b.Checkin >= checkIn && b.Checkin < checkOut)
                        || (b.Checkout > checkIn && b.Checkout <= checkOut))
                    .Select(b => b.PropertyId)
                    .Distinct();

                // Filter out properties that have bookings overlapping with the specified dates
                query = query.Where(p => !bookedPropertyIds.Contains(p.Id));
            }

            // Filter by check-in date (if provided)
            if (checkIn.HasValue)
            {
                // Get the list of property IDs that have bookings overlapping with the selected check-in date
                var bookedPropertyIds = _dbContext.Bookings
                    .Where(b => b.Checkin <= checkIn.Value && b.Checkout > checkIn.Value)
                    .Select(b => b.PropertyId)
                    .Distinct();

                // Filter out properties that have bookings overlapping with the selected check-in date
                query = query.Where(p => !bookedPropertyIds.Contains(p.Id));
            }

            // Adjust the check-out date range based on the selected check-in date
            if (checkIn.HasValue)
            {
                // Calculate the maximum check-out date based on the selected check-in date
                DateTime maxCheckOutDate = checkIn.Value.AddDays(7); // Maximum 7 days stay
                if (checkOut.HasValue && checkOut.Value > maxCheckOutDate)
                {
                    // If the selected check-out date exceeds the maximum, adjust it
                    checkOut = maxCheckOutDate;
                }
            }

            // Filter by check-out date (if provided)
            if (checkOut.HasValue)
            {
                // Get the list of property IDs that have bookings overlapping with the selected check-out date
                var bookedPropertyIds = _dbContext.Bookings
                    .Where(b => b.Checkin < checkOut.Value && b.Checkout >= checkOut.Value)
                    .Select(b => b.PropertyId)
                    .Distinct();

                // Filter out properties that have bookings overlapping with the selected check-out date
                query = query.Where(p => !bookedPropertyIds.Contains(p.Id));
            }

            // Filter by guest count (if provided)
            if (guestNumber.HasValue)
            {
                query = query.Where(p => p.GuestMax >= guestNumber);
            }

            // Filter by cost per night (if provided)
            if (costPerNight.HasValue)
            {
                // Define a range within which the property prices should fall
                decimal minPrice = costPerNight.Value - 10; // Adjust the range as needed
                decimal maxPrice = costPerNight.Value + 10; // Adjust the range as needed

                // Join PropertyNightlyPrice to get the nightly rates
                query = query.Join(_dbContext.PropertyNightlyPrices,
                                    p => p.Id,
                                    pnp => pnp.PropertyId,
                                    (p, pnp) => new { Property = p, Price = pnp })
                             .Where(x => x.Price.Rate >= (float)minPrice && x.Price.Rate <= (float)maxPrice)
                             .Select(x => x.Property)
                             .Distinct();
            }

            // Filter by selected amenities
            if (selectedAmenities != null && selectedAmenities.Any())
            {
                // Join Property and PropertyAmenity to filter properties based on amenities
                query = query.Join(_dbContext.PropertyAmenities,
                                    p => p.Id,
                                    pa => pa.PropertyId,
                                    (p, pa) => new { Property = p, PropertyAmenity = pa })
                             .Where(x => selectedAmenities.Contains(x.PropertyAmenity.AmenityId))
                             .Select(x => x.Property)
                             .Distinct();
            }

            // Filter by bedroom count (if provided)
            if (bedroomCount.HasValue)
            {
                query = query.Where(p => p.BedroomNum >= bedroomCount);
            }

            // Filter by bathroom count (if provided)
            if (bathroomCount.HasValue)
            {
                query = query.Where(p => p.BathroomNum >= bathroomCount);
            }

            // Filter by selected city, county, and state IDs (if provided)
            if (selectedCityId.HasValue)
            {
                query = query.Where(p => p.CityId == selectedCityId);
            }

            if (selectedCountyId.HasValue)
            {
                query = query.Where(p => p.CountyId == selectedCountyId);
            }

            if (selectedStateId.HasValue)
            {
                query = query.Where(p => p.StateId == selectedStateId);
            }

            // Execute the query and return the results
            return query.ToList();
        }

        public void Update(T entity)
        {
            //for track changes I'm flagging modified to the system
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        IEnumerable<Property> IGenericRepository<T>.GetAllWithLocationsCitiesCountiesStates()
        {
            return _dbContext.Properties
                .Include(p => p.City)
                .Include(p => p.County)
                .Include(p => p.State);
        }
    }
}