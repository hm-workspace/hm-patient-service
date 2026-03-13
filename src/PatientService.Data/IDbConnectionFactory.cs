using System.Data;

namespace PatientService.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

