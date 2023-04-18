﻿using Application.Contracts.Persistance;
using Application.CQRS;
using AutoMapper;
using Domain.Entities.Autitables;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Reports.Queries
{
    public class GetReportsQuery : AuthorizedRequest, IRequest<IList<ReportDto>>
    {
    }
    public class GetReportsHandler : IRequestHandler<GetReportsQuery, IList<ReportDto>>
    {
        private readonly IAsyncRepository<Report> _reportRepository;
        private readonly IMapper _mapper;

        public GetReportsHandler(IMapper mapper, IAsyncRepository<Report> reportRepository)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<IList<ReportDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            if (request.IsAdmin)
            {
                var allReports = await _reportRepository.GetAllAsync();
                return _mapper.Map<IList<ReportDto>>(allReports);

            }
            else
            {
                var userReports = await _reportRepository.FindManyByIncludeAsync(x => x.CreatedBy == request.UserId);
                return _mapper.Map<IList<ReportDto>>(userReports);
            }
        }

    }
    public class ReportDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public Status Status { get; set; }
        public ReportType Type { get; set; }
        public Guid Author { get; set; }
        public DateTime Created { get; set; }
    }
    public class CommentDto
    {
        public string Response { get; set; }
        public Guid Id { get; set; }
        public Guid Author { get; set; }
        public DateTime Created { get; set; }

    }
}