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
    public string MedicationName { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Notes { get; set; } = string.Empty;

    public static MedicationDto FromEntity(MedicationEntity entity) => new()
    {
        MedicationName = entity.MedicationName,
        Dosage = entity.Dosage,
        Frequency = entity.Frequency,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        Notes = entity.Notes
    };
}

public class DiscontinueMedicationRequest
{
    public DateTime? EndDate { get; set; }
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


