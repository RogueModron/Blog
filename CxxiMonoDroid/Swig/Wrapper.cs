/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.3
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace Wrapper {

using System;
using System.Runtime.InteropServices;

public class Wrapper : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Wrapper(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(Wrapper obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~Wrapper() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ClippingLibraryPINVOKE.delete_Wrapper(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public Wrapper() : this(ClippingLibraryPINVOKE.new_Wrapper(), true) {
  }

  public void AddInnerRing() {
    ClippingLibraryPINVOKE.Wrapper_AddInnerRing(swigCPtr);
  }

  public void AddPoint(double x, double y) {
    ClippingLibraryPINVOKE.Wrapper_AddPoint(swigCPtr, x, y);
  }

  public void AddPolygon() {
    ClippingLibraryPINVOKE.Wrapper_AddPolygon(swigCPtr);
  }

  public void ExecuteDifference() {
    ClippingLibraryPINVOKE.Wrapper_ExecuteDifference(swigCPtr);
  }

  public void ExecuteIntersection() {
    ClippingLibraryPINVOKE.Wrapper_ExecuteIntersection(swigCPtr);
  }

  public void ExecuteUnion() {
    ClippingLibraryPINVOKE.Wrapper_ExecuteUnion(swigCPtr);
  }

  public void ExecuteXor() {
    ClippingLibraryPINVOKE.Wrapper_ExecuteXor(swigCPtr);
  }

  public bool GetInnerRing() {
    bool ret = ClippingLibraryPINVOKE.Wrapper_GetInnerRing(swigCPtr);
    return ret;
  }

  public bool GetPoint(ref double x, ref double y) {
    bool ret = ClippingLibraryPINVOKE.Wrapper_GetPoint(swigCPtr, ref x, ref y);
    return ret;
  }

  public bool GetPolygon() {
    bool ret = ClippingLibraryPINVOKE.Wrapper_GetPolygon(swigCPtr);
    return ret;
  }

  public void InitializeClip() {
    ClippingLibraryPINVOKE.Wrapper_InitializeClip(swigCPtr);
  }

  public void InitializeSubject() {
    ClippingLibraryPINVOKE.Wrapper_InitializeSubject(swigCPtr);
  }

}

}
