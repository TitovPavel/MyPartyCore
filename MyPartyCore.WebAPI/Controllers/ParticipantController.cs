using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DB.BL;
using MyPartyCore.DB.Models;

namespace MyPartyCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {

        private readonly IPartyService _partyService;

        public ParticipantController(IPartyService partyService)
        {
            _partyService = partyService;
        }

        // GET: api/Participant
        [HttpGet]
        public IEnumerable<Participant> Get()
        {
            return _partyService.ListAll();
        }

        // GET: api/Participant/Attendent
        [HttpGet("Attendent")]
        public IEnumerable<Participant> GetAttendent()
        {
            return _partyService.ListAttendent();
        }

        // POST: api/Participant
        [HttpPost]
        public IActionResult Post([FromBody] Participant participant)
        {         
            _partyService.Vote(participant);

            return CreatedAtAction(nameof(Get), new { id = participant.Id }, participant);
        }
    }
}
