namespace ProjectManagement.Application.DTOs.Users;

public class UpdateUserDto
{
    public Guid Id { get; set; }          
    public string? UserName { get; set; } 
    public string? Email { get; set; }    
    public string? Password { get; set; } 
}
