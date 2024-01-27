using SmartHomeBackend.Database;
using SmartHomeBackend.Models;
using SmartHomeBackend.Models.Dto;

namespace SmartHomeBackend.Services
{
    public class AuthService
    {
        private readonly SmartHomeDbContext _context;

        public AuthService(SmartHomeDbContext context)
        {
            _context = context;
        }

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

        public bool VerifyUser(UserLoginDto model)
        {
            return _context.Users.Any(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password));
        }

        public bool CheckIfEmailIsAlreadyInUse(string email)
        {
            return _context.Users.Any(u => u.Email.Equals(email));
        }

        public User? FindUserFromLogin(UserLoginDto model)
        {
            return _context.Users.First(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password));
        }
    }
}
