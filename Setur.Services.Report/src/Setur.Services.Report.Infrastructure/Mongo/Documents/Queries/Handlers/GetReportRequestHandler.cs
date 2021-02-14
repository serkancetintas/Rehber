using Setur.Services.Report.Application.DTO;
using Setur.Services.Report.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setur.Services.Report.Infrastructure.Mongo.Documents.Queries.Handlers
{
    public class GetReportRequestHandler: IQueryHandler<GetReportRequest, IEnumerable<ReportRequestDto>>
    {
        private readonly IMongoRepository<ReportRequestDocument, Guid> _repository;

        public GetReportRequestHandler(IMongoRepository<ReportRequestDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReportRequestDto>> HandleAsync(GetReportRequest query)
        {
            var result = await _repository.FindAsync(p => true);

            return result.Select(r => r.AsDto());
        }
    }
}
