using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HMS.Models
{
	public class ApplicationUser : IdentityUser
    {

        [Required(ErrorMessage = "Enter your Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should have alphabets only")]
        public string Name { get; set; }

    }
}

