using FluentValidation;

namespace Application.Features.Lobbies.Commands.CreateLobby
{
    public class CreateLobbyCommandValidation:AbstractValidator<CreateLobbyCommand>
    {
        public CreateLobbyCommandValidation()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.CreationDate).NotEmpty();
            RuleFor(p => p.HasPassword).NotEmpty();
        }
    }
}
