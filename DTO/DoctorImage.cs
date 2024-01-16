using System;
namespace HMS.DTO
{
	public class DoctorImage
	{
		
        
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Phone { get; set; }
            //public string Email { get; set; }
            public string Gender { get; set; }
            public string BloodGroup { get; set; }
            public string Department { get; set; }
            public IFormFile ProfileImage { get; set; }
        
    }

}


