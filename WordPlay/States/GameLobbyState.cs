using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordPlay.Controls;
using WordPlay.Models;

namespace WordPlay.States
{
    public class GameLobbyState : State
    {
        private GameModeModel _gameModeModel;
        public GameLobbyState(GameCore game, GraphicsDevice graphicsDevice, ContentManager content, GameModeModel gameModeModel)
          : base(game, graphicsDevice, content)
        {
            _gameModeModel = gameModeModel;

            this.BackgroundMusic = content.Load<Song>("Sounds/MainTheme");

            var background = new Background(_game.SharedResources.BackgroundTexture, 10f, true)
            {
                Layer = 0.1f,
            };

            var logoTexture = _content.Load<Texture2D>($"images/titles/{_gameModeModel.GameMode}");
            var logo = new Image(logoTexture)
            {
                Position = new Vector2(50, 50),
            };

            var trophyTexture = _content.Load<Texture2D>($"images/controls/trophyButton");
            var trophy = new Image(trophyTexture)
            {
                Position = new Vector2(300, 200),
            };

            var topScoreTitleLabel = new Label(_game.SharedResources.BasicFont, $"Top 5", new Vector2(350, 200));
            var topScoreLabel = new Label(_game.SharedResources.BasicFont, $"{_game.SharedResources.TopScoreDictionary[_gameModeModel.GameMode][0].Score}-{_game.SharedResources.TopScoreDictionary[_gameModeModel.GameMode][0].Name}", new Vector2(350, 250), Color.LightSteelBlue, 1.1f);
            var runnerUpsScoreLabel = new Label(_game.SharedResources.SmallBasicFont, $"{string.Join(Environment.NewLine, _game.SharedResources.TopScoreDictionary[_gameModeModel.GameMode].Skip(1).Select(t=> $"{t.Score}-{t.Name}"))}", new Vector2(350, 280));

            var playButtonTexture = _content.Load<Texture2D>($"images/controls/playButton");
            var playButton = new Button(playButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(720, 400)
            };

            playButton.Click += PlayButton_Click;

            var shortcutEnter = new Shortcut(Keys.Enter);
            shortcutEnter.ShortcutEvent += PlayButton_Click;

            _components = new List<Component>()
            {
                background,
                logo,
                trophy,
                topScoreTitleLabel,
                topScoreLabel,
                runnerUpsScoreLabel,
                QuitButton,
                BackButton,
                playButton,
                shortcutEnter
            };

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void Play()
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _gameModeModel));
            _game.SharedResources.PositiveSoundEffect.Play();
        }
    }
}
