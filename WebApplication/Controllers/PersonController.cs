using System.Threading.Tasks;
using ClassLibrary2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class PersonController : Controller
    {
        [HttpGet("GetUserByMail/{mail}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<ActionResult<User>> GetPersonByMail(string mail)
        {
            var manageUser = new ManageUser.ManageUser();
            var user = await manageUser.GetUserByMail(mail);
            
            return user.Id == -1 ? NotFound($"User with the mail of {mail} was not found") : Ok(user);
        }

        [HttpGet("GetUserById/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var manageUser = new ManageUser.ManageUser();
            var user = await manageUser.GetUserById(id);
            return user.Id == -1 ? NotFound($"User by the id of {id} was not found") : Ok(user);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<ActionResult> InsertUser([FromBody] User user)
        {
            var manageUser = new ManageUser.ManageUser();
            var stat = await manageUser.InsertUserM(user);

            return stat.ErrorCode == ErrorList.OK ? Ok("User inserted") : BadRequest(stat.Description);
        }
        

    }
}