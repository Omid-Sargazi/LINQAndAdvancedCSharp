using AuthDemoAuth.Models;

namespace AuthDemoAuth.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterModel model);
        Task<bool> ValidateUserAsync(LoginModel model);
        Task<bool> UserExistsAsync(string email);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> IsUserInRoleAsync(string email, UserRole role);
    }

    public class UserService : IUserService
    {
        // private readonly List<RegisterModel> _users = new();
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            return await Task.FromResult(user!);
        }

        public async Task<bool> IsUserInRoleAsync(string email, UserRole role)
        {
            var user = await GetUserByEmailAsync(email);
            if(user==null) return false;

            if(Enum.TryParse<UserRole>(user.Role, out var userRole))
            {
                return userRole == role;
            }
            return false;
        }

        public async Task<bool> RegisterUserAsync(RegisterModel model)
        {

            var role = model.Email.ToLower().Contains("admin")?
            UserRole.Admin.ToString():
            UserRole.User.ToString();
            var user = new User
            {
                Id = _nextId++,
                Email = model.Email,
                Password = model.Password,
                // Role = model.Role ?? UserRole.User.ToString()
                Role = role
            };
            _users.Add(user);
             foreach (var item in _users)
            {
                Console.WriteLine($"User: {item.Email}, Role: {item.Role}");
            }
            return await Task.FromResult(true);

           
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var user = _users.FirstOrDefault(u=>u.Email==email);
            return await Task.FromResult(user !=null);
        }

        public Task<bool> ValidateUserAsync(LoginModel model)
        {
            var user = _users.FirstOrDefault(u=>u.Email == model.Email && u.Password==model.Password);
            return Task.FromResult(user !=null);
        }
    }
}