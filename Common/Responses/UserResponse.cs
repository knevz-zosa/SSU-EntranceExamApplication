namespace Common.Responses;
public class UserResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public string Access { get; set; }
}
public class UserSession
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}

