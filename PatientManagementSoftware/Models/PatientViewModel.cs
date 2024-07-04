using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class PatientViewModel
    {

        public int PatientID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]


        [DOBNotGreaterThanToday(ErrorMessage = "Date of Birth cannot be greater than today's date")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number. It should be a 10-digit number.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
    }


    public class DOBNotGreaterThanTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                // If the value is null or not a DateTime object, it's considered invalid             return false;
            }
            DateTime dob = (DateTime)value;
            DateTime today = DateTime.Today;
            // Check if DOB is greater than today's date
            return dob <= today;
        }
    }
}


