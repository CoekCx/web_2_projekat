using Microsoft.AspNetCore.Mvc;
using NewAvalon.Presentation.Controllers;

namespace NewAvalon.Storage.Presentation.Abstractions
{
    /// <summary>
    /// Represents the base Storage module controller.
    /// </summary>
    [Route("api/storage/[controller]")]
    public abstract class StorageModuleController : BaseController
    {
    }
}
