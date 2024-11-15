﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wojcik.Persistence.Entities;
using Wojcik.Shared.Models;

namespace Wojcik.Controllers;

[Route("Api/[controller]/[action]")]
[ApiController]
public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null) return BadRequest("User does not exist");

        var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!singInResult.Succeeded) return BadRequest("Invalid password");

        await _signInManager.SignInAsync(user, request.RememberMe);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel parameters)
    {
		ApplicationUser user = new()
		{
			UserName = parameters.UserName
		};

		var result = await _userManager.CreateAsync(user, parameters.Password);
        if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

        return await Login(new LoginModel
        {
            UserName = parameters.UserName,
            Password = parameters.Password
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet]
	public CurrentUserModel CurrentUserInfo()
	{
		var roles = User.Claims
			.Where(c => c.Type == ClaimTypes.Role)
			.Select(c => c.Value)
			.ToList();

		var claimsDictionary = User.Claims
			.GroupBy(c => c.Type)
			.ToDictionary(
				g => g.Key,
				g => string.Join(", ", g.Select(c => c.Value))
			);

		return new CurrentUserModel
		{
			IsAuthenticated = User.Identity!.IsAuthenticated,
			UserName = User.Identity.Name ?? string.Empty,
			Roles = roles,
			Claims = claimsDictionary
		};
	}
}
