using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Commands.UpdateVote
{
    public class UpdateVoteCommandValidator:AbstractValidator<UpdateVoteCommand>
    {
        public UpdateVoteCommandValidator()
        {
            RuleFor(p => p.Day).NotNull();
            RuleFor(p => p.DayType).NotNull();
            RuleFor(p => p.PlayerId).NotEmpty();
            RuleFor(p => p.TargetId).NotEmpty();
            RuleFor(p => p.GameSettingId).NotEmpty();
        }
    }
}
