using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Authorization;
using WebApplication2.DataAccess.Models;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public partial class GuideModel
    {
        public int EmployeeCode { get; set; }

        public string Name { get; set; }

        public int Agency { get; set; }

        public double Rating { get; set; }
    }

    [Authorize]
        [Route("api/[controller]")]
    [ApiController]
    public class GuideController : BaseController
    {
        public PractikaContext Context { get; set; }
        public GuideController(PractikaContext context) { Context = context; }

       // [Authorize(Role.Admin)]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            List<Guide> list = Context.Guides.ToList();
            return Ok(list);
        }
       // [Authorize(Role.Admin)]
         [AllowAnonymous]
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            Guide? country = Context.Guides.Where(x => x.EmployeeCode == id).FirstOrDefault();
            if (country == null) return BadRequest("not found");
            return Ok(country);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(GuideModel model)
        {
            var model1 = new Guide()
            {
                Name = model.Name,
                Agency = model.Agency,
                Rating = model.Rating,

            };

            if (model.Rating < 0 || model.Rating > 5) return BadRequest("рейтинг должен быть от 0 до 5");
            Context.Guides.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(GuideModel model)
        {
            var model1 = new Guide()
            {
                Name = model.Name,
                Agency = model.Agency,
                Rating = model.Rating,
                EmployeeCode = model.EmployeeCode,

            };

            if (model.Rating < 0 || model.Rating > 5) return BadRequest("рейтинг должен быть от 0 до 5");
            bool IdExist = Context.Guides.Any(x => x.EmployeeCode == model1.EmployeeCode);
            if (!IdExist) return BadRequest("not found");
            Context.Guides.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Guide? model = Context.Guides.Where(x => x.EmployeeCode == id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.Guides.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }
    }
}
