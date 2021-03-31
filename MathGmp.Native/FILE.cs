
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents a file stream.
    /// </summary>
    /// <remarks></remarks>
    public struct FILE
    {

        /// <summary>
        /// File pointer in unmanaged memory.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
        public IntPtr Value;

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>True</c> if <paramref name="obj"/> is an instance of <see cref="FILE">FILE</see> and equals the value of this instance; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is FILE))
                return false;

            return Equals((FILE)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="FILE">FILE</see> value.
        /// </summary>
        /// <param name="other">A <see cref="FILE">FILE</see> value to compare to this instance.</param>
        /// <returns><c>True</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>False</c>.</returns>
        public bool Equals(FILE other)
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
        /// <param name="value1">A <see cref="FILE">FILE</see> value.</param>
        /// <param name="value2">A <see cref="FILE">FILE</see> value.</param>
        /// <returns><c>True</c> if the two values are equal, and <c>False</c> otherwise.</returns>
        public static bool operator ==(FILE value1, FILE value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Gets a value that indicates whether the two argument values are different.
        /// </summary>
        /// <param name="value1">A <see cref="FILE">FILE</see> value.</param>
        /// <param name="value2">A <see cref="FILE">FILE</see> value.</param>
        /// <returns><c>True</c> if the two FILE are different, and <c>False</c> otherwise.</returns>
        public static bool operator !=(FILE value1, FILE value2)
        {
            return !value1.Equals(value2);
        }

    }

}