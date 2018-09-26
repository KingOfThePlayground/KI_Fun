﻿using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class GameApi
    {
        private Game _game;
        private Player.BasePlayer _player;

        private static Dictionary<Api, Wrapped> _accessDictionary = new Dictionary<Api, Wrapped>();

        public GameApi(Game game, Player.BasePlayer player)
        {
            _game = game;
            _player = player;
        }

        public CountryApi Country { get => (CountryApi)_player.Country.Api; }

        public static void AddAccess(Wrapped wrapped)
        {
            if (!_accessDictionary.ContainsKey(wrapped.Api))
                _accessDictionary.Add(wrapped.Api, wrapped);
        }

        public bool TryDeclareWar(Country country)
        {
            if (country.MarchAccess.Contains(country) || country.War.Contains(country))
                return false;
            else
            {
                country.War.Add(_player.Country);
                _player.Country.War.Add(country);
                country.Player.MessageQueue.Enqueue(new WarDeclarationMessage(_player.Country, country));
                return true;
            }
        }

        public bool TrySendArmy(ArmyApi armyApi, Direction direction)
        {
            if (IsArmyMovePossible(armyApi, direction))
            {
                Army army = (Army)_accessDictionary[armyApi];
                if (army.Owner.Player != _player)
                    throw new AccessViolationException("Zugriff auf fremde Armee");
                army.MovingDirection = direction;
                army.MovingProgress = 0d;
                return true;
            }
            else
                return false;
        }

        public void EnqueMarchOrder(ArmyApi armyApi, Direction direction)
        {
            Army army = (Army)_accessDictionary[armyApi];
            if (army.Owner.Player != _player)
                throw new AccessViolationException("Zugriff auf fremde Armee");
            army.MoveQueue.Enqueue(direction);
        }

        public bool TryGetMoveTarget(ArmyApi armyApi, Direction direction, out Province target)
        {
            return _game.TryGetMoveTarget(((Army)(Army)_accessDictionary[armyApi]).InProvince, direction, out target);
        }

        public bool IsArmyMovePossible(ArmyApi armyApi, Direction direction)
        {
            Army army = (Army)_accessDictionary[armyApi];
            if (_game.TryGetMoveTarget(army.InProvince, direction, out Province target))
                return _game.IsArmyAllowedInProvince(army, target);
            else
                return false;
        }
    }
}
