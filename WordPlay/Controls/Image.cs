using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WordPlay.Controls
{
    public class Image
    : Component
    {
        #region Fields

        private Texture2D _texture;

        #endregion

        #region Properties
        public Vector2 Position { get; set; }

        public float Scale { get; set; } = 1f;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        #endregion

        #region Methods

        public Image(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (Scale == 1f) spriteBatch.Draw(_texture, Rectangle, null, colour, 0, new Vector2(0, 0), SpriteEffects.None, 0.8f);
            else spriteBatch.Draw(_texture, Position, null, colour, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0.8f);

        }

        public override void Update(GameTime gameTime)
        {

        }

        #endregion
    }
}
