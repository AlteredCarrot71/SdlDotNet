#region LICENSE
/*
 * Copyright (C) 2005 Rob Loach (http://www.robloach.net)
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

using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;

namespace SdlDotNet.Particles
{
    /// <summary>
    /// A particle represented by a circle.
    /// </summary>
    /// <remarks>Use ParticleCircleEmitter to emit this particle.</remarks>
    public class ParticleCircle : ParticlePixel
    {
        /// <summary>
        /// Creates a particle represented by a circle with the default values.
        /// </summary>
        public ParticleCircle()
        {
        }
        /// <summary>
        /// Creates a particle represented by a circle with a set radius.
        /// </summary>
        /// <param name="radius"></param>
        public ParticleCircle(short radius)
        {
            m_Radius = radius;
        }
        private short m_Radius = 1;
        /// <summary>
        /// Gets and sets the radius of the particles.
        /// </summary>
        public short Radius
        {
            get
            {
                return m_Radius;
            }
            set
            {
                m_Radius = value;
            }
        }

        /// <summary>
        /// Gets and sets the height of the circle.
        /// </summary>
        public override float Height
        {
            get
            {
                return m_Radius * 2;
            }
            set
            {
                m_Radius = (short)(value / 2);
            }
        }

        /// <summary>
        /// Gets the y-coordinate of the bottom edge of the circle.
        /// </summary>
        public override float Bottom
        {
            get
            {
                return this.Y + m_Radius;
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the left edge of the circle.
        /// </summary>
        public override float Left
        {
            get
            {
                return this.X - m_Radius;
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the right edge of the circle.
        /// </summary>
        public override float Right
        {
            get
            {
                return this.X + m_Radius;
            }
        }

        /// <summary>
        /// Gets the y-coordinate of the top edge of the circle.
        /// </summary>
        public override float Top
        {
            get
            {
                return this.Y - m_Radius;
            }
        }

        /// <summary>
        /// Gets and sets the width of the circle.
        /// </summary>
        public override float Width
        {
            get
            {
                return m_Radius * 2;
            }
            set
            {
                m_Radius = (short)(value / 2);
            }
        }

        /// <summary>
        /// Draws the particle on the destination surface represented by a circle.
        /// </summary>
        /// <param name="destination">The destination surface where to draw the particle.</param>
        public override void Render(Surface destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (this.Life != -1)
            {
                float alpha;
                if (this.Life >= this.LifeFull)
                    alpha = 255;
                else if (this.Life <= 0)
                    alpha = 0;
                else
                    alpha = ((float)this.Life / this.LifeFull) * 255F;

                destination.Draw(
                    new Circle((short)this.X, (short)this.Y, m_Radius),
                    System.Drawing.Color.FromArgb((int)alpha, this.Color), false, true);
            }
            else
            {
                destination.Draw(
                    new Circle((short)this.X, (short)this.Y, m_Radius),
                    this.Color, false, true);
            }
        }
    }
}
