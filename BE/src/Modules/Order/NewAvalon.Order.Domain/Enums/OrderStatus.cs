using System.ComponentModel.DataAnnotations;

namespace NewAvalon.Order.Domain.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// No priority.
        /// </summary>
        [Display(Name = "Shipping")]
        Shipping = 0,

        /// <summary>
        /// Attention level priority.
        /// </summary>
        [Display(Name = "Finished")]
        Finished = 1,

        /// <summary>
        /// Important level priority.
        /// </summary>
        [Display(Name = "Cancelled")]
        Cancelled = 2
    }
}
