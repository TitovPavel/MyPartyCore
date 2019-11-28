using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DB.BL;
using MyPartyCore.DB.Models;



namespace MyPartyCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PartyController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IPartyService _partyService;

        public PartyController(UserManager<User> userManager, IPartyService partyService)
        {
            _partyService = partyService;
            _userManager = userManager;
        }

        // GET: api/Party
        [HttpGet]
        public IEnumerable<Party> Get()
        {
            return _partyService.ListOfCurrentParties();
        }

        // GET: api/Party/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            Party party = _partyService.GetPartyByID(id);
            
            if (party == null)
            {
                return NotFound();
            }

            return Ok(party);
        }

        // POST: api/Party
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Party party)
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            party.OwnerId = user.Id;
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

            return CreatedAtAction(nameof(Get), new { id = party.Id }, party);
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

            return Ok(id);
        }
    }
}
