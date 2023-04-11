using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Drawing;
using WordPlay.Models;
using WordPlay.States;

namespace WordPlay
{
    public class GameCore : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private State _currentState;
        private State _nextState;
        public static int ScreenWidth = 800;
        public static int ScreenHeight = 480;
        public PlayerModel Player;
        public SharedResourcesModel SharedResources;

        public GameCore()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        public void ChangeState(State state)
        {
            _nextState = state;
            ChangeMusic();
        }



        protected override void LoadContent()
        {
            Player = new PlayerModel();
            SharedResources = new SharedResourcesModel(Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new RegistrationState(this, _graphics.GraphicsDevice, Content);

            MediaPlayer.Play(_currentState.BackgroundMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_currentState.BackgroundColor);
            _currentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }

        private void ChangeMusic()
        {
            var changingMusic = typeof(GameState) == _currentState.GetType() || typeof(GameState) == _nextState.GetType();
            if (changingMusic)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(_nextState.BackgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
        }
    }
}