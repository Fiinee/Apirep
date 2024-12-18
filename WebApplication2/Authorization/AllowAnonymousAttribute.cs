namespace WebApplication2.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute:Attribute
    {
    }
}
