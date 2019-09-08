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

using System;

using SdlDotNet.Core;
//using Tao.Sdl;

namespace SdlDotNet.Input
{
    #region JoystickAxis

    /// <summary>
    /// JoystickAxes
    /// </summary>
    /// <remarks></remarks>
    public enum JoystickAxis
    {
        /// <summary>
        /// Horizontal Axis
        /// </summary>
        Horizontal = 0,
        /// <summary>
        /// Vertical Axis
        /// </summary>
        Vertical = 1,
        /// <summary>
        /// For some controllers
        /// </summary>
        Axis3 = 2,
        /// <summary>
        /// For some controllers
        /// </summary>
        Axis4 = 3,
        /// <summary>
        /// For some controllers
        /// </summary>
        Axis5 = 4,
        /// <summary>
        /// For some controllers
        /// </summary>
        Axis6 = 5
    }

    #endregion

    #region JoystickHatStates

    /// <summary>
    /// JoystickHatStates
    /// </summary>
    /// <remarks></remarks>
    [FlagsAttribute]
    public enum JoystickHatStates
    {
        /// <summary>
        /// Centered state. FXCop was complaining that this should be called "None".
        /// </summary>
        None = Sdl.SDL_HAT_CENTERED,
        /// <summary>
        /// Up
        /// </summary>
        Up = Sdl.SDL_HAT_UP,
        /// <summary>
        /// Right
        /// </summary>
        Right = Sdl.SDL_HAT_RIGHT,
        /// <summary>
        /// Down
        /// </summary>
        Down = Sdl.SDL_HAT_DOWN,
        /// <summary>
        /// Left
        /// </summary>
        Left = Sdl.SDL_HAT_LEFT,
        /// <summary>
        /// Right and Up
        /// </summary>
        RightUp = Sdl.SDL_HAT_RIGHTUP,
        /// <summary>
        /// Right and Down
        /// </summary>
        RightDown = Sdl.SDL_HAT_RIGHTDOWN,
        /// <summary>
        /// Left and Up
        /// </summary>
        LeftUp = Sdl.SDL_HAT_LEFTUP,
        /// <summary>
        /// Left and Down
        /// </summary>
        LeftDown = Sdl.SDL_HAT_LEFTDOWN
    }

    #endregion

    /// <summary>
    /// Represents a joystick on the system
    /// </summary>
    public class Joystick : BaseSdlResource
    {
        #region Private fields

        private int index;
        private bool disposed;
        private const float JOYSTICK_ADJUSTMENT = 32768;
        private const float JOYSTICK_SCALE = 65535;
        static bool isInitialized = Joysticks.Initialize();

        #endregion

        #region Contructors

        /// <summary>
        /// open joystick at index number
        /// </summary>
        /// <param name="index"></param>
        public Joystick(int index)
        {
            if (Joysticks.IsValidJoystickNumber(index))
            {
                this.Handle = Sdl.SDL_JoystickOpen(index);
            }
            if (this.Handle == IntPtr.Zero)
            {
                throw SdlException.Generate();
            }
            else
            {
                this.index = index;
            }
        }
        internal Joystick(IntPtr handle)
        {
            this.Handle = handle;
            this.index = Sdl.SDL_JoystickIndex(handle);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Destroys the surface object and frees its memory
        /// </summary>
        /// <param name="disposing">True for manual disposing</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                    }
                    this.disposed = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Closes Joystick handle
        /// </summary>
        protected override void CloseHandle()
        {
            try
            {
                if (this.Handle != IntPtr.Zero)
                {
                    Sdl.SDL_JoystickClose(this.Handle);
                    this.Handle = IntPtr.Zero;
                }
            }
            catch (NullReferenceException)
            {
            }
            catch (AccessViolationException)
            {
            }
            finally
            {
                this.Handle = IntPtr.Zero;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public static bool IsInitialized
        {
            get { return Joystick.isInitialized; }
        }

        /// <summary>
        /// Gets the 0-based numeric index of this joystick
        /// </summary>
        public int Index
        {
            get
            {
                return this.index;
            }
        }

        /// <summary>
        /// Gets the number of axes on this joystick (usually 2 for each stick handle)
        /// </summary>
        public int NumberOfAxes
        {
            get
            {
                int result = Sdl.SDL_JoystickNumAxes(this.Handle);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Gets the number of trackballs on this joystick
        /// </summary>
        public int NumberOfBalls
        {
            get
            {
                int result = Sdl.SDL_JoystickNumBalls(this.Handle);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Gets the number of hats on this joystick
        /// </summary>
        public int NumberOfHats
        {
            get
            {
                int result = Sdl.SDL_JoystickNumHats(this.Handle);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Gets the number of buttons on this joystick
        /// </summary>
        public int NumberOfButtons
        {
            get
            {
                int result = Sdl.SDL_JoystickNumButtons(this.Handle);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Gets the name of this joystick
        /// </summary>
        public string Name
        {
            get
            {
                string result = Sdl.SDL_JoystickName(this.Index);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Gets the current axis position
        /// </summary>
        /// <param name="axis">Vertical or horizontal axis</param>
        /// <returns>Joystick position</returns>
        public float GetAxisPosition(JoystickAxis axis)
        {
            return ((float)(Sdl.SDL_JoystickGetAxis(this.Handle, (int)axis) + JOYSTICK_ADJUSTMENT) / JOYSTICK_SCALE);
        }

        /// <summary>
        /// Gets the ball motion
        /// </summary>
        /// <param name="ball">ball</param>
        /// <returns>Ballmotion struct</returns>
        public BallMotion GetBallMotion(int ball)
        {
            int motionX;
            int motionY;

            if (Sdl.SDL_JoystickGetBall(
                this.Handle, ball, out motionX, out motionY) ==
                (int)SdlFlag.Success)
            {
                return new BallMotion(motionX, motionY);
            }
            else
            {
                throw new SdlException();
            }
        }

        /// <summary>
        /// Gets the current button state
        /// </summary>
        /// <param name="button">Button to query</param>
        /// <returns>Pressed or not pressed</returns>
        public ButtonKeyState GetButtonState(int button)
        {
            return (ButtonKeyState)Sdl.SDL_JoystickGetButton(this.Handle, button);
        }

        /// <summary>
        /// Gets the current Hat state
        /// </summary>
        /// <param name="hat">Hat to query</param>
        /// <returns>Hat state</returns>
        public JoystickHatStates GetHatState(int hat)
        {
            return (JoystickHatStates)Sdl.SDL_JoystickGetHat(this.Handle, (int)hat);
        }

        #endregion
    }
}
