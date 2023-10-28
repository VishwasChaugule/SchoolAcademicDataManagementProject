using System;
using SchoolAcademicDataManagement.Models.User;

namespace SchoolAcademicDataManagement.Services
{
	public interface IUserService
	{
        ApplicationUser Authenticate(string email, string password);
        void Register(ApplicationUser user);
    }
}

