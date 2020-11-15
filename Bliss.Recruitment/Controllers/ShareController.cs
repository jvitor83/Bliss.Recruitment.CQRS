using Bliss.Recruitment.Api.Models;
using Bliss.Recruitment.Application.Share;
using Bliss.Recruitment.Application.Share.ShareUrlContent;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShareController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ShareDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ShareDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromQuery] string destination_email, [FromQuery] string content_url)
        {
            try
            {
                var response = await _mediator.Send(new ShareUrlContentCommand(destination_email, content_url));
                return Ok(response);
            }
            catch(ArgumentNullException exception)
            {
                return BadRequest(new ShareDto($"Bad Request. Either {nameof(destination_email)} not valid or empty {nameof(content_url)}"));
            }
        }
    }
}