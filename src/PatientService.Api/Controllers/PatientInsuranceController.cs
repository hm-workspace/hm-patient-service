using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.InternalModels.DTOs;
using PatientService.Services;
using PatientService.Utils.Common;

namespace PatientService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patients/{patientId:int}/insurance")]
    public class PatientInsuranceController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientInsuranceController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<InsuranceDto>>>> GetInsurance(int patientId)
        {
            return Ok(await _patientService.GetInsuranceAsync(patientId));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<InsuranceDto>>> GetInsuranceItem(int patientId, int id)
        {
            var result = await _patientService.GetInsuranceAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<InsuranceDto>>> AddInsurance(int patientId, [FromBody] InsuranceDto dto)
        {
            var result = await _patientService.AddInsuranceAsync(patientId, dto);
            return CreatedAtAction(nameof(GetInsuranceItem), new { patientId, id = 0 }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<InsuranceDto>>> UpdateInsurance(int patientId, int id, [FromBody] InsuranceDto dto)
        {
            var result = await _patientService.UpdateInsuranceAsync(patientId, id, dto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteInsurance(int patientId, int id)
        {
            var result = await _patientService.DeleteInsuranceAsync(patientId, id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
