using System;

namespace GildedRose.DTO
{
    /// <summary>
    /// User Response model when new user has been created
    /// </summary>
    public class UserResponseModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }
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
        /// User created date time utc
        /// </summary>
        public DateTime AddedAt { get; set; }
    }
}
