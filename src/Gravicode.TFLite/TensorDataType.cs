namespace Gravicode.TFLite;

/// <summary>
/// Enum representing the types supported by tensors in RTLite.
/// </summary>
public enum TensorDataType
{
    /// <summary>
    /// Type not defined.
    /// </summary>
    NoType = 0,

    /// <summary>
    /// Floating-point type with regular size of 4 bytes.
    /// </summary>
    Float32 = 1,

    /// <summary>
    /// Integer type with regular size of 4 bytes.
    /// </summary>
    Int32 = 2,

    /// <summary>
    /// Unsigned integer type with a size of 1 byte.
    /// </summary>
    UInt8 = 3,

    /// <summary>
    /// Integer type with regular size of 8 bytes.
    /// </summary>
    Int64 = 4,

    /// <summary>
    /// String type.
    /// </summary>
    String = 5,

    /// <summary>
    /// Boolean type.
    /// </summary>
    Bool = 6,

    /// <summary>
    /// Integer type with regular size of 2 bytes.
    /// </summary>
    Int16 = 7,

    /// <summary>
    /// Single-precision complex type.
    /// </summary>
    Complex64 = 8,

    /// <summary>
    /// Integer type with regular size of 1 byte.
    /// </summary>
    Int8 = 9,

    /// <summary>
    /// Floating-point type with regular size of 2 bytes.
    /// </summary>
    Float16 = 10,

    /// <summary>
    /// Floating-point type with regular size of 4 bytes.
    /// </summary>
    Float64 = 11,

    /// <summary>
    /// Double-precision complex type.
    /// </summary>
    Complex128 = 12,

    /// <summary>
    /// Unsigned integer type with regular size of 8 bytes.
    /// </summary>
    UInt64 = 13,

    /// <summary>
    /// Resource type.
    /// </summary>
    Resource = 14,

    /// <summary>
    /// Identifies variant data types.
    /// </summary>
    Variant = 15,

    /// <summary>
    /// Unsigned integer type with regular size of 4 bytes.
    /// </summary>
    UInt32 = 16,
}