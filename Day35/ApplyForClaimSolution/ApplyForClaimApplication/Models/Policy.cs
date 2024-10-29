using System.ComponentModel.DataAnnotations;

namespace ApplyForClaimApplication.Models
{
    public class Policy
    {
        [Key]
        public string PolicyNumber {  get; set; }

       public  string InsuredName { get; set; }=string.Empty;

        public int PolicyType { get; set;} //claim type id
    }
}
