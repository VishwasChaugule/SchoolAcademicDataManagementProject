using SchoolAcademicDataManagement.Data;
using SchoolAcademicDataManagement.Models.User;
using SchoolAcademicDataManagement.Utilities;

namespace SchoolAcademicDataManagement.Services
{
    public class UserService : IUserService
    {
        private readonly SchoolDBContext _context;

        public UserService(SchoolDBContext context)
        {
            _context = context;
        }

        public ApplicationUser Authenticate(string email, string password)
        {
            try
            {
                // Get user by email
                var user = GetUserByEmail(email);
                // Check user input password is valid or not
                if (user != null && PasswordIsValid(password, user.Password))
                {
                    return user;
                }
            }
            catch (Exception)
            { 
            }

            return null;
        }

        public void Register(ApplicationUser user)
        {
            try
            {
                // Hash the input Password
                user.Password = PasswordHasher.HashPassword(user.Password);
                _context.ApplicationUsers.Add(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        private ApplicationUser GetUserByEmail(string email)
        {
            return _context.ApplicationUsers.SingleOrDefault(u => u.Email == email);
        }

        private bool PasswordIsValid(string password, string hashedPassword)
        {
            return PasswordHasher.VerifyPassword(password, hashedPassword);
        }
    }
}

