using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Timers;
using System.Resources;
using WordPlay.Content.Words;
using WordPlay.Controls;
using WordPlay.Models;
using Microsoft.Xna.Framework.Input;
using System.IO;
using WordPlay.Enumerables;
using WordPlay.Manager;

namespace WordPlay.States
{
    public class GameState : State
    {
        GameModeModel _gameModeModel;
        GameStateModel _gameStateModel;
        public GameState(GameCore game, GraphicsDevice graphicsDevice, ContentManager content, GameModeModel gameModeModel)
          : base(game, graphicsDevice, content)
        {
            _gameModeModel = gameModeModel;
            _gameStateModel = new GameStateModel();
            InitializeGameValues();
            CreateLetterBubbles();

            this.BackgroundMusic = content.Load<Song>("Sounds/AquaticTheme");
            _gameStateModel.Background = new Background(_game.SharedResources.BackgroundTexture, 10f, true)
            {
                Layer = 0.1f,
            };

            var logoTexture = _content.Load<Texture2D>($"images/titles/{_gameModeModel.GameMode}");
            _gameStateModel.Logo = new Image(logoTexture)
            {
                Position = new Vector2(50, 20),
                Scale = 0.3f
            };


            _gameStateModel.TimerLabel = new Label(_game.SharedResources.LargeBasicFont, $"{_gameModeModel.RemainingTime}", new Vector2(400, 50));
            _gameStateModel.ScoreLabel = new Label(_game.SharedResources.BasicFont, $"{_gameModeModel.CurrentScore}", new Vector2(750, 100));
            _gameStateModel.CorrectWordLabel = new Label(_game.SharedResources.LargeBasicFont, $"", new Vector2(50, 400));

            _gameStateModel.ShuffleButton= new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Text = "Shuffle",
                Position = new Vector2(135, 215)
            };
            _gameStateModel.ShuffleButton.Click += ShuffleButton_Click;

            _gameStateModel.GuessTextbox = new Textbox(_game.SharedResources.TextboxTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(75, 300),
                PlaceholderText = "Type a word",
                Text = "Type a word",
                PenColour = Color.Gray
            };

            _gameStateModel.GuessTextbox.Enter += GuessWord_EnterClick;

            _gameStateModel.EnterButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Text = "Enter",
                Position = new Vector2(135, 350)
            };

            _gameStateModel.EnterButton.Click += EnterButton_Click;

            _gameStateModel.ShuffleShortcut = new Shortcut(Keys.Space);
            _gameStateModel.ShuffleShortcut.ShortcutEvent += ShuffleButton_Click;

            _gameStateModel.ExitLabel = new Label(_game.SharedResources.BasicFont, "", new Vector2(400, 120));

            _gameStateModel.ExitShortcut = new Shortcut(Keys.Space);
            _gameStateModel.ExitShortcut.ShortcutEvent += ExitShortcut_Click;

            var pipeTexture = _content.Load<Texture2D>($"images/dyno/pipe");
            _gameStateModel.Pipe = new Image(pipeTexture)
            {
                Position = new Vector2(600, 280),
                Scale = 0.8f
            };

            var waterAnimation = new Animation(_content.Load<Texture2D>($"images/background/sea_background"), 8);
            _gameStateModel.WaterAnimatedSprite = new AnimatedSprite(waterAnimation)
            {
                Position = new Vector2(575, 480)
            };


            var dynoAnimations = new Dictionary<string, Animation>()
              {
                { "Idle", new Animation(_content.Load<Texture2D>("images/dyno/dyno-idle"), 8) },
                { "Knockout", new Animation(_content.Load<Texture2D>("images/dyno/dyno-knockout"), 8) },
              };
            _gameStateModel.DynoAnimatedSprite = new DynoSprite(dynoAnimations) { Position = new Vector2(630, 150) };


            _components = new List<Component>()
            {
                _gameStateModel.Background,
                _gameStateModel.Logo,
                _gameStateModel.TimerLabel,
                _gameStateModel.ScoreLabel,
                _gameStateModel.ShuffleButton,
                _gameStateModel.GuessTextbox,
                _gameStateModel.EnterButton,
                _gameStateModel.ShuffleShortcut,
                base.QuitButton,
                base.BackButton,
                _gameStateModel.ExitLabel,
                _gameStateModel.ExitShortcut,
                _gameStateModel.Pipe,
                _gameStateModel.DynoAnimatedSprite,
                _gameStateModel.WaterAnimatedSprite,
                _gameStateModel.CorrectWordLabel
            };
            _components.AddRange(_gameStateModel.LetterBubbles);

            _gameStateModel.Timer.Start();
        }

        private void InitializeGameValues()
        {
            _gameModeModel.TopScores = _game.SharedResources.TopScoreDictionary[_gameModeModel.GameMode];
            _gameModeModel.RemainingTime = _gameModeModel.TimeLimitSeconds;
            _gameModeModel.CurrentScore = 0;
            _gameModeModel.CorrectWords = GetRandomWordList();
            _gameModeModel.GuessedWords.Clear();
            _gameModeModel.GameComplete = false;
            _gameStateModel.Timer = new System.Timers.Timer(1000);
            _gameStateModel.Timer.Elapsed += OnTimedEvent;
            _gameModeModel.IsCorrectWordGuessed = false;
            _timeListCorrectGuess = _gameModeModel.RemainingTime;
        }

        private static List<string> GetRandomWordList()
        {
            ResourceSet rs = WordsResource.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            var resources = rs.Cast<DictionaryEntry>();
            var nextIndex = ThreadSafeRandom.ThisThreadsRandom.Next(resources.Count());
            string randomResourceEntry = resources.ElementAt(nextIndex).Value.ToString();
            return randomResourceEntry.Split(",").ToList();
        }

        private void CreateLetterBubbles()
        {
            _gameStateModel.LetterBubbles = new List<Button>();
            var letters = _gameModeModel.CorrectWords[0].ToCharArray().ToList();
            letters.Shuffle();
            for (var ctr = 0; ctr < 7; ctr++)
            {
                var letterBubble = new Button(_content.Load<Texture2D>($"images/controls/bubble"), _game.SharedResources.BasicFont)
                {
                    Position = new Vector2(50 + ctr * 45, 150),
                    Text = letters.Skip(ctr).First().ToString()
                };
                _gameStateModel.LetterBubbles.Add(letterBubble);
            }
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            Shuffle();
        }

        private void Shuffle()
        {
            if (_gameModeModel.GameComplete) return;

            var letters = _gameModeModel.CorrectWords[0].ToCharArray().ToList();
            letters.Shuffle();
            _game.Content.Load<SoundEffect>("Sounds/Shuffle").Play();
            for (var ctr = 0; ctr < 7; ctr++)
            {
                _gameStateModel.LetterBubbles[ctr].Text = letters.Skip(ctr).First().ToString();
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            EnterWord();
        }

        private void GuessWord_EnterClick(object sender, EventArgs e)
        {
            EnterWord();
        }

        private void EnterWord()
        {
            if (_gameModeModel.GameComplete) return;

            if (_gameModeModel.GuessedWords.Contains(_gameStateModel.GuessTextbox.Text))
            {
                RespondToDuplicateEntry();
            }
            else if (_gameModeModel.CorrectWords.Contains(_gameStateModel.GuessTextbox.Text))
            {
                RespondToCorrectEntry();
            }
            else
            {
                RepondToWrongEntry();
            }
        }

        private void RepondToWrongEntry()
        {
            _game.SharedResources.NegativeSoundEffect.Play();
            _gameStateModel.GuessTextbox.PenColour = Color.Red;
            RaiseWaterLevel();
        }

        private void RespondToCorrectEntry()
        {
            _game.SharedResources.PositiveSoundEffect.Play();
            CHeckForCorrectSevenLetterWord();
            _gameModeModel.CurrentScore += _gameStateModel.GuessTextbox.Text.Length == 7 ? 20 : _gameStateModel.GuessTextbox.Text.Length;
            _gameModeModel.GuessedWords.Add(_gameStateModel.GuessTextbox.Text);
            _gameStateModel.ScoreLabel.Text = _gameModeModel.CurrentScore.ToString();
            _gameStateModel.GuessTextbox.PenColour = Color.Green;
            _timeListCorrectGuess = _gameModeModel.RemainingTime;
        }

        private void RespondToDuplicateEntry()
        {
            _game.SharedResources.NegativeSoundEffect.Play();
            _gameStateModel.GuessTextbox.PenColour = Color.Orange;
        }

        private void CHeckForCorrectSevenLetterWord()
        {
            if (_gameStateModel.GuessTextbox.Text.Length == 7)
            {
                _gameModeModel.IsCorrectWordGuessed = true;
                _gameStateModel.CorrectWordLabel.Text = _gameStateModel.GuessTextbox.Text;
            }
        }

        int _timeListCorrectGuess;
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            CheckForEnd();
            if (_gameModeModel.RemainingTime == 0) return;

            if (_gameModeModel.TimerBuffer > 0)
            {
                _gameModeModel.TimerBuffer--;
                _gameStateModel.TimerLabel.Text = _gameModeModel.RemainingTime.ToString();
            }
            else if (_gameModeModel.RemainingTime > 0)
            {
                _gameModeModel.RemainingTime--;
                _gameStateModel.TimerLabel.Text = _gameModeModel.RemainingTime.ToString();
                if (_gameModeModel.RemainingTime <= _timeListCorrectGuess - 10)
                {
                    _timeListCorrectGuess = _gameModeModel.RemainingTime;
                    RaiseWaterLevel();
                }
            }
        }

        private void RaiseWaterLevel()
        {
            if (_gameModeModel.IsCorrectWordGuessed) return;

            _gameStateModel.WaterAnimatedSprite.Position = new Vector2(_gameStateModel.WaterAnimatedSprite.Position.X, _gameStateModel.WaterAnimatedSprite.Position.Y - 250 * _gameModeModel.WaterLevelRiseRate / 100);
            if (_gameStateModel.WaterAnimatedSprite.Position.Y <= 235)
            {
                _gameModeModel.RemainingTime = 0;
                _gameStateModel.DynoAnimatedSprite.GotKnockedOut = true;
            }
        }

        private void CheckForEnd()
        {
            if (_gameModeModel.RemainingTime > 0 || _gameStateModel.TimerLabel.Text.Length > 2 || _gameModeModel.GameComplete) return;

            _gameModeModel.GameComplete = true;

            _gameStateModel.TimerLabel.Text = _gameModeModel.IsWinner ? "You Win" : "You Lose";
            _gameStateModel.ExitLabel.Text = "Press SPACEBAR to return to profile.";
            _gameStateModel.Timer.Stop();

            if (_gameModeModel.TopScores.Min(t => t.Score) > _gameModeModel.CurrentScore) return;

            _gameModeModel.TopScores[4].Score = _gameModeModel.CurrentScore;
            _gameModeModel.TopScores[4].Name = _game.Player.Name;
            _gameModeModel.TopScores = _gameModeModel.TopScores.OrderByDescending(s => s.Score).ToList();

            WriteTopScores();
        }

        private void WriteTopScores()
        {
            if (!File.Exists("TopScore.txt"))
                File.Create("TopScore.txt").Close();

            using (StreamWriter sw = new StreamWriter("TopScore.txt"))
            {
                for (var topScoreIndex = 1; topScoreIndex < 5; topScoreIndex++)
                {
                    var topScores = _game.SharedResources.TopScoreDictionary[(GameMode)topScoreIndex];
                    var topScoresRaw = topScores.OrderByDescending(t => t.Score).Select(t => $"{t.Name}-{t.Score}").ToList();
                    var joinedTopsScores = string.Join(",", topScoresRaw);
                    sw.WriteLine(joinedTopsScores);
                }
            }
        }

        private void ExitShortcut_Click(object sender, EventArgs e)
        {
            if (_gameModeModel.GameComplete)
            {
                base.BackButton_Click(sender, e);
            }
        }

        ~GameState()
        {
            _gameStateModel.Timer.Stop();
        }
    }
}
