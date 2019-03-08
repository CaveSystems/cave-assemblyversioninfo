using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Cave.Reflection;

namespace Cave
{
    /// <summary>
    /// Obtains extende version info for the specified file (replacement for FileVersionInfo).
    /// </summary>
    public struct AssemblyVersionInfo
    {
        #region static implementation

        /// <summary>
        /// Checks two <see cref="AssemblyVersionInfo"/> for equality.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator ==(AssemblyVersionInfo v1, AssemblyVersionInfo v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// Checks two <see cref="AssemblyVersionInfo"/> for inequality.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator !=(AssemblyVersionInfo v1, AssemblyVersionInfo v2)
        {
            return !v1.Equals(v2);
        }

        static object programAssemblyVersionInfo;

        /// <summary>
        /// Gets the <see cref="AssemblyVersionInfo"/> for the current entry assembly.
        /// </summary>
        public static AssemblyVersionInfo Program
        {
            get
            {
                if (programAssemblyVersionInfo == null)
                {
                    Assembly a = MainAssembly.Get();
                    if (a == null)
                    {
                        throw new InvalidOperationException("AppDomain inaccessible!");
                    }

                    programAssemblyVersionInfo = FromAssembly(a);
                }
                return (AssemblyVersionInfo)programAssemblyVersionInfo;
            }
        }

        /// <summary>
        /// Obtains the <see cref="AssemblyVersionInfo"/> for the specified FileName.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static AssemblyVersionInfo FromAssemblyFile(string fileName)
        {
            return FromAssembly(Assembly.LoadFile(fileName));
        }

        /// <summary>
        /// Obtains the <see cref="AssemblyVersionInfo"/> for the specified AssemblyName.
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static AssemblyVersionInfo FromAssemblyName(AssemblyName assemblyName)
        {
            return FromAssembly(Assembly.Load(assemblyName));
        }

        /// <summary>
        /// Obtains the <see cref="AssemblyVersionInfo"/> for the specified Assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static AssemblyVersionInfo FromAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            AssemblyVersionInfo i = default(AssemblyVersionInfo);
            AssemblyName assemblyName = assembly.GetName();
            #region get assembly attributes
            foreach (Attribute attribute in assembly.GetCustomAttributes(false))
            {
                {
                    AssemblyCompanyAttribute a = attribute as AssemblyCompanyAttribute;
                    if (a != null)
                    {
                        i.Company = a.Company;
                        continue;
                    }
                }
                {
                    AssemblyConfigurationAttribute a = attribute as AssemblyConfigurationAttribute;
                    if (a != null)
                    {
                        i.Configuration = a.Configuration;
                        continue;
                    }
                }
                {
                    AssemblyCopyrightAttribute a = attribute as AssemblyCopyrightAttribute;
                    if (a != null)
                    {
                        i.Copyright = a.Copyright;
                        continue;
                    }
                }
                {
                    AssemblyDescriptionAttribute a = attribute as AssemblyDescriptionAttribute;
                    if (a != null)
                    {
                        i.Description = a.Description;
                        continue;
                    }
                }
                {
                    AssemblyFileVersionAttribute a = attribute as AssemblyFileVersionAttribute;
                    if (a != null)
                    {
                        i.FileVersion = new Version(a.Version);
                        continue;
                    }
                }
                {
                    AssemblyInformationalVersionAttribute a = attribute as AssemblyInformationalVersionAttribute;
                    if (a != null)
                    {
                        i.InformalVersion = SemanticVersion.TryParse(a.InformationalVersion);
                        continue;
                    }
                }
                {
                    AssemblyProductAttribute a = attribute as AssemblyProductAttribute;
                    if (a != null)
                    {
                        i.Product = a.Product;
                        continue;
                    }
                }
                {
                    AssemblyTitleAttribute a = attribute as AssemblyTitleAttribute;
                    if (a != null)
                    {
                        i.Title = a.Title;
                        continue;
                    }
                }
                {
                    AssemblyTrademarkAttribute a = attribute as AssemblyTrademarkAttribute;
                    if (a != null)
                    {
                        i.Trademark = a.Trademark;
                        continue;
                    }
                }
                {
                    GuidAttribute a = attribute as GuidAttribute;
                    if (a != null)
                    {
                        i.Guid = new Guid(a.Value);
                        continue;
                    }
                }
                {
                    AssemblyUpdateURI a = attribute as AssemblyUpdateURI;
                    if (a != null)
                    {
                        i.UpdateURI = a.URI;
                        continue;
                    }
                }
                {
                    AssemblySoftwareFlags a = attribute as AssemblySoftwareFlags;
                    if (a != null)
                    {
                        i.SoftwareFlags = a.Flags;
                        continue;
                    }
                }
                {
                    AssemblySetupVersion a = attribute as AssemblySetupVersion;
                    if (a != null)
                    {
                        i.SetupVersion = a.SetupVersion;
                        continue;
                    }
                }
                {
                    AssemblySetupPackage a = attribute as AssemblySetupPackage;
                    if (a != null)
                    {
                        i.SetupPackage = a.SetupPackage;
                        continue;
                    }
                }
            }
            #endregion

            // get assembly name properties
            {
                i.AssemblyVersion = assemblyName.Version;
                i.Culture = assemblyName.CultureInfo;
                i.PublicKey = assemblyName.GetPublicKey();
                i.PublicKeyToken = assemblyName.GetPublicKeyToken().ToHexString();
            }
            return i;
        }
        #endregion

        #region fields

        /// <summary>
        /// Dataset ID. This is only used when reading/writing at a database.
        /// </summary>
        public long ID;

        /// <summary>
        /// The File Version.
        /// </summary>
        public Version FileVersion;

        /// <summary>
        /// The Assembly / Product Version.
        /// </summary>
        public Version AssemblyVersion;

        /// <summary>
        /// The Assembly display version.
        /// </summary>
        public SemanticVersion InformalVersion;

        /// <summary>
        /// The Setup Version.
        /// </summary>
        public Version SetupVersion;

        /// <summary>
        /// The Setup Package Name.
        /// </summary>
        public string SetupPackage;

        /// <summary>
        /// The SoftwareFlags.
        /// </summary>
        public SoftwareFlags SoftwareFlags;

        /// <summary>
        /// The title.
        /// </summary>
        public string Title;

        /// <summary>
        /// The product name.
        /// </summary>
        public string Product;

        /// <summary>
        /// The product description.
        /// </summary>
        public string Description;

        /// <summary>
        /// Name of the company.
        /// </summary>
        public string Company;

        /// <summary>
        /// Compiler configuration of the program.
        /// </summary>
        public string Configuration;

        /// <summary>
        /// The Assemblies' copyright.
        /// </summary>
        public string Copyright;

        /// <summary>
        /// The Assemblies' trademark.
        /// </summary>
        public string Trademark;

        /// <summary>
        /// The Assemblies' Culture LCID.
        /// </summary>
        public int CultureID;

        /// <summary>
        /// The Assemblies' full PublicKey.
        /// </summary>
        public byte[] PublicKey;

        /// <summary>
        /// The Assemblies' PublicKeyToken.
        /// </summary>
        public string PublicKeyToken;

        /// <summary>
        /// The Assemblies' Guid.
        /// </summary>
        public Guid Guid;

        /// <summary>
        /// The Assemblies' Update URI.
        /// </summary>
        public Uri UpdateURI;

        #endregion

        /// <summary>
        /// Gets or sets the Assemblies' CultureInfo.
        /// </summary>
        public CultureInfo Culture
        {
            get => new CultureInfo(CultureID);
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                CultureID = value.LCID;
            }
        }

        /// <summary>Gets the release date.</summary>
        /// <value>The release date.</value>
        public DateTime ReleaseDate
        {
            get
            {
                try
                {
                    return new DateTime(FileVersion.Major, FileVersion.Minor / 100, FileVersion.Minor % 100, FileVersion.Build / 100, FileVersion.Build % 100, 0, DateTimeKind.Utc);
                }
                catch
                {
                    return new DateTime(0);
                }
            }
        }

        /// <summary>
        /// Obtains a latestversion instance for the current assembly (this populates only fields present at this instance).
        /// </summary>
        /// <returns></returns>
        public LatestVersion ToLatestVersion()
        {
            return new LatestVersion()
            {
                AssemblyVersion = AssemblyVersion,
                FileVersion = FileVersion,
                ReleaseDate = ReleaseDate,
                SoftwareName = Title,
                UpdateURI = UpdateURI,
                SetupPackage = SetupPackage,
                SetupVersion = SetupVersion,
                SoftwareFlags = SoftwareFlags,
            };
        }

        /// <summary>
        /// Obtains the string describing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Product + " " + InformalVersion;
        }

        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <param name="format">The format.</param>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        /// <exception cref="NotSupportedException">Invalid format.</exception>
        public string ToString(string format)
        {
            StringBuilder result = new StringBuilder();
            switch (format)
            {
                case "X":
                    foreach (FieldInfo field in typeof(AssemblyVersionInfo).GetFields())
                    {
                        result.Append(field.Name);
                        result.Append(": ");
                        result.Append(field.GetValue(this));
                        result.AppendLine();
                    }
                    break;
                default: throw new NotSupportedException();
            }
            return result.ToString();
        }

        /// <summary>
        /// Obtains a hashcode for this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        /// <summary>
        /// Checks this instance for equality with another one.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AssemblyVersionInfo))
            {
                return false;
            }

            AssemblyVersionInfo other = (AssemblyVersionInfo)obj;
            FieldInfo[] fields = typeof(AssemblyVersionInfo).GetFields(BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                if (field.GetValue(this) != field.GetValue(other))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
