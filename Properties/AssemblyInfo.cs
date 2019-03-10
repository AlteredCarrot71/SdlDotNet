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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Resources;
using System.IO;
using System.Diagnostics.CodeAnalysis;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle("SdlDotNet")]
[assembly: AssemblyCopyright("Copyright ©2003-2007 David Hudson.  All rights reserved.")]
[assembly: AssemblyDescription(".NET Bindings for SDL")]
[assembly: AssemblyDefaultAlias("SdlDotNet")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("The SDL.NET Project")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyTrademark("http://cs-sdl.sourceforge.net")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]
[assembly: AssemblyVersion("6.1.0.0")]
[assembly: AssemblyFileVersion("6.1.0.0")]
[assembly: AssemblyInformationalVersion("6.1.0.0")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision 
// and Build Numbers 
// by using the '*' as shown below:

//
// In order to sign your assembly you must specify a key to use. 
// Refer to the 
// Microsoft .NET Framework documentation for more information on 
// assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
//       When specifying the KeyFile, the location of the KeyFile should be
//       relative to the project output directory which is
//       %Project Directory%\obj\<configuration>. For example, if your KeyFile is
//       located in the project directory, you would specify the AssemblyKeyFile 
//       attribute as [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Flags = SecurityPermissionFlag.Execution)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Flags = SecurityPermissionFlag.SkipVerification)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Flags = SecurityPermissionFlag.UnmanagedCode)]
[assembly: AssemblyDelaySign(false)]
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "SdlDotNet.Windows")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "SdlDotNet.Windows.SurfaceControl+BlitEvent", MessageId = "Blit")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "SdlDotNet.Graphics.Sprites.SpriteCollection.Erase(SdlDotNet.Graphics.Surface,SdlDotNet.Graphics.Surface):System.Void")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "SdlDotNet.Graphics.Surface.Erase(System.Drawing.Rectangle,SdlDotNet.Graphics.Surface):System.Void")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "member", Target = "SdlDotNet.Graphics.Sprites.Sprite.LastBlitRectangle", MessageId = "Blit")]
[module: SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Scope = "member", Target = "SdlDotNet.Graphics.Surface.GetColors(System.Drawing.Rectangle):System.Drawing.Color[,]", MessageId = "Body")]
[module: SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Scope = "member", Target = "SdlDotNet.Graphics.Surface.GetColors(System.Drawing.Rectangle):System.Drawing.Color[,]", MessageId = "Return")]
[module: SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Scope = "member", Target = "SdlDotNet.Graphics.Surface.SetPixels(System.Drawing.Point,System.Drawing.Color[,]):System.Void", MessageId = "1#")]
[module: SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Scope = "member", Target = "SdlDotNet.Graphics.MarshalHelper..cctor()")]
[module: SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate", Scope = "member", Target = "SdlDotNet.Graphics.MarshalHelper.offset")]
[module: SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Scope = "member", Target = "SdlDotNet.Graphics.OverlayFormat.UYVY", MessageId = "Member")]
[module: SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Scope = "member", Target = "SdlDotNet.Graphics.OverlayFormat.YVYU", MessageId = "Member")]
[module: SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Scope = "member", Target = "SdlDotNet.Graphics.OverlayFormat.IYUV", MessageId = "Member")]
[module: SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Scope = "member", Target = "SdlDotNet.Graphics.NativeMethods.gluBuild2DMipmaps(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.IntPtr):System.Int32")]
