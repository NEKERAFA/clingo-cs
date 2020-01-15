using ClingoSharp.API;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ClingoSharp
{
    public class Control
    {
        internal IntPtr ControlObject { get; } = new IntPtr(0);

        public delegate void Logger(MessageCode code, string message);

        /// <summary>
        /// Control object to the grounding/solving process.
        /// </summary>
        /// <param name="arguments">Arguments to the grounder and solver</param>
        /// <param name="logger">Function to intercept messages normally printed to standard error</param>
        /// <param name="messageLimit">The maximum number of messages passed to the logger</param>
        public Control(List<string> arguments = null, Logger logger = null, int messageLimit = 20)
        {
            // Wrappers the clingo logger function
            void LoggerWrapper(int code, string message, IntPtr data)
            {
                if (logger != null)
                {
                    MessageCode codeEnum = MessageCode.Unknown;
                    if (Enum.IsDefined(typeof(MessageCode), code))
                    {
                        codeEnum = (MessageCode)code;
                    }

                    logger(codeEnum, message);
                }
            }

            // Creates pointers
            string[] argsPtr = (arguments != null) ? arguments.ToArray() : new string[0];
            UIntPtr argsSize = new UIntPtr((uint)argsPtr.Length);
            IntPtr[] controlPtr = { ControlObject };

            // Creates control object
            if (CControl.New(argsPtr, argsSize, LoggerWrapper, IntPtr.Zero, (uint)messageLimit, controlPtr) == 0)
            {
                throw new ClingoException();
            }
        }

        ~Control()
        {
            CControl.Free(ControlObject);
        }

        public void Load(string path)
        {
            if(CControl.Load(ControlObject, path) == 0)
            {
                throw new ClingoException();
            }
        }
    }
}
