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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;

using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;

namespace SdlDotNet.Graphics.Sprites
{
    /// <summary>
    /// The SpriteCollection is used to group sprites into an easily managed whole. 
    /// </summary>
    /// <remarks>The sprite manager has no size.</remarks>
    [Serializable]
    public class SpriteCollection : BindingList<Sprite>
    {
        #region Constructors
        /// <summary>
        /// Creates a new SpriteCollection without any elements in it.
        /// </summary>
        public SpriteCollection()
            : base()
        {
            Sprite.ChangedZAxis += new EventHandler<ChangedZAxisEventArgs>(SpriteCollection_ChangedZAxis);
            Sprite.KillSprite += new EventHandler<KillSpriteEventArgs>(SpriteCollection_KillSprite);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            this.isSorted = false;
            base.OnAddingNew(e);
        }

        Collection<Sprite> lostSprites = new Collection<Sprite>();

        void SpriteCollection_KillSprite(object sender, KillSpriteEventArgs e)
        {
            if (this.Contains(e.Sprite))
            {
                this.lostRects.Add(e.Rectangle);
                this.lostSprites.Add(e.Sprite);
            }
        }

        void SpriteCollection_ChangedZAxis(object sender, ChangedZAxisEventArgs e)
        {
            if (this.Contains(e.Sprite))
            {
                this.isSorted = false;
            }
        }

        #endregion

        #region Display
        Collection<Rectangle> lostRects = new Collection<Rectangle>();
        Collection<Rectangle> rects = new Collection<Rectangle>();
        bool isSorted;

        /// <summary>
        /// Draws all surfaces within the collection on the given destination.
        /// </summary>
        /// <param name="destination">The destination surface.</param>
        public virtual Collection<Rectangle> Draw(Surface destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            rects.Clear();
            if (!isSorted)
            {
                this.SortByZAxis();
                this.isSorted = true;
            }

            foreach (Sprite s in this)
            {
                if (s.Visible)
                {
                    rects.Add(s.LastBlitRectangle);
                    s.LastBlitRectangle = destination.Blit(s.Surface, s.Rectangle);
                    rects.Add(s.LastBlitRectangle);
                }
            }
            this.Remove(this.lostSprites);
            return rects;
        }

        /// <summary>
        /// Erases SpriteCollection from surface
        /// </summary>
        /// <param name="surface">
        /// Surface to remove the SpriteCollection from</param>
        /// <param name="background">
        /// Background to use to paint over Sprites in SpriteCollection
        /// </param>
        public void Erase(Surface surface, Surface background)
        {
            if (surface == null)
            {
                throw new ArgumentNullException("surface");
            }
            if (this.lostRects.Count > 0)
            {
                foreach (Rectangle r in this.lostRects)
                {
                    surface.Blit(background, r, r);
                }

                surface.Update(this.lostRects);
                this.lostRects.Clear();
            }

            foreach (Rectangle s in this.rects)
            {
                surface.Blit(background, s, s);
            }
        }

        #endregion

        #region Sprites
        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        public void SortByZAxis(ListSortDirection direction)
        {
            List<Sprite> sprites = this.Items as List<Sprite>;
            sprites.Sort(delegate(Sprite sprite1, Sprite sprite2)
           {
               return Comparer<int>.Default.Compare(sprite1.Z, sprite2.Z) * (direction == ListSortDirection.Descending ? -1 : 1);
           });
        }

        /// <summary>
        /// 
        /// </summary>
        public void SortByZAxis()
        {
            SortByZAxis(ListSortDirection.Ascending);
        }


        /// <summary>
        /// Adds sprites from another group to this group
        /// </summary>
        /// <param name="spriteCollection">SpriteCollection to add Sprites from</param>
        public int Add(SpriteCollection spriteCollection)
        {
            if (spriteCollection == null)
            {
                throw new ArgumentNullException("SpriteCollection");
            }
            foreach (Sprite s in spriteCollection)
            {
                base.Add(s);
            }
            return this.Count;
        }

        /// <summary>
        /// Rectangles of Sprites that have been removed
        /// </summary>
        /// <remarks>
        /// These Rectangles are kept temporarily until their 
        /// positions can be properly erased.
        /// </remarks>
        protected Collection<Rectangle> LostRects
        {
            get
            {
                return this.lostRects;
            }
        }

        /// <summary>
        /// Removes sprite from this group if they are contained in the given group
        /// </summary>
        /// <param name="spriteCollection">
        /// Remove SpriteCollection from this SpriteCollection.
        /// </param>
        public virtual void Remove(SpriteCollection spriteCollection)
        {
            if (spriteCollection == null)
            {
                throw new ArgumentNullException("spriteCollection");
            }
            foreach (Sprite s in spriteCollection)
            {
                if (this.Contains(s))
                {
                    this.lostRects.Add(s.LastBlitRectangle);
                    base.Remove(s);
                }
            }
        }

        /// <summary>
        /// Removes sprite from this group if they are contained in the given collection
        /// </summary>
        /// <param name="spriteCollection">
        /// Remove all sprite in the Collection from this SpriteCollection.
        /// </param>
        public virtual void Remove(Collection<Sprite> spriteCollection)
        {
            if (spriteCollection == null)
            {
                throw new ArgumentNullException("spriteCollection");
            }
            foreach (Sprite s in spriteCollection)
            {
                if (this.Contains(s))
                {
                    this.lostRects.Add(s.LastBlitRectangle);
                    base.Remove(s);
                }
            }
        }

        #endregion

        #region Geometry

        /// <summary>
        /// Gets the size of the first sprite in the collection, otherwise a size of 0,0.
        /// </summary>
        /// <returns>The size of the first sprite in the collection.</returns>
        public virtual Size Size
        {
            get
            {
                return new Size(0, 0);
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableActiveEvent()
        {
            Events.AppActive += new EventHandler<ActiveEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableJoystickAxisEvent()
        {
            Events.JoystickAxisMotion += new EventHandler<JoystickAxisEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableJoystickBallEvent()
        {
            Events.JoystickBallMotion += new EventHandler<JoystickBallEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableJoystickButtonEvent()
        {
            Events.JoystickButtonDown += new EventHandler<JoystickButtonEventArgs>(Update);
            Events.JoystickButtonUp += new EventHandler<JoystickButtonEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableJoystickButtonDownEvent()
        {
            Events.JoystickButtonDown += new EventHandler<JoystickButtonEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableJoystickButtonUpEvent()
        {
            Events.JoystickButtonUp += new EventHandler<JoystickButtonEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableJoystickHatEvent()
        {
            Events.JoystickHatMotion += new EventHandler<JoystickHatEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableKeyboardEvent()
        {
            Events.KeyboardUp += new EventHandler<KeyboardEventArgs>(Update);
            Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableKeyboardDownEvent()
        {
            Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(Update);
        }
        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableKeyboardUpEvent()
        {
            Events.KeyboardUp += new EventHandler<KeyboardEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableMouseButtonEvent()
        {
            Events.MouseButtonDown += new EventHandler<MouseButtonEventArgs>(Update);
            Events.MouseButtonUp += new EventHandler<MouseButtonEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableMouseButtonDownEvent()
        {
            Events.MouseButtonDown += new EventHandler<MouseButtonEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableMouseButtonUpEvent()
        {
            Events.MouseButtonUp += new EventHandler<MouseButtonEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableMouseMotionEvent()
        {
            Events.MouseMotion += new EventHandler<MouseMotionEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableUserEvent()
        {
            Events.UserEvent += new EventHandler<UserEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableQuitEvent()
        {
            Events.Quit += new EventHandler<QuitEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableVideoExposeEvent()
        {
            Events.VideoExpose += new EventHandler<VideoExposeEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableVideoResizeEvent()
        {
            Events.VideoResize += new EventHandler<VideoResizeEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableChannelFinishedEvent()
        {
            Events.ChannelFinished += new EventHandler<ChannelFinishedEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableMusicFinishedEvent()
        {
            Events.MusicFinished += new EventHandler<MusicFinishedEventArgs>(Update);
        }

        /// <summary>
        /// Enables Event for SpriteCollection
        /// </summary>
        public void EnableTickEvent()
        {
            Events.Tick += new EventHandler<TickEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableActiveEvent()
        {
            Events.AppActive -= new EventHandler<ActiveEventArgs>(Update);
        }

        ///// <summary>
        ///// Disables Event for SpriteCollection
        ///// </summary>
        //public void DisableAllEvents()
        //{
        //    Events.AppActive -= new EventHandler<ActiveEventArgs>(Update);
        //    Events.JoystickAxisMotion -= new EventHandler<JoystickAxisEventArgs>(Update);

        //    Events.JoystickBallMotion -= new EventHandler<JoystickBallEventArgs>(Update);
        //    Events.JoystickButtonDown -= new EventHandler<JoystickButtonEventArgs>(Update);
        //    Events.JoystickButtonUp -= new EventHandler<JoystickButtonEventArgs>(Update);
        //    Events.JoystickHatMotion -= new EventHandler<JoystickHatEventArgs>(Update);
        //    Events.KeyboardUp -= new EventHandler<KeyboardEventArgs>(Update);
        //    Events.KeyboardDown -= new EventHandler<KeyboardEventArgs>(Update);
        //    Events.MouseButtonDown -= new EventHandler<MouseButtonEventArgs>(Update);
        //    Events.MouseButtonUp -= new EventHandler<MouseButtonEventArgs>(Update);
        //    Events.MouseMotion -= new EventHandler<MouseMotionEventArgs>(Update);
        //    Events.UserEvent -= new EventHandler<UserEventArgs>(Update);

        //}

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableJoystickAxisEvent()
        {
            Events.JoystickAxisMotion -= new EventHandler<JoystickAxisEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableJoystickBallEvent()
        {
            Events.JoystickBallMotion -= new EventHandler<JoystickBallEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableJoystickButtonEvent()
        {
            Events.JoystickButtonDown -= new EventHandler<JoystickButtonEventArgs>(Update);
            Events.JoystickButtonUp -= new EventHandler<JoystickButtonEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableJoystickButtonDownEvent()
        {
            Events.JoystickButtonDown -= new EventHandler<JoystickButtonEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableJoystickButtonUpEvent()
        {
            Events.JoystickButtonUp -= new EventHandler<JoystickButtonEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableJoystickHatEvent()
        {
            Events.JoystickHatMotion -= new EventHandler<JoystickHatEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableKeyboardEvent()
        {
            Events.KeyboardUp -= new EventHandler<KeyboardEventArgs>(Update);
            Events.KeyboardDown -= new EventHandler<KeyboardEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableKeyboardDownEvent()
        {
            Events.KeyboardDown -= new EventHandler<KeyboardEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableKeyboardUpEvent()
        {
            Events.KeyboardUp -= new EventHandler<KeyboardEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableMouseButtonEvent()
        {
            Events.MouseButtonDown -= new EventHandler<MouseButtonEventArgs>(Update);
            Events.MouseButtonUp -= new EventHandler<MouseButtonEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableMouseButtonDownEvent()
        {
            Events.MouseButtonDown -= new EventHandler<MouseButtonEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableMouseButtonUpEvent()
        {
            Events.MouseButtonUp -= new EventHandler<MouseButtonEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableMouseMotionEvent()
        {
            Events.MouseMotion -= new EventHandler<MouseMotionEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableUserEvent()
        {
            Events.UserEvent -= new EventHandler<UserEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableQuitEvent()
        {
            Events.Quit -= new EventHandler<QuitEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableVideoExposeEvent()
        {
            Events.VideoExpose -= new EventHandler<VideoExposeEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableVideoResizeEvent()
        {
            Events.VideoResize -= new EventHandler<VideoResizeEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableChannelFinishedEvent()
        {
            Events.ChannelFinished -= new EventHandler<ChannelFinishedEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableMusicFinishedEvent()
        {
            Events.MusicFinished -= new EventHandler<MusicFinishedEventArgs>(Update);
        }

        /// <summary>
        /// Disables Event for SpriteCollection
        /// </summary>
        public void DisableTickEvent()
        {
            Events.Tick -= new EventHandler<TickEventArgs>(Update);
        }


        /// <summary>
        /// Processes an active event.
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, ActiveEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a joystick motion event. This event is triggered by
        /// SDL. Only
        /// sprites that are JoystickSensitive are processed.
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, JoystickAxisEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a joystick hat motion event. This event is triggered by
        /// SDL. Only
        /// sprites that are JoystickSensitive are processed.
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, JoystickBallEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a joystick button event. This event is triggered by
        /// SDL. Only
        /// sprites that are JoystickSensitive are processed.
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, JoystickButtonEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a joystick hat motion event. This event is triggered by
        /// SDL. Only
        /// sprites that are JoystickSensitive are processed.
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, JoystickHatEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes the keyboard.
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, KeyboardEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a mouse button. This event is trigger by the SDL
        /// system. 
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, MouseButtonEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a mouse motion event. This event is triggered by
        /// SDL. Only
        /// sprites that are MouseSensitive are processed.
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, MouseMotionEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a Quit event
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, QuitEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a user event
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, UserEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a VideoExposeEvent
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, VideoExposeEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a VideoResizeEvent
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, VideoResizeEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a ChannelFinishedEvent
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, ChannelFinishedEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// Processes a MusicfinishedEvent
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, MusicFinishedEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        /// <summary>
        /// All sprites are tickable, regardless if they actual do
        /// anything. This ensures that the functionality is there, to be
        /// overridden as needed.
        /// </summary>
        /// <param name="sender">Object that sent event</param>
        /// <param name="e">Event arguments</param>
        private void Update(object sender, TickEventArgs e)
        {
            foreach (Sprite s in this)
            {
                s.Update(e);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Removes sprites from all SpriteCollections
        /// </summary>
        public virtual void Kill()
        {
            foreach (Sprite s in this)
            {
                this.lostRects.Add(s.LastBlitRectangle);
                s.Kill();
            }
        }

        /// <summary>
        /// Detects if a given sprite intersects with any sprites in this sprite collection.
        /// </summary>
        /// <param name="sprite">Sprite to intersect with</param>
        /// <returns>
        /// SpriteCollection of sprite in this SpriteCollection that 
        /// intersect with the given Sprite
        /// </returns>
        public virtual SpriteCollection IntersectsWith(Sprite sprite)
        {
            SpriteCollection intersection = new SpriteCollection();
            foreach (Sprite s in this)
            {
                if (s.IntersectsWith(sprite))
                {
                    intersection.Add(s);
                }
            }
            return intersection;
        }

        /// <summary>
        /// Detects if any sprites in a given SpriteCollection 
        /// intersect with any sprites in this SpriteCollection.
        /// </summary>
        /// <param name="spriteCollection">
        /// SpriteCollection to check intersections
        /// </param>
        /// <returns>
        /// Hashtable with sprites in this SpriteCollection as 
        /// keys and SpriteCollections containing sprites they 
        /// intersect with from the given SpriteCollection
        /// </returns>
        public virtual Dictionary<Sprite, Sprite> IntersectsWith(SpriteCollection spriteCollection)
        {
            if (spriteCollection == null)
            {
                throw new ArgumentNullException("SpriteCollection");
            }
            Dictionary<Sprite, Sprite> intersection = new Dictionary<Sprite, Sprite>();
            foreach (Sprite s in this)
            {
                foreach (Sprite t in spriteCollection)
                {
                    if (s.IntersectsWith(t))
                    {
                        if (intersection.ContainsKey(s))
                        {
                            //((SpriteCollection)intersection[s]).Add(t);
                        }
                        else
                        {
                            intersection.Add(s, t);
                        }
                    }
                }
            }
            return intersection;
        }

        #endregion
    }
}
