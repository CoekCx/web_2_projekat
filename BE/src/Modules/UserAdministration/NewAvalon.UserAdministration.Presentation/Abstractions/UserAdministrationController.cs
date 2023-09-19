using Microsoft.AspNetCore.Mvc;
using NewAvalon.Presentation.Controllers;

namespace NewAvalon.UserAdministration.Presentation.Abstractions
{
    /// <summary>
    /// Represents the base User Administration controller.
    /// </summary>
    [Route("api/administration/[controller]")]
    public abstract class UserAdministrationController : BaseController
    {
    }
}
