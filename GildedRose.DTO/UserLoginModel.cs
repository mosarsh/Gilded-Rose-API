namespace GildedRose.DTO
{
    /// <summary>
    /// User Login Model
    /// </summary>
    public class UserLoginModel
    {
        /// <summary>
        /// User Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User Confirm Password
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
