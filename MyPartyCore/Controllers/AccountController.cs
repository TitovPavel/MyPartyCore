using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using MyPartyCore.DB.BL;
using MyPartyCore.DB.Models;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyPartyCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly IPhotoService _photoService;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IMapper mapper, 
            IStringLocalizer<AccountController> localizer,
            IPhotoService photoService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _localizer = localizer;
            _photoService = photoService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<User>(registerViewModel);

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {

                    var plus18Claim = new Claim(ClaimTypes.DateOfBirth, registerViewModel.Birthday.ToString(), typeof(DateTime).ToString());
                    await _userManager.AddClaimAsync(user, plus18Claim);

                    var gender = new Claim(ClaimTypes.Gender, registerViewModel.Sex.ToString(), typeof(String).ToString());
                    await _userManager.AddClaimAsync(user, gender);

                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
   
                    return RedirectToAction("Index", "Home");
  
                }
                else
                {
                    ModelState.AddModelError("", _localizer["IncorrectUsername"]);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Profile(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ProfileViewModel model = _mapper.Map<ProfileViewModel>(user);
           
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Profile(ProfileViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.Birthday = model.Birthday;
                    user.Sex = model.Sex;
                    if (file != null)
                    {
                        if (model.AvatarExist)
                        {
                            _photoService.UpdatePhoto((int)user.AvatarId, file);
                        }
                        else
                        {
                            user.Avatar = _photoService.AddPhoto(file);
                        }
                    }
                    else if(file == null && model.AvatarExist && HttpContext.Request.Form.Keys.Contains("file"))
                    {
                        _photoService.DeletePhotoFromUser(user);
                    }

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}
