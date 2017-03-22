#! /bin/bash
clear
gcc -c -O2 -o Native.o Native.c
gcc -s -shared -o Native.so Native.o
#Without -define:SAVECALLI, EmitCalli throws an exception (Mono 2.6.7-5).
gmcs -define:MONO -define:POSIX -define:SAVECALLI -optimize -out:Test.exe -target:exe Main.cs AbstractTest.cs TestCalli.cs TestDelegate.cs TestDelegateS.cs TestDynamicS.cs TestManaged.cs TestPInvoke.cs TestPInvokeS.cs UnmanagedLibrary.cs
