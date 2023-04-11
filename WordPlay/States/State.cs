using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using WordPlay.Controls;
using System;

namespace WordPlay.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected GameCore _game;

        public Color BackgroundColor = Color.Coral;

        public Song BackgroundMusic;

        protected List<Component> _components;

        protected Button BackButton;
        protected Button QuitButton;

        #endregion

        #region Methods



        public State(GameCore game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;

            BackButton = new Button(_game.SharedResources.BackButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(720, 25)
            };

            BackButton.Click += BackButton_Click;

            QuitButton = new Button(_game.SharedResources.QuitButtonTexture, _game.SharedResources.BasicFont)
            {
                Position = new Vector2(760, 25)
            };

            QuitButton.Click += QuitGameButton_Click;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public virtual void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
        #endregion

        protected virtual void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.SharedResources.NegativeSoundEffect.Play();
            _game.Exit();
        }

        protected virtual void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new ProfileState(_game, _graphicsDevice, _content));
            _game.SharedResources.PositiveSoundEffect.Play();
        }
    }
}
