using Business.AdminBusiness;
using Business.AuthenticationBusiness;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace BoyutBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _adminBusiness;

        public AdminController(IAdminBusiness adminBusiness)
        {
            _adminBusiness = adminBusiness;
        }

        [HttpGet("GetUsers")]
        public async Task<ObjectResult> GetUsers()
        {
            try
            {
                var contextUser = HttpContext.User;
                List<UserDTO> users = await _adminBusiness.GetUsers();
                return StatusCode(200, new { users });

            }
            catch (ApplicationException ae)
            {
                return StatusCode(400, new { ae.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetUser/{userID}")]
        public async Task<ObjectResult> GetUser(Guid userID)
        {
            try
            {
                var contextUser = HttpContext.User;
                Users user = await _adminBusiness.GetUser(userID);
                return StatusCode(200, new { user });
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

        [HttpDelete("DeleteUser/{userID}")]
        public async Task<ObjectResult> DeleteUser(Guid userID)
        {
            try
            {
                var contextUser = HttpContext.User;
                Guid LoggedInUser = Guid.Parse(contextUser.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                RestFromBusiness rest = await _adminBusiness.DeleteUser(userID, LoggedInUser);
                return StatusCode(rest.StatusCode, new { rest.Message });
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

        [HttpPost("CreateUser")]
        public async Task<ObjectResult> CreateUser(Users createUserRequest)
        {
            try
            {
                var contextUser = HttpContext.User;
                Guid LoggedInUser = Guid.Parse(contextUser.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                RestFromBusiness rest = await _adminBusiness.CreateUser(createUserRequest, LoggedInUser);
                return StatusCode(rest.StatusCode, new { rest.Message });
            }
            catch (ApplicationException ae)
            {
                return StatusCode(400, new { ae.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch("UpdateUser")]
        public async Task<ObjectResult> UpdateUser(Users createUserRequest)
        {
            try
            {
                var contextUser = HttpContext.User;
                Guid LoggedInUser = Guid.Parse(contextUser.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                RestFromBusiness rest = await _adminBusiness.CreateUser(createUserRequest, LoggedInUser);
                return StatusCode(rest.StatusCode, new { rest.Message });
            }
            catch (ApplicationException ae)
            {
                return StatusCode(400, new { ae.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
