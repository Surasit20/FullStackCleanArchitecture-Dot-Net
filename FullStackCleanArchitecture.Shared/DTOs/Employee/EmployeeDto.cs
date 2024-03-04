namespace FullStackCleanArchitecture.Shared.DTOs;
public class EmployeeDto
{
    public int EmployeeId { get; init; } // EmployeeId (Primary key)
    public string FirstName { get; set; } // FirstName (length: 300)
    public string LastName { get; set; } // LastName (length: 300)
    public string? FullName { get; init; } // FullName (length: 300)
    public bool IsDelete { get; init; } // IsDelete
}
