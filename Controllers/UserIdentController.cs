using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;
using GreatNews.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreatNews.Controllers
{

    public class UserIdentController : Controller
    {
        private UserManager<UserIdent> _userManager;

        public UserIdentController(UserManager<UserIdent> userManager)
        {
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index() => View(_userManager.Users.ToList());
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdent user = new UserIdent { Email = model.Email, UserName = model.Email, Year = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            UserIdent user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Year = user.Year };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdent user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
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

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            UserIdent user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            UserIdent user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdent user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<UserIdent>)) as
                            IPasswordValidator<UserIdent>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<UserIdent>)) as
                            IPasswordHasher<UserIdent>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь ненайден");
                }
            }

            return View(model);

            //второй вариант

            //if (ModelState.IsValid)
            //{
            //    User user = await _userManager.FindByIdAsync(model.Id);
            //    if (user != null)
            //    {
            //        IdentityResult result =
            //            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            //        if (result.Succeeded)
            //        {
            //            return RedirectToAction("Index");
            //        }
            //        else
            //        {
            //            foreach (var error in result.Errors)
            //            {
            //                ModelState.AddModelError(string.Empty, error.Description);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Пользователь не найден");
            //    }
            //}
            //return View(model);
        }
    }
}
