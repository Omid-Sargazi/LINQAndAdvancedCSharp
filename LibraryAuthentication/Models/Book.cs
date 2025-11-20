namespace LibraryAuthentication.Models
{
    public class Book
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public string Author {get;set;}
        public string ISBN {get;set;}
        public bool IsAvilable {get;set;}
    }

    public class User
    {
        public int Id {get;set;}
        public string UserName {get;set;}
        public string HashPassword {get;set;}
        public string Role {get;set;}
    }

    public class Review
    {
        public int Id {get;set;}
        public int BookId {get;set;}
        public int UserId {get;set;}
        public string Command {get;set;}
    }


    public class LoginRequest
    {
        public string UserName {get; set;}
        public  string Password {get; set;}
    }
}