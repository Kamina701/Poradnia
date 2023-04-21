using Application.CQRS.Reports.Queries;

using AutoMapper;

using Domain.Entities.Autitables;

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;


namespace Application.Profiles
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


        }
    }
}
