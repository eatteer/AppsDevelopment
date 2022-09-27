using Microsoft.AspNetCore.Mvc;
using Activity2API.Models;

namespace Activity2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<User>> FindAll()
        {
            return Ok(State.Users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> FindById([FromRoute(Name = "id")] int id)
        {
            var user = State.Users.Find(user => user.Id == id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }
    }
}
