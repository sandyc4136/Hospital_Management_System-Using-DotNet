﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HMS.DTO
{
	public class PatientImage
	{
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        //public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Disease { get; set; }

        public string? BloodGroup { get; set; }

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}

