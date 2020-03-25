using AutoMapper;
using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.CoreServices.Types;
using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Extensions;
using System;
using System.Runtime.InteropServices;
using clingo_symbol = System.UInt64;
using clingo_literal = System.Int32;
using clingo_id = System.UInt32;

namespace ClingoSharp.NativeWrapper
{
    public class ModelModuleImpl : IModelModule
    {
        private readonly IMapper m_mapper;

        #region Constructor

        public ModelModuleImpl()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateClingoMaps();
            });

            m_mapper = config.CreateMapper();
        }

        #endregion

        #region Clingo C API Functions

        #region Functions for Inspecting Models

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_type(IntPtr model, [Out] clingo_model_type[] type);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_number(IntPtr model, [Out] ulong[] number);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_symbols_size(IntPtr model, clingo_show_type show, [Out] UIntPtr[] size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_symbols(IntPtr model, clingo_show_type show, [Out] clingo_symbol[] symbols, UIntPtr size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_is_true(IntPtr model, clingo_literal literal, [Out] bool[] result);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_cost_size(IntPtr model, [Out] UIntPtr[] size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_cost(IntPtr model, [Out] long[] costs, UIntPtr size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_optimality_proven(IntPtr model, [Out] bool[] proven);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_thread_id(IntPtr model, [Out] clingo_id[] id);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_contains(IntPtr model, clingo_symbol atom, [Out] bool[] contained);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_extend(IntPtr model, clingo_symbol[] symbols, UIntPtr size);

        #endregion

        #region Functions for Adding Clauses

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_context(IntPtr model, [Out] IntPtr[] control);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_control_add_clause(IntPtr control, clingo_literal[] clause, UIntPtr size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_control_symbolic_atoms(IntPtr control, [Out] IntPtr[] atoms);

        #endregion

        #endregion

        #region Module implementation

        #region Functions for Inspecting Models

        public bool Contains(Model model, Symbol atom, out bool contained)
        {
            bool[] containedPtr = new bool[1];

            var success = clingo_model_contains(model.Object, atom.Value, containedPtr);
            contained = containedPtr[0];

            return success != 0;
        }

        public bool Extends(Model model, Symbol[] atoms)
        {
            clingo_symbol[] clingoSymbols = m_mapper.Map<Symbol[], clingo_symbol[]>(atoms);
            UIntPtr clingoSymbolsSize = new UIntPtr(Convert.ToUInt32(atoms == null ? 0 : atoms.Length));

            var success = clingo_model_extend(model.Object, clingoSymbols, clingoSymbolsSize);

            return success != 0;
        }

        public bool GetCosts(Model model, out long[] costs)
        {
            UIntPtr[] costSizePtr = new UIntPtr[1];
            var success = clingo_model_cost_size(model.Object, costSizePtr);

            if (success != 0)
            {
                costs = new long[costSizePtr[0].ToUInt64()];

                success = clingo_model_cost(model.Object, costs, costSizePtr[0]);

                return success != 0;               
            }

            costs = null;
            return false;
        }

        public bool GetNumber(Model model, out ulong number)
        {
            ulong[] numberPtr = new ulong[1];

            var success = clingo_model_number(model.Object, numberPtr);
            number = numberPtr[0];

            return success != 0;
        }

        public bool GetSymbols(Model model, ShowType showType, out Symbol[] symbols)
        {
            UIntPtr[] symbolsSizePtr = new UIntPtr[1];
            var success = clingo_model_symbols_size(model.Object, (clingo_show_type)showType, symbolsSizePtr);

            if (success != 0)
            {
                clingo_symbol[] clingoSymbols = new clingo_symbol[symbolsSizePtr[0].ToUInt64()];

                success = clingo_model_symbols(model.Object, (clingo_show_type)showType, clingoSymbols, symbolsSizePtr[0]);
                symbols = m_mapper.Map<clingo_symbol[], Symbol[]>(clingoSymbols);

                return success != 0;
            }

            symbols = null;
            return false;
        }

        public bool GetThreadId(Model model, out uint id)
        {
            clingo_id[] idPtr = new clingo_id[1];
            
            var success = clingo_model_thread_id(model.Object, idPtr);
            id = idPtr[0];

            return success != 0;
        }

        public bool GetType(Model model, out ModelType type)
        {
            clingo_model_type[] typePtr = new clingo_model_type[1];

            var success = clingo_model_type(model.Object, typePtr);

            type = (ModelType)typePtr[0];

            return success != 0;
        }

        public bool IsOptimalityProven(Model model, out bool proven)
        {
            bool[] provenPtr = new bool[1];

            var success = clingo_model_optimality_proven(model.Object, provenPtr);

            proven = provenPtr[0];

            return success != 0;
        }

        public bool IsTrue(Model model, Literal literal, out bool result)
        {
            bool[] resultPtr = new bool[1];

            var success = clingo_model_is_true(model.Object, m_mapper.Map<Literal, clingo_literal>(literal), resultPtr);
            result = resultPtr[0];

            return success != 0;
        }

        #endregion

        #region Functions for Adding Clauses

        public bool GetContext(Model model, out SolveControl context)
        {
            IntPtr[] contextPtr = new IntPtr[1];

            var success = clingo_model_context(model.Object, contextPtr);
            context = new SolveControl() { Object = contextPtr[0] };

            return success != 0;
        }

        public bool GetSymbolicAtoms(SolveControl control, SymbolicAtoms atoms)
        {
            IntPtr[] symbolicAtomsPtr = new IntPtr[1];

            var success = clingo_solve_control_symbolic_atoms(control.Object, symbolicAtomsPtr);
            atoms = new SymbolicAtoms() { Object = symbolicAtomsPtr[0] };

            return success != 0;
        }

        public bool AddClause(SolveControl control, Literal[] clause)
        {
            clingo_literal[] clingoClause = m_mapper.Map<Literal[], clingo_literal[]>(clause);
            UIntPtr size = new UIntPtr(Convert.ToUInt32(clause != null ? clause.Length : 0));

            var success = clingo_solve_control_add_clause(control.Object, clingoClause, size);

            return success != 0;
        }

        #endregion

        #endregion
    }
}
