using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator:AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator()
        {
            RuleFor(p => p.IsOwner).NotNull();
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.LobbyId).NotEmpty();
        }
    }
}
