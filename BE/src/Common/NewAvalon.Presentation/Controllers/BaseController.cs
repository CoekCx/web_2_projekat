using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace NewAvalon.Presentation.Controllers
{
    /// <summary>
    /// Represents the base API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private ISender _sender;

        /// <summary>
        /// Gets the sender.
        /// </summary>
        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
