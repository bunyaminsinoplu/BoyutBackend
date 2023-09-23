using Business.AuthenticationBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace BoyutBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusiness _authenticationBusiness;

        public AuthenticationController(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }

        [Route("login")]
        [HttpPost]
        public async Task<ObjectResult> Login([FromBody] Credential credentials)
        {
            try
            {
                RestFromAuthentication rest = await _authenticationBusiness.Login(credentials);
                return StatusCode(rest.StatusCode, new { rest.Message, rest.JWTToken });
            }
            catch (ApplicationException ae)
            {
                return StatusCode(400, new { ae.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}