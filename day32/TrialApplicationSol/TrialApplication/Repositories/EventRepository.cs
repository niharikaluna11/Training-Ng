using Microsoft.EntityFrameworkCore;
using TrialApplication.Context;
using TrialApplication.Exceptions;
using TrialApplication.Interfaces;
using TrialApplication.Models;

namespace TrialApplication.Repositories
{
    public class EventRepository : IRepository<int, Event>
    {
        private readonly EventBookingContext _context;

        public EventRepository(EventBookingContext eventbookingContext)
        {
            _context = eventbookingContext;
        }

        public async Task<Event> Add(Event entity)
        {
            try
            {
                _context.Events.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new CouldNotAddException("Event");
            }
        }

        public async Task<Event> Get(int key)
        {
            var eventd = _context.Events.FirstOrDefault(c => c.EventId == key);
            return eventd;
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            var eventd = await _context.Events.ToListAsync();
            if (eventd.Count == 0)
            {
                throw new CollectionEmptyException("event");
            }
            return eventd;
            
        }


      

        public Task<Event> Delete(int key)
        {
            throw new NotImplementedException();
        }

     

        public Task<Event> Update(Event entity)
        {
            throw new NotImplementedException();
        }
    }
    }
