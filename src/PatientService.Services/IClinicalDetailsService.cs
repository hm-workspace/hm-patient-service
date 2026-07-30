using PatientService.InternalModels.DTOs;
using PatientService.Utils.Common;

namespace PatientService.Services;

public interface IClinicalDetailsService
{
    Task<ApiResponse<PatientWithClinicalDetailsDto>> CreatePatientWithClinicalDetailsAsync(CreatePatientWithClinicalDetailsDto createPatientDto);
    Task<ApiResponse<PatientWithClinicalDetailsDto>> UpdatePatientWithClinicalDetailsAsync(int id, UpdatePatientWithClinicalDetailsDto updatePatientDto);

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

    Task<ApiResponse<IEnumerable<EmergencyContactDto>>> GetEmergencyContactsAsync(int patientId);
    Task<ApiResponse<EmergencyContactDto>> GetEmergencyContactAsync(int patientId, int id);
    Task<ApiResponse<EmergencyContactDto>> AddEmergencyContactAsync(int patientId, EmergencyContactDto dto);
    Task<ApiResponse<EmergencyContactDto>> UpdateEmergencyContactAsync(int patientId, int id, EmergencyContactDto dto);
    Task<ApiResponse<string>> DeleteEmergencyContactAsync(int patientId, int id);

    Task<ApiResponse<IEnumerable<InsuranceDto>>> GetInsuranceAsync(int patientId);
    Task<ApiResponse<InsuranceDto>> GetInsuranceAsync(int patientId, int id);
    Task<ApiResponse<InsuranceDto>> AddInsuranceAsync(int patientId, InsuranceDto dto);
    Task<ApiResponse<InsuranceDto>> UpdateInsuranceAsync(int patientId, int id, InsuranceDto dto);
    Task<ApiResponse<string>> DeleteInsuranceAsync(int patientId, int id);

    Task<ApiResponse<IEnumerable<VitalDto>>> GetVitalsAsync(int patientId);
    Task<ApiResponse<VitalDto>> GetVitalAsync(int patientId, int id);
    Task<ApiResponse<VitalDto>> AddVitalAsync(int patientId, VitalDto dto);
    Task<ApiResponse<VitalDto>> UpdateVitalAsync(int patientId, int id, VitalDto dto);
    Task<ApiResponse<string>> DeleteVitalAsync(int patientId, int id);
}
