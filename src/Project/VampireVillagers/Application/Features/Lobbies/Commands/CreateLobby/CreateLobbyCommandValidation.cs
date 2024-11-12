using FluentValidation;

namespace Application.Features.Lobbies.Commands.CreateLobby
{
    public class CreateLobbyCommandValidation:AbstractValidator<CreateLobbyCommand>
    {
        public CreateLobbyCommandValidation()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.CreationDate).GreaterThan(DateTime.MinValue).WithMessage("CreationDate must be a valid date.");
        }
    }
}
