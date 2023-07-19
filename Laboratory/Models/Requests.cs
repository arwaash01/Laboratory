using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.Models
{
    public class Requests
    {
        public int Id { get; set; }
       
        public int NationalResidenceId { get; set; }
        public int  UniversityNumber { get; set; }
        public string StudentStatus { get; set; }
        public string College { get; set; }
       
        public string FirstNameEnglish { get; set; }
        public string LastNameEnglish { get; set;}
        public string GrandFatherNameEnglish { get; set; }
        public string FamilyNameEnglish { get; set; }

        public string FirstNameArabic { get; set; }
        public string LastNameArabic { get; set; }
        public string GrandFatherNameArabic { get; set; }
        public string FamilyNameArabic { get; set; }


        public string Email { get; set; }
        public int PhoneNo { get; set; }
        public DateTime BirthDate { get; set; }
        public int MedicalFileNo { get; set; }


        public DateTime TestDate { get; set; }



    }
}
