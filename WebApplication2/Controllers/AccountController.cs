using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Authorization;
using WebApplication2.DataAccess.Models;

namespace WebApplication2.Controllers
{
    public partial class AccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public string Email { get; set; }

        public string Password { get; set; } 
    }
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
 
    public class AccountController : BaseController
    {
        public PractikaContext Context { get; set; }
        public AccountController(PractikaContext context) { Context = context; }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            List<Account> list = Context.Accounts.ToList();
            return Ok(list);
        }
        [HttpGet("get-by-id")]
        public IActionResult GetId(int id)
        {
            Account? country = Context.Accounts.Where(x => x.Id == id).FirstOrDefault();
            if (country == null) return BadRequest("такого пользователя нет");
            return Ok(country);
        }
        [HttpPost]
        public IActionResult Add(AccountModel model)
        {
            var model1 = new Account()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
               
            };
            bool NameExist = Context.Accounts.Any(x => x.Name == model1.Name);
            if (NameExist) return BadRequest("имя уже используется");
            bool EmExist = Context.Accounts.Any(x => x.Email == model1.Email);
            if (EmExist) return BadRequest("почта уже используется");

            Context.Accounts.Add(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpPut]
        public IActionResult Update(AccountModel model)
        {
            var model1 = new Account()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,

            };
         

            bool NameExist = Context.Accounts.Any(x => x.Name == model1.Name && x.Id != model.Id);
            if (NameExist) return BadRequest("имя уже используется");
            bool EmExist = Context.Accounts.Any(x => x.Email == model1.Email && x.Id != model.Id);
            if (EmExist) return BadRequest("почта уже используется");
            bool IdExist = Context.Accounts.Any(x => x.Id == model1.Id);
            if (!IdExist) return BadRequest("такого пользователя нет");
            Context.Accounts.Update(model1);
            Context.SaveChanges();
            return Ok(model1);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Account? model = Context.Accounts.Where(x => x.Id == id).FirstOrDefault();
            if (model == null) return BadRequest("not found");
            Context.Accounts.Remove(model);
            Context.SaveChanges();
            return Ok(model);
        }
    }
}
