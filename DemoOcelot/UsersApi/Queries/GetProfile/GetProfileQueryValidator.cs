using FluentValidation;

namespace UsersApi.Queries.GetProfile;

public class GetProfileQueryValidator
    : AbstractValidator<GetProfileQuery>
{
    public GetProfileQueryValidator()
    { }
}