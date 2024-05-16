// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OgWeb.Models;

namespace OgWeb.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        //down
        private readonly UserManager<ApplicationUser> _userManager;
        
        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            //down
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            var name = User.Identity.Name;   

            var user = await _userManager.FindByNameAsync(name);
            var claims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, claims);

            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }
        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}
