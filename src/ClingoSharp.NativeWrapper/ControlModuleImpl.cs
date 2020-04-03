using AutoMapper;
using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Callbacks;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.EventHandlers;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.CoreServices.Types;
using ClingoSharp.NativeWrapper.Callbacks;
using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.EventHandlers;
using ClingoSharp.NativeWrapper.Types;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper
{
    /// <summary>
    /// Functions to control the grounding and solving process.
    /// </summary>
    class ControlModuleImpl : IControlModule
    {
        private readonly IMapper m_mapper;

        #region Constructor

        public ControlModuleImpl()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Part, clingo_part>()
                    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.params_list, opt => opt.MapFrom(src => src.Params))
                    .ForMember(dest => dest.size, opt => opt.MapFrom(src => new UIntPtr(Convert.ToUInt32(src.Params.Length))))
                    .ReverseMap();

                cfg.CreateMap<Location, clingo_location>()
                    .ForMember(dest => dest.begin_file, opt => opt.MapFrom(src => src.BeginFile))
                    .ForMember(dest => dest.begin_line, opt => opt.MapFrom(src => src.BeginLine))
                    .ForMember(dest => dest.begin_column, opt => opt.MapFrom(src => new UIntPtr(Convert.ToUInt32(src.BeginColumn))))
                    .ForMember(dest => dest.end_file, opt => opt.MapFrom(src => new UIntPtr(Convert.ToUInt32(src.EndFile))))
                    .ForMember(dest => dest.end_line, opt => opt.MapFrom(src => new UIntPtr(Convert.ToUInt32(src.EndLine))))
                    .ForMember(dest => dest.end_column, opt => opt.MapFrom(src => new UIntPtr(Convert.ToUInt32(src.EndColumn))))
                    .ReverseMap();
            });

            m_mapper = config.CreateMapper();
        }

        #endregion

        #region Clingo C API Functions

        #region Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_new(string[] arguments, UIntPtr arguments_size, clingo_logger logger, IntPtr logger_data, uint message_limit, [Out] IntPtr[] control);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_control_free(IntPtr control);

        #endregion

        #region Grounding Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_add(IntPtr control, string name, string[] parameters, UIntPtr parameters_size, string program);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_ground(IntPtr control, clingo_part[] parts, UIntPtr parts_size, clingo_ground_callback ground_callback, IntPtr ground_callback_data);

        #endregion

        #region Solving Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_control_solve(IntPtr control, clingo_solve_mode mode, int[] assumptions, UIntPtr assumptions_size, clingo_solve_event_callback notify, IntPtr data, [Out] IntPtr[] handle);

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

        public bool New(string[] arguments, LoggerCallback logger, uint messageLimit, out Control control)
        {
            void clingoLoggerCallback(clingo_warning code, string message, IntPtr data)
            {
                logger((WarningCode)code, message, data);
            }

            UIntPtr argumentsSize = new UIntPtr(Convert.ToUInt32(arguments == null ? 0 : arguments.Length));

            IntPtr[] controlPtr = new IntPtr[1];

            var success = clingo_control_new(arguments, argumentsSize, (logger == null) ? null : (clingo_logger)clingoLoggerCallback, IntPtr.Zero, messageLimit, controlPtr);
            control = new Control() { Object = controlPtr[0] };

            return success != 0;
        }

        public void Free(Control control)
        {
            clingo_control_free(control.Object);
        }

        #endregion

        #region Grounding Functions

        public bool Add(Control control, string name, string[] parameters, string program)
        {
            UIntPtr parametersSize = new UIntPtr(Convert.ToUInt32(parameters == null ? 0 : parameters.Length));

            var success = clingo_control_add(control.Object, name, parameters, parametersSize, program);
            
            return success != 0;
        }

        public bool Ground(Control control, Part[] parts, GroundCallback callback)
        {
            int clingoGroundCallback(clingo_location[] clingoLocation, string name, ulong[] clingoArguments, UIntPtr arguments_size, IntPtr data, clingo_symbol_callback symbol_callback, IntPtr symbol_callback_data)
            {
                bool symbolCallback(Symbol[] symbols, IntPtr data)
                {
                    ulong[] clingoSymbols = symbols?.Select(s => (ulong)s).ToArray();
                    UIntPtr clingoSymbolsSize = new UIntPtr(Convert.ToUInt32(symbols == null ? 0 : symbols.Length));

                    var success = symbol_callback(clingoSymbols, new UIntPtr(Convert.ToUInt32(symbols.Length)), data);

                    return success != 0;
                }

                Location location = clingoLocation == null ? null : m_mapper.Map<clingo_location, Location>(clingoLocation[0]);

                Symbol[] arguments = clingoArguments?.Select(arg => (Symbol)arg).ToArray();

                var success = callback(location, name, arguments, data, symbolCallback, symbol_callback_data);

                return success ? 1 : 0;
            }

            clingo_part[] clingoParts = parts == null ? null : m_mapper.Map<Part[], clingo_part[]>(parts);
            UIntPtr clingoPartsSize = new UIntPtr(Convert.ToUInt32(parts == null ? 0 : parts.Length));

            var success = clingo_control_ground(control.Object, clingoParts, clingoPartsSize, (callback == null) ? null : (clingo_ground_callback)clingoGroundCallback, IntPtr.Zero);

            return success != 0;
        }

        #endregion

        #region Solving Functions

        public bool Solve(Control control, SolveMode mode, Literal[] assumptions, SolveEventHandler callback, out SolveHandle handler)
        {
            int clingoSolveEventCallback(clingo_solve_event_type type, IntPtr eventData, IntPtr data, out bool[] goon)
            {
                SolveEventType solveEventType = (SolveEventType)type;

                var success = callback(solveEventType, eventData, out bool doneValue);

                goon = new bool[1] { doneValue };

                return success ? 1 : 0;
            }

            clingo_solve_mode cligoSolveMode = (clingo_solve_mode)mode;

            int[] clingoAssumptions = assumptions?.Select(ass => (int)ass).ToArray();
            UIntPtr clingoAssumptionsSize = new UIntPtr(Convert.ToUInt32(assumptions == null ? 0 : assumptions.Length));

            IntPtr[] handlerPtr = new IntPtr[1];

            var success = clingo_control_solve(control.Object, cligoSolveMode, clingoAssumptions, clingoAssumptionsSize, (callback == null) ? null : (clingo_solve_event_callback)clingoSolveEventCallback, IntPtr.Zero, handlerPtr);

            handler = new SolveHandle() { Object = handlerPtr[0] };

            return success != 0;
        }

        #endregion

        #region Program Inspection Functions

        public bool GetConst(Control control, string name, out Symbol symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            var success = clingo_control_get_const(control.Object, name, symbolPtr);
            symbol = symbolPtr[0];
            return success != 0;
        }

        public bool HasConst(Control control, string name, out bool exists)
        {
            bool[] existsPtr = new bool[1];
            var success = clingo_control_has_const(control.Object, name, existsPtr);
            exists = existsPtr[0];
            return success != 0;
        }

        public bool GetSymbolicAtoms(Control control, out SymbolicAtoms symbolicAtoms)
        {
            IntPtr[] symbolicAtomsPtr = new IntPtr[1];
            var success = clingo_control_symbolic_atoms(control.Object, symbolicAtomsPtr);
            symbolicAtoms = new SymbolicAtoms() { Object = symbolicAtomsPtr[0] };
            return success != 0;
        }

        #endregion

        #endregion
    }
}
