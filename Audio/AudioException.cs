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
using System.Runtime.Serialization;

using SdlDotNet.Core;

namespace SdlDotNet.Audio
{
    /// <summary>
    /// Represents a Audio run-time error from the Sdl library.
    /// </summary>
    [Serializable()]
    public class AudioException : SdlException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Returns basic exception
        /// </summary>
        public AudioException()
        {
            AudioException.Generate();
        }

        /// <summary>
        /// Initializes an AudioException instance
        /// </summary>
        /// <param name="message">
        /// The string representing the error message
        /// </param>
        public AudioException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Returns exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="exception">Exception type</param>
        public AudioException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Returns SerializationInfo
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected AudioException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion Constructors and Destructors
    }
}
