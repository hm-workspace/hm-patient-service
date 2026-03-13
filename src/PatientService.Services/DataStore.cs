using PatientService.InternalModels.Entities;

namespace PatientService.Services;

public static class PatientStore
{
    public static int PatientSeed = 1;
    public static int AllergySeed = 0;
    public static int MedicationSeed = 0;
    public static readonly List<PatientEntity> Patients = new()
    {
        new PatientEntity
        {
            Id = 1,
            PatientId = "PAT001",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = "Male",
            Email = "john.doe@hm.local",
            Phone = "9000000001",
            Address = "Hyderabad",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    };
    public static readonly List<AllergyEntity> Allergies = new();
    public static readonly List<MedicationEntity> Medications = new();
}


