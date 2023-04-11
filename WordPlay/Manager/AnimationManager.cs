using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WordPlay.Controls;

namespace WordPlay.Manager
{
    public class AnimationManager
    {
        private Animation _animation;

        private float _timer;

        private bool _repeat = true;

        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_animation.Texture,
                             Position,
                             new Rectangle(_animation.CurrentFrame * _animation.FrameWidth,
                                           0,
                                           _animation.FrameWidth,
                                           _animation.FrameHeight),
                             Color.White);
        }

        public void PlayContinuous(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.CurrentFrame = 0;

            _timer = 0;

            _repeat = true;
        }

        public void PlaySingle(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.CurrentFrame = 0;

            _timer = 0;

            _repeat = false;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                {
                    if (_repeat) _animation.CurrentFrame = 0;
                    else _animation.CurrentFrame  = _animation.FrameCount - 1;
                }

            }
        }
    }
}
