﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BowlingSimulator.Models
{
    public class GameRecord
    {
        private List<Game> games;
        private string path;

        public GameRecord()
        {
            this.path = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), @"bowlingRecords.json")).LocalPath;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                if (json != "")
                    games = JsonConvert.DeserializeObject<List<Game>>(json);
                else
                    games = new List<Game>();
            }
        }

        public Game FindGameByGameId(string gameId)
        {
            return games.Find(x => x.Id == gameId);
        }

        public Game AddGame(string player)
        {
            var game = new Game(player);
            this.games.Add(game);

            Save();

            return game;
        }

        public void Save()
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, this.games);
            }
        }
    }
} 