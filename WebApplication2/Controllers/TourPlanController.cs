using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DataAccess.Models;
using WebApplication2.Authorization;

namespace WebApplication2.Controllers
{

    public partial class TourPlanModel
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public int CityId { get; set; }

        public double Rating { get; set; }

    }
    [Authorize]
        [Route("api/[controller]")]
    [ApiController]
    public class TourPlanController : ControllerBase
    {
        public PractikaContext Context { get; set; }
        public TourPlanController(PractikaContext context) { Context = context; }

        [HttpGet]
        public IActionResult Get()
        {
            List<TourPlan> list = Context.TourPlans.ToList();
            return Ok(list);
        }
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            TourPlan? country = Context.TourPlans.Where(x => x.Id == id).FirstOrDefault();
            if (country == null) return BadRequest("not found");
            return Ok(country);
        }
        [HttpPost]
        public IActionResult Add(TourPlanModel model)
        {
            var model1 = new TourPlan()
            {
                Name = model.Name,
                Rating = model.Rating,
                CityId = model.CityId,
            };

            if (model.Rating < 0 || model.Rating > 5) return BadRequest("рейтинг должен быть от 0 до 5");
            bool IdExist = Context.Cities.Any(x => x.Id == model1.CityId);
            if (!IdExist) return BadRequest("такого города нет");
            Context.TourPlans.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpPut]
        public IActionResult Update(TourPlanModel model)
        {
            var model1 = new TourPlan()
            {
                Id = model.Id,
                Name = model.Name,
                Rating = model.Rating,
                CityId = model.CityId,
            };

            if (model.Rating < 0 || model.Rating > 5) return BadRequest("рейтинг должен быть от 0 до 5");
            bool CExist = Context.Cities.Any(x => x.Id == model1.CityId);
            if (!CExist) return BadRequest("такого города нет");
            bool IdExist = Context.TourPlans.Any(x => x.Id == model1.Id);
            if (!IdExist) return BadRequest("not found");
            Context.TourPlans.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            TourPlan? model = Context.TourPlans.Where(x => x.Id == id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.TourPlans.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }
    }
}
