
using AutoMapper;
using SRP.Models.Enties;
using SRP.Models;
using SRP.Models.DTOs;
using System;

namespace SRP.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Reports
            CreateMap<Report, ReportDto>()
                .ForMember(dst => dst.Author, opt => opt.MapFrom(src => src.CreatedBy))
                .ReverseMap();

            CreateMap<Report, ReportDetails>()
                .ForMember(dst => dst.Report, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<Comment, CommentDto>()
                .ForMember(dst => dst.Author, opt => opt.MapFrom(src => src.CreatedBy))
                .ReverseMap();

            CreateMap<SRPUser, SRPUserDTO>()
                .ForMember(dst => dst.IsLockedOut, opt => opt.MapFrom(src => src.LockoutEnd != null))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.NormalizedUserName))
                .AfterMap((src, dst) =>
                {
                    if (src.LockoutEnd > DateTime.Now)
                        dst.Status = Models.Enums.Status.Zablokowany;
                    else if (!src.EmailConfirmed)
                        dst.Status = Models.Enums.Status.Niepotwierdzony;
                    else
                        dst.Status = Models.Enums.Status.Aktywny;
                })
                .ReverseMap();
        }
    }
}
