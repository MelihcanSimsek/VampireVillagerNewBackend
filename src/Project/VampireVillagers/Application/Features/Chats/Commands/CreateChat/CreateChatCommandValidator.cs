using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Commands.CreateChat
{
    public class CreateChatCommandValidator:AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {
            RuleFor(p => p.PlayerId).NotEmpty();
            RuleFor(p => p.LobbyId).NotEmpty();
            RuleFor(p => p.Message).NotEmpty();
            RuleFor(p => p.MessageDate)
           .GreaterThan(DateTime.MinValue).WithMessage("MessageDate must be a valid date.");
        }
    }
}
