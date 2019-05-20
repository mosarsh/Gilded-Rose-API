using System;

namespace GildedRose.DTO
{
    /// <summary>
    /// User Request Model for adding new user
    /// </summary>
    public class UserRequestModel
    {
        /// <summary>
        /// User First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User Last Name
        /// </summary>
        public string LastName { get; set; }

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
