class TestManaged : AbstractTest{
    private void DoWithNoParameters() { 
        return;
    }

    private int DoWithInt(int a, int b) {
        return a + b;
    }
    
    private double DoWithDouble(double a, double b) {
        return a + b;
    }
    
    private void DoWithIntPointer(int a, int b, out int r) {
        r = a + b;
    }
    
    private void DoWithDoublePointer(double a, double b, out double r) {
        r = a + b;
    }
    

    public TestManaged() {
        base.Name = "Managed";
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