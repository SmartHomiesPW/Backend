using SmartHomeBackend.Database;
using SmartHomeBackend.Models;
using SmartHomeBackend.Models.Dto;

namespace SmartHomeBackend.Services
{
    /// <summary>
    /// Service responsible for operations associated with authentication.
    /// </summary>
    public class AuthService
    {
        private readonly SmartHomeDbContext _context;

        public AuthService(SmartHomeDbContext context)
        {
            _context = context;
        }

        /// <summary>Makes necessary checks and operations to create a new user.</summary>
        /// <returns>Created user on success.</returns>
        public User? CreateNewUser(UserRegistrationDto model)
        {
            bool userWithGivenEmailAlreadyExists = CheckIfEmailIsAlreadyInUse(model.Email);

            if (userWithGivenEmailAlreadyExists)
            {
                return null;
            }

            var user = new User
            {
                User_Id = Guid.NewGuid(),
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        /// <summary>Removes a specific user from database.</summary>
        /// <returns>User that was deleted on success.</returns>
        public User? RemoveUser(Guid user_Id)
        {
            var userToDelete = _context.Users.First(u => u.User_Id.Equals(user_Id));

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
            
            return userToDelete;
        }

        /// <summary>Verifies user's credentials.</summary>
        /// <returns>True on proper user's credentials and false otherwise.</returns>
        public bool VerifyUser(UserLoginDto model)
        {
            return _context.Users.Any(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password));
        }

        /// <summary>Checks if database contains any user with given email address.</summary>
        /// <returns>True on email already in use and false otherwise.</returns>
        public bool CheckIfEmailIsAlreadyInUse(string email)
        {
            return _context.Users.Any(u => u.Email.Equals(email));
        }

        /// <summary>Finds user in database using only his login credentials.</summary>
        /// <returns>User with given login credentials on success.</returns>
        public User? FindUserFromLogin(UserLoginDto model)
        {
            return _context.Users.First(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password));
        }
    }
}
