using Microsoft.AspNetCore.Mvc;
using Setur.Services.Report.Api.Controllers.Base;
using Setur.Services.Report.Application.Commands;
using Setur.Services.Report.Application.DTO;
using Setur.Services.Report.Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReportRequestController: BaseApiController
    {
        private readonly ICommandHandler<CreateReportRequest> _commandHandler;
        private readonly IQueryHandler<GetReportRequest,IEnumerable<ReportRequestDto>> _queryHandler;
        private readonly IQueryHandler<GetReportDetails,ReportDetailDto> _reportDetailQueryHandler;
      

        public ReportRequestController(ICommandHandler<CreateReportRequest> commandHandler,
                                        IQueryHandler<GetReportRequest, IEnumerable<ReportRequestDto>> queryHandler,
                                        IQueryHandler<GetReportDetails, ReportDetailDto> reportDetailQueryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _reportDetailQueryHandler = reportDetailQueryHandler;
        }


        [HttpGet]
        public async Task<IEnumerable<ReportRequestDto>> GetReportRequests()
        {
            return await _queryHandler.HandleAsync(new GetReportRequest());
        }

        [HttpGet("{id}")]
        public async Task<ReportDetailDto> GetReportDetail([FromRoute] GetReportDetails getReportDetails)
        {
            return await _reportDetailQueryHandler.HandleAsync(getReportDetails);
        }


        [HttpPost]
        public async Task<IActionResult> CreateReportRequest()
        {
            await _commandHandler.HandleAsync(new CreateReportRequest());

            return Ok();
        }
    }
}
