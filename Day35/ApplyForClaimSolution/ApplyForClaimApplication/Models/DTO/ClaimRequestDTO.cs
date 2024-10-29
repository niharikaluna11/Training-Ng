using System.ComponentModel.DataAnnotations;

namespace ApplyForClaimApplication.Models.DTO
{
    public class ClaimRequestDTO
    {
     
        
        [Required(ErrorMessage ="Please Enter Name")]
        public string ClaimantName { get; set; }      // Name of the person filing the claim

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string ClaimantPhone { get; set; }     // Phone number of the claimant

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
        public string ClaimantEmail { get; set; }     // Email of the claimant

        public DateTime DateOfIncident { get; set; } = DateTime.UtcNow; // Date when the incident occurred

    }
}
