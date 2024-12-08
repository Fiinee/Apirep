using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    public partial class TourPlan
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public int CityId { get; set; }

        public double Rating { get; set; }

    }
        [Route("api/[controller]")]
    [ApiController]
    public class TourPlanController : ControllerBase
    {
    }
}
