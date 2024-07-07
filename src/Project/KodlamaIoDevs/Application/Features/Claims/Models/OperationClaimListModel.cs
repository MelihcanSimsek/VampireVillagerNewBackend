using Application.Features.Claims.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Models
{
    public class OperationClaimListModel:BasePageableModel
    {
        public IList<GetListOperationClaimDto> Items { get; set; }
    }
}
