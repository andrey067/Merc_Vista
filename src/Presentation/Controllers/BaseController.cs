using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ISender Sender;

        public BaseController(ISender sender)
        {
            Sender = sender;
        }
    }
}
