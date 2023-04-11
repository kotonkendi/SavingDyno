using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace WordPlay.Controls
{
    public class Shortcut : Component
    {
        #region Fields

        private Keys _key;

        private bool _anyKey = false;

        #endregion

        #region Properties

        public event EventHandler ShortcutEvent;
        private bool _keyRecordPending = false;

        #endregion

        #region Methods

        public Shortcut(Keys key)
        {
            _key = key;
        }

        public Shortcut()
        {
            _anyKey = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();

            if (keys.Length == 1 && _keyRecordPending && (_anyKey || _key == keys[0]))
            {
                ShortcutEvent.Invoke(this, new EventArgs());
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
