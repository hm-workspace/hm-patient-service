using PatientService.InternalModels.DTOs;
using PatientService.InternalModels.Entities;
using PatientService.Repository;
using PatientService.Utils.Common;

namespace PatientService.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<ApiResponse<PagedResult<PatientDto>>> GetPatientsAsync(SearchQuery searchQuery)
    {
        var page = await _patientRepository.GetPatientsAsync(searchQuery);
        var dto = new PagedResult<PatientDto>(page.Items.Select(PatientDto.FromEntity).ToList(), page.TotalCount, page.PageNumber, page.PageSize);
        return ApiResponse<PagedResult<PatientDto>>.Ok(dto);
    }

    public async Task<ApiResponse<PatientDto>> GetPatientByIdAsync(int id)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);
        return patient is null ? ApiResponse<PatientDto>.Fail("Patient not found") : ApiResponse<PatientDto>.Ok(PatientDto.FromEntity(patient));
    }

    public async Task<ApiResponse<PatientDto>> GetPatientByPatientIdAsync(string patientId)
    {
        var patient = await _patientRepository.GetPatientByPatientIdAsync(patientId);
        return patient is null ? ApiResponse<PatientDto>.Fail("Patient not found") : ApiResponse<PatientDto>.Ok(PatientDto.FromEntity(patient));
    }

    public Task<ApiResponse<PagedResult<PatientDto>>> SearchPatientsAsync(string searchTerm, int pageNumber, int pageSize)
    {
        return GetPatientsAsync(new SearchQuery { SearchTerm = searchTerm, PageNumber = pageNumber, PageSize = pageSize });
    }

    public async Task<ApiResponse<string>> GeneratePatientIdAsync()
    {
        return ApiResponse<string>.Ok(await _patientRepository.GeneratePatientIdAsync());
    }

    public async Task<ApiResponse<PatientDto>> CreatePatientAsync(CreatePatientDto createPatientDto)
    {
        if (string.IsNullOrWhiteSpace(createPatientDto.PatientId))
        {
            createPatientDto.PatientId = await _patientRepository.GeneratePatientIdAsync();
        }

        var entity = new PatientEntity
        {
            PatientId = createPatientDto.PatientId,
            FirstName = createPatientDto.FirstName,
            LastName = createPatientDto.LastName,
            DateOfBirth = createPatientDto.DateOfBirth,
            Gender = createPatientDto.Gender,
            Email = createPatientDto.Email,
            Phone = createPatientDto.Phone,
            Address = createPatientDto.Address,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _patientRepository.CreatePatientAsync(entity);
        return ApiResponse<PatientDto>.Ok(PatientDto.FromEntity(created), "Patient created successfully");
    }

    public async Task<ApiResponse<PatientDto>> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto)
    {
        var entity = new PatientEntity
        {
            PatientId = updatePatientDto.PatientId,
            FirstName = updatePatientDto.FirstName,
            LastName = updatePatientDto.LastName,
            DateOfBirth = updatePatientDto.DateOfBirth,
            Gender = updatePatientDto.Gender,
            Email = updatePatientDto.Email,
            Phone = updatePatientDto.Phone,
            Address = updatePatientDto.Address,
            UpdatedAt = DateTime.UtcNow
        };

        var updated = await _patientRepository.UpdatePatientAsync(id, entity);
        return updated is null ? ApiResponse<PatientDto>.Fail("Patient not found") : ApiResponse<PatientDto>.Ok(PatientDto.FromEntity(updated), "Patient updated successfully");
    }

    public async Task<ApiResponse<string>> DeletePatientAsync(int id)
    {
        var deleted = await _patientRepository.DeletePatientAsync(id);
        return deleted ? ApiResponse<string>.Ok("Patient deleted successfully") : ApiResponse<string>.Fail("Patient not found");
    }

    public async Task<ApiResponse<IEnumerable<AllergyDto>>> GetAllergiesAsync(int patientId)
    {
        var data = await _patientRepository.GetAllergiesAsync(patientId);
        return ApiResponse<IEnumerable<AllergyDto>>.Ok(data.Select(AllergyDto.FromEntity).ToList());
    }

    public async Task<ApiResponse<AllergyDto>> GetAllergyAsync(int patientId, int id)
    {
        var data = await _patientRepository.GetAllergyAsync(patientId, id);
        return data is null ? ApiResponse<AllergyDto>.Fail("Allergy not found") : ApiResponse<AllergyDto>.Ok(AllergyDto.FromEntity(data));
    }

    public async Task<ApiResponse<AllergyDto>> AddAllergyAsync(int patientId, AllergyDto dto)
    {
        var entity = new AllergyEntity { PatientId = patientId, Allergy = dto.Allergy, Severity = dto.Severity, Notes = dto.Notes };
        var data = await _patientRepository.AddAllergyAsync(entity);
        return ApiResponse<AllergyDto>.Ok(AllergyDto.FromEntity(data), "Allergy added");
    }

    public async Task<ApiResponse<AllergyDto>> UpdateAllergyAsync(int patientId, int id, AllergyDto dto)
    {
        var entity = new AllergyEntity { PatientId = patientId, Allergy = dto.Allergy, Severity = dto.Severity, Notes = dto.Notes };
        var data = await _patientRepository.UpdateAllergyAsync(patientId, id, entity);
        return data is null ? ApiResponse<AllergyDto>.Fail("Allergy not found") : ApiResponse<AllergyDto>.Ok(AllergyDto.FromEntity(data), "Allergy updated");
    }

    public async Task<ApiResponse<string>> DeleteAllergyAsync(int patientId, int id)
    {
        var deleted = await _patientRepository.DeleteAllergyAsync(patientId, id);
        return deleted ? ApiResponse<string>.Ok("Allergy deleted") : ApiResponse<string>.Fail("Allergy not found");
    }

    public async Task<ApiResponse<IEnumerable<MedicationDto>>> GetMedicationsAsync(int patientId)
    {
        var data = await _patientRepository.GetMedicationsAsync(patientId);
        return ApiResponse<IEnumerable<MedicationDto>>.Ok(data.Select(MedicationDto.FromEntity).ToList());
    }

    public async Task<ApiResponse<IEnumerable<MedicationDto>>> GetActiveMedicationsAsync(int patientId)
    {
        var data = await _patientRepository.GetActiveMedicationsAsync(patientId);
        return ApiResponse<IEnumerable<MedicationDto>>.Ok(data.Select(MedicationDto.FromEntity).ToList());
    }

    public async Task<ApiResponse<MedicationDto>> GetMedicationAsync(int patientId, int id)
    {
        var data = await _patientRepository.GetMedicationAsync(patientId, id);
        return data is null ? ApiResponse<MedicationDto>.Fail("Medication not found") : ApiResponse<MedicationDto>.Ok(MedicationDto.FromEntity(data));
    }

    public async Task<ApiResponse<MedicationDto>> AddMedicationAsync(int patientId, MedicationDto dto)
    {
        var entity = new MedicationEntity
        {
            PatientId = patientId,
            MedicationName = dto.MedicationName,
            Dosage = dto.Dosage,
            Frequency = dto.Frequency,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Notes = dto.Notes
        };

        var data = await _patientRepository.AddMedicationAsync(entity);
        return ApiResponse<MedicationDto>.Ok(MedicationDto.FromEntity(data), "Medication added");
    }

    public async Task<ApiResponse<MedicationDto>> UpdateMedicationAsync(int patientId, int id, MedicationDto dto)
    {
        var entity = new MedicationEntity
        {
            PatientId = patientId,
            MedicationName = dto.MedicationName,
            Dosage = dto.Dosage,
            Frequency = dto.Frequency,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Notes = dto.Notes
        };

        var data = await _patientRepository.UpdateMedicationAsync(patientId, id, entity);
        return data is null ? ApiResponse<MedicationDto>.Fail("Medication not found") : ApiResponse<MedicationDto>.Ok(MedicationDto.FromEntity(data), "Medication updated");
    }

    public async Task<ApiResponse<MedicationDto>> DiscontinueMedicationAsync(int patientId, int id, DateTime endDate)
    {
        var data = await _patientRepository.DiscontinueMedicationAsync(patientId, id, endDate);
        return data is null ? ApiResponse<MedicationDto>.Fail("Medication not found") : ApiResponse<MedicationDto>.Ok(MedicationDto.FromEntity(data), "Medication discontinued");
    }

    public async Task<ApiResponse<string>> DeleteMedicationAsync(int patientId, int id)
    {
        var deleted = await _patientRepository.DeleteMedicationAsync(patientId, id);
        return deleted ? ApiResponse<string>.Ok("Medication deleted") : ApiResponse<string>.Fail("Medication not found");
    }
}
