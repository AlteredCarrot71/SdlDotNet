#region LICENSE
/*
 * Copyright (C) 2004 - 2007 David Hudson (jendave@yahoo.com)
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
#endregion LICENSE

using System.Drawing;

//using Tao.Sdl;
using SdlDotNet.Graphics;

namespace SdlDotNet.Input
{
    #region ButtonKeyState

    /// <summary>
    /// ButtonKeyState
    /// </summary>
    /// <remarks>/remarks> byte</remarks>
    public enum ButtonKeyState
    {
        /// <summary>
        /// Button has been released.
        /// </summary>
        NotPressed = Sdl.SDL_RELEASED,
        /// <summary>
        /// Button has been pressed down.
        /// </summary>
        Pressed = Sdl.SDL_PRESSED
    }

    #endregion

    #region MouseButton

    /// <summary>
    /// MouseButton
    /// </summary>
    /// <remarks>Byte</remarks>
    public enum MouseButton
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Primary button. Typically the left button
        /// </summary>
        PrimaryButton = Sdl.SDL_BUTTON_LEFT,
        /// <summary>
        /// Secondary button. typically the right button
        /// </summary>
        SecondaryButton = Sdl.SDL_BUTTON_RIGHT,
        /// <summary>
        /// Middle button
        /// </summary>
        MiddleButton = Sdl.SDL_BUTTON_MIDDLE,
        /// <summary>
        /// Wheel up
        /// </summary>
        WheelUp = Sdl.SDL_BUTTON_WHEELUP,
        /// <summary>
        /// Wheel down
        /// </summary>
        WheelDown = Sdl.SDL_BUTTON_WHEELDOWN
    }

    #endregion

    /// <summary>
    /// Mouse.
    /// </summary>
    public static class Mouse
    {
        #region Private Fields

        static bool isInitialized = Video.Initialize();

        #endregion

        #region Public Methods


        /// <summary>
        /// 
        /// </summary>
        public static bool IsInitialized
        {
            get { return Mouse.isInitialized; }
        }

        /// <summary> 
        /// Gets and sets whether or not the mouse cursor is visible. 
        /// </summary> 
        public static bool ShowCursor
        {
            get
            {
                return (Sdl.SDL_ShowCursor(Sdl.SDL_QUERY) == Sdl.SDL_ENABLE);
            }
            set
            {
                Sdl.SDL_ShowCursor(value ? Sdl.SDL_ENABLE : Sdl.SDL_DISABLE);
            }
        }

        /// <summary> 
        /// Gets and sets the current mouse position. 
        /// </summary> 
        public static Point MousePosition
        {
            get
            {
                int x;
                int y;
                Sdl.SDL_GetMouseState(out x, out y);
                return new Point(x, y);
            }
            set
            {
                Sdl.SDL_WarpMouse((short)value.X, (short)value.Y);
            }
        }

        /// <summary> 
        /// Gets and sets the relative mouse position. 
        /// </summary> 
        public static Point MousePositionChange
        {
            get
            {
                int x;
                int y;
                Sdl.SDL_GetRelativeMouseState(out x, out y);
                return new Point(x, y);
            }
            set  // Change the relative mouse position 
            {
                Point mousePos = MousePosition;
                Sdl.SDL_WarpMouse((short)(mousePos.X + value.X),
                    (short)(mousePos.Y + value.Y));
            }
        }

        /// <summary>
        /// Returns true if app has mouse focus
        /// </summary>
        public static bool HasMouseFocus
        {
            get
            {
                return (Sdl.SDL_GetAppState() & Sdl.SDL_APPMOUSEFOCUS) != 0;
            }
        }

        /// <summary>
        /// Gets the pressed or released state of a mouse button
        /// </summary>
        /// <param name="button">The mouse button to check</param>
        /// <returns>
        /// If the button is pressed, returns True, otherwise returns False
        /// </returns>
        public static bool IsButtonPressed(MouseButton button)
        {
            int dummyX;
            int dummyY;
            return (Sdl.SDL_GetMouseState(out dummyX, out dummyY) & Sdl.SDL_BUTTON((byte)button)) != 0;
        }

        #endregion
    }
}
