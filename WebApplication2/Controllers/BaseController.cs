using Microsoft.AspNetCore.Mvc;
using WebApplication2.DataAccess.Models;
namespace WebApplication2.Controllers
{
    [Controller]
    public class BaseController : ControllerBase
    {
        public Account Account => (Account)HttpContext.Items["User"];
    }
}
