#region LICENSE
/*
 * Copyright (C) 2004-2007 David Hudson (jendave@yahoo.com)
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

using SdlDotNet.Core;
using Tao.Sdl;

namespace SdlDotNet.Audio
{
    /// <summary>
    /// Represents a music sample.  Music is generally longer than a sound effect sample,
    /// however it can also be compressed e.g. by Ogg Vorbis
    /// </summary>
    /// <example>
    /// <code>
    /// Music techno = new Music("techno.mp3");
    /// Music jazz = new Music("jazz.mid");
    /// 
    /// techno.Play();
    /// 
    /// if(Music.CurrentMusic == techno)
    ///		jazz.Play();
    ///		
    ///	Music.FadeOut(1000);
    ///	Music.Volume = 50;
    ///	
    ///	techno.FadeIn(1, 500);
    ///	techno.QueuedMusic = jazz;  // Play jazz after techno finishes playing
    ///	jazz.QueuedMusic = techno;  // Play techno after jazz finishes playing
    ///	
    ///	Music.EnableMusicFinishedCallback(); // Enable processing queues.
    /// </code>
    /// </example>
    public class Music : BaseSdlResource
    {
        #region Private fields

        string m_FileName = "";
        Music m_QueuedMusic;
        static bool isInitialized = Mixer.Initialize();

        #endregion

        #region Public methods

        /// <summary>
        /// Has the music system been initialized
        /// </summary>
        public static bool IsInitialized
        {
            get { return Music.isInitialized; }
        }

        /// <summary>
        /// Gets the filename of the music sample.
        /// </summary>
        public string FileName
        {
            get
            {
                return m_FileName;
            }
        }

        /// <summary>
        /// Gets and sets the next queued music sample after this one completes.
        /// </summary>
        /// <remarks>
        /// You must call Music.EnableMusicFinishedCallback before this can work.
        /// </remarks>
        public Music QueuedMusic
        {
            get
            {
                return m_QueuedMusic;
            }
            set
            {
                m_QueuedMusic = value;
            }
        }

        /// <summary>
        /// Loads a music sample from a file.
        /// </summary>
        /// <param name="fileName">The file path to load from.</param>
        public Music(string fileName)
        {
            Mixer.OpenInternal();
            this.Handle = Mixer.LoadMusic(fileName);
            m_FileName = fileName;
        }

        /// <summary>
        /// Loads a music sample from a byte array.
        /// </summary>
        /// <param name="data">data buffer</param>
        public Music(byte[] data)
        {
            Mixer.OpenInternal();
            this.Handle = Mixer.LoadMusic(data);
        }

        /// <summary>
        /// Closes Music handle
        /// </summary>
        protected override void CloseHandle()
        {
            try
            {
                if (this.Handle != IntPtr.Zero)
                {
                    SdlMixer.Mix_FreeMusic(this.Handle);
                    this.Handle = IntPtr.Zero;
                }
            }
            catch (NullReferenceException)
            {
            }
            catch (AccessViolationException)
            {
            }
            finally
            {
                this.Handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Plays the music sample
        /// </summary>
        public void Play()
        {
            Play(1);
        }

        /// <summary>
        /// Plays the music sample
        /// </summary>
        public void Play(bool continuous)
        {
            if (continuous == true)
            {
                Play(-1);
            }
            else
            {
                Play(1);
            }
        }

        /// <summary>
        /// Plays the music sample
        /// </summary>
        /// <param name="numberOfTimes">
        /// The number of times to play. 
        /// Specify 1 to play a single time, -1 to loop forever.
        /// </param>
        public void Play(int numberOfTimes)
        {
            try
            {
                MusicPlayer.CurrentMusic = this;
                MusicPlayer.Play(numberOfTimes);
            }
            catch (DivideByZeroException)
            {
                // Linux audio problem
            }
        }

        /// <summary>
        /// Plays the music sample and fades it in
        /// </summary>
        /// <param name="numberOfTimes">
        /// The number of times to play. 
        /// Specify 1 to play a single time, -1 to loop forever.
        /// </param>
        /// <param name="milliseconds">
        /// The number of milliseconds to fade in for
        /// </param>
        public void FadeIn(int numberOfTimes, int milliseconds)
        {
            MusicPlayer.CurrentMusic = this;
            if (SdlMixer.Mix_FadeInMusic(this.Handle, numberOfTimes, milliseconds) != 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Plays the music sample, starting from a specific 
        /// position and fades it in
        /// </summary>
        /// <param name="numberOfTimes">
        /// The number of times to play.
        ///  Specify 1 to play a single time, -1 to loop forever.
        ///  </param>
        /// <param name="milliseconds">
        /// The number of milliseconds to fade in for
        /// </param>
        /// <param name="position">
        /// A format-defined position value. 
        /// For Ogg Vorbis, this is the number of seconds from the
        ///  beginning of the song
        ///  </param>
        public void FadeInPosition(int numberOfTimes, int milliseconds, double position)
        {
            MusicPlayer.CurrentMusic = this;
            if (SdlMixer.Mix_FadeInMusicPos(this.Handle,
                numberOfTimes, milliseconds, position) != 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Gets the format of the music data type.
        /// </summary>
        public MusicType MusicType
        {
            get
            {
                return (MusicType)SdlMixer.Mix_GetMusicType(this.Handle);
            }
        }

        /// <summary>
        /// Returns a System.String that represents the current Music object.
        /// </summary>
        /// <returns>
        /// A System.String that represents the Music object (the filename).
        /// </returns>
        public override string ToString()
        {
            return m_FileName;
        }

        #endregion
    }
}