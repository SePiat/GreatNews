using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;
using GreatNews.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserIdent> _userManager;
        private readonly SignInManager<UserIdent> _signInManager;

        public UserController(SignInManager<UserIdent> signInManager, UserManager<UserIdent> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        /// <summary>
        /// GET: api/Users
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public IEnumerable<UserIdent> Get()
        {
            return _userManager.Users.ToList();
        }

        /// <summary>
        /// GET: api/Users/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public UserIdent Get(Guid id)
        {
            return _userManager.Users.FirstOrDefault(x => x.Id.Equals(id));
        }


        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdent user = new UserIdent { Email = model.Email, UserName = model.Email, Year = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdent user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return null;

        }


        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            UserIdent user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                return Ok();
            }
            else
            {
                return NotFound("Not found");
            }

        }
    }
}
