﻿using Booky_Utility;
using Booky_Web.Models.Dto;
using Booky_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Booky_Web.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Booky_Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			LoginRequestDTO obj = new();
			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginRequestDTO obj)
		{
			APIResponse response = await _authService.LoginAsync<APIResponse>(obj);
			if (response != null && response.IsSuccess)
			{
				LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

				var handler = new JwtSecurityTokenHandler();
				var jwt = handler.ReadJwtToken(model.Token);

				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == "unique_name").Value));
				identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				//jwt.Claims.FirstOrDefault(u => u.Type == "name").Value

				HttpContext.Session.SetString(SD.SessionToken, model.Token);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());
				return View(obj);
			}
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterationRequestDTO obj)
		{
			APIResponse result = await _authService.RegisterAsync<APIResponse>(obj);
			if (result != null && result.IsSuccess)
			{
				return RedirectToAction("Login");
			}
			return View();
		}


		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			HttpContext.Session.SetString(SD.SessionToken, "");
			return RedirectToAction("Index","Home");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}

