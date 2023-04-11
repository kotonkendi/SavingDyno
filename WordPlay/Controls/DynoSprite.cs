using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WordPlay.Manager;
using System.Linq;

namespace WordPlay.Controls
{
    public class DynoSprite : Component
    {
        #region Fields

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

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

        public DynoSprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
            _animationManager.PlayContinuous(_animations["Idle"]);
        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);
            if (GotKnockedOut)
            {
                GotKnockedOut = false;
                _animationManager.PlaySingle(_animations["Knockout"]);
            }
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
