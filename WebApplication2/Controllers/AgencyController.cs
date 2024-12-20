using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DataAccess.Models;
using WebApplication2.Entities;
using WebApplication2.Authorization;

namespace WebApplication2.Controllers
{
    public partial class AgencyModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumEmployeer { get; set; }

        public double Rating { get; set; }
    }
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : BaseController
    {
        public PractikaContext Context { get; set; }
        public AgencyController(PractikaContext context) { Context = context; }

       
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            List<Agency> list = Context.Agencies.ToList();
            return Ok(list);
        }
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            Agency? country = Context.Agencies.Where(x => x.Id == id).FirstOrDefault();
            if (country == null) return BadRequest("такого агенства нет");
            return Ok(country);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public IActionResult Add(AgencyModel model)
        {
            var model1 = new Agency()
            {
                Name = model.Name,
                Rating = model.Rating,
                NumEmployeer = model.NumEmployeer
            };
          
            if (model.Rating <0 || model.Rating>5) return BadRequest("рейтинг должен быть от 0 до 5");
            if (model.NumEmployeer < 0) return BadRequest("кол-во работников должно быть не меньше 0");
            Context.Agencies.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [Authorize(Role.Admin)]
        [HttpPut]
        public IActionResult Update(AgencyModel model)
        {
            var model1 = new Agency()
            {
                Id = model.Id,
                Name = model.Name,
                Rating = model.Rating,
                NumEmployeer = model.NumEmployeer
            };

            if (model.Rating < 0 || model.Rating > 5) return BadRequest("рейтинг должен быть от 0 до 5");
            if (model.NumEmployeer < 0) return BadRequest("кол-во работников должно быть не меньше 0");
            bool IdExist = Context.Agencies.Any(x => x.Id == model1.Id);
            if (!IdExist) return BadRequest("такого агенства нет");
            Context.Agencies.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Agency? model = Context.Agencies.Where(x => x.Id == id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.Agencies.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }

    }
}
