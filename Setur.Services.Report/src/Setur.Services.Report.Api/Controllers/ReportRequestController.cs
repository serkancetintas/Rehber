using Microsoft.AspNetCore.Mvc;
using Setur.Services.Report.Api.Controllers.Base;
using Setur.Services.Report.Application.Commands;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReportRequestController: BaseApiController
    {
        private readonly ICommandHandler<CreateReportRequest> _commandHandler;
      

        public ReportRequestController(ICommandHandler<CreateReportRequest> commandHandler)
        {
            _commandHandler = commandHandler;
        }


        [HttpPost]
        public async Task<IActionResult> CreateReportRequest()
        {
            await _commandHandler.HandleAsync(new CreateReportRequest());

            return Ok();
        }
    }
}
