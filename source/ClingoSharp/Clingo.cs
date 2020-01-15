using ClingoSharp.API;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp
{
    public class Clingo
    {
        private static string _version = null;

        public static string Version
        {
            get
            {
                if (_version == null)
                {
                    int major, minor, revision = 0;

                    unsafe
                    {
                        IntPtr majorPtr = new IntPtr((void*) &major);
                        IntPtr minorPtr = new IntPtr((void*) &minor);
                        IntPtr revisionPtr = new IntPtr((void*) &revision);

                        CClingo.GetVersion(majorPtr, minorPtr, revisionPtr);
                    }

                    _version = string.Format("{0}.{1}.{2}", major, minor, revision);
                }

                return _version;
            }
        }
    }
}
