using System;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
	public class Booking
	{
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Enter a valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name should have alphabets only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid phone number")]
        [MinLength(10, ErrorMessage = "Phone should not be less then 10 digits")]
        [MaxLength(10, ErrorMessage = "Phone should not be greater then 10 digits")]
        public string Phone { get; set; }

        public string Diseases { get; set; }

        [RegularExpression(@"^(?:[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Please enter a valid age")]
        public int Age { get; set; }

        [RegularExpression("^(Male|Female)$", ErrorMessage = "Invalid gender.")]
        public string Gender { get; set; }

        public string Address { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        
    }
    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    //public class NotFutureDateAttribute : ValidationAttribute
    //{
    //    public override bool IsValid(object value)
    //    {
    //        if (value is DateTime date)
    //        {
    //            return date >= DateTime.Now;
    //        }

    //        // Return false for non-DateTime values (you can modify this behavior based on your requirements)
    //        return false;
    //    }
    //}
}

