using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequestState;
using UpSchool.HelpDesk.BusinessLayer.CQRS.DeleteWorkRequestState;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequestState;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequestStateByWorkRequestId;
using UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequestState;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkRequestStatesController : ControllerBase
    {
        private readonly IMediator mediator;

        public WorkRequestStatesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this.mediator.Send(new GetWorkRequestStateByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("GetWorkRequestStates/{workRequestId}")]
        public async Task<IActionResult> GetWorkRequestStates(int workRequestId)
        {
            var result = await this.mediator.Send(new GetWorkStatesByWorkRequestIdQuery(workRequestId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkRequestStateCommand command)
        {
            var result = await this.mediator.Send(command);
            return Created("", result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateWorkRequestStateCommand command)
        {
            await this.mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await this.mediator.Send(new DeleteWorkRequestStateCommand(id));
            return NoContent();
        }
    }
}
