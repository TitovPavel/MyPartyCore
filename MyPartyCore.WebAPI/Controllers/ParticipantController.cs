using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DB.BL;
using MyPartyCore.DB.Models;
using MyPartyCore.WebAPI.Mediatr.Queries;

namespace MyPartyCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ParticipantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Participant
        [HttpGet]
        public async Task<IEnumerable<Participant>> Get()
        {
            IEnumerable<Participant> participants = await _mediator.Send(new ParticipantsQuery() {OnlyAttendent = false });
            return participants;
        }

        // GET: api/Participant/Attendent
        [HttpGet("Attendent")]
        public async Task<IEnumerable<Participant>> GetAttendent()
        {
            IEnumerable<Participant> participants = await _mediator.Send(new ParticipantsQuery() { OnlyAttendent = true });
            return participants;
        }

        // POST: api/Participant
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ParticipantVoteQuery request)
        {
            Participant participant = await _mediator.Send(request);
            
            return CreatedAtAction(nameof(Get), new { id = participant.Id }, participant);
        }
    }
}
