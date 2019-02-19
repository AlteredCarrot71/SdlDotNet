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
using System.Collections.ObjectModel;
using System.IO;

namespace SdlDotNet.Graphics
{
    /// <summary>
    /// Encapsulates the collection of Surface objects.
    /// </summary>
    public class SurfaceCollection : Collection<Surface>
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public SurfaceCollection()
            : base()
        {
        }

        /// <summary>
        /// Load in multiple files as surfaces
        /// </summary>
        /// <param name="baseName">Base name of files</param>
        /// <param name="extension">file extension of images</param>
        public virtual void Add(string baseName, string extension)
        {
            // Load the images into memory
            int i = 0;

            while (true)
            {
                string fn = null;

                if (i < 10)
                {
                    fn = baseName + "-0" + i + extension;
                }
                else
                {
                    fn = baseName + "-" + i + extension;
                }

                if (!File.Exists(fn))
                {
                    break;
                }

                // Load it
                this.Add(new Surface(fn));
                i++;
            }
        }

        /// <summary>
        /// Loads a collection of tiled surfaces from one larger surface. 
        /// </summary>
        /// <param name="fileName">
        /// The filename of the surface which contains all the tiles.
        /// </param>
        /// <param name="tileSize">
        /// The size of each tile.
        /// </param>
        public virtual void Add(string fileName, Size tileSize)
        {
            this.Add(new Surface(fileName), tileSize);
        }

        /// <summary>
        /// Loads a row of tiled surfaces from one larger surface.
        /// </summary>
        /// <param name="fileName">
        /// The filename of the large surface.
        /// </param>
        /// <param name="tileSize">
        /// The size of each tile.
        /// </param>
        /// <param name="rowNumber">
        /// The row number of which to load the surface collection.
        /// </param>
        public virtual void Add(string fileName, Size tileSize, int rowNumber)
        {
            this.Add(new Surface(fileName), tileSize, rowNumber);
        }

        /// <summary> 
        /// Loads a collection of tiled surfaces from one larger surface. 
        /// </summary> 
        /// <param name="fullImage">
        /// The larger surface which contains all the tiles.
        /// </param> 
        /// <param name="tileSize">
        /// The size of each tile.
        /// </param> 
        public virtual void Add(
            Surface fullImage,
            Size tileSize)
        {
            if (fullImage == null)
            {
                throw new ArgumentNullException("fullImage");
            }
            fullImage.Alpha = 255;

            for (int tileY = 0; tileY * tileSize.Height < fullImage.Height; tileY++)
            {
                for (int tileX = 0; tileX * tileSize.Width < fullImage.Width; tileX++)
                {
                    Surface tile = fullImage.CreateCompatibleSurface(tileSize.Width, tileSize.Height);

                    //tile.ClearTransparentColor();
                    //fullImage.ClearTransparentColor();
                    //Surface tile = new Surface(tileSize.Width, tileSize.Height);
                    //tile.TransparentColor = Color.Black;

                    tile.Blit(
                        fullImage,
                        new Point(0, 0),
                        new Rectangle(tileX * tileSize.Width,
                        tileY * tileSize.Height, tileSize.Width, tileSize.Height));
                    this.Add(tile);
                }
            }
        }

        /// <summary>
        /// Loads only one row of tiled surfaces from a larger surface.
        /// </summary>
        /// <param name="fullImage">
        /// The larger surface which contains all the tiles.
        /// </param> 
        /// <param name="tileSize">
        /// The size of each tile.
        /// </param>
        /// <param name="rowNumber">
        /// The row to be loaded.
        /// </param>
        public virtual void Add(Surface fullImage, Size tileSize, int rowNumber)
        {
            if (fullImage == null)
            {
                throw new ArgumentNullException("fullImage");
            }
            fullImage.Alpha = 0;
            for (int tileX = 0; tileX * tileSize.Width < fullImage.Width; tileX++)
            {
                Surface tile = fullImage.CreateCompatibleSurface(tileSize.Width, tileSize.Height);
                //tile.Fill(fullImage.);
                tile.Blit(
                    fullImage,
                    new Point(0, 0),
                    new Rectangle(tileX * tileSize.Width,
                    rowNumber * tileSize.Height, tileSize.Width, tileSize.Height));
                //tile.TransparentColor = fullImage.TransparentColor;
                this.Add(tile);
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Size of first surface
        /// </summary>
        public virtual Size Size
        {
            get
            {
                return new Size(this[0].Width, this[0].Height);
            }
        }

        /// <summary>
        /// Gets the transparent color of the first surface.
        /// Sets the transparent color of every surface in the collection.
        /// </summary>
        public Color TransparentColor
        {
            get
            {
                return this[0].TransparentColor;
            }
            set
            {
                foreach (Surface surface in this)
                {
                    surface.TransparentColor = value;
                }
            }
        }

        /// <summary>
        /// Gets the transparency of the first surface.
        /// Sets the transparency of every surface in the collection.
        /// </summary>
        public bool Transparent
        {
            get
            {
                return this[0].Transparent;
            }
            set
            {
                foreach (Surface surface in this)
                {
                    surface.Transparent = value;
                }
            }
        }

        /// <summary>
        /// Gets the alpha of the first surface.
        /// Sets the alpha of every surface in the collection.
        /// </summary>
        public byte Alpha
        {
            get
            {
                return this[0].Alpha;
            }
            set
            {
                foreach (Surface surface in this)
                {
                    surface.Alpha = value;
                }
            }
        }

        /// <summary>
        /// Gets the transparency of the first surface.
        /// Sets the transparency of every surface in the collection.
        /// </summary>
        public bool AlphaBlending
        {
            get
            {
                return this[0].AlphaBlending;
            }
            set
            {
                foreach (Surface surface in this)
                {
                    surface.AlphaBlending = value;
                }
            }
        }

        /// <summary>
        /// Constructor to make a new surface collection based off of an existing one.
        /// </summary>
        /// <param name="surfaces">The surface collection to copy.</param>
        public void Add(SurfaceCollection surfaces)
        {
            foreach (Surface s in surfaces)
            {
                base.Add(s);
            }
        }

        #endregion
    }
}
