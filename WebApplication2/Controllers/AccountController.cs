using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public partial class AccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public string Email { get; set; }

        public string Password { get; set; } 
    }

        [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
    }
}
