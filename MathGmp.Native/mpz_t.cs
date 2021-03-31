
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents a multiple precision integer.
    /// </summary>
    /// <remarks></remarks>
    /// <seealso cref="mp_limb_t">mp_limb_t</seealso>
    /// <seealso cref="mpf_t">mpf_t</seealso>
    /// <seealso cref="mpq_t">mpq_t</seealso>
    public class mpz_t : mp_base, IDisposable
    {

        /// <summary>
        /// Creates a new multiple precision integer.
        /// </summary>
        public mpz_t()
        {
        }

        internal mpz_t(IntPtr pointer)
        {
            Pointer = pointer;
        }

        internal void Initializing()
        {
            size_t length = /*sizeof(int) + sizeof(int)*/ 8 + (size_t)IntPtr.Size;
            Pointer = gmp_lib.allocate(length).ToIntPtr();
            //gmp_lib.ZeroMemory(Pointer, (int)length);
        }

        internal void Clear()
        {
            if (Pointer != IntPtr.Zero) gmp_lib.free(Pointer);
            Pointer = IntPtr.Zero;
        }
        
        public void Dispose()
        {
            Clear();
        }

        /// <summary>
        /// The number of limbs currently allocated at <see cref="mp_base._mp_d">mp_base._mp_d</see>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="mpz_t._mp_alloc">mpz_t._mp_alloc</see> is the number of limbs currently allocated at <see cref="mp_base._mp_d">mp_base._mp_d</see>,
        /// and naturally <see cref="mpz_t._mp_alloc">mpz_t._mp_alloc</see> >= ABS(<see cref="mpz_t._mp_size">mpz_t._mp_size</see>).
        /// When an mpz routine is about to (or might be about to) increase <see cref="mpz_t._mp_size">mpz_t._mp_size</see>, it checks
        /// <see cref="mpz_t._mp_alloc">mpz_t._mp_alloc</see> to see whether there’s enough space, and reallocates if not.
        /// </para>
        /// </remarks>
        public int _mp_alloc
        {
            get
            {
                return Marshal.ReadInt32(Pointer, 0);
            }
        }

        /// <summary>
        /// The number of limbs, or the negative of that when representing a negative integer.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The number of limbs, or the negative of that when representing a negative integer.
        /// Zero is represented by <see cref="mp_base._mp_size">mp_base._mp_size</see> set to zero, in which case
        /// the <see cref="mp_base._mp_d">mp_base._mp_d</see> data is unused.
        /// </para>
        /// </remarks>
        public override mp_size_t _mp_size
        {
            get
            {
                return Marshal.ReadInt32(Pointer, sizeof(int));
            }
            set
            {
                Marshal.WriteInt32(Pointer, sizeof(int), value);
            }
        }

        /// <summary>
        /// Gets or sets the pointer to the array of limbs of the integer.
        /// </summary>
        //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override IntPtr _mp_d_intptr
        {
            get
            {
                return Marshal.ReadIntPtr(Pointer, sizeof(int) + sizeof(int));
            }
            set
            {
                Marshal.WriteIntPtr(Pointer, sizeof(int) + sizeof(int), value);
            }
        }

        /// <summary>
        /// Gets the unmanaged memory pointer of the multiple precision integer.
        /// </summary>
        /// <returns>The unmanaged memory pointer of the multiple precision integer.</returns>
        public IntPtr ToIntPtr()
        {
            return Pointer;
        }

        /// <summary>
        /// Converts a <see cref="string">string</see> value to an <see cref="mpz_t">mpz_t</see> value.
        /// </summary>
        /// <param name="value">A <see cref="string">string</see> value.</param>
        /// <returns>An <see cref="mpz_t">mpz_t</see> value.</returns>
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
            if (Pointer == IntPtr.Zero) return "uninitialized";
            char_ptr s_ptr = gmp_lib.mpz_get_str(char_ptr.Zero, 10, this);
            string s = s_ptr.ToString();
            gmp_lib.free(s_ptr);
            return s;
        }

    }

}
