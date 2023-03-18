using Microsoft.AspNetCore.Mvc.Filters;

namespace ReadingHub.Cores.Services
{
    public class ExceptionResponseFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 409;
            base.OnException(context);
        }
    }
}
