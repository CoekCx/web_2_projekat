using Microsoft.AspNetCore.Mvc;
using NewAvalon.Presentation.Controllers;

namespace NewAvalon.Order.Presentation.Abstractions
{
    /// <summary>
    /// Represents the base Order controller.
    /// </summary>
    [Route("api/order/[controller]")]
    public abstract class OrderController : BaseController
    {
    }
}