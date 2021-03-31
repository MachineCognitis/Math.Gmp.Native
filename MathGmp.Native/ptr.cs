
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native
{

    /// <summary>
    /// Represents a pointer to a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">A value type.</typeparam>
    /// <remarks>
    /// <para>
    /// Mimics the C address-of (&amp;) construct to pass the address of a value type variable
    /// to a function of the GMP library.
    /// </para>
    /// <para>
    /// Note that this is only for value types. Strings and arrays have their own "pointer"
    /// types defined with names ending in <c>_ptr</c>.
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    public class ptr<T> where T : struct
    {
        /// <summary>
        /// The value that is "pointed to".
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public T Value;

        /// <summary>
        /// Creates a new pointer with default value.
        /// </summary>
        public ptr()
        {
        }

        /// <summary>
        /// Creates a new pointer with <see cref="Value">Value</see> set to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public ptr(T value)
        {
            this.Value = value;
        }

    }

}
