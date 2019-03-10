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
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;

namespace SdlDotNet.Audio
{
    /// <summary>
    /// Encapulates a Dictionary of Sound objects in a Sound Dictionary.
    /// </summary>
    /// <remarks>
    /// Every sound object within the Dictionary is indexed by a string key.
    /// </remarks>
    /// <example>
    /// <code>
    /// SoundDictionary sounds = new SoundDictionary();
    /// sounds.Add("boom", Mixer.Sound("explosion.wav"));
    /// sounds.Add("boing.wav");
    /// sounds.Add("baseName", ".ogg");
    /// 
    /// sounds["boing.wav"].Play();
    /// sounds["boom"].Play();
    /// sounds["baseName-01.ogg"].Play(); 
    /// </code>
    /// </example>
    /// <seealso cref="Sound"/>
    [Serializable]
    public class SoundDictionary : Dictionary<string, Sound>
    {
        #region Constructors

        /// <summary>
        /// Creates a new SoundDictionary object.
        /// </summary>
        public SoundDictionary()
            : base()
        {
        }

        /// <summary>
        /// Loads a number of files.
        /// </summary>
        /// <param name="baseName">
        /// The string contained before the file index number.
        /// </param>
        /// <param name="extension">The extension of each file.</param>
        public SoundDictionary(string baseName, string extension)
        {
            // Save the fields
            //this.filename = baseName + "-*" + extension;
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
                this.Add(fn, Mixer.Sound(fn));
                i++;
            }
        }

        /// <summary>
        /// Adds the contents of an existing SoundDictionary to a new one.
        /// </summary>
        /// <param name="soundDictionary">
        /// The SoundDictionary to copy.
        /// </param>
        public SoundDictionary(SoundDictionary soundDictionary)
        {
            if (soundDictionary == null)
            {
                throw new ArgumentNullException("soundDictionary");
            }
            IDictionaryEnumerator enumer = soundDictionary.GetEnumerator();
            while (enumer.MoveNext())
            {
                this.Add((string)enumer.Key, (Sound)enumer.Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SoundDictionary(
           SerializationInfo info,
           StreamingContext context) : base(info, context)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an existing SoundDictionary to the Dictionary.
        /// </summary>
        /// <param name="soundDictionary">
        /// The SoundDictionary to add.
        /// </param>
        /// <returns>
        /// The final number of objects within the Dictionary.
        /// </returns>
        public int Add(SoundDictionary soundDictionary)
        {
            if (soundDictionary == null)
            {
                throw new ArgumentNullException("soundDictionary");
            }
            IDictionaryEnumerator dict = soundDictionary.GetEnumerator();
            while (dict.MoveNext())
            {
                this.Add((string)dict.Key, (Sound)dict.Value);
            }
            return this.Count;
        }

        /// <summary>
        /// Stops every sound within the Dictionary.
        /// </summary>
        public void Stop()
        {
            foreach (Sound sound in this.Values)
            {
                sound.Stop();
            }
        }

        /// <summary>
        /// Plays every sound within the Dictionary.
        /// </summary>
        public void Play()
        {
            foreach (Sound sound in this.Values)
            {
                sound.Play();
            }
        }

        /// <summary>
        /// Sets the volume of every sound object within the Dictionary. 
        /// Gets the average volume of all sound 
        /// objects within the Dictionary.
        /// </summary>
        public int Volume
        {
            get
            {
                if (this.Count > 0)
                {
                    int total = 0;
                    foreach (Sound sound in this.Values)
                    {
                        total += sound.Volume;
                    }
                    return total / this.Count;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                foreach (Sound sound in this.Values)
                {
                    sound.Volume = value;
                }
            }
        }

        #endregion
    }
}
