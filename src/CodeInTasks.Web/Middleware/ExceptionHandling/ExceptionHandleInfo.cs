namespace CodeInTasks.Web.Middleware.ExceptionHandling
{
    public record struct ExceptionHandleInfo(Type ExceptionType, int ResultStatusCode, string ResultMessage);
}
