using Backend.DataAccess;
using Backend.Models;
using Backend.ViewModels;
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
        public readonly EnrolmentContext _context;

        public UserController(IConfiguration config, EnrolmentContext context)
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
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User successfully added");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(Login user)
        {
            var isExistUser = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            LoginResponse response = new LoginResponse();

            try
            {
                if (isExistUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(user.Password, isExistUser.Password))
                    {
                        response.Message = "Login successful";
                        response.Status = "success";
                        response.JwtToken = new JwtServicecs(_config).GenerateToken(isExistUser).ToString();
                        return Ok(response);
                    }
                    else
                    {
                        response.Message = "Password does not match";
                        return StatusCode(400, response);
                    }
                }
                else
                {
                    response.Message = "User not found";
                    return StatusCode(404, response);
                }
            }catch(Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}

