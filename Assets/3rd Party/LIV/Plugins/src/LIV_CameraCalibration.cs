//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class LIV_CameraCalibration : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal LIV_CameraCalibration(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(LIV_CameraCalibration obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~LIV_CameraCalibration() {
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
          LIV_NativePINVOKE.delete_LIV_CameraCalibration(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public LIV_RigidTransform camera {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_camera_set(swigCPtr, LIV_RigidTransform.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = LIV_NativePINVOKE.LIV_CameraCalibration_camera_get(swigCPtr);
      LIV_RigidTransform ret = (cPtr == global::System.IntPtr.Zero) ? null : new LIV_RigidTransform(cPtr, false);
      return ret;
    } 
  }

  public LIV_Vector3 position {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_position_set(swigCPtr, LIV_Vector3.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = LIV_NativePINVOKE.LIV_CameraCalibration_position_get(swigCPtr);
      LIV_Vector3 ret = (cPtr == global::System.IntPtr.Zero) ? null : new LIV_Vector3(cPtr, false);
      return ret;
    } 
  }

  public float yaw {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_yaw_set(swigCPtr, value);
    } 
    get {
      float ret = LIV_NativePINVOKE.LIV_CameraCalibration_yaw_get(swigCPtr);
      return ret;
    } 
  }

  public float pitch {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_pitch_set(swigCPtr, value);
    } 
    get {
      float ret = LIV_NativePINVOKE.LIV_CameraCalibration_pitch_get(swigCPtr);
      return ret;
    } 
  }

  public float roll {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_roll_set(swigCPtr, value);
    } 
    get {
      float ret = LIV_NativePINVOKE.LIV_CameraCalibration_roll_get(swigCPtr);
      return ret;
    } 
  }

  public float VerticalFieldOfView {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_VerticalFieldOfView_set(swigCPtr, value);
    } 
    get {
      float ret = LIV_NativePINVOKE.LIV_CameraCalibration_VerticalFieldOfView_get(swigCPtr);
      return ret;
    } 
  }

  public float fov_override {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_fov_override_set(swigCPtr, value);
    } 
    get {
      float ret = LIV_NativePINVOKE.LIV_CameraCalibration_fov_override_get(swigCPtr);
      return ret;
    } 
  }

  public float nearClipPlane {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_nearClipPlane_set(swigCPtr, value);
    } 
    get {
      float ret = LIV_NativePINVOKE.LIV_CameraCalibration_nearClipPlane_get(swigCPtr);
      return ret;
    } 
  }

  public float farClipPlane {
    set {
      LIV_NativePINVOKE.LIV_CameraCalibration_farClipPlane_set(swigCPtr, value);
    } 
    get {
      float ret = LIV_NativePINVOKE.LIV_CameraCalibration_farClipPlane_get(swigCPtr);
      return ret;
    } 
  }

  public LIV_CameraCalibration() : this(LIV_NativePINVOKE.new_LIV_CameraCalibration(), true) {
  }

}