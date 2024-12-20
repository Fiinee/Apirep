using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DataAccess.Models;
using WebApplication2.Authorization;

namespace WebApplication2.Controllers
{
    public partial class Tour2Model
    {
        public int TourCode { get; set; }

        public int TourPlan { get; set; }

        public int GuideCode { get; set; }

        public DateTime DateTime { get; set; }
    }
    [Authorize]
        [Route("api/[controller]")]
    [ApiController]
    public class Tour2Controller : ControllerBase
    {
        public PractikaContext Context { get; set; }
        public Tour2Controller(PractikaContext context) { Context = context; }

        [HttpGet]
        public IActionResult Get()
        {
            List<Tour2> list = Context.Tour2s.ToList();
            return Ok(list);
        }
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            Tour2? country = Context.Tour2s.Where(x => x.TourCode== id).FirstOrDefault();
            if (country == null) return BadRequest("not found");
            return Ok(country);
        }
        [HttpPost]
        public IActionResult Add(Tour2Model model)
        {
            var model1 = new Tour2()
            {
                DateTime = model.DateTime,
                TourPlan = model.TourPlan,
                GuideCode = model.GuideCode,
            };

            bool TourExist = Context.TourPlans.Any(x => x.Id == model1.TourPlan);
            if (!TourExist) return BadRequest("такого плана нет");
            bool GExist = Context.Guides.Any(x => x.EmployeeCode == model1.GuideCode);
            if (!GExist) return BadRequest("такого гида нет");
            Context.Tour2s.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpPut]
        public IActionResult Update(Tour2Model model)
        {
            var model1 = new Tour2()
            {
                TourCode = model.TourCode,
                DateTime = model.DateTime,
                TourPlan = model.TourPlan,
                GuideCode = model.GuideCode,
            };

            bool TourExist = Context.TourPlans.Any(x => x.Id == model1.TourPlan);
            if (!TourExist) return BadRequest("такого плана нет");
            bool GExist = Context.Guides.Any(x => x.EmployeeCode == model1.GuideCode);
            if (!GExist) return BadRequest("такого гида нет");
            bool IdExist = Context.Tour2s.Any(x => x.TourCode == model1.TourCode);
            if (!IdExist) return BadRequest("not found");
            Context.Tour2s.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Tour2? model = Context.Tour2s.Where(x => x.TourCode == id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.Tour2s.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }
    }
}
