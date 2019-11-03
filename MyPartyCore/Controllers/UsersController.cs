using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
       
        }

        public IActionResult Index()
        {           
            return View(_mapper.ProjectTo<UserViewModel>(_userManager.Users));
        }

        public IActionResult Create()
        {
            ViewBag.SexList = GetSexList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<User>(createUserViewModel);
                var result = await _userManager.CreateAsync(user, createUserViewModel.Password);
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
            ViewBag.SexList = GetSexList();

            return View(createUserViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = _mapper.Map<EditUserViewModel>(user);
         
            ViewBag.SexList = GetSexList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
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

            ViewBag.SexList = GetSexList();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Lock(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddYears(100)));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UnLock(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, null);
            }
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetSexList()
        {
            List<SelectListItem> sexList = new List<SelectListItem>
            {
                new SelectListItem {Text = "M", Value = "M"},
                new SelectListItem {Text = "F", Value = "F"}
             };

            return sexList;
        }
    }
}
