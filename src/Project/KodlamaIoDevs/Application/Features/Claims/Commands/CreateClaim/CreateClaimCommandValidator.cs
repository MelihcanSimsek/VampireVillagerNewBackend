using FluentValidation;

namespace Application.Features.Claims.Commands.CreateClaim
{
    public class CreateClaimCommandValidator:AbstractValidator<CreateClaimCommand>
    {
        public CreateClaimCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MinimumLength(2);
        }
    }
}
