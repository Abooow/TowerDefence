using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Managers;

namespace TowerDefence.Moldels
{
    public delegate void RawMouseEventHandler(object sender, MouseState state);
    public delegate void RawKeyboardEventHandler(object sender, KeyboardState state);
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
    public delegate void MousePositionEventHandler(object sender, MousePositionEventArgs e);

    /// <summary>
    /// A collections of arguments for a Key event.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        public readonly Keys Key;
        public readonly bool CapsDown;
        public readonly bool ShiftDown;
        public readonly bool CtrlDown;
        public readonly bool AltDown;

        /// <summary>
        /// Initialize a new KeyEventArgs with a given key.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        public KeyEventArgs(Keys key) : this(key, false, false, false, false)
        {
        }

        /// <summary>
        /// Initialize a new KeyEventArgs.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        /// <param name="capsDown">Was Caps Lock enabled?</param>
        /// <param name="shiftDown">Was any Shift held down?</param>
        /// <param name="ctrlDown">Was any Ctrl held down?</param>
        /// <param name="altDown">Was any Alt held down?</param>
        public KeyEventArgs(Keys key, bool capsDown, bool shiftDown, bool ctrlDown, bool altDown)
        {
            Key = key;
            CapsDown = capsDown;
            ShiftDown = shiftDown;
            CtrlDown = ctrlDown;
            AltDown = altDown;
        }
    }

    /// <summary>
    /// A collections of arguments for a mouse event.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        public readonly MouseButton Key;
        public readonly Point Position;
        public readonly int X;
        public readonly int Y;
        public readonly int Wheel;

        /// <summary>
        /// Initialize a new MouseEventArgs.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        /// <param name="position">The cursor position when the key was pressed.</param>
        public MouseEventArgs(MouseButton key, Point position) : this(key, position, 0)
        {
        }

        /// <summary>
        /// Initialize a new MouseEventArgs.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        /// <param name="position">The cursor position when the key was pressed.</param>
        /// <param name="wheel">The scroll wheel value.</param>
        public MouseEventArgs(MouseButton key, Point position, int wheel)
        {
            Position = position;
            Wheel = wheel;

            X = position.X;
            Y = position.Y;
        }
    }

    /// <summary>
    /// A collections of arguments for a mouse position change event.
    /// </summary>
    public class MousePositionEventArgs : EventArgs
    {
        public readonly MouseButton Key;
        public readonly Point NewPosition;
        public readonly Point OldPosition;
        public readonly Point DeltaPosition;
        public readonly int DeltaX;
        public readonly int DeltaY;

        /// <summary>
        /// Initialize a new MousePositionEventArgs.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        /// <param name="newPosition">The new position of the cursor.</param>
        /// <param name="oldPosition">The old position of the cursor.</param>
        public MousePositionEventArgs(MouseButton key, Point newPosition, Point oldPosition)
        {
            Key = key;
            NewPosition = newPosition;
            OldPosition = oldPosition;
            DeltaPosition = newPosition - oldPosition;
            DeltaX = DeltaPosition.X;
            DeltaY = DeltaPosition.Y;
        }
    }
}