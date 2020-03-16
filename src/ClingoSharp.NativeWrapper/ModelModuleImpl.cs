using AutoMapper;
using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.CoreServices.Types;
using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.EventHandlers;
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

        /// <summary>
        /// Get the type of the model.
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="type">the type of the model</param>
        /// <returns>whether the call was successful</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_type(IntPtr model, [Out] clingo_model_type[] type);

        /// <summary>
        /// Get the running number of the model.
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="number">the number of the model</param>
        /// <returns>whether the call was successful</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_number(IntPtr model, [Out] ulong[] number);

        /// <summary>
        /// Get the number of symbols of the selected types in the model.
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="show">which symbols to select</param>
        /// <param name="size">the number symbols</param>
        /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/></returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_symbols_size(IntPtr model, clingo_show_type show, [Out] UIntPtr[] size);

        /// <summary>
        /// Get the symbols of the selected types in the model.
        /// </summary>
        /// <remarks>
        /// CSP assignments are represented using functions with name "$" where the first argument is the name of the CSP variable and the second one its value.
        /// </remarks>
        /// <param name="model">the target</param>
        /// <param name="show">which symbols to select</param>
        /// <param name="symbols">the resulting symbols</param>
        /// <param name="size">the number of selected symbols</param>
        /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/>, <see cref="clingo_error.clingo_error_runtime"/> if the size is too small</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_symbols(IntPtr model, clingo_show_type show, [Out] clingo_symbol[] symbols, UIntPtr size);

        /// <summary>
        /// Check if a program literal is true in a model.
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="literal">the literal to lookup</param>
        /// <param name="result">whether the literal is true</param>
        /// <returns>whether the call was successful</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_is_true(IntPtr model, clingo_literal literal, [Out] bool[] result);

        /// <summary>
        /// Get the number of cost values of a model.
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="size">the number of costs</param>
        /// <returns>whether the call was successful</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_cost_size(IntPtr model, [Out] UIntPtr[] size);

        /// <summary>
        /// Get the cost vector of a model
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="costs">the resulting costs</param>
        /// <param name="size">the number of costs</param>
        /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/>, <see cref="clingo_error.clingo_error_runtime"/> if the size is too small</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_cost(IntPtr model, [Out] long[] costs, UIntPtr size);

        /// <summary>
        /// Whether the optimality of a model has been proven.
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="proven">whether the optimality has been proven</param>
        /// <returns>whether the call was successful</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_optimality_proven(IntPtr model, [Out] bool[] proven);

        /// <summary>
        /// Get the id of the solver thread that found the model.
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="id">the resulting thread id</param>
        /// <returns>whether the call was successful</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_thread_id(IntPtr model, [Out] clingo_id[] id);

        /// <summary>
        /// Constant time lookup to test whether an atom is in a model.
        /// </summary>
        /// <param name="model">the target</param>
        /// <param name="atom">the atom to lookup</param>
        /// <param name="contained">whether the atom is contained</param>
        /// <returns>whether the atom is contained</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_contains(IntPtr model, clingo_symbol atom, [Out] bool[] contained);

        /// <summary>
        /// Add symbols to the model.
        /// </summary>
        /// These symbols will appear in clingo's output, which means that this function is only meaningful if there is an underlying clingo application. Only models passed to the <see cref="clingo_solve_event_callback"/> are extendable.
        /// <param name="model">the target</param>
        /// <param name="symbols">the symbols to add</param>
        /// <param name="size">the number of symbols to add</param>
        /// <returns>whether the call was successful</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_extend(IntPtr model, clingo_symbol[] symbols, UIntPtr size);

        /// <summary>
        /// Get the associated solve control object of a model.
        /// </summary>
        /// This object allows for adding clauses during model enumeration.
        /// <param name="model">the target</param>
        /// <param name="control">the resulting solve control object</param>
        /// <returns>whether the call was successful</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_model_context(IntPtr model, [Out] IntPtr[] control);

        #endregion

        #region Module implementation

        public bool Contains(IntPtr model, Symbol atom, out bool contained)
        {
            bool[] containedPtr = new bool[1];

            var success = clingo_model_contains(model, atom.Value, containedPtr);
            contained = containedPtr[0];

            return success != 0;
        }

        public bool Extends(IntPtr model, Symbol[] atoms)
        {
            clingo_symbol[] clingoSymbols = m_mapper.Map<Symbol[], clingo_symbol[]>(atoms);
            UIntPtr clingoSymbolsSize = new UIntPtr(Convert.ToUInt32(atoms == null ? 0 : atoms.Length));

            var success = clingo_model_extend(model, clingoSymbols, clingoSymbolsSize);

            return success != 0;
        }

        public bool GetContext(IntPtr model, out IntPtr context)
        {
            IntPtr[] contextPtr = new IntPtr[1];
            
            var success = clingo_model_context(model, contextPtr);
            context = contextPtr[0];

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

        public bool GetSymbols(IntPtr model, ShowType showType, out Symbol[] symbols)
        {
            UIntPtr[] symbolsSizePtr = new UIntPtr[1];
            var success = clingo_model_symbols_size(model, (clingo_show_type)showType, symbolsSizePtr);

            if (success != 0)
            {
                clingo_symbol[] clingoSymbols = new clingo_symbol[symbolsSizePtr[0].ToUInt64()];

                success = clingo_model_symbols(model, (clingo_show_type)showType, clingoSymbols, symbolsSizePtr[0]);
                symbols = m_mapper.Map<clingo_symbol[], Symbol[]>(clingoSymbols);

                return success != 0;
            }

            symbols = null;
            return false;
        }

        public bool GetThreadId(IntPtr model, out uint id)
        {
            clingo_id[] idPtr = new clingo_id[1];
            
            var success = clingo_model_thread_id(model, idPtr);
            id = idPtr[0];

            return success != 0;
        }

        public bool GetType(IntPtr model, out ModelType type)
        {
            clingo_model_type[] typePtr = new clingo_model_type[1];

            var success = clingo_model_type(model, typePtr);

            type = (ModelType)typePtr[0];

            return success != 0;
        }

        public bool IsOptimalityProven(IntPtr model, out bool proven)
        {
            bool[] provenPtr = new bool[1];

            var success = clingo_model_optimality_proven(model, provenPtr);

            proven = provenPtr[0];

            return success != 0;
        }

        public bool IsTrue(IntPtr model, Literal literal, out bool result)
        {
            bool[] resultPtr = new bool[1];

            var success = clingo_model_is_true(model, m_mapper.Map<Literal, clingo_literal>(literal), resultPtr);
            result = resultPtr[0];

            return success != 0;
        }

        #endregion
    }
}
