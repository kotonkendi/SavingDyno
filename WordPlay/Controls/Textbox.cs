using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace WordPlay.Controls
{
    public class Textbox : Component
    {
        #region Fields

        private SpriteFont _font;

        private Texture2D _texture;

        #endregion

        #region Properties

        public event EventHandler Enter;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text { get; set; }

        public string PlaceholderText { get; set; }

        public bool HasValue { get => !string.IsNullOrEmpty(Text) && Text != PlaceholderText; }

        #endregion

        #region Methods

        public Textbox(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.Gray;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            spriteBatch.Draw(_texture, Rectangle, null, colour, 0, new Vector2(0, 0), SpriteEffects.None, 0.9f);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0.91f);
            }
        }

        private bool _keyRecordPending = true;

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();

            if (keys.Length > 0 && _keyRecordPending)
            {
                var currentKey = keys[0];
                if (Text == PlaceholderText || PenColour == Color.Red || PenColour == Color.Green) Text = string.Empty;

                if ((int)currentKey > 64 && (int)currentKey < 91 && Text.Length <= 6)
                {
                    var keyValue = currentKey.ToString();
                    Text += keyValue;
                    PenColour = Color.Black;
                }

                else if (currentKey == Keys.Back && Text.Length > 0)
                {
                    Text = Text.Substring(0, Text.Length - 1);
                }

                else if (currentKey == Keys.Enter && Text.Length > 2)
                {
                    Enter.Invoke(this, new EventArgs());
                }

                _keyRecordPending = false;
            }
            else if (keys.Length == 0)
            {
                _keyRecordPending = true;
            }

        }

        #endregion
    }
}
