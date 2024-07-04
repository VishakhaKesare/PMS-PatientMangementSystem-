using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Discovery;

namespace PatientManagementSoftware.Models
{
    public class DoctorViewModel
    {
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, ErrorMessage = "Specialization cannot be longer than 100 characters")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number. It should be a 10-digit number.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Availability is required")]
        [StringLength(200, ErrorMessage = "Availability cannot be longer than 200 characters")]
        public string Availability { get; set; }
    }



}
