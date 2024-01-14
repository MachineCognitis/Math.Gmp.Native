
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents a pointer to an array of <see cref="mp_limb_t">mp_limb_t</see> values in unmanaged memory,
    /// </summary>
    /// <remarks>
    /// <para>
    /// </para>
    /// </remarks>
    public class mp_ptr : IEnumerable<mp_limb_t>
    {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal mp_base mp;

        /// <summary>
        /// Creates a new array of <paramref name="size"/> limbs in unmanaged memory.
        /// </summary>
        /// <param name="size">The number of limbs.</param>
        /// <remarks>
        /// <para>
        /// When done with the array, you must release the unmanaged memory by calling <see cref="gmp_lib.free(mp_ptr[])">free</see>. 
        /// </para>
        /// </remarks>
        public mp_ptr(mp_size_t size) : this(new uint[size * gmp_lib.mp_uint_per_limb])
        {
        }

        /// <summary>
        /// Creates a new array of limbs initialized with <paramref name="values"/> in unmanaged memory.
        /// </summary>
        /// <param name="values">The values of the limbs.</param>
        /// <remarks>
        /// <para>
        /// If there is not enough bytes to fill out the most significant limb, it is padded with zeroes.
        /// </para>
        /// <para>
        /// When done with the array, you must release the unmanaged memory by calling <see cref="gmp_lib.free(mp_ptr[])">free</see>. 
        /// </para>
        /// </remarks>
        public mp_ptr(byte[] values)
        {
            if (values == null) throw new ArgumentNullException("values");
            mp = new mp_base();
            if (values.GetLength(0) == 0)
            {
                mp._size = 0;
                mp.Pointer = IntPtr.Zero;
            }
            else
            {
                mp._size = (values.Length + IntPtr.Size - 1) / IntPtr.Size;
                mp.Pointer = gmp_lib.allocate((size_t)(mp._size * IntPtr.Size)).ToIntPtr();
                Marshal.Copy(new Int32[] { 0, 0 }, 0, (IntPtr)(mp.Pointer.ToInt64() + IntPtr.Size * (mp._size - 1)), IntPtr.Size >> 2);
                Marshal.Copy(values, 0, mp.Pointer, values.Length);
            }
        }

        /// <summary>
        /// Creates a new array of limbs initialized with <paramref name="values"/> in unmanaged memory.
        /// </summary>
        /// <param name="values">The values of the limbs.</param>
        /// <remarks>
        /// <para>
        /// If there is not enough 16-bit words to fill out the most significant limb, it is padded with zeroes.
        /// </para>
        /// <para>
        /// When done with the array, you must release the unmanaged memory by calling <see cref="gmp_lib.free(mp_ptr[])">free</see>. 
        /// </para>
        /// </remarks>
        public mp_ptr(ushort[] values)
        {
            if (values == null) throw new ArgumentNullException("values");
            mp = new mp_base();
            mp._size = (2 * values.Length + IntPtr.Size - 1) / IntPtr.Size;
            mp.Pointer = gmp_lib.allocate((size_t)(mp._size * IntPtr.Size)).ToIntPtr();
            Marshal.Copy(new Int32[] { 0, 0 }, 0, (IntPtr)(mp.Pointer.ToInt64() + IntPtr.Size * (mp._size - 1)), IntPtr.Size >> 2);
            Marshal.Copy((short[])(object)values, 0, mp.Pointer, values.Length);
        }

        /// <summary>
        /// Creates a new array of limbs initialized with <paramref name="values"/> in unmanaged memory.
        /// </summary>
        /// <param name="values">The values of the limbs.</param>
        /// <remarks>
        /// <para>
        /// If there is not enough 32-bit words to fill out the most significant limb, it is padded with zeroes.
        /// </para>
        /// <para>
        /// When done with the array, you must release the unmanaged memory by calling <see cref="gmp_lib.free(mp_ptr[])">free</see>. 
        /// </para>
        /// </remarks>
        public mp_ptr(uint[] values)
        {
            if (values == null) throw new ArgumentNullException("values");
            mp = new mp_base();
            mp._size = (4 * values.Length + IntPtr.Size - 1) / IntPtr.Size;
            mp.Pointer = gmp_lib.allocate((size_t)(mp._size * IntPtr.Size)).ToIntPtr();
            Marshal.Copy(new Int32[] { 0, 0 }, 0, (IntPtr)(mp.Pointer.ToInt64() + IntPtr.Size * (mp._size - 1)), IntPtr.Size >> 2);
            Marshal.Copy((int[])(object)values, 0, mp.Pointer, values.Length);
        }

        /// <summary>
        /// Creates a new array of limbs initialized with <paramref name="values"/> in unmanaged memory.
        /// </summary>
        /// <param name="values">The values of the limbs.</param>
        /// <remarks>
        /// <para>
        /// If limbs size is 32 bits, the 64-bit values are split into 32-bit limbs.
        /// </para>
        /// <para>
        /// When done with the array, you must release the unmanaged memory by calling <see cref="gmp_lib.free(mp_ptr[])">free</see>. 
        /// </para>
        /// </remarks>
        public mp_ptr(ulong[] values)
        {
            if (values == null) throw new ArgumentNullException("values");
            mp = new mp_base();
            mp._size = (8 * values.Length + IntPtr.Size - 1) / IntPtr.Size;
            mp.Pointer = gmp_lib.allocate((size_t)(mp._size * IntPtr.Size)).ToIntPtr();
            Marshal.Copy(new Int32[] { 0, 0 }, 0, (IntPtr)(mp.Pointer.ToInt64() + IntPtr.Size * (mp._size - 1)), IntPtr.Size >> 2);
            Marshal.Copy((long[])(object)values, 0, mp.Pointer, values.Length);
        }

        /// <summary>
        /// Creates new pointer to array of limbs at <paramref name="mp"/>.
        /// </summary>
        /// <param name="mp">Represents an array of limbs.</param>
        public mp_ptr(mp_base mp)
        {
            this.mp = mp;
        }

        /// <summary>
        /// The number of limbs.
        /// </summary>
        /// <remarks></remarks>
        public mp_size_t Size
        {
            get
            {
                return mp._mp_size;
            }
        }

        /// <summary>
        /// Gets or sets the value of the limb at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the limb to get or set.</param>
        /// <returns></returns>
        public mp_limb_t this[int index]
        {
            get
            {
                if (index < 0) throw new ArgumentOutOfRangeException("index", "Index cannot be negative.");
                if (IntPtr.Size == 4)
                {
                    const int max_index = int.MaxValue / sizeof(int);
                    if (index > max_index) throw new ArgumentOutOfRangeException("index", "Index must be less than or equal to " + max_index.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".");
                    return new mp_limb_t((uint)Marshal.ReadInt32(mp._mp_d_intptr, index * sizeof(int)));
                }
                else
                {
                    const int max_index = int.MaxValue / sizeof(long);
                    if (index > max_index) throw new ArgumentOutOfRangeException("index", "Index must be less than or equal to " + max_index.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".");
                    return new mp_limb_t((ulong)Marshal.ReadInt64(mp._mp_d_intptr, index * sizeof(long)));
                }
            }
            set
            {
                if (index < 0) throw new ArgumentOutOfRangeException("index", "Index cannot be negative.");
                if (IntPtr.Size == 4)
                {
                    const int max_index = int.MaxValue / sizeof(int);
                    if (index > max_index) throw new ArgumentOutOfRangeException("index", "Index must be less than or equal to " + max_index.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".");
                    Marshal.WriteInt32(mp._mp_d_intptr, index * sizeof(int), (int)(value.Value));
                }
                else
                {
                    const int max_index = int.MaxValue / sizeof(long);
                    if (index > max_index) throw new ArgumentOutOfRangeException("index", "Index must be less than or equal to " + max_index.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".");
                    Marshal.WriteInt64(mp._mp_d_intptr, index * sizeof(long), (long)(value.Value));
                }
            }
        }

        /// <summary>
        /// Returns pointer to limbs in unmanaged memory.
        /// </summary>
        /// <returns>Returns pointer to limbs in unmanaged memory.</returns>
        public IntPtr ToIntPtr()
        {
            return mp._mp_d_intptr;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the array of limbs.
        /// </summary>
        /// <returns>An enumerator that iterates through the array of limbs.</returns>
        public IEnumerator<mp_limb_t> GetEnumerator()
        {
            return new _limb_enumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the array of limbs.
        /// </summary>
        /// <returns>An enumerator that iterates through the array of limbs.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new _limb_enumerator(this);
        }

        private class _limb_enumerator : IEnumerator<mp_limb_t>
        {
            int index;
            mp_ptr limbs;

            public _limb_enumerator(mp_ptr limbs)
            {
                this.limbs = limbs;
                Reset();
            }

            public mp_limb_t Current
            {
                get
                {
                    return this.limbs[index];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return (object)Current;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                index++;
                return index < System.Math.Abs(limbs.Size);
            }

            public void Reset()
            {
                index = -1;
            }
        }

    }

}