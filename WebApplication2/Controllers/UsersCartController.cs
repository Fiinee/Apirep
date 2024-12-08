using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public partial class UsersCartModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TourCode { get; set; }
    }
        [Route("api/[controller]")]
    [ApiController]
    public class UsersCartController : ControllerBase
    {
    }
}
