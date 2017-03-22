using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using CSLoadLibrary;


class TestCalli : AbstractTest {
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void DelegateWithNoParameters();
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int DelegateWithInt(int a, int b);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate double DelegateWithDouble(double a, double b);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void DelegateWithIntPointer(int a, int b, out int r);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void DelegateWithDoublePointer(double a, double b, out double r);
    
    
    private DelegateWithNoParameters doWithNoParameters = null;
    private DelegateWithInt doWithInt = null;
    private DelegateWithDouble doWithDouble = null;
    private DelegateWithIntPointer doWithIntPointer = null;
    private DelegateWithDoublePointer doWithDoublePointer = null;
    
    private UnmanagedLibrary nativeLib = null;
    
    
    public TestCalli() {
        base.Name = "Calli";
        
        this.nativeLib = new UnmanagedLibrary("Native");
        
        IntPtr nativeMethodAddress = 
            this.nativeLib.GetUnmanagedFunctionAddress(
                "DoWithNoParameters"
            );
        this.doWithNoParameters = 
            GetCalliDelegate
                <DelegateWithNoParameters>(
                nativeMethodAddress
            );
            
        nativeMethodAddress = 
            this.nativeLib.GetUnmanagedFunctionAddress(
                "DoWithInt"
            );
        this.doWithInt = 
            GetCalliDelegate
                <DelegateWithInt>(
                nativeMethodAddress
            );
            
        nativeMethodAddress = 
            this.nativeLib.GetUnmanagedFunctionAddress(
                "DoWithDouble"
            );
        this.doWithDouble = 
            GetCalliDelegate
                <DelegateWithDouble>(
                nativeMethodAddress
            );
            
        nativeMethodAddress = 
            this.nativeLib.GetUnmanagedFunctionAddress(
                "DoWithIntPointer"
            );
        this.doWithIntPointer = 
            GetCalliDelegate
                <DelegateWithIntPointer>(
                nativeMethodAddress
            );
            
        nativeMethodAddress = 
            this.nativeLib.GetUnmanagedFunctionAddress(
                "DoWithDoublePointer"
            );
        this.doWithDoublePointer = 
            GetCalliDelegate
                <DelegateWithDoublePointer>(
                nativeMethodAddress
            );
    }

    
    public override void TestWithNoParameters(int loops) {
        for (int i = 0; i < loops; i++) {
            this.doWithNoParameters();
        }
    }
  
    public override int TestWithInt(int loops) {
        int result = 0;
        for (int i = 0; i < loops; i++) {
            result = this.doWithInt(result, i);
        }
        return result;
    }
    
    public override double TestWithDouble(int loops) {
        double result = 0;
        for (int i = 0; i < loops; i++) {
            result = this.doWithDouble(result, i);
        }
        return result;
    }
    
    public override int TestWithIntPointer(int loops) {
        int result = 0;
        for (int i = 0; i < loops; i++) {
            this.doWithIntPointer(result, i, out result);
        }
        return result;
    }
    
    public override double TestWithDoublePointer(int loops) {
        double result = 0;
        for (int i = 0; i < loops; i++) {
            this.doWithDoublePointer(result, i, out result);
        }
        return result;
    }
    
    
    private static TDelegate GetCalliDelegate
        <TDelegate>(
        IntPtr methodAddress
    ) where TDelegate : class
    {
        Type delegateType = typeof(TDelegate);
        MethodInfo invokeInfo = delegateType.GetMethod("Invoke");
        // Gets the return type for the dynamic method and calli.
        // Note: for calli, a type such as System.Int32&amp; must be
        // converted to System.Int32* otherwise the execution will be slower.
        Type invokeReturnType = invokeInfo.ReturnType;
        Type calliReturnType = GetPointerTypeIfReference(
            invokeInfo.ReturnType);
        // Gets the parameter types for the dynamic method and calli.
        ParameterInfo[] invokeParameters = invokeInfo.GetParameters();
        Type[] invokeParameterTypes = new Type[invokeParameters.Length];
        Type[] calliParameterTypes = new Type[invokeParameters.Length];
        for (int i = 0; i < invokeParameters.Length; i++) {
            invokeParameterTypes[i] = invokeParameters[i].ParameterType;
            calliParameterTypes[i] = GetPointerTypeIfReference(
                invokeParameters[i].ParameterType);
        }

#if SAVECALLI
        // Defines an assembly with a module and a type.
        string outputDllName = "TestCalli_" + delegateType.Name + ".dll";
        AssemblyName assemblyName = new AssemblyName("TestAssembly");
        AssemblyBuilder assemblyBuilder = 
            AppDomain.CurrentDomain.DefineDynamicAssembly(
                assemblyName, 
                AssemblyBuilderAccess.RunAndSave
            );
        ModuleBuilder moduleBuilder = 
            assemblyBuilder.DefineDynamicModule(
                "TestModule", 
                outputDllName
            );
        TypeBuilder typeBuilder = 
            moduleBuilder.DefineType(
                "TestCalli", 
                TypeAttributes.Public | TypeAttributes.Sealed
            );
        MethodBuilder methodBuilder = 
            typeBuilder.DefineMethod(
                "CalliInvoke", 
                MethodAttributes.Public | MethodAttributes.Static, 
                invokeReturnType, 
                invokeParameterTypes
            );
            
        // Gets an ILGenerator.
        ILGenerator generator = methodBuilder.GetILGenerator();
#else
        // Defines the dynamic method.
        DynamicMethod calliMethod = 
            new DynamicMethod(
                "CalliInvoke", 
                invokeReturnType, 
                invokeParameterTypes, 
                typeof(TestCalli), 
                true
            );
            
        // Gets an ILGenerator.
        ILGenerator generator = calliMethod.GetILGenerator();
#endif

        // Emits instructions for loading the parameters into the stack.
        for (int i = 0; i < calliParameterTypes.Length; i++) {
            if (i == 0) {
                generator.Emit(OpCodes.Ldarg_0);
            } else if (i == 1) {
                generator.Emit(OpCodes.Ldarg_1);
            } else if (i == 2) {
                generator.Emit(OpCodes.Ldarg_2);
            } else if (i == 3) {
                generator.Emit(OpCodes.Ldarg_3);
            } else {
                generator.Emit(OpCodes.Ldarg, i);
            }
        }
        // Emits instruction for loading the address of the native function
        // into the stack.
        switch (IntPtr.Size) {
            case 4:
                generator.Emit(OpCodes.Ldc_I4, methodAddress.ToInt32());
                break;
            case 8:
                generator.Emit(OpCodes.Ldc_I8, methodAddress.ToInt64());
                break;
            default:
                throw new PlatformNotSupportedException();
        }
        // Emits calli opcode.
        generator.EmitCalli(OpCodes.Calli, CallingConvention.Cdecl, 
            calliReturnType, calliParameterTypes);
        // Emits instruction for returning a value.
        generator.Emit(OpCodes.Ret);
                
#if SAVECALLI
        // Finishes the type.
        Type newType = typeBuilder.CreateType();
         // Saves the assembly.
        assemblyBuilder.Save(outputDllName);
        
        object tmp = (object)Delegate.CreateDelegate(
            delegateType, newType.GetMethod("CalliInvoke"));
#else
        object tmp = (object)calliMethod.CreateDelegate(delegateType);
#endif

        return (TDelegate)tmp;
    }
    
    private static Type GetPointerTypeIfReference(Type type) {
        if (type.IsByRef) {
            return Type.GetType(type.FullName.Replace("&", "*"));
        }
        return type;
    }
}
