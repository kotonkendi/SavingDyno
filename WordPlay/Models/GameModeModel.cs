using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using WordPlay.Controls;
using WordPlay.Enumerables;

namespace WordPlay.Models
{
    public class GameModeModel
    {
        public GameModeModel(GameMode gameMode)
        {
            GameMode = gameMode;
        }

        public GameMode GameMode { get; set; }
        public bool IsCorrectWordGuessed { get; set; }
        public int TimeLimitSeconds { get; set; }
        public int WaterLevelRiseRate { get; set; }
        public int RemainingTime { get; set; }
        public int CurrentScore { get; set; }
        public bool GameComplete { get; set; }

        public int TimerBuffer { get; set; } = 5;
        public int MinimumWinScore { get; set; } = 75;

        public List<string> CorrectWords { get; set; } = new List<string>();
        public List<string> GuessedWords { get; set; } = new List<string>();
        public List<TopScoreModel> TopScores { get; set; } = new List<TopScoreModel> { };

        public bool IsWinner { get => CurrentScore >= MinimumWinScore || IsCorrectWordGuessed; }
    }
}
