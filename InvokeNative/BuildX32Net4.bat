cls


cl /nologo /c /Gd /LD /O2 /Oi /TP Native.c
lib /NOLOGO Native.obj /DEF:Native.def /OUT:Native.lib
cl /nologo /clr /LD /O2 /Oi NativeCppCliWrapper.cpp /link /DLL Native.lib

cl /nologo /Gd /LD /O2 /Oi Native.c Native.def /link /OUT:Native.dll

csc /nologo /platform:x86 /o /out:Test.exe Main.cs AbstractTest.cs TestCalli.cs TestCppCli.cs TestDelegate.cs TestDelegateS.cs TestDynamicS.cs TestManaged.cs TestPInvoke.cs TestPInvokeS.cs UnmanagedLibrary.cs /r:NativeCppCliWrapper.dll