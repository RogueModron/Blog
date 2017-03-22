%module ClippingLibrary

%include <windows.i>

%include "typemaps.i"
 
%define %TypeOutParam(TYPE)
    %apply TYPE& INOUT { TYPE& };
%enddef
 
%TypeOutParam(double)

%{
#include "Wrapper.h"
%}

%include "Wrapper.h"