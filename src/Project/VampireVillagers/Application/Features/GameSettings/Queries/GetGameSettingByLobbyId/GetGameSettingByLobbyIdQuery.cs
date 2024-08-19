using Application.Features.GameSettings.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameSettings.Queries.GetGameSettingByLobbyId
{
    public class GetGameSettingByLobbyIdQuery:IRequest<GetGameSettingByLobbyIdDto>,ISecuredRequest
    {
        public Guid LobbyId { get; set; }

        public string[] Roles { get; } = ["user"];

        public class GetGameSettingByLobbyIdQueryHandler : IRequestHandler<GetGameSettingByLobbyIdQuery, GetGameSettingByLobbyIdDto>
        {
            private readonly IGameSettingRepository _gameSettingRepository;
            private readonly IMapper _mapper;

            public GetGameSettingByLobbyIdQueryHandler(IGameSettingRepository gameSettingRepository, IMapper mapper)
            {
                _gameSettingRepository = gameSettingRepository;
                _mapper = mapper;
            }

            public async Task<GetGameSettingByLobbyIdDto> Handle(GetGameSettingByLobbyIdQuery request, CancellationToken cancellationToken)
            {
                GameSetting gameSetting = (await _gameSettingRepository.GetListAsync(p => p.LobbyId == request.LobbyId, orderBy: m => m.OrderByDescending(c => c.CreationDate))).Items.FirstOrDefault();
                GetGameSettingByLobbyIdDto getByLobbyIdDto = _mapper.Map<GetGameSettingByLobbyIdDto>(gameSetting);
                return getByLobbyIdDto;
            }
        }
    }
}
