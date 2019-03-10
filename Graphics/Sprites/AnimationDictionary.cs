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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SdlDotNet.Graphics.Sprites
{
    /// <summary>
    /// Summary description for Animation.
    /// </summary>
    [Serializable]
    public class AnimationDictionary : Dictionary<string, AnimationCollection>
    {
        #region Constructors
        /// <summary>
        /// Creates an empty AnimationDictionary.
        /// </summary>
        public AnimationDictionary()
            : base()
        {
        }

        /// <summary>
        /// Creates an AnimationDictionary with one animation with the key "Default".
        /// </summary>
        /// <param name="animation"></param>
        public AnimationDictionary(AnimationCollection animation)
        {
            this.Add("Default", animation);
        }

        /// <summary>
        /// Creates an AnimationDictionary with one element within it.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="animation"></param>
        public AnimationDictionary(string key, AnimationCollection animation)
        {
            this.Add(key, animation);
        }

        ///// <summary>
        ///// Creates an AnimationDictionary with a "Default" animation of surfaces.
        ///// </summary>
        ///// <param name="surfaces"></param>
        //public AnimationDictionary(SurfaceCollection surfaces)
        //{
        //    this.Add("Default", surfaces);
        //}


        /// <summary>
        /// Creates a new AnimationDictionary with the contents of an existing AnimationDictionary.
        /// </summary>
        /// <param name="animationDictionary">The existing music Dictionary to add.</param>
        public AnimationDictionary(AnimationDictionary animationDictionary)
        {
            this.Add(animationDictionary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected AnimationDictionary(
           SerializationInfo info,
           StreamingContext context) : base(info, context)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the average delay of all animations in the Dictionary, 
        /// sets the delay of every animation in the Dictionary.
        /// </summary>
        public double Delay
        {
            get
            {
                double average = 0;
                IDictionaryEnumerator dict = this.GetEnumerator();
                while (dict.MoveNext())
                {
                    average += ((AnimationCollection)dict.Value).Delay;
                }
                return average / this.Count;
            }
            set
            {
                IDictionaryEnumerator dict = this.GetEnumerator();
                while (dict.MoveNext())
                {
                    ((AnimationCollection)dict.Value).Delay = value;
                }
            }
        }

        /// <summary>
        /// Gets the average FrameIncrement for each AnimationCollection.  
        /// Sets the FrameIncrement of each AnimationCollection in the Dictionary.
        /// </summary>
        public int FrameIncrement
        {
            get
            {
                int average = 0;
                IDictionaryEnumerator dict = this.GetEnumerator();
                while (dict.MoveNext())
                {
                    average += ((AnimationCollection)dict.Value).FrameIncrement;
                }
                return average / this.Count;
            }
            set
            {
                IDictionaryEnumerator dict = this.GetEnumerator();
                while (dict.MoveNext())
                {
                    ((AnimationCollection)dict.Value).FrameIncrement = value;
                }
            }
        }


        /// <summary>
        /// Gets whether all animations animate forward and 
        /// sets whether all animations in the Dictionary 
        /// are to animate forward.
        /// </summary>
        public bool AnimateForward
        {
            get
            {
                IDictionaryEnumerator dict = this.GetEnumerator();
                while (dict.MoveNext())
                {
                    if (!((AnimationCollection)dict.Value).AnimateForward)
                    {
                        return false;
                    }
                }
                return true;
            }
            set
            {
                IDictionaryEnumerator dict = this.GetEnumerator();
                while (dict.MoveNext())
                {
                    ((AnimationCollection)dict.Value).AnimateForward = value;
                }
            }
        }

        /// <summary>
        /// Gets whether the first animation is looping, sets whether every animation in the Dictionary is to be looped.
        /// </summary>
        public bool Loop
        {
            get
            {
                IDictionaryEnumerator dict = this.GetEnumerator();
                while (dict.MoveNext())
                {
                    return ((AnimationCollection)dict.Value).Loop;
                }
                return true;
            }
            set
            {
                IDictionaryEnumerator dict = this.GetEnumerator();
                while (dict.MoveNext())
                {
                    ((AnimationCollection)dict.Value).Loop = value;
                }
            }
        }
        #endregion Properties

        #region Functions

        ///// <summary>
        ///// Adds a surface Dictionary to the Dictionary as an animation.
        ///// </summary>
        ///// <param name="key">The name of the animation.</param>
        ///// <param name="surfaces">The SurfaceDictionary that represents the animation.</param>
        ///// <returns>The final number of elements within the Dictionary.</returns>
        //public int Add(string key, SurfaceCollection surfaces)
        //{
        //    this.Add(key, new AnimationCollection(surfaces));
        //    return this.Count;
        //}

        /// <summary>
        /// Adds a Dictionary of music to the current music Dictionary.
        /// </summary>
        /// <param name="animationDictionary">
        /// The Dictionary of 
        /// music samples to add.
        /// </param>
        /// <returns>
        /// The total number of elements within 
        /// the Dictionary after adding the sample.
        /// </returns>
        public int Add(AnimationDictionary animationDictionary)
        {
            if (animationDictionary == null)
            {
                throw new ArgumentNullException("animationDictionary");
            }
            IDictionaryEnumerator dict = animationDictionary.GetEnumerator();
            while (dict.MoveNext())
            {
                this.Add((string)dict.Key, (AnimationCollection)dict.Value);
            }
            return this.Count;
        }

        #endregion Functions

        #region IDictionary Members

        /// <summary>
        /// Provide the explicit interface member for IDictionary.
        /// </summary>
        /// <param name="array">Array to copy Dictionary to</param>
        /// <param name="index">Index at which to insert the Dictionary items</param>
        public virtual void CopyTo(AnimationCollection[] array, int index)
        {
            ((IDictionary)this).CopyTo(array, index);
        }

        /// <summary>
        /// Insert a item into the Dictionary
        /// </summary>
        /// <param name="index">Index at which to insert the item</param>
        /// <param name="animation">item to insert</param>
        public virtual void Insert(int index, AnimationCollection animation)
        {
            this.Insert(index, animation);
        }

        /// <summary>
        /// Gets the index of the given item in the Dictionary.
        /// </summary>
        /// <param name="animation">The item to search for.</param>
        /// <returns>The index of the given sprite.</returns>
        public virtual int IndexOf(AnimationCollection animation)
        {
            return this.IndexOf(animation);
        }

        #endregion
    }
}
