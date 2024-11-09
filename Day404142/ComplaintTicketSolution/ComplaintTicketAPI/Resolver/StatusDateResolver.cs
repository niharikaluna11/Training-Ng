using AutoMapper;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using System;
using System.Linq;

public class StatusDateResolver : IValueResolver<ComplaintStatus, ComplaintStatusDTO, DateTime>
{
    public DateTime Resolve(ComplaintStatus source, ComplaintStatusDTO destination, DateTime destMember, ResolutionContext context)
    {
        return source.ComplaintStatusDates?
                   .OrderByDescending(cs => cs.StatusDate)
                   .FirstOrDefault()?.StatusDate ?? DateTime.MinValue;
    }
}
