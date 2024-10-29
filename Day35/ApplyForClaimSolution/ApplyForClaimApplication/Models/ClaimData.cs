using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ApplyForClaimApplication.Models
{
    public class ClaimData
    {

        public int ClaimId { get; set; }              // Unique identifier for the clai
        public DateTime DateOfIncident { get; set; }  // Date when the incident occurred

        // [Required(ErrorMessage ="Please Enter Name")]

        public int ClaimType { get; set; } //claim type id
        public string ClaimantName { get; set; }      // Name of the person filing the claim

        public string ClaimantPhone { get; set; }     // Phone number of the claimant
        public string ClaimantEmail { get; set; }     // Email of the claimant
        public List<DocumentData> Documents { get; set; } // List of uploaded documents related to the claim

    }
}
