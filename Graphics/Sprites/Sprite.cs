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
using System.Diagnostics.CodeAnalysis;

using SdlDotNet.Core;
using SdlDotNet.Audio;
using SdlDotNet.Input;

namespace SdlDotNet.Graphics.Sprites
{
    /// <summary>
    /// Sprite class contains both a Surface and a Rectangle so that 
    /// an object can be easily displayed and manipulated.
    /// </summary>
    public class Sprite : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Basic constructor. 
        /// </summary>
        /// <remarks>
        /// Use this with caution. 
        /// This is provided as a convenience. 
        /// Please give the sprite a Surface and a Vector.</remarks>
        public Sprite()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler<ChangedZAxisEventArgs> ChangedZAxis;

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler<KillSpriteEventArgs> KillSprite;

        /// <summary>
        /// Create a new Sprite
        /// </summary>
        /// <param name="position">Starting position</param>
        /// <param name="surface">Surface of Sprite</param>
        public Sprite(Surface surface, Point position)
            : this(surface, new Vector(position))
        {
        }

        /// <summary>
        /// Create new Sprite at (0, 0)
        /// </summary>
        /// <param name="surface">Surface of Sprite</param>
        public Sprite(Surface surface)
            : this(surface, new Vector(0, 0, 0))
        {
        }

        /// <summary>
        /// Creates a new sprite using the given surface file.
        /// </summary>
        /// <param name="surfaceFile">
        /// The file path of the surface to use as the sprite.
        /// </param>
        public Sprite(string surfaceFile)
            : this(new Surface(surfaceFile))
        {
        }

        /// <summary>
        /// Create new Sprite
        /// </summary>
        /// <param name="vector">Vector of Sprite</param>
        /// <param name="surface">Surface of Sprite</param>
        public Sprite(Surface surface, Vector vector)
        {
            if (surface == null)
            {
                throw new ArgumentNullException("surface");
            }
            this.surf = surface;
            this.vector = vector;
        }

        internal Sprite(SurfaceCollection surfaces, Vector vector)
        {
            if (surfaces == null)
            {
                throw new ArgumentNullException("surfaces");
            }
            this.surf = surfaces[0];
            this.vector = vector;
        }

        /// <summary>
        /// Create new sprite
        /// </summary>
        /// <param name="vector">Vector of Sprite</param>
        /// <param name="surface">Surface of Sprite</param>
        /// <param name="group">
        /// SpriteCollection group to put Sprite into.
        /// </param>
        public Sprite(Surface surface, Vector vector, SpriteCollection group)
            : this(surface, vector)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            group.Add(this);
        }

        /// <summary>
        /// Create new sprite
        /// </summary>
        /// <param name="position">position of Sprite</param>
        /// <param name="surface">Surface of Sprite</param>
        /// <param name="group">
        /// SpriteCollection group to put Sprite into.
        /// </param>
        public Sprite(Surface surface, Point position, SpriteCollection group)
            : this(surface, new Vector(position), group)
        {
        }

        #endregion

        #region Display
        private Surface surf;
        /// <summary>
        /// Gets and sets the surface of the sprite.
        /// </summary>
        public virtual Surface Surface
        {
            get
            {
                return surf;
            }
            set
            {
                surf = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Processes Active events
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(ActiveEventArgs args)
        {
        }
        /// <summary>
        /// Processes the keyboard.
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(KeyboardEventArgs args)
        {
        }

        /// <summary>
        /// Processes a mouse button. This event is trigger by the SDL
        /// system. 
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(MouseButtonEventArgs args)
        {
        }

        /// <summary>
        /// Processes a mouse motion event. This event is triggered by
        /// SDL. Only
        /// sprites that are MouseSensitive are processed.
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(MouseMotionEventArgs args)
        {
        }

        /// <summary>
        /// Processes a joystick motion event. This event is triggered by
        /// SDL. Only
        /// sprites that are JoystickSensitive are processed.
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(JoystickAxisEventArgs args)
        {
        }

        /// <summary>
        /// Processes a joystick button event. This event is triggered by
        /// SDL. Only
        /// sprites that are JoystickSensitive are processed.
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(JoystickButtonEventArgs args)
        {
        }

        /// <summary>
        /// Processes a joystick hat motion event. This event is triggered by
        /// SDL. Only
        /// sprites that are JoystickSensitive are processed.
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(JoystickHatEventArgs args)
        {
        }

        /// <summary>
        /// Processes a joystick hat motion event. This event is triggered by
        /// SDL. Only
        /// sprites that are JoystickSensitive are processed.
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(JoystickBallEventArgs args)
        {
        }

        /// <summary>
        /// Processes Quit Events
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(QuitEventArgs args)
        {
        }
        /// <summary>
        /// Process User Events
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(UserEventArgs args)
        {
        }

        /// <summary>
        /// Process VideoExposeEvents
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(VideoExposeEventArgs args)
        {
        }

        /// <summary>
        /// Process VideoResizeEvents
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(VideoResizeEventArgs args)
        {
        }

        /// <summary>
        /// Process ChannelFinishedEvents
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(ChannelFinishedEventArgs args)
        {
        }

        /// <summary>
        /// Process MusicFinishedEvents
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(MusicFinishedEventArgs args)
        {
        }

        /// <summary>
        /// All sprites are tickable, regardless if they actual do
        /// anything. This ensures that the functionality is there, to be
        /// overridden as needed.
        /// </summary>
        /// <param name="args">Event args</param>
        public virtual void Update(TickEventArgs args)
        {
        }
        #endregion

        #region Geometry

        private Vector vector;

        /// <summary>
        /// 
        /// </summary>
        public Vector Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        private bool boundingBox;

        /// <summary>
        /// 
        /// </summary>
        public bool BoundingBox
        {
            get
            {
                return boundingBox;
            }
            set
            {
                boundingBox = value;
            }
        }

        private Rectangle rectangle;

        /// <summary>
        /// Gets and sets the sprite's surface rectangle.
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                if (boundingBox)
                {
                    if (rectangle.IsEmpty)
                    {
                        this.rectangle = new Rectangle(new Point((int)vector.X, (int)vector.Y), surf == null ? new Size(0, 0) : this.surf.Size);
                    }
                    return rectangle;
                }
                else
                {
                    if (vector.IsEmpty)
                    {
                        this.vector = new Vector(0, 0, 0);
                    }
                    return new Rectangle(new Point((int)vector.X, (int)vector.Y), surf == null ? new Size(0, 0) : this.surf.Size);
                }
            }
            set
            {
                if (boundingBox)
                {
                    this.rectangle = value;
                }
                else
                {
                    this.vector = new Vector(value.X, value.Y, 0);
                }
            }
        }

        Rectangle lastBlitRectangle;

        /// <summary>
        /// 
        /// </summary>
        public Rectangle LastBlitRectangle
        {
            get { return lastBlitRectangle; }
            set { lastBlitRectangle = value; }
        }

        /// <summary>
        /// Gets and sets the sprites current x,y location.
        /// </summary>
        public Point Position
        {
            get
            {
                return new Point((int)vector.X, (int)vector.Y);
            }
            set
            {
                vector.X = value.X;
                vector.Y = value.Y;
            }
        }

        /// <summary>
        /// Center point of Sprite
        /// </summary>
        public Point Center
        {
            get
            {
                return new Point(((this.X) + (this.Width) / 2),
                    ((this.Y) + (this.Height) / 2));
            }
            set
            {
                this.X = (value.X - this.Width / 2);
                this.Y = (value.Y - this.Height / 2);
            }
        }

        //private int coordinateZ;

        /// <summary>
        /// Gets and sets the sprite's x location.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public int X
        {
            get
            {
                return (int)this.vector.X;
            }
            set
            {
                this.vector.X = value;
            }
        }

        /// <summary>
        /// Gets and sets the sprite's y location.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public int Y
        {
            get
            {
                return (int)this.vector.Y;
            }
            set
            {
                this.vector.Y = value;
            }
        }

        /// <summary>
        /// Gets and sets the sprite's z coordinate.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        public int Z
        {
            get
            {
                return (int)this.vector.Z;
            }
            set
            {
                this.vector.Z = value;
                if (ChangedZAxis != null)
                {
                    ChangedZAxis(this, new ChangedZAxisEventArgs(this));
                }
            }
        }

        /// <summary>
        /// Gets the left edge of the sprite.
        /// </summary>
        public int Left
        {
            get
            {
                return this.X;
            }
        }

        /// <summary>
        /// Gets the right edge of the sprite.
        /// </summary>
        public int Right
        {
            get
            {
                return this.X + this.Width;
            }
        }

        /// <summary>
        /// Gets the top edge of the sprite.
        /// </summary>
        public int Top
        {
            get
            {
                return this.Y;
            }
        }

        /// <summary>
        /// Gets the bottom edge of the sprite.
        /// </summary>
        public int Bottom
        {
            get
            {
                return this.Y + this.Height;
            }
        }

        /// <summary>
        /// Gets and sets the sprite's size.
        /// </summary>
        public Size Size
        {
            get
            {
                return new Size(this.surf.Width, this.surf.Height);
            }
            //set
            //{
            //    //this.surf.Width = value.Width;
            //    //this.surf.Height = value.Height;
            //}
        }

        /// <summary>
        /// Gets and sets the sprite's height.
        /// </summary>
        public virtual int Height
        {
            get
            {
                return this.surf.Height;
            }
            set
            {
                //this.rect.Height = value;
            }
        }

        /// <summary>
        /// Gets and sets the sprite's width.
        /// </summary>
        public virtual int Width
        {
            get
            {
                return this.surf.Width;
            }
            set
            {
                //this.rect.Width = value;
            }
        }

        #region IntersectsWith
        /// <summary>
        /// Checks if Sprite intersects with a point
        /// </summary>
        /// <param name="point">Point to intersect with</param>
        /// <returns>True if Sprite intersects with the Point</returns>
        public virtual bool IntersectsWith(Point point)
        {
            Rectangle rect = new Rectangle((int)vector.X, (int)vector.Y, this.surf.Width, this.surf.Height);
            return rect.IntersectsWith(new Rectangle(point, new Size(0, 0)));
        }

        /// <summary>
        /// Checks if Sprite intersects with a rectangle
        /// </summary>
        /// <param name="rectangle">rectangle to intersect with
        /// </param>
        /// <returns>True if Sprite intersect with Rectangle</returns>
        public virtual bool IntersectsWith(Rectangle rectangle)
        {
            Rectangle rect = new Rectangle((int)vector.X, (int)vector.Y, this.surf.Width, this.surf.Height);
            return rect.IntersectsWith(rectangle);
        }

        /// <summary>
        /// Check if two Sprites intersect
        /// </summary>
        /// <param name="sprite">Sprite to check intersection with</param>
        /// <returns>True if sprites intersect</returns>
        public virtual bool IntersectsWith(Sprite sprite)
        {
            if (sprite == null)
            {
                throw new ArgumentNullException("sprite");
            }
            return this.IntersectsWith(sprite.Rectangle);
        }

        /// <summary>
        /// Checks if Sprite intersects with a rectangle with tolerance
        /// </summary>
        /// <param name="rect">rectangle to intersect with
        /// </param>
        /// <param name="tolerance">The tolerance of the collision check</param>
        /// <returns>True if Sprite intersect with Rectangle</returns>
        public virtual bool IntersectsWith(Rectangle rect, int tolerance)
        {
            if (rect.Right - this.Left < tolerance) return false;
            if (rect.X - this.Right > -tolerance) return false;
            if (rect.Bottom - this.Y < tolerance) return false;
            if (rect.Y - this.Bottom > -tolerance) return false;
            return true;
        }

        /// <summary>
        /// Check if two Sprites intersect
        /// </summary>
        /// <param name="sprite">Sprite to check intersection with</param>
        /// <param name="tolerance">The amount of tolerance to give the collision.</param>
        /// <returns>True if sprites intersect</returns>
        public virtual bool IntersectsWith(Sprite sprite, int tolerance)
        {
            if (sprite == null)
            {
                throw new ArgumentNullException("sprite");
            }
            if (sprite.Right - this.Left < tolerance) return false;
            if (sprite.X - this.Right > -tolerance) return false;
            if (sprite.Bottom - this.Y < tolerance) return false;
            if (sprite.Y - this.Bottom > -tolerance) return false;
            return true;
        }

        /// <summary>
        /// Checks for collision between two sprites using a radius from the center of the sprites.
        /// </summary>
        /// <param name="sprite">The sprite to compare to.</param>
        /// <param name="radius">The radius of the current sprite. Defaults to the radius of the sprite.</param>
        /// <param name="radiusOther">The other sprite's radius. Defaults to the radius of the sprite.</param>
        /// <param name="tolerance">The size of the buffer zone for collision detection. Defaults to 0.</param>
        /// <returns>True if they intersect, false if they don't.</returns>
        /// <remarks>If they radius is not given, it calculates it for you using half the width plus half the height.</remarks>
        public virtual bool IntersectsWithRadius(Sprite sprite, int radius, int radiusOther, int tolerance)
        {
            if (sprite == null)
            {
                throw new ArgumentNullException("sprite");
            }
            Point center1 = this.Center;
            Point center2 = sprite.Center;
            int xdiff = center2.X - center1.X;	// x plane difference
            int ydiff = center2.Y - center1.Y;	// y plane difference

            // distance between the circles centres squared
            int dcentre_sq = (ydiff * ydiff) + (xdiff * xdiff);

            // calculate sum of radiuses squared
            int r_sum_sq = radius + radiusOther;
            r_sum_sq *= r_sum_sq;

            return (dcentre_sq - r_sum_sq <= (tolerance * tolerance));
        }

        /// <summary>
        /// Checks for collision between two sprites using a radius from the center of the sprites.
        /// </summary>
        /// <param name="sprite">The sprite to compare to.</param>
        /// <param name="radius">The radius of the current sprite. Defaults to the radius of the sprite.</param>
        /// <param name="radiusOther">The other sprite's radius. Defaults to the radius of the sprite.</param>
        /// <returns>True if they intersect, false if they don't.</returns>
        /// <remarks>The offset defaults to 0.</remarks>
        public virtual bool IntersectsWithRadius(Sprite sprite, int radius, int radiusOther)
        {
            return IntersectsWithRadius(sprite, radius, radiusOther, 0);
        }

        /// <summary>
        /// Checks for collision between two sprites using a radius from the center of the sprites.
        /// </summary>
        /// <param name="sprite">The sprite to compare to.</param>
        /// <param name="radius">The radius of the sprites.</param>
        /// <returns>True if they intersect, false if they don't.</returns>
        public virtual bool IntersectsWithRadius(Sprite sprite, int radius)
        {
            return IntersectsWithRadius(sprite, radius, radius, 0);
        }

        /// <summary>
        /// Checks for collision between two sprites using a radius from the center of the sprites.
        /// </summary>
        /// <param name="sprite">The sprite to compare to.</param>
        /// <returns>True if they intersect, false if they don't.</returns>
        /// <remarks>The radius for both the sprites is calculated by using half the width and half the height.</remarks>
        public virtual bool IntersectsWithRadius(Sprite sprite)
        {
            if (sprite == null)
            {
                throw new ArgumentNullException("sprite");
            }
            int r1 = (this.Width + this.Height) / 4;
            int r2 = (sprite.Width + sprite.Height) / 4;
            return IntersectsWithRadius(sprite, r1, r2, 0);
        }

        /// <summary>
        /// Check to see if Sprite intersects with any sprite in a SpriteCollection
        /// </summary>
        /// <param name="spriteCollection">Collection to chekc the intersection with</param>
        /// <returns>True if sprite intersects with any sprite in collection</returns>
        public virtual bool IntersectsWith(SpriteCollection spriteCollection)
        {
            if (spriteCollection == null)
            {
                throw new ArgumentNullException("SpriteCollection");
            }
            foreach (Sprite sprite in spriteCollection)
            {
                if (this.IntersectsWith(sprite))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion IntersectsWith
        #endregion

        #region Properties

        private bool allowDrag;

        /// <summary>
        /// Allows sprite to be dragged via the mouse
        /// </summary>
        public bool AllowDrag
        {
            get
            {
                return allowDrag;
            }
            set
            {
                allowDrag = value;
            }
        }

        private bool beingDragged;

        /// <summary>
        /// true when sprite is being dragged by the mouse
        /// </summary>
        public bool BeingDragged
        {
            get
            {
                return beingDragged;
            }
            set
            {
                beingDragged = value;
            }
        }

        private bool visible = true;

        /// <summary>
        /// Gets and sets whether or not the sprite is visible when rendered.
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        /// <summary>
        /// Gets and sets the alpha associated with the sprite's surface.
        /// </summary>
        public virtual byte Alpha
        {
            get
            {
                return surf.Alpha;
            }
            set
            {
                surf.Alpha = value;
            }
        }

        /// <summary>
        /// Gets and sets the alpha blending associated with the sprite's surface.
        /// </summary>
        public virtual bool AlphaBlending
        {
            get
            {
                return surf.AlphaBlending;
            }
            set
            {
                surf.AlphaBlending = value;
            }
        }

        /// <summary>
        /// Gets and sets the transparent color associated with the sprite's surface.
        /// </summary>
        public virtual Color TransparentColor
        {
            get
            {
                return surf.TransparentColor;
            }
            set
            {
                surf.TransparentColor = value;
            }
        }

        /// <summary>
        /// Gets and sets the transparency associated with the sprite's surface.
        /// </summary>
        public virtual bool Transparent
        {
            get
            {
                return surf.Transparent;
            }
            set
            {
                surf.Transparent = value;
            }
        }
        #endregion

        #region IDisposable Members

        private bool disposed;

        /// <summary>
        /// Destroy sprite
        /// </summary>
        /// <param name="disposing">If true, remove all unamanged resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.surf != null)
                    {
                        this.surf.Dispose();
                        this.surf = null;
                    }
                }
                this.disposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Kill()
        {
            if (KillSprite != null)
            {
                KillSprite(this, new KillSpriteEventArgs(this));
            }
        }
        /// <summary>
        /// Destroy object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destroy object
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Destroy object
        /// </summary>
        ~Sprite()
        {
            Dispose(false);
        }

        #endregion
    }
}
