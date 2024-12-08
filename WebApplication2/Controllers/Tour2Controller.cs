using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public partial class Tour2Model
    {
        public int TourCode { get; set; }

        public int TourPlan { get; set; }

        public int GuideCode { get; set; }

        public DateTime DateTime { get; set; }
    } 

        [Route("api/[controller]")]
    [ApiController]
    public class Tour2Controller : ControllerBase
    {
    }
}
