using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using WordPlay.Controls;
using System;
using Microsoft.Xna.Framework.Input;
using System.IO;
using WordPlay.Enumerables;

namespace WordPlay.States
{
    public class ProfileState : State
    {
        public ProfileState(GameCore game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            this.BackgroundColor = Color.Coral;
            this.BackgroundMusic = content.Load<Song>("Sounds/MainTheme");
            var infoButtonTexture = _content.Load<Texture2D>("Images/Controls/infoButton");
            var smallBasicFont = _content.Load<SpriteFont>("Fonts/SmallBasicFont");
            var largeBasicFont = _content.Load<SpriteFont>("Fonts/LargeBasicFont");
            ReadTopScores();

            var background = new Background(content.Load<Texture2D>("Images/Background/Main"), 0f, true)
            {
                Layer = 0.1f,
            };

            var playerNameLabel = new Label(largeBasicFont, $"Welcome, {_game.Player.Name}", new Vector2(50, 50));
            var selectModeLabel = new Label(_game.SharedResources.BasicFont, $"Please select a mode", new Vector2(50, 150));

            var beginnerButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(300, 200),
                Text = "Beginner"
            };

            beginnerButton.Click += BeginnerButton_Click;

            var advancedButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(300, 250),
                Text = "Advanced"
            };

            advancedButton.Click += AdvancedButton_Click;

            var hardcoreButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(300, 300),
                Text = "Hardcore"
            };

            hardcoreButton.Click += HardcoreButton_Click;

            var nightmareButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(300, 350),
                Text = "Nightmare"
            };

            nightmareButton.Click += NightmareButton_Click;

            var infoButton = new Button(infoButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(720, 25)
            };

            infoButton.Click += InfoButton_Click;

            var shortcutEnter = new Shortcut(Keys.Enter);
            shortcutEnter.ShortcutEvent += BeginnerButton_Click;

            _components = new List<Component>()
            {
                background,
                selectModeLabel,
                beginnerButton,
                advancedButton,
                hardcoreButton,
                nightmareButton,
                playerNameLabel,
                QuitButton,
                infoButton,
                shortcutEnter
            };
        }

        private void ReadTopScores()
        {
            if (!File.Exists("TopScore.txt")) return;

            using (StreamReader sr = new StreamReader("TopScore.txt"))
            {
                for (var lineNumber = 1;  lineNumber < 5; lineNumber++)
                {
                    var line = sr.ReadLine();
                    var topsScoresRaw = line.Split(",");
                    for(var scoreNumber = 0; scoreNumber < 5; scoreNumber++)
                    {
                        var data = topsScoresRaw[scoreNumber].Split("-");
                        var currentModel = _game.SharedResources.TopScoreDictionary[(GameMode)lineNumber][scoreNumber];
                        currentModel.Name = data[0];
                        currentModel.Score = int.Parse(data[1]);
                    }
                }
            }
        }

        private void BeginnerButton_Click(object sender, EventArgs e)
        {
            EnterBeginnerMode();
        }

        private void EnterBeginnerMode()
        {
            _game.ChangeState(new GameLobbyState(_game, _graphicsDevice, _content, _game.Player.BeginnerGameMode));
            _game.SharedResources.PositiveSoundEffect.Play();
        }

        private void AdvancedButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameLobbyState(_game, _graphicsDevice, _content, _game.Player.AdvancedGameMode));
            _game.SharedResources.PositiveSoundEffect.Play();
        }

        private void HardcoreButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameLobbyState(_game, _graphicsDevice, _content, _game.Player.HardcoreGameMode));
            _game.SharedResources.PositiveSoundEffect.Play();
        }

        private void NightmareButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameLobbyState(_game, _graphicsDevice, _content, _game.Player.NightmareGameMode));
            _game.SharedResources.PositiveSoundEffect.Play();
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new InfoState(_game, _graphicsDevice, _content, MechanicsType.Main));
            _game.SharedResources.PositiveSoundEffect.Play();
        }
    }
}
