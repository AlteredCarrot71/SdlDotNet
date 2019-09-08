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
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using SdlDotNet.Graphics;
using SdlDotNet.Core;
//using Tao.Sdl;

namespace SdlDotNet.Audio
{
    #region AudioFormat

    /// <summary>
    /// Specifies an audio format to mix audio in
    /// </summary>
    /// <remarks></remarks>
    public enum AudioFormat
    {
        /// <summary>
        /// None. required by FXCop
        /// </summary>
        None = 0,
        /// <summary>
        /// Unsigned 8-bit
        /// </summary>
        Unsigned8 = Sdl.AUDIO_U8,
        /// <summary>
        /// Signed 8-bit
        /// </summary>
        Signed8 = Sdl.AUDIO_S8,
        /// <summary>
        /// Unsigned 16-bit, little-endian
        /// </summary>
        Unsigned16Little = Sdl.AUDIO_U16LSB,
        /// <summary>
        /// Signed 16-bit, little-endian
        /// </summary>
        Signed16Little = Sdl.AUDIO_S16LSB,
        /// <summary>
        /// Unsigned 16-bit, big-endian
        /// </summary>
        Unsigned16Big = Sdl.AUDIO_U16MSB,
        /// <summary>
        /// Signed 16-bit, big-endian
        /// </summary>
        Signed16Big = Sdl.AUDIO_S16MSB,
        /// <summary>
        /// Default, equal to Signed16Little
        /// </summary>
        Default = Sdl.AUDIO_S16LSB
    }

    #endregion

    #region FadingStatus

    /// <summary>
    /// Indicates the current fading status of a sound
    /// </summary>
    /// <remarks></remarks>
    public enum FadingStatus
    {
        /// <summary>
        /// Sound is not fading
        /// </summary>
        NoFading = SdlMixer.MIX_NO_FADING,
        /// <summary>
        /// Sound is fading out
        /// </summary>
        FadingOut = SdlMixer.MIX_FADING_OUT,
        /// <summary>
        /// Sound is fading in
        /// </summary>
        FadingIn = SdlMixer.MIX_FADING_IN
    }

    #endregion

    #region SoundChannel

    /// <summary>
    /// Type of sound channel
    /// </summary>
    /// <remarks></remarks>
    public enum SoundChannel
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Mono
        /// </summary>
        Mono = 1,
        /// <summary>
        /// Stereo
        /// </summary>
        Stereo = 2
    }

    #endregion

    #region CDTrackType

    /// <summary>
    /// CD Track Type
    /// </summary>
    /// <remarks></remarks>
    [SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "Not flags")]
    public enum CDTrackType
    {
        /// <summary>
        /// Audio
        /// </summary>
        Audio = Sdl.SDL_AUDIO_TRACK,
        /// <summary>
        /// Data
        /// </summary>
        Data = Sdl.SDL_DATA_TRACK
    }

    #endregion

    #region AudioStatus

    /// <summary>
    /// Audio playing status
    /// </summary>
    public enum AudioStatus
    {
        /// <summary>
        /// Audio is not playing
        /// </summary>
        Stopped,
        /// <summary>
        /// Audio is paused
        /// </summary>
        Playing,
        /// <summary>
        /// Audio is currently playing
        /// </summary>
        Paused
    }

    #endregion

    #region CDstatus

    /// <summary>
    /// The possible states which a CD-ROM drive can be in.
    /// </summary>
    /// <remarks></remarks>
    public enum CDStatus
    {
        /// <summary>
        /// The CD tray is empty.
        /// </summary>
        TrayEmpty = Sdl.CD_TRAYEMPTY,
        /// <summary>
        /// The CD has stopped playing.
        /// </summary>
        Stopped = Sdl.CD_STOPPED,
        /// <summary>
        /// The CD is playing.
        /// </summary>
        Playing = Sdl.CD_PLAYING,
        /// <summary>
        /// The CD has been paused.
        /// </summary>
        Paused = Sdl.CD_PAUSED,
        /// <summary>
        /// An error occured while getting the status.
        /// </summary>
        Error = Sdl.CD_ERROR
    }

    #endregion CDstatus

    #region SoundAction

    /// <summary>
    /// SoundAction
    /// </summary>
    /// <remarks></remarks>
    public enum SoundAction
    {
        /// <summary>
        /// Stop sound
        /// </summary>
        Stop,
        /// <summary>
        /// Fadeout sound
        /// </summary>
        Fadeout
    }

    #endregion

    #region MusicType

    /// <summary>
    /// MusicType
    /// </summary>
    /// <remarks></remarks>
    public enum MusicType
    {
        /// <summary>
        /// None
        /// </summary>
        None = SdlMixer.MUS_NONE,
        /// <summary>
        /// Starts external player
        /// </summary>
        ExternalCommand = SdlMixer.MUS_CMD,
        /// <summary>
        /// .WAV file
        /// </summary>
        Wave = SdlMixer.MUS_WAV,
        /// <summary>
        /// Mod music file
        /// </summary>
        Mod = SdlMixer.MUS_MOD,
        /// <summary>
        /// MIDI file
        /// </summary>
        Midi = SdlMixer.MUS_MID,
        /// <summary>
        /// Ogg file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        Ogg = SdlMixer.MUS_OGG,
        /// <summary>
        /// mp3 file
        /// </summary>
        Mp3 = SdlMixer.MUS_MP3
    }

    #endregion

    #region Public Delegates
    /// <summary>
    /// Used in the SDL_AudioSpec struct
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AudioCallback(IntPtr userData, IntPtr stream, int len);
    #endregion Public Delegates

    /// <summary>
    /// Provides methods to access the sound system.
    /// You can obtain an instance of this class by accessing the 
    /// Mixer property of the main Sdl object.
    /// </summary>
    /// <remarks>
    /// Before instantiating an instance of Movie,
    /// you must call Mixer.Close() to turn off the default mixer.
    /// If you do not do this, any movie will play very slowly. 
    /// Smpeg uses a custom mixer for audio playback. 
    /// </remarks>
    public static class Mixer
    {
        #region Private fields

        const int DEFAULT_CHUNK_SIZE = 1024;
        const int DEFAULT_NUMBER_OF_CHANNELS = 8;
        static private byte distance;
        static bool isInitialized = Initialize();
        static bool isOpen;
        static bool audioOpen;

        /// <summary>
        /// 
        /// </summary>
        public static bool AudioOpen
        {
            get { return Mixer.audioOpen; }
            set { Mixer.audioOpen = value; }
        }

        static bool audioLocked;

        /// <summary>
        /// 
        /// </summary>
        public static bool AudioLocked
        {
            get { return Mixer.audioLocked; }
            set { Mixer.audioLocked = value; }
        }

        #endregion

        #region Private Methods

        internal static void OpenInternal()
        {
            if (!isOpen)
            {
                SdlMixer.Mix_OpenAudio(SdlMixer.MIX_DEFAULT_FREQUENCY,
                    unchecked((short)AudioFormat.Default),
                    (int)SoundChannel.Stereo,
                    DEFAULT_CHUNK_SIZE);
                ChannelsAllocated = DEFAULT_NUMBER_OF_CHANNELS;
                isOpen = true;
            }
        }

        //static void CheckOpenStatus()
        //{
        //    if (!audioOpen)
        //    {
        //        throw new AudioException(Events.StringManager.GetString("OpenAudioNotInit", CultureInfo.CurrentUICulture));
        //    }
        //}

        internal static void CheckOpenStatus(AudioStream stream)
        {
            if (!audioOpen)
            {
                Mixer.OpenAudio(stream);
                audioOpen = true;
            }
        }

        private static void PrivateOpen(
            int frequency, AudioFormat format, SoundChannel soundChannels, int chunksize)
        {
            if (!isOpen)
            {
                SdlMixer.Mix_OpenAudio(frequency, (short)format, (int)soundChannels, chunksize);
                isOpen = true;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Open Audio device
        /// </summary>
        /// <param name="stream">stream to open</param>
        public static void OpenAudio(AudioStream stream)
        {
            Close();
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            IntPtr pSpec = Marshal.AllocHGlobal(Marshal.SizeOf(stream.Spec));
            try
            {
                Marshal.StructureToPtr(stream.Spec, pSpec, false);

                if (Sdl.SDL_OpenAudio(pSpec, IntPtr.Zero) < 0)
                {
                    throw new AudioException();
                }

                stream.Spec = (Sdl.SDL_AudioSpec)Marshal.PtrToStructure(pSpec, typeof(Sdl.SDL_AudioSpec));

                if (((ushort)stream.Spec.format & 0x8000) == 0x8000)    // signed
                {
                    stream.Offset = 0;
                }
                else
                {
                    stream.Offset = 2 << ((byte)stream.Spec.format - 2);
                }
                Mixer.AudioOpen = true;
            }
            finally
            {
                Marshal.FreeHGlobal(pSpec);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsInitialized
        {
            get { return Mixer.isInitialized; }
        }

        /// <summary>
        /// Closes and destroys this object
        /// </summary>
        public static void Close()
        {
            isOpen = false;
            Events.CloseMixer();
        }

        /// <summary>
        /// Gets or sets the locked status of the audio subsystem.  Necessary when data is 
        /// shared between the <see cref="Sdl.AudioSpecCallbackDelegate">callback</see> and the main thread.
        /// </summary>
        public static bool Locked
        {
            get
            {
                return audioLocked;
            }

            set
            {
                audioLocked = value;
                if (value)
                {
                    Sdl.SDL_LockAudio();
                }
                else
                {
                    Sdl.SDL_UnlockAudio();
                }
            }
        }

        ///// <summary>
        ///// Returns the current playback state of the audio subsystem.  See <see cref="AudioStatus"/>.
        ///// </summary>
        //public static AudioStatus AudioStatus
        //{
        //    get
        //    {
        //        //CheckOpenStatus();

        //        return (AudioStatus)Sdl.SDL_GetAudioStatus();
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the paused state of the audio subsystem.
        ///// </summary>
        //public static bool Paused
        //{
        //    get
        //    {
        //        //CheckOpenStatus();

        //        return AudioStatus != AudioStatus.Playing;
        //    }

        //    set
        //    {
        //        //CheckOpenStatus();

        //        Sdl.SDL_PauseAudio(value ? (int)PauseAction.Pause : (int)PauseAction.UnPause);
        //    }
        //}

        /// <summary>
        /// Returns whether the audio subsystem is open or not.
        /// </summary>
        public static bool IsOpen
        {
            get
            {
                return audioOpen;
            }
        }

        /// <summary>
        /// Start Mixer subsystem
        /// </summary>
        public static bool Initialize()
        {
            Video.Initialize(); //If this is not here, the Mixer will not be properly initialized.
            if (Sdl.SDL_Init(Sdl.SDL_INIT_AUDIO) != 0)
            {
                //throw SdlException.Generate();
                return false;
            }
            else
            {
                Mixer.OpenInternal();
                return true;
            }
        }

        //		/// <summary>
        //		/// Queries if the Mixer subsystem has been intialized.
        //		/// </summary>
        //		/// <remarks>
        //		/// </remarks>
        //		/// <returns>True if Mixer subsystem has been initialized, false if it has not.</returns>
        //		public static bool IsInitialized
        //		{
        //			get
        //			{
        //				if ((Sdl.SDL_WasInit(Sdl.SDL_INIT_AUDIO) & Sdl.SDL_INIT_AUDIO) 
        //					!= (int) SdlFlag.FalseValue)
        //				{
        //					return true;
        //				}
        //				else 
        //				{
        //					return false;
        //				}
        //			}
        //		}

        /// <summary>
        /// Re-opens the sound system with default values.  
        /// You do not have to call this method
        /// in order to start using the Mixer object.
        /// </summary>
        public static void Open()
        {
            Close();
            OpenInternal();
        }

        /// <summary>
        /// Re-opens the sound-system. You do not have to call this method
        /// in order to start using the Mixer object.
        /// </summary>
        /// <param name="frequency">The frequency to mix at</param>
        /// <param name="format">The audio format to use</param>
        /// <param name="soundChannels">
        /// Number of sound channels in output.  
        /// Set to SoundChannel.Stereo for stereo, SoundChannel.Mono for mono. 
        /// This has nothing to do with mixing channels.
        /// </param>
        /// <param name="chunkSize">The chunk size for samples</param>
        public static void Open(int frequency, AudioFormat format, SoundChannel soundChannels, int chunkSize)
        {
            Close();
            PrivateOpen(frequency, format, soundChannels, chunkSize);
        }

        /// <summary>
        /// Creates sound channel
        /// </summary>
        /// <param name="index">Index of new channel</param>
        /// <returns>new Channel</returns>
        public static Channel CreateChannel(int index)
        {
            if (index < 0 || index >= Mixer.ChannelsAllocated)
            {
                throw new SdlException();
            }
            else
            {
                return new Channel(index);
            }
        }

        /// <summary>
        /// Loads a .wav, .ogg, .mp3, .mod or .mid file into memory
        /// </summary>
        /// <param name="file">sound file name</param>
        /// <returns>Sound object</returns>
        public static Sound Sound(string file)
        {
            return new Sound(file);
        }

        /// <summary>
        /// Loads a .wav, .ogg, .mp3, .mod or .mid  file into memory
        /// </summary>
        /// <param name="file">The filename to load</param>
        /// <param name="size">Output long variable for the size of the sound object.</param>
        /// <returns>A new Sound object</returns>
        internal static IntPtr Load(string file, out long size)
        {
            IntPtr p = SdlMixer.Mix_LoadWAV_RW(Sdl.SDL_RWFromFile(file, "rb"), 1);
            if (p == IntPtr.Zero)
            {
                throw SdlException.Generate();
            }
            size = new FileInfo(file).Length;
            return p;
        }

        /// <summary>
        /// Loads a .wav, .ogg, .mp3, .mod or .mid file from a byte array
        /// </summary>
        /// <param name="data">The data to load</param>
        /// <returns>A new Sound object</returns>
        public static Sound Sound(byte[] data)
        {
            return new Sound(data);
        }

        /// <summary>
        /// Loads a .wav, .ogg, .mp3, .mod or .mid file from a byte array
        /// </summary>
        /// <param name="data">The data to load</param>
        /// <param name="size">Output variable for the size of the sound object.</param>
        /// <returns>A new Sound object</returns>
        internal static IntPtr Load(byte[] data, out long size)
        {
            IntPtr p = SdlMixer.Mix_LoadWAV_RW(Sdl.SDL_RWFromMem(data, data.Length), 1);
            if (p == IntPtr.Zero)
            {
                throw SdlException.Generate();
            }
            size = data.Length;
            return p;
        }

        /// <summary>
        /// Loads a music sample from a filename returning the pointer to the sample.
        /// </summary>
        /// <param name="filename">The file path to load.</param>
        /// <returns>The IntPtr handle to the music sample in memory.</returns>
        /// <exception cref="SdlException">Thrown if an error occurs when loading the sample.</exception>
        internal static IntPtr LoadMusic(string filename)
        {
            IntPtr handle = SdlMixer.Mix_LoadMUS(filename);
            if (handle == IntPtr.Zero)
            {
                throw SdlException.Generate();
            }
            return handle;
        }

        /// <summary>
        /// Loads a music sample from a byte array returning the pointer to the sample.
        /// </summary>
        /// <param name="data">data buffer to load.</param>
        /// <returns>The IntPtr handle to the music sample in memory.</returns>
        /// <exception cref="SdlException">Thrown if an error occurs when loading the sample.</exception>
        internal static IntPtr LoadMusic(byte[] data)
        {
            IntPtr handle = SdlMixer.Mix_LoadMUS_RW(Sdl.SDL_RWFromMem(data, data.Length));
            if (handle == IntPtr.Zero)
            {
                throw SdlException.Generate();
            }
            return handle;
        }

        /// <summary>
        /// Changes the number of channels allocated for mixing
        /// </summary>
        /// <returns>The number of channels allocated</returns>
        public static int ChannelsAllocated
        {
            get
            {
                return SdlMixer.Mix_AllocateChannels(-1);
            }
            set
            {
                SdlMixer.Mix_AllocateChannels(value);
            }
        }

        /// <summary>
        /// These channels will be resrved
        /// </summary>
        /// <param name="numberOfChannels">number of channels to reserve</param>
        /// <returns>
        /// Number of channels actually reserved. This may be fewer than the number requested.
        /// </returns>
        public static int ReserveChannels(int numberOfChannels)
        {
            return SdlMixer.Mix_ReserveChannels(numberOfChannels);
        }

        /// <summary>
        /// Stop reserving any channels.
        /// </summary>
        public static void CancelReserveChannels()
        {
            SdlMixer.Mix_ReserveChannels(0);
        }

        /// <summary>
        /// Returns the index of an available channel
        /// </summary>
        /// <returns>Index of available channel</returns>
        public static int FindAvailableChannel()
        {
            return SdlMixer.Mix_GroupAvailable(-1);
        }

        /// <summary>
        /// Sets the volume for all channels
        /// </summary>
        /// <param name="volume">A new volume value, between 0 and 128 inclusive</param>
        /// <returns>New average channel volume</returns>
        public static int SetAllChannelsVolume(int volume)
        {
            return SdlMixer.Mix_Volume(-1, volume);
        }

        /// <summary>
        /// Pauses playing on all channels
        /// </summary>
        public static void Pause()
        {
            SdlMixer.Mix_Pause(-1);
        }

        /// <summary>
        /// Resumes playing on all paused channels
        /// </summary>
        public static void Resume()
        {
            SdlMixer.Mix_Resume(-1);
        }

        /// <summary>
        /// Stop playing on all channels
        /// </summary>
        public static void Stop()
        {
            SdlMixer.Mix_HaltChannel(-1);
        }

        /// <summary>
        /// Stop playing on all channels after a specified time interval
        /// </summary>
        /// <param name="milliseconds">
        /// The number of milliseconds to stop playing after
        /// </param>
        public static void Expire(int milliseconds)
        {
            SdlMixer.Mix_ExpireChannel(-1, milliseconds);
        }

        /// <summary>
        /// Fades out all channels
        /// </summary>
        /// <param name="milliseconds">
        /// The number of milliseconds to fade out for
        /// </param>
        /// <returns>The number of channels fading out</returns>
        public static int Fadeout(int milliseconds)
        {
            return SdlMixer.Mix_FadeOutChannel(-1, milliseconds);
        }

        /// <summary>
        /// Returns the number of currently playing channels
        /// </summary>
        /// <returns>The number of channels playing</returns>
        public static int NumberOfChannelsPlaying()
        {
            return SdlMixer.Mix_Playing(-1);
        }

        /// <summary>
        /// Returns the number of paused channels
        /// </summary>
        /// <remarks>
        /// Number of channels paused.
        /// </remarks>
        /// <returns>The number of channels paused</returns>
        public static int NumberOfChannelsPaused()
        {
            return SdlMixer.Mix_Paused(-1);
        }

        /// <summary>
        /// Sets the panning (stereo attenuation) for all channels
        /// </summary>
        /// <param name="left">
        /// A left speaker value from 0-255 inclusive
        /// </param>
        /// <param name="right">
        /// A right speaker value from 0-255 inclusive
        /// </param>
        public static void SetPanning(int left, int right)
        {
            if (SdlMixer.Mix_SetPanning(-1, (byte)left, (byte)right) == 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Sets the distance (attenuate sounds based on distance 
        /// from listener) for all channels
        /// </summary>
        public static byte Distance
        {
            set
            {
                if (SdlMixer.Mix_SetDistance(-1, value) == 0)
                {
                    throw SdlException.Generate();
                }
                distance = value;
            }
            get
            {
                return distance;
            }
        }

        /// <summary>
        /// Sets the "position" of a sound (approximate '3D' audio) 
        /// for all channels
        /// </summary>
        /// <param name="angle">The angle of the sound, between 0 and 359,
        ///  0 = directly in front</param>
        /// <param name="distance">
        /// The distance of the sound from 0-255 inclusive
        /// </param>
        public static void SetPosition(int angle, int distance)
        {
            if (SdlMixer.Mix_SetPosition(-1, (short)angle, (byte)distance) == 0)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Flips the left and right stereo for all channels
        /// </summary>
        /// <param name="flip">True to flip, False to reset to normal</param>
        public static void ReverseStereo(bool flip)
        {
            if (SdlMixer.Mix_SetReverseStereo(-1, flip ? 1 : 0) == 0)
            {
                throw SdlException.Generate();
            }
        }

        #endregion
    }
}
