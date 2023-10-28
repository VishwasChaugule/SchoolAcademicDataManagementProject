using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolAcademicDataManagement.Filters
{
    public class AdminAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userRole = string.Empty;
            var storedData = context.HttpContext.Session.Get("UserRole");
            if (storedData != null)
            {
                userRole = Encoding.UTF8.GetString(storedData);
                // Use the originalData as needed
                // Check if user has admin role
                
            }

            if (userRole != "Admin")
            {
                context.Result = new RedirectResult("/Account/Login"); // Redirect to login page
            }
        }
    }
}

