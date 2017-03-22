cls


cl /Gd /LD /O2 /Oi Native.c Native.def /link /OUT:"Native.dll"

rem gcc -c -O2 -o Native.o Native.c
rem gcc -s -shared -o Native.dll Native.o Native.def

gmcs -define:MONO -optimize -out:Test.exe -target:exe Main.cs AbstractTest.cs TestCalli.cs TestDelegate.cs TestDelegateS.cs TestDynamicS.cs TestManaged.cs TestPInvoke.cs TestPInvokeS.cs UnmanagedLibrary.cs