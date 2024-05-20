using Hangfire.SubPub;
using Microsoft.AspNetCore.Mvc;
using Registration.Api.Events;
using Registration.Api.Models;

namespace Registration.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IHangfireEventHandlerContainer _hangfireEventHandlerContainer;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ILogger<RegistrationController> logger, IHangfireEventHandlerContainer hangfireEventHandlerContainer)
        {
            _logger = logger;
            _hangfireEventHandlerContainer = hangfireEventHandlerContainer;
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            _hangfireEventHandlerContainer.Publish(new RegisterEvent
            {
                Email = model.Email,
                Date = DateTimeOffset.Now,
            });
            return Ok();
        }
    }
}