using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class LoginViewModel
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Phone(ErrorMessage = "Invalid mobile number")]
        public long MobileNo { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string Password { get; set; }
        public string SelectedType { get; set; }
        public List<string> Types { get; set; }
        public bool RememberMe { get; set; }
    }

}