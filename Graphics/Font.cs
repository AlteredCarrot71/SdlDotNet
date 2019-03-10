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

using SdlDotNet.Core;
using Tao.Sdl;

namespace SdlDotNet.Graphics
{
    #region Styles

    /// <summary>
    /// Text Style
    /// </summary>
    /// <remarks></remarks>
    [FlagsAttribute]
    public enum Styles
    {
        /// <summary>
        /// Normal.
        /// </summary>
        /// <remarks>
        /// FXCop wants this to be called 'None' instead of 'Normal'
        /// </remarks>
        None = SdlTtf.TTF_STYLE_NORMAL,
        /// <summary>
        /// Bold
        /// </summary>
        Bold = SdlTtf.TTF_STYLE_BOLD,
        /// <summary>
        /// Italic
        /// </summary>
        Italic = SdlTtf.TTF_STYLE_ITALIC,
        /// <summary>
        /// Underline
        /// </summary>
        Underline = SdlTtf.TTF_STYLE_UNDERLINE
    }

    #endregion

    /// <summary>
    /// Font Class.
    /// </summary>
    /// <remarks>
    /// This class is used to instantiate fonts in an SDL.NET application.
    /// </remarks>
    public class Font : BaseSdlResource
    {
        #region Private fields

        private bool disposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Font Constructor
        /// </summary>
        /// <param name="fileName">Font filename</param>
        /// <param name="pointSize">Size of font</param>
        public Font(string fileName, int pointSize)
        {
            if (!Font.IsFontSystemInitialized)
            {
                Font.InitializeFontSystem();
            }

            this.Handle = SdlTtf.TTF_OpenFont(fileName, pointSize);
            if (this.Handle == IntPtr.Zero)
            {
                throw FontException.Generate();
            }
        }

        /// <summary>
        /// Create a Font from a byte array in memory.
        /// </summary>
        /// <param name="array">A array of byte that should be the font data</param>
        /// <param name="pointSize">Size of font</param>
        public Font(byte[] array, int pointSize)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (!Font.IsFontSystemInitialized)
            {
                Font.InitializeFontSystem();
            }

            this.Handle = SdlTtf.TTF_OpenFontRW(Sdl.SDL_RWFromMem(array, array.Length), 0, pointSize);
            if (this.Handle == IntPtr.Zero)
            {
                throw FontException.Generate();
            }
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Queries if the Font subsystem has been intialized.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>True if Font subsystem has been initialized, false if it has not.</returns>
        private static bool IsFontSystemInitialized
        {
            get
            {

                if (SdlTtf.TTF_WasInit() == (int)SdlFlag.TrueValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Initialize Font subsystem.
        /// </summary>
        private static void InitializeFontSystem()
        {
            if (SdlTtf.TTF_Init() != (int)SdlFlag.Success)
            {
                FontException.Generate();
            }
        }

        /// <summary>
        /// Render Text to Solid
        /// </summary>
        /// <param name="textItem">String to display</param>
        /// <param name="color">Color of text</param>
        /// <returns>Surface containing the text</returns>
        private Surface RenderTextSolid(string textItem, Color color)
        {
            Sdl.SDL_Color colorSdl = SdlColor.ConvertColor(color);
            return new Surface(SdlTtf.TTF_RenderUNICODE_Solid(this.Handle, textItem, colorSdl));
        }

        /// <summary>
        /// Shade text
        /// </summary>
        /// <param name="textItem"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="textColor"></param>
        /// <returns></returns>
        private Surface RenderTextShaded(
            string textItem, Color textColor, Color backgroundColor)
        {
            Sdl.SDL_Color textColorSdl =
                SdlColor.ConvertColor(textColor);
            Sdl.SDL_Color backgroundColorSdl =
                SdlColor.ConvertColor(backgroundColor);
            if (textItem == null || textItem.Length == 0)
            {
                textItem = " ";
            }
            return new Surface(SdlTtf.TTF_RenderUNICODE_Shaded(
                this.Handle, textItem, textColorSdl, backgroundColorSdl));
        }

        /// <summary>
        /// Blended Text
        /// </summary>
        /// <param name="textColor"></param>
        /// <param name="textItem"></param>
        /// <returns></returns>
        private Surface RenderTextBlended(
            string textItem, Color textColor)
        {
            Sdl.SDL_Color colorSdl = SdlColor.ConvertColor(textColor);
            if (textItem == null || textItem.Length == 0)
            {
                textItem = " ";
            }
            return new Surface(SdlTtf.TTF_RenderUNICODE_Blended(
                this.Handle, textItem, colorSdl));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get System Font Names
        /// </summary>
        /// <returns></returns>
        public static System.Drawing.Text.FontCollection SystemFontNames
        {
            get
            {
                return new System.Drawing.Text.InstalledFontCollection();
            }
        }

        /// <summary>
        /// Gets and sets the font style of the text.
        /// </summary>
        public Styles Style
        {
            set
            {
                SdlTtf.TTF_SetFontStyle(this.Handle, (int)value);
                GC.KeepAlive(this);
            }
            get
            {
                Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                GC.KeepAlive(this);
                return style;
            }
        }

        /// <summary>
        /// Gets and sets whether or not the font is bold.
        /// </summary>
        public bool Bold
        {
            set
            {
                if (value)
                {
                    Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                    SdlTtf.TTF_SetFontStyle(this.Handle, (int)style | (int)Styles.Bold);
                    GC.KeepAlive(this);
                }
                else
                {
                    Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                    if ((int)(style & Styles.Bold) != (int)SdlFlag.FalseValue)
                    {
                        SdlTtf.TTF_SetFontStyle(this.Handle, (int)style ^ (int)Styles.Bold);
                    }
                    GC.KeepAlive(this);
                }
            }
            get
            {
                Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                GC.KeepAlive(this);
                if ((int)(style & Styles.Bold) == (int)SdlFlag.FalseValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets and sets whether or not the font is italicized.
        /// </summary>
        public bool Italic
        {
            set
            {
                if (value == true)
                {
                    Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                    SdlTtf.TTF_SetFontStyle(this.Handle, (int)style | (int)Styles.Italic);
                    GC.KeepAlive(this);
                }
                else
                {
                    Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                    if ((int)(style & Styles.Italic) != (int)SdlFlag.FalseValue)
                    {
                        SdlTtf.TTF_SetFontStyle(this.Handle, (int)style ^ (int)Styles.Italic);
                    }
                    GC.KeepAlive(this);
                }
            }
            get
            {
                Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                GC.KeepAlive(this);
                if ((int)(style & Styles.Italic) == (int)SdlFlag.FalseValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets and sets whether the font is underlined.
        /// </summary>
        public bool Underline
        {
            set
            {
                if (value == true)
                {
                    Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                    SdlTtf.TTF_SetFontStyle(this.Handle, (int)style | (int)Styles.Underline);
                    GC.KeepAlive(this);
                }
                else
                {
                    Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                    if ((int)(style & Styles.Underline) != (int)SdlFlag.FalseValue)
                    {
                        SdlTtf.TTF_SetFontStyle(this.Handle, (int)style ^ (int)Styles.Underline);
                    }
                    GC.KeepAlive(this);
                }
            }
            get
            {
                Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                GC.KeepAlive(this);
                if ((int)(style & Styles.Underline) == (int)SdlFlag.FalseValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets and sets whether the font style is not underlined, italic or bold.
        /// </summary>
        public bool Normal
        {
            set
            {
                if (value == true)
                {
                    SdlTtf.TTF_SetFontStyle(this.Handle, (int)Styles.None);
                    GC.KeepAlive(this);
                }
            }
            get
            {
                Styles style = (Styles)SdlTtf.TTF_GetFontStyle(this.Handle);
                GC.KeepAlive(this);
                if ((int)(style | Styles.Underline | Styles.Bold | Styles.Italic) == (int)SdlFlag.TrueValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets the height of the font.
        /// </summary>
        public int Height
        {
            get
            {
                int result = SdlTtf.TTF_FontHeight(this.Handle);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Ascent Property
        /// </summary>
        public int Ascent
        {
            get
            {
                int result = SdlTtf.TTF_FontAscent(this.Handle);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Descent Property
        /// </summary>
        public int Descent
        {
            get
            {
                int result = SdlTtf.TTF_FontDescent(this.Handle);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Line Size property
        /// </summary>
        public int LineSize
        {
            get
            {
                int result = SdlTtf.TTF_FontLineSkip(this.Handle);
                GC.KeepAlive(this);
                return result;
            }
        }

        /// <summary>
        /// Gets the size of the given text in the current font.
        /// </summary>
        /// <param name="textItem">String to display</param>
        /// <returns>The size of the text in pixels.</returns>
        public Size SizeText(string textItem)
        {
            int width;
            int height;

            SdlTtf.TTF_SizeUNICODE(this.Handle, textItem, out width, out height);
            GC.KeepAlive(this);
            return new Size(width, height);
        }

        /// <summary>
        /// Render text to a surface.
        /// </summary>
        /// <param name="textItem">String to display</param>
        /// <param name="antiAlias">If true, text will be anti-aliased</param>
        /// <param name="textColor">Color of text</param>
        /// <returns>Surface with text</returns>
        public Surface Render(string textItem, Color textColor, bool antiAlias)
        {
            return this.Render(textItem, textColor, Color.Empty, antiAlias, 0, 0);
        }

        /// <summary>
        /// Render text to a surface with a background color
        /// </summary>
        /// <param name="textItem">String to display</param>
        /// <param name="textColor">Color of text</param>
        /// <param name="backgroundColor">Color of background</param>
        /// <returns>Surface with text</returns>
        public Surface Render(string textItem, Color textColor, Color backgroundColor)
        {
            return this.Render(textItem, textColor, backgroundColor, true, 0, 0);
        }

        /// <summary>
        /// Render Text to a surface
        /// </summary>
        /// <param name="textItem">Text string</param>
        /// <param name="textColor">Color of text</param>
        /// <returns>Surface with text</returns>
        public Surface Render(string textItem, Color textColor)
        {
            return this.Render(textItem, textColor, Color.Empty, true, 0, 0);
        }

        /// <summary>
        /// Render text to a surface with a background color
        /// </summary>
        /// <param name="textItem">String to display</param>
        /// <param name="textColor">Color of text</param>
        /// <param name="backgroundColor">Color of background</param>
        /// <param name="antiAlias"></param>
        /// <returns>Surface with text</returns>
        public Surface Render(string textItem, Color textColor, Color backgroundColor, bool antiAlias)
        {
            return this.Render(textItem, textColor, backgroundColor, antiAlias, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textItem"></param>
        /// <param name="textColor"></param>
        /// <param name="antiAlias"></param>
        /// <param name="textWidth"></param>
        /// <param name="maxLines"></param>
        /// <returns></returns>
        public Surface Render(string textItem, Color textColor, bool antiAlias, int textWidth, int maxLines)
        {
            return Render(textItem, textColor, Color.Empty, antiAlias, textWidth, maxLines);
        }

        // c# doesn't allow the optional paramter - everything has to be passed 
        // textWidth default = 0   - no wrapping (otherwise it is the number of pixels to count as the textWidth) 
        // maxLines default = 0   - render all lines (otherwise it is the maximum number of lines to render) 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textItem"></param>
        /// <param name="textColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="antiAlias"></param>
        /// <param name="textWidth"></param>
        /// <param name="maxLines"></param>
        /// <returns></returns>
        public Surface Render(string textItem, Color textColor, Color backgroundColor, bool antiAlias, int textWidth, int maxLines)
        {
            if (textItem == null)
            {
                throw new ArgumentNullException("textItem");
            }
            int x = 0;
            int y = 0;
            int tempWidth = 0;
            int stringlength;
            int stringpos;
            int countback;
            int newX;
            string templine;
            int countLines;
            string[] splitline;

            splitline = textItem.Replace("\r", string.Empty).Split('\n');
            for (int k = 0; k <= splitline.GetUpperBound(0); k++)
            {
                splitline[k] = splitline[k].Trim();
            }

            if (textWidth == 0 && splitline.Length == 1)
            {
                if (backgroundColor.IsEmpty)
                {
                    if (antiAlias)
                    {
                        return this.RenderTextBlended(textItem, textColor);
                    }
                    else
                    {
                        return this.RenderTextSolid(textItem, textColor);
                    }
                }
                else
                {
                    return this.RenderTextShaded(textItem, textColor, backgroundColor);
                }
            }
            else
            {

                Surface surfFinal;
                Surface blitSurf;

                // it is faster to set aside a large amount of temporary surface 
                // than recreate a new one every time the surface is to expand. 

                // if no textWidth is given, then set the textWidth to ludicrusly high 
                // in all cases, set the height to an impossible high number. 

                // a possible improvement would be to pass in the the temporary 
                // textWidth & height for performance 
                Surface surfTemp;
                if (textWidth > 0)
                {
                    surfTemp = new Surface(textWidth, 10000);
                }
                else
                {
                    surfTemp = new Surface(10000, 10000);
                }

                // the height of fonts are always the same from what I can tell 
                // no matter which letter is used. It's the widths that cause trouble 
                int fontHeight = this.SizeText(" ").Height;

                countLines = 1;
                for (int k = 0; k <= splitline.GetUpperBound(0); k++)
                {
                    // no word wrap, only newline wrap 
                    if (textWidth == 0)
                    {
                        if (maxLines == 0 || countLines <= maxLines)
                        {
                            if (!String.IsNullOrEmpty(splitline[k]))
                            {
                                blitSurf = this.Render(splitline[k], textColor, backgroundColor, antiAlias);
                            }
                            else
                            {
                                blitSurf = new Surface(1, 1);
                            }
                            if (blitSurf.Width > tempWidth)
                            {
                                tempWidth = blitSurf.Width;
                            }
                            surfTemp.Blit(blitSurf, new Point(x, y));
                            blitSurf.Dispose();
                            blitSurf = null;   // doing this reuses the surface memory :) 
                            y = y + fontHeight;
                            countLines = countLines + 1;
                        }
                        textWidth = tempWidth;
                    }
                    // word wrapping & new line wrapping 
                    else
                    {
                        stringpos = 0;
                        //indented = true; 
                        if (String.IsNullOrEmpty(splitline[k]))
                        {
                            y = y + fontHeight;
                        }
                        while (stringpos < splitline[k].Length)
                        {
                            if (maxLines == 0 || countLines <= maxLines)
                            {
                                stringlength = 1;
                                templine = splitline[k].Substring(stringpos, stringlength);

                                // this is the secret: keep checking the width of the string to blit, adding 
                                // one letter at a time, until the width has been hit 
                                while (this.SizeText(templine).Width <= textWidth & (stringpos + stringlength < splitline[k].Length))
                                {
                                    stringlength = stringlength + 1;
                                    templine = splitline[k].Substring(stringpos, stringlength);
                                }
                                countback = 0;
                                // now count backwards, until a space is found 
                                while (!(templine.EndsWith(" ")) & (stringpos + stringlength < splitline[k].Length))
                                {
                                    countback = countback + 1;
                                    if (countback >= splitline[k].Length)
                                    {
                                        countback = 0;
                                        templine = splitline[k].Substring(stringpos, stringlength);
                                        break;
                                    }
                                    templine = stringlength - countback > 0 ? templine.Substring(0, stringlength - countback) : "";
                                }

                                // move the current string position forward 
                                stringpos = stringpos + stringlength - countback;
                                //if (indented) 
                                //{ 
                                //	newX = x + indent; 
                                //} 
                                //else 
                                //{ 
                                newX = x;
                                //} 
                                // render the wrapped line 
                                blitSurf = this.Render(templine, textColor, backgroundColor, antiAlias);
                                surfTemp.Blit(blitSurf, new Point(newX, y));
                                blitSurf.Dispose();
                                blitSurf = null;
                                y = y + fontHeight;
                                countLines = countLines + 1;
                            }
                            else
                            {
                                stringpos = stringpos + 1;
                            }
                            //indented = false; 
                        }
                    }
                }
                surfFinal = surfTemp.CreateSurfaceFromClipRectangle(new Rectangle(0, 0, textWidth, y));
                surfTemp.Dispose();
                surfTemp = null;
                return surfFinal;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Destroys the surface object and frees its memory
        /// </summary>
        /// <param name="disposing">If true, it will dispose all objects</param>
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
        /// Closes Surface handle
        /// </summary>
        protected override void CloseHandle()
        {
            try
            {
                if (this.Handle != IntPtr.Zero)
                {
                    SdlTtf.TTF_CloseFont(this.Handle);
                    this.Handle = IntPtr.Zero;
                }
            }
            catch (System.NullReferenceException e)
            {
                e.ToString();
            }
            catch (AccessViolationException e)
            {
                Console.WriteLine(e.StackTrace);
                e.ToString();
            }
            finally
            {
                this.Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}
