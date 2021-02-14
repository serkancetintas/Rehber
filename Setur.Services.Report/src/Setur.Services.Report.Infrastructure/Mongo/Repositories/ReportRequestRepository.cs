using Setur.Services.Report.Core.Entities;
using Setur.Services.Report.Core.Repositories;
using Setur.Services.Report.Infrastructure.Mongo.Documents;
using System;
using System.Threading.Tasks;

namespace Setur.Services.Report.Infrastructure.Mongo.Repositories
{
    public class ReportRequestRepository:IReportRequestRepository
    {
        private readonly IMongoRepository<ReportRequestDocument, Guid> _repository;
        public ReportRequestRepository(IMongoRepository<ReportRequestDocument, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<ReportRequest> GetAsync(AggregateId id)
        {
            var document = await _repository.GetAsync(r => r.Id == id);
            return document?.AsEntity();
        }

        public Task AddAsync(ReportRequest reportRequest) => _repository.AddAsync(reportRequest.AsDocument());
        public Task UpdateAsync(ReportRequest reportRequest) => _repository.UpdateAsync(reportRequest.AsDocument());

    }
}
