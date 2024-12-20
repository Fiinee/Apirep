using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Authorization;
using WebApplication2.DataAccess.Models;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public partial class CityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
    [Authorize]
        [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        public PractikaContext Context { get; set; }
        public CityController(PractikaContext context) { Context = context; }

      //  [Authorize(Role.Admin)]
         [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            List<City> list = Context.Cities.ToList();
            return Ok(list);
        }
       // [Authorize(Role.Admin)]
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            City? country = Context.Cities.Where(x => x.Id == id).FirstOrDefault();
            if (country == null) return BadRequest("такого города нет");
            return Ok(country);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(CityModel model)
        {
            var model1 = new City()
            {
                Name = model.Name,
                Country = model.Country,
            };
            bool NameExist = Context.Countries.Any(x => x.Name == model1.Country);
            if (!NameExist) return BadRequest("такой страны нет");
            Context.Cities.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(CityModel model)
        {
            var model1 = new City()
            {
                Name = model.Name,
                Id = model.Id,
                Country = model.Country,
            };
            bool NameExist = Context.Countries.Any(x => x.Name == model1.Country);
            if (!NameExist) return BadRequest("такой страны нет");
            bool IdExist = Context.Cities.Any(x => x.Name == model1.Name);
            if (!IdExist) return BadRequest("такой страны нет");
            Context.Cities.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            City? model = Context.Cities.Where(x => x.Id== id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.Cities.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }
    }
}
