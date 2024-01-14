
using System;
using System.Diagnostics;

namespace MathGmp.Native
{

    /// <summary>
    /// Provides common functionality to <see cref="mpz_t">mpz_t</see>, <see cref="mpf_t">mpf_t</see>, and <see cref="gmp_randstate_t">gmp_randstate_t</see>.
    /// </summary>
    public class mp_base
    {

        /// <summary>
        /// Pointer to limbs in unmanaged memory.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IntPtr Pointer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal mp_size_t _size;

        /// <summary>
        /// The number of limbs.
        /// </summary>
        /// <remarks></remarks>
        public virtual mp_size_t _mp_size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        /// <summary>
        /// Gets or sets the pointer to limbs in unmanaged memory.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IntPtr _mp_d_intptr
        {
            get
            {
                return Pointer;
            }
            set
            {
                Pointer = value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private mp_ptr __mp_d = null;

        /// <summary>
        /// A pointer to an array of limbs which is the magnitude.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In <see cref="mpz_t">mpz_t</see>:
        /// </para>
        /// <para>
        /// A pointer to an array of limbs which is the magnitude.
        /// These are stored “little endian” as per the mpn functions, so <c><see cref="_mp_d">_mp_d</see>[0]</c>
        /// is the least significant limb and <c><see cref="_mp_d">_mp_d</see>[ABS(<see cref="_mp_size">_mp_size</see>) - 1]</c>
        /// is the most significant.
        /// Whenever <see cref="_mp_size">_mp_size</see> is non-zero, the most significant limb is non-zero.
        /// </para>
        /// <para>
        /// Currently there’s always at least one limb allocated, so for instance <see cref="gmp_lib.mpz_set_ui">gmp_lib.mpz_set_ui</see>
        /// never needs to reallocate, and <see cref="gmp_lib.mpz_get_ui">gmp_lib.mpz_get_ui</see> can fetch <c><see cref="_mp_d">_mp_d</see>[0]</c>
        /// unconditionally (though its value is then only wanted if <see cref="_mp_size">_mp_size</see> is non-zero).
        /// </para>
        /// <para>
        /// In <see cref="mpz_t">mpz_t</see>:
        /// </para>
        /// <para>
        /// A pointer to the array of limbs which is the absolute value of the mantissa.
        /// These are stored "little endian" as per the <c>mpn</c> functions, so <c>_mp_d[0]</c> is the least
        /// significant limb and <c>_mp_d[ABS(_mp_size)-1]</c> the most significant. 
        /// </para>
        /// <para>
        /// The most significant limb is always non-zero, but there are no other restrictions on its value,
        /// in particular the highest <c>1</c> bit can be anywhere within the limb. 
        /// </para>
        /// <para>
        /// <c>_mp_prec + 1</c> limbs are allocated to <see cref="mp_base._mp_d">mp_base._mp_d</see>, the extra limb being for
        /// convenience (see below).
        /// There are no reallocations during a calculation, only in a change of precision with <see cref="gmp_lib.mpf_set_prec">gmp_lib.mpf_set_prec</see>. 
        /// </para>
        /// </remarks>
        public virtual mp_ptr _mp_d
        {
            get
            {
                if (__mp_d == null)
                    __mp_d = new mp_ptr(this);
                return __mp_d;
            }
        }

    }

}
