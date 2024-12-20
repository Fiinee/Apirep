using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DataAccess.Models;
using WebApplication2.Authorization;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public partial class CountryModel
    {
        public string Name { get; set; } 

        public int Population { get; set; }

        
    }
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        public PractikaContext Context { get; set; }
        public CountryController(PractikaContext context) { Context = context; }

        [HttpGet]
        public IActionResult Get()
        {
            List<Country> list = Context.Countries.ToList();
            return Ok(list);
        }
        [HttpGet("get-by-name")]
        public IActionResult GetId(string name)
        {
            Country? country = Context.Countries.Where(x => x.Name == name).FirstOrDefault();
            if (country == null) return BadRequest("такой страны нет");
            return Ok(country);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(CountryModel model)
        {
            var model1 = new Country()
            {
                Name = model.Name,
                Population = model.Population,
            };
            bool NameExist = Context.Countries.Any(x => x.Name == model1.Name);
            if (NameExist) return BadRequest("такая страна уже есть");
            Context.Countries.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(CountryModel model)
        {
            var model1 = new Country()
            {
                Name = model.Name,
                Population = model.Population,
            };
            bool NameExist = Context.Countries.Any(x => x.Name == model1.Name);
            if (!NameExist) return BadRequest("такой страны нет");
            if(model.Population<0) return BadRequest("население должно быть не меньше 0");
            Context.Countries.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [Authorize(Role.Admin)]
        // [AllowAnonymous]
        [HttpDelete]
        public IActionResult Delete(string name)
        {
            Country? country = Context.Countries.Where(x => x.Name == name).FirstOrDefault();
            if (country == null) return BadRequest("такой страны нет");
            Context.Countries.Remove(country);
            Context.SaveChanges();
            return Ok(country);
        }

    }
}
