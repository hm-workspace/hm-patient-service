using PatientService.InternalModels.DTOs;
using PatientService.Utils.Common;

namespace PatientService.Services;

public interface IPatientService
{
    Task<ApiResponse<PagedResult<PatientDto>>> GetPatientsAsync(SearchQuery searchQuery);
    Task<ApiResponse<PatientDto>> GetPatientByIdAsync(int id);
    Task<ApiResponse<PatientDetailsDto>> GetPatientByPatientIdAsync(string patientId);
    Task<ApiResponse<PagedResult<PatientDto>>> SearchPatientsAsync(string searchTerm, int pageNumber, int pageSize);
    Task<ApiResponse<string>> GeneratePatientIdAsync();
    Task<ApiResponse<PatientDto>> CreatePatientAsync(CreatePatientDto createPatientDto);
    Task<ApiResponse<PatientWithClinicalDetailsDto>> CreatePatientWithClinicalDetailsAsync(CreatePatientWithClinicalDetailsDto createPatientDto);
    Task<ApiResponse<PatientDto>> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto);
    Task<ApiResponse<string>> DeletePatientAsync(int id);

    Task<ApiResponse<IEnumerable<AllergyDto>>> GetAllergiesAsync(int patientId);
    Task<ApiResponse<AllergyDto>> GetAllergyAsync(int patientId, int id);
    Task<ApiResponse<AllergyDto>> AddAllergyAsync(int patientId, AllergyDto dto);
    Task<ApiResponse<AllergyDto>> UpdateAllergyAsync(int patientId, int id, AllergyDto dto);
    Task<ApiResponse<string>> DeleteAllergyAsync(int patientId, int id);

    Task<ApiResponse<IEnumerable<MedicationDto>>> GetMedicationsAsync(int patientId);
    Task<ApiResponse<IEnumerable<MedicationDto>>> GetActiveMedicationsAsync(int patientId);
    Task<ApiResponse<MedicationDto>> GetMedicationAsync(int patientId, int id);
    Task<ApiResponse<MedicationDto>> AddMedicationAsync(int patientId, MedicationDto dto);
    Task<ApiResponse<MedicationDto>> UpdateMedicationAsync(int patientId, int id, MedicationDto dto);
    Task<ApiResponse<MedicationDto>> DiscontinueMedicationAsync(int patientId, int id, DateTime endDate);
    Task<ApiResponse<string>> DeleteMedicationAsync(int patientId, int id);

    Task<ApiResponse<IEnumerable<MedicalHistoryDto>>> GetMedicalHistoryAsync(int patientId);
    Task<ApiResponse<MedicalHistoryDto>> GetMedicalHistoryAsync(int patientId, int id);
    Task<ApiResponse<MedicalHistoryDto>> AddMedicalHistoryAsync(int patientId, MedicalHistoryDto dto);
    Task<ApiResponse<MedicalHistoryDto>> UpdateMedicalHistoryAsync(int patientId, int id, MedicalHistoryDto dto);
    Task<ApiResponse<string>> DeleteMedicalHistoryAsync(int patientId, int id);
}
