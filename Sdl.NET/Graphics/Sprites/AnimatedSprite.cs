#region LICENSE
/*
 * Copyright (C) 2004 D. R. E. Moonfire (d.moonfire@mfgames.com)
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

namespace SdlDotNet.Graphics.Sprites
{
    /// <summary>
    /// 
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public AnimatedSprite()
            : base()
        {
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(m_Timer_Elapsed);
        }

        /// <summary>
        /// Create AnimatedSprite from AnimationCollection
        /// </summary>
        /// <param name="name">Name of animation</param>
        /// <param name="animation">animation</param>
        public AnimatedSprite(string name, AnimationCollection animation)
            : base(animation, new Vector(0, 0, 0))
        {
            if (animation == null)
            {
                throw new ArgumentNullException("animation");
            }
            m_Animations.Add(name, animation);
            //base.Surface = animation[0];
            this.m_CurrentAnimation = name;
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(m_Timer_Elapsed);
            m_Timer.Interval = m_Animations[m_CurrentAnimation].Delay;
            //this.CurrentAnimation = name;
        }
        ///// <summary>
        ///// Creates a new AnimatedSprite from a surface collection and a name
        ///// </summary>
        ///// <param name="name">The name of the animation</param>
        ///// <param name="surfaces">The surface collection containing the frames of the animation.</param>
        //public AnimatedSprite(string name, SurfaceCollection surfaces)
        //    : this(surfaces, new Vector(0, 0, 0))
        //{
        //    this.CurrentAnimation = name;
        //}

        /// <summary>
        /// Creates a new AnimatedSprite from a surface collection and a name
        /// </summary>
        /// <param name="name">The name of the animation</param>
        /// <param name="surfaces">The surface collection containing the frames of the animation.</param>
        /// <param name="vector">Vector of sprite</param>
        public AnimatedSprite(string name, SurfaceCollection surfaces, Vector vector)
            : base(surfaces, vector)
        {
            this.m_CurrentAnimation = name;
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(m_Timer_Elapsed);
            m_Timer.Interval = m_Animations[m_CurrentAnimation].Delay;
            //this.CurrentAnimation = name;
            AnimationCollection animation = new AnimationCollection();
            animation.Add(surfaces);
            m_Animations.Add(name, animation);
        }

        /// <summary>
        /// Create AnimatedSprite from AnimationCollection
        /// </summary>
        /// <param name="animation">animation</param>
        public AnimatedSprite(AnimationCollection animation)
            : this("Default", animation)
        {
        }

        /// <summary>
        /// Create Animated Sprite from SurfaceCollection
        /// </summary>
        /// <param name="surfaces">SurfaceCollection</param>
        /// <param name="coordinates">Initial Coordinates</param>
        public AnimatedSprite(SurfaceCollection surfaces, Point coordinates)
            : this(surfaces, new Vector(coordinates))
        {
        }

        /// <summary>
        /// Creates AnumatedSprite from Surface Collection
        /// </summary>
        /// <param name="surfaces">SurfaceCollection</param>
        /// <param name="vector">Starting coordinates</param>
        public AnimatedSprite(SurfaceCollection surfaces, Vector vector)
            : base(surfaces, vector)
        {
            if (surfaces == null)
            {
                throw new ArgumentNullException("surfaces");
            }
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(m_Timer_Elapsed);
            AnimationCollection anim = new AnimationCollection();
            anim.Add(surfaces);
            m_Animations.Add("Default", anim);
        }

        /// <summary>
        /// Creates AnimatedSprite from SurfaceCollection
        /// </summary>
        /// <param name="surfaces">SurfaceCollection</param>
        public AnimatedSprite(SurfaceCollection surfaces)
            : base(surfaces, new Vector(0, 0, 0))
        {
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(m_Timer_Elapsed);
        }
        #endregion Constructors

        #region Properties
        private bool disposed;
        private AnimationDictionary m_Animations = new AnimationDictionary();

        /// <summary>
        /// The collection of animations for the animated sprite
        /// </summary>
        public AnimationDictionary Animations
        {
            get
            {
                return m_Animations;
            }
        }

        /// <summary>
        /// Gets and sets whether the animation is going.
        /// </summary>
        public bool Animate
        {
            get
            {
                return m_Timer.Enabled;
            }
            set
            {
                m_Timer.Enabled = value;
            }
        }

        private string m_CurrentAnimation = "Default";

        /// <summary>
        /// Gets and sets the current animation.
        /// </summary>
        public string CurrentAnimation
        {
            get
            {
                return m_CurrentAnimation;
            }
            set
            {
                // Check to see if it exists.
                if (!m_Animations.ContainsKey(value))
                {
                    throw new AnimationException();
                }

                // Set the animation settings.
                m_CurrentAnimation = value;
                this.Surface = this.m_Animations[m_CurrentAnimation][0];
                m_Timer.Interval = m_Animations[m_CurrentAnimation].Delay;
                //this.Size = this.m_Animations[m_CurrentAnimation].Size;
            }
        }

        /// <summary>
        /// Gets the current animations surface
        /// </summary>
        public override Surface Surface
        {
            get
            {
                //return this.m_Animations[m_CurrentAnimation][m_Frame];
                return this.m_Animations[m_CurrentAnimation][m_Frame % this.Animations[m_CurrentAnimation].Count];
            }
            set
            {
                base.Surface = value;
            }
        }

        ///// <summary>
        ///// Renders the surface
        ///// </summary>
        ///// <returns>
        ///// A surface representing the rendered animated sprite.
        ///// </returns>
        //public override Surface Render()
        //{
        //    return this.m_Animations[m_CurrentAnimation][m_Frame % this.Animations[m_CurrentAnimation].Count];
        //}

        private int m_Frame;
        /// <summary>
        /// Gets and sets the current frame in the animation.
        /// </summary>
        public int Frame
        {
            get
            {
                return m_Frame;
            }
            set
            {
                m_Frame = value;
            }
        }

        /// <summary>
        /// Gets and sets the AnimateForward flag for all animations in this sprite's Animations.
        /// </summary>
        public bool AnimateForward
        {
            get
            {
                return m_Animations.AnimateForward;
            }
            set
            {
                m_Animations.AnimateForward = value;
            }
        }

        //		/// <summary>
        //		/// Gets the sprite's current surface's width.
        //		/// </summary>
        //		public override int Width
        //		{
        //			get
        //			{
        //				return Surface.Width;
        //			}
        //			set
        //			{
        //				base.Width = value;
        //			}
        //		}
        //
        //		/// <summary>
        //		/// Gets the sprite's current surface's height.
        //		/// </summary>
        //		public override int Height
        //		{
        //			get
        //			{
        //				return Surface.Height;
        //			}
        //			set
        //			{
        //				base.Height = value;
        //			}
        //		}

        /// <summary>
        /// Gets and alpha of the sprite's current animation.  Sets the alpha of every animation made for the sprite.
        /// </summary>
        public override byte Alpha
        {
            get
            {
                return m_Animations[m_CurrentAnimation].Alpha;
            }
            set
            {
                foreach (AnimationCollection anim in m_Animations.Values)
                {
                    anim.Alpha = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the alpha blending of the sprite's current animation.  Sets the alpha blending of every animation made for the sprite.
        /// </summary>
        public override bool AlphaBlending
        {
            get
            {
                return m_Animations[m_CurrentAnimation].AlphaBlending;
            }
            set
            {
                foreach (AnimationCollection anim in m_Animations.Values)
                {
                    anim.AlphaBlending = value;
                }
            }
        }

        /// <summary>
        /// Gets and transparent color of the sprite's current animation.  Sets the transparent color of every animation made for the sprite.
        /// </summary>
        public override Color TransparentColor
        {
            get
            {
                return m_Animations[m_CurrentAnimation].TransparentColor;
            }
            set
            {
                foreach (AnimationCollection anim in m_Animations.Values)
                {
                    anim.TransparentColor = value;
                }
            }
        }

        /// <summary>
        /// Gets and transparency of the sprite's current animation.  Sets the transparency of every animation made for the sprite.
        /// </summary>
        public override bool Transparent
        {
            get
            {
                return m_Animations[m_CurrentAnimation].Transparent;
            }
            set
            {
                foreach (AnimationCollection anim in m_Animations.Values)
                {
                    anim.Transparent = value;
                }
            }
        }

        #endregion

        #region Private Methods
        private System.Timers.Timer m_Timer = new System.Timers.Timer();

        private void m_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            AnimationCollection current = m_Animations[m_CurrentAnimation];
            m_Timer.Interval = current.Delay;

            if (m_Frame >= current.Count && current.AnimateForward) // Going forwards and past last frame
            {
                if (current.Loop)
                {
                    m_Frame = 0;
                }
            }
            else if (m_Frame <= 0 && !current.AnimateForward) // Going backwards and past first frame
            {
                if (current.Loop)
                {
                    m_Frame = current.Count - 1;
                }
            }
            else // Still going
            {
                m_Frame = m_Frame + current.FrameIncrement;
            }
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Destroys the surface object and frees its memory
        /// </summary>
        /// <param name="disposing">If ture, dispose unmanaged resources</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        if (m_Timer != null)
                        {
                            m_Timer.Dispose();
                            m_Timer = null;
                        }
                    }
                    this.disposed = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion IDisposable Members
    }
}
