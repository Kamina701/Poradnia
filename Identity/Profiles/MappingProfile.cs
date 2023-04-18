using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Application.DTOs;
using Domain.Common;
using Identity.Models;

namespace Identity.Profiles
{
    public class MappingProfile : Profile
    {
        private readonly UserManager<SRPUser> _userManager;

        public MappingProfile(UserManager<SRPUser> userManager)
        {
            _userManager = userManager;
        }
        public MappingProfile()
        {
            CreateMap<SRPUser, SRPUserDTO>()
                .ForMember(dst => dst.IsLockedOut, opt => opt.MapFrom(src => src.LockoutEnd != null))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.NormalizedUserName))
                .AfterMap((src, dst) =>
                {
                    if (src.LockoutEnd > DateTime.Now)
                        dst.Status = Status.Zablokowany;
                    else if (!src.EmailConfirmed)
                        dst.Status = Status.Niepotwierdzony;
                    else
                        dst.Status = Status.Aktywny;
                })
                .ReverseMap();

        }
    }
}