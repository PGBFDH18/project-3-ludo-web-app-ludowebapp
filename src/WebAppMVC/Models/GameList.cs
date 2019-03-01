﻿using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace WebAppMVC.Models
{
    public class GameList
    {
        public Dictionary<string, Game> AllGames { get; set; }
    }

    public partial class Game
    {
        [JsonProperty("dice")]
        public Dice Dice { get; set; }

        [JsonProperty("gameState")]
        public long GameState { get; set; }

        [JsonProperty("players")]
        public Player[] Players { get; set; }

        [JsonProperty("currentPlayerId")]
        public long CurrentPlayerId { get; set; }
    }

    public partial class Dice
    {
    }

    public partial class Player
    {
        [JsonProperty("playerId")]
        public long PlayerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playerColor")]
        public long PlayerColor { get; set; }

        [JsonProperty("pieces")]
        public Piece[] Pieces { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }
    }

    public partial class Piece
    {
        [JsonProperty("pieceId")]
        public long PieceId { get; set; }

        [JsonProperty("state")]
        public long State { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("pieceColor")]
        public long PieceColor { get; set; }
    }

    public partial class Game
    {
        public static Dictionary<string, Game> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, Game>>(json, WebAppMVC.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, Game> self) => JsonConvert.SerializeObject(self, WebAppMVC.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
