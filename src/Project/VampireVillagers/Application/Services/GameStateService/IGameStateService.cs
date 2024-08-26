using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.GameStateService
{
    public interface IGameStateService
    {
        Task StartGame(GameSetting gameSetting);
    }
}
