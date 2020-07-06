// credits to: https://github.com/A1rPun/MonoGame.InputManager

// Known bugs: MouseJustReleased and KeyJustReleased events doesn't work.
//   The OnMouseClick and OnKeyPress methods will only be invoked once the currentButtonState is pressed, 
//   but JustReleased needs the currentButtonState to be released and lastButtonState to be pressed.

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Moldels;

namespace TowerDefence.Managers
{
    /// <summary>
    /// Managers any keyboard and mouse presses/changes.
    /// </summary>
    public static class InputManager
    {
        #region Events
        /// <summary>
        /// An event that will fire once any keyboard key have been pressed.
        /// </summary>
        public static event RawKeyboardEventHandler KeyboardChanged;
        /// <summary>
        /// An event that will fire once any mouse button have been pressed.
        /// </summary>
        public static event RawMouseEventHandler MouseChanged;
        /// <summary>
        /// An event that will fire once the cursor have moved.
        /// </summary>
        public static event MousePositionEventHandler MousePositionChanged;
        /// <summary>
        /// An event that will fire once a mouse button is being pressed.
        /// </summary>
        public static event MouseEventHandler MousePressed;
        /// <summary>
        /// An event that will fire once a mouse button is being held down.
        /// </summary>
        public static event MouseEventHandler MouseHeld;
        /// <summary>
        /// An event that will fire once a mouse button was just pressed.
        /// </summary>
        public static event MouseEventHandler MouseJustPressed;
        /// <summary>
        /// An event that will fire once a mouse button was just released.
        /// </summary>
        public static event MouseEventHandler MouseJustReleased;
        /// <summary>
        /// An event that will fire once a key is being pressed.
        /// </summary>
        public static event KeyEventHandler KeyPressed;
        /// <summary>
        /// An event that will fire once a key is being held down.
        /// </summary>
        public static event KeyEventHandler KeyHeld;
        /// <summary>
        /// An event that will fire once a key was just pressed.
        /// </summary>
        public static event KeyEventHandler KeyJustPressed;
        /// <summary>
        /// An event that will fire once a key was just released.
        /// </summary>
        public static event KeyEventHandler KeyJustReleased;
        #endregion

        private static KeyboardState previousKeyboardState;
        private static KeyboardState currentKeyboardState;
        private static MouseState previousMouseState;
        private static MouseState currentMouseState;

        /// <summary>
        /// Used to initialize the InputManager.
        /// </summary>
        static InputManager()
        {
            MousePressed += OnMouseClick;
            KeyPressed += OnKeyPress;
        }

        /// <summary>
        /// A method that is attached to the MousePressed event; used to invoke other events, such as MouseHeld, MouseJustPressed and MouseJustReleased.
        /// </summary>
        private static void OnMouseClick(object sender, MouseEventArgs e)
        {
            // MouseHeld event: 
            if (IsMouseHeld(e.Key))
                MouseHeld?.Invoke(null, e);

            // MouseJustPressed event: 
            if (IsMouseJustPressed(e.Key))
                MouseJustPressed?.Invoke(null, e);

            // DOESN'T WORK
            // MouseJustReleased event: 
            if (IsMouseJustReleased(e.Key))
                MouseJustReleased?.Invoke(null, e);
        }

        /// <summary>
        /// A method that is attached to the KeyPressed event; used to invoke other events, such as KeyHeld, KeyJustPressed and KeyJustReleased.
        /// </summary>
        private static void OnKeyPress(object sender, KeyEventArgs e)
        {
            // KeyHeld event: 
            if (IsKeyHeld(e.Key))
                KeyHeld?.Invoke(null, e);

            // KeyJustPressed event: 
            if (IsKeyJustPressed(e.Key))
                KeyJustPressed?.Invoke(null, e);

            // DOESN'T WORK
            // KeyJustReleased event: 
            if (IsKeyJustReleased(e.Key))
                KeyJustReleased?.Invoke(null, e);
        }

        /// <summary>
        /// Used to update the InputManager for getting the latest mouse and keyboard state.
        /// </summary>
        public static void Update()
        {
            // Update previous and current Keyboard state.
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            // Update previous and current mouse state.
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // Check for input changes.
            CheckForKeyboardChange();
            CheckForMouseChange();
        }

        #region MouseEvents
        /// <summary>
        /// Is a mouse button pressed?
        /// </summary>
        /// <param name="input">The mouse button to check.</param>
        /// <returns>true if the specified mouse button is pressed; false otherwise.</returns>
        public static bool IsMousePressed(MouseButton input)
        {
            return IsMousePressed(currentMouseState, input);
        }

        /// <summary>
        /// Is a mouse button held down?
        /// </summary>
        /// <param name="input">The mouse button to check.</param>
        /// <returns>true if the specified mouse button is held down; false otherwise.</returns>
        public static bool IsMouseHeld(MouseButton input)
        {
            return IsMousePressed(currentMouseState, input) && IsMousePressed(previousMouseState, input);
        }

        /// <summary>
        /// Was a mouse button just pressed?
        /// </summary>
        /// <param name="input">The mouse button to check.</param>
        /// <returns>true if the specified mouse button was just pressed; false otherwise.</returns>
        public static bool IsMouseJustPressed(MouseButton input)
        {
            return IsMousePressed(currentMouseState, input) && !IsMousePressed(previousMouseState, input);
        }

        /// <summary>
        /// Was a mouse button just released?
        /// </summary>
        /// <param name="input">The mouse button to check.</param>
        /// <returns>true if the specified mouse button was just released; false otherwise.</returns>
        public static bool IsMouseJustReleased(MouseButton input)
        {
            return !IsMousePressed(currentMouseState, input) && IsMousePressed(previousMouseState, input);
        }
        #endregion

        #region KeyboadEvents
        /// <summary>
        /// Is a key pressed?
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>true if the specified key is pressed; false otherwise.</returns>
        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Is a key held down?
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>true if the specified key is held down; false otherwise.</returns>
        public static bool IsKeyHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Was a key just pressed?
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>true if the specified key was just pressed; false otherwise.</returns>
        public static bool IsKeyJustPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Was a mouse button just released?
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>true if the specified key was just released; false otherwise.</returns>
        public static bool IsKeyJustReleased(Keys key)
        {
            return !currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }
        #endregion

        /// <summary>
        /// Get how much the cursor moved since last frame of update.
        /// </summary>
        /// <returns>The distance the mouse cursor have moved.</returns>
        public static Point GetDeltaMouseMovement()
        {
            return currentMouseState.Position - previousMouseState.Position;
        }

        /// <summary>
        /// Is any keyboard or mouse key pressed?
        /// </summary>
        /// <returns>true if any keyboard or mouse key is pressed; false otherwise.</returns>
        public static bool AnythingPressed()
        {
            return AnyKeyPressed() || AnyMousePressed();
        }

        /// <summary>
        /// Is any keyboard key pressed?
        /// </summary>
        /// <returns>true if any keyboard key is pressed; false otherwise.</returns>
        public static bool AnyKeyPressed()
        {
            return currentKeyboardState.GetPressedKeys().Length > 0;
        }

        /// <summary>
        /// Is any mouse key pressed?
        /// </summary>
        /// <returns>true if any mouse key is pressed; false otherwise.</returns>
        public static bool AnyMousePressed()
        {
            foreach (MouseButton key in Enum.GetValues(typeof(MouseButton)))
            {
                if (IsMousePressed(currentMouseState, key))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Get what mouse button is currently being pressed.
        /// </summary>
        /// <returns>The first mouse button found that was pressed.</returns>
        private static MouseButton GetPressedMouseButton()
        {
            foreach (MouseButton key in Enum.GetValues(typeof(MouseButton)))
            {
                if (IsMousePressed(currentMouseState, key))
                    return key;
            }

            return MouseButton.None;
        }

        /// <summary>
        /// Check if a given mouse button is being pressed in a given mouse state.
        /// </summary>
        /// <param name="state">The mouse state to use for checking if the mouse button is being pressed.</param>
        /// <param name="input">The mouse button to check.</param>
        /// <returns></returns>
        private static bool IsMousePressed(MouseState state, MouseButton input)
        {
            switch (input)
            {
                case MouseButton.LeftButton:
                return state.LeftButton == ButtonState.Pressed;
                case MouseButton.MiddleButton:
                return state.MiddleButton == ButtonState.Pressed;
                case MouseButton.RightButton:
                return state.RightButton == ButtonState.Pressed;
                case MouseButton.Button1:
                return state.XButton1 == ButtonState.Pressed;
                case MouseButton.Button2:
                return state.XButton2 == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        /// Check for any change on the keyboard and perform the right actions.
        /// </summary>
        private static void CheckForKeyboardChange()
        {
            if (AnyKeyPressed())
            {
                // KeyboardChanged event: Check if any key was pressed.
                KeyboardChanged?.Invoke(null, currentKeyboardState);

                // KeyPress event: 
                List<Keys> pressedKeys = currentKeyboardState.GetPressedKeys().ToList();
                KeyPressed?.Invoke(null, new KeyEventArgs(
                    pressedKeys[0],
                    currentKeyboardState.CapsLock,
                    pressedKeys.Contains(Keys.LeftShift) || pressedKeys.Contains(Keys.RightShift),
                    pressedKeys.Contains(Keys.LeftControl) || pressedKeys.Contains(Keys.RightControl),
                    pressedKeys.Contains(Keys.LeftAlt) || pressedKeys.Contains(Keys.RightAlt)));
            }
        }

        /// <summary>
        /// Check for any change on the mouse and perform the right actions.
        /// </summary>
        private static void CheckForMouseChange()
        {
            MouseButton theMouseButtonDown = GetPressedMouseButton();

            // MouseChanged event: Check if any mouse button was clicked.
            if (AnyMousePressed())
                MouseChanged?.Invoke(null, currentMouseState);

            // MouseDown event: Check if any mouse button was clicked.
            if (theMouseButtonDown != MouseButton.None)
            {
                MousePressed?.Invoke(null, new MouseEventArgs(theMouseButtonDown, currentMouseState.Position, currentMouseState.ScrollWheelValue));
            }

            // MousePositionChanged event: Check if the cursor moved and what mouse button was held while it was.
            Point cursorDelta = GetDeltaMouseMovement();
            if (cursorDelta.X != 0 && cursorDelta.Y != 0)
            {
                MousePositionChanged?.Invoke(null, new MousePositionEventArgs(theMouseButtonDown, currentMouseState.Position, previousMouseState.Position));
            }
        }
    }

    /// <summary>
    /// Represents a mouse button.
    /// </summary>
    public enum MouseButton
    {
        None,
        LeftButton,
        MiddleButton,
        RightButton,
        Button1,
        Button2
    }
}