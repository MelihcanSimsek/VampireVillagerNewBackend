using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.VoteService
{
    public class VoteManager : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteManager(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        //Refactoring
        public async Task<Player> GetHangedPlayer(Guid gameSettingId, int day, bool dayType)
        {
            List<Vote> votes = (await _voteRepository.GetListAsync(p => p.GameSettingId == gameSettingId &&
            p.Day == day && p.DayType == dayType, include: m => m.Include(c => c.Target).ThenInclude(c=>c.GameStates))).Items.ToList();

            var groupedVoteList = votes.GroupBy(p => p.Target)
                .Select(group => new
                {
                    Player = group.Key,
                    VoteCount = group.Count()
                }).OrderByDescending(c => c.VoteCount)
            .ToList();

            if (groupedVoteList.Count > 1 && groupedVoteList[0].VoteCount == groupedVoteList[1].VoteCount) return null;

            if (groupedVoteList[0] == null) return null;

            return groupedVoteList.FirstOrDefault()?.Player;
        }
    }
}
