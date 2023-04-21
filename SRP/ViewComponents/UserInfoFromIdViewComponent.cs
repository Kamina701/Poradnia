using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.DTOs;
using Identity.Models;


namespace SRP.ViewComponents
{
    public class UserInfoFromIdViewComponent : ViewComponent
    {
        private readonly UserManager<SRPUser> _userManager;
        private readonly IMapper _mapper;

        public UserInfoFromIdViewComponent(UserManager<SRPUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var userVCm = await _userManager.FindByIdAsync(id.ToString());
            return View(_mapper.Map<SRPUserDTO>(userVCm));
        }
    }
    public class UsernameViewComponent : ViewComponent
    {
        private readonly UserManager<SRPUser> _userManager;
        private readonly IMapper _mapper;

        public UsernameViewComponent(UserManager<SRPUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var userVCm = await _userManager.FindByIdAsync(id.ToString());
            return View(_mapper.Map<SRPUserDTO>(userVCm));
        }
    }
}