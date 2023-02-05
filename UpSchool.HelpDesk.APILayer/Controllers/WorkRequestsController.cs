using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpSchool.HelpDesk.BusinessLayer.CQRS.AssignUser;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CompletedWorkRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetAllWorkRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequestByUserId;
using UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequest;

namespace UpSchool.HelpDesk.APILayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkRequestsController : ControllerBase
    {
        /* Tüm işlerin listelenmesi : */
        /* İlgili usera ait işlerin listelenmesi*/
        /*  */

        private readonly IMediator _mediator;

        public WorkRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllWorkRequestQuery());
            return Ok(result);
        }

        [Authorize(Roles ="Admin, Member")]
        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _mediator.Send(new GetWorkRequestByUserIdQuery(userId));
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Created("",result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateWorkRequestCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetWorkRequestByIdQuery(id));
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> AssignUser(AssignUserCommand command)
        {
             await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPut("[action]")]
        public async Task<IActionResult> SetCompleted(CompletedWorkRequestCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
