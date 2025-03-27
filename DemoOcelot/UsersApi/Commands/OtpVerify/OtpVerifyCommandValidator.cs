using FluentValidation;

namespace UsersApi.Commands.OtpVerify;

public class OtpVerifyCommandValidator
    : AbstractValidator<OtpVerifyCommand>
{
    public OtpVerifyCommandValidator()
    {
        RuleFor(r => r.Token)
            .NotEqual(Guid.Empty);

        RuleFor(r => r.Code)
            .NotNull()
            .NotEmpty()
            .MaximumLength(10);
    }
}