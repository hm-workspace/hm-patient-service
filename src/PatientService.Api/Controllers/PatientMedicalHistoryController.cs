using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.InternalModels.DTOs;
using PatientService.Services;
using PatientService.Utils.Common;

namespace PatientService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patients/{patientId:int}/medical-history")]
    public class PatientMedicalHistoryController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientMedicalHistoryController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<MedicalHistoryDto>>>> GetMedicalHistory(int patientId)
        {
            return Ok(await _patientService.GetMedicalHistoryAsync(patientId));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<MedicalHistoryDto>>> GetMedicalHistoryItem(int patientId, int id)
        {
            var result = await _patientService.GetMedicalHistoryAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<MedicalHistoryDto>>> AddMedicalHistory(int patientId, [FromBody] MedicalHistoryDto dto)
        {
            var result = await _patientService.AddMedicalHistoryAsync(patientId, dto);
            return CreatedAtAction(nameof(GetMedicalHistoryItem), new { patientId, id = 0 }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<MedicalHistoryDto>>> UpdateMedicalHistory(int patientId, int id, [FromBody] MedicalHistoryDto dto)
        {
            var result = await _patientService.UpdateMedicalHistoryAsync(patientId, id, dto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteMedicalHistory(int patientId, int id)
        {
            var result = await _patientService.DeleteMedicalHistoryAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}