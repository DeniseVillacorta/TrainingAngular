using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models.DTO
{
    public class UsersSignInDTO
    {
        [Required(ErrorMessage = "username is required.")]
        public string username { get; set; }
        [Required(ErrorMessage = "password is required.")]
        public string password { get; set; }
    }
    public class UsersReturnDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class UsersCreateDTO
    {
        [Required(ErrorMessage = "username is required.")]
        [StringLength(100, ErrorMessage = "You have exceeded the maximum allowed characters (100) for a username.")]
        public string username { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [StringLength(100, ErrorMessage = "You have exceeded the maximum allowed characters (100) for a password.")]
        public string password { get; set; }

        [Required(ErrorMessage = "name is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a name.")]
        public string name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "email is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a email.")]
        public string email { get; set; }
    }
    
    public class UsersUpdateDTO
    {
        [Required(ErrorMessage = "name is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a name.")]
        public string name { get; set; }
        [Required(ErrorMessage = "is_active is required.")]
        public bool is_active { get; set; }
        [Required(ErrorMessage = "email is required.")]
        [StringLength(250, ErrorMessage = "You have exceeded the maximum allowed characters (250) for a email.")]
        public string email { get; set; }
    }

    public class ChangeStatusDTO
    {
        public bool? is_active { get; set; }
    }

    public class UserForgotPasswordDTO
    {
        [Required(ErrorMessage = "email is required.")]
        public string email { get; set; }
    }
}
