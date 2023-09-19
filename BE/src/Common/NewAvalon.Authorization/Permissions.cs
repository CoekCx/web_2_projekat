using System.ComponentModel.DataAnnotations;

namespace NewAvalon.Authorization
{
    public enum Permissions
    {
        /// <summary>
        /// Read user.
        /// </summary>
        [Display(GroupName = "User", Name = "Read", Description = "Can read users")]
        UserRead = 1,

        /// <summary>
        /// Read order.
        /// </summary>
        [Display(GroupName = "Order", Name = "Read", Description = "Can read orders")]
        OrderRead = 100,

        /// <summary>
        /// Read order.
        /// </summary>
        [Display(GroupName = "Order", Name = "Create", Description = "Can create orders")]
        OrderCreate = 101,

        /// <summary>
        /// Read order.
        /// </summary>
        [Display(GroupName = "Order", Name = "Delete", Description = "Can delete orders")]
        OrderDelete = 102,

        /// <summary>
        /// Create product.
        /// </summary>
        [Display(GroupName = "Product", Name = "Create", Description = "Can create products")]
        ProductCreate = 200,

        /// <summary>
        /// Create product.
        /// </summary>
        [Display(GroupName = "Product", Name = "Update", Description = "Can update products")]
        ProductUpdate = 201,

        /// <summary>
        /// Create product.
        /// </summary>
        [Display(GroupName = "Product", Name = "Delete", Description = "Can delete products")]
        ProductDelete = 202,

        /// <summary>
        /// Create product.
        /// </summary>
        [Display(GroupName = "Dealer", Name = "Read", Description = "Can read dealer")]
        DealerRead = 300,

        /// <summary>
        /// Create product.
        /// </summary>
        [Display(GroupName = "Dealer", Name = "Update", Description = "Can update dealer")]
        DealerUpdate = 301,
    }
}
