
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents a pointer to a string in unmanaged memory.
    /// </summary>
    /// <remarks></remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    public struct char_ptr : IDisposable
    {

        /// <summary>
        /// Pointer to string in unmanaged memory.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
        public IntPtr Pointer;

        /// <summary>
        /// Creates new string in unmanaged memory and initializes it with <paramref name="str"/>.
        /// </summary>
        /// <param name="str">The value of the new string.</param>
        /// <remarks>
        /// <para>
        /// When done with the string, unmanaged memory must be released with <see cref="gmp_lib.free(char_ptr)">free</see>.
        /// </para>
        /// </remarks>
        public char_ptr(string str)
        {
            if (str == null) throw new ArgumentNullException("str");
            Pointer = gmp_lib.allocate((size_t)(str.Length + 1)).ToIntPtr();
            Marshal.Copy(System.Text.Encoding.ASCII.GetBytes(str), 0, Pointer, str.Length);
            Marshal.Copy(new Byte[] { 0 }, 0, (IntPtr)(Pointer.ToInt64() + str.Length), 1);
        }

        /// <summary>
        /// Creates new string using an already allocated string in unmanaged memory.
        /// </summary>
        /// <param name="pointer">Pointer to existing string in unmanaged memory.</param>
        public char_ptr(IntPtr pointer)
        {
            this.Pointer = pointer;
        }

        /// <summary>
        /// Gets pointer to string in unmanaged memory.
        /// </summary>
        /// <returns>Pointer to string in unmanaged memory.</returns>
        public IntPtr ToIntPtr()
        {
            return Pointer;
        }

        /// <summary>
        /// Gets a null <see cref="char_ptr">char_ptr</see>.
        /// </summary>
        public static readonly char_ptr Zero = new char_ptr(IntPtr.Zero);

        /// <summary>
        /// Gets the .NET <see cref="string">string</see> equivalent of the unmanaged string.
        /// </summary>
        /// <returns>The .NET <see cref="string">string</see> equivalent of the unmanaged string.</returns>
        public override string ToString()
        {
            return Marshal.PtrToStringAnsi(Pointer);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="char_ptr">char_ptr</see> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is char_ptr))
                return false;

            return Equals((char_ptr)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="char_ptr">char_ptr</see> value.
        /// </summary>
        /// <param name="other">A <see cref="char_ptr">char_ptr</see> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(char_ptr other)
        {
            return Pointer == other.Pointer;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return Pointer.GetHashCode();
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are equal.
        /// </summary>
        /// <param name="value1">A <see cref="char_ptr">char_ptr</see> value.</param>
        /// <param name="value2">A <see cref="char_ptr">char_ptr</see> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(char_ptr value1, char_ptr value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="char_ptr">char_ptr</see> value.</param>
        /// <param name="value2">A <see cref="char_ptr">char_ptr</see> value.</param>
        /// <returns><c>True</c> if the two values are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(char_ptr value1, char_ptr value2)
        {
            return !value1.Equals(value2);
        }

        public void Dispose()
        {
            if (Pointer != IntPtr.Zero) gmp_lib.free(Pointer);
            Pointer = IntPtr.Zero;
        }
    }

}
