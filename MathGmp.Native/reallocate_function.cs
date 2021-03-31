
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native
{

    /// <summary>
    /// Resize a previously allocated block ptr of <paramref name="old_size"/> bytes to be <paramref name="new_size"/> bytes.
    /// </summary>
    /// <param name="ptr">Pointer to previously allocated block.</param>
    /// <param name="old_size">Number of bytes of previously allocated block.</param>
    /// <param name="new_size">New number of bytes of previously allocated block.</param>
    /// <returns>A previously allocated block ptr of <paramref name="old_size"/> bytes to be <paramref name="new_size"/> bytes.</returns>
    public delegate void_ptr reallocate_function(void_ptr ptr, size_t old_size, size_t new_size);

}
