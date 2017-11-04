
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Math.Gmp.Native
{

    /// <summary>
    /// Represents a multiple precision integer.
    /// </summary>
    /// <remarks></remarks>
    /// <seealso cref="mp_limb_t"/>
    /// <seealso cref="mpf_t"/>
    /// <seealso cref="mpq_t"/>
    public class mpz_t : mp_base
    {

        /// <summary>
        /// Creates a new multiple precision integer.
        /// </summary>
        public mpz_t()
        {
            size_t length = /*sizeof(int) + sizeof(int)*/ 8 + (size_t)IntPtr.Size;
            _pointer = gmp_lib.allocate(length).ToIntPtr();
            gmp_lib.ZeroMemory(_pointer, (int)length);
        }

        internal mpz_t(IntPtr pointer)
        {
            _pointer = pointer;
        }

        /// <summary>
        /// The number of limbs currently allocated at <see cref="mp_base._mp_d"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="mpz_t._mp_alloc"/> is the number of limbs currently allocated at <see cref="mp_base._mp_d"/>,
        /// and naturally <see cref="mpz_t._mp_alloc"/> >= ABS(<see cref="mpz_t._mp_size"/>).
        /// When an mpz routine is about to (or might be about to) increase <see cref="mpz_t._mp_size"/>, it checks
        /// <see cref="mpz_t._mp_alloc"/> to see whether there’s enough space, and reallocates if not.
        /// </para>
        /// </remarks>
        public int _mp_alloc
        {
            get
            {
                return Marshal.ReadInt32(_pointer, 0);
            }
        }

        /// <summary>
        /// The number of limbs, or the negative of that when representing a negative integer.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The number of limbs, or the negative of that when representing a negative integer.
        /// Zero is represented by <see cref="mp_base._mp_size"/> set to zero, in which case
        /// the <see cref="mp_base._mp_d"/> data is unused.
        /// </para>
        /// </remarks>
        public override mp_size_t _mp_size
        {
            get
            {
                return Marshal.ReadInt32(_pointer, sizeof(int));
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal override IntPtr _mp_d_intptr
        {
            get
            {
                return Marshal.ReadIntPtr(_pointer, sizeof(int) + sizeof(int));
            }
            set
            {
                Marshal.WriteIntPtr(_pointer, sizeof(int) + sizeof(int), value);
            }
        }

        /// <summary>
        /// Gets the unmanaged memory pointer of the multiple precision integer.
        /// </summary>
        /// <returns>The unmanaged memory pointer of the multiple precision integer.</returns>
        public IntPtr ToIntPtr()
        {
            return _pointer;
        }

        /// <summary>
        /// Converts a <see cref="string"/> value to an <see cref="mpz_t"/> value.
        /// </summary>
        /// <param name="value">A <see cref="string"/> value.</param>
        /// <returns>An <see cref="mpz_t"/> value.</returns>
        /// <remarks>
        /// <para>
        /// The leading characters are used: <c>0x</c> and <c>0X</c> for hexadecimal,
        /// <c>0b</c> and <c>0B</c> for binary, <c>0</c> for octal, or decimal otherwise.
        /// </para>
        /// </remarks>
        public static implicit operator mpz_t(string value)
        {
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);
            char_ptr s = new char_ptr(value);
            gmp_lib.mpz_set_str(x, s, 0);
            gmp_lib.free(s);
            return x;
        }

        /// <summary>
        /// Return the string representation of the integer.
        /// </summary>
        /// <returns>The string representation of the integer.</returns>
        public override string ToString()
        {
            char_ptr s_ptr = gmp_lib.mpz_get_str(char_ptr.Zero, 10, this);
            string s = s_ptr.ToString();
            gmp_lib.free(s_ptr);
            return s;
        }

    }

}