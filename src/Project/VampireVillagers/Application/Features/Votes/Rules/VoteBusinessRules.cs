using Application.Features.Votes.Constants;
using Application.Services.GameSettingService;
using Application.Services.PlayerService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Votes.Rules
{
    public class VoteBusinessRules
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IPlayerService _playerService;
        private readonly IGameSettingService _gameSettingService;

        public VoteBusinessRules(IVoteRepository voteRepository, IPlayerService playerService, IGameSettingService gameSettingService)
        {
            _voteRepository = voteRepository;
            _playerService = playerService;
            _gameSettingService = gameSettingService;
        }

        public async Task PlayerShouldBeExistsWhenVoteCreated(Guid playerId)
        {
            Player? player = await _playerService.GetPlayerById(playerId);
            if (player == null) throw new BusinessException(Messages.PlayerNotFoundWhenAddingVote);
        }

        public async Task TargetShouldBeExistsWhenVoteCreated(Guid targetId)
        {
            Player? target = await _playerService.GetPlayerById(targetId);
            if (target == null) throw new BusinessException(Messages.TargetNotFoundWhenAddingVote);
        }

        public async Task GameSettingShouldBeExistsWhenVoteCreated(Guid gameSettingId)
        {
            GameSetting? gameSetting = await _gameSettingService.GetGameSettingById(gameSettingId);
            if (gameSetting == null) throw new BusinessException(Messages.GameSettingNotFoundWhenAddingVote);
        }

        public async Task VoteShouldBeExistsWhenDeleted(Guid id)
        {
            Vote? vote = await _voteRepository.GetAsync(p => p.Id == id);
            if (vote == null) throw new BusinessException(Messages.VoteNotFoundWhenDeleted);
        }

        public async Task VoteShouldBeExistsWhenUpdated(Guid gameSettingId, int day, bool dayType, Guid playerId)
        {
            Vote? vote = await _voteRepository.GetAsync(p => p.PlayerId == playerId 
            && p.DayType == dayType 
            && p.GameSettingId == gameSettingId 
            && p.Day == day);

            if (vote == null) throw new BusinessException(Messages.VoteNotFoundWhenUpdated);
        }

        public async Task VoteCanNotBeDuplicatedWhenCreated(Guid gameSettingId, int day, bool dayType, Guid playerId)
        {
            Vote? vote = await _voteRepository.GetAsync(p => p.PlayerId == playerId
            && p.DayType == dayType
            && p.GameSettingId == gameSettingId
            && p.Day == day);

            if (vote != null) throw new BusinessException(Messages.VoteAlreadyExists);
        }
    }
}
