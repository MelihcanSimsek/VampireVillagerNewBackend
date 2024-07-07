using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommandValidator:AbstractValidator<UpdateClaimCommand>
    {
        public UpdateClaimCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MinimumLength(2);
        }
    }
}
