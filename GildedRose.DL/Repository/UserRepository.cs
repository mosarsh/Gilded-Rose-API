using System.Data.Entity;

namespace GildedRose.DL.Repository
{
    /// <summary>
    /// User Rpository Class
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }
    }
}
