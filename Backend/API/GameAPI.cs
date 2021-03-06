﻿using KI_Fun.Backend.Messages;
using System;
using System.Collections.Generic;

namespace KI_Fun.Backend.API
{
    class GameApi
    {
        private Game _game;
        private Country _country;

        public ApiCollection<CountryApi> Countries { get { return new ApiCollection<CountryApi>(_game.Countries); } }

        public GameApi(Game game, Country country)
        {
            _game = game;
            _country = country;
        }

        public CountryApi Country { get => _country.Api; }
    }
}
