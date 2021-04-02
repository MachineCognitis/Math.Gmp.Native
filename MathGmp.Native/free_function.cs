
using System;
using System.Runtime.InteropServices;

namespace MathGmp.Native

{
    /// <summary>
    /// De-allocate the space pointed to by <paramref name="ptr"/>.
    /// </summary>
    /// <param name="ptr">Pointer to previously allocated block.</param>
    /// <param name="size">Number of bytes of previously allocated block.</param>
    public delegate void free_function(void_ptr ptr, size_t size);

}
