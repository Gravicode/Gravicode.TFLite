using System;

namespace Gravicode.TFLite;

/// <summary>
/// Represents a RTLite exception
/// </summary>
public class RTLiteException : Exception
{
    public RuntimeStatus? Status { get; }

    internal RTLiteException(string message)
        : base(message)
    {
    }

    internal RTLiteException(string message, RuntimeStatus status)
        : base($"{message}: {status}")
    {
    }
}
