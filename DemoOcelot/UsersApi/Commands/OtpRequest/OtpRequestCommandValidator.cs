using FluentValidation;

namespace UsersApi.Commands.OtpRequest;

public class OtpRequestCommandValidator
    : AbstractValidator<OtpRequestCommand>
{
    public OtpRequestCommandValidator()
    {
        RuleFor(r => r.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(50);
    }
}