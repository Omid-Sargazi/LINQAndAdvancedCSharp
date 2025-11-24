using System.ComponentModel.DataAnnotations;

namespace AuthDemoAuth.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Please enter a valid Email Address.")]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength =8,ErrorMessage ="Password must be 8 character.")]
        [Required(ErrorMessage ="Password is required.")]
        public string Password {get;set;}

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The password and confirm password do not match.")]
        public string ConfirmPassword {get;set;}

       // public string Role {get;set;}
    }

    public class User
    {
        public int Id {get;set;}
        [Required]
        [EmailAddress(ErrorMessage ="Please Enter a valid Email.")]
        public string Email {get;set;}
        
        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [Required]
        public string Role {get;set;}
    }

    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Please Enter a email Address.")]
        public string Email {get;set;}
        
        [DataType(DataType.Password)]
        // [StringLength(100,MinimumLength =8,ErrorMessage ="Password must be 8 charachter.")]
        [Required]
        public string Password {get;set;}
        public bool RememberMe {get;set;}
    }

    public enum UserRole
    {
        User,
        Admin,
        Manager
    }
}