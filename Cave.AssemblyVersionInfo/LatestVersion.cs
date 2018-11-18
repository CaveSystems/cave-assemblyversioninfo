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
    /// Provides access to LATESTVERSION files
    /// </summary>
    public struct LatestVersion : IComparable<LatestVersion>
    {
        #region static functions
        /// <summary>
        /// Obtains a new empty version
        /// </summary>
        public static LatestVersion Empty
        {
            get
            {
                return new LatestVersion
                {
                    AssemblyVersion = new Version(0, 0),
                    FileVersion = new Version(0, 0),
                    SetupVersion = new Version(0, 0),
                    ReleaseDate = default(DateTime),
                    SetupPackage = "",
                    SetupArguments = "",
                    SoftwareName = "",
                    SoftwareFlags = SoftwareFlags.None
                };
            }
        }

        /// <summary>
        /// Checks whether the specified latest version is newer then the current one
        /// </summary>
        /// <param name="latest"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public static bool VersionIsNewer(Version latest, Version current)
        {
            if (latest == null)
            {
                throw new ArgumentNullException("latest");
            }

            return latest.CompareTo(current) > 0;
        }

        #region static comparison operators
        /// <summary>
        /// Provides <see cref="LatestVersion"/> comparison
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator <(LatestVersion v1, LatestVersion v2)
        {
            if (v1.SoftwareName != v2.SoftwareName)
            {
                throw new ArgumentException(string.Format("Softwarename does not match!"));
            }

            return VersionIsNewer(v2.SetupVersion, v1.SetupVersion);
        }

        /// <summary>
        /// Provides <see cref="LatestVersion"/> comparison
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator <=(LatestVersion v1, LatestVersion v2)
        {
            return (v1 < v2) || (v1 == v2);
        }

        /// <summary>
        /// Provides <see cref="LatestVersion"/> comparison
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator >(LatestVersion v1, LatestVersion v2)
        {
            if (v1.SoftwareName != v2.SoftwareName)
            {
                throw new ArgumentException(string.Format("Softwarename does not match!"));
            }

            return VersionIsNewer(v1.SetupVersion, v2.SetupVersion);
        }

        /// <summary>
        /// Provides <see cref="LatestVersion"/> comparison
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator >=(LatestVersion v1, LatestVersion v2)
        {
            return (v1 > v2) || (v1 == v2);
        }

        /// <summary>
        /// Provides <see cref="LatestVersion"/> comparison
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator ==(LatestVersion v1, LatestVersion v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// Provides <see cref="LatestVersion"/> comparison
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator !=(LatestVersion v1, LatestVersion v2)
        {
            return !v1.Equals(v2);
        }
        #endregion

        #endregion

        #region public fields
        /// <summary>
        /// Name of the Software this LatestVersion belongs to
        /// </summary>
        public string SoftwareName { get; set; }

        /// <summary>
        /// Gets / sets the Assembly <see cref="Version"/>.
        /// </summary>
        public Version AssemblyVersion { get; set; }

        /// <summary>
        /// Gets / sets the File <see cref="Version"/>.
        /// </summary>
        public Version FileVersion { get; set; }

        /// <summary>
        /// Gets / sets the Installer <see cref="Version"/>. (This is the version used while comparing two instances).
        /// </summary>
        public Version SetupVersion { get; set; }

        /// <summary>
        /// Path of update
        /// </summary>
        public Uri UpdateURI { get; set; }

        /// <summary>
        /// Gets / sets the SoftwareFlags
        /// </summary>
        public SoftwareFlags SoftwareFlags { get; set; }

        /// <summary>
        /// Gets / sets the release date
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets / sets the SetupPackage fileName.
        /// </summary>
        public string SetupPackage { get; set; }

        /// <summary>
        /// Gets / sets the setup arguments.
        /// </summary>
        public string SetupArguments { get; set; }

        #endregion

        /// <summary>
        /// Returns "{<see cref="SoftwareName"/>} {<see cref="AssemblyVersion"/>}"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{SoftwareName} {AssemblyVersion}";
        }

        /// <summary>
        /// Obtains the hashcode of this instance
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Checks two <see cref="LatestVersion"/> instances for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is LatestVersion))
            {
                return false;
            }

            LatestVersion other = (LatestVersion)obj;
            return
                (SoftwareName == other.SoftwareName) &&
                (AssemblyVersion == other.AssemblyVersion) &&
                (FileVersion == other.FileVersion) &&
                (SetupVersion == other.SetupVersion) &&
                (SetupPackage == other.SetupPackage) &&
                (SoftwareFlags == other.SoftwareFlags);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, 
        /// follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(LatestVersion other)
        {
            int result = AssemblyVersion.CompareTo(other.AssemblyVersion);
            if (result == 0)
            {
                result = FileVersion.CompareTo(other.FileVersion);
            }

            return result;
        }
    }
}
