using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApp_Core.Data;
using WebApp_Core.Dto;
using WebApp_Core.Models;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace WebApp_Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        public AuthController(IAuthRepository repo, IMapper mapper, IConfiguration config)
        {
            this.config = config;
            this.mapper = mapper;
            this.repo = repo;

        }
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto user)
        {
            user.Username = user.Username.ToLower();

            if (await repo.UserExists(user.Username))
                return BadRequest("User Already Exists");

            User userForCreation = mapper.Map<User>(user);

            User createdUser = await repo.Register(userForCreation, user.Password);
            var userForReturn = mapper.Map<UserForReturnDto>(createdUser);

            return Ok(userForReturn);

            throw new System.Exception("Failed To Create User");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            userForLoginDto.Username = userForLoginDto.Username.ToLower();
            User userFromRepo = await repo.Login(userForLoginDto.Username, userForLoginDto.Password);
            if (userFromRepo == null)
                return Unauthorized();

//Creating Claims For The Token That Contains the user's name and the user's ID from the database
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.ID.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };
// Creating key and adding it to the signing credentials needed for the token descriptor
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
// Creating the Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userForReturn = mapper.Map<UserForReturnDto>(userFromRepo);
              return Ok(new {
                user = userForReturn,
                token = tokenHandler.WriteToken(token)});
        }
    }
}