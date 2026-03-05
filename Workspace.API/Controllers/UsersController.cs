using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workspace.API.Base;

namespace Workspace.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }
    }
}