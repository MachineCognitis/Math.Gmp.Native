
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents a part of a multiple precision number.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A limb means the part of a multi-precision number that fits in a single machine word.
    /// (We chose this word because a limb of the human body is analogous to a digit, only larger,
    /// and containing several digits.) Normally a limb is 32 or 64 bits.
    /// </para>
    /// </remarks>
    /// <seealso cref="mpf_t">mpf_t</seealso>
    /// <seealso cref="mpq_t">mpq_t</seealso>
    /// <seealso cref="mpz_t">mpz_t</seealso>
    public struct mp_limb_t
    {

        /// <summary>
        ///  The <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public ulong Value;

        /// <summary>
        /// Creates a new <see cref="mp_limb_t">mp_limb_t</see>, and sets its <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the new <see cref="mp_limb_t">mp_limb_t</see>.</param>
        public mp_limb_t(ulong value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Converts a <see cref="Byte">Byte</see> value to an <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="Byte">Byte</see> value.</param>
        /// <returns>An <see cref="mp_limb_t">mp_limb_t</see> value.</returns>
        public static implicit operator mp_limb_t(byte value)
        {
            return new mp_limb_t(value);
        }

        /// <summary>
        /// Converts a <see cref="SByte">SByte</see> value to an <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="SByte">SByte</see> value.</param>
        /// <returns>An <see cref="mp_limb_t">mp_limb_t</see> value.</returns>
        public static explicit operator mp_limb_t(sbyte value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_limb_t data type.", value));
            return new mp_limb_t((ulong)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt16">UInt16</see> value to an <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt16">UInt16</see> value.</param>
        /// <returns>An <see cref="mp_limb_t">mp_limb_t</see> value.</returns>
        public static implicit operator mp_limb_t(ushort value)
        {
            return new mp_limb_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int16">Int16</see> value to an <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int16">Int16</see> value.</param>
        /// <returns>An <see cref="mp_limb_t">mp_limb_t</see> value.</returns>
        public static explicit operator mp_limb_t(short value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_limb_t data type.", value));
            return new mp_limb_t((ulong)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt32">UInt32</see> value to an <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt32">UInt32</see> value.</param>
        /// <returns>An <see cref="mp_limb_t">mp_limb_t</see> value.</returns>
        public static implicit operator mp_limb_t(uint value)
        {
            return new mp_limb_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int32">Int32</see> value to an <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int32">Int32</see> value.</param>
        /// <returns>An <see cref="mp_limb_t">mp_limb_t</see> value.</returns>
        public static explicit operator mp_limb_t(int value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_limb_t data type.", value));
            return new mp_limb_t((ulong)(uint)value);
        }

        /// <summary>
        /// Converts a <see cref="UInt64">UInt64</see> value to an <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt64">UInt64</see> value.</param>
        /// <returns>An <see cref="mp_limb_t">mp_limb_t</see> value.</returns>
        public static implicit operator mp_limb_t(ulong value)
        {
            return new mp_limb_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int64">Int64</see> value to an <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int64">Int64</see> value.</param>
        /// <returns>An <see cref="mp_limb_t">mp_limb_t</see> value.</returns>
        public static explicit operator mp_limb_t(long value)
        {
            if (value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_limb_t data type.", value));
            return new mp_limb_t((ulong)value);
        }

        /// <summary>
        /// Converts a <see cref="mp_limb_t">mp_limb_t</see> value to a <see cref="Byte">Byte</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns>A <see cref="Byte">Byte</see> value.</returns>
        public static explicit operator byte(mp_limb_t value)
        {
            if (value.Value > byte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Byte data type.", value));
            return (byte)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="mp_limb_t">mp_limb_t</see> value to an <see cref="SByte">SByte</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns>An <see cref="SByte">SByte</see> value.</returns>
        public static explicit operator sbyte(mp_limb_t value)
        {
            if (value.Value > (ulong)sbyte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the SByte data type.", value));
            return (sbyte)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="mp_limb_t">mp_limb_t</see> value to a <see cref="UInt16">UInt16</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns>A <see cref="UInt16">UInt16</see> value.</returns>
        public static explicit operator ushort(mp_limb_t value)
        {
            if (value.Value > ushort.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt16 data type.", value));
            return (ushort)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="mp_limb_t">mp_limb_t</see> value to an <see cref="Int16">Int16</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns>An <see cref="Int16">Int16</see> value.</returns>
        public static explicit operator short(mp_limb_t value)
        {
            if (value.Value > (ulong)short.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int16 data type.", value));
            return (short)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="mp_limb_t">mp_limb_t</see> value to a <see cref="UInt32">UInt32</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns>A <see cref="UInt32">UInt32</see> value.</returns>
        public static explicit operator uint(mp_limb_t value)
        {
            if (value.Value > uint.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt32 data type.", value));
            return (uint)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="mp_limb_t">mp_limb_t</see> value to an <see cref="Int32">Int32</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns>An <see cref="Int32">Int32</see> value.</returns>
        public static explicit operator int(mp_limb_t value)
        {
            if (value.Value > int.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int32 data type.", value));
            return (int)value.Value;
        }

        /// <summary>
        /// Converts a <see cref="mp_limb_t">mp_limb_t</see> value to a <see cref="UInt64">UInt64</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns>A <see cref="UInt64">UInt64</see> value.</returns>
        public static implicit operator ulong(mp_limb_t value)
        {
            return value.Value;
        }

        /// <summary>
        /// Converts a <see cref="mp_limb_t">mp_limb_t</see> value to an <see cref="Int64">Int64</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns>An <see cref="Int64">Int64</see> value.</returns>
        public static explicit operator long(mp_limb_t value)
        {
            if (value.Value > long.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int64 data type.", value));
            return (long)value.Value;
        }

        /// <summary>
        /// Gets the string representation of the <see cref="mp_limb_t">mp_limb_t</see>.
        /// </summary>
        /// <returns>The string representation of the <see cref="mp_limb_t">mp_limb_t</see>.</returns>
        public override string ToString()
        {
            return "0x" + Value.ToString(gmp_lib.mp_bytes_per_limb == 4 ? "x8" : "x16", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="mp_limb_t">mp_limb_t</see> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is mp_limb_t))
                return false;

            return Equals((mp_limb_t)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="mp_limb_t">mp_limb_t</see> value.
        /// </summary>
        /// <param name="other">A <see cref="mp_limb_t">mp_limb_t</see> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(mp_limb_t other)
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
        /// <param name="value1">A <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <param name="value2">A <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(mp_limb_t value1, mp_limb_t value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <param name="value2">A <see cref="mp_limb_t">mp_limb_t</see> value.</param>
        /// <returns><c>True</c> if the two values are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(mp_limb_t value1, mp_limb_t value2)
        {
            return !value1.Equals(value2);
        }

    }

}