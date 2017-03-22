using System;
using System.Diagnostics;


class Test {
    static void Main() {
        AbstractTest[] tests = new AbstractTest[] {
            new TestManaged(),
#if !MONO
            new TestCppCli(),
#endif
            new TestPInvoke(),
            new TestPInvokeS(),
            new TestDelegate(),
            new TestDelegateS(),
            new TestDynamicS(),
            new TestCalli()
        };

        const int MaxLoops = 100000000;

        Stopwatch watch = new Stopwatch();
       
        foreach (AbstractTest test in tests) {
            watch.Start();
            test.TestWithNoParameters(MaxLoops);
            watch.Stop();
            WriteOutput(test.Name, "N", watch.ElapsedMilliseconds, "NA");
            watch.Reset();
            
            watch.Start();
            int intResult = test.TestWithInt(MaxLoops);
            watch.Stop();
            WriteOutput(test.Name, "I", watch.ElapsedMilliseconds, intResult);
            watch.Reset();
            
            watch.Start();
            double doubleResult = test.TestWithDouble(MaxLoops);
            watch.Stop();
            WriteOutput(test.Name, "D", watch.ElapsedMilliseconds, doubleResult);
            watch.Reset();
            
            watch.Start();
            intResult = test.TestWithIntPointer(MaxLoops);
            watch.Stop();
            WriteOutput(test.Name, "IP", watch.ElapsedMilliseconds, intResult);
            watch.Reset();
            
            watch.Start();
            doubleResult = test.TestWithDoublePointer(MaxLoops);
            watch.Stop();
            WriteOutput(test.Name, "DP", watch.ElapsedMilliseconds, doubleResult);
            watch.Reset();
            
            Console.WriteLine();
        }
    }
    
    static void WriteOutput(string testName, string testId, long milliseconds, object result) {
        Console.WriteLine("{0,-15} {1,-2}  ms {2:000000}  r {3}",
            testName, testId, milliseconds, result);
    }

}