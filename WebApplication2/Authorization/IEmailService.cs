namespace WebApplication2.Authorization
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
