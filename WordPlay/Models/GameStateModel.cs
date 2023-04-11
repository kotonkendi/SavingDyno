using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WordPlay.Controls;

namespace WordPlay.Models
{
    public class GameStateModel : StateModel
    {
        public List<Button> LetterBubbles { get; set; } = new List<Button>();
        public Textbox GuessTextbox { get; set; }
        public Label TimerLabel { get; set; }
        public Label ScoreLabel { get; set; }
        public Label ExitLabel { get; set; }
        public Timer Timer { get; set; }
        public AnimatedSprite WaterAnimatedSprite { get; set; }
        public DynoSprite DynoAnimatedSprite { get; set; }
        public Button ShuffleButton { get; set; }

        public Button EnterButton { get; set; }
        public Shortcut ExitShortcut { get; set; }

        public Shortcut ShuffleShortcut { get; set; }

        public Image Pipe { get; set; }

        public Label CorrectWordLabel { get; set; }

    }
}
