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
                var user = _context.ApplicationUsers.SingleOrDefault(u => u.Email == email);
                var isPasswordValid = user != null ? PasswordHasher.VerifyPassword(password, user.Password) : false;
                if (isPasswordValid)
                    return user;
            }
            catch (Exception ex)
            {
                
            }

            return null;
        }

        public void Register(ApplicationUser user)
        {
            try
            {
                // Hash Password
                user.Password = PasswordHasher.HashPassword(user.Password);
                _context.ApplicationUsers.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

