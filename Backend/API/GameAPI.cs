using KI_Fun.Backend.Messages;
using System;
using System.Collections.Generic;

namespace KI_Fun.Backend.API
{
    class GameApi
    {
        private Game _game;
        private Player.BasePlayer _player;

        private static Dictionary<Api, Wrapped> _innerDictionary = new Dictionary<Api, Wrapped>();

        public GameApi(Game game, Player.BasePlayer player)
        {
            _game = game;
            _player = player;
        }

        public CountryApi Country { get => _player.Country.Api; }

        public static void AddInner(Wrapped wrapped)
        {
            if (!_innerDictionary.ContainsKey(wrapped.Api))
                _innerDictionary.Add(wrapped.Api, wrapped);
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
                Army army = (Army)_innerDictionary[armyApi];
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
            Army army = (Army)_innerDictionary[armyApi];
            if (army.Owner.Player != _player)
                throw new AccessViolationException("Zugriff auf fremde Armee");
            army.MoveQueue.Enqueue(direction);
        }

        public bool TryGetMoveTarget(ArmyApi armyApi, Direction direction, out Province target)
        {
            return _game.TryGetMoveTarget(((Army)_innerDictionary[armyApi]).InProvince, direction, out target);
        }

        public bool IsArmyMovePossible(ArmyApi armyApi, Direction direction)
        {
            Army army = (Army)_innerDictionary[armyApi];
            if (_game.TryGetMoveTarget(army.InProvince, direction, out Province target))
                return _game.IsArmyAllowedInProvince(army, target);
            else
                return false;
        }
    }
}
