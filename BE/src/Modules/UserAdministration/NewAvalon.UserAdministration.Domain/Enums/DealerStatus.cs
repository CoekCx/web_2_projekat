using System.ComponentModel.DataAnnotations;

namespace NewAvalon.UserAdministration.Domain.Enums
{
    public enum DealerStatus
    {
        /// <summary>
        /// No priority.
        /// </summary>
        [Display(Name = "Pending")]
        Pending = 0,

        /// <summary>
        /// Attention level priority.
        /// </summary>
        [Display(Name = "Approved")]
        Approved = 1,

        /// <summary>
        /// Important level priority.
        /// </summary>
        [Display(Name = "Disapproved")]
        Disapproved = 2
    }
}
