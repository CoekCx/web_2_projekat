using Microsoft.AspNetCore.Mvc;
using NewAvalon.Presentation.Controllers;

namespace NewAvalon.Catalog.Presentation.Abstractions
{
    /// <summary>
    /// Represents the base User Administration controller.
    /// </summary>
    [Route("api/catalog/[controller]")]
    public abstract class CatalogController : BaseController
    {
    }
}
