using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Authorization;
using WebApplication2.DataAccess.Models;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public partial class CommentTourModel
    {
        public int CommentId { get; set; }

        public int TourPlan { get; set; }

        public string CommentText { get; set; }
    }

    [Authorize]
        [Route("api/[controller]")]
    [ApiController]
    public class CTController : BaseController
    {
        public PractikaContext Context { get; set; }
        public CTController(PractikaContext context) { Context = context; }

       [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            List<CommentTour> list = Context.CommentTours.ToList();
            return Ok(list);
        }
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            CommentTour? country = Context.CommentTours.Where(x => x.CommentId == id).FirstOrDefault();
            if (country == null) return BadRequest("такого id нет");
            return Ok(country);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(CommentTourModel model)
        {
            var model1 = new CommentTour()
            {
                CommentText = model.CommentText,
                TourPlan = model.TourPlan,
            };

            bool IdExist = Context.TourPlans.Any(x => x.Id == model1.TourPlan);
            if (!IdExist) return BadRequest("такого плана тура нет");
            Context.CommentTours.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(CommentTourModel model)
        {
            var model1 = new CommentTour()
            {
                CommentId = model.CommentId,
                CommentText = model.CommentText,
                TourPlan = model.TourPlan

            };

            bool ComExist = Context.CommentAgencies.Any(x => x.CommentId == model1.CommentId);
            if (!ComExist) return BadRequest("такого комментария нет");
            bool IdExist = Context.TourPlans.Any(x => x.Id == model1.TourPlan);
            if (!IdExist) return BadRequest("такого плана тура нет");
            Context.CommentTours.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [AllowAnonymous]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            CommentTour? model = Context.CommentTours.Where(x => x.CommentId == id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.CommentTours.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }
    }
}
