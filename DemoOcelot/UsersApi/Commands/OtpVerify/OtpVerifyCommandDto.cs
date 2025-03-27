namespace UsersApi.Commands.OtpVerify;

public class OtpVerifyCommandDto
{
    public string AccessToken { get; set; } = null!;
    public TimeSpan ExpiryIn { get; set; }
}