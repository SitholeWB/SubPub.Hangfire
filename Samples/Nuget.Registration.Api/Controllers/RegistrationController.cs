using SubPub.Hangfire;
using Microsoft.AspNetCore.Mvc;
using Nuget.Registration.Api.Events;
using Nuget.Registration.Api.Models;

namespace Nuget.Registration.Api.Controllers
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
            var registerEvent = new RegisterEvent
            {
                Email = model.Email,
                Date = DateTimeOffset.Now,
            };

            _hangfireEventHandlerContainer.Publish(registerEvent);

            return Ok();
        }

        [HttpPost("royal")]
        public IActionResult RegisterRoyal(RegisterModel model)
        {
            var registerEvent = new RoyalRegisterEvent
            {
                Email = model.Email,
                Date = DateTimeOffset.Now,
            };

            _hangfireEventHandlerContainer.Publish(registerEvent);

            return Ok();
        }

        [HttpPost("base")]
        public IActionResult RegisterBase(RegisterModel model)
        {
            var registerEvent = new BaseRegisterEvent
            {
                PhysicalAddress = "Somewhere peaceful",
                PostalAddress = "12 peaceful street, Nice Place 4001",
            };

            _hangfireEventHandlerContainer.Publish(registerEvent);

            return Ok();
        }

        [HttpPost("schedule")]
        public IActionResult RegisterSchedule(RegisterModel model)
        {
            var registerEvent = new RegisterEvent
            {
                Email = model.Email,
                Date = DateTimeOffset.Now,
            };

            var options = new HangfireJobOptions
            {
                HangfireJobType = HangfireJobType.Schedule,
                TimeSpan = TimeSpan.FromSeconds(15)
            };

            _hangfireEventHandlerContainer.Publish(registerEvent, options);

            return Ok();
        }
    }
}