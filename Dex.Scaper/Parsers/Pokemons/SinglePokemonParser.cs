﻿using Dex.Core.Entities;
using Dex.Scaper.Parsers.Pokemons;

namespace Dex.Scaper.Parsers
{
    public class SinglePokemonParser : IParser<Pokemon>
    {
        private readonly IPokemonParsers _pokemonParsers;

        public SinglePokemonParser(IPokemonParsers pokemonParsers)
        {
            _pokemonParsers = pokemonParsers;
        }

        public Pokemon Parse(string htmlRow)
        {
            var pokemon = new Pokemon();
            pokemon.DexNumber = _pokemonParsers.DexNumberParser.Parse(htmlRow);
            pokemon.Name = _pokemonParsers.NameParser.Parse(htmlRow);

            pokemon.Types = _pokemonParsers.TypeParser.Parse(htmlRow);

            pokemon.CandiesToEvolve = _pokemonParsers.CandiesParser.Parse(htmlRow);
            pokemon.EggDistance = _pokemonParsers.EggDistanceParser.Parse(htmlRow);

            pokemon.Moves = _pokemonParsers.MovesParser.Parse(htmlRow);

            pokemon.CatchRate = _pokemonParsers.CatchRateParser.Parse(htmlRow);
            pokemon.FleeRate = _pokemonParsers.FleeRateParser.Parse(htmlRow);
            pokemon.Attack = new CombatStat(_pokemonParsers.AttackParser.Parse(htmlRow));
            pokemon.Defense = new CombatStat(_pokemonParsers.DefenseParser.Parse(htmlRow));
            pokemon.Stamina = new CombatStat(_pokemonParsers.StaminaParser.Parse(htmlRow));

            return pokemon;
        }
    }
}