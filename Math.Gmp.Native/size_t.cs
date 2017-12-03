
using System;
using System.Runtime.InteropServices;

namespace Math.Gmp.Native
{

    /// <summary>
    /// Represents a count of characters or bytes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In .NET, this is an unsigned 64-bit integer.
    /// </para>
    /// </remarks>
    public struct size_t
    {

        /// <summary>
        ///  The <see cref="size_t"/> value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public ulong Value;

        /// <summary>
        /// Creates a new <see cref="size_t"/>, and sets its <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the new <see cref="size_t"/>.</param>
        public size_t(ulong value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Converts a <see cref="Byte"/> value to a <see cref="size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="Byte"/> value.</param>
        /// <returns>A <see cref="size_t"/> value.</returns>
        public static implicit operator size_t(byte value)
        {
            return new size_t(value);
        }

        /// <summary>
        /// Converts a <see cref="SByte"/> value to a <see cref="size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="SByte"/> value.</param>
        /// <returns>A <see cref="size_t"/> value.</returns>
        public static explicit operator size_t(sbyte value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the size_t data type.", value));
            return new size_t((uint)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt16"/> value to a <see cref="size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt16"/> value.</param>
        /// <returns>A <see cref="size_t"/> value.</returns>
        public static implicit operator size_t(ushort value)
        {
            return new size_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int16"/> value to a <see cref="size_t"/> value.
        /// </summary>
        /// <param name="value">An <see cref="Int16"/> value.</param>
        /// <returns>A <see cref="size_t"/> value.</returns>
        public static explicit operator size_t(short value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the size_t data type.", value));
            return new size_t((uint)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt32"/> value to a <see cref="size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt32"/> value.</param>
        /// <returns>A <see cref="size_t"/> value.</returns>
        public static implicit operator size_t(uint value)
        {
            return new size_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int32"/> value to a <see cref="size_t"/> value.
        /// </summary>
        /// <param name="value">An <see cref="Int32"/> value.</param>
        /// <returns>A <see cref="size_t"/> value.</returns>
        public static explicit operator size_t(int value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the size_t data type.", value));
            return new size_t((ulong)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt64"/> value to a <see cref="size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt64"/> value.</param>
        /// <returns>A <see cref="size_t"/> value.</returns>
        public static implicit operator size_t(ulong value)
        {
            return new size_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int64"/> value to a <see cref="size_t"/> value.
        /// </summary>
        /// <param name="value">An <see cref="Int64"/> value.</param>
        /// <returns>A <see cref="size_t"/> value.</returns>
        public static explicit operator size_t(long value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the size_t data type.", value));
            return new size_t((ulong)value);
        }

        /// <summary>
        /// Converts a <see cref="size_t"/> value to a <see cref="Byte"/> value.
        /// </summary>
        /// <param name="value">An <see cref="size_t"/> value.</param>
        /// <returns>A <see cref="Byte"/> value.</returns>
        public static explicit operator byte(size_t value)
        {
            if (value.Value > byte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Byte data type.", value));
            return (byte)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="size_t"/> value to an <see cref="SByte"/> value.
        /// </summary>
        /// <param name="value">An <see cref="size_t"/> value.</param>
        /// <returns>An <see cref="SByte"/> value.</returns>
        public static explicit operator sbyte(size_t value)
        {
            if (value.Value > (ulong)sbyte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the SByte data type.", value));
            return (sbyte)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="size_t"/> value to a <see cref="UInt16"/> value.
        /// </summary>
        /// <param name="value">An <see cref="size_t"/> value.</param>
        /// <returns>A <see cref="UInt16"/> value.</returns>
        public static explicit operator ushort(size_t value)
        {
            if (value.Value > ushort.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt16 data type.", value));
            return (ushort)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="size_t"/> value to an <see cref="Int16"/> value.
        /// </summary>
        /// <param name="value">An <see cref="size_t"/> value.</param>
        /// <returns>An <see cref="Int16"/> value.</returns>
        public static explicit operator short(size_t value)
        {
            if (value.Value > (ulong)short.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int16 data type.", value));
            return (short)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="size_t"/> value to a <see cref="UInt32"/> value.
        /// </summary>
        /// <param name="value">An <see cref="size_t"/> value.</param>
        /// <returns>A <see cref="UInt32"/> value.</returns>
        public static explicit operator uint(size_t value)
        {
            if (value.Value > uint.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt32 data type.", value));
            return (uint)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="size_t"/> value to an <see cref="Int32"/> value.
        /// </summary>
        /// <param name="value">An <see cref="size_t"/> value.</param>
        /// <returns>An <see cref="Int32"/> value.</returns>
        public static explicit operator int(size_t value)
        {
            if (value.Value > int.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int32 data type.", value));
            return (int)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="size_t"/> value to a <see cref="UInt64"/> value.
        /// </summary>
        /// <param name="value">An <see cref="size_t"/> value.</param>
        /// <returns>A <see cref="UInt64"/> value.</returns>
        public static implicit operator ulong(size_t value)
        {
            return value.Value;
        }

        /// <summary>
        /// Converts a <see cref="size_t"/> value to an <see cref="Int64"/> value.
        /// </summary>
        /// <param name="value">An <see cref="size_t"/> value.</param>
        /// <returns>An <see cref="Int64"/> value.</returns>
        public static explicit operator long(size_t value)
        {
            if (value.Value > long.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int64 data type.", value));
            return (long)value.Value;
        }

        /// <summary>
        /// Gets the string representation of the <see cref="size_t"/>.
        /// </summary>
        /// <returns>The string representation of the <see cref="size_t"/>.</returns>
        public override string ToString()
        {
            return Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="size_t"/> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is size_t))
                return false;

            return Equals((size_t)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="size_t"/> value.
        /// </summary>
        /// <param name="other">A <see cref="size_t"/> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(size_t other)
        {
            return Value == other.Value;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are equal.
        /// </summary>
        /// <param name="value1">A <see cref="size_t"/> value.</param>
        /// <param name="value2">A <see cref="size_t"/> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(size_t value1, size_t value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="size_t"/> value.</param>
        /// <param name="value2">A <see cref="size_t"/> value.</param>
        /// <returns><c>True</c> if the two values are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(size_t value1, size_t value2)
        {
            return !value1.Equals(value2);
        }

    }

}