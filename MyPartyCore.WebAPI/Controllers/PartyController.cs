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
    public class PartyController : ControllerBase
    {

        private readonly IPartyService _partyService;

        public PartyController(IPartyService partyService)
        {
            _partyService = partyService;
        }

        // GET: api/Party
        [HttpGet]
        public IEnumerable<Party> Get()
        {
            return _partyService.ListOfCurrentParties();
        }

        // GET: api/Party/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Party> Get(int id)
        {
            Party party = _partyService.GetPartyByID(id);
            
            if (party == null)
            {
                return NotFound();
            }

            return party;
        }

        // POST: api/Party
        [HttpPost]
        public IActionResult Post([FromBody] Party party)
        {         
            _partyService.AddParty(party);

            return CreatedAtAction(nameof(Get), new { id = party.Id }, party);
        }

        // PUT: api/Party/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Party party)
        {
            if (id != party.Id)
            {
                return BadRequest();
            }

            if (_partyService.GetPartyByID(id) == null)
            {
                return NotFound();
            }

            _partyService.UpdateParty(party);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Party party = _partyService.GetPartyByID(id);

            if (party == null)
            {
                return NotFound();
            }

            _partyService.DeleteParty(party);

            return NoContent();
        }
    }
}
