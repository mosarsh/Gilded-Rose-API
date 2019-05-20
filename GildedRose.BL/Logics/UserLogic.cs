using AutoMapper;
using GildedRose.BL.Utilities;
using GildedRose.DL;
using GildedRose.DL.UoW;
using GildedRose.DTO;
using System;
using System.Threading.Tasks;

namespace GildedRose.BL.Logics
{
    /// <summary>
    /// User Logic Class
    /// </summary>
    public class UserLogic : IUserLogic
    {
        /// <summary>
        /// Local Property for mapper
        /// </summary>
        public IMapper _mapper { get; private set; }

        /// <summary>
        /// Local property for unit of work
        /// </summary>
        public IUnitOfWork _unitOfWork { get; private set; }

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="unitOfWork">UnitofWork instance</param>
        public UserLogic(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Add new User
        /// </summary>
        /// <param name="userModel">User model</param>
        /// <returns>User response model when user is created</returns>
        public UserResponseModel Add(UserRequestModel userModel)
        {
            string _salt = CryptographyProcessor.CreateSalt(16);
            string _passwordHash = CryptographyProcessor.GenerateHash(userModel.Password, _salt);

            var user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                PasswordHash = _passwordHash,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                Salt = _salt,
                AddedAt = DateTime.UtcNow
            };

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();

            return _mapper.Map<UserResponseModel>(user);
        }

        /// <summary>
        /// Check user method to verify if such a user exists
        /// </summary>
        /// <param name="model">User login model</param>
        /// <returns>true/false if user found/not found</returns>
        public async Task<bool> CheckUser(UserLoginModel model)
        {
            var user = await _unitOfWork.UserRepository.FindOneAsync(u => u.Email == model.Email);

            if (user == null)
            {
                throw new ArgumentException($"User with Email: {model.Email} doesn't exists");
            }

            if (!CryptographyProcessor.AreEqual(model.Password, user.PasswordHash, user.Salt))
            {
                throw new ArgumentException("You have entered an invalid password");
            }

            return true;
        }

        /// <summary>
        /// Get User By Email to get user Identity data to supply Principal
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User Identity model</returns>
        public UserIdentityModel GetUserByEmail(string email)
        {
            User user = _unitOfWork.UserRepository.FindOne(u => u.Email == email);
            return _mapper.Map<UserIdentityModel>(user);
        }
    }
}
