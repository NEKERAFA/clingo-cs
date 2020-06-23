using ClingoSharp.NativeWrapper.Callbacks;
using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Interfaces.Modules;
using ClingoSharp.NativeWrapper.Types;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Managers
{
    /// <summary>
    /// Functions to control the grounding and solving process.
    /// </summary>
    public class ControlModule : IControl
    {
        #region Clingo C API Functions

        #region Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_new(string[] arguments, UIntPtr arguments_size, Callbacks.LoggerCallback logger, IntPtr logger_data, uint message_limit, [Out] IntPtr[] control);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_control_free(IntPtr control);

        #endregion

        #region Grounding Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_add(IntPtr control, string name, string[] parameters, UIntPtr parameters_size, string program);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_ground(IntPtr control, Part[] parts, UIntPtr parts_size, GroundCallback ground_callback, IntPtr ground_callback_data);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_load(IntPtr control, string filename);

        #endregion

        #region Solving Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_solve(IntPtr control, Enums.SolveMode mode, int[] assumptions, UIntPtr assumptions_size, SolveEventCallback notify, IntPtr data, [Out] IntPtr[] handle);

        #endregion

        #region Program Inspection Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_get_const(IntPtr control, string name, [Out] ulong[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_has_const(IntPtr control, string name, [Out] bool[] exists);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_symbolic_atoms(IntPtr control, [Out] IntPtr[] atoms);

        #endregion

        #endregion

        #region Module implementation

        #region Functions

        public bool New(string[] arguments, LoggerCallback logger, uint messageLimit, out IntPtr control)
        {
            UIntPtr argumentsSize = new UIntPtr(Convert.ToUInt32(arguments == null ? 0 : arguments.Length));
            IntPtr[] controlPtr = new IntPtr[1];
            var success = clingo_control_new(arguments, argumentsSize, logger, IntPtr.Zero, messageLimit, controlPtr);
            control = controlPtr[0];
            return success != 0;
        }

        public void Free(IntPtr control)
        {
            clingo_control_free(control);
        }

        #endregion

        #region Grounding Functions

        public bool Load(IntPtr control, string filename)
        {
            var success = clingo_control_load(control, filename);
            return success != 0;
        }

        public bool Add(IntPtr control, string name, string[] parameters, string program)
        {
            UIntPtr parametersSize = new UIntPtr(Convert.ToUInt32(parameters == null ? 0 : parameters.Length));
            var success = clingo_control_add(control, name, parameters, parametersSize, program);
            return success != 0;
        }

        public bool Ground(IntPtr control, Part[] parts, GroundCallback callback)
        {
            UIntPtr partsSize = new UIntPtr(Convert.ToUInt32(parts == null ? 0 : parts.Length));
            var success = clingo_control_ground(control, parts, partsSize, callback, IntPtr.Zero);
            return success != 0;
        }

        #endregion

        #region Solving Functions

        public bool Solve(IntPtr control, SolveMode mode, int[] assumptions, SolveEventCallback callback, out IntPtr handler)
        {
            UIntPtr assumptionsSize = new UIntPtr(Convert.ToUInt32(assumptions == null ? 0 : assumptions.Length));
            IntPtr[] handlerPtr = new IntPtr[1];
            var success = clingo_control_solve(control, mode, assumptions, assumptionsSize, callback, IntPtr.Zero, handlerPtr);
            handler = handlerPtr[0];
            return success != 0;
        }

        #endregion

        #region Program Inspection Functions

        public bool GetConst(IntPtr control, string name, out ulong symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            var success = clingo_control_get_const(control, name, symbolPtr);
            symbol = symbolPtr[0];
            return success != 0;
        }

        public bool HasConst(IntPtr control, string name, out bool exists)
        {
            bool[] existsPtr = new bool[1];
            var success = clingo_control_has_const(control, name, existsPtr);
            exists = existsPtr[0];
            return success != 0;
        }

        public bool GetSymbolicAtoms(IntPtr control, out IntPtr symbolicAtoms)
        {
            IntPtr[] symbolicAtomsPtr = new IntPtr[1];
            var success = clingo_control_symbolic_atoms(control, symbolicAtomsPtr);
            symbolicAtoms = symbolicAtomsPtr[0];
            return success != 0;
        }

        #endregion

        #endregion
    }
}
