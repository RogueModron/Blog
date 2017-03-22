using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;


class TestDynamicS : AbstractTest {
    delegate void DelegateWithNoParameters();
    
    delegate int DelegateWithInt(int a, int b);
    
    delegate double DelegateWithDouble(double a, double b);

    delegate void DelegateWithIntPointer(int a, int b, out int r);

    delegate void DelegateWithDoublePointer(double a, double b, out double r);
    
    
    private DelegateWithNoParameters doWithNoParameters = null;
    private DelegateWithInt doWithInt = null;
    private DelegateWithDouble doWithDouble = null;
    private DelegateWithIntPointer doWithIntPointer = null;
    private DelegateWithDoublePointer doWithDoublePointer = null;
    
    
    public TestDynamicS() {
        base.Name = "DynamicS";
        
        string libraryName = "Native";
        CallingConvention callingConvention = CallingConvention.Cdecl;
        
        this.doWithNoParameters = 
            GetDynamicSDelegate
                <DelegateWithNoParameters>(
                libraryName, 
                "DoWithNoParameters", 
                callingConvention
            );
        this.doWithInt = 
            GetDynamicSDelegate
                <DelegateWithInt>(
                libraryName, 
                "DoWithInt", 
                callingConvention
            );
        this.doWithDouble = 
            GetDynamicSDelegate
                <DelegateWithDouble>(
                libraryName, 
                "DoWithDouble", 
                callingConvention
            );
        this.doWithIntPointer = 
            GetDynamicSDelegate
                <DelegateWithIntPointer>(
                libraryName, 
                "DoWithIntPointer", 
                callingConvention
            );
        this.doWithDoublePointer = 
            GetDynamicSDelegate
                <DelegateWithDoublePointer>(
                libraryName, 
                "DoWithDoublePointer", 
                callingConvention
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
    

    private static TDelegate GetDynamicSDelegate
        <TDelegate>(
        string libraryName, 
        string entryPoint, 
        CallingConvention callingConvention
    ) where TDelegate : class 
    {
        Type delegateType = typeof(TDelegate);
        MethodInfo invokeInfo = delegateType.GetMethod("Invoke");
        // Gets the return type for the P/Invoke method.
        Type invokeReturnType = invokeInfo.ReturnType;
        // Gets the parameter types for the P/Invoke method.
        ParameterInfo[] invokeParameters = invokeInfo.GetParameters();
        Type[] invokeParameterTypes = new Type[invokeParameters.Length];
        for (int i = 0; i < invokeParameters.Length; i++) {
            invokeParameterTypes[i] = invokeParameters[i].ParameterType;
        }

        // Defines an assembly with a module and a type.
        AssemblyName assemblyName = new AssemblyName("TestAssembly");
        AssemblyBuilder assemblyBuilder = 
            AppDomain.CurrentDomain.DefineDynamicAssembly(
                assemblyName, 
                AssemblyBuilderAccess.Run
            );
        ModuleBuilder moduleBuilder = 
            assemblyBuilder.DefineDynamicModule(
                "TestModule"
            );
        TypeBuilder typeBuilder = 
            moduleBuilder.DefineType(
                "TestDynamicS"
            );
            
        //Defines a P/Invoke method called Invoke.
#if POSIX
        libraryName = "./" + libraryName + ".so";
#else
        libraryName = libraryName + ".dll";
#endif
        MethodBuilder methodBuilder = 
            typeBuilder.DefinePInvokeMethod(
                "Invoke", 
                libraryName, 
                entryPoint, 
                MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl,
                CallingConventions.Standard, 
                invokeReturnType, 
                invokeParameterTypes, 
                callingConvention, 
                CharSet.Ansi
            );
        methodBuilder.SetImplementationFlags(
            methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig
        );
        
        // Adds SuppressUnmanagedCodeSecurityAttribute to the method.
        Type attributeType = typeof(System.Security.SuppressUnmanagedCodeSecurityAttribute);
        ConstructorInfo attributeConstructorInfo = attributeType.GetConstructor(new Type[] {});
        CustomAttributeBuilder attributeBuilder = 
            new CustomAttributeBuilder(
                attributeConstructorInfo, 
                new object[] {}
            );
        methodBuilder.SetCustomAttribute(attributeBuilder);

        // Finishes the type.
        Type newType = typeBuilder.CreateType();
        
        object tmp = (object)Delegate.CreateDelegate(delegateType, newType.GetMethod("Invoke"));
        return (TDelegate)tmp;
    }
}
