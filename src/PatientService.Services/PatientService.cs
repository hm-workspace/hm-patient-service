using PatientService.InternalModels.DTOs;
using PatientService.Utils.Common;

namespace PatientService.Services;

public class PatientService : IPatientService
{
    private readonly IPatientProfileService _patientProfileService;
    private readonly IClinicalDetailsService _clinicalDetailsService;

    public PatientService(IPatientProfileService patientProfileService, IClinicalDetailsService clinicalDetailsService)
    {
        _patientProfileService = patientProfileService;
        _clinicalDetailsService = clinicalDetailsService;
    }

    public Task<ApiResponse<PagedResult<PatientDto>>> GetPatientsAsync(SearchQuery searchQuery)
        => _patientProfileService.GetPatientsAsync(searchQuery);

    public Task<ApiResponse<PatientDto>> GetPatientByIdAsync(int id)
        => _patientProfileService.GetPatientByIdAsync(id);

    public Task<ApiResponse<PatientDetailsDto>> GetPatientByPatientIdAsync(string patientId)
        => _patientProfileService.GetPatientByPatientIdAsync(patientId);

    public Task<ApiResponse<PagedResult<PatientDto>>> SearchPatientsAsync(string searchTerm, int pageNumber, int pageSize)
        => _patientProfileService.SearchPatientsAsync(searchTerm, pageNumber, pageSize);

    public Task<ApiResponse<string>> GeneratePatientIdAsync()
        => _patientProfileService.GeneratePatientIdAsync();

    public Task<ApiResponse<PatientDto>> CreatePatientAsync(CreatePatientDto createPatientDto)
        => _patientProfileService.CreatePatientAsync(createPatientDto);

    public Task<ApiResponse<PatientWithClinicalDetailsDto>> CreatePatientWithClinicalDetailsAsync(CreatePatientWithClinicalDetailsDto createPatientDto)
        => _clinicalDetailsService.CreatePatientWithClinicalDetailsAsync(createPatientDto);

    public Task<ApiResponse<PatientDto>> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto)
        => _patientProfileService.UpdatePatientAsync(id, updatePatientDto);

    public Task<ApiResponse<PatientWithClinicalDetailsDto>> UpdatePatientWithClinicalDetailsAsync(int id, UpdatePatientWithClinicalDetailsDto updatePatientDto)
        => _clinicalDetailsService.UpdatePatientWithClinicalDetailsAsync(id, updatePatientDto);

    public Task<ApiResponse<string>> DeletePatientAsync(int id)
        => _patientProfileService.DeletePatientAsync(id);

    public Task<ApiResponse<IEnumerable<AllergyDto>>> GetAllergiesAsync(int patientId)
        => _clinicalDetailsService.GetAllergiesAsync(patientId);

    public Task<ApiResponse<AllergyDto>> GetAllergyAsync(int patientId, int id)
        => _clinicalDetailsService.GetAllergyAsync(patientId, id);

    public Task<ApiResponse<AllergyDto>> AddAllergyAsync(int patientId, AllergyDto dto)
        => _clinicalDetailsService.AddAllergyAsync(patientId, dto);

    public Task<ApiResponse<AllergyDto>> UpdateAllergyAsync(int patientId, int id, AllergyDto dto)
        => _clinicalDetailsService.UpdateAllergyAsync(patientId, id, dto);

    public Task<ApiResponse<string>> DeleteAllergyAsync(int patientId, int id)
        => _clinicalDetailsService.DeleteAllergyAsync(patientId, id);

    public Task<ApiResponse<IEnumerable<MedicationDto>>> GetMedicationsAsync(int patientId)
        => _clinicalDetailsService.GetMedicationsAsync(patientId);

    public Task<ApiResponse<IEnumerable<MedicationDto>>> GetActiveMedicationsAsync(int patientId)
        => _clinicalDetailsService.GetActiveMedicationsAsync(patientId);

    public Task<ApiResponse<MedicationDto>> GetMedicationAsync(int patientId, int id)
        => _clinicalDetailsService.GetMedicationAsync(patientId, id);

    public Task<ApiResponse<MedicationDto>> AddMedicationAsync(int patientId, MedicationDto dto)
        => _clinicalDetailsService.AddMedicationAsync(patientId, dto);

    public Task<ApiResponse<MedicationDto>> UpdateMedicationAsync(int patientId, int id, MedicationDto dto)
        => _clinicalDetailsService.UpdateMedicationAsync(patientId, id, dto);

    public Task<ApiResponse<MedicationDto>> DiscontinueMedicationAsync(int patientId, int id, DateTime endDate)
        => _clinicalDetailsService.DiscontinueMedicationAsync(patientId, id, endDate);

    public Task<ApiResponse<string>> DeleteMedicationAsync(int patientId, int id)
        => _clinicalDetailsService.DeleteMedicationAsync(patientId, id);

    public Task<ApiResponse<IEnumerable<MedicalHistoryDto>>> GetMedicalHistoryAsync(int patientId)
        => _clinicalDetailsService.GetMedicalHistoryAsync(patientId);

    public Task<ApiResponse<MedicalHistoryDto>> GetMedicalHistoryAsync(int patientId, int id)
        => _clinicalDetailsService.GetMedicalHistoryAsync(patientId, id);

    public Task<ApiResponse<MedicalHistoryDto>> AddMedicalHistoryAsync(int patientId, MedicalHistoryDto dto)
        => _clinicalDetailsService.AddMedicalHistoryAsync(patientId, dto);

    public Task<ApiResponse<MedicalHistoryDto>> UpdateMedicalHistoryAsync(int patientId, int id, MedicalHistoryDto dto)
        => _clinicalDetailsService.UpdateMedicalHistoryAsync(patientId, id, dto);

    public Task<ApiResponse<string>> DeleteMedicalHistoryAsync(int patientId, int id)
        => _clinicalDetailsService.DeleteMedicalHistoryAsync(patientId, id);

    public Task<ApiResponse<IEnumerable<EmergencyContactDto>>> GetEmergencyContactsAsync(int patientId)
        => _clinicalDetailsService.GetEmergencyContactsAsync(patientId);

    public Task<ApiResponse<EmergencyContactDto>> GetEmergencyContactAsync(int patientId, int id)
        => _clinicalDetailsService.GetEmergencyContactAsync(patientId, id);

    public Task<ApiResponse<EmergencyContactDto>> AddEmergencyContactAsync(int patientId, EmergencyContactDto dto)
        => _clinicalDetailsService.AddEmergencyContactAsync(patientId, dto);

    public Task<ApiResponse<EmergencyContactDto>> UpdateEmergencyContactAsync(int patientId, int id, EmergencyContactDto dto)
        => _clinicalDetailsService.UpdateEmergencyContactAsync(patientId, id, dto);

    public Task<ApiResponse<string>> DeleteEmergencyContactAsync(int patientId, int id)
        => _clinicalDetailsService.DeleteEmergencyContactAsync(patientId, id);

    public Task<ApiResponse<IEnumerable<InsuranceDto>>> GetInsuranceAsync(int patientId)
        => _clinicalDetailsService.GetInsuranceAsync(patientId);

    public Task<ApiResponse<InsuranceDto>> GetInsuranceAsync(int patientId, int id)
        => _clinicalDetailsService.GetInsuranceAsync(patientId, id);

    public Task<ApiResponse<InsuranceDto>> AddInsuranceAsync(int patientId, InsuranceDto dto)
        => _clinicalDetailsService.AddInsuranceAsync(patientId, dto);

    public Task<ApiResponse<InsuranceDto>> UpdateInsuranceAsync(int patientId, int id, InsuranceDto dto)
        => _clinicalDetailsService.UpdateInsuranceAsync(patientId, id, dto);

    public Task<ApiResponse<string>> DeleteInsuranceAsync(int patientId, int id)
        => _clinicalDetailsService.DeleteInsuranceAsync(patientId, id);

    public Task<ApiResponse<IEnumerable<VitalDto>>> GetVitalsAsync(int patientId)
        => _clinicalDetailsService.GetVitalsAsync(patientId);

    public Task<ApiResponse<VitalDto>> GetVitalAsync(int patientId, int id)
        => _clinicalDetailsService.GetVitalAsync(patientId, id);

    public Task<ApiResponse<VitalDto>> AddVitalAsync(int patientId, VitalDto dto)
        => _clinicalDetailsService.AddVitalAsync(patientId, dto);

    public Task<ApiResponse<VitalDto>> UpdateVitalAsync(int patientId, int id, VitalDto dto)
        => _clinicalDetailsService.UpdateVitalAsync(patientId, id, dto);

    public Task<ApiResponse<string>> DeleteVitalAsync(int patientId, int id)
        => _clinicalDetailsService.DeleteVitalAsync(patientId, id);
}
