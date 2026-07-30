using PatientService.InternalModels.Entities;
using PatientService.Utils.Common;

namespace PatientService.Repository;

public interface IPatientRepository
{
    Task<PagedResult<PatientEntity>> GetPatientsAsync(SearchQuery searchQuery);
    Task<PatientEntity?> GetPatientByIdAsync(int id);
    Task<PatientEntity?> GetPatientByPatientIdAsync(string patientId);
    Task<PatientEntity> CreatePatientAsync(PatientEntity patient);
    Task<PatientEntity?> UpdatePatientAsync(int id, PatientEntity patient);
    Task<bool> DeletePatientAsync(int id);
    Task<string> GeneratePatientIdAsync();

    Task<IReadOnlyCollection<AllergyEntity>> GetAllergiesAsync(int patientId);
    Task<AllergyEntity?> GetAllergyAsync(int patientId, int id);
    Task<AllergyEntity> AddAllergyAsync(AllergyEntity allergy);
    Task<AllergyEntity?> UpdateAllergyAsync(int patientId, int id, AllergyEntity allergy);
    Task<bool> DeleteAllergyAsync(int patientId, int id);

    Task<IReadOnlyCollection<MedicationEntity>> GetMedicationsAsync(int patientId);
    Task<IReadOnlyCollection<MedicationEntity>> GetActiveMedicationsAsync(int patientId);
    Task<MedicationEntity?> GetMedicationAsync(int patientId, int id);
    Task<MedicationEntity> AddMedicationAsync(MedicationEntity medication);
    Task<MedicationEntity?> UpdateMedicationAsync(int patientId, int id, MedicationEntity medication);
    Task<MedicationEntity?> DiscontinueMedicationAsync(int patientId, int id, DateTime endDate);
    Task<bool> DeleteMedicationAsync(int patientId, int id);

    Task<IReadOnlyCollection<MedicalHistoryEntity>> GetMedicalHistoryAsync(int patientId);
    Task<MedicalHistoryEntity?> GetMedicalHistoryAsync(int patientId, int id);
    Task<MedicalHistoryEntity> AddMedicalHistoryAsync(MedicalHistoryEntity medicalHistory);
    Task<MedicalHistoryEntity?> UpdateMedicalHistoryAsync(int patientId, int id, MedicalHistoryEntity medicalHistory);
    Task<bool> DeleteMedicalHistoryAsync(int patientId, int id);

    Task<IReadOnlyCollection<EmergencyContactEntity>> GetEmergencyContactsAsync(int patientId);
    Task<EmergencyContactEntity?> GetEmergencyContactAsync(int patientId, int id);
    Task<EmergencyContactEntity> AddEmergencyContactAsync(EmergencyContactEntity emergencyContact);
    Task<EmergencyContactEntity?> UpdateEmergencyContactAsync(int patientId, int id, EmergencyContactEntity emergencyContact);
    Task<bool> DeleteEmergencyContactAsync(int patientId, int id);

    Task<IReadOnlyCollection<InsuranceEntity>> GetInsuranceAsync(int patientId);
    Task<InsuranceEntity?> GetInsuranceAsync(int patientId, int id);
    Task<InsuranceEntity> AddInsuranceAsync(InsuranceEntity insurance);
    Task<InsuranceEntity?> UpdateInsuranceAsync(int patientId, int id, InsuranceEntity insurance);
    Task<bool> DeleteInsuranceAsync(int patientId, int id);

    Task<IReadOnlyCollection<VitalEntity>> GetVitalsAsync(int patientId);
    Task<VitalEntity?> GetVitalAsync(int patientId, int id);
    Task<VitalEntity> AddVitalAsync(VitalEntity vital);
    Task<VitalEntity?> UpdateVitalAsync(int patientId, int id, VitalEntity vital);
    Task<bool> DeleteVitalAsync(int patientId, int id);
}

