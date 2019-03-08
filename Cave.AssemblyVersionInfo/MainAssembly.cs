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
     Gernot Lenkner <g.lenkner@cavemail.org>

   Contributors:

 */
#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Cave
{
    /// <summary>
    /// Retrieves the main assembly of the running program.
    /// </summary>
    public static class MainAssembly
    {
        static Assembly mainAssembly = null;

        /// <summary>Gets the MainAssembly.</summary>
        /// <returns>Returns the main assembly instance.</returns>
        public static Assembly Get()
        {
            if (mainAssembly == null)
            {
                mainAssembly = FindProgramAssembly();
            }
            return mainAssembly;
        }

        static Assembly FindProgramAssembly()
        {
            if (Platform.Type == PlatformType.Android)
            {
                Debug.WriteLine("androidEntryPoint");
                MethodInfo bestOnCreate = null;
                MethodInfo first = null;
                foreach (StackFrame frame in new StackTrace().GetFrames())
                {
                    MethodInfo method = frame.GetMethod() as MethodInfo;
                    if (method == null)
                    {
                        continue;
                    }

                    string asmName = method.Module?.Assembly?.GetName().Name;
                    if (asmName != null && (asmName != "mscorlib") && (asmName != "Mono.Android"))
                    {
                        first = method;
                        if (method.Name == "OnCreate")
                        {
                            bestOnCreate = method;
                        }
                    }
                }
                if (bestOnCreate != null)
                {
                    return bestOnCreate.Module.Assembly;
                }

                return first.Module.Assembly;
            }

            Debug.WriteLine("GetEntryAssembly");
            Assembly result = Assembly.GetEntryAssembly();
            if (result != null)
            {
                return result;
            }

            Debug.WriteLine("bestStatic");
            MethodInfo bestStatic = null;
            foreach (StackFrame frame in new StackTrace().GetFrames())
            {
                MethodInfo method = frame.GetMethod() as MethodInfo;
                if (method == null)
                {
                    continue;
                }

                if (method.IsStatic && Path.GetExtension(method.Module.Name) == ".exe")
                {
                    bestStatic = method;
                }
            }
            if (bestStatic != null)
            {
                return bestStatic.Module.Assembly;
            }

            Debug.WriteLine("CurrentDomain");
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (a.EntryPoint != null && !a.ReflectionOnly)
                {
                    return a;
                }
            }
            return null;
        }
    }
}
