using Microsoft.AspNetCore.Mvc;
using NZwalks.APi.Models.DTO;
using NZwalks.APi.Repository;

namespace NZwalks.APi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Authcontroller : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly Itokenhandler itokenhandler;

        public Authcontroller(IUserRepository userRepository, Itokenhandler itokenhandler)
        {
            this.userRepository = userRepository;
            this.itokenhandler = itokenhandler;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult>  LoginAsync(Loginreq loginreq)
        {
            //validate the incoming req -- done through fluent validators

            //check user if authenticated or not
            //check username and pwd
            var user = await userRepository.AuthenticateUserAsync(loginreq.Username, loginreq.Password);

                if (user != null )
            {
                //generaate JWT token
                var token = await itokenhandler.CreateTokenAsync(user);
                return Ok(token);

            };
            return BadRequest("Username or Pwd incorrect");
        }
    }
}
