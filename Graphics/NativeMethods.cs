#region LICENSE
/*
 * $RCSfile$
 * Copyright (C) 2006 - 2007 David Hudson (jendave@yahoo.com)
 * Copyright (C) 2007 Jonathan Porter (jono.porter@gmail.com)
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
using System.Runtime.InteropServices;
using System.Security;

namespace SdlDotNet.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        private const CallingConvention CALLING_CONVENTION = CallingConvention.Winapi;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="internalFormat"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="format"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport("glu32.dll", CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
        internal static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] IntPtr data);
    }
}
