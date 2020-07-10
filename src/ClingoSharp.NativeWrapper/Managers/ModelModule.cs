using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Managers
{
    public class ModelModule : IModel
    {
        #region Clingo C API Functions

        #region Functions for Inspecting Models

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_type(IntPtr model, [Out] ModelType[] type);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_number(IntPtr model, [Out] ulong[] number);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_symbols_size(IntPtr model, ShowType show, [Out] UIntPtr[] size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_symbols(IntPtr model, ShowType show, [Out] ulong[] symbols, UIntPtr size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_is_true(IntPtr model, int literal, [Out] bool[] result);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_cost_size(IntPtr model, [Out] UIntPtr[] size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_cost(IntPtr model, [Out] long[] costs, UIntPtr size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_optimality_proven(IntPtr model, [Out] bool[] proven);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_thread_id(IntPtr model, [Out] uint[] id);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_contains(IntPtr model, ulong atom, [Out] bool[] contained);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_extend(IntPtr model, ulong[] symbols, UIntPtr size);

        #endregion

        #region Functions for Adding Clauses

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_context(IntPtr model, [Out] IntPtr[] control);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_control_add_clause(IntPtr control, int[] clause, UIntPtr size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_control_symbolic_atoms(IntPtr control, [Out] IntPtr[] atoms);

        #endregion

        #endregion

        #region Module implementation

        #region Functions for Inspecting Models

        public bool Contains(IntPtr model, ulong atom, out bool contained)
        {
            bool[] containedPtr = new bool[1];
            var success = clingo_model_contains(model, atom, containedPtr);
            contained = containedPtr[0];
            return success != 0;
        }

        public bool Extends(IntPtr model, ulong[] atoms)
        {
            UIntPtr symbolsSize = new UIntPtr(Convert.ToUInt32(atoms == null ? 0 : atoms.Length));
            var success = clingo_model_extend(model, atoms, symbolsSize);
            return success != 0;
        }

        public bool GetCosts(IntPtr model, out long[] costs)
        {
            UIntPtr[] costSizePtr = new UIntPtr[1];
            var success = clingo_model_cost_size(model, costSizePtr);

            if (success != 0)
            {
                costs = new long[costSizePtr[0].ToUInt64()];
                success = clingo_model_cost(model, costs, costSizePtr[0]);
                return success != 0;
            }

            costs = null;
            return false;
        }

        public bool GetNumber(IntPtr model, out ulong number)
        {
            ulong[] numberPtr = new ulong[1];
            var success = clingo_model_number(model, numberPtr);
            number = numberPtr[0];
            return success != 0;
        }

        public bool GetSymbols(IntPtr model, ShowType showType, out ulong[] symbols)
        {
            UIntPtr[] symbolsSizePtr = new UIntPtr[1];
            var success = clingo_model_symbols_size(model, showType, symbolsSizePtr);

            if (success != 0)
            {
                ulong[] symbolArray = new ulong[symbolsSizePtr[0].ToUInt64()];
                success = clingo_model_symbols(model, showType, symbolArray, symbolsSizePtr[0]);
                symbols = symbolArray;
                return success != 0;
            }

            symbols = null;
            return false;
        }

        public bool GetThreadId(IntPtr model, out uint id)
        {
            uint[] idPtr = new uint[1];
            var success = clingo_model_thread_id(model, idPtr);
            id = idPtr[0];
            return success != 0;
        }

        public bool GetType(IntPtr model, out ModelType type)
        {
            ModelType[] typePtr = new ModelType[1];
            var success = clingo_model_type(model, typePtr);
            type = typePtr[0];
            return success != 0;
        }

        public bool IsOptimalityProven(IntPtr model, out bool proven)
        {
            bool[] provenPtr = new bool[1];
            var success = clingo_model_optimality_proven(model, provenPtr);
            proven = provenPtr[0];
            return success != 0;
        }

        public bool IsTrue(IntPtr model, int literal, out bool result)
        {
            bool[] resultPtr = new bool[1];
            var success = clingo_model_is_true(model, literal, resultPtr);
            result = resultPtr[0];
            return success != 0;
        }

        #endregion

        #region Functions for Adding Clauses

        public bool GetContext(IntPtr model, out IntPtr context)
        {
            IntPtr[] contextPtr = new IntPtr[1];
            var success = clingo_model_context(model, contextPtr);
            context = contextPtr[0];
            return success != 0;
        }

        public bool GetSymbolicAtoms(IntPtr control, out IntPtr atoms)
        {
            IntPtr[] symbolicAtomsPtr = new IntPtr[1];
            var success = clingo_solve_control_symbolic_atoms(control, symbolicAtomsPtr);
            atoms = symbolicAtomsPtr[0];
            return success != 0;
        }

        public bool AddClause(IntPtr control, int[] clause)
        {
            UIntPtr size = new UIntPtr(Convert.ToUInt32(clause != null ? clause.Length : 0));
            var success = clingo_solve_control_add_clause(control, clause, size);
            return success != 0;
        }

        #endregion

        #endregion
    }
}
