using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using TrialApplication.Context;
using TrialApplication.Exceptions;
using TrialApplication.Interfaces;
using TrialApplication.Models;

namespace TrialApplication.Repositories
{
    public class BookingRepository : IRepository<int, Booking>
    {

        private readonly EventBookingContext _context;

        public BookingRepository(EventBookingContext eventbookingContext)
        {
            _context = eventbookingContext;
        }



       

        public async Task<Booking> Add(Booking entity)
        {
            try
            {
                _context.Bookings.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new CouldNotAddException("Bookings");
            }
        }

        public Task<Booking> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            var bookings = await _context.Bookings.ToListAsync();
            if (bookings.Count == 0)
            {
                throw new CollectionEmptyException("Employee");
            }
            return bookings;
        }

        public Task<Booking> Update(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
