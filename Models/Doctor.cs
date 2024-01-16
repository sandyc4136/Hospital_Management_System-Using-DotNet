using System;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
	public class Doctor
	{
		
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[A-Za-z]+(?:[-'][A-Za-z]+)?$", ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[A-Za-z]+(?:[-'][A-Za-z]+)?$", ErrorMessage = "Please enter a valid name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [RegularExpression(@"^(?:[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Please enter a valid age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number should be exactly 10 digits")]
        public string Phone { get; set; }
        //public string Email { get; set; }

        [Required]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Invalid gender.")]
        public string Gender { get; set; }

        [RegularExpression("^(A+|A-|B+|B-|AB+|AB-|O+|O-)$", ErrorMessage = "Invalid BloodGroup.")]
        public string BloodGroup { get; set; }

        [RegularExpression("^(Orthopedics|Cardiology|Internal Medicine|Surgery|Pediatrics|Radiology|Anesthesiology|Ophthalmology|Ophthalmology(ENT)|Dermatology|Psychiatry|Pathology)$", ErrorMessage = "Invalid Department.")]
        public string Department { get; set; }

        public string ProfileImage { get; set; }
        
    }
}



