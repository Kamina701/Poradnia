using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRP.Models;
using SRP.Models.DTOs;

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

        public async Task<IViewComponentResult> InvokeAsync(Guid id, bool email, bool firstName = false, bool lastName = false)
        {
            var userDto = new SRPUserDTO();
            var userVCm = await _userManager.FindByIdAsync(id.ToString());
            if (userVCm != null)
            {
                if (email && userVCm.Email != null) { userDto.Email = userVCm.Email; }
                if (firstName && userVCm.FirstName != null) { userDto.FirstName = userVCm.FirstName; }
                if (lastName && userVCm.LastName != null) { userDto.LastName = userVCm.LastName; }
            }


            return View(userDto);
        }
    }
}
