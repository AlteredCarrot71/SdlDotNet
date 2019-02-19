#region License
/*
 * $RCSfile$
 * Copyright (C) 2005 David Hudson (jendave@yahoo.com)
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
#endregion License

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Input;

namespace SdlDotNet.Windows
{
    #region Class Documentation
    /// <summary>
    ///     Provides a simple Sdl Surface control allowing 
    ///     quick development of Windows Forms-based
    ///     Sdl Surface applications.
    /// </summary>
    #endregion Class Documentation
    [DefaultProperty("Image")]
    [ToolboxBitmap(typeof(Bitmap), "SurfaceControl.bmp")]
    public partial class SurfaceControl : System.Windows.Forms.PictureBox
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public SurfaceControl()
        {
            InitializeComponent();
            BlitRequestHandler = Blit;
        }

        #endregion

        #region Private fields

        int lastX;
        int lastY;

        #endregion

        #region Public Methods

        /// <summary>
        /// Copies surface to this surface
        /// </summary>
        /// <param name="surface">surface to copy onto control</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public void Blit(Surface surface)
        {
            if (surface == null)
            {
                throw new ArgumentNullException("surface");
            }
            try
            {
                if (this.InvokeRequired)
                {
                    Object[] args = { surface };
                    this.Invoke(BlitRequestHandler, args);
                }
                else
                {
                    this.Image = surface.Bitmap;
                }
            }
            catch (InvalidOperationException e)
            {
                e.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="surface"></param>
        public delegate void BlitEvent(Surface surface);
        private BlitEvent BlitRequestHandler;

        /// <summary>
        /// Copies surface onto this surface at a set position.
        /// </summary>
        /// <param name="sourceSurface"></param>
        /// <param name="destinationPosition"></param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public void Blit(Surface sourceSurface, System.Drawing.Point destinationPosition)
        {
            Surface destinationSurface = new Surface((Bitmap)this.Image);
            destinationSurface.Blit(sourceSurface, destinationPosition);
            this.Image = destinationSurface.Bitmap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void KeyPressed(KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void KeyReleased(KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Raises the OnResize event
        /// </summary>
        /// <param name="e">Contains the event data</param>
        protected override void OnResize(EventArgs e)
        {
            if (!this.DesignMode)
            {
                SdlDotNet.Core.Events.Add(new VideoResizeEventArgs(this.Width, this.Height));
            }
            base.OnResize(e);
        }

        /// <summary>
        /// Raises the SizeChanged event
        /// </summary>
        /// <param name="e">Contains the event data</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            if (!this.DesignMode)
            {
                SdlDotNet.Core.Events.Add(new VideoResizeEventArgs(this.Width, this.Height));
            }
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Raises the MouseDown event
        /// </summary>
        /// <param name="e">Contains the event data</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!this.DesignMode)
            {
                SdlDotNet.Core.Events.Add(new MouseButtonEventArgs(SurfaceControl.ConvertMouseButtons(e), true, (short)e.X, (short)e.Y));
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the MouseUp event
        /// </summary>
        /// <param name="e">Contains the event data</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!this.DesignMode)
            {
                SdlDotNet.Core.Events.Add(new MouseButtonEventArgs(SurfaceControl.ConvertMouseButtons(e), false, (short)e.X, (short)e.Y));
            }
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the MouseWheel Event
        /// </summary>
        /// <param name="e">Contains the event data</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!this.DesignMode)
            {
                SdlDotNet.Core.Events.Add(new MouseButtonEventArgs(SurfaceControl.ConvertMouseButtons(e), false, (short)e.X, (short)e.Y));
            }
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// Raises the MouseMove event
        /// </summary>
        /// <param name="e">Contains the event data</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!this.DesignMode)
            {
                //if (e.Button != MouseButtons.None)
                //{
                SdlDotNet.Core.Events.Add(new MouseMotionEventArgs(true, SurfaceControl.ConvertMouseButtons(e), (short)e.X, (short)e.Y, (short)(e.X - lastX), (short)(e.Y - lastY)));
                //}
                lastX = e.X;
                lastY = e.Y;
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the 
        /// <see cref="E:System.Windows.Forms.Control.KeyDown"/> event.
        /// </summary>
        /// <param name="e">A 
        /// <see cref="T:System.Windows.Forms.KeyEventArgs"/> 
        /// that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    SdlDotNet.Core.Events.Add(new KeyboardEventArgs((SdlDotNet.Input.Key)Enum.Parse(typeof(SdlDotNet.Input.Key), e.KeyCode.ToString()), (ModifierKeys)e.Modifiers, true));
                }
            }
            catch (ArgumentException ex)
            {
                ex.ToString();
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Raises the 
        /// <see cref="E:System.Windows.Forms.Control.KeyUp"/> 
        /// event.
        /// </summary>
        /// <param name="e">A 
        /// <see cref="T:System.Windows.Forms.KeyEventArgs"/> 
        /// that contains the event data.</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    SdlDotNet.Core.Events.Add(new KeyboardEventArgs((SdlDotNet.Input.Key)Enum.Parse(typeof(SdlDotNet.Input.Key), e.KeyCode.ToString()), (ModifierKeys)e.Modifiers, false));
                }
            }
            catch (ArgumentException ex)
            {
                ex.ToString();
            }
            base.OnKeyUp(e);
        }

        #endregion

        #region Private Methods

        private static MouseButton ConvertMouseButtons(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                return MouseButton.PrimaryButton;
            }
            else if (e.Button == MouseButtons.Right)
            {
                return MouseButton.SecondaryButton;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                return MouseButton.MiddleButton;
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                return MouseButton.WheelDown;
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                return MouseButton.WheelUp;
            }
            else
            {
                return MouseButton.None;
            }
        }

        #endregion
    }
}
