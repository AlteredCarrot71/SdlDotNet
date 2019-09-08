#region LICENSE
/*
 * Copyright (C) 2006 Stuart Carnie (stuart.carnie@gmail.com)
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
using System.Collections.Generic;
using System.Runtime.InteropServices;

using SdlDotNet.Core;
//using Tao.Sdl;

namespace SdlDotNet.Audio
{
    enum PauseAction
    {
        UnPause,
        Pause
    }
    /// <summary>
    /// An active audio stream for queueing audio data to be played asynchronously.
    /// </summary>
    public class AudioStream : MemoryStream
    {
        #region Private fields

        short samples;
        Queue<short[]> queue;
        int sampleFrequency;
        int samplesInQueue;
        Sdl.SDL_AudioSpec spec;

        internal Sdl.SDL_AudioSpec Spec
        {
            get { return spec; }
            set { spec = value; }
        }
        int offset;

        /// <summary>
        /// 
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Returns the current playback state of the audio subsystem.  See <see cref="AudioStatus"/>.
        /// </summary>
        public static AudioStatus AudioStatus
        {
            get
            {
                return (AudioStatus)Sdl.SDL_GetAudioStatus();
            }
        }

        /// <summary>
        /// Gets or sets the paused state of the audio subsystem.
        /// </summary>
        public bool Paused
        {
            get
            {
                Mixer.CheckOpenStatus(this);

                return AudioStatus != AudioStatus.Playing;
            }

            set
            {
                Mixer.CheckOpenStatus(this);

                Sdl.SDL_PauseAudio(value ? (int)PauseAction.Pause : (int)PauseAction.UnPause);
            }
        }

        /// <summary>
        /// Returns the format of the stream
        /// </summary>
        public short Bits
        {
            get { return spec.format; }
        }
        #endregion Private fields

        #region Constructors and Destructors

        /// <summary>
        /// Creates an AudioStream
        /// </summary>
        /// <param name="sampleFrequency">Frequency</param>
        /// <param name="format">format of stream data</param>
        /// <param name="channels">Mono or Stereo</param>
        /// <param name="samples">number of samples</param>
        /// <param name="callback">Method callback to get more data</param>
        /// <param name="data">data object</param>
        public AudioStream(int sampleFrequency, AudioFormat format, SoundChannel channels, short samples, AudioCallback callback, object data)
        {
            this.samples = samples;
            this.queue = new Queue<short[]>(5);
            this.sampleFrequency = sampleFrequency;

            // To keep compiler happy, we must 'initialize' these values
            spec.padding = 0;
            spec.size = 0;
            spec.silence = 0;

            spec.freq = sampleFrequency;
            spec.format = (short)format;
            spec.channels = (byte)channels;
            if (callback != null)
            {
                spec.callback = Marshal.GetFunctionPointerForDelegate(callback);
            }
            spec.samples = samples;
            spec.userdata = data;
            if (((ushort)spec.format & 0x8000) != 0x8000)    // signed
            {
                this.offset = 2 << ((byte)spec.format - 2);
                //this.offset = 0;
            }
            //else
            //{
            //    this.offset = 2 << ((byte)spec.format - 2);
            //}
        }
        AudioCallback callback;

        /// <summary>
        /// Creates an AudioStream
        /// </summary>
        /// <param name="sampleFrequency">Frequency</param>
        /// <param name="format">format of stream data</param>
        /// <param name="channels">Mono or Stereo</param>
        /// <param name="samples">number of samples</param>
        public AudioStream(int sampleFrequency, AudioFormat format, SoundChannel channels, short samples)
            : this(sampleFrequency, format, channels, samples, null, null)
        {
            if (format != AudioFormat.Unsigned16Little)
            {
                throw new AudioException(Events.StringManager.GetString("SupportedAudioFormats"));
            }
            callback = new AudioCallback(Unsigned16LittleStream);
            spec.callback = Marshal.GetFunctionPointerForDelegate(callback);
        }

        #endregion Constructors and Destructors

        #region Internal methods

        internal void Unsigned16LittleStream(IntPtr userData, IntPtr stream, int len)
        {
            len /= 2;

            if (queue.Count > 0)
            {
                short[] buf = queue.Dequeue();
                samplesInQueue -= buf.Length;
                Marshal.Copy(buf, 0, stream, len);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Asynchronously queues audio data in <paramref name="data"/>.
        /// </summary>
        /// <param name="data">Buffer formatted as <see cref="AudioFormat.Unsigned16Little"/> of audio data to be played</param>
        public void Write(short[] data)
        {
            Mixer.CheckOpenStatus(this);
            Mixer.Locked = true;
            short[] copy = (short[])data.Clone();
            samplesInQueue += copy.Length;
            queue.Enqueue(copy);
            Mixer.Locked = false;
        }

        /// <summary>
        /// Size of the SDL audio sample buffer
        /// </summary>
        public short Samples
        {
            get
            {
                return samples;
            }
            //internal set
            //{
            //    samples = value;
            //}
        }

        /// <summary>
        /// Total remaining samples queued
        /// </summary>
        public int RemainingSamples
        {
            get
            {
                return samplesInQueue;
            }
        }

        /// <summary>
        /// Total remaining milliseconds before sample queue is emptied
        /// </summary>
        public int RemainingMilliseconds
        {
            get
            {
                return (int)((double)RemainingSamples / sampleFrequency * 1000);
            }
        }

        /// <summary>
        /// Remaining number of buffers queued
        /// </summary>
        public int RemainingQueues
        {
            get
            {
                return queue.Count;
            }
        }

        /// <summary>
        /// Audio frequency in samples per second
        /// </summary>
        /// <remarks>
        /// The number of samples sent to the sound device every second.  
        /// Common values are 11025, 22050 and 44100. The higher the better.
        /// </remarks>
        public int Frequency
        {
            get
            {
                return this.spec.freq;
            }
        }

        /// <summary>
        /// Audio data format.
        /// </summary>
        /// <remarks>
        /// Specifies the size and type of each sample element.
        /// </remarks>
        public AudioFormat Format
        {
            get
            {
                return (AudioFormat)this.spec.format;
            }
        }

        /// <summary>
        /// Number of channels: 1 mono, 2 stereo.
        /// </summary>
        /// <remarks>
        /// The number of seperate sound channels. 
        /// 1 is mono (single channel), 2 is stereo (dual channel).
        /// </remarks>
        public byte Channels
        {
            get
            {
                return this.spec.channels;
            }
        }

        /// <summary>
        /// Audio buffer size in samples.
        /// </summary>
        /// <remarks>
        /// When used with <see cref="Mixer.OpenAudio"/> this refers 
        /// to the size of the 
        /// audio buffer in samples. A sample a chunk of audio data
        ///  of the size specified in format mulitplied by the number
        ///   of channels.
        /// </remarks>
        public short BufferSamples
        {
            get
            {
                return this.spec.samples;
            }
        }

        /// <summary>
        /// Audio buffer size in bytes (calculated)
        /// </summary>
        public int BufferSize
        {
            get
            {
                return this.spec.size;
            }
        }

        /// <summary>
        /// Audio buffer silence value (calculated).
        /// </summary>
        public int Silence
        {
            get
            {
                return this.spec.silence;
            }
        }

        #endregion Public methods
    }
}
