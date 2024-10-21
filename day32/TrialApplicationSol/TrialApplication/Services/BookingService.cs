using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrialApplication.Exceptions;
using TrialApplication.Interfaces;
using TrialApplication.Models;
using TrialApplication.Models.DTO;

namespace TrialApplication.Services
{
    public class BookingService : IBookingService
    {

        private readonly IRepository<int, Booking> _bookingRepo;
        private readonly IRepository<int, Employee> _employeeRepo;
        private readonly IRepository<int, Event> _eventRepo;
        private readonly IMapper _mapper;

        public BookingService(IRepository<int, Booking> bookingRepo, IRepository<int, Employee> employeeRepo, IRepository<int, Event> eventRepo, IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            _employeeRepo = employeeRepo;
            _eventRepo = eventRepo;
            _mapper = mapper;
        }

        public async Task<int> BookEvent(BookingDTO bookingDto)
        {
            var employee = await _employeeRepo.Get(bookingDto.EmployeeId);
            if (employee == null)
            {
                throw new NotFoundException("Employee not found.");
            }

         
            var eventObj = await _eventRepo.Get(bookingDto.EventId);
            if (eventObj == null)
            {
                throw new NotFoundException("Event not found.");
            }

            
            bookingDto.BookingDate = DateTime.Now;

            
            var booking = _mapper.Map<Booking>(bookingDto);

            
            var bookingId = await _bookingRepo.Add(booking);

           
            return bookingId.BookingId; // Return the booking ID after successful registration

        }
       
        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            try
            {
                var emp = await _bookingRepo.GetAll();
                return emp;
            }
            catch (CollectionEmptyException)
            {
                throw new CollectionEmptyException("Bookings");
            }
        }
    }
}
