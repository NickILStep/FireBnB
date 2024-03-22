
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

        public IEnumerable<Property> SearchProperties(string searchQuery, DateTime? checkIn, DateTime? checkOut, int? guestNumber)
        {
            // Start with querying all properties
            IQueryable<Property> query = _dbContext.Properties;

            // Include related entities to avoid lazy loading
            query = query.Include(p => p.Location);

            // Filter by search query (if provided)
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.Location.City.CityName.Contains(searchQuery)
                         || p.Location.County.CountyName.Contains(searchQuery)
                         || p.Location.State.StateName.Contains(searchQuery)
                         || p.Location.Address.Contains(searchQuery)
                         || p.Location.Zipcode.Contains(searchQuery));
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

            // Filter by guest count (if provided)
            if (guestNumber.HasValue)
            {
                query = query.Where(p => p.GuestMax >= guestNumber);
            }

            // Execute the query and return the results
            return query.ToList();
        }

        public void Update(T entity)
        {
            //for track changes I'm flagging modified to the system
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}