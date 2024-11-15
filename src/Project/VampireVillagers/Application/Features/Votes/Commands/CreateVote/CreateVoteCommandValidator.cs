﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Commands.CreateVote
{
    public class CreateVoteCommandValidator:AbstractValidator<CreateVoteCommand>
    {
        public CreateVoteCommandValidator()
        {
            RuleFor(p => p.Day).NotNull();
            RuleFor(p => p.DayType).NotNull();
            RuleFor(p => p.PlayerId).NotEmpty();
            RuleFor(p => p.GameSettingId).NotEmpty();
        }
    }
}
