using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class MedicalRecordViewModel
    {
        
        public int RecordID { get; set; }
        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        public string Medications { get; set; }

        public string PatientName { get; set; }

        public string DoctorName { get; set; }

        public string BloodGroup { get; set; }

        public List<MedicalRecordViewModel> MedicalRecords { get; set; }

    }
}