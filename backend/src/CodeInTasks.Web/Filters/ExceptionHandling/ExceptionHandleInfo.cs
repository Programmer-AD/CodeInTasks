namespace CodeInTasks.Web.Filters.ExceptionHandling
{
    public record struct ExceptionHandleInfo(Type ExceptionType, int ResultStatusCode, string ResultMessage);
}
