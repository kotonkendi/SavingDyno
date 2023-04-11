using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPlay.Manager;

namespace WordPlay.Controls
{
    public class AnimatedSprite : Component
    {
        #region Fields

        protected AnimationManager _animationManager;

        protected Animation _animation;

        protected Vector2 _position;

        protected Texture2D _texture;


        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public float Speed = 1f;

        public Vector2 Velocity;

        public bool GotKnockedOut = false;

        #endregion

        public AnimatedSprite(Animation animation)
        {
            _animation = animation;
            _animationManager = new AnimationManager(_animation);
            _animationManager.PlayContinuous(_animation);
        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
        }
    }
}
