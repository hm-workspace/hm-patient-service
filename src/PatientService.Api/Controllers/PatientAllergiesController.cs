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
    [Route("api/patients/{patientId:int}/allergies")]
    public class PatientAllergiesController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientAllergiesController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AllergyDto>>>> GetAllergies(int patientId)
        {
            return Ok(await _patientService.GetAllergiesAsync(patientId));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<AllergyDto>>> GetAllergy(int patientId, int id)
        {
            var result = await _patientService.GetAllergyAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AllergyDto>>> AddAllergy(int patientId, [FromBody] AllergyDto dto)
        {
            var result = await _patientService.AddAllergyAsync(patientId, dto);
            return CreatedAtAction(nameof(GetAllergy), new { patientId, id = 0 }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<AllergyDto>>> UpdateAllergy(int patientId, int id, [FromBody] AllergyDto dto)
        {
            var result = await _patientService.UpdateAllergyAsync(patientId, id, dto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteAllergy(int patientId, int id)
        {
            var result = await _patientService.DeleteAllergyAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
