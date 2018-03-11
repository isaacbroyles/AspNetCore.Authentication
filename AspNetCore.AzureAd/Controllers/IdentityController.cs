using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AspNetCore.AzureAd.Controllers
{
    [Route("[controller]/[action]")]
    public class IdentityController : Controller
    {
        [HttpGet]
        public IActionResult Me()
        {
            return Ok(new
            {
                User.Identity.Name,
                User.Identity.IsAuthenticated,
                User.Identity.AuthenticationType,
                Claims = User.Claims.Select(claim => new { claim.Type, claim.Value })
            });
        }
    }
}
