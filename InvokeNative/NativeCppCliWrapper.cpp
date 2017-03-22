#include "Native.h"

namespace NativeCppCliWrapper
{


using namespace System;
using namespace System::Runtime::InteropServices;


public ref class Wrapper {
public:
    
    static void CallDoWithNoParameters() {
        DoWithNoParameters();
    }
    
    static Int32 CallDoWithInt(Int32 a, Int32 b) {
        return DoWithInt(a, b);
    }
    
    static Double CallDoWithDouble(Double a, Double b) {
        return DoWithDouble(a, b);
    } 
    
    static void CallDoWithIntPointer(Int32 a, Int32 b, [Out] Int32% r) {
        // pin_ptr<int> p = &r;
        // DoWithIntPointer(a, b, p);
        
        int tmp;
        DoWithIntPointer(a, b, &tmp);
        r = tmp;
    }

    static void CallDoWithDoublePointer(Double a, Double b, [Out] Double% r) {
        // pin_ptr<double> p = &r;
        // DoWithDoublePointer(a, b, p);

        double tmp;
        DoWithDoublePointer(a, b, &tmp);
        r = tmp;
    }
};


}