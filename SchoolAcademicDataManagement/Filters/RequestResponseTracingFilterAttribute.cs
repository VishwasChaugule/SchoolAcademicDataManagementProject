using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolAcademicDataManagement.Filters
{
    public class RequestResponseTracingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Log request details (if needed)
            // Example: Log request parameters, headers, etc.
            // Logger.LogRequest(filterContext.HttpContext.Request);

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                // Log exception details (if needed)
                // Example: Logger.LogException(filterContext.Exception);
            }

            // Log response details (if needed)
            // Example: Log response content, status code, headers, etc.
            // Logger.LogResponse(filterContext.HttpContext.Response);

            base.OnActionExecuted(filterContext);
        }
    }

}

