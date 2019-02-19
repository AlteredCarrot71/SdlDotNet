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
using System.IO;
using System.Runtime.Serialization;

namespace SdlDotNet.Audio
{
	/// <summary>
	/// Represents a collection of music samples 
	/// held together by a dictionary key-value base.
	/// </summary>
	/// <example>
	/// <code>
	/// MusicCollection tunes = new MusicCollection();
	/// 
	/// tunes.Add("techno", "techno.mid");
	/// tunes.Add("jazz.mid");
	/// 
	/// tunes["techo"].Play();
	/// tunes["jazz.mid"].Play();
	/// </code>
	/// </example>
    [Serializable]
    public class MusicDictionary : Dictionary<string, Music>
    {
        #region Constructor

        /// <summary>
        /// Creates an empty dictionary
        /// </summary>
        public MusicDictionary()
        {
        }

        /// <summary>
        /// Loads multiple files from a directory into the collection.
        /// </summary>
        /// <param name="baseName">
        /// The name held before the file index.
        /// </param>
        /// <param name="extension">
        /// The extension of the files (.mp3)
        /// </param>
        public virtual void Add(string baseName, string extension)
        {
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
                this.Add(fn, new Music(fn));
                i++;
            }
        }

        /// <summary>
        /// Creates a new MusicCollection with the contents 
        /// of an existing MusicCollection.
        /// </summary>
        /// <param name="musicDictionary">
        /// The existing music collection to add.
        /// </param>
        public virtual void Add(MusicDictionary musicDictionary)
        {
            if (musicDictionary == null)
            {
                throw new ArgumentNullException("musicDictionary");
            }
            IDictionaryEnumerator enumer = musicDictionary.GetEnumerator();
            while (enumer.MoveNext())
            {
                this.Add((string)enumer.Key, (Music)enumer.Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected MusicDictionary(
           SerializationInfo info,
           StreamingContext context) : base(info, context)
        {
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Makes all items in the collection 
        /// queued to each other for a playlist effect.
        /// </summary>
        public void CreateQueueList()
        {
            IDictionaryEnumerator enumer = this.GetEnumerator();
            Music currItem = null;
            Music prevItem = null;
            while(enumer.MoveNext())
            {
                currItem = (Music)enumer.Value;
				if (prevItem != null)
				{
					prevItem.QueuedMusic = currItem;
				}
                prevItem = currItem;
            }
        }

        #endregion Public Methods
    }
}
