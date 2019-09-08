#region LICENSE
/*
 * $RCSfile$
 * Copyright (C) 2006 - 2007 David Hudson (jendave@yahoo.com)
 * Copyright (C) 2007 Jonathan Porter (jono.porter@gmail.com)
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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

//using Tao.Sdl;

namespace SdlDotNet.Graphics
{
    /// <summary>
    /// The texture minifying function is used whenever
    /// the	pixel being textured maps to an	area greater
    /// than one texture element. There are	six defined
    /// minifying functions.  Two of them use the nearest
    /// one	or nearest four	texture	elements to compute
    /// the	texture	value. The other four use mipmaps.
    /// 
    /// A mipmap is	an ordered set of arrays representing
    /// the	same image at progressively lower resolutions.
    /// If the texture has dimensions 2nx2m, there are
    /// max(n,m)+1 mipmaps.	The first mipmap is the
    /// original texture, with dimensions 2nx2m. Each
    /// subsequent mipmap has dimensions 2k-1x2l-1,	where
    /// 2kx2l are the dimensions of	the previous mipmap,
    /// until either k=0 or	l=0.  At that point,
    /// subsequent mipmaps have dimension 1x2l-1 or	2k-1x1
    /// until the final mipmap, which has dimension	1x1.
    /// To define the mipmaps, call	glTexImage1D,
    /// glTexImage2D, glCopyTexImage1D, or
    /// glCopyTexImage2D with the level argument
    /// indicating the order of the	mipmaps.  Level	0 is
    /// the	original texture; level	max(n,m) is the	final
    /// 1x1	mipmap.
    /// </summary>
    public enum MinifyingOption : int
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// Returns the value	of the texture element
        ///	that is nearest (in Manhattan distance)
        ///	to the center of the pixel being
        ///	textured.
        /// </summary>
        Nearest = ((int)0x2600), //Gl.GL_NEAREST,
        /// <summary>
        /// Returns the weighted average of the four
        /// texture elements that are closest	to the
        /// center of the pixel being textured.
        /// These can include border texture
        /// elements, depending on the values	of
        /// GL_TEXTURE_WRAP_S and GL_TEXTURE_WRAP_T,
        /// and on the exact mapping.
        /// </summary>
        Linear = ((int)0x2601), //Gl.GL_LINEAR,
        /// <summary>
        /// Chooses the mipmap that most closely
        /// matches the size of the pixel being
        /// textured and uses the GL_NEAREST
        /// criterion (the texture element nearest
        /// to the center of the pixel) to produce a
        /// texture value.
        /// </summary>
        NearestMipMapNearest = ((int)0x2700), //Gl.GL_NEAREST_MIPMAP_NEAREST,
        /// <summary>
        /// Chooses the mipmap that most closely
        /// matches the size of the pixel being
        /// textured and uses the GL_LINEAR
        /// criterion (a weighted average of the
        /// four texture elements that are closest
        /// to the center of the pixel) to produce a
        /// texture value.
        /// </summary>
        LinearMipMapNearest = ((int)0x2701), //Gl.GL_LINEAR_MIPMAP_NEAREST,
        /// <summary>
        /// Chooses the two mipmaps that most
        /// closely match the	size of	the pixel
        /// being textured and uses the GL_NEAREST
        /// criterion	(the texture element nearest
        /// to the center of the pixel) to produce a
        /// texture value from each mipmap. The
        /// final texture value is a weighted
        /// average of those two values.
        /// </summary>
        NearestMipMapLinear = ((int)0x2702), //Gl.GL_NEAREST_MIPMAP_LINEAR,
        /// <summary>
        /// Chooses the two mipmaps that most
        /// closely match the size of the pixel
        /// being textured and uses the GL_LINEAR
        /// criterion (a weighted average of the
        /// four texture elements that are closest
        /// to the center of the pixel) to produce a
        /// texture value from each mipmap. The
        /// final texture value is a weighted
        /// average of those two values.
        /// </summary>
        LinearMipMapLinear = ((int)0x2703), //Gl.GL_LINEAR_MIPMAP_LINEAR
    }
    /// <summary>
    /// The texture magnification function is used when
    /// the	pixel being textured maps to an	area less than
    /// or equal to	one texture element.  It sets the
    /// texture magnification function to either
    /// GL_NEAREST or GL_LINEAR. GL_NEAREST is
    /// generally faster than GL_LINEAR, but it can
    /// produce textured images with sharper edges because
    /// the	transition between texture elements is not as
    /// smooth. The	initial	value of GL_TEXTURE_MAG_FILTER
    /// is GL_LINEAR.
    /// </summary>
    public enum MagnificationOption : int
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// Returns the value	of the texture element
        ///	that is nearest (in Manhattan distance)
        ///	to the center of the pixel being
        ///	textured.
        /// </summary>
        Nearest = ((int)0x2600), //Gl.GL_NEAREST,
        /// <summary>
        /// Returns the weighted average of the four
        /// texture elements that are closest	to the
        /// center of the pixel being textured.
        /// These can include border texture
        /// elements, depending on the values	of
        /// GL_TEXTURE_WRAP_S and GL_TEXTURE_WRAP_T,
        /// and on the exact mapping.
        /// </summary>
        Linear = ((int)0x2601) //Gl.GL_LINEAR
    }
    /// <summary>
    /// The wrap parameter for a texture coordinate
    /// </summary>
    public enum WrapOption : int
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// Causes texture coordinates to be clamped to the range [0,1] and
        /// is useful for preventing wrapping artifacts	when
        /// mapping a single image onto	an object.
        /// </summary>
        Clamp = ((int)0x2900), //Gl.GL_CLAMP,
        /// <summary>
        /// Causes texture coordinates to loop around so to remain in the 
        /// range [0,1] where 1.5 would be .5. this is useful for repeating
        /// a texture for a tiled floor.
        /// </summary>
        Repeat = ((int)0x2901) //Gl.GL_REPEAT
    }

    /// <summary>
    /// Loads a Surface into a OpenGl Texture.   
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Correct Spelling")]
    public class SurfaceGl : IDisposable
    {
        #region Static
        /// <summary>
        /// 
        /// </summary>
        const int GL_ENABLE_BIT = ((int)0x00002000);
        /// <summary>
        /// 
        /// </summary>
        const int GL_DEPTH_TEST = ((int)0x0B71);
        /// <summary>
        /// 
        /// </summary>
        const int GL_QUADS = ((int)0x0007);
        /// <summary>
        /// 
        /// </summary>
        const int GL_CULL_FACE = ((int)0x0B44);
        /// <summary>
        /// 
        /// </summary>
        const int GL_TEXTURE_2D = ((int)0x0DE1);
        /// <summary>
        /// 
        /// </summary>
        const int GL_BLEND = ((int)0x0BE2);
        /// <summary>
        /// 
        /// </summary>
        const int GL_ONE_MINUS_SRC_ALPHA = ((int)0x0303);
        /// <summary>
        /// 
        /// </summary>
        const int GL_SRC_ALPHA = ((int)0x0302);
        /// <summary>
        /// 
        /// </summary>
        const int GL_PROJECTION = ((int)0x1701);
        /// <summary>
        /// 
        /// </summary>
        const int GL_TEXTURE_ENV = ((int)0x2300);
        /// <summary>
        /// 
        /// </summary>
        const int GL_TEXTURE_ENV_MODE = ((int)0x2200);
        /// <summary>
        /// 
        /// </summary>
        const int GL_DECAL = ((int)0x2101);
        /// <summary>
        /// 
        /// </summary>
        const int GL_MODELVIEW = ((int)0x1700);
        /// <summary>
        /// 
        /// </summary>
        const int GL_TEXTURE_MIN_FILTER = ((int)0x2801);
        /// <summary>
        /// 
        /// </summary>
        const int GL_TEXTURE_MAG_FILTER = ((int)0x2800);
        /// <summary>
        /// 
        /// </summary>
        const int GL_TEXTURE_WRAP_T = ((int)0x2803);
        /// <summary>
        /// 
        /// </summary>
        const int GL_TEXTURE_WRAP_S = ((int)0x2802);
        /// <summary>
        /// 
        /// </summary>
        const int GL_RGBA = ((int)0x1908);
        /// <summary>
        /// 
        /// </summary>
        const int GL_UNSIGNED_BYTE = ((int)0x1401);

        static bool mode2D;

        /// <summary>
        /// 
        /// </summary>
        public static bool Mode2D
        {
            get
            {
                return mode2D;
            }
            set
            {
                if (value)
                {
                    glPushAttrib(GL_ENABLE_BIT);
                    glDisable(GL_DEPTH_TEST);
                    glDisable(GL_CULL_FACE);
                    glEnable(GL_TEXTURE_2D);

                    /* This allows alpha blending of 2D textures with the scene */
                    glEnable(GL_BLEND);
                    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

                    glViewport(0, 0, Video.Screen.Width, Video.Screen.Height);

                    glMatrixMode(GL_PROJECTION);
                    glPushMatrix();
                    glLoadIdentity();

                    glOrtho(0.0, (double)Video.Screen.Width, (double)Video.Screen.Height, 0.0, 0.0, 1.0);

                    glMatrixMode(GL_MODELVIEW);
                    glPushMatrix();
                    glLoadIdentity();

                    glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_DECAL);
                    mode2D = value;
                }
                else
                {
                    glDisable(GL_BLEND);
                    glEnable(GL_DEPTH_TEST);
                    glMatrixMode(GL_MODELVIEW);
                    glPopMatrix();
                    glMatrixMode(GL_PROJECTION);
                    glPopMatrix();
                    glPopAttrib();
                    mode2D = value;
                }
            }
        }

        private static bool IsMipMap(MinifyingOption option)
        {
            return option != MinifyingOption.Linear && option != MinifyingOption.Nearest;
        }

        #endregion

        #region Fields
        Surface surface;
        bool isFlipped;
        int textureId;
        int textureWidth;
        int textureHeight;
        float widthRatio;
        float heightRatio;

        bool needRefresh;
        bool needSetOptions;
        MinifyingOption minifyingFilter;
        MagnificationOption magnificationFilter;
        WrapOption wrapS;
        WrapOption wrapT;
        #endregion

        delegate void glLoadIndentityDelegate();
        static glLoadIndentityDelegate glLoadIdentity = (glLoadIndentityDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glLoadIdentity"), typeof(glLoadIndentityDelegate));

        delegate void glBindTextureDeledate(int target, int texture);
        static glBindTextureDeledate glBindTexture = (glBindTextureDeledate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glBindTexture"), typeof(glBindTextureDeledate));

        delegate void glTexParameteriDelegate(int target, int pname, int param);
        static glTexParameteriDelegate glTexParameteri = (glTexParameteriDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glTexParameteri"), typeof(glTexParameteriDelegate));

        delegate int glIsTextureDelegate(int texture);
        static glIsTextureDelegate glIsTexture = (glIsTextureDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glIsTexture"), typeof(glIsTextureDelegate));

        delegate void glBeginDelegate(int mode);
        static glBeginDelegate glBegin = (glBeginDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glBegin"), typeof(glBeginDelegate));

        delegate void glEndDelegate();
        static glEndDelegate glEnd = (glEndDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glEnd"), typeof(glEndDelegate));

        delegate void glTexCoord2fDelegate(float s, float t);
        static glTexCoord2fDelegate glTexCoord2f = (glTexCoord2fDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glTexCoord2f"), typeof(glTexCoord2fDelegate));

        delegate void glVertex2fDelegate(float x, float y);
        static glVertex2fDelegate glVertex2f = (glVertex2fDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glVertex2f"), typeof(glVertex2fDelegate));

        delegate void glPushAttribDelegate(int mask);
        static glPushAttribDelegate glPushAttrib = (glPushAttribDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glPushAttrib"), typeof(glPushAttribDelegate));

        delegate void glPopAttribDelegate();
        static glPopAttribDelegate glPopAttrib = (glPopAttribDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glPopAttrib"), typeof(glPopAttribDelegate));

        delegate void glDisableDelegate(int cap);
        static glDisableDelegate glDisable = (glDisableDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glDisable"), typeof(glDisableDelegate));

        delegate void glEnableDelegate(int cap);
        static glEnableDelegate glEnable = (glEnableDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glEnable"), typeof(glEnableDelegate));

        delegate void glBlendFuncDelegate(int sfactor, int dfactor);
        static glBlendFuncDelegate glBlendFunc = (glBlendFuncDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glBlendFunc"), typeof(glBlendFuncDelegate));

        delegate void glViewportDelegate(int x, int y, int width, int height);
        static glViewportDelegate glViewport = (glViewportDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glViewport"), typeof(glViewportDelegate));

        delegate void glMatrixModeDelegate(int mode);
        static glMatrixModeDelegate glMatrixMode = (glMatrixModeDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glMatrixMode"), typeof(glMatrixModeDelegate));

        delegate void glPopMatrixDelegate();
        static glPopMatrixDelegate glPopMatrix = (glPopMatrixDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glPopMatrix"), typeof(glPopMatrixDelegate));

        delegate void glPushMatrixDelegate();
        static glPushMatrixDelegate glPushMatrix = (glPushMatrixDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glPushMatrix"), typeof(glPushMatrixDelegate));

        delegate void glTexEnvfDelegate(int target, int pname, float param);
        static glTexEnvfDelegate glTexEnvf = (glTexEnvfDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glTexEnvf"), typeof(glTexEnvfDelegate));

        delegate void glOrthoDelegate(double left, double right, double bottom, double top, double zNear, double zFar);
        static glOrthoDelegate glOrtho = (glOrthoDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glOrtho"), typeof(glOrthoDelegate));

        delegate void glTexImage2DDelegate(int target, int level, int internalformat, int width, int height, int border, int format, int type, IntPtr pixels);
        static glTexImage2DDelegate glTexImage2D = (glTexImage2DDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glTexImage2D"), typeof(glTexImage2DDelegate));

        delegate void glGenTexturesDelegate(int n, int[] textures);
        static glGenTexturesDelegate glGenTextures = (glGenTexturesDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glGenTextures"), typeof(glGenTexturesDelegate));

        delegate void glDeleteTexturesDelegate(int n, int[] textures);
        static glDeleteTexturesDelegate glDeleteTextures = (glDeleteTexturesDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glDeleteTextures"), typeof(glDeleteTexturesDelegate));

        #region Constructors
        //static SurfaceGl()
        //{
            //glLoadIdentity = (glLoadIndentityDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glLoadIdentity"), typeof(glLoadIndentityDelegate));
            //glBindTexture = (glBindTextureDeledate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glBindTexture"), typeof(glBindTextureDeledate));
            //glTexParameteri = (glTexParameteriDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glTexParameteri"), typeof(glTexParameteriDelegate));
            //glIsTexture = (glIsTextureDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glIsTexture"), typeof(glIsTextureDelegate));
            //glBegin = (glBeginDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glBegin"), typeof(glBeginDelegate));
            //glEnd = (glEndDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glEnd"), typeof(glEndDelegate));
            //glTexCoord2f = (glTexCoord2fDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glTexCoord2f"), typeof(glTexCoord2fDelegate));
            //glVertex2f = (glVertex2fDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glVertex2f"), typeof(glVertex2fDelegate));
            //glPushAttrib = (glPushAttribDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glPushAttrib"), typeof(glPushAttribDelegate));
            //glPopAttrib = (glPopAttribDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glPopAttrib"), typeof(glPopAttribDelegate));
            //glDisable = (glDisableDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glDisable"), typeof(glDisableDelegate));
            //glEnable = (glEnableDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glEnable"), typeof(glEnableDelegate));
            //glBlendFunc = (glBlendFuncDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glBlendFunc"), typeof(glBlendFuncDelegate));
            //glViewport = (glViewportDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glViewport"), typeof(glViewportDelegate));
            //glMatrixMode = (glMatrixModeDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glMatrixMode"), typeof(glMatrixModeDelegate));
            //glPushMatrix = (glPushMatrixDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glPushMatrix"), typeof(glPushMatrixDelegate));
            //glPopMatrix = (glPopMatrixDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glPopMatrix"), typeof(glPopMatrixDelegate));
            //glTexEnvf = (glTexEnvfDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glTexEnvf"), typeof(glTexEnvfDelegate));
            //glOrtho = (glOrthoDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glOrtho"), typeof(glOrthoDelegate));
            //glTexImage2D = (glTexImage2DDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glTexImage2D"), typeof(glTexImage2DDelegate));
            //glGenTextures = (glGenTexturesDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glGenTextures"), typeof(glGenTexturesDelegate));
            //glDeleteTextures = (glDeleteTexturesDelegate)Marshal.GetDelegateForFunctionPointer(Sdl.SDL_GL_GetProcAddress("glDeleteTextures"), typeof(glDeleteTexturesDelegate));
        //}
        /// <summary>
        /// Creates a new Instance of SurfaceGl.
        /// </summary>
        /// <param name="surface">The surface to be copied into a OpenGl Texture.</param>
        public SurfaceGl(Surface surface)
            : this(surface, true)
        { }

        /// <summary>
        /// Creates a new Instance of SurfaceGl.
        /// </summary>
        /// <param name="surface">The surface to be copied into a OpenGl Texture.</param>
        /// <param name="isFlipped">States if the surface should be flipped when copied into a OpenGl Texture.</param>
        public SurfaceGl(Surface surface, bool isFlipped)
        {
            if (surface == null)
            {
                throw new ArgumentNullException("surface");
            }
            this.surface = surface;
            this.isFlipped = isFlipped;
            this.textureId = -1;
            this.textureWidth = -1;
            this.textureHeight = -1;
            this.widthRatio = -1;
            this.heightRatio = -1;
            this.minifyingFilter = MinifyingOption.Linear;
            this.magnificationFilter = MagnificationOption.Linear;
            this.wrapS = WrapOption.Repeat;
            this.wrapT = WrapOption.Repeat;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets and Sets the surface the Texture is made from.
        /// </summary>
        public Surface Surface
        {
            get { return surface; }
            set
            {
                if (value == null) { throw new ArgumentNullException("value"); }
                if (surface != value)
                {
                    surface = value;
                    needRefresh = true;
                }
            }
        }

        /// <summary>
        /// Gets the Width of the texture.
        /// </summary>
        public int TextureWidth
        {
            get { Check(); return textureWidth; }
        }

        /// <summary>
        /// Gets the Height of the texture.
        /// </summary>
        public int TextureHeight
        {
            get { Check(); return textureHeight; }
        }

        /// <summary>
        /// Gets the Percent of the OpenGl Texture the original Surface is utilizing along it's Width.
        /// </summary>
        public float WidthRatio
        {
            get { Check(); return widthRatio; }
        }

        /// <summary>
        /// Gets the Percent of the OpenGl Texture the original Surface  is utilizing along it's Height.
        /// </summary>
        public float HeightRatio
        {
            get { Check(); return heightRatio; }
        }

        /// <summary>
        /// Gets the OpenGl Texture Name.
        /// </summary>
        public int TextureId
        {
            get { Check(); return textureId; }
        }

        /// <summary>
        /// Gets and Sets if the texture is Flipped.
        /// </summary>
        public bool IsFlipped
        {
            get { return isFlipped; }
            set
            {
                if (isFlipped ^ value)
                {
                    isFlipped = value;
                    needRefresh = true;
                }
            }
        }

        /// <summary>
        /// Gets and Sets 
        /// The texture minifying function is used whenever
        /// the	pixel being textured maps to an	area greater
        /// than one texture element. There are	six defined
        /// minifying functions.  Two of them use the nearest
        /// one	or nearest four	texture	elements to compute
        /// the	texture	value. The other four use mipmaps.
        /// 
        /// A mipmap is	an ordered set of arrays representing
        /// the	same image at progressively lower resolutions.
        /// If the texture has dimensions 2nx2m, there are
        /// max(n,m)+1 mipmaps.	The first mipmap is the
        /// original texture, with dimensions 2nx2m. Each
        /// subsequent mipmap has dimensions 2k-1x2l-1,	where
        /// 2kx2l are the dimensions of	the previous mipmap,
        /// until either k=0 or	l=0.  At that point,
        /// subsequent mipmaps have dimension 1x2l-1 or	2k-1x1
        /// until the final mipmap, which has dimension	1x1.
        /// To define the mipmaps, call	glTexImage1D,
        /// glTexImage2D, glCopyTexImage1D, or
        /// glCopyTexImage2D with the level argument
        /// indicating the order of the	mipmaps.  Level	0 is
        /// the	original texture; level	max(n,m) is the	final
        /// 1x1	mipmap.
        /// </summary>
        public MinifyingOption MinFilter
        {
            get { return minifyingFilter; }
            set
            {
                if (minifyingFilter != value)
                {
                    if (IsMipMap(minifyingFilter) ^ IsMipMap(value))
                    {
                        needRefresh = true;
                    }
                    else
                    {
                        needSetOptions = true;
                    }
                    minifyingFilter = value;
                }
            }
        }

        /// <summary>
        /// Gets and Sets 
        /// The texture magnification function is used when
        /// the	pixel being textured maps to an	area less than
        /// or equal to	one texture element.  It sets the
        /// texture magnification function to either
        /// GL_NEAREST or GL_LINEAR. GL_NEAREST is
        /// generally faster than GL_LINEAR, but it can
        /// produce textured images with sharper edges because
        /// the	transition between texture elements is not as
        /// smooth. The	initial	value of GL_TEXTURE_MAG_FILTER
        /// is GL_LINEAR.
        /// </summary>
        public MagnificationOption MagnificationFilter
        {
            get { return magnificationFilter; }
            set
            {
                if (magnificationFilter != value)
                {
                    magnificationFilter = value;
                    needSetOptions = true;
                }
            }
        }

        /// <summary>
        /// Gets and Sets
        /// The wrap parameter for texture coordinate s
        /// to either GL_CLAMP or GL_REPEAT.  GL_CLAMP causes
        /// s coordinates to be	clamped	to the range [0,1] and
        /// is useful for preventing wrapping artifacts	when
        /// mapping a single image onto	an object. GL_REPEAT
        /// causes the integer part of the s coordinate	to be
        /// ignored; the GL uses only the fractional part,
        /// thereby creating a repeating pattern. Border
        /// texture elements are accessed only if wrapping is
        /// set	to GL_CLAMP.  Initially, GL_TEXTURE_WRAP_S is
        /// set	to GL_REPEAT.
        /// </summary>
        public WrapOption WrapS
        {
            get { return wrapS; }
            set
            {
                if (wrapS != value)
                {
                    wrapS = value;
                    needSetOptions = true;
                }
            }
        }

        /// <summary>
        /// Gets and Sets
        /// The wrap parameter for	texture	coordinate t
        /// to either GL_CLAMP or GL_REPEAT.  GL_CLAMP causes
        /// t coordinates to be	clamped	to the range [0,1] and
        /// is useful for preventing wrapping artifacts	when
        /// mapping a single image onto	an object. GL_REPEAT
        /// causes the integer part of the s coordinate	to be
        /// ignored; the GL uses only the fractional part,
        /// thereby creating a repeating pattern. Border
        /// texture elements are accessed only if wrapping is
        /// set	to GL_CLAMP.  Initially, GL_TEXTURE_WRAP_T is
        /// set	to GL_REPEAT.
        /// </summary>
        public WrapOption WrapT
        {
            get { return wrapT; }
            set
            {
                if (wrapT != value)
                {
                    wrapT = value;
                    needSetOptions = true;
                }
            }
        }

        #endregion

        #region Methods

        private void Check()
        {
            if (needRefresh || textureId <= 0)
            {
                Refresh();
            }
            else if (needSetOptions)
            {
                BindOptions();
            }
        }

        private void BindOptions()
        {
            glBindTexture(GL_TEXTURE_2D, textureId);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, (int)minifyingFilter);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, (int)magnificationFilter);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, (int)wrapS);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, (int)wrapT);
            needSetOptions = false;
        }

        private Surface TransformSurface(bool isFlipped)
        {
            byte alpha = surface.Alpha;
            Surface textureSurface2 = null;
            surface.Alpha = 0;
            try
            {
                Surface textureSurface = surface;
                if (isFlipped)
                {
                    textureSurface = textureSurface.CreateFlippedVerticalSurface();
                    textureSurface2 = textureSurface;
                    textureSurface2.Alpha = 0;
                }
                return textureSurface.CreateResizedSurface();
            }
            finally
            {
                surface.Alpha = alpha;
                if (isFlipped)
                {
                    textureSurface2.Dispose();
                }
            }
        }

        /// <summary>
        /// Deletes the texture from OpenGl memory.
        /// </summary>
        public void Delete()
        {
            if (glIsTexture(this.textureId) != 0)
            {
                int[] texId = new int[] { textureId };
                glDeleteTextures(1, texId);
            }
            this.textureId = -1;
            this.textureWidth = -1;
            this.textureHeight = -1;
            this.widthRatio = -1;
            this.heightRatio = -1;
        }

        /// <summary>
        /// Reloads the OpenGl Texture from the Surface.
        /// </summary>
        public void Refresh()
        {
            Refresh(this.surface, this.isFlipped, this.minifyingFilter, this.magnificationFilter, this.wrapS, this.wrapT);
        }

        /// <summary>
        /// Reloads the OpenGl Texture from the Surface.
        /// </summary>
        /// <param name="surface">The surface to load from.</param>
        /// <param name="isFlipped">States if the surface should be flipped when moved into the OpenGl Texture.</param>
        public void Refresh(Surface surface, bool isFlipped)
        {
            Refresh(surface, isFlipped, this.minifyingFilter, this.magnificationFilter, this.wrapS, this.wrapT);
        }

        /// <summary>
        /// Reloads the OpenGl Texture from the Surface.
        /// </summary>
        /// <param name="surface">The surface to load from.</param>
        /// <param name="isFlipped">States if the surface should be flipped when moved into the OpenGl Texture.</param>
        /// <param name="minifyingFilter">"The openGl filter used for minifying"</param>
        /// <param name="magnificationFilter">"The openGl filter used for magnification"</param>
        /// <param name="wrapS">The wrap parameter for texture coordinate S</param>
        /// <param name="wrapT">The wrap parameter for texture coordinate T</param>
        public void Refresh(Surface surface, bool isFlipped, MinifyingOption minifyingFilter, MagnificationOption magnificationFilter, WrapOption wrapS, WrapOption wrapT)
        {
            if (surface == null) { throw new ArgumentNullException("surface"); }
            this.surface = surface;
            this.Delete();
            using (Surface textureSurface = TransformSurface(isFlipped))
            {
                int[] textures = new int[1];
                glGenTextures(1, textures);
                this.textureId = textures[0];

                this.textureWidth = textureSurface.Width;
                this.textureHeight = textureSurface.Height;
                this.isFlipped = isFlipped;
                this.minifyingFilter = minifyingFilter;
                this.magnificationFilter = magnificationFilter;
                this.wrapS = wrapS;
                this.wrapT = wrapT;
                this.widthRatio = (float)surface.Width / textureWidth;
                this.heightRatio = (float)surface.Height / textureHeight;

                glBindTexture(GL_TEXTURE_2D, textureId);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, (int)minifyingFilter);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, (int)magnificationFilter);

                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, (int)wrapS);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, (int)wrapT);



                if (minifyingFilter == MinifyingOption.Linear || minifyingFilter == MinifyingOption.Nearest)
                {
                    glTexImage2D(GL_TEXTURE_2D, 0, textureSurface.BytesPerPixel, textureWidth, textureHeight, 0, GL_RGBA, GL_UNSIGNED_BYTE, textureSurface.Pixels);
                }
                else
                {
                    NativeMethods.gluBuild2DMipmaps(GL_TEXTURE_2D, textureSurface.BytesPerPixel, textureWidth, textureHeight, GL_RGBA, GL_UNSIGNED_BYTE, textureSurface.Pixels);
                }

                needRefresh = false;
                needSetOptions = false;
            }
        }

        /// <summary>
        /// Draws the Texture.
        /// </summary>
        public void Draw()
        {
            this.Draw(0, 0, surface.Width, surface.Height);
        }

        /// <summary>
        /// Draws the Texture.
        /// </summary>
        /// <param name="location">The offset for the Texture.</param>
        public void Draw(Point location)
        {
            Draw(location.X, location.Y, surface.Width, surface.Height);
        }

        /// <summary>
        /// Draws the Texture.
        /// </summary>
        /// <param name="locationX">The x offset for the Texture.</param>
        /// <param name="locationY">The y offset for the Texture.</param>
        public void Draw(float locationX, float locationY)
        {
            Draw(locationX, locationY, surface.Width, surface.Height);
        }

        /// <summary>
        /// Draws the Texture.
        /// </summary>
        /// <param name="rectangle">the rectagle where the texture will be drawn.</param>
        public void Draw(Rectangle rectangle)
        {
            Draw(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// Draws the Texture.
        /// </summary>
        /// <param name="locationX">The x offset for the Texture.</param>
        /// <param name="locationY">The y offset for the Texture.</param>
        /// <param name="width">The width fo the area where the Texture will be drawn.</param>
        /// <param name="height">The height fo the area where the Texture will be drawn.</param>
        public void Draw(float locationX, float locationY, float width, float height)
        {
            Check();
            glBindTexture(GL_TEXTURE_2D, this.textureId);
            glBegin(GL_QUADS);
            glTexCoord2f(0, heightRatio);
            glVertex2f(locationX, locationY);
            glTexCoord2f(widthRatio, heightRatio);
            glVertex2f(locationX + width, locationY);
            glTexCoord2f(widthRatio, 0);
            glVertex2f(locationX + width, locationY + height);
            glTexCoord2f(0, 0);
            glVertex2f(locationX, locationY + height);
            glEnd();
        }

        /// <summary>
        ///  Disposes the Object by freeing all OpenGl memory allocated to it.
        /// </summary>
        /// <param name="disposing">States if it is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Delete();
            }
        }

        /// <summary>
        /// Disposes the Object by freeing all OpenGl memory allocated to it.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
