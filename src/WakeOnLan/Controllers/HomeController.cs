using Microsoft.AspNetCore.Mvc;

namespace Raspberry.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get() => "Hello World!";
    }
}
