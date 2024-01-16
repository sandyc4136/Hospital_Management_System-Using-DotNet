using System;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{

	public class Patient
	{
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name should have alphabets only")]
        public string? Name { get; set; }

        //public string? Email { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid phone number")]
        [MinLength(10, ErrorMessage = "Phone should not be less then 10 digits")]
        [MaxLength(10, ErrorMessage = "Phone should not be greater then 10 digits")]
        public string? Phone { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9]+)*$", ErrorMessage = "EnterValid Input")]
        public string? Disease { get; set; }

        [Required]
        [RegularExpression("^(A+|A-|B+|B-|AB+|AB-|O+|O-)$", ErrorMessage = "Invalid BloodGroup.")]
        public string? BloodGroup { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Please enter a valid age")]
        public int? Age { get; set; }

        [Required]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Invalid gender.")]
        public string? Gender { get; set; }

        [Required]
        public string? Address { get; set; }

        
        public string ProfileImage { get; set; }
    }
}

