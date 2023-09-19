namespace NewAvalon.Domain.Errors
{
    /// <summary>
    /// Represents the error.
    /// </summary>
    /// <param name="Code">The error code.</param>
    /// <param name="Description">The error description.</param>
    /// <remarks>
    /// When introducing a new region for errors, increment the starting error code by 100.
    /// </remarks>>
    public sealed record Error(int Code, string Description)
    {
        #region Common

        public static Error Default => new(1, "Default error");

        public static Error BadRequest => new(2, "Bad request");

        public static Error NotFound => new(3, "Not found");

        public static Error Conflict => new(4, "Conflict");

        public static Error UnProcessableEntity => new(5, "Un-processable entity");

        public static Error ServerError => new(9, "Server error");

        public static Error ValidationFailure => new(10, "Validation failure");

        #endregion

        #region Users

        public static Error UserNotFound => new(1000, "User not found");

        public static Error UserAlreadyExists => new(1001, "User already exists");

        public static Error DealerAlreadyProcessed => new(1002, "Dealer already processed");

        public static Error IncorrectPassword => new(1003, "Incorrect password");

        #endregion

        #region Products

        public static Error ProductNotFound => new(2000, "Product not found");

        public static Error ProductInvalidQuantity => new(2001, "Product invalid quantity");

        public static Error ProductCannotBeDeleted => new(2002, "Product cannot be deleted");

        #endregion

        #region Storage

        public static Error ImageNotFound => new(3000, "Image not found");

        public static Error FileNotFound => new(3001, "File not found");

        public static Error FileAlreadyAttached => new(3002, "File already attached");

        #endregion

        #region Order

        public static Error OrderNotFound => new(4000, "Order not found");

        #endregion
    }
}
