using Setur.Services.Report.Application.DTO;
using Setur.Services.Report.Application.Queries;
using System;
using System.Threading.Tasks;

namespace Setur.Services.Report.Infrastructure.Mongo.Documents.Queries.Handlers
{
    public class GetReportDetailsHandler : IQueryHandler<GetReportDetails, ReportDetailDto>
    {
        private readonly IMongoRepository<ReportRequestDocument, Guid> _repository;

        public GetReportDetailsHandler(IMongoRepository<ReportRequestDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ReportDetailDto> HandleAsync(GetReportDetails query)
        {
            var result = await _repository.GetAsync(query.Id);

            return result.AsDetailDto();
        }
    }
}
