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

using SdlDotNet.Core;
using Tao.Sdl;

namespace SdlDotNet.Graphics
{
    #region MovieStatus

    /// <summary>
    /// Status of Movie
    /// </summary>
    /// <remarks></remarks>
    internal enum MovieStatus
    {
        /// <summary>
        /// Movie is playing
        /// </summary>
        Playing = Smpeg.SMPEG_PLAYING,
        /// <summary>
        /// Movie is stopped
        /// </summary>
        Stopped = Smpeg.SMPEG_STOPPED,
        /// <summary>
        /// Error in movie playback
        /// </summary>
        Error = Smpeg.SMPEG_ERROR
    }

    #endregion

    /// <summary>
    /// Represents a movie mpg file.
    /// </summary>
    /// <remarks>
    /// Before instantiating an instance of Movie,
    /// you must call Mixer.Close() to turn off the default mixer.
    /// If you do not do this, any movie will play very slowly. 
    /// Smpeg uses a custom mixer for audio playback. 
    /// </remarks>
    public class Movie : BaseSdlResource
    {
        #region Private fields

        private Smpeg.SMPEG_Info movieInfo;
        private bool disposed;
        static bool isInitialized = Video.Initialize();

        #endregion

        #region Constructors

        /// <summary>
        /// Create movie object from file
        /// </summary>
        /// <param name="file">filename of mpeg-1 movie.</param>
        public Movie(string file)
        {
            this.Handle =
                Smpeg.SMPEG_new(file, out this.movieInfo,
                (int)SdlFlag.TrueValue);
            if (this.Handle == IntPtr.Zero)
            {
                throw MovieStatusException.Generate();
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Destroys the surface object and frees its memory
        /// </summary>
        /// <param name="disposing">Manually dispose if true.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                    }
                    this.disposed = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
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
                    Smpeg.SMPEG_delete(this.Handle);
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

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public static bool IsInitialized
        {
            get { return Movie.isInitialized; }
        }

        /// <summary>
        /// Enable video during playback
        /// </summary>
        /// <remarks>Enabled by default</remarks>
        public void EnableVideo()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_enablevideo(this.Handle, (int)SdlFlag.TrueValue);
            }
        }

        /// <summary>
        /// Disable video during playback
        /// </summary>
        public void DisableVideo()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_enablevideo(this.Handle, (int)SdlFlag.FalseValue);
            }
        }

        /// <summary>
        /// Enable audio during playback
        /// </summary>
        /// <remarks>Enabled by default</remarks>
        public void EnableAudio()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_enableaudio(this.Handle, (int)SdlFlag.TrueValue);
            }
        }

        /// <summary>
        /// Disable audio during playback
        /// </summary>
        public void DisableAudio()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_enableaudio(this.Handle, (int)SdlFlag.FalseValue);
            }
        }

        /// <summary>
        /// Display video surface
        /// </summary>
        /// <param name="resource">Surface to display movie object on</param>
        public void Display(BaseSdlResource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_setdisplay(
                    this.Handle,
                    resource.Handle,
                    IntPtr.Zero, null);
            }
        }

        /// <summary>
        /// Sets the volume for the movie.
        /// </summary>
        /// <param name="volume">volume to set movie to.</param>
        public void AdjustVolume(int volume)
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_setvolume(this.Handle, volume);
            }
        }

        /// <summary>
        /// Returns Size of movie
        /// </summary>
        public System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(this.movieInfo.width, this.movieInfo.height);
            }
        }

        /// <summary>
        /// Width of movie
        /// </summary>
        public int Width
        {
            get
            {
                return movieInfo.width;
            }
        }

        /// <summary>
        /// Height of movie
        /// </summary>
        public int Height
        {
            get
            {
                return movieInfo.height;
            }
        }

        /// <summary>
        /// Returns the length of the movie in seconds
        /// </summary>
        public double Length
        {
            get
            {
                return movieInfo.total_time;
            }
        }

        /// <summary>
        /// Get the movie file size in bytes.
        /// </summary>
        public int FileSize
        {
            get
            {
                return movieInfo.total_size;
            }
        }

        /// <summary>
        /// Returns current frames per second
        /// </summary>
        public double CurrentFps
        {
            get
            {
                return movieInfo.current_fps;
            }
        }

        /// <summary>
        /// Returns current frame number in movie.
        /// </summary>
        public double CurrentFrame
        {
            get
            {
                return movieInfo.current_frame;
            }
        }

        /// <summary>
        /// Returns current offset??? in movie.
        /// </summary>
        public double CurrentOffset
        {
            get
            {
                return movieInfo.current_offset;
            }
        }

        /// <summary>
        /// Returns the current time in the movie in seconds.
        /// </summary>
        public double CurrentTime
        {
            get
            {
                return movieInfo.current_time;
            }
        }

        /// <summary>
        /// Returns current audio frame
        /// </summary>
        public int CurrentAudioFrame
        {
            get
            {
                return movieInfo.audio_current_frame;
            }
        }

        /// <summary>
        /// Returns audio string
        /// </summary>
        public string AudioString
        {
            get
            {
                return movieInfo.audio_string;
            }
        }

        /// <summary>
        /// Returns true if the movie has a valid audio stream.
        /// </summary>
        public bool HasAudio
        {
            get
            {
                if (movieInfo.has_audio != (int)SdlFlag.FalseValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// If true, movie is playing
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                if ((this.Handle != IntPtr.Zero) && (Smpeg.SMPEG_status(this.Handle) == (int)MovieStatus.Playing))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// If true, movie has been stopped
        /// </summary>
        public bool IsStopped
        {
            get
            {
                if ((this.Handle != IntPtr.Zero) && (Smpeg.SMPEG_status(this.Handle) == (int)MovieStatus.Stopped))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns true if the movie has a valid video stream.
        /// </summary>
        public bool HasVideo
        {
            get
            {
                if (movieInfo.has_video != (int)SdlFlag.FalseValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Starts playback of a movie.
        /// </summary>
        public void Play()
        {
            if (this.Handle != IntPtr.Zero)
            {
                this.Loop(false);
                Smpeg.SMPEG_play(this.Handle);
            }
        }

        /// <summary>
        /// Starts playback of a movie.
        /// </summary>
        /// <param name="repeat">Loop movie while playing</param>
        public void Play(bool repeat)
        {
            if (this.Handle != IntPtr.Zero)
            {
                this.Loop(repeat);
                Smpeg.SMPEG_play(this.Handle);
            }
        }

        /// <summary>
        /// Stops playback of a movie.
        /// </summary>
        public void Stop()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_stop(this.Handle);
                // It seems to take some time to actually stop.
                System.Threading.Thread.Sleep(5);
            }
        }

        /// <summary>
        /// This pauses playback of the movie
        /// </summary>
        public void Pause()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_pause(this.Handle);
            }
        }

        /// <summary>
        /// Sets the movie playback position to the start of the movie.
        /// </summary>
        public void Rewind()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_rewind(this.Handle);
            }
        }

        /// <summary>
        /// Loop movie
        /// </summary>
        private void Loop(bool repeat)
        {
            if (this.Handle != IntPtr.Zero)
            {
                if (repeat)
                {
                    Smpeg.SMPEG_loop(this.Handle, (int)SdlFlag.TrueValue);
                }
                else
                {
                    Smpeg.SMPEG_loop(this.Handle, (int)SdlFlag.FalseValue);
                }
            }
        }

        /// <summary>
        /// Resize movie
        /// </summary>
        /// <param name="width">Scale movie height to this width in pixels</param>
        /// <param name="height">Scale movie height to this height in pixels</param>
        public void ScaleXY(int width, int height)
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_scaleXY(this.Handle, width, height);
            }
        }

        /// <summary>
        /// Change size of movie by the same amount in both axes
        /// </summary>
        /// <param name="scalingFactor">scale both width and height by this factor</param>
        public void ScaleSize(int scalingFactor)
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_scale(this.Handle, scalingFactor);
            }
        }

        /// <summary>
        /// Double size of movie
        /// </summary>
        public void ScaleDouble()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_scale(this.Handle, 2);
            }
        }

        /// <summary>
        /// Return size to normal
        /// </summary>
        public void ScaleNormal()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_scale(this.Handle, 1);
            }
        }

        /// <summary>
        /// Move the video display area within the destination surface
        /// </summary>
        /// <param name="axisX">move the display area to this</param>
        /// <param name="axisY">move the display area to this</param>
        public void Move(int axisX, int axisY)
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_move(this.Handle, axisX, axisY);
            }
        }

        /// <summary>
        /// Move the video display area within the destination surface
        /// </summary>
        /// <param name="width">change width of display area</param>
        /// <param name="height">change height of display area</param>
        /// <param name="axisX">move the display area to this</param>
        /// <param name="axisY">move the display area to this</param>
        public void DisplayRegion(
            int width, int height,
            int axisX, int axisY)
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_setdisplayregion(this.Handle, axisX, axisY, width, height);
            }
        }

        /// <summary>
        /// Skip forward a certain number of seconds
        /// </summary>
        /// <param name="seconds">number of seconds to skip</param>
        public void Skip(float seconds)
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_skip(this.Handle, seconds);
            }
        }

        /// <summary>
        /// Seeks a specified number of bytes forward in the movie stream.
        /// </summary>
        /// <param name="bytes">number of bytes to move forward in movie</param>
        public void Seek(int bytes)
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_seek(this.Handle, bytes);
            }
        }

        /// <summary>
        /// Renders specified frame of movie.
        /// </summary>
        /// <param name="frameNumber">frame number</param>
        public void RenderFrame(int frameNumber)
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_renderFrame(this.Handle, frameNumber);
            }
        }

        /// <summary>
        /// Renders specified frame of movie.
        /// </summary>
        public void RenderFirstFrame()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_renderFrame(this.Handle, 0);
            }
        }

        /// <summary>
        /// Renders final frame of movie and puts it on a surface
        /// </summary>
        /// <param name="surface">surface to display frame to</param>
        public void RenderFinalFrame(BaseSdlResource surface)
        {
            if (surface == null)
            {
                throw new ArgumentNullException("surface");
            }
            if (this.Handle != IntPtr.Zero)
            {
                Smpeg.SMPEG_renderFinal(this.Handle, surface.Handle, 0, 0);
            }
        }

        #endregion
    }
}
