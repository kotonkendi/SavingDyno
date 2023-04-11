using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WordPlay.Controls
{
    public class Label : Component
    {
        protected SpriteBatch spriteBatch;
        protected SpriteFont font;
        protected String text;
        protected Vector2 position;
        protected Color color = Color.GhostWhite;
        protected float scale = 1f;

        public string Text { get => text; set => text = value; }
        public Vector2 Position { get => position; set => position = value; }

        public Label(SpriteFont font,
            String message,
            Vector2 position,
            Color color,
            float scale)
        {
            this.font = font;
            this.text = message;
            this.position = position;
            this.color = color;
            this.scale = scale;
        }

        public Label(SpriteFont font,
            String text,
            Vector2 position)
        {
            this.font = font;
            this.text = text;
            this.position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color, 0, new Vector2(0,0), scale, SpriteEffects.None, 0.8f);
        }

        public override void Update(GameTime gameTime)
        {


        }
    }
}
