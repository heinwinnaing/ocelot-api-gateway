namespace UsersApi.Commands.OtpRequest;

public class OtpRequestCommandDto
{
    public Guid Token { get; set; }
    public TimeSpan ExpiryIn { get; set; }
}
