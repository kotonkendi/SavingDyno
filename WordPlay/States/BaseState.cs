using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using WordPlay.Controls;

namespace WordPlay.States
{
    public class BaseState : State
    {
        public BaseState(GameCore game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            this.BackgroundMusic = content.Load<Song>("Sounds/MainTheme");

            var background = new Background(_game.SharedResources.BackgroundTexture, 10f, true)
            {
                Layer = 0.1f,
            };

            _components = new List<Component>()
            {
                background,
                QuitButton,
                BackButton,
            };

        }
    }
}
