using System;
using System.Runtime.InteropServices;

namespace Gravicode.TFLite;

/// <summary>
/// A Safe handle for a RTLite tensor.
/// </summary>
public class TensorSafeHandle : SafeHandle
{
    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Constructor of the TensorSafeHandle struct.
    /// </summary>
    /// <param name="handle">The tensor handle.</param>
    public TensorSafeHandle(IntPtr handle) : base(IntPtr.Zero, true)
    {
        SetHandle(handle);
    }

    /// <inheritdoc />
    protected override bool ReleaseHandle()
    {
        // ToDo: Implement the release handle logic.
        return false;
    }

    /// <summary>
    /// Implicit conversion from IntPtr to TensorSafeHandle.
    /// </summary>
    /// <param name="handle">The tensor handle.</param>
    /// <returns>The TensorSafeHandle corresponding to the handle.</returns>
    public static implicit operator TensorSafeHandle(IntPtr handle)
        => new(handle);

    /// <summary>
    /// Implicit conversion from TensorSafeHandle to IntPtr.
    /// </summary>
    /// <param name="tensor">The TensorSafeHandle to be converted.</param>
    /// <returns>The handle of the tensor.</returns>
    public static implicit operator IntPtr(TensorSafeHandle tensor)
        => tensor.handle;
}