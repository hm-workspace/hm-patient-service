using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.Utils.Common;
using PatientService.InternalModels.DTOs;
using PatientService.Services;

namespace PatientService.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<PatientDto>>>> GetPatients([FromQuery] SearchQuery searchQuery)
    {
        return Ok(await _patientService.GetPatientsAsync(searchQuery));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<PatientDto>>> GetPatient(int id)
    {
        var result = await _patientService.GetPatientByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("by-patient-id/{patientId}")]
    public async Task<ActionResult<ApiResponse<PatientDetailsDto>>> GetPatientByPatientId(string patientId)
    {
        var result = await _patientService.GetPatientByPatientIdAsync(patientId);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<PatientDto>>>> SearchPatients([FromQuery] string searchTerm, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        return Ok(await _patientService.SearchPatientsAsync(searchTerm, pageNumber, pageSize));
    }

    [HttpGet("generate-id")]
    public async Task<ActionResult<ApiResponse<string>>> GeneratePatientId()
    {
        return Ok(await _patientService.GeneratePatientIdAsync());
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PatientDto>>> CreatePatient([FromBody] CreatePatientDto createPatientDto)
    {
        var result = await _patientService.CreatePatientAsync(createPatientDto);
        return CreatedAtAction(nameof(GetPatient), new { id = result.Data?.Id ?? 0 }, result);
    }

    [HttpPost("with-clinical-details")]
    public async Task<ActionResult<ApiResponse<PatientWithClinicalDetailsDto>>> CreatePatientWithClinicalDetails([FromBody] CreatePatientWithClinicalDetailsDto createPatientDto)
    {
        var result = await _patientService.CreatePatientWithClinicalDetailsAsync(createPatientDto);
        return CreatedAtAction(nameof(GetPatient), new { id = result.Data?.Patient.Id ?? 0 }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<PatientDto>>> UpdatePatient(int id, [FromBody] UpdatePatientDto updatePatientDto)
    {
        var result = await _patientService.UpdatePatientAsync(id, updatePatientDto);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<string>>> DeletePatient(int id)
    {
        var result = await _patientService.DeletePatientAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
