
using System;
using System.Runtime.InteropServices;

namespace Math.Gmp.Native
{

    /// <summary>
    /// Represents a pointer to a string in unmanaged memory.
    /// </summary>
    /// <remarks></remarks>
    public struct char_ptr
    {

        internal IntPtr pointer;

        /// <summary>
        /// Creates new string in unmanaged memory and initializes it with <paramref name="str"/>.
        /// </summary>
        /// <param name="str">The value of the new string.</param>
        /// <remarks>
        /// <para>
        /// When done with the string, unmanaged memory must be released with <see cref="gmp_lib.free(char_ptr)"/> .
        /// </para>
        /// </remarks>
        public char_ptr(string str)
        {
            if (str == null) throw new ArgumentNullException("str");
            pointer = gmp_lib.allocate((size_t)(str.Length + 1)).ToIntPtr();
            Marshal.Copy(System.Text.Encoding.ASCII.GetBytes(str), 0, pointer, str.Length);
            Marshal.Copy(new Byte[] { 0 }, 0, (IntPtr)(pointer.ToInt64() + str.Length), 1);
        }

        internal char_ptr(IntPtr pointer)
        {
            this.pointer = pointer;
        }

        /// <summary>
        /// Gets pointer to string in unmanaged memory.
        /// </summary>
        /// <returns>Pointer to string in unmanaged memory.</returns>
        public IntPtr ToIntPtr()
        {
            return pointer;
        }

        /// <summary>
        /// Gets a null <see cref="char_ptr"/>.
        /// </summary>
        public static readonly char_ptr Zero = new char_ptr(IntPtr.Zero);

        /// <summary>
        /// Gets the .NET <see cref="string"/> equivalent of the unmanaged string.
        /// </summary>
        /// <returns>The .NET <see cref="string"/> equivalent of the unmanaged string.</returns>
        public override string ToString()
        {
            return Marshal.PtrToStringAnsi(pointer);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="char_ptr"/> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is char_ptr))
                return false;

            return Equals((char_ptr)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="char_ptr"/> value.
        /// </summary>
        /// <param name="other">A <see cref="char_ptr"/> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(char_ptr other)
        {
            return pointer == other.pointer;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return pointer.GetHashCode();
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are equal.
        /// </summary>
        /// <param name="value1">A <see cref="char_ptr"/> value.</param>
        /// <param name="value2">A <see cref="char_ptr"/> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(char_ptr value1, char_ptr value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="char_ptr"/> value.</param>
        /// <param name="value2">A <see cref="char_ptr"/> value.</param>
        /// <returns><c>True</c> if the two values are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(char_ptr value1, char_ptr value2)
        {
            return !value1.Equals(value2);
        }

    }

}