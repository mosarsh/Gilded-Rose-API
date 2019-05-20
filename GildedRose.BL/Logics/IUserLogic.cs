using GildedRose.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.BL.Logics
{
    /// <summary>
    /// User Logic Interface
    /// </summary>
    public interface IUserLogic
    {
        /// <summary>
        /// Add method contract
        /// </summary>
        /// <param name="userModel">User model</param>
        /// <returns>User Response model</returns>
        UserResponseModel Add(UserRequestModel userModel);

        /// <summary>
        /// Check User method contract
        /// </summary>
        /// <param name="model">User Login model</param>
        /// <returns></returns>
        Task<bool> CheckUser(UserLoginModel model);

        /// <summary>
        /// Get User By Email method contract
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>User Identity model</returns>
        UserIdentityModel GetUserByEmail(string email);
    }
}
