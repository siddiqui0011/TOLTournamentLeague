using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TOLTournamentLeague.DOM;
using TOLTournamentLeague.TOLRegistrationRepository;

namespace TOLTournamentLeague.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet("Candidates")]
        public async Task<ActionResult<IEnumerable<TOLRegistration>>> GetRegistrations()
        {
            var registrations = await _registrationService.GetRegistrationsAsync();
            return Ok(registrations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TOLRegistration>> GetRegistration(int id)
        {
            var registration = await _registrationService.GetRegistrationByIdAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return Ok(registration);
        }

        [HttpPost("Create")]
        //[Route("Create")]
        public async Task<ActionResult> AddRegistration([FromBody] TOLRegistration registration)
        {
            await _registrationService.AddRegistrationAsync(registration);
            return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
        }

       // [HttpPut("{id}")]
        [HttpPut("Update")]
        //[Route("Update")]
        //public async Task<ActionResult> UpdateRegistration(int id, [FromBody] TOLRegistration registration)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        { 
        //            if (id != registration.Id)
        //            {
        //                return BadRequest("Id in path does not match Id in body.");
        //            }
        //        }

        //        await _registrationService.UpdateRegistrationAsync(registration);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        public async Task<ActionResult> UpdateRegistration(int id, [FromBody] TOLRegistration updatedRegistration)
        {
            try
            {
                if (!ModelState.IsValid) { 
                if (id != updatedRegistration.Id)
                {
                    return BadRequest("ID in path does not match ID in body.");
                }
                }

                // Retrieve existing registration data
                var existingRegistration = await _registrationService.GetRegistrationByIdAsync(id);
                if (existingRegistration == null)
                {
                    return NotFound($"Registration with ID {id} not found.");
                }

                // Update only the properties that are allowed to be updated
                existingRegistration.FullName = updatedRegistration.FullName;
                existingRegistration.EmailId = updatedRegistration.EmailId;
                existingRegistration.Mobile = updatedRegistration.Mobile;
                existingRegistration.LinkedInUrl = updatedRegistration.LinkedInUrl;

                // Perform additional validation if needed

                // Call service method to update registration
                await _registrationService.UpdateRegistrationAsync(existingRegistration);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpDelete("Delete/{id}")]
        //[Route("Delete")]
        public async Task<ActionResult> DeleteRegistration(int id)
        {
            await _registrationService.DeleteRegistrationAsync(id);
            return NoContent();
        }
    }
}
