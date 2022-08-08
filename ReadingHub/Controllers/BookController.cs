using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;

namespace ReadingHub.Controllers
{
    
    public class BookController : ApiController
    {
        [HttpPost]
        [Route("PublishBook")]
       public IActionResult PublishBook([FromForm]BookViewModel model) {
              
            
            return Ok();
        }
    }
}
