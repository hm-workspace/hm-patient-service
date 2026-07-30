namespace PatientService.Repository;

public static class StoredProcedureNames
{
    public const string GetPatientsPaged = "dbo.GetPatientsPaged";
    public const string GetPatientById = "dbo.GetPatientById";
    public const string GetPatientByPatientId = "dbo.GetPatientByPatientId";
    public const string CreatePatient = "dbo.CreatePatient";
    public const string UpdatePatient = "dbo.UpdatePatient";
    public const string DeletePatient = "dbo.DeletePatient";
    public const string GeneratePatientId = "dbo.GeneratePatientId";

    public const string GetAllergiesByPatientId = "dbo.GetAllergiesByPatientId";
    public const string GetAllergyById = "dbo.GetAllergyById";
    public const string AddAllergy = "dbo.AddAllergy";
    public const string UpdateAllergy = "dbo.UpdateAllergy";
    public const string DeleteAllergy = "dbo.DeleteAllergy";

    public const string GetMedicationsByPatientId = "dbo.GetMedicationsByPatientId";
    public const string GetActiveMedicationsByPatientId = "dbo.GetActiveMedicationsByPatientId";
    public const string GetMedicationById = "dbo.GetMedicationById";
    public const string AddMedication = "dbo.AddMedication";
    public const string UpdateMedication = "dbo.UpdateMedication";
    public const string DiscontinueMedication = "dbo.DiscontinueMedication";
    public const string DeleteMedication = "dbo.DeleteMedication";

    public const string GetMedicalHistoryByPatientId = "dbo.GetMedicalHistoryByPatientId";
    public const string GetMedicalHistoryById = "dbo.GetMedicalHistoryById";
    public const string AddMedicalHistory = "dbo.AddMedicalHistory";
    public const string UpdateMedicalHistory = "dbo.UpdateMedicalHistory";
    public const string DeleteMedicalHistory = "dbo.DeleteMedicalHistory";

    public const string GetEmergencyContactsByPatientId = "dbo.GetEmergencyContactsByPatientId";
    public const string GetEmergencyContactById = "dbo.GetEmergencyContactById";
    public const string AddEmergencyContact = "dbo.AddEmergencyContact";
    public const string UpdateEmergencyContact = "dbo.UpdateEmergencyContact";
    public const string DeleteEmergencyContact = "dbo.DeleteEmergencyContact";

    public const string GetInsuranceByPatientId = "dbo.GetInsuranceByPatientId";
    public const string GetInsuranceById = "dbo.GetInsuranceById";
    public const string AddInsurance = "dbo.AddInsurance";
    public const string UpdateInsurance = "dbo.UpdateInsurance";
    public const string DeleteInsurance = "dbo.DeleteInsurance";

    public const string GetVitalsByPatientId = "dbo.GetVitalsByPatientId";
    public const string GetVitalById = "dbo.GetVitalById";
    public const string AddVital = "dbo.AddVital";
    public const string UpdateVital = "dbo.UpdateVital";
    public const string DeleteVital = "dbo.DeleteVital";
}
