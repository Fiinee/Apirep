using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DataAccess.Models;
using WebApplication2.Authorization;

namespace WebApplication2.Controllers
{
    public partial class UsersCartModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TourCode { get; set; }
    }
    [Authorize]
        [Route("api/[controller]")]
    [ApiController]
    public class UsersCartController : ControllerBase
    {
        public PractikaContext Context { get; set; }
        public UsersCartController(PractikaContext context) { Context = context; }

        
        [HttpGet]
        public IActionResult Get()
        {
            List<UsersCart> list = Context.UsersCarts.ToList();
            return Ok(list);
        }
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            UsersCart? country = Context.UsersCarts.Where(x => x.TourCode == id).FirstOrDefault();
            if (country == null) return BadRequest("not found");
            return Ok(country);
        }
        [HttpPost]
        public IActionResult Add(UsersCartModel model)
        {
            var model1 = new UsersCart()
            {
               TourCode = model.TourCode,
               UserId = model.UserId,
            };

            bool TourExist = Context.Tour2s.Any(x => x.TourCode == model1.TourCode);
            if (!TourExist) return BadRequest("такого тура нет");
            bool GExist = Context.Accounts.Any(x => x.Id == model1.UserId);
            if (!GExist) return BadRequest("такого пользователя нет");
            Context.UsersCarts.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpPut]
        public IActionResult Update(UsersCartModel model)
        {
            var model1 = new UsersCart()
            {
                Id = model.Id,
                TourCode = model.TourCode,
                UserId = model.UserId,
            };

            bool TourExist = Context.Tour2s.Any(x => x.TourCode == model1.TourCode);
            if (!TourExist) return BadRequest("такого тура нет");
            bool GExist = Context.Accounts.Any(x => x.Id == model1.UserId);
            if (!GExist) return BadRequest("такого пользователя нет");
            bool IdExist = Context.UsersCarts.Any(x => x.Id == model1.Id);
            if (!IdExist) return BadRequest("not found");
            Context.UsersCarts.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            UsersCart? model = Context.UsersCarts.Where(x => x.TourCode == id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.UsersCarts.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }
    }
}
