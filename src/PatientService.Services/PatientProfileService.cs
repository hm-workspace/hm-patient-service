using PatientService.InternalModels.DTOs;
using PatientService.InternalModels.Entities;
using PatientService.Repository;
using PatientService.Utils.Common;

namespace PatientService.Services;

public class PatientProfileService : IPatientProfileService
{
    private readonly IPatientRepository _patientRepository;

    public PatientProfileService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<ApiResponse<PagedResult<PatientDto>>> GetPatientsAsync(SearchQuery searchQuery)
    {
        var page = await _patientRepository.GetPatientsAsync(searchQuery);
        var dto = new PagedResult<PatientDto>(page.Items.Select(PatientMapper.ToDto).ToList(), page.TotalCount, page.PageNumber, page.PageSize);
        return ApiResponse<PagedResult<PatientDto>>.Ok(dto);
    }

    public async Task<ApiResponse<PatientDto>> GetPatientByIdAsync(int id)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);
        return patient is null ? ApiResponse<PatientDto>.Fail("Patient not found") : ApiResponse<PatientDto>.Ok(PatientMapper.ToDto(patient));
    }

    public async Task<ApiResponse<PatientDetailsDto>> GetPatientByPatientIdAsync(string patientId)
    {
        var patient = await _patientRepository.GetPatientByPatientIdAsync(patientId);
        if (patient is null)
        {
            return ApiResponse<PatientDetailsDto>.Fail("Patient not found");
        }

        var details = await LoadClinicalDetailsAsync(patient.Id);
        return ApiResponse<PatientDetailsDto>.Ok(new PatientDetailsDto
        {
            Patient = PatientMapper.ToDto(patient),
            Allergies = details.Allergies,
            Medications = details.Medications,
            MedicalHistory = details.MedicalHistory,
            EmergencyContacts = details.EmergencyContacts,
            Insurance = details.Insurance,
            Vitals = details.Vitals
        });
    }

    public Task<ApiResponse<PagedResult<PatientDto>>> SearchPatientsAsync(string searchTerm, int pageNumber, int pageSize)
        => GetPatientsAsync(new SearchQuery { SearchTerm = searchTerm, PageNumber = pageNumber, PageSize = pageSize });

    public async Task<ApiResponse<string>> GeneratePatientIdAsync()
        => ApiResponse<string>.Ok(await _patientRepository.GeneratePatientIdAsync());

    public async Task<ApiResponse<PatientDto>> CreatePatientAsync(CreatePatientDto createPatientDto)
    {
        if (string.IsNullOrWhiteSpace(createPatientDto.PatientId))
        {
            createPatientDto.PatientId = await _patientRepository.GeneratePatientIdAsync();
        }

        var entity = PatientMapper.ToEntity(createPatientDto);
        var created = await _patientRepository.CreatePatientAsync(entity);
        return ApiResponse<PatientDto>.Ok(PatientMapper.ToDto(created), "Patient created successfully");
    }

    public async Task<ApiResponse<PatientDto>> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto)
    {
        var entity = PatientMapper.ToEntity(updatePatientDto);
        var updated = await _patientRepository.UpdatePatientAsync(id, entity);
        return updated is null ? ApiResponse<PatientDto>.Fail("Patient not found") : ApiResponse<PatientDto>.Ok(PatientMapper.ToDto(updated), "Patient updated successfully");
    }

    public async Task<ApiResponse<string>> DeletePatientAsync(int id)
    {
        var deleted = await _patientRepository.DeletePatientAsync(id);
        return deleted ? ApiResponse<string>.Ok("Patient deleted successfully") : ApiResponse<string>.Fail("Patient not found");
    }

    private async Task<ClinicalDetailsBundle> LoadClinicalDetailsAsync(int patientId)
    {
        var allergies = await _patientRepository.GetAllergiesAsync(patientId);
        var medications = await _patientRepository.GetMedicationsAsync(patientId);
        var medicalHistory = await _patientRepository.GetMedicalHistoryAsync(patientId);
        var emergencyContacts = await _patientRepository.GetEmergencyContactsAsync(patientId);
        var insurance = await _patientRepository.GetInsuranceAsync(patientId);
        var vitals = await _patientRepository.GetVitalsAsync(patientId);

        return new ClinicalDetailsBundle(
            allergies.Select(PatientMapper.ToDto).ToList(),
            medications.Select(PatientMapper.ToDto).ToList(),
            medicalHistory.Select(PatientMapper.ToDto).ToList(),
            emergencyContacts.Select(PatientMapper.ToDto).ToList(),
            insurance.Select(PatientMapper.ToDto).ToList(),
            vitals.Select(PatientMapper.ToDto).ToList());
    }

    private sealed record ClinicalDetailsBundle(
        IReadOnlyCollection<AllergyDto> Allergies,
        IReadOnlyCollection<MedicationDto> Medications,
        IReadOnlyCollection<MedicalHistoryDto> MedicalHistory,
        IReadOnlyCollection<EmergencyContactDto> EmergencyContacts,
        IReadOnlyCollection<InsuranceDto> Insurance,
        IReadOnlyCollection<VitalDto> Vitals);
}
