namespace PatientService.InternalModels.Entities;

public class PatientEntity
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
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AllergyEntity
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Allergy { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class MedicationEntity
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string MedicationName { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Notes { get; set; } = string.Empty;
}

public class MedicalHistoryEntity
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string ConditionName { get; set; } = string.Empty;
    public DateTime? DiagnosedDate { get; set; }
    public string Notes { get; set; } = string.Empty;
}

public class EmergencyContactEntity
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class InsuranceEntity
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
}

public class VitalEntity
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
}


