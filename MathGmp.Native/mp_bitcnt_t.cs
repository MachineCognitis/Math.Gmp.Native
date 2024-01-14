
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents a count of bits.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Counts of bits of a multi-precision number are represented in the C type <see cref="mp_bitcnt_t">mp_bitcnt_t</see>.
    /// Currently this is always an unsigned long, but on some systems it will be an unsigned long long in the future.
    /// </para>
    /// <para>
    /// In .NET, this is an unsigned 32-bit integer.
    /// </para>
    /// </remarks>
    /// <seealso cref="mpf_t">mpf_t</seealso>
    /// <seealso cref="mpq_t">mpq_t</seealso>
    /// <seealso cref="mpz_t">mpz_t</seealso>
    public struct mp_bitcnt_t
    {

        /// <summary>
        ///  The <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public uint Value;

        /// <summary>
        /// Creates a new <see cref="mp_bitcnt_t">mp_bitcnt_t</see>, and sets its <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the new <see cref="mp_bitcnt_t">mp_bitcnt_t</see>.</param>
        public mp_bitcnt_t(uint value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Converts a <see cref="Byte">Byte</see> value to an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="Byte">Byte</see> value.</param>
        /// <returns>An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</returns>
        public static implicit operator mp_bitcnt_t(byte value)
        {
            return new mp_bitcnt_t(value);
        }

        /// <summary>
        /// Converts a <see cref="Byte">Byte</see> value to an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="Byte">Byte</see> value.</param>
        /// <returns>An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</returns>
        public static explicit operator mp_bitcnt_t(sbyte value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_bitcnt_t data type.", value));
            return new mp_bitcnt_t((uint)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt16">UInt16</see> value to an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt16">UInt16</see> value.</param>
        /// <returns>An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</returns>
        public static implicit operator mp_bitcnt_t(ushort value)
        {
            return new mp_bitcnt_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int16">Int16</see> value to an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int16">Int16</see> value.</param>
        /// <returns>An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</returns>
        public static explicit operator mp_bitcnt_t(short value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_bitcnt_t data type.", value));
            return new mp_bitcnt_t((uint)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt32">UInt32</see> value to an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt32">UInt32</see> value.</param>
        /// <returns>An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</returns>
        public static implicit operator mp_bitcnt_t(uint value)
        {
            return new mp_bitcnt_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int32">Int32</see> value to an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int32">Int32</see> value.</param>
        /// <returns>An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</returns>
        public static explicit operator mp_bitcnt_t(int value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_bitcnt_t data type.", value));
            return new mp_bitcnt_t((uint)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt64">UInt64</see> value to an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt64">UInt64</see> value.</param>
        /// <returns>An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</returns>
        public static explicit operator mp_bitcnt_t(ulong value)
        {
            if (value > uint.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_bitcnt_t data type.", value));
            return new mp_bitcnt_t((uint)value);
        }

        /// <summary>
        /// Converts an <see cref="Int64">Int64</see> value to a <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int64">Int64</see> value.</param>
        /// <returns>An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</returns>
        public static explicit operator mp_bitcnt_t(long value)
        {
            if (value < 0 || value > uint.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_bitcnt_t data type.", value));
            return new mp_bitcnt_t((uint)value);
        }

        /// <summary>
        /// Converts an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to a <see cref="Byte">Byte</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns>A <see cref="Byte">Byte</see> value.</returns>
        public static explicit operator byte(mp_bitcnt_t value)
        {
            if (value.Value > byte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Byte data type.", value));
            return (byte)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to an <see cref="SByte">SByte</see> value.
        /// </summary>
        /// <param name="value">An <see cref="SByte">SByte</see> value.</param>
        /// <returns>An <see cref="Byte">Byte</see> value.</returns>
        public static explicit operator sbyte(mp_bitcnt_t value)
        {
            if (value.Value > sbyte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the SByte data type.", value));
            return (sbyte)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to a <see cref="UInt16">UInt16</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns>A <see cref="UInt16">UInt16</see> value.</returns>
        public static explicit operator ushort(mp_bitcnt_t value)
        {
            if (value.Value > ushort.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt16 data type.", value));
            return (ushort)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to an <see cref="Int16">Int16</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns>An <see cref="Int16">Int16</see> value.</returns>
        public static explicit operator short(mp_bitcnt_t value)
        {
            if (value.Value > short.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int16 data type.", value));
            return (short)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to a <see cref="UInt32">UInt32</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns>A <see cref="UInt32">UInt32</see> value.</returns>
        public static implicit operator uint(mp_bitcnt_t value)
        {
            return value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to an <see cref="Int32">Int32</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns>An <see cref="Int32">Int32</see> value.</returns>
        public static explicit operator int(mp_bitcnt_t value)
        {
            if (value.Value > int.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int32 data type.", value));
            return (int)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to a <see cref="UInt64">UInt64</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns>A <see cref="UInt64">UInt64</see> value.</returns>
        public static implicit operator ulong(mp_bitcnt_t value)
        {
            return value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to an <see cref="Int64">Int64</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns>An <see cref="Int64">Int64</see> value.</returns>
        public static implicit operator long(mp_bitcnt_t value)
        {
            return value.Value;
        }

        /// <summary>
        /// Gets the string representation of the <see cref="mp_bitcnt_t">mp_bitcnt_t</see>.
        /// </summary>
        /// <returns>The string representation of the <see cref="mp_bitcnt_t">mp_bitcnt_t</see>.</returns>
        public override string ToString()
        {
            return Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="mp_bitcnt_t">mp_bitcnt_t</see> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is mp_bitcnt_t))
                return false;

            return Equals((mp_bitcnt_t)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.
        /// </summary>
        /// <param name="other">A <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(mp_bitcnt_t other)
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
        /// <param name="value1">A <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <param name="value2">A <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(mp_bitcnt_t value1, mp_bitcnt_t value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <param name="value2">A <see cref="mp_bitcnt_t">mp_bitcnt_t</see> value.</param>
        /// <returns><c>True</c> if the two values are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(mp_bitcnt_t value1, mp_bitcnt_t value2)
        {
            return !value1.Equals(value2);
        }

    }

}