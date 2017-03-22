public abstract class AbstractTest {
    private string testName;
    
    public string Name {
        get { 
            return testName;
        }
        set {
            testName = value;
        }
    }
    
    abstract public void TestWithNoParameters(int loops);
    
    abstract public int TestWithInt(int loops);
    
    abstract public double TestWithDouble(int loops);
    
    abstract public int TestWithIntPointer(int loops);
    
    abstract public double TestWithDoublePointer(int loops);
}