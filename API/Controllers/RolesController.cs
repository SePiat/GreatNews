using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserIdent> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<UserIdent> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        /// <summary>
        /// GET: api/Roles
        /// </summary>
        /// <returns></returns>        
        [HttpGet]        
        public IEnumerable<IdentityRole> Get()
        { 
            return _roleManager.Roles.ToList();
        }
        /// <summary>
        /// GET: api/Roles/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public IdentityRole Get(Guid id)
        {
            return _roleManager.Roles.FirstOrDefault(x => x.Id.Equals(id));
        }

        /// <summary>
        /// POST: api/Roles
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(value));
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
        /// DELETE: api/ApiWithActions/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                return Ok(result);
            }
            else
            {
                return NotFound("Not found");
            }
            
        }
    }
}