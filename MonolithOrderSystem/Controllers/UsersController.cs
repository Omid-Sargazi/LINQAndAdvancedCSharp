namespace MonolithOrderSystem.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly InMemoryStore _store;
        public UsersController(InMemoryStore store) => _store = store;

        [HttpPost]
        public ActionResult<User> Create([FromBody] CreateUserRequest request)
        {
            var user = _store.AddUser(request.UserName, request.Email);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
            => Ok(_store.GetUsers());
    }

}