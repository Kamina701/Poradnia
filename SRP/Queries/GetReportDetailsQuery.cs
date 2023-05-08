using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SRP.Interfaces;
using SRP.Models.Enties;

namespace SRP.Queries
{
    public class GetReportDetailsQuery : IRequest<ReportDetails>
    {
        public Guid Id { get; set; }
        public GetReportDetailsQuery(Guid id)
        {
            Id = id;
        }
    }
    public class GetReportDetailsHandler : IRequestHandler<GetReportDetailsQuery, ReportDetails>
    {

        private readonly IAsyncRepository<Report> _reportRepository;
        private readonly IMapper _mapper;

        public GetReportDetailsHandler(IAsyncRepository<Report> reportRepository, IAsyncRepository<Comment> commentRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<ReportDetails> Handle(GetReportDetailsQuery request, CancellationToken cancellationToken)
        {
            var reportWithDetails = await _reportRepository.FindByIncludeAsync(r => r.Id == request.Id, inc => inc.Comments);
            return _mapper.Map<ReportDetails>(reportWithDetails);
        }
    }

    public class ReportDetails
    {
        public ReportDto Report { get; set; }
    }
}
