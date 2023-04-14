using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelativeStrengthController: BaseController
    {
        public RelativeStrengthController(ISender sender) : base(sender) { }


        [HttpGet]
        public async Task<IActionResult> GetRelativeStrengthController()
        {
            var result = await Sender.Send(new GetRelativeStrengthQuery());

            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }
    }
}
