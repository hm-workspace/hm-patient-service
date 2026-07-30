using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.InternalModels.DTOs;
using PatientService.Services;
using PatientService.Utils.Common;

namespace PatientService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patients/{patientId:int}/emergency-contacts")]
    public class PatientEmergencyContactsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientEmergencyContactsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmergencyContactDto>>>> GetEmergencyContacts(int patientId)
        {
            return Ok(await _patientService.GetEmergencyContactsAsync(patientId));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<EmergencyContactDto>>> GetEmergencyContact(int patientId, int id)
        {
            var result = await _patientService.GetEmergencyContactAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmergencyContactDto>>> AddEmergencyContact(int patientId, [FromBody] EmergencyContactDto dto)
        {
            var result = await _patientService.AddEmergencyContactAsync(patientId, dto);
            return CreatedAtAction(nameof(GetEmergencyContact), new { patientId, id = 0 }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<EmergencyContactDto>>> UpdateEmergencyContact(int patientId, int id, [FromBody] EmergencyContactDto dto)
        {
            var result = await _patientService.UpdateEmergencyContactAsync(patientId, id, dto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteEmergencyContact(int patientId, int id)
        {
            var result = await _patientService.DeleteEmergencyContactAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
