using Microsoft.Extensions.Logging;
using Setur.Services.Report.Application.Events;
using Setur.Services.Report.Application.Services;
using Setur.Services.Report.Core.Entities;
using Setur.Services.Report.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Setur.Services.Report.Application.Commands.Handlers
{
    public class CreateReportRequestHandler : ICommandHandler<CreateReportRequest>
    {
        private readonly IReportRequestRepository _repository;
        private readonly IMessageBroker _messageBroker;
        public CreateReportRequestHandler(IReportRequestRepository repository,
                                          IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(CreateReportRequest command)
        {
            var reportId = Guid.NewGuid();
            var reportRequest = new ReportRequest(reportId, DateTime.Now, State.Preparing);

            await _repository.AddAsync(reportRequest);

            await _messageBroker.PublishAsync(new ReportRequestCreated(reportId));
        }
    }
}
