using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using WordPlay.Controls;

namespace WordPlay.States
{
    public class RegistrationState : State
    {
        Button startGameButton;
        Textbox nameTextbox;
        public RegistrationState(GameCore game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            this.BackgroundColor = Color.Azure;
            this.BackgroundMusic = content.Load<Song>("Sounds/MainTheme");
            var logoTexture = _content.Load<Texture2D>("images/titles/gameTitle");


            var logo = new Image(logoTexture)
            {
                Position = new Vector2(50, 100),
            };

            nameTextbox = new Textbox(_game.SharedResources.TextboxTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(240, 250),
                PlaceholderText = "Enter Player Name",
                Text = "Enter Player Name",
                PenColour = Color.Gray
            };
            nameTextbox.Enter += NameTextbox_EnterClick;

            startGameButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(300, 300),
                Text = "Start Game",
                IsEnabled = false
            };

            startGameButton.Click += StartButton_Click;

            var quitGameButton = new Button(_game.SharedResources.ButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(300, 350),
                Text = "Quit Game",

            };

            quitGameButton.Click += QuitGameButton_Click;

            var background = new Background(_game.SharedResources.BackgroundTexture, 15f, true)
            {
                Layer = 0.1f,
            };

            _components = new List<Component>()
            {
                background,
                logo,
                nameTextbox,
                startGameButton,
                quitGameButton,
                QuitButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            startGameButton.IsEnabled = nameTextbox.HasValue;

            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void NameTextbox_EnterClick(object sender, System.EventArgs e)
        {
            StartGame();
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            if (!startGameButton.IsEnabled) return;

            _game.Player.Name = nameTextbox.Text;
            _game.SharedResources.PositiveSoundEffect.Play();
            _game.ChangeState(new ProfileState(_game, _graphicsDevice, _content));
        }
    }
}
