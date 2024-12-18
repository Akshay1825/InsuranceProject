using InsuranceProject.Models;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace InsuranceProject.Exceptions
{
    public class ExceptionHandler:IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            var response = new ErrorResponse();
            if (exception is CustomerNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is AdminNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is InsurancePlanNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is AgentNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is EmployeeNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is DataNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is DocumentNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is SchemeNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is RoleNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is EmployeeNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is PolicyNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is UserNotFoundException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            if (exception is UserNameExistsException)
            {
                response.ErrorCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            {
                response.ErrorCode = StatusCodes.Status500InternalServerError;
                response.ExceptionMessage = exception.Message;
                response.Title = "Something Went Wrong";
            }
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            Log.Error("Error Occurred: " + exception.Message);
            return true;
        }
    }
}
