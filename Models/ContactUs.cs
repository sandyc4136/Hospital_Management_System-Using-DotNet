using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name should have alphabets only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage = "Enter a valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage ="Please enter valid phone number")]
        [MinLength(10, ErrorMessage = "Phone should not be less then 10 digits")]
        [MaxLength(10, ErrorMessage = "Phone should not be greater then 10 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter your message")]
        public string Message { get; set; }
    }
}
