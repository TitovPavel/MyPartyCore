using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DAL;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private readonly UserRepository _userRepository;
        
        public UserViewComponent(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IViewComponentResult Invoke()
        {
            UserLoginViewModel userLoginViewModel = new UserLoginViewModel();

            string guidUserSessionID = HttpContext.Session.GetString("guidUserSessionID");

            if (!String.IsNullOrEmpty(guidUserSessionID))
            {
                User user = _userRepository.GetBySessionID(guidUserSessionID);
                if (user != null)
                {
                    userLoginViewModel.IsSignedIn = true;
                    userLoginViewModel.Login = user.Login;
                }
            }

            return View(userLoginViewModel);
        }
    }
}
