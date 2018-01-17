using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace dotnet_jwt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProtectedController : Controller
    {
        // GET api/protected
        [HttpGet]
        public IActionResult Get()
        {
            var Username = this.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
            return Ok($"Hello: {Username}");
        }
    }
}
