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
using System.Drawing;
using System.Reflection;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

using SdlDotNet.Input;
using SdlDotNet.Core;
using Tao.Sdl;

namespace SdlDotNet.Graphics
{
    #region OpenGLAttr
    /// <summary>
    /// Public enumeration for setting the OpenGL window Attributes
    /// </summary>
    /// <remarks>
    /// While you can set most OpenGL attributes normally, 
    /// the attributes list above must be known before SDL 
    /// sets the video mode.
    /// </remarks>
    public enum OpenGLAttr
    {
        /// <summary>
        /// Size of the framebuffer red component, in bits
        /// </summary>
        RedSize = Sdl.SDL_GL_RED_SIZE,
        /// <summary>
        /// Size of the framebuffer green component, in bits
        /// </summary>
        GreenSize = Sdl.SDL_GL_GREEN_SIZE,
        /// <summary>
        /// Size of the framebuffer blue component, in bits
        /// </summary>
        BlueSize = Sdl.SDL_GL_BLUE_SIZE,
        /// <summary>
        /// Size of the framebuffer alpha component, in bits
        /// </summary>
        AlphaSize = Sdl.SDL_GL_ALPHA_SIZE,
        /// <summary>
        /// Size of the framebuffer, in bits
        /// </summary>
        BufferSize = Sdl.SDL_GL_BUFFER_SIZE,
        /// <summary>
        /// 0 to disable or 1 to enable double buffering
        /// </summary>
        DoubleBuffer = Sdl.SDL_GL_DOUBLEBUFFER,
        /// <summary>
        /// Size of the depth buffer, in bits
        /// </summary>
        DepthSize = Sdl.SDL_GL_DEPTH_SIZE,
        /// <summary>
        /// Size of the stencil buffer, in bits.
        /// </summary>
        StencilSize = Sdl.SDL_GL_STENCIL_SIZE,
        /// <summary>
        /// Size of the accumulation buffer red component, in bits.
        /// </summary>
        AccumulationRedSize = Sdl.SDL_GL_ACCUM_RED_SIZE,
        /// <summary>
        /// Size of the accumulation buffer green component, in bits.
        /// </summary>
        AccumulationGreenSize = Sdl.SDL_GL_ACCUM_GREEN_SIZE,
        /// <summary>
        /// Size of the accumulation buffer blue component, in bits.
        /// </summary>
        AccumulationBlueSize = Sdl.SDL_GL_ACCUM_BLUE_SIZE,
        /// <summary>
        /// Size of the accumulation buffer alpha component, in bits.
        /// </summary>
        AccumulationAlphaSize = Sdl.SDL_GL_ACCUM_ALPHA_SIZE,
        /// <summary>
        /// Enable or disable stereo (left and right) buffers (0 or 1).
        /// </summary>
        StereoRendering = Sdl.SDL_GL_STEREO,
        /// <summary>
        /// Number of multisample buffers (0 or 1). 
        /// Requires the GL_ARB_MULTISAMPLE extension.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        MultiSampleBuffers = Sdl.SDL_GL_MULTISAMPLEBUFFERS,
        /// <summary>
        /// Number of samples per pixel when multisampling is enabled. 
        /// Requires the GL_ARB_MULTISAMPLE extension.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        MultiSampleSamples = Sdl.SDL_GL_MULTISAMPLESAMPLES,
        /// <summary>
        /// Guarantees hardware acceleration
        /// </summary>
        AcceleratedVisual = Sdl.SDL_GL_ACCELERATED_VISUAL,
        /// <summary>
        /// Waits for vsync in OpenGL applications.
        /// </summary>
        SwapControl = Sdl.SDL_GL_SWAP_CONTROL
    }
    #endregion OpenGLAttr

    #region VideoModes

    /// <summary>
    /// 
    /// </summary>
    [FlagsAttribute]
    internal enum VideoModes
    {
        /// <summary>
        /// Create the video surface in system memory
        /// </summary>
        None = Sdl.SDL_SWSURFACE,
        /// <summary>
        /// Create the video surface in video memory
        /// </summary>
        HardwareSurface = Sdl.SDL_HWSURFACE,
        /// <summary>
        /// Enables the use of asynchronous updates of the display surface. 
        /// This will usually slow down blitting on single CPU machines, 
        /// but may provide a speed increase on SMP systems.
        /// </summary>
        AsynchronousBlit = Sdl.SDL_ASYNCBLIT,

        //Anyformat = Sdl.SDL_ANYFORMAT,
        /// <summary>
        /// Give SDL.NET exclusive palette access. 
        /// </summary>
        HardwarePalette = Sdl.SDL_HWPALETTE,
        /// <summary>
        /// Enable hardware double buffering; only valid with <see cref="HardwareSurface"/>. 
        /// Calling Surface.Update() will flip the buffers and update the screen. 
        /// All drawing will take place on the surface that is not displayed 
        /// at the moment. If double buffering could not be enabled then 
        /// <see cref="Surface.Update()"/> will just update on the entire screen.
        /// </summary>
        DoubleBuffering = Sdl.SDL_DOUBLEBUF,
        /// <summary>
        /// SDL.NET will attempt to use a fullscreen mode. 
        /// If a hardware resolution change is not possible 
        /// (for whatever reason), the next higher resolution 
        /// will be used and the display window centered on a black background.
        /// </summary>
        FullScreen = Sdl.SDL_FULLSCREEN,
        /// <summary>
        /// Create an OpenGL rendering context. 
        /// You should have previously set OpenGL video 
        /// attributes with SDL_GL_SetAttribute.
        /// </summary>
        OpenGL = Sdl.SDL_OPENGL,
        /// <summary>
        /// Create a resizable window. When the window is resized by 
        /// the user a <see cref="Events.VideoResize"/> event is generated and 
        /// Video.SetVideoMode can be called again with the new size.
        /// </summary>
        Resizable = Sdl.SDL_RESIZABLE,
        /// <summary>
        /// If possible, NoFrame causes SDL to create a window with 
        /// no title bar or frame decoration. 
        /// Fullscreen modes automatically have this flag set.
        /// </summary>
        NoFrame = Sdl.SDL_NOFRAME
    }

    #endregion

    #region OverlayFormats

    /// <summary>
    /// 
    /// </summary>
    public enum OverlayFormat
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// Planar mode
        /// </summary>
        YV12 = Sdl.SDL_YV12_OVERLAY,
        /// <summary>
        /// Planar mode
        /// </summary>
        IYUV = Sdl.SDL_IYUV_OVERLAY,
        /// <summary>
        /// Packed mode
        /// </summary>
        YUY2 = Sdl.SDL_YUY2_OVERLAY,
        /// <summary>
        /// Packed mode
        /// </summary>
        UYVY = Sdl.SDL_UYVY_OVERLAY,
        /// <summary>
        /// Packed mode
        /// </summary>
        YVYU = Sdl.SDL_YVYU_OVERLAY
    }

    #endregion

    /// <summary>
    /// Provides methods to set the video mode, create video surfaces, 
    /// hide and show the mouse cursor,
    /// and interact with OpenGL
    /// </summary>
    public static class Video
    {
        #region Private fields

        const int USE_CURRENT_BPP = 0;
        static bool isInitialized = Initialize();

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public static bool IsInitialized
        {
            get { return Video.isInitialized; }
        }

        /// <summary>
        /// Closes and destroys this object
        /// </summary>
        public static void Close()
        {
            Events.CloseVideo();
        }

        /// <summary>
        /// Initializes Video subsystem.
        /// </summary>
        public static bool Initialize()
        {
            if (Sdl.SDL_Init(Sdl.SDL_INIT_VIDEO) != 0)
            {
                //throw SdlException.Generate();
                return false;
            }
            else
            {
                return true;
            }
        }

        //		/// <summary>
        //		/// Queries if the Video subsystem has been intialized.
        //		/// </summary>
        //		/// <remarks>
        //		/// </remarks>
        //		/// <returns>True if Video subsystem has been initialized, false if it has not.</returns>
        //		public static bool IsInitialized
        //		{
        //			get
        //			{
        //				if ((Sdl.SDL_WasInit(Sdl.SDL_INIT_VIDEO) & Sdl.SDL_INIT_VIDEO) 
        //					!= (int) SdlFlag.FalseValue)
        //				{
        //					return true;
        //				}
        //				else 
        //				{
        //					return false;
        //				}
        //			}
        //		}

        /// <summary>
        /// Checks if the requested video mode is supported
        /// </summary>
        /// <param name="width">Width of mode</param>
        /// <param name="height">Height of mode</param>
        /// <param name="fullScreen">Fullscreen or not</param>
        /// <param name="bitsPerPixel">
        /// Bits per pixel. Typically 8, 16, 24 or 32
        /// </param>
        /// <remarks></remarks>
        /// <returns>
        /// True is mode is supported, false if it is not.
        /// </returns>
        public static bool IsVideoModeOk(int width, int height, bool fullScreen, int bitsPerPixel)
        {
            VideoModes flags = VideoModes.None;
            if (fullScreen)
            {
                flags = VideoModes.FullScreen;
            }
            int result = Sdl.SDL_VideoModeOK(
                width,
                height,
                bitsPerPixel,
                (int)flags);
            if (result == bitsPerPixel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the application is active
        /// </summary>
        /// <returns>True is applications is active</returns>
        public static bool IsActive
        {
            get
            {
                return (Sdl.SDL_GetAppState() & (int)Focus.Application) != 0;
            }
        }

        /// <summary>
        /// Returns the highest bitsperpixel supported 
        /// for the given width and height
        /// </summary>
        /// <param name="width">Width of mode</param>
        /// <param name="height">Height of mode</param>
        /// <param name="fullScreen">Fullscreen mode</param>
        public static int BestBitsPerPixel(int width, int height, bool fullScreen)
        {
            VideoModes flags = VideoModes.None;
            if (fullScreen)
            {
                flags = VideoModes.FullScreen;
            }
            return Sdl.SDL_VideoModeOK(
                width,
                height,
                VideoInfo.BitsPerPixel,
                (int)flags);
        }

        /// <summary>
        /// Returns array of modes supported
        /// </summary>
        /// <returns>Array of Size structs</returns>
        public static Size[] ListModes()
        {
            VideoModes flags = VideoModes.FullScreen;
            IntPtr format = IntPtr.Zero;
            Sdl.SDL_Rect[] rects = Sdl.SDL_ListModes(format, (int)flags);
            Size[] size = new Size[rects.Length];
            for (int i = 0; i < rects.Length; i++)
            {
                size[i].Width = rects[i].w;
                size[i].Height = rects[i].h;
            }
            return size;
        }

        /// <summary>
        /// Sets the video mode of the application. 
        /// This overload is simply for initializing the video subsystem.
        /// It is useful when integrating with System.Windows.Forms
        /// </summary>
        /// <remarks>
        /// The surface will not have a height nor width.
        /// The surface will use the current bit depth.
        /// The surface will not be resizable.
        /// The surface will be an SDL surface for 2D drawing.
        /// The surface will be in windowed mode.
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode()
        {
            return Video.SetVideoMode(0, 0, USE_CURRENT_BPP, false, false, false, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>
        /// The surface will use the current bit depth.
        /// The surface will not be resizable.
        /// The surface will be an SDL surface for 2D drawing.
        /// The surface will be in windowed mode.
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height)
        {
            return SetVideoMode(width, height, USE_CURRENT_BPP, false, false, false, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>
        /// The surface will not be resizable.
        /// The surface will be an SDL surface for 2D drawing.
        /// The surface will be in windowed mode.
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="bitsPerPixel"></param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, int bitsPerPixel)
        {
            return SetVideoMode(width, height, bitsPerPixel, false, false, false, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>
        /// The surface will be an SDL surface for 2D drawing.
        /// The surface will be in windowed mode.
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <param name="width"></param>
        /// <param name="height">height</param>
        /// <param name="resizable">window will be resizable</param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, bool resizable)
        {
            return SetVideoMode(width, height, USE_CURRENT_BPP, resizable, false, false, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>
        /// The surface will be an SDL surface for 2D drawing.
        /// The surface will be in windowed mode.
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <param name="width"></param>
        /// <param name="height">height</param>
        /// <param name="bitsPerPixel">bits per pixel</param>
        /// <param name="resizable">window will be resizable</param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, int bitsPerPixel, bool resizable)
        {
            return SetVideoMode(width, height, bitsPerPixel, resizable, false, false, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>
        /// The surface will be in windowed mode.
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="resizable">window will be resizable</param>
        /// <param name="openGL">OpenGL surface</param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, bool resizable, bool openGL)
        {
            return SetVideoMode(width, height, USE_CURRENT_BPP, resizable, openGL, false, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>
        /// The surface will be in windowed mode.
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="bitsPerPixel">bits per pixel</param>
        /// <param name="resizable">window will be resizable</param>
        /// <param name="openGL">OpenGL surface</param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, int bitsPerPixel, bool resizable, bool openGL)
        {
            return SetVideoMode(width, height, bitsPerPixel, resizable, openGL, false, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="resizable">window will be resizable</param>
        /// <param name="openGL">OpenGL surface</param>
        /// <param name="fullScreen">fullscreen</param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, bool resizable, bool openGL, bool fullScreen)
        {
            return SetVideoMode(width, height, USE_CURRENT_BPP, resizable, openGL, fullScreen, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>
        /// The surface will have a frame unless it is fullscreen. 
        /// The surface will be a "software" surface. 
        /// Hardware surfaces are problematic and should only be used under certain circumstances.
        /// </remarks>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="bitsPerPixel">bits per pixel</param>
        /// <param name="resizable">window will be resizable</param>
        /// <param name="openGL">OpenGL surface</param>
        /// <param name="fullScreen">fullscreen</param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, int bitsPerPixel, bool resizable, bool openGL, bool fullScreen)
        {
            return SetVideoMode(width, height, bitsPerPixel, resizable, openGL, fullScreen, false, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>The surface will have a frame unless it is fullscreen.</remarks>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="resizable">window will be resizable</param>
        /// <param name="openGL">OpenGL surface</param>
        /// <param name="fullScreen">fullscreen</param>
        /// <param name="hardwareSurface"></param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, bool resizable, bool openGL, bool fullScreen, bool hardwareSurface)
        {
            return SetVideoMode(width, height, USE_CURRENT_BPP, resizable, openGL, fullScreen, hardwareSurface, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <remarks>The surface will have a frame unless it is fullscreen.</remarks>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="bitsPerPixel">bits per pixel</param>
        /// <param name="resizable">window will be resizable</param>
        /// <param name="openGL">OpenGL surface</param>
        /// <param name="fullScreen">fullscreen</param>
        /// <param name="hardwareSurface"></param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, int bitsPerPixel, bool resizable, bool openGL, bool fullScreen, bool hardwareSurface)
        {
            return SetVideoMode(width, height, bitsPerPixel, resizable, openGL, fullScreen, hardwareSurface, true);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="resizable">window will be resizable</param>
        /// <param name="openGL">OpenGL surface</param>
        /// <param name="fullScreen">fullscreen</param>
        /// <param name="hardwareSurface"></param>
        /// <param name="frame">
        /// If true, the window will have a frame around it. If fullscreen is true, then the frame will not appear
        /// </param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, bool resizable, bool openGL, bool fullScreen, bool hardwareSurface, bool frame)
        {
            return SetVideoMode(width, height, USE_CURRENT_BPP, resizable, openGL, fullScreen, hardwareSurface, frame);
        }

        /// <summary>
        /// Sets the video mode of the application
        /// </summary>
        /// <param name="width">screen width</param>
        /// <param name="height">screen height</param>
        /// <param name="bitsPerPixel">bits per pixel</param>
        /// <param name="resizable">window will be resizable</param>
        /// <param name="openGL">OpenGL surface</param>
        /// <param name="fullScreen">fullscreen</param>
        /// <param name="hardwareSurface"></param>
        /// <param name="frame">
        /// If true, the window will have a frame around it. If fullscreen is true, then the frame will not appear
        /// </param>
        /// <returns>a surface to draw to</returns>
        public static Surface SetVideoMode(int width, int height, int bitsPerPixel, bool resizable, bool openGL, bool fullScreen, bool hardwareSurface, bool frame)
        {
            VideoModes flags = VideoModes.None;
            if (hardwareSurface)
            {
                flags |= VideoModes.HardwareSurface;
                flags |= VideoModes.DoubleBuffering;
            }
            if (fullScreen)
            {
                flags |= VideoModes.FullScreen;
            }
            if (openGL)
            {
                flags |= VideoModes.OpenGL;
            }
            if (resizable)
            {
                flags |= VideoModes.Resizable;
            }
            if (!frame)
            {
                flags |= VideoModes.NoFrame;
            }
            return new Surface(Sdl.SDL_SetVideoMode(width, height, bitsPerPixel, (int)flags), true);
        }

        /// <summary>
        /// Gets the surface for the window or screen, 
        /// must be preceded by a call to SetVideoMode
        /// </summary>
        /// <returns>The main screen surface</returns>
        public static Surface Screen
        {
            get
            {
                return Surface.FromScreenPtr(Sdl.SDL_GetVideoSurface());
            }
        }

        /// <summary>
        /// Creates a new empty surface
        /// </summary>
        /// <param name="width">The width of the surface</param>
        /// <param name="height">The height of the surface</param>
        /// <param name="depth">The bits per pixel of the surface</param>
        /// <param name="redMask">
        /// A bitmask giving the range of red color values in the surface 
        /// pixel format
        /// </param>
        /// <param name="greenMask">
        /// A bitmask giving the range of green color values in the surface 
        /// pixel format
        /// </param>
        /// <param name="blueMask">
        /// A bitmask giving the range of blue color values in the surface 
        /// pixel format
        /// </param>
        /// <param name="alphaMask">
        /// A bitmask giving the range of alpha color values in the surface 
        /// pixel format
        /// </param>
        /// <param name="hardware">
        /// A flag indicating whether or not to attempt to place this surface
        ///  into video memory</param>
        /// <returns>A new surface</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public static Surface CreateRgbSurface(
            int width,
            int height,
            int depth,
            int redMask,
            int greenMask,
            int blueMask,
            int alphaMask,
            bool hardware)
        {
            return new Surface(Sdl.SDL_CreateRGBSurface(
                hardware ? (int)VideoModes.HardwareSurface : (int)VideoModes.None,
                width, height, depth,
                redMask, greenMask, blueMask, alphaMask));
        }

        /// <summary>
        /// Creates a new empty surface
        /// </summary>
        /// <param name="width">The width of the surface</param>
        /// <param name="height">The height of the surface</param>
        /// <returns>A new surface</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public static Surface CreateRgbSurface(int width, int height)
        {
            return Video.CreateRgbSurface(width, height, VideoInfo.BitsPerPixel, VideoInfo.RedMask, VideoInfo.GreenMask, VideoInfo.BlueMask, VideoInfo.AlphaMask, false);
        }

        /// <summary>
        /// Swaps the OpenGL screen, only if the double-buffered 
        /// attribute was set.
        /// Call this instead of Surface.Update() for OpenGL windows.
        /// </summary>
        public static void GLSwapBuffers()
        {
            Sdl.SDL_GL_SwapBuffers();
        }

        /// <summary>
        /// Sets an OpenGL attribute
        /// </summary>
        /// <param name="attribute">The attribute to set</param>
        /// <param name="attributeValue">The new attribute value</param>
        public static void GLSetAttribute(OpenGLAttr attribute, int attributeValue)
        {
            if (Sdl.SDL_GL_SetAttribute((int)attribute, attributeValue) != 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Gets the value of an OpenGL attribute
        /// </summary>
        /// <param name="attribute">The attribute to get</param>
        /// <returns>The current attribute value</returns>
        public static int GLGetAttribute(OpenGLAttr attribute)
        {
            int returnValue;
            if (Sdl.SDL_GL_GetAttribute((int)attribute, out returnValue) != 0)
            {
                throw SdlException.Generate();
            }
            return returnValue;
        }

        /// <summary>
        /// Gets or sets the size of the GL red framebuffer.
        /// </summary>
        /// <value>The size of the GL red framebuffer.</value>
        public static int GLRedFrameBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.RedSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.RedSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL green framebuffer.
        /// </summary>
        /// <value>The size of the GL green framebuffer.</value>
        public static int GLGreenFrameBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.GreenSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.GreenSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL blue framebuffer.
        /// </summary>
        /// <value>The size of the GL blue framebuffer.</value>
        public static int GLBlueFrameBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.BlueSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.BlueSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL alpha framebuffer.
        /// </summary>
        /// <value>The size of the GL alpha framebuffer.</value>
        public static int GLAlphaFrameBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.AlphaSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.AlphaSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL framebuffer.
        /// </summary>
        /// <value>The size of the GL framebuffer.</value>
        public static int GLFrameBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.BufferSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.BufferSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL depth.
        /// </summary>
        /// <value>The size of the GL depth.</value>
        public static int GLDepthSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.DepthSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.DepthSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL stencil.
        /// </summary>
        /// <value>The size of the GL stencil.</value>
        public static int GLStencilSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.StencilSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.StencilSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL red accumulation buffer.
        /// </summary>
        /// <value>The size of the GL red accumulation buffer.</value>
        public static int GLRedAccumulationBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.AccumulationRedSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.AccumulationRedSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL green accumulation buffer.
        /// </summary>
        /// <value>The size of the GL green accumulation buffer.</value>
        public static int GLGreenAccumulationBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.AccumulationGreenSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.AccumulationGreenSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL blue accumulation buffer.
        /// </summary>
        /// <value>The size of the GL blue accumulation buffer.</value>
        public static int GLBlueAccumulationBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.AccumulationBlueSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.AccumulationBlueSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the GL alpha accumulation buffer.
        /// </summary>
        /// <value>The size of the GL alpha accumulation buffer.</value>
        public static int GLAlphaAccumulationBufferSize
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.AccumulationAlphaSize);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.AccumulationAlphaSize, value);
            }
        }

        /// <summary>
        /// Gets or sets the GL stereo rendering.
        /// </summary>
        /// <value>The GL stereo rendering.</value>
        public static int GLStereoRendering
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.StereoRendering);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.StereoRendering, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is GL double buffer enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is GL double buffer enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool GLDoubleBufferEnabled
        {
            get
            {
                int result = Video.GLGetAttribute(OpenGLAttr.DoubleBuffer);
                if (result == 1)
                {
                    return true;
                }
                else if (result == 0)
                {
                    return false;
                }
                else
                {
                    throw new SdlException(Events.StringManager.GetString(
                        "GLGetAttributeImproperResult", CultureInfo.CurrentUICulture));
                }
            }
            set
            {
                if (value == true)
                {
                    Video.GLSetAttribute(OpenGLAttr.DoubleBuffer, 1);
                }
                else
                {
                    Video.GLSetAttribute(OpenGLAttr.DoubleBuffer, 0);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is GL stereo rendering enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is GL stereo rendering enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool GLStereoRenderingEnabled
        {
            get
            {
                int result = Video.GLGetAttribute(OpenGLAttr.StereoRendering);
                if (result == 1)
                {
                    return false;
                }
                else if (result == 0)
                {
                    return true;
                }
                else
                {
                    throw new SdlException(Events.StringManager.GetString(
                        "GLGetAttributeImproperResult", CultureInfo.CurrentUICulture));
                }
            }
            set
            {
                if (value == true)
                {
                    Video.GLSetAttribute(OpenGLAttr.StereoRendering, 0);
                }
                else
                {
                    Video.GLSetAttribute(OpenGLAttr.StereoRendering, 1);
                }
            }
        }

        /// <summary>
        /// Gets or sets the GL multi sample buffers.
        /// </summary>
        /// <value>The GL multi sample buffers.</value>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public static int GLMultiSampleBuffers
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.MultiSampleBuffers);
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 1)
                {
                    value = 1;
                }
                Video.GLSetAttribute(OpenGLAttr.MultiSampleBuffers, value);
            }
        }

        /// <summary>
        /// Gets or sets the GL multi sample samples.
        /// </summary>
        /// <value>The GL multi sample samples.</value>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public static int GLMultiSampleSamples
        {
            get
            {
                return Video.GLGetAttribute(OpenGLAttr.MultiSampleSamples);
            }
            set
            {
                Video.GLSetAttribute(OpenGLAttr.MultiSampleSamples, value);
            }
        }

        /// <summary>
        /// gets or sets the text for the current window
        /// </summary>
        public static string WindowCaption
        {
            get
            {
                string ret;
                string dummy;

                Sdl.SDL_WM_GetCaption(out ret, out dummy);
                return ret;
            }
            set
            {
                Sdl.SDL_WM_SetCaption(value, "");
            }
        }

        /// <summary>
        /// sets the icon for the current window
        /// </summary>
        /// <param name="icon">the surface containing the image</param>
        /// <remarks>This should be called before Video.SetVideoMode</remarks>
        public static void WindowIcon(BaseSdlResource icon)
        {
            if (icon == null)
            {
                throw new ArgumentNullException("icon");
            }
            Video.Initialize();
            Sdl.SDL_WM_SetIcon(icon.Handle, null);
        }

        /// <summary>
        /// sets the icon for the current window
        /// </summary>
        /// <remarks>
        /// On OS X, this method returns nothing since OS X does not use window icons.
        /// </remarks>
        /// <param name="icon">Icon to use</param>
        /// <remarks>This should be called before Video.SetVideoMode</remarks>
        public static void WindowIcon(Icon icon)
        {
            if (icon == null)
            {
                throw new ArgumentNullException("icon");
            }
            try
            {
                Bitmap bitmap = icon.ToBitmap();
                Surface surface = new Surface(bitmap);
                surface.TransparentColor = Color.Empty;
                WindowIcon(surface);
            }
            catch (SdlException e)
            {
                e.ToString();
                return;
            }
        }

        /// <summary>
        /// Sets the icon for the current window to an icon in the given assembly's embedded resources.
        /// </summary>
        /// <param name="assembly">The assembly where the icon resource is held.</param>
        /// <param name="iconName">The name of the icon (e.g. &quot;App.ico&quot;).</param>
        public static void WindowIcon(Assembly assembly, string iconName)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            foreach (string s in assembly.GetManifestResourceNames())
            {
                if (s.EndsWith(iconName))
                {
                    Video.WindowIcon(new Icon(assembly.GetManifestResourceStream(s)));
                    break;
                }
            }
        }

        /// <summary>
        /// Sets the icon for the current window to &quot;App.ico&quot; in the given assembly's embedded resources.
        /// </summary>
        /// <param name="assembly">The assembly where &quot;App.ico&quot; is held.</param>
        public static void WindowIcon(Assembly assembly)
        {
            Video.WindowIcon(assembly, "App.ico");
        }

        /// <summary>
        /// Sets the icon for the current window. 
        /// This method assumes there is an embedded 
        /// resource named &quot;App.ico&quot;.
        /// </summary>
        /// <remarks>This should be called before Video.SetVideoMode</remarks>
        public static void WindowIcon()
        {
            Video.WindowIcon(Assembly.GetCallingAssembly(), "App.ico");
        }

        /// <summary>
        /// Gets the default SDL.NET icon
        /// </summary>
        /// <returns></returns>
        public static Icon DefaultIcon
        {
            get
            {
                return new Icon(Assembly.GetCallingAssembly().GetManifestResourceStream("App.ico"));
            }
        }

        /// <summary>
        /// Minimizes the current window
        /// </summary>
        /// <returns>True if the action succeeded, otherwise False</returns>
        public static bool Hide()
        {
            return Video.IconifyWindow();
        }

        /// <summary>
        /// Iconifies (minimizes) the current window
        /// </summary>
        /// <returns>True if the action succeeded, otherwise False</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public static bool IconifyWindow()
        {
            return (Sdl.SDL_WM_IconifyWindow() != (int)SdlFlag.Success);
        }

        /// <summary>
        /// Forces keyboard focus and prevents the mouse from leaving the window
        /// </summary>
        public static bool GrabInput
        {
            get
            {
                return (Sdl.SDL_WM_GrabInput(Sdl.SDL_GRAB_QUERY) == Sdl.SDL_GRAB_ON);
            }
            set
            {
                if (value)
                {
                    Sdl.SDL_WM_GrabInput(Sdl.SDL_GRAB_ON);
                }
                else
                {
                    Sdl.SDL_WM_GrabInput(Sdl.SDL_GRAB_OFF);
                }
            }
        }

        /// <summary>
        /// Returns video driver name
        /// </summary>
        public static string VideoDriver
        {
            get
            {
                string buffer = string.Empty;
                return Sdl.SDL_VideoDriverName(buffer, 100);
            }
        }

        /// <summary>
        /// Sets gamma
        /// </summary>
        /// <param name="red">Red</param>
        /// <param name="green">Green</param>
        /// <param name="blue">Blue</param>
        public static void Gamma(float red, float green, float blue)
        {
            int result = Sdl.SDL_SetGamma(red, green, blue);
            if (result != 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Sets gamma for all colors
        /// </summary>
        /// <param name="gammaValue">Gamma to set for all colors</param>
        public static void Gamma(float gammaValue)
        {
            int result = Sdl.SDL_SetGamma(gammaValue, gammaValue, gammaValue);
            if (result != 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Gets red gamma ramp
        /// </summary>
        public static short[] GetGammaRampRed()
        {
            short[] red = new short[256];
            int result = Sdl.SDL_GetGammaRamp(red, null, null);
            if (result != 0)
            {
                throw SdlException.Generate();
            }
            return red;
        }

        /// <summary>
        /// Sets red gamma ramp
        /// </summary>
        /// <param name="gammaArray"></param>
        public static void SetGammaRampRed(short[] gammaArray)
        {
            int result = Sdl.SDL_SetGammaRamp(gammaArray, null, null);
            if (result != 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Gets blue gamma ramp
        /// </summary>
        public static short[] GetGammaRampBlue()
        {
            short[] blue = new short[256];
            int result = Sdl.SDL_GetGammaRamp(null, null, blue);
            if (result != 0)
            {
                throw SdlException.Generate();
            }
            return blue;
        }

        /// <summary>
        /// Sets blue gamma ramp
        /// </summary>
        /// <param name="gammaArray"></param>
        public static void SetGammaRampBlue(short[] gammaArray)
        {
            int result = Sdl.SDL_SetGammaRamp(null, null, gammaArray);
            if (result != 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Gets green gamma ramp
        /// </summary>
        public static short[] GetGammaRampGreen()
        {
            short[] green = new short[256];
            int result = Sdl.SDL_GetGammaRamp(null, green, null);
            if (result != 0)
            {
                throw SdlException.Generate();
            }
            return green;
        }

        /// <summary>
        /// Sets green gamma ramp
        /// </summary>
        /// <param name="gammaArray"></param>
        public static void SetGammaRampGreen(short[] gammaArray)
        {
            int result = Sdl.SDL_SetGammaRamp(null, gammaArray, null);
            if (result != 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Update entire screen
        /// </summary>
        public static void Update()
        {
            Video.Screen.Update();
        }

        /// <summary>
        /// Updates rectangle
        /// </summary>
        /// <param name="rectangle">
        /// Rectangle to update
        /// </param>
        public static void Update(System.Drawing.Rectangle rectangle)
        {
            Video.Screen.Update(rectangle);
        }

        /// <summary>
        /// Update an array of rectangles
        /// </summary>
        /// <param name="rectangles">
        /// Array of rectangles to update
        /// </param>
        public static void Update(System.Drawing.Rectangle[] rectangles)
        {
            Video.Screen.Update(rectangles);
        }

        /// <summary>
        /// This returns the platform window handle for the SDL window.
        /// </summary>
        /// <remarks>
        /// TODO: The Unix SysWMinfo struct has not been finished. 
        /// This only runs on Windows right now.
        /// </remarks>
        public static IntPtr WindowHandle
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                if ((p == 4) || (p == 128))
                {
                    Sdl.SDL_SysWMinfo wmInfo;
                    Sdl.SDL_GetWMInfo(out wmInfo);
                    return new IntPtr(wmInfo.data);
                }
                else
                {
                    Sdl.SDL_SysWMinfo_Windows wmInfo;
                    Sdl.SDL_GetWMInfo(out wmInfo);
                    return new IntPtr(wmInfo.window);
                }
            }
        }

        #endregion
    }
}
