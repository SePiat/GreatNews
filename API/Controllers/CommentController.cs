using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GreatNews.Models;
using MediatR;
using MediatR_.Commands;
using MediatR_.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    /*[Authorize]*/
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<UserIdent> _userManager;

        public CommentController(IMediator mediator, UserManager<UserIdent> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        /// <summary>
        /// GetNewsAll
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetCommentsByNewsIdQuery(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        /// <summary>
        /// GetNewsById
        /// </summary>
        /// <param name="text"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string text, Guid id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
                var comment = new Comment()
                {
                    UsersId = null,
                    TextComment = text,
                    DatePub = DateTime.SpecifyKind(
                    DateTime.UtcNow,
                    DateTimeKind.Utc),
                    News = await _mediator.Send(new GetNewsByIdQuery(id))
                };
                await _mediator.Send(new AddCommentCommand(comment));
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        /// <summary>
        /// DeleteNewsById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCommentByIdCommand(id));
            if (result == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(id);
        }

    }
}

