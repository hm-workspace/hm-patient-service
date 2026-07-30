using Dapper;
using PatientService.Data;
using PatientService.InternalModels.Entities;
using PatientService.Utils.Common;
using System.Data;

namespace PatientService.Repository;

public class PatientRepository : BaseRepository, IPatientRepository
{
    public PatientRepository(IDbConnectionFactory connectionFactory)
        : base(connectionFactory)
    {
    }

    public async Task<PagedResult<PatientEntity>> GetPatientsAsync(SearchQuery searchQuery)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            using var grid = await connection.QueryMultipleAsync(
                StoredProcedureNames.GetPatientsPaged,
                new { searchQuery.PageNumber, searchQuery.PageSize, SearchTerm = searchQuery.SearchTerm },
                commandType: CommandType.StoredProcedure);

            var items = (await grid.ReadAsync<PatientEntity>()).ToList();
            var total = await grid.ReadFirstAsync<int>();
            return new PagedResult<PatientEntity>(items, total, searchQuery.PageNumber, searchQuery.PageSize);
        });
    }

    public Task<PatientEntity?> GetPatientByIdAsync(int id)
    {
        return QuerySingleOrDefaultAsync<PatientEntity>(
            StoredProcedureNames.GetPatientById,
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public Task<PatientEntity?> GetPatientByPatientIdAsync(string patientId)
    {
        return QuerySingleOrDefaultAsync<PatientEntity>(
            StoredProcedureNames.GetPatientByPatientId,
            new { PatientId = patientId },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PatientEntity> CreatePatientAsync(PatientEntity patient)
    {
        var id = await ExecuteScalarAsync<int>(
            StoredProcedureNames.CreatePatient,
            new
            {
                patient.PatientId,
                patient.FirstName,
                patient.LastName,
                patient.DateOfBirth,
                patient.Gender,
                patient.Email,
                patient.Phone,
                patient.Address,
                patient.IsActive,
                patient.CreatedAt,
                patient.UpdatedAt
            },
            commandType: CommandType.StoredProcedure);

        patient.Id = id;
        if (string.IsNullOrWhiteSpace(patient.PatientId))
        {
            patient.PatientId = $"PAT{id:000}";
        }
        return patient;
    }

    public async Task<PatientEntity?> UpdatePatientAsync(int id, PatientEntity patient)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.UpdatePatient,
            new
            {
                Id = id,
                patient.PatientId,
                patient.FirstName,
                patient.LastName,
                patient.DateOfBirth,
                patient.Gender,
                patient.Email,
                patient.Phone,
                patient.Address,
                patient.IsActive,
                UpdatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);

        if (rowsAffected <= 0)
        {
            return null;
        }

        return await GetPatientByIdAsync(id);
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DeletePatient,
            new { Id = id },
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }

    public async Task<string> GeneratePatientIdAsync()
    {
        return await ExecuteScalarAsync<string>(
            StoredProcedureNames.GeneratePatientId,
            commandType: CommandType.StoredProcedure) ?? string.Empty;
    }

    public async Task<IReadOnlyCollection<AllergyEntity>> GetAllergiesAsync(int patientId)
    {
        var items = await QueryAsync<AllergyEntity>(
            StoredProcedureNames.GetAllergiesByPatientId,
            new { PatientId = patientId },
            commandType: CommandType.StoredProcedure);
        return items.ToList();
    }

    public Task<AllergyEntity?> GetAllergyAsync(int patientId, int id)
    {
        return QuerySingleOrDefaultAsync<AllergyEntity>(
            StoredProcedureNames.GetAllergyById,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<AllergyEntity> AddAllergyAsync(AllergyEntity allergy)
    {
        var id = await ExecuteScalarAsync<int>(
            StoredProcedureNames.AddAllergy,
            new { allergy.PatientId, allergy.Allergy, allergy.Severity, allergy.Notes },
            commandType: CommandType.StoredProcedure);
        allergy.Id = id;
        return allergy;
    }

    public async Task<AllergyEntity?> UpdateAllergyAsync(int patientId, int id, AllergyEntity allergy)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.UpdateAllergy,
            new { PatientId = patientId, Id = id, allergy.Allergy, allergy.Severity, allergy.Notes },
            commandType: CommandType.StoredProcedure);

        if (rowsAffected <= 0)
        {
            return null;
        }

        return await GetAllergyAsync(patientId, id);
    }

    public async Task<bool> DeleteAllergyAsync(int patientId, int id)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DeleteAllergy,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }

    public async Task<IReadOnlyCollection<MedicationEntity>> GetMedicationsAsync(int patientId)
    {
        var items = await QueryAsync<MedicationEntity>(
            StoredProcedureNames.GetMedicationsByPatientId,
            new { PatientId = patientId },
            commandType: CommandType.StoredProcedure);
        return items.ToList();
    }

    public async Task<IReadOnlyCollection<MedicationEntity>> GetActiveMedicationsAsync(int patientId)
    {
        var items = await QueryAsync<MedicationEntity>(
            StoredProcedureNames.GetActiveMedicationsByPatientId,
            new { PatientId = patientId },
            commandType: CommandType.StoredProcedure);
        return items.ToList();
    }

    public Task<MedicationEntity?> GetMedicationAsync(int patientId, int id)
    {
        return QuerySingleOrDefaultAsync<MedicationEntity>(
            StoredProcedureNames.GetMedicationById,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<MedicationEntity> AddMedicationAsync(MedicationEntity medication)
    {
        var id = await ExecuteScalarAsync<int>(
            StoredProcedureNames.AddMedication,
            new
            {
                medication.PatientId,
                medication.MedicationName,
                medication.Dosage,
                medication.Frequency,
                medication.StartDate,
                medication.EndDate,
                medication.Notes
            },
            commandType: CommandType.StoredProcedure);
        medication.Id = id;
        return medication;
    }

    public async Task<MedicationEntity?> UpdateMedicationAsync(int patientId, int id, MedicationEntity medication)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.UpdateMedication,
            new
            {
                medication.PatientId,
                medication.Id,
                medication.MedicationName,
                medication.Dosage,
                medication.Frequency,
                medication.StartDate,
                medication.EndDate,
                medication.Notes
            },
            commandType: CommandType.StoredProcedure);

        if (rowsAffected <= 0)
        {
            return null;
        }

        return await GetMedicationAsync(patientId, id);
    }

    public async Task<MedicationEntity?> DiscontinueMedicationAsync(int patientId, int id, DateTime endDate)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DiscontinueMedication,
            new { PatientId = patientId, Id = id, EndDate = endDate },
            commandType: CommandType.StoredProcedure);

        if (rowsAffected <= 0)
        {
            return null;
        }

        return await GetMedicationAsync(patientId, id);
    }

    public async Task<bool> DeleteMedicationAsync(int patientId, int id)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DeleteMedication,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }

    public async Task<IReadOnlyCollection<MedicalHistoryEntity>> GetMedicalHistoryAsync(int patientId)
    {
        var items = await QueryAsync<MedicalHistoryEntity>(
            StoredProcedureNames.GetMedicalHistoryByPatientId,
            new { PatientId = patientId },
            commandType: CommandType.StoredProcedure);
        return items.ToList();
    }

    public Task<MedicalHistoryEntity?> GetMedicalHistoryAsync(int patientId, int id)
    {
        return QuerySingleOrDefaultAsync<MedicalHistoryEntity>(
            StoredProcedureNames.GetMedicalHistoryById,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<MedicalHistoryEntity> AddMedicalHistoryAsync(MedicalHistoryEntity medicalHistory)
    {
        var id = await ExecuteScalarAsync<int>(
            StoredProcedureNames.AddMedicalHistory,
            new
            {
                medicalHistory.PatientId,
                medicalHistory.ConditionName,
                medicalHistory.DiagnosedDate,
                medicalHistory.Notes
            },
            commandType: CommandType.StoredProcedure);
        medicalHistory.Id = id;
        return medicalHistory;
    }

    public async Task<MedicalHistoryEntity?> UpdateMedicalHistoryAsync(int patientId, int id, MedicalHistoryEntity medicalHistory)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.UpdateMedicalHistory,
            new
            {
                PatientId = patientId,
                Id = id,
                medicalHistory.ConditionName,
                medicalHistory.DiagnosedDate,
                medicalHistory.Notes
            },
            commandType: CommandType.StoredProcedure);

        if (rowsAffected <= 0)
        {
            return null;
        }

        return await GetMedicalHistoryAsync(patientId, id);
    }

    public async Task<bool> DeleteMedicalHistoryAsync(int patientId, int id)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DeleteMedicalHistory,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }

    public async Task<IReadOnlyCollection<EmergencyContactEntity>> GetEmergencyContactsAsync(int patientId)
    {
        var items = await QueryAsync<EmergencyContactEntity>(
            StoredProcedureNames.GetEmergencyContactsByPatientId,
            new { PatientId = patientId },
            commandType: CommandType.StoredProcedure);
        return items.ToList();
    }

    public Task<EmergencyContactEntity?> GetEmergencyContactAsync(int patientId, int id)
    {
        return QuerySingleOrDefaultAsync<EmergencyContactEntity>(
            StoredProcedureNames.GetEmergencyContactById,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EmergencyContactEntity> AddEmergencyContactAsync(EmergencyContactEntity emergencyContact)
    {
        var id = await ExecuteScalarAsync<int>(
            StoredProcedureNames.AddEmergencyContact,
            new { emergencyContact.PatientId, emergencyContact.Name, emergencyContact.Phone },
            commandType: CommandType.StoredProcedure);
        emergencyContact.Id = id;
        return emergencyContact;
    }

    public async Task<EmergencyContactEntity?> UpdateEmergencyContactAsync(int patientId, int id, EmergencyContactEntity emergencyContact)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.UpdateEmergencyContact,
            new { PatientId = patientId, Id = id, emergencyContact.Name, emergencyContact.Phone },
            commandType: CommandType.StoredProcedure);

        if (rowsAffected <= 0)
        {
            return null;
        }

        return await GetEmergencyContactAsync(patientId, id);
    }

    public async Task<bool> DeleteEmergencyContactAsync(int patientId, int id)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DeleteEmergencyContact,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }

    public async Task<IReadOnlyCollection<InsuranceEntity>> GetInsuranceAsync(int patientId)
    {
        var items = await QueryAsync<InsuranceEntity>(
            StoredProcedureNames.GetInsuranceByPatientId,
            new { PatientId = patientId },
            commandType: CommandType.StoredProcedure);
        return items.ToList();
    }

    public Task<InsuranceEntity?> GetInsuranceAsync(int patientId, int id)
    {
        return QuerySingleOrDefaultAsync<InsuranceEntity>(
            StoredProcedureNames.GetInsuranceById,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<InsuranceEntity> AddInsuranceAsync(InsuranceEntity insurance)
    {
        var id = await ExecuteScalarAsync<int>(
            StoredProcedureNames.AddInsurance,
            new { insurance.PatientId, insurance.Provider, insurance.PolicyNumber },
            commandType: CommandType.StoredProcedure);
        insurance.Id = id;
        return insurance;
    }

    public async Task<InsuranceEntity?> UpdateInsuranceAsync(int patientId, int id, InsuranceEntity insurance)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.UpdateInsurance,
            new { PatientId = patientId, Id = id, insurance.Provider, insurance.PolicyNumber },
            commandType: CommandType.StoredProcedure);

        if (rowsAffected <= 0)
        {
            return null;
        }

        return await GetInsuranceAsync(patientId, id);
    }

    public async Task<bool> DeleteInsuranceAsync(int patientId, int id)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DeleteInsurance,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }

    public async Task<IReadOnlyCollection<VitalEntity>> GetVitalsAsync(int patientId)
    {
        var items = await QueryAsync<VitalEntity>(
            StoredProcedureNames.GetVitalsByPatientId,
            new { PatientId = patientId },
            commandType: CommandType.StoredProcedure);
        return items.ToList();
    }

    public Task<VitalEntity?> GetVitalAsync(int patientId, int id)
    {
        return QuerySingleOrDefaultAsync<VitalEntity>(
            StoredProcedureNames.GetVitalById,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<VitalEntity> AddVitalAsync(VitalEntity vital)
    {
        var id = await ExecuteScalarAsync<int>(
            StoredProcedureNames.AddVital,
            new
            {
                vital.PatientId,
                vital.MeasurementDate,
                vital.BloodPressureSystolic,
                vital.BloodPressureDiastolic,
                vital.HeartRate,
                vital.Temperature,
                vital.Weight,
                vital.Height,
                vital.Notes
            },
            commandType: CommandType.StoredProcedure);
        vital.Id = id;
        return vital;
    }

    public async Task<VitalEntity?> UpdateVitalAsync(int patientId, int id, VitalEntity vital)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.UpdateVital,
            new
            {
                PatientId = patientId,
                Id = id,
                vital.MeasurementDate,
                vital.BloodPressureSystolic,
                vital.BloodPressureDiastolic,
                vital.HeartRate,
                vital.Temperature,
                vital.Weight,
                vital.Height,
                vital.Notes
            },
            commandType: CommandType.StoredProcedure);

        if (rowsAffected <= 0)
        {
            return null;
        }

        return await GetVitalAsync(patientId, id);
    }

    public async Task<bool> DeleteVitalAsync(int patientId, int id)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DeleteVital,
            new { PatientId = patientId, Id = id },
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }
}
