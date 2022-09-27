using Microsoft.AspNetCore.Mvc;
using Activity2API.Models;
using Activity2API.Models.DTO;

namespace Activity2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] AuthenticationDTO authenticationDTO)
        {
            var user = State.Users.Find(user => user.Username == authenticationDTO.Username);
            if (user == null) return BadRequest("Invalid credentials");
            if (user.Password != authenticationDTO.Password) return BadRequest("Invalid credentials");
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            return token;
        }

        [HttpPost("Register")]
        public ActionResult<User> Register([FromBody] UserDTO userDTO)
        {
            var doesExist = State.Users.Exists(currentUser => currentUser.Username == userDTO.Username);
            if (doesExist) return BadRequest("User already exists");
            User user = new User()
            {
                Id = State.Users.Count,
                Username = userDTO.Username,
                Password = userDTO.Password
            };
            State.Users.Add(user);
            return Ok(user);
        }
    }
}
