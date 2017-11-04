
using System;
using System.Runtime.InteropServices;

namespace Math.Gmp.Native
{

    /// <summary>
    /// Represents a count of limbs.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Counts of limbs of a multi-precision number represented in the C type <see cref="mp_size_t"/>.
    /// Currently this is normally a long, but on some systems it’s an int for efficiency,
    /// and on some systems it will be long long in the future.
    /// </para>
    /// <para>
    /// In .Net, this is a 32-bit integer. 
    /// </para>
    /// </remarks>
    /// <seealso cref="mp_limb_t"/>
    /// <seealso cref="mpf_t"/>
    /// <seealso cref="mpq_t"/>
    /// <seealso cref="mpz_t"/>
    public struct mp_size_t
    {

        internal int _value;

        /// <summary>
        /// Creates a new <see cref="mp_size_t"/>, and sets its <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the new <see cref="mp_size_t"/>.</param>
        public mp_size_t(int value)
        {
            _value = value;
        }

        /// <summary>
        /// Converts a <see cref="Byte"/> value to an <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="Byte"/> value.</param>
        /// <returns>An <see cref="mp_size_t"/> value.</returns>
        public static implicit operator mp_size_t(byte value)
        {
            return new mp_size_t(value);
        }

        /// <summary>
        /// Converts a <see cref="Byte"/> value to an <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="Byte"/> value.</param>
        /// <returns>An <see cref="mp_size_t"/> value.</returns>
        public static implicit operator mp_size_t(sbyte value)
        {
            return new mp_size_t(value);
        }

        /// <summary>
        /// Converts a <see cref="UInt16"/> value to an <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt16"/> value.</param>
        /// <returns>An <see cref="mp_size_t"/> value.</returns>
        public static implicit operator mp_size_t(ushort value)
        {
            return new mp_size_t(value);
        }

        /// <summary>
        /// Converts an <see cref="Int16"/> value to an <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="value">An <see cref="Int16"/> value.</param>
        /// <returns>An <see cref="mp_size_t"/> value.</returns>
        public static implicit operator mp_size_t(short value)
        {
            return new mp_size_t(value);
        }

        /// <summary>
        /// Converts a <see cref="UInt32"/> value to an <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt32"/> value.</param>
        /// <returns>An <see cref="mp_size_t"/> value.</returns>
        public static explicit operator mp_size_t(uint value)
        {
            if (value > int.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_size_t data type.", value));
            return new mp_size_t((int)value);
        }

        /// <summary>
        /// Converts an <see cref="Int32"/> value to an <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="value">An <see cref="Int32"/> value.</param>
        /// <returns>An <see cref="mp_size_t"/> value.</returns>
        public static implicit operator mp_size_t(int value)
        {
            return new mp_size_t(value);
        }

        /// <summary>
        /// Converts a <see cref="UInt64"/> value to an <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="UInt64"/> value.</param>
        /// <returns>An <see cref="mp_size_t"/> value.</returns>
        public static explicit operator mp_size_t(ulong value)
        {
            if (value > int.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_size_t data type.", value));
            return new mp_size_t((int)value);
        }

        /// <summary>
        /// Converts an <see cref="Int64"/> value to a <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="value">An <see cref="Int64"/> value.</param>
        /// <returns>An <see cref="mp_size_t"/> value.</returns>
        public static explicit operator mp_size_t(long value)
        {
            if (value < uint.MinValue || value > uint.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the mp_size_t data type.", value));
            return new mp_size_t((int)value);
        }

        /// <summary>
        /// Converts an <see cref="mp_size_t"/> value to a <see cref="Byte"/> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_size_t"/> value.</param>
        /// <returns>A <see cref="Byte"/> value.</returns>
        public static explicit operator byte(mp_size_t value)
        {
            if (value._value > byte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Byte data type.", value));
            return (byte)value._value;
        }

        /// <summary>
        /// Converts an <see cref="mp_size_t"/> value to an <see cref="SByte"/> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_size_t"/> value.</param>
        /// <returns>An <see cref="SByte"/> value.</returns>
        public static explicit operator sbyte(mp_size_t value)
        {
            if (value._value < sbyte.MinValue || value._value > sbyte.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the SByte data type.", value));
            return (sbyte)value._value;
        }

        /// <summary>
        /// Converts an <see cref="mp_size_t"/> value to a <see cref="UInt16"/> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_size_t"/> value.</param>
        /// <returns>A <see cref="UInt16"/> value.</returns>
        public static explicit operator ushort(mp_size_t value)
        {
            if (value._value > ushort.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt16 data type.", value));
            return (ushort)value._value;
        }

        /// <summary>
        /// Converts an <see cref="mp_size_t"/> value to an <see cref="Int16"/> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_size_t"/> value.</param>
        /// <returns>An <see cref="Int16"/> value.</returns>
        public static explicit operator short(mp_size_t value)
        {
            if (value._value < short.MinValue || value._value > short.MaxValue) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the Int16 data type.", value));
            return (short)value._value;
        }

        /// <summary>
        /// Converts an <see cref="mp_size_t"/> value to a <see cref="UInt32"/> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_size_t"/> value.</param>
        /// <returns>A <see cref="UInt32"/> value.</returns>
        public static explicit operator uint(mp_size_t value)
        {
            if (value._value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt32 data type.", value));
            return (uint)value._value;
        }

        /// <summary>
        /// Converts an <see cref="mp_size_t"/> value to an <see cref="Int32"/> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_size_t"/> value.</param>
        /// <returns>An <see cref="Int32"/> value.</returns>
        public static implicit operator int(mp_size_t value)
        {
            return value._value;
        }

        /// <summary>
        /// Converts an <see cref="mp_size_t"/> value to a <see cref="UInt64"/> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_size_t"/> value.</param>
        /// <returns>A <see cref="UInt64"/> value.</returns>
        public static explicit operator ulong(mp_size_t value)
        {
            if (value._value < 0) throw new System.OverflowException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "'{0}' is out of range of the UInt64 data type.", value));
            return (ulong)value._value;
        }

        /// <summary>
        /// Converts an <see cref="mp_size_t"/> value to an <see cref="Int64"/> value.
        /// </summary>
        /// <param name="value">An <see cref="mp_size_t"/> value.</param>
        /// <returns>An <see cref="Int64"/> value.</returns>
        public static implicit operator long(mp_size_t value)
        {
            return value._value;
        }

        /// <summary>
        /// Gets the string representation of the <see cref="mp_size_t"/>.
        /// </summary>
        /// <returns>The string representation of the <see cref="mp_size_t"/>.</returns>
        public override string ToString()
        {
            return _value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="mp_size_t"/> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is mp_size_t))
                return false;

            return Equals((mp_size_t)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="mp_size_t"/> value.
        /// </summary>
        /// <param name="other">A <see cref="mp_size_t"/> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(mp_size_t other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are equal.
        /// </summary>
        /// <param name="value1">A <see cref="mp_size_t"/> value.</param>
        /// <param name="value2">A <see cref="mp_size_t"/> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(mp_size_t value1, mp_size_t value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="mp_size_t"/> value.</param>
        /// <param name="value2">A <see cref="mp_size_t"/> value.</param>
        /// <returns><c>True</c> if the two values are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(mp_size_t value1, mp_size_t value2)
        {
            return !value1.Equals(value2);
        }

    }

}