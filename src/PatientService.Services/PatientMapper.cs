using PatientService.InternalModels.DTOs;
using PatientService.InternalModels.Entities;

namespace PatientService.Services;

internal static class PatientMapper
{
    public static PatientDto ToDto(PatientEntity entity) => PatientDto.FromEntity(entity);

    public static PatientEntity ToEntity(CreatePatientDto dto, bool isActive = true) => new()
    {
        PatientId = dto.PatientId,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        DateOfBirth = dto.DateOfBirth,
        Gender = dto.Gender,
        Email = dto.Email,
        Phone = dto.Phone,
        Address = dto.Address,
        IsActive = isActive,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    public static PatientEntity ToEntity(UpdatePatientDto dto) => new()
    {
        PatientId = dto.PatientId,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        DateOfBirth = dto.DateOfBirth,
        Gender = dto.Gender,
        Email = dto.Email,
        Phone = dto.Phone,
        Address = dto.Address,
        UpdatedAt = DateTime.UtcNow
    };

    public static AllergyDto ToDto(AllergyEntity entity) => AllergyDto.FromEntity(entity);

    public static AllergyEntity ToEntity(int patientId, AllergyDto dto) => new()
    {
        PatientId = patientId,
        Allergy = dto.Allergy,
        Severity = dto.Severity,
        Notes = dto.Notes
    };

    public static MedicationDto ToDto(MedicationEntity entity) => MedicationDto.FromEntity(entity);

    public static MedicationEntity ToEntity(int patientId, MedicationDto dto) => new()
    {
        PatientId = patientId,
        MedicationName = dto.MedicationName,
        Dosage = dto.Dosage,
        Frequency = dto.Frequency,
        StartDate = dto.StartDate,
        EndDate = dto.EndDate,
        Notes = dto.Notes
    };

    public static MedicalHistoryDto ToDto(MedicalHistoryEntity entity) => MedicalHistoryDto.FromEntity(entity);

    public static MedicalHistoryEntity ToEntity(int patientId, MedicalHistoryDto dto) => new()
    {
        PatientId = patientId,
        ConditionName = dto.ConditionName,
        DiagnosedDate = dto.DiagnosedDate,
        Notes = dto.Notes
    };

    public static EmergencyContactDto ToDto(EmergencyContactEntity entity) => EmergencyContactDto.FromEntity(entity);

    public static EmergencyContactEntity ToEntity(int patientId, EmergencyContactDto dto) => new()
    {
        PatientId = patientId,
        Name = dto.Name,
        Phone = dto.Phone
    };

    public static InsuranceDto ToDto(InsuranceEntity entity) => InsuranceDto.FromEntity(entity);

    public static InsuranceEntity ToEntity(int patientId, InsuranceDto dto) => new()
    {
        PatientId = patientId,
        Provider = dto.Provider,
        PolicyNumber = dto.PolicyNumber
    };

    public static VitalDto ToDto(VitalEntity entity) => VitalDto.FromEntity(entity);

    public static VitalEntity ToEntity(int patientId, VitalDto dto) => new()
    {
        PatientId = patientId,
        MeasurementDate = dto.MeasurementDate,
        BloodPressureSystolic = dto.BloodPressureSystolic,
        BloodPressureDiastolic = dto.BloodPressureDiastolic,
        HeartRate = dto.HeartRate,
        Temperature = dto.Temperature,
        Weight = dto.Weight,
        Height = dto.Height,
        Notes = dto.Notes
    };
}
