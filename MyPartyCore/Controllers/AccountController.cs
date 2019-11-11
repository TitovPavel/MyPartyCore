using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DAL;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserRepository _userRepository;
       
        public AccountController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Login()
        {         
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel userViewModel)
        {
            User user = _userRepository.GetById(userViewModel.Login);
            if (user == null)
            {
                user = new User
                {
                    Login = userViewModel.Login,
                    Password = userViewModel.Password
                };
            }

            string guidUserSessionID = Guid.NewGuid().ToString();
            user.SessionID = guidUserSessionID;

            _userRepository.Update(user);
            _userRepository.Save();

            HttpContext.Session.SetString("guidUserSessionID", guidUserSessionID);

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult LogOff()
        {

            string guidUserSessionID = HttpContext.Session.GetString("guidUserSessionID");
            HttpContext.Session.SetString("guidUserSessionID", "");

            User user = _userRepository.GetBySessionID(guidUserSessionID);
            if (user != null)
            {
                user.SessionID = "";
                _userRepository.Save();
            }
                  
            return RedirectToAction("Index", "Home");
        }
    }
}
