using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetMemberUsers;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetProfille;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetStatics;
using UpSchool.HelpDesk.BusinessLayer.CQRS.GetUserName;
using UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateProfile;

namespace UpSchool.HelpDesk.APILayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserName(int id)
        {
            var result = await this.mediator.Send(new GetUserNameByIdQuery(id));

            return Ok(result);
        }

        [HttpGet("GetMembers")]
        public async Task<IActionResult> GetMembers()
        {
            var result = await this.mediator.Send(new GetMemberUserListQuery());

            return Ok(result);
        }

        [HttpGet("GetProfile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var result = await this.mediator.Send(new GetProfileQuery(id));

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UpdateProfileCommand command)
        {
            await this.mediator.Send(command);
            return NoContent();
        }



        [HttpGet("Statics")]

        public async Task<IActionResult> GetStatics()
        {
            var result = await this.mediator.Send(new GetStaticsQuery());

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("StaticForGraph")]
        public async Task<IActionResult> GetStaticForGraph()
        {
            var result = await this.mediator.Send(new GetStaticForGraphQuery());
            return Ok(result);
        }
    }
}
