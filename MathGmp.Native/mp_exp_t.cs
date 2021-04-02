
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents the exponent of a floating-point number.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The floating point functions accept and return exponents in the C type <see cref="mp_exp_t">mp_exp_t</see>.
    /// Currently this is usually a long, but on some systems it’s an int for efficiency.
    /// </para>
    /// <para>
    /// In .Net, this is a 32-bit integer. 
    /// </para>
    /// </remarks>
    public struct mp_exp_t
    {

        /// <summary>
        ///  The <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public int Value;

        /// <summary>
        /// Creates a new <see cref="mp_exp_t">mp_exp_t</see>, and sets its <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the new <see cref="mp_exp_t">mp_exp_t</see>.</param>
        public mp_exp_t(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Converts a <see cref="Byte">Byte</see> value to an <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="Byte">Byte</see> value.</param>
        /// <returns>An <see cref="mp_exp_t">mp_exp_t</see> value.</returns>
        public static implicit operator mp_exp_t(byte value)
        {
            return new mp_exp_t(value);
        }

        /// <summary>
        /// Converts a <see cref="Byte">Byte</see> value to an <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="Byte">Byte</see> value.</param>
        /// <returns>An <see cref="mp_exp_t">mp_exp_t</see> value.</returns>
        public static implicit operator mp_exp_t(sbyte value)
        {
            return new mp_exp_t(value);
        }

        /// <summary>
        /// Converts a <see cref="UInt16">UInt16</see> value to an <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt16">UInt16</see> value.</param>
        /// <returns>An <see cref="mp_exp_t">mp_exp_t</see> value.</returns>
        public static implicit operator mp_exp_t(ushort value)
        {
            return new mp_exp_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int16">Int16</see> value to an <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int16">Int16</see> value.</param>
        /// <returns>An <see cref="mp_exp_t">mp_exp_t</see> value.</returns>
        public static implicit operator mp_exp_t(short value)
        {
            return new mp_exp_t(value);
        }

        /// <summary>
        /// Converts a <see cref="UInt32">UInt32</see> value to an <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt32">UInt32</see> value.</param>
        /// <returns>An <see cref="mp_exp_t">mp_exp_t</see> value.</returns>
        public static explicit operator mp_exp_t(uint value)
        {
            if (value > int.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_exp_t data type.", value));
            return new mp_exp_t((int)value);
        }

        /// <summary>
        /// Converts an <see cref="Int32">Int32</see> value to an <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int32">Int32</see> value.</param>
        /// <returns>An <see cref="mp_exp_t">mp_exp_t</see> value.</returns>
        public static implicit operator mp_exp_t(int value)
        {
            return new mp_exp_t(value);
        }

        /// <summary>
        /// Converts a <see cref="UInt64">UInt64</see> value to an <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt64">UInt64</see> value.</param>
        /// <returns>An <see cref="mp_exp_t">mp_exp_t</see> value.</returns>
        public static explicit operator mp_exp_t(ulong value)
        {
            if (value > int.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_exp_t data type.", value));
            return new mp_exp_t((int)value);
        }

        /// <summary>
        /// Converts an <see cref="Int64">Int64</see> value to a <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="value">An <see cref="Int64">Int64</see> value.</param>
        /// <returns>An <see cref="mp_exp_t">mp_exp_t</see> value.</returns>
        public static explicit operator mp_exp_t(long value)
        {
            if (value < uint.MinValue || value > uint.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_exp_t data type.", value));
            return new mp_exp_t((int)value);
        }

        /// <summary>
        /// Converts an <see cref="mp_exp_t">mp_exp_t</see> value to a <see cref="Byte">Byte</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns>A <see cref="Byte">Byte</see> value.</returns>
        public static explicit operator byte(mp_exp_t value)
        {
            if (value.Value < 0 || value.Value > byte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Byte data type.", value));
            return (byte)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_exp_t">mp_exp_t</see> value to an <see cref="SByte">SByte</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns>An <see cref="SByte">SByte</see> value.</returns>
        public static explicit operator sbyte(mp_exp_t value)
        {
            if (value.Value < sbyte.MinValue || value.Value > sbyte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the SByte data type.", value));
            return (sbyte)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_exp_t">mp_exp_t</see> value to a <see cref="UInt16">UInt16</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns>A <see cref="UInt16">UInt16</see> value.</returns>
        public static explicit operator ushort(mp_exp_t value)
        {
            if (value.Value < 0 || value.Value > ushort.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt16 data type.", value));
            return (ushort)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_exp_t">mp_exp_t</see> value to an <see cref="Int16">Int16</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns>An <see cref="Int16">Int16</see> value.</returns>
        public static explicit operator short(mp_exp_t value)
        {
            if (value.Value < short.MinValue || value.Value > short.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int16 data type.", value));
            return (short)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_exp_t">mp_exp_t</see> value to a <see cref="UInt32">UInt32</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns>A <see cref="UInt32">UInt32</see> value.</returns>
        public static explicit operator uint(mp_exp_t value)
        {
            if (value.Value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt32 data type.", value));
            return (uint)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_exp_t">mp_exp_t</see> value to an <see cref="Int32">Int32</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns>An <see cref="Int32">Int32</see> value.</returns>
        public static implicit operator int(mp_exp_t value)
        {
            return value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_exp_t">mp_exp_t</see> value to a <see cref="UInt64">UInt64</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns>A <see cref="UInt64">UInt64</see> value.</returns>
        public static explicit operator ulong(mp_exp_t value)
        {
            if (value.Value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt64 data type.", value));
            return (ulong)value.Value;
        }

        /// <summary>
        /// Converts an <see cref="mp_exp_t">mp_exp_t</see> value to an <see cref="Int64">Int64</see> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns>An <see cref="Int64">Int64</see> value.</returns>
        public static implicit operator long(mp_exp_t value)
        {
            return value.Value;
        }

        /// <summary>
        /// Gets the string representation of the <see cref="mp_exp_t">mp_exp_t</see>.
        /// </summary>
        /// <returns>The string representation of the <see cref="mp_exp_t">mp_exp_t</see>.</returns>
        public override string ToString()
        {
            return Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="mp_exp_t">mp_exp_t</see> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is mp_exp_t))
                return false;

            return Equals((mp_exp_t)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="mp_exp_t">mp_exp_t</see> value.
        /// </summary>
        /// <param name="other">A <see cref="mp_exp_t">mp_exp_t</see> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(mp_exp_t other)
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
        /// <param name="value1">A <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <param name="value2">A <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(mp_exp_t value1, mp_exp_t value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <param name="value2">A <see cref="mp_exp_t">mp_exp_t</see> value.</param>
        /// <returns><c>True</c> if the two values are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(mp_exp_t value1, mp_exp_t value2)
        {
            return !value1.Equals(value2);
        }

    }

}