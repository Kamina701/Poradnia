using AutoMapper;
using MediatR;
using SRP.interfaces;
using SRP.Interfaces;
using SRP.Models;
using SRP.Models.Enties;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SRP.Queries

{
    public class GetReportsQuery : AuthorizedRequest, IRequest<IList<ReportDto>>
    {
    }
    public class GetReportsHandler : IRequestHandler<GetReportsQuery, IList<ReportDto>>
    {
        private readonly IAsyncRepository<Report> _reportRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetReportsHandler(IMapper mapper, IAsyncRepository<Report> reportRepository, ICurrentUserService currentUserService )
        {
            _reportRepository = reportRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<IList<ReportDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.Role== "SuperAdmin" || _currentUserService.Role == "Doctor")
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
