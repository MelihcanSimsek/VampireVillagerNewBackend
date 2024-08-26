using Application.Services.PlayerService;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.GameStateService
{
    public class GameStateManager : IGameStateService
    {
        private readonly IGameStateRepository _gameStateRepository;
        private readonly IPlayerService _playerService;

        private const int Priest_Skill_Number = 3;
        private const int Witch_Skill_Number = 3;
        private const int Vampire_Hunter_Skill_Number = 1;
        private const int Villager_Skill_Number = 0;
        private const int Vampire_Skill_Number = 0;
        private const int Shapeshifter_Vampire_Skill_Number = 3;
        private const int Transformer_Vampire_Skill_Number = 1;

        private const int Villager_Role_Number = 1;
        private const int Vampire_Role_Number = 2;
        private const int Priest_Role_Number = 3;
        private const int Witch_Role_Number = 4;
        private const int Vampire_Hunter_Role_Number = 5;
        private const int Shapeshifter_Vampire_Role_Number = 6;
        private const int Transformer_Vampire_Role_Number = 7;

        private const int VampireRoleRatio = 3;
        private const int Shapeshifter_or_Transformer_Role_Ratio = 5;

        public GameStateManager(IGameStateRepository gameStateRepository, IPlayerService playerService)
        {
            _gameStateRepository = gameStateRepository;
            _playerService = playerService;
        }

        public async Task StartGame(GameSetting gameSetting)
        {
            List<Player> players = (await _playerService.GetAllPlayerByLobbyId(gameSetting.LobbyId)).OrderBy(x=> Guid.NewGuid()).ToList();
            List<GameState> gameStates = new List<GameState>();
            int selectedPlayerNumber = gameSetting.VampireNumber + gameSetting.PriestNumber + gameSetting.WitchNumber + gameSetting.VampireHunterNumber;
            int selectedIndex = 0;

            selectedIndex =  SelectVampire(selectedIndex, gameStates, players, gameSetting);
            selectedIndex =  SelectPriest(selectedIndex, gameStates, players, gameSetting);
            selectedIndex =  SelectWitch(selectedIndex, gameStates, players, gameSetting);
            selectedIndex =  SelectVampireHunter(selectedIndex, gameStates, players, gameSetting);
            selectedIndex =  SelectVillager(selectedIndex, gameStates, players, gameSetting);

            foreach (var gameState in gameStates)
            {
               await _gameStateRepository.AddAsync(gameState);
            }
        }

        private int SelectVillager(int selectedIndex,List<GameState> gameStates,List<Player> players,GameSetting gameSetting)
        {
            int index;
            for (index=selectedIndex; index < players.Count; index++)
            {
                GameState gameState = new GameState()
                {
                    LiveState = true,
                    PlayerId = players[index].Id,
                    Role = Villager_Role_Number,
                    Skill = Villager_Skill_Number,
                    GameSettingId = gameSetting.Id
                };

                gameStates.Add(gameState);

            }
            return index;
        }

        private int SelectPriest(int selectedIndex,List<GameState> gameStates,List<Player> players,GameSetting gameSetting)
        {
            int index = selectedIndex;
            for (int i = 0; i < gameSetting.PriestNumber; i++)
            {
                GameState gameState = new GameState()
                {
                    LiveState = true,
                    PlayerId = players[index].Id,
                    Role = Priest_Role_Number,
                    Skill = Priest_Skill_Number,
                    GameSettingId = gameSetting.Id
                };

                gameStates.Add(gameState);
                index++;
            }

            return index;
        }

        private int SelectWitch(int selectedIndex,List<GameState> gameStates,List<Player> players,GameSetting gameSetting)
        {
            int index = selectedIndex;
            for (int i = 0; i < gameSetting.WitchNumber; i++)
            {
                GameState gameState = new GameState()
                {
                    LiveState = true,
                    PlayerId = players[index].Id,
                    Role = Witch_Role_Number,
                    Skill = Witch_Skill_Number,
                    GameSettingId = gameSetting.Id
                };

                gameStates.Add(gameState);
                index++;
            }
            return index;
        }

        private int SelectVampireHunter(int selectedIndex,List<GameState> gameStates,List<Player> players,GameSetting gameSetting)
        {
            int index = selectedIndex;
            for (int i = 0; i < gameSetting.VampireHunterNumber; i++)
            {
                GameState gameState = new GameState()
                {
                    LiveState = true,
                    PlayerId = players[index].Id,
                    Role = Vampire_Hunter_Role_Number,
                    Skill = Vampire_Hunter_Skill_Number,
                    GameSettingId = gameSetting.Id
                };

                gameStates.Add(gameState);
                index++;
            }

            return index;
        }

        private int SelectVampire(int selectedIndex,List<GameState> gameStates, List<Player> players, GameSetting gameSetting)
        {
            int index = selectedIndex;
            for (int i = 0; i < gameSetting.VampireNumber; i++)
            {
                if(gameSetting.TransformerState && gameSetting.ShapeshifterState)
                {
                    gameStates.Add(AssignVampireRole(1, players[index], gameSetting.Id));
                }
                else if(gameSetting.TransformerState)
                {
                    gameStates.Add(AssignVampireRole(2, players[index], gameSetting.Id));

                }
                else
                {
                    gameStates.Add(AssignVampireRole(3, players[index], gameSetting.Id));

                }

                index++;            
            }

            return index;
        }

        private GameState AssignVampireRole(int status,Player player,Guid gameSettingId)
        {
            bool isVampireHasASpecialRole = new Random().Next(1,10) < VampireRoleRatio ? true : false;
            if(isVampireHasASpecialRole)
            {
                return AssignSpecialVampireRole(status, player, gameSettingId);
            }
            else
            {
                GameState gameState = new GameState()
                {
                    LiveState = true,
                    PlayerId = player.Id,
                    Role = Vampire_Role_Number,
                    Skill = Vampire_Skill_Number,
                    GameSettingId = gameSettingId
                };

                return gameState;
            }
        }

        private GameState AssignSpecialVampireRole(int status,Player player,Guid gameSettingId)
        {
            GameState gameState;
            if (status == 1)
            {
                int role = new Random().Next(1, 10) < Shapeshifter_or_Transformer_Role_Ratio ? Shapeshifter_Vampire_Role_Number : Transformer_Vampire_Role_Number;

                 gameState = new GameState()
                {
                    LiveState = true,
                    PlayerId = player.Id,
                    Role = role == Shapeshifter_Vampire_Role_Number ? Shapeshifter_Vampire_Role_Number : Transformer_Vampire_Role_Number,
                    Skill = role == Shapeshifter_Vampire_Role_Number ? Shapeshifter_Vampire_Skill_Number : Transformer_Vampire_Skill_Number,
                    GameSettingId = gameSettingId
                };

                return gameState;
            }
            else if(status == 2)
            {
                 gameState = new GameState()
                {
                    LiveState = true,
                    PlayerId = player.Id,
                    Role = Shapeshifter_Vampire_Role_Number,
                    Skill = Shapeshifter_Vampire_Skill_Number,
                    GameSettingId = gameSettingId
                };

                return gameState;
            }
            else
            {
                 gameState = new GameState()
                {
                    LiveState = true,
                    PlayerId = player.Id,
                    Role = Transformer_Vampire_Role_Number,
                    Skill = Transformer_Vampire_Skill_Number,
                    GameSettingId = gameSettingId
                };

                return gameState;
            }
        }
    }
}
