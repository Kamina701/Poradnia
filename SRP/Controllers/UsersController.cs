﻿using Application.Contracts.Identity;
using Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using Persistance;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs;

namespace SRP.Controllers
{
    [Authorize(Policy = "ManageUsers")]
    public class UsersController : BaseController
    {
        private readonly IUsersService _usersService;
        private readonly SRPDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public UsersController(IUsersService usersService, SRPDbContext context, IHttpContextAccessor accessor)
        {
            _usersService = usersService;
            _context = context;
            _accessor = accessor;
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Unconfirmed()
        {
            var am = await _usersService.GetUnconfirmedUsersAsync();
            return View(am);
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string query = null)
        {
            var am = await _usersService.GetUsers(pageNumber < 1 ? 1 : pageNumber, pageSize, query);
            return View(am);
        }
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            return RedirectToAction("Index", new { query });
        }
        public async Task<IActionResult> Confirm(Guid id)
        {
            var confirmedResult = await _usersService.ConfirmUser(id);
            SetTempDataMessage(confirmedResult, "Użytkownik potwierdzony.", "Spróbuj ponownie potwierdzić użytkownika.");
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> UnlockUser(Guid id)
        {
            var unlockResult = await _usersService.UnlockUser(id);
            SetTempDataMessage(unlockResult, "Użytkownik odblokowany", "Nie udało się odblokować konta.");
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> LockoutUser(Guid id)
        {
            var lockoutResult = await _usersService.LockoutUserAsync(id);
            SetTempDataMessage(lockoutResult, "Użytkownik zablokowany.", "Blokowanie nie powiodło się.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> ChangeRole(Guid id)
        {
            var viewModel = await _usersService.GetUserWithRolesList(id);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<IActionResult> ChangeRole(ChangeRoleForUserRequest request)
        {
            var response = await _usersService.ModifyUserRolesAsync(request);
            SetTempDataMessage(response, "Uprawnienia użytkownika zostały zmienione.", "Nie udało się zmienić uprawnień użytkownikowi.");
            return RedirectToAction("Index");
        }




        public async Task<IActionResult> Details(Guid id)
        {
            if (_accessor.HttpContext.Request.Headers["Referer"].ToString() != null)
            {
                ViewData["PageNumber"] = Regex.Match(_accessor.HttpContext.Request.Headers["Referer"].ToString(), "\\?pageNumber=(.*)");
            }
            var am = await _usersService.Details(id);
            return View(am);
        }


        public async Task<IActionResult> LockedOut()
        {
            List<SRPUserDTO> lockedOutUsers = await _usersService.GetLockedOutUsers();
            return View(lockedOutUsers);
        }

        private void SetTempDataMessage(bool response, string successMessage, string failedMessage)
        {
            if (response)
            {
                TempData["Message"] = successMessage;
            }
            else
            {
                TempData["Message"] = failedMessage;
            }
        }
    }
}
