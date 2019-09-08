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
using System.Globalization;

using SdlDotNet.Core;
//using Tao.Sdl;

namespace SdlDotNet.Graphics.Primitives
{
    /// <summary>
    /// Box primitive
    /// </summary>
    public struct Box : IPrimitive
    {
        short x1;
        short y1;
        short x2;
        short y2;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x1">X position of upper-left point</param>
        /// <param name="y1">Y position of upper-left point</param>
        /// <param name="x2">X position of lower-right point</param>
        /// <param name="y2">Y position of lower-right point</param>
        public Box(short x1, short y1, short x2, short y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="upperLeftPoint">Position of upper-left point</param>
        /// <param name="size">Size of box</param>
        public Box(Point upperLeftPoint, Size size)
        {
            this.x1 = (short)upperLeftPoint.X;
            this.y1 = (short)upperLeftPoint.Y;
            this.x2 = (short)(upperLeftPoint.X + size.Width);
            this.y2 = (short)(upperLeftPoint.Y + size.Height);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="upperLeftPoint">Position of upper-left point</param>
        /// <param name="lowerRightPoint">Position of lower-right point</param>
        public Box(Point upperLeftPoint, Point lowerRightPoint)
        {
            this.x1 = (short)upperLeftPoint.X;
            this.y1 = (short)upperLeftPoint.Y;
            this.x2 = (short)lowerRightPoint.X;
            this.y2 = (short)lowerRightPoint.Y;
        }

        /// <summary>
        /// X position of upper-left point
        /// </summary>
        public short XPosition1
        {
            get
            {
                return this.x1;
            }
            set
            {
                this.x1 = value;
            }
        }

        /// <summary>
        /// Y position of upper-left point
        /// </summary>
        public short YPosition1
        {
            get
            {
                return this.y1;
            }
            set
            {
                this.y1 = value;
            }
        }
        /// <summary>
        /// X position of lower-right point
        /// </summary>
        public short XPosition2
        {
            get
            {
                return this.x2;
            }
            set
            {
                this.x2 = value;
            }
        }


        /// <summary>
        /// Y position of lower-right point
        /// </summary>
        public short YPosition2
        {
            get
            {
                return this.y2;
            }
            set
            {
                this.y2 = value;
            }
        }

        /// <summary>
        /// Height of box
        /// </summary>
        public short Height
        {
            get
            {

                return (short)(this.y2 - this.y1);
            }
            set
            {
                this.y2 = (short)(this.y1 + value);
            }
        }

        /// <summary>
        /// Width of box
        /// </summary>
        public short Width
        {
            get
            {

                return (short)(this.x2 - this.x1);
            }
            set
            {
                this.x2 = (short)(this.x1 + value);
            }
        }

        /// <summary>
        /// Position of upper-left point
        /// </summary>
        public Point Location
        {
            get
            {
                return new Point(x1, y1);
            }
            set
            {
                this.y2 = (short)(value.Y + this.Height);
                this.x2 = (short)(value.X + this.Width);
                this.x1 = (short)value.X;
                this.y1 = (short)value.Y;

            }
        }

        /// <summary>
        /// Size of box
        /// </summary>
        public Size Size
        {
            get
            {
                return new Size(this.Width, this.Height);
            }
            set
            {
                this.Width = (short)value.Width;
                this.Height = (short)value.Height;
            }
        }

        /// <summary>
        /// Draw filled primitive onto surface
        /// </summary>
        /// <param name="surface">Surface to draw to</param>
        /// <param name="color">Color to fill primitive</param>
        /// <param name="antiAlias">antialias primitive</param>
        /// <param name="fill">fill primitive with color</param>
        public void Draw(Surface surface, System.Drawing.Color color, bool antiAlias, bool fill)
        {
            if (surface == null)
            {
                throw new ArgumentNullException("surface");
            }
            if (fill)
            {
                int result = SdlGfx.boxRGBA(
                surface.Handle, this.XPosition1, this.YPosition1,
                this.XPosition2, this.YPosition2,
                color.R, color.G, color.B,
                color.A);
                GC.KeepAlive(this);
                if (result != (int)SdlFlag.Success)
                {
                    throw SdlException.Generate();
                }
            }
            else
            {
                int result = SdlGfx.rectangleRGBA(
                surface.Handle, this.XPosition1, this.YPosition1,
                this.XPosition2, this.YPosition2,
                color.R, color.G, color.B,
                color.A);
                GC.KeepAlive(this);

                if (result != (int)SdlFlag.Success)
                {
                    throw SdlException.Generate();
                }
            }
        }

        /// <summary>
        /// Draw primitive onto surface
        /// </summary>
        /// <param name="surface">surface to draw to</param>
        /// <param name="color">Color of primitive</param>
        /// <param name="antiAlias">Antialias primitive</param>
        public void Draw(Surface surface, System.Drawing.Color color, bool antiAlias)
        {
            this.Draw(surface, color, antiAlias, false);
        }

        /// <summary>
        /// Draw primitive onto surface
        /// </summary>
        /// <param name="surface">surface to draw to</param>
        /// <param name="color">Color of primitive</param>
        public void Draw(Surface surface, System.Drawing.Color color)
        {
            Draw(surface, color, true, false);
        }

        /// <summary>
        /// String representation of box
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "({0},{1}, {2}, {3})", x1, y1, x2, y2);
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="obj">box to compare</param>
        /// <returns>true if boxes are the same</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(Box))
                return false;

            Box box = (Box)obj;
            return (
                (this.x1 == box.x1) &&
                (this.y1 == box.y1) &&
                (this.x2 == box.x2) &&
                (this.y2 == box.y2)
                );
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="box1">box to compare</param>
        /// <param name="box2">box to compare</param>
        /// <returns>true if boxes are the equal</returns>
        public static bool operator ==(Box box1, Box box2)
        {
            return (
                (box1.x1 == box2.x1) &&
                (box1.y1 == box2.y1) &&
                (box1.x2 == box2.x2) &&
                (box1.y2 == box2.y2)
                );
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="box1">box to compare</param>
        /// <param name="box2">box to compare</param>
        /// <returns>true if boxes are not equal</returns>
        public static bool operator !=(Box box1, Box box2)
        {
            return !(box1 == box2);
        }

        /// <summary>
        /// Hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return x1 ^ y1 ^ x2 ^ y2;

        }
        #region IPrimitive Members

        /// <summary>
        /// Center of box
        /// </summary>
        public Point Center
        {
            get
            {
                return new Point((x1 + x2) / 2, (y1 + y2) / 2);
            }
            set
            {
                short xDelta = (short)(value.X - (x2 + x1) / 2);
                short yDelta = (short)(value.Y - (y2 + y1) / 2);

                this.x1 += xDelta;
                this.x2 += xDelta;
                this.y1 += yDelta;
                this.y2 += yDelta;
            }
        }

        #endregion
    }
}
