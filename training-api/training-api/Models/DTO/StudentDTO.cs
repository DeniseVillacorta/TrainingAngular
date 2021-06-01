using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace training_api.Models.DTO
{
    public class StudentCreateDTO
    {
        [Required(ErrorMessage = "Firstname is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a firstname.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a lastname.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Middlename is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a middlename.")]
        public string middleName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(50, ErrorMessage = "You have exceeded the maximum allowed characters (50) for a gender.")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public int age { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a address.")]
        public string address { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a course.")]
        public string course { get; set; }
	}

    public class StudentReturnDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Course { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class StudentUpdateDTO
    {
        [Required(ErrorMessage = "Firstname is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a firstname.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a lastname.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Middlename is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a middlename.")]
        public string middleName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(50, ErrorMessage = "You have exceeded the maximum allowed characters (50) for a gender.")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public int age { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a address.")]
        public string address { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a course.")]
        public string course { get; set; }

        [Required(ErrorMessage = "is_active is required.")]
        public bool is_active { get; set; }
    }

    public class StudentChangeStatusDTO
    {
        [Required(ErrorMessage = "is_active is required.")]
        public bool is_active { get; set; }
    }
}