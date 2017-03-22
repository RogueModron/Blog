using System.Runtime.InteropServices;
using System.Security;


[SuppressUnmanagedCodeSecurity]
class TestPInvokeS : AbstractTest {
#if POSIX
    [DllImport("./Native.so", CallingConvention = CallingConvention.Cdecl)]
#else
    [DllImport("Native.dll", CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern void DoWithNoParameters();
    
#if POSIX
    [DllImport("./Native.so", CallingConvention = CallingConvention.Cdecl)]
#else
    [DllImport("Native.dll", CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern int DoWithInt(int a, int b);
    
#if POSIX
    [DllImport("./Native.so", CallingConvention = CallingConvention.Cdecl)]
#else
    [DllImport("Native.dll", CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern double DoWithDouble(double a, double b);
    
#if POSIX
    [DllImport("./Native.so", CallingConvention = CallingConvention.Cdecl)]
#else
    [DllImport("Native.dll", CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern void DoWithIntPointer(int a, int b, out int r); 
    
#if POSIX
    [DllImport("./Native.so", CallingConvention = CallingConvention.Cdecl)]
#else
    [DllImport("Native.dll", CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern void DoWithDoublePointer(double a, double b, out double r);

    
    public TestPInvokeS() {
        base.Name = "PInvokeS";
    }
    
    
    public override void TestWithNoParameters(int loops) {
        for (int i = 0; i < loops; i++) {
            DoWithNoParameters();
        }
    }
    
    public override int TestWithInt(int loops) {
        int result = 0;
        for (int i = 0; i < loops; i++) {
            result = DoWithInt(result, i);
        }
        return result;
    }
    
    public override double TestWithDouble(int loops) {
        double result = 0;
        for (int i = 0; i < loops; i++) {
            result = DoWithDouble(result, i);
        }
        return result;
    }
    
    public override int TestWithIntPointer(int loops) {
        int result = 0;
        for (int i = 0; i < loops; i++) {
            DoWithIntPointer(result, i, out result);
        }
        return result;
    }
    
    public override double TestWithDoublePointer(int loops) {
        double result = 0;
        for (int i = 0; i < loops; i++) {
            DoWithDoublePointer(result, i, out result);
        }
        return result;
    }
}
