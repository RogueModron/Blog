using System.Runtime.InteropServices;
using System.Security;
using CSLoadLibrary;


class TestDelegateS : AbstractTest {
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void DelegateWithNoParameters();
    
    [SuppressUnmanagedCodeSecurity]    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int DelegateWithInt(int a, int b);
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate double DelegateWithDouble(double a, double b);
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void DelegateWithIntPointer(int a, int b, out int r);
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void DelegateWithDoublePointer(double a, double b, out double r);
    
    
    private DelegateWithNoParameters doWithNoParameters = null;
    private DelegateWithInt doWithInt = null;
    private DelegateWithDouble doWithDouble = null;
    private DelegateWithIntPointer doWithIntPointer = null;
    private DelegateWithDoublePointer doWithDoublePointer = null;
    
    private UnmanagedLibrary nativeLib = null;
    
    
    public TestDelegateS() {
        base.Name = "DelegateS";
        
        this.nativeLib = new UnmanagedLibrary("Native");
        this.doWithNoParameters = 
            this.nativeLib.GetUnmanagedFunction
                <DelegateWithNoParameters>(
                "DoWithNoParameters"
            );
        this.doWithInt = 
            this.nativeLib.GetUnmanagedFunction
                <DelegateWithInt>(
                "DoWithInt"
            );
        this.doWithDouble = 
            this.nativeLib.GetUnmanagedFunction
                <DelegateWithDouble>(
                "DoWithDouble"
            );
        this.doWithIntPointer = 
            this.nativeLib.GetUnmanagedFunction
                <DelegateWithIntPointer>(
                "DoWithIntPointer"
            );
        this.doWithDoublePointer = 
            this.nativeLib.GetUnmanagedFunction
                <DelegateWithDoublePointer>(
                "DoWithDoublePointer"
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
    
    public override int TestWithIntPointer(int loops) {
        int result = 0;
        for (int i = 0; i < loops; i++) {
            this.doWithIntPointer(result, i, out result);
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
    
    public override double TestWithDoublePointer(int loops) {
        double result = 0;
        for (int i = 0; i < loops; i++) {
            this.doWithDoublePointer(result, i, out result);
        }
        return result;
    }
}
