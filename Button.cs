using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalPendulum
{
    internal class Button
    {
        int posX, posY;
        int width, height;
        string name;

        public Point size
        {
            get
            {
                return new Point(width, height);
            }
        }

        public Point position { get
            {
                return new Point(posX, posY);
            } }

        internal Button(int X, int Y, int width, int height, string name)
        {
            posX = X;
            posY = Y;
            this.width = width;
            this.height = height;
            this.name = name;
        }
        public bool isInButton()
         => Game1.mouseState.Position.X > posX && Game1.mouseState.Position.Y > posY
         && Game1.mouseState.Position.X < posX + width && Game1.mouseState.Position.Y < posY + height;

        public void Update(GameTime gameTime)
        {
            if (isInButton() && Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.lastMouseState.LeftButton == ButtonState.Released)
            {
                 switch(name)
                 {
                    case "reset":
                        Game1.isReset = true;
                        break;
                 }
            }
        }
    }
}
