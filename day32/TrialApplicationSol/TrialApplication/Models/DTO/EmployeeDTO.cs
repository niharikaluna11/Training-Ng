using System.ComponentModel.DataAnnotations;

namespace TrialApplication.Models.DTO
{
    public class EmployeeDTO 
    {
        //http attribute  passibg to make it required

        [Required(ErrorMessage ="Employee name cant be empty")]
        public string Name { get; set; }=string.Empty;

        public string Email { get; set; } = string.Empty;
   public string Phone { get; set; } = string.Empty;


    }
}
