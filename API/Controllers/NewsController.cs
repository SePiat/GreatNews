using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;
using MediatR;
using MediatR_.Commands;
using MediatR_.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// GetNewsById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
           var result = await _mediator.Send(new GetNewsByIdQuery(id));
            if(result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        /// <summary>
        /// GetNewsAll
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new GetNewsAllQuery());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        /// <summary>
        /// DeleteNewsById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result =await _mediator.Send(new DeleteNewsByIdCommand(id));
            if (result == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(id);
        }

    }
}