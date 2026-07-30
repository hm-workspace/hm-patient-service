using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.InternalModels.DTOs;
using PatientService.Services;
using PatientService.Utils.Common;

namespace PatientService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patients/{patientId:int}/vitals")]
    public class PatientVitalsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientVitalsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<VitalDto>>>> GetVitals(int patientId)
        {
            return Ok(await _patientService.GetVitalsAsync(patientId));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<VitalDto>>> GetVital(int patientId, int id)
        {
            var result = await _patientService.GetVitalAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<VitalDto>>> AddVital(int patientId, [FromBody] VitalDto dto)
        {
            var result = await _patientService.AddVitalAsync(patientId, dto);
            return CreatedAtAction(nameof(GetVital), new { patientId, id = 0 }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<VitalDto>>> UpdateVital(int patientId, int id, [FromBody] VitalDto dto)
        {
            var result = await _patientService.UpdateVitalAsync(patientId, id, dto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteVital(int patientId, int id)
        {
            var result = await _patientService.DeleteVitalAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
