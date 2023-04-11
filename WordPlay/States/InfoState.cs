using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using WordPlay.Controls;
using WordPlay.Enumerables;

namespace WordPlay.States
{
    public class InfoState : State
    {
        private Button mainButton;
        private Button wordFormationButton;
        private Button pointsCalculationButton;
        private Button scoreboardButton;
        private Button levelsButton;
        public InfoState(GameCore game, GraphicsDevice graphicsDevice, ContentManager content, MechanicsType mechanicsType)
          : base(game, graphicsDevice, content)
        {
            this.BackgroundMusic = content.Load<Song>("Sounds/MainTheme");

            var background = new Background(content.Load<Texture2D>("Images/Background/Main"), 0f, true)
            {
                Layer = 0.1f,
            };

            var mechanicTitleLabel = new Label(_game.SharedResources.BasicFont, $"{GetMechanicsTitle(mechanicsType)}", new Vector2(50, 150));
            var mechanicsInformationLabel = new Label(_game.SharedResources.SmallBasicFont, $"{GetMechanics(mechanicsType)}", new Vector2(50, 200));

            mainButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Text = "Main",
                Position = new Vector2(50, 75),
                IsEnabled = mechanicsType != MechanicsType.Main
            };

            mainButton.Click += MainButton_Click;

            wordFormationButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Text = "Word",
                Position = new Vector2(200, 75),
                IsEnabled = mechanicsType != MechanicsType.WordFormation
            };

            wordFormationButton.Click += WordFormationButton_Click;

            pointsCalculationButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Text = "Points",
                Position = new Vector2(350, 75),
                IsEnabled = mechanicsType != MechanicsType.PointsCalculation
            };

            pointsCalculationButton.Click += PointsCalculationButton_Click;

            scoreboardButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Text = "Scoreboard",
                Position = new Vector2(500, 75),
                IsEnabled = mechanicsType != MechanicsType.Scoreboard
            };

            scoreboardButton.Click += ScoreboardButton_Click;

            levelsButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Text = "Levels",
                Position = new Vector2(650, 75),
                IsEnabled = mechanicsType != MechanicsType.Levels
            };

            levelsButton.Click += LevelsButton_Click;

            _components = new List<Component>()
            {
                background,
                mainButton,
                wordFormationButton,
                pointsCalculationButton,
                scoreboardButton,
                levelsButton,
                mechanicTitleLabel,
                mechanicsInformationLabel,
                QuitButton,
                BackButton,
            };

        }

        private static string GetMechanics(MechanicsType mechanicsType)
        {
            var mechanicsStringBuilder = new StringBuilder();
            switch (mechanicsType)
            {
                case MechanicsType.Main:
                    mechanicsStringBuilder.AppendLine("Every word has 7 jumbled letters.");
                    mechanicsStringBuilder.AppendLine("The player main goal is to save Dyno");
                    mechanicsStringBuilder.AppendLine("The player will form words to help Dyno survive");
                    mechanicsStringBuilder.AppendLine("Each correct word will freeze the water level");
                    mechanicsStringBuilder.AppendLine("Each incorrect word guess will cause the water level to rise");
                    mechanicsStringBuilder.AppendLine("When the player fails to guess a correct word in 10 seconds, the water level will rise");
                    mechanicsStringBuilder.AppendLine("When the water level passes DY-NOs head, he will drown, and the game is over.");
                    break;
                case MechanicsType.WordFormation:
                    mechanicsStringBuilder.AppendLine("Every word has 7 - letters jumbled");
                    mechanicsStringBuilder.AppendLine("The player will form 3 to 7 - letter words from the jumbled letters");
                    mechanicsStringBuilder.AppendLine("Each correct word formed has an equivalent point");
                    mechanicsStringBuilder.AppendLine("The player will have 90 seconds to form words");
                    mechanicsStringBuilder.AppendLine("A minimum score of 100 must be reached to win the game");
                    mechanicsStringBuilder.AppendLine("If the player guesses a 7 - letter word correctly:");
                    mechanicsStringBuilder.AppendLine("     He wins the round regardless if he reaches the minimum score or not");
                    mechanicsStringBuilder.AppendLine("     The water level will be permanently frozen for the round");
                    mechanicsStringBuilder.AppendLine("When the player gets the 7 - letter word correctly:");
                    mechanicsStringBuilder.AppendLine("     The game will continue until the end of the timer for the scoreboard.");
                    mechanicsStringBuilder.AppendLine("The player loses when: ");
                    mechanicsStringBuilder.AppendLine("     Timer ends and the player does not guess the 7 - letter word");
                    mechanicsStringBuilder.AppendLine("     Timer ends and the player doesnt reach the minimum score");


                    break;
                case MechanicsType.PointsCalculation:
                    mechanicsStringBuilder.AppendLine("Each letter is worth 1 point for a 3-letter to a 6-letter word");
                    mechanicsStringBuilder.AppendLine("A 7-letter word is worth 20 points");
                    break;
                case MechanicsType.Scoreboard:
                    mechanicsStringBuilder.AppendLine("The game will store the score for each game");
                    mechanicsStringBuilder.AppendLine("It will show the top 5 scores on the scoreboard");
                    break;
                case MechanicsType.Levels:
                    mechanicsStringBuilder.AppendLine("Beginner: Timer is 90 seconds, water level rise is 5 %");
                    mechanicsStringBuilder.AppendLine("Advanced: Timer is 75 seconds, water level rise is 10 %");
                    mechanicsStringBuilder.AppendLine("Hardcore: Timer is 60 seconds, water level rise is 15 %");
                    mechanicsStringBuilder.AppendLine("Nightmare: Timer is 50 seconds, water level rise is 20 %");

                    break;
            }
            return mechanicsStringBuilder.ToString();
        }

        private static string GetMechanicsTitle(MechanicsType mechanicsType)
        {
            var mechanicsTypeString = string.Empty;
            switch (mechanicsType)
            {
                case MechanicsType.Main:
                case MechanicsType.Scoreboard:
                case MechanicsType.Levels:
                    mechanicsTypeString = mechanicsType.ToString();
                    break;
                case MechanicsType.WordFormation:
                    mechanicsTypeString = "Word Formation";
                    break;
                case MechanicsType.PointsCalculation:
                    mechanicsTypeString = "Points Calculation";
                    break;
            }
            return mechanicsTypeString;
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            if (mainButton.IsEnabled)
            {
                _game.ChangeState(new InfoState(_game, _graphicsDevice, _content, MechanicsType.Main));
                _game.SharedResources.PositiveSoundEffect.Play();
            }
        }

        private void WordFormationButton_Click(object sender, EventArgs e)
        {
            if (wordFormationButton.IsEnabled)
            {
                _game.ChangeState(new InfoState(_game, _graphicsDevice, _content, MechanicsType.WordFormation));
                _game.SharedResources.PositiveSoundEffect.Play();
            }
        }

        private void PointsCalculationButton_Click(object sender, EventArgs e)
        {
            if (pointsCalculationButton.IsEnabled)
            {
                _game.ChangeState(new InfoState(_game, _graphicsDevice, _content, MechanicsType.PointsCalculation));
                _game.SharedResources.PositiveSoundEffect.Play();
            }
        }

        private void ScoreboardButton_Click(object sender, EventArgs e)
        {
            if (scoreboardButton.IsEnabled)
            {
                _game.ChangeState(new InfoState(_game, _graphicsDevice, _content, MechanicsType.Scoreboard));
                _game.SharedResources.PositiveSoundEffect.Play();
            }
        }

        private void LevelsButton_Click(object sender, EventArgs e)
        {
            if (levelsButton.IsEnabled)
            {
                _game.ChangeState(new InfoState(_game, _graphicsDevice, _content, MechanicsType.Levels));
                _game.SharedResources.PositiveSoundEffect.Play();
            }
        }
    }
}
