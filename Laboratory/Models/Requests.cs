using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace Laboratory.Models
{
    public class Requests
    {
        public int Id { get; set; }

        [Range(1000000000,9999999999)]
        public int NationalResidenceId { get; set; }
        [Range(1000000000, 9999999999)]
        public int  UniversityNumber { get; set; }
        public string StudentStatus { get; set; }
        public string College { get; set; }

        [StringLength(20, MinimumLength = 1)]
        public string FirstNameEnglish { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string FatherNameEnglish { get; set;}
        [StringLength(20, MinimumLength = 1)]
        public string GrandFatherNameEnglish { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string FamilyNameEnglish { get; set; }

        [StringLength(20, MinimumLength = 1)]
        public string FirstNameArabic { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string FatherNameArabic { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string GrandFatherNameArabic { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string FamilyNameArabic { get; set; }


        public string Email { get; set; }
        
        
       // [RegularExpression(@"(05)+[0-9]{8}")]
        public int PhoneNo { get; set; }
        public DateTime BirthDate { get; set; }
        public int? MedicalFileNo { get; set; }


        public DateTime TestDate { get; set; }



    }
}
