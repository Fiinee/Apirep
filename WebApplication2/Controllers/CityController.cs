using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public partial class CityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
        [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public PractikaContext Context { get; set; }
        public CityController(PractikaContext context) { Context = context; }

        [HttpGet]
        public IActionResult Get()
        {
            List<City> list = Context.Cities.ToList();
            return Ok(list);
        }
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            City? country = Context.Cities.Where(x => x.Id == id).FirstOrDefault();
            if (country == null) return BadRequest("такого города нет");
            return Ok(country);
        }
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
