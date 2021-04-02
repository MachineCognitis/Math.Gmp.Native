
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents the state of a random number generator.
    /// </summary>
    /// <remarks></remarks>
    public class gmp_randstate_t
    {

        private IntPtr _pointer;

        /// <summary>
        /// Creates a new random number generator state.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When done with the random number generator state, unmanaged memory must be released with <see cref="gmp_lib.free(gmp_randstate_t)">free</see>.
        /// </para>
        /// </remarks>
        public gmp_randstate_t()
        {
            // Allocation sizes take into account the members alignment done by the C compiler.
            _pointer = gmp_lib.allocate(IntPtr.Size == 4 ? 20U : 32U).ToIntPtr();
        }

        /// <summary>
        /// Get unmanaged memory pointer to the state of a random number generator.
        /// </summary>
        /// <returns>The unmanaged memory pointer to the state of a random number generator.</returns>
        public IntPtr ToIntPtr()
        {
            return _pointer;
        }

    }

}