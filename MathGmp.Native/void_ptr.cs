
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents a pointer to a block of unmanaged memory.
    /// </summary>
    /// <remarks></remarks>
    public struct void_ptr : IDisposable
    {

        private IntPtr _pointer;

        /// <summary>
        /// Creates new <see cref="void_ptr">void_ptr</see> from an exidting pointer to unmanaged memory.
        /// </summary>
        /// <param name="pointer">Pointer to unmanaged memory.</param>
        public void_ptr(IntPtr pointer)
        {
            _pointer = pointer;
        }

        /// <summary>
        /// Gets a <see cref="void_ptr">void_ptr</see> from a pointer to a block of unmanaged memory.
        /// </summary>
        /// <param name="value">A pointer to a block of unmanaged memory.</param>
        /// <returns>A <see cref="void_ptr">void_ptr</see> from a pointer to a block of unmanaged memory.</returns>
        public void_ptr FromIntPtr(IntPtr value)
        {
            _pointer = value;
            return this;
        }

        /// <summary>
        /// Gets pointer to block of unmanaged memory.
        /// </summary>
        /// <returns>Pointer to block of unmanaged memory.</returns>
        public IntPtr ToIntPtr()
        {
            return _pointer;
        }

        /// <summary>
        /// Gets a null <see cref="void_ptr">void_ptr</see>.
        /// </summary>
        public static readonly void_ptr Zero = new void_ptr(IntPtr.Zero);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="void_ptr">void_ptr</see> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is void_ptr))
                return false;

            return Equals((void_ptr)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="void_ptr">void_ptr</see> value.
        /// </summary>
        /// <param name="other">A <see cref="void_ptr">void_ptr</see> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(void_ptr other)
        {
            return _pointer == other._pointer;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _pointer.GetHashCode();
        }

        public void Dispose()
        {
            if (_pointer != IntPtr.Zero) gmp_lib.free(_pointer);
            _pointer = IntPtr.Zero;
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are equal.
        /// </summary>
        /// <param name="value1">A <see cref="void_ptr">void_ptr</see> value.</param>
        /// <param name="value2">A <see cref="void_ptr">void_ptr</see> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(void_ptr value1, void_ptr value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="void_ptr">void_ptr</see> value.</param>
        /// <param name="value2">A <see cref="void_ptr">void_ptr</see> value.</param>
        /// <returns><c>True</c> if the two values are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(void_ptr value1, void_ptr value2)
        {
            return !value1.Equals(value2);
        }

    }

}
