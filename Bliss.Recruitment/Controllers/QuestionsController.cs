using Bliss.Recruitment.Api.Models.Response;
using Bliss.Recruitment.Application;
using Bliss.Recruitment.Application.Orders.GetQuestions;
using Bliss.Recruitment.Application.Questions;
using Bliss.Recruitment.Application.Questions.CreateQuestion;
using Bliss.Recruitment.Application.Questions.UpdateQuestion;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<GetQuestionsWithParamsResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetQuestionsWithParamsRequestModel requestModel)
        {
            var response = await _mediator.Send(new GetQuestionsQuery(requestModel.Offset, requestModel.Limit, requestModel.Filter));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetQuestionByIdResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetQuestionQuery(id));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateQuestionRequestModel requestModel)
        {
            var responseCreation = await _mediator.Send(new CreateQuestionCommand(requestModel.Question, requestModel.ImageUrl, requestModel.ThumbUrl, requestModel.Choices));

            var response = await _mediator.Send(new GetQuestionQuery(responseCreation.Id));

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }


        [HttpPut("{id:Guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] CreateQuestionRequestModel requestModel)
        {
            var responseUpdate = await _mediator.Send(new UpdateQuestionCommand(id, requestModel.Question, requestModel.ImageUrl, requestModel.ThumbUrl, requestModel.Choices));

            var response = await _mediator.Send(new GetQuestionQuery(responseUpdate.Id));

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
    }
}
