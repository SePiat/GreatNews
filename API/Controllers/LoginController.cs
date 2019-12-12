using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using GreatNews.Models;
using GreatNews.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<UserIdent> _userManager;
        private readonly SignInManager<UserIdent> _signInManager;
        private readonly IConfiguration _config;

        public LoginController(UserManager<UserIdent> userManager, SignInManager<UserIdent> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }



        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> PostAsync([FromBody] LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
                if (result.Succeeded)
                {
                    var user = Authenticate(login);
                    if (user == null) return Unauthorized();
                    var tokenString = BuildToken(user);
                    return Ok(new { token = tokenString });
                }
                else
                {
                    return "Wrong login or(and) password";
                }
            }
            return null;
        }


        private string BuildToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModel login)
        {
            /*if (login.Username.Equals("Mario") && login.Password.Equals("secret"))*/
            if (login.Username == null && login.Password == null)
            {
                return null;
            }
            else
            {
                return new UserModel
                {
                    Name = login.Username,
                    Email = login.Password
                };
            }

        }


    }
}