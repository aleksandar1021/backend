using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AngularApp")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public readonly UserContext _context;

        public UserController(IConfiguration config, UserContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            if (_context.Users.Select(x => x.Email == user.Email).FirstOrDefault())
            {
                return Ok("User alredy exist");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User successfully added");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(Login user)
        {
            var isExistUser = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            if (isExistUser != null)
            {
                if(isExistUser.Password == user.Password)
                {
                    return Ok(new JwtServicecs(_config).GenerateToken(
                            isExistUser.UserId.ToString(),
                            isExistUser.Name,
                            isExistUser.Lastname,
                            isExistUser.Email
                        ));
                }
                else
                    return Ok(new { message = "Password does not match", status = "uncorect" });
            }
            else
            {
                return Ok(new { message = "User not exist", status = "unauthorization" });
            }
            


        }
    }
}
