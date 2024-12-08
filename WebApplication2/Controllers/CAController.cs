using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public partial class CommentAgencyModel
    {
        public int CommentId { get; set; }

        public int Agency { get; set; }

        public string CommentText { get; set; }
    }

        [Route("api/[controller]")]
    [ApiController]
    public class CAController : ControllerBase
    {
        public PractikaContext Context { get; set; }
        public CAController(PractikaContext context) { Context = context; }

        [HttpGet]
        public IActionResult Get()
        {
            List<CommentAgency> list = Context.CommentAgencies.ToList();
            return Ok(list);
        }
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            CommentAgency? country = Context.CommentAgencies.Where(x => x.CommentId == id).FirstOrDefault();
            if (country == null) return BadRequest("такого id нет");
            return Ok(country);
        }
        [HttpPost]
        public IActionResult Add(CommentAgencyModel model)
        {
            var model1 = new CommentAgency()
            {
                CommentText = model.CommentText,
                Agency = model.Agency,
                
            };

            bool IdExist = Context.Agencies.Any(x => x.Id == model1.Agency);
            if (!IdExist) return BadRequest("такого агенства нет");
            Context.CommentAgencies.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpPut]
        public IActionResult Update(CommentAgencyModel model)
        {
            var model1 = new CommentAgency()
            {
                CommentId = model.CommentId,
                CommentText = model.CommentText,
                Agency = model.Agency,

            };

            bool ComExist = Context.CommentAgencies.Any(x => x.CommentId == model1.CommentId);
            if (!ComExist) return BadRequest("такого комментария нет");
            bool IdExist = Context.Agencies.Any(x => x.Id == model1.Agency);
            if (!IdExist) return BadRequest("такого агенства нет");
            Context.CommentAgencies.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            CommentAgency? model = Context.CommentAgencies.Where(x => x.CommentId == id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.CommentAgencies.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }

    }
}
