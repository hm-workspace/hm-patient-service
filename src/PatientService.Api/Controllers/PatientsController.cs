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
    public async Task<ActionResult<ApiResponse<PatientDto>>> GetPatientByPatientId(string patientId)
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


