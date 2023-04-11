using System;
using System.Text;
using System.Threading.Tasks;
using WordPlay.Enumerables;

namespace WordPlay.Models
{
    public class PlayerModel
    {
        public string Name { get; set; } = String.Empty;
        public GameModeModel BeginnerGameMode { get; set; } = new GameModeModel(GameMode.Beginner) { TimeLimitSeconds = 90, WaterLevelRiseRate = 5};
        public GameModeModel AdvancedGameMode { get; set; } = new GameModeModel(GameMode.Advanced) { TimeLimitSeconds = 75, WaterLevelRiseRate = 10 };
        public GameModeModel HardcoreGameMode { get; set; } = new GameModeModel(GameMode.Hardcore) { TimeLimitSeconds = 60, WaterLevelRiseRate = 15 };
        public GameModeModel NightmareGameMode { get; set; } = new GameModeModel(GameMode.Nightmare) { TimeLimitSeconds = 50, WaterLevelRiseRate = 20 };
    }
}
