using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp_MedaitR.Application.ToDoItems.Commands;
using ToDoApp_MedaitR.Application.ToDoItems.Queries;
using ToDoApp_MedaitR.Domain.Entities;

namespace ToDoApp_MedaitR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ToDoItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllToDoItemsQuery());
            return Ok(response);
        }

        [HttpGet("date")]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        {
            var response = await _mediator.Send(new GetToDoItemsByDateQuery(date));
            return Ok(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var response = await _mediator.Send(new GetToDoItemsByUserQuery(userId));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddToDoItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteToDoItemCommand(id));

            if (!result)
                return NotFound();

            return NoContent(); // 204
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoItem>> Update(Guid id, [FromBody] UpdateToDoItemCommand command)
        {

            command.Id = id;
            var updatedItem = await _mediator.Send(command);

            if (updatedItem == null)
                return NotFound();

            return Ok(updatedItem);
        }
    }
}