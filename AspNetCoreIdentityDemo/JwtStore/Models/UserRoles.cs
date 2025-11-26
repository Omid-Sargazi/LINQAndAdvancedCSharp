using System.ComponentModel.DataAnnotations;

namespace JwtStore.Models
{
    public static class UserRoles
    {
        public const string Admin="Admin";
        public const string User="User";
        public const string Manager="Manager";
    }

    public class LoginModel
    {
        [Required(ErrorMessage ="نام کاربری الزامی است.")]
        public string UserName {get;set;} = string.Empty;

        [Required(ErrorMessage ="پسورد الزامی است.")]
        public string Password {get;set;} = string.Empty;
    }

    public class RegisterModel
    {
        [Required(ErrorMessage ="Username is required.")]
        public string UserName {get;set;} =string.Empty;

        [Required(ErrorMessage ="Email Is required.")]
        public string Email {get;set;} = string.Empty;

        [Required(ErrorMessage ="Password is Required.")]
        public string Password {get;set;} = string.Empty;
    }

    public class Response
    {
        public string Status {get;set;} = string.Empty;
        public string Message {get;set;} = string.Empty;
    }

    
}