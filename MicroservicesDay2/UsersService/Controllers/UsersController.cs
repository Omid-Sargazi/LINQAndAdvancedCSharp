using Microsoft.AspNetCore.Mvc;
using UsersService.Models;

namespace UsersService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> items = new();
        private static int id = 0;
        [HttpGet] public IEnumerable<User> GetAll() => items;
          [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = items.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpPost] public User Create(User dto) { dto.Id = ++id; items.Add(dto); return dto; }
    }


}