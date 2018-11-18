#region CopyRight 2018
/*
    Copyright (c) 2003-2018 Andreas Rohleder (andreas@rohleder.cc)
    All rights reserved
*/
#endregion
#region License LGPL-3
/*
    This program/library/sourcecode is free software; you can redistribute it
    and/or modify it under the terms of the GNU Lesser General Public License
    version 3 as published by the Free Software Foundation subsequent called
    the License.

    You may not use this program/library/sourcecode except in compliance
    with the License. The License is included in the LICENSE file
    found at the installation directory or the distribution package.

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    "Software"), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be included
    in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
#region Authors & Contributors
/*
   Author:
     Andreas Rohleder <andreas@rohleder.cc>

   Contributors:

 */
#endregion

using System;

namespace Cave
{
    /// <summary>
    /// Provides flags for software revisions / versions
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags")]
    [Flags]
    public enum SoftwareFlags
    {
        /// <summary>
        /// No flags
        /// </summary>
        None = 0,

        /// <summary>
        /// Software suite (<see cref="Stable"/>, <see cref="Testing"/>)
        /// </summary>
        Suite = 0x0F,

        /// <summary>
        /// Software is stable
        /// </summary>
        Stable = 0x01,

        /// <summary>
        /// Software is testing
        /// </summary>
        Testing = 0x02,

        /// <summary>
        /// Software unstable trunk or branch
        /// </summary>
        Unstable = 0x03,

        /// <summary>
        /// Software configuration (<see cref="Release"/>, <see cref="Debug"/>)
        /// </summary>
        Configuration = 0xF0,

        /// <summary>
        /// Software with release configuration
        /// </summary>
        Release = 0x10,

        /// <summary>
        /// Software with debug configuration
        /// </summary>
        Debug = 0x20,

        /// <summary>
        /// Stable release version (end user version)
        /// </summary>
        Stable_Release = Stable | Release,

        /// <summary>
        /// Stable debug version (end user version for debugging and bug fixing)
        /// </summary>
        Stable_Debug = Stable | Debug,

        /// <summary>
        /// Testing release version (betatester version with newly implemented functionality)
        /// </summary>
        Testing_Release = Testing | Release,

        /// <summary>
        /// Testing debug version (betatester version with newly implemented functionality for debugging and bug fixing)
        /// </summary>
        Testing_Debug = Testing | Debug,
    }
}
