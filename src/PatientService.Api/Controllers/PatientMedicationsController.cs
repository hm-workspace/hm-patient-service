using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientService.InternalModels.DTOs;
using PatientService.Services;
using PatientService.Utils.Common;

namespace PatientService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patients/{patientId:int}/medications")]
    public class PatientMedicationsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientMedicationsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<MedicationDto>>>> GetMedications(int patientId)
        {
            return Ok(await _patientService.GetMedicationsAsync(patientId));
        }

        [HttpGet("active")]
        public async Task<ActionResult<ApiResponse<IEnumerable<MedicationDto>>>> GetActiveMedications(int patientId)
        {
            return Ok(await _patientService.GetActiveMedicationsAsync(patientId));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<MedicationDto>>> GetMedication(int patientId, int id)
        {
            var result = await _patientService.GetMedicationAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<MedicationDto>>> AddMedication(int patientId, [FromBody] MedicationDto dto)
        {
            var result = await _patientService.AddMedicationAsync(patientId, dto);
            return CreatedAtAction(nameof(GetMedication), new { patientId, id = 0 }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<MedicationDto>>> UpdateMedication(int patientId, int id, [FromBody] MedicationDto dto)
        {
            var result = await _patientService.UpdateMedicationAsync(patientId, id, dto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("{id:int}/discontinue")]
        public async Task<ActionResult<ApiResponse<MedicationDto>>> DiscontinueMedication(int patientId, int id, [FromBody] DiscontinueMedicationRequest request)
        {
            var result = await _patientService.DiscontinueMedicationAsync(patientId, id, request.EndDate ?? DateTime.UtcNow);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteMedication(int patientId, int id)
        {
            var result = await _patientService.DeleteMedicationAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
