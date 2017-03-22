using NativeCppCliWrapper;


class TestCppCli : AbstractTest {
    public TestCppCli() {
        base.Name = "CppCli";
    }
    
    
    public override void TestWithNoParameters(int loops) {
        for (int i = 0; i < loops; i++) {
            Wrapper.CallDoWithNoParameters();
        }
    }
    
     public override int TestWithInt(int loops) {
        int result = 0;
        for (int i = 0; i < loops; i++) {
            result = Wrapper.CallDoWithInt(result, i);
        }
        return result;
    }   
    
    public override double TestWithDouble(int loops) {
        double result = 0;
        for (int i = 0; i < loops; i++) {
            result = Wrapper.CallDoWithDouble(result, i);
        }
        return result;
    }
    
    public override int TestWithIntPointer(int loops) {
        int result = 0;
        for (int i = 0; i < loops; i++) {
            Wrapper.CallDoWithIntPointer(result, i, out result);
        }
        return result;
    }
    
    public override double TestWithDoublePointer(int loops) {
        double result = 0;
        for (int i = 0; i < loops; i++) {
            Wrapper.CallDoWithDoublePointer(result, i, out result);
        }
        return result;
    }
}