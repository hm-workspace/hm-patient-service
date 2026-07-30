using PatientService.InternalModels.Entities;

namespace PatientService.InternalModels.DTOs;

public class CreatePatientDto
{
    public string PatientId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class UpdatePatientDto : CreatePatientDto
{
}

public class AllergyDto
{
    public string Allergy { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public static AllergyDto FromEntity(AllergyEntity entity) => new()
    {
        Allergy = entity.Allergy,
        Severity = entity.Severity,
        Notes = entity.Notes
    };
}

public class MedicationDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string MedicationName { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Notes { get; set; } = string.Empty;

    public static MedicationDto FromEntity(MedicationEntity entity) => new()
    {
        Id = entity.Id,
        PatientId = entity.PatientId,
        MedicationName = entity.MedicationName,
        Dosage = entity.Dosage,
        Frequency = entity.Frequency,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        Notes = entity.Notes
    };
}

public class MedicalHistoryDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string ConditionName { get; set; } = string.Empty;
    public DateTime? DiagnosedDate { get; set; }
    public string Notes { get; set; } = string.Empty;

    public static MedicalHistoryDto FromEntity(MedicalHistoryEntity entity) => new()
    {
        Id = entity.Id,
        PatientId = entity.PatientId,
        ConditionName = entity.ConditionName,
        DiagnosedDate = entity.DiagnosedDate,
        Notes = entity.Notes
    };
}

public class EmergencyContactDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public static EmergencyContactDto FromEntity(EmergencyContactEntity entity) => new()
    {
        Id = entity.Id,
        PatientId = entity.PatientId,
        Name = entity.Name,
        Phone = entity.Phone
    };
}

public class InsuranceDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;

    public static InsuranceDto FromEntity(InsuranceEntity entity) => new()
    {
        Id = entity.Id,
        PatientId = entity.PatientId,
        Provider = entity.Provider,
        PolicyNumber = entity.PolicyNumber
    };
}

public class VitalDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateTime MeasurementDate { get; set; }
    public int? BloodPressureSystolic { get; set; }
    public int? BloodPressureDiastolic { get; set; }
    public int? HeartRate { get; set; }
    public decimal? Temperature { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public string? Notes { get; set; }

    public static VitalDto FromEntity(VitalEntity entity) => new()
    {
        Id = entity.Id,
        PatientId = entity.PatientId,
        MeasurementDate = entity.MeasurementDate,
        BloodPressureSystolic = entity.BloodPressureSystolic,
        BloodPressureDiastolic = entity.BloodPressureDiastolic,
        HeartRate = entity.HeartRate,
        Temperature = entity.Temperature,
        Weight = entity.Weight,
        Height = entity.Height,
        Notes = entity.Notes
    };
}

public class DiscontinueMedicationRequest
{
    public DateTime? EndDate { get; set; }
}

public class CreatePatientWithClinicalDetailsDto : CreatePatientDto
{
    public List<AllergyDto> Allergies { get; set; } = new();
    public List<MedicationDto> Medications { get; set; } = new();
    public List<MedicalHistoryDto> MedicalHistory { get; set; } = new();
    public List<EmergencyContactDto> EmergencyContacts { get; set; } = new();
    public List<InsuranceDto> Insurance { get; set; } = new();
    public List<VitalDto> Vitals { get; set; } = new();
}

public class UpdatePatientWithClinicalDetailsDto : UpdatePatientDto
{
    public List<AllergyDto> Allergies { get; set; } = new();
    public List<MedicationDto> Medications { get; set; } = new();
    public List<MedicalHistoryDto> MedicalHistory { get; set; } = new();
    public List<EmergencyContactDto> EmergencyContacts { get; set; } = new();
    public List<InsuranceDto> Insurance { get; set; } = new();
    public List<VitalDto> Vitals { get; set; } = new();
}

public class PatientWithClinicalDetailsDto
{
    public PatientDto Patient { get; set; } = new();
    public IReadOnlyCollection<AllergyDto> Allergies { get; set; } = Array.Empty<AllergyDto>();
    public IReadOnlyCollection<MedicationDto> Medications { get; set; } = Array.Empty<MedicationDto>();
    public IReadOnlyCollection<MedicalHistoryDto> MedicalHistory { get; set; } = Array.Empty<MedicalHistoryDto>();
    public IReadOnlyCollection<EmergencyContactDto> EmergencyContacts { get; set; } = Array.Empty<EmergencyContactDto>();
    public IReadOnlyCollection<InsuranceDto> Insurance { get; set; } = Array.Empty<InsuranceDto>();
    public IReadOnlyCollection<VitalDto> Vitals { get; set; } = Array.Empty<VitalDto>();
}

public class PatientDetailsDto
{
    public PatientDto Patient { get; set; } = new();
    public IReadOnlyCollection<AllergyDto> Allergies { get; set; } = Array.Empty<AllergyDto>();
    public IReadOnlyCollection<MedicationDto> Medications { get; set; } = Array.Empty<MedicationDto>();
    public IReadOnlyCollection<MedicalHistoryDto> MedicalHistory { get; set; } = Array.Empty<MedicalHistoryDto>();
    public IReadOnlyCollection<EmergencyContactDto> EmergencyContacts { get; set; } = Array.Empty<EmergencyContactDto>();
    public IReadOnlyCollection<InsuranceDto> Insurance { get; set; } = Array.Empty<InsuranceDto>();
    public IReadOnlyCollection<VitalDto> Vitals { get; set; } = Array.Empty<VitalDto>();
}

public class PatientDto
{
    public int Id { get; set; }
    public string PatientId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public static PatientDto FromEntity(PatientEntity entity) => new()
    {
        Id = entity.Id,
        PatientId = entity.PatientId,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        DateOfBirth = entity.DateOfBirth,
        Gender = entity.Gender,
        Email = entity.Email,
        Phone = entity.Phone,
        Address = entity.Address
    };
}


