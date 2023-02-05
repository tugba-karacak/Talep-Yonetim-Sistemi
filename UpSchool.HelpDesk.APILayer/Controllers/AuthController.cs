using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpSchool.HelpDesk.BusinessLayer.CQRS.LoginRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.RegisterRequest;

namespace UpSchool.HelpDesk.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Created("", result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Created("", result);
        }
    }
}
