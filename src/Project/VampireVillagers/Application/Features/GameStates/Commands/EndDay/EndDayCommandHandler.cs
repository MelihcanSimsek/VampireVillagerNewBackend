using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameStates.Commands.EndDay
{
    public class EndDayCommandHandler : AbstractValidator<EndDayCommand>
    {
        public EndDayCommandHandler()
        {
            RuleFor(c => c.Day).NotNull();
            RuleFor(c => c.GameSettingId).NotEmpty();
        }
    }
}
