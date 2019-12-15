using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE_;
using GreatNews.Models;
using GreatNews.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosIndexController : ControllerBase
    {
      private readonly IPositivityIndexService _positivityIndexService;
        

        public PosIndexController(IPositivityIndexService positivityIndexService)
        {
         _positivityIndexService = positivityIndexService;
        }
        /// <summary>
        /// AddIndex
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AddIndex()
        {
            await _positivityIndexService.AddPsitiveIndexToNews();
            return Ok();
        }
        
    }
}