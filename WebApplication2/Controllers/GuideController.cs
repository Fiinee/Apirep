using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public partial class GuideModel
    {
        public int EmployeeCode { get; set; }

        public string Name { get; set; }

        public int Agency { get; set; }

        public double Rating { get; set; }
    }

        [Route("api/[controller]")]
    [ApiController]
    public class GuideController : ControllerBase
    {
    }
}
