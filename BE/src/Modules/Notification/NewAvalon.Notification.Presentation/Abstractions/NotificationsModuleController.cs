using Microsoft.AspNetCore.Mvc;
using NewAvalon.Presentation.Controllers;

namespace NewAvalon.Notification.Presentation.Abstractions
{
    /// <summary>
    /// Represents the base Notification module controller.
    /// </summary>
    [Route("api/notification/[controller]")]
    public abstract class NotificationModuleController : BaseController
    {
    }
}
