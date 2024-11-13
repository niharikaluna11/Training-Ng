﻿using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ComplaintTicketContext _context;
        private readonly ILogger<UserProfileService> _logger;
        private readonly IEmailSender _emailSender;


      
        public UserProfileService(ComplaintTicketContext context,
             IEmailSender emailSender,
              ILogger<UserProfileService> logger)
        {

            _context = context;
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task<UserProfile> GetProfile(int userId)
        {
            // Retrieve the profile from the database by userId
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            return profile; // Return the found profile
        }

        private void SendMail(string title, string email, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        email },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }
        public async Task<UserProfile> UpdateProfile(int userId, ProfileUpdateDTO updateDto)
        {
            
            try
            {
                var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
                if (profile != null)
                {


                    // Update profile properties
                    profile.FirstName = updateDto.FirstName;
                    profile.LastName = updateDto.LastName;
                    profile.Address = updateDto.Address;
                    profile.DateOfBirth = updateDto.DateOfBirth;
                    profile.Email = updateDto.Email;
                    profile.Phone = updateDto.Phone;
                    profile.Preferences = updateDto.Preferences;

                    try
                    {
                        string body = $"Dear {profile.FirstName},\n\n" +
                                 "We are pleased to inform you that your account profile has been successfully updated.\n\n" +
                                 "If you have any questions or need further assistance, please do not hesitate to contact us.\n\n" +
                               "Best regards,\n" +
                                     "ComplaintTicketApp Team";

                        string email = profile.Email;
                        SendMail("Your Account Has Been Created", email, body);
                    }
                    catch { throw new Exception("Mail Cannot be send Becuase of Invalid Mail"); }

                    await _context.SaveChangesAsync();
                }
                return profile; // Return the updated profile
            }
               catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user profile for user: {UserId}", userId);
                throw new Exception("Failed to create user profile");
            }
        }
    }
}
