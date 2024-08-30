//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class LIV_ClipPlane : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal LIV_ClipPlane(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(LIV_ClipPlane obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~LIV_ClipPlane() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          LIV_NativePINVOKE.delete_LIV_ClipPlane(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public LIV_Matrix4x4 transform {
    set {
      LIV_NativePINVOKE.LIV_ClipPlane_transform_set(swigCPtr, LIV_Matrix4x4.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = LIV_NativePINVOKE.LIV_ClipPlane_transform_get(swigCPtr);
      LIV_Matrix4x4 ret = (cPtr == global::System.IntPtr.Zero) ? null : new LIV_Matrix4x4(cPtr, false);
      return ret;
    } 
  }

  public int width {
    set {
      LIV_NativePINVOKE.LIV_ClipPlane_width_set(swigCPtr, value);
    } 
    get {
      int ret = LIV_NativePINVOKE.LIV_ClipPlane_width_get(swigCPtr);
      return ret;
    } 
  }

  public int height {
    set {
      LIV_NativePINVOKE.LIV_ClipPlane_height_set(swigCPtr, value);
    } 
    get {
      int ret = LIV_NativePINVOKE.LIV_ClipPlane_height_get(swigCPtr);
      return ret;
    } 
  }

  public float tesselation {
    set {
      LIV_NativePINVOKE.LIV_ClipPlane_tesselation_set(swigCPtr, value);
    } 
    get {
      float ret = LIV_NativePINVOKE.LIV_ClipPlane_tesselation_get(swigCPtr);
      return ret;
    } 
  }

  public LIV_ClipPlane() : this(LIV_NativePINVOKE.new_LIV_ClipPlane(), true) {
  }

}