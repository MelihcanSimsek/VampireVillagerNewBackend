using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameSettings.Commands.CreateGameSetting
{
    public class CreateGameSettingCommandValidator:AbstractValidator<CreateGameSettingCommand>
    {
        public CreateGameSettingCommandValidator()
        {
            RuleFor(p => p.LobbyId).NotEmpty();
            RuleFor(p => p.CreationDate).NotNull();
            RuleFor(p => p.WitchNumber).NotNull();
            RuleFor(p => p.VampireNumber).NotNull();
            RuleFor(p => p.PriestNumber).NotNull();
            RuleFor(p => p.ShapeshifterState).NotNull();
            RuleFor(p => p.TransformerState).NotNull();
            RuleFor(p => p.VampireHunterNumber).NotNull();
            RuleFor(p => p.DayTime).NotNull();
            RuleFor(p => p.NightTime).NotNull();
        }
    }
}
