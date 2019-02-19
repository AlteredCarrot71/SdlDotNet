#region LICENSE
/*
 * Copyright (C) 2005 Rob Loach (http://www.robloach.net)
 * Copyright (C) 2007 David Hudson
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
using System.IO;

namespace SdlDotNet.Audio
{
    /// <summary>
    /// Encapsulates the collection of Music objects.
    /// </summary>
    public class MusicCollection : Collection<Music>
    {
        #region Constructor

        /// <summary>
        /// Creates an empty List
        /// </summary>
        public MusicCollection()
        {
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Makes each music item in the collection queue the next item in the collection.
        /// </summary>
        /// <remarks>You must call <see cref="MusicPlayer.EnableMusicFinishedCallback"/>() to enable queueing.</remarks>
        public void CreateQueueList()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].QueuedMusic = this[i + 1];
                // Note that the last element refers to the first element because of the this indexer
            }
        }

        /// <summary>
        /// Makes a music collection from the files held within the given directory.
        /// </summary>
        /// <remarks>Doesn't throw an exception when attempting to load from an invalid file format within the directory.</remarks>
        /// <param name="dir">The path to the directory to load.</param>
        /// <returns>The new MusicCollection that was created.</returns>
        public static MusicCollection AddFromDirectory(string dir)
        {
            DirectoryInfo directory = new DirectoryInfo(dir);
            FileInfo[] files = directory.GetFiles();
            MusicCollection collection = new MusicCollection();
            foreach (FileInfo file in files)
            {
                try
                {
                    collection.Add(new Music(file.FullName));
                }
                catch (System.IO.FileNotFoundException e)
                {
                    e.ToString();
                    // Do nothing and move onto next file
                }
            }
            return collection;
        }

        #endregion Public Methods
    }
}
