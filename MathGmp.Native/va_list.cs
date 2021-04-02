
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MathGmp.Native
{

    /// <summary>
    /// Represent a variable argument list.
    /// </summary>
    public class va_list
    {

        private IntPtr arguments;

        private struct va_arg
        {
            //public int arg_size;
            //public int data_size;
            public int arg_offset;
            public int data_offset;
            public Action<int> write;  // Write to unmanaged memory.
            public Action<int> read;   // Read from unmanaged memory.
        }

        private va_arg[] va_args;
        private int args_size = 0;
        private int data_size = 0;

        private Stack<int> readables = new Stack<int>();

        /// <summary>
        /// Creates a variable list of arguments in unmanaged memory.
        /// </summary>
        /// <param name="args">The list of arguments.</param>
        public va_list(params object[] args)
        {
            va_args = new va_arg[args.Length];
            
            for (int j = 0; j < args.Length; j++)
            {
                string name = args[j].GetType().Name;
                if (name == "ptr`1")
                    name = name.Replace("`1", "<" + args[j].GetType().GetGenericArguments()[0].Name + ">");

                va_args[j].arg_offset = args_size;
                va_args[j].data_offset = data_size;

                switch (name)
                {
                    case "Char":
                        args_size += 1;
                        va_args[j].write = (i) => Marshal.WriteByte(arguments, va_args[i].arg_offset, System.Text.Encoding.ASCII.GetBytes(new Char[] { (Char)args[i] })[0]);
                        break;

                    case "Byte":
                    case "SByte":
                        args_size += 1;
                        va_args[j].write = (i) => Marshal.WriteByte(arguments, va_args[i].arg_offset, (Byte)args[i]);
                        break;

                    case "Int16":
                    case "UInt16":
                        args_size += 2;
                        va_args[j].write = (i) => Marshal.WriteInt16(arguments, va_args[i].arg_offset, (Int16)args[i]);
                        break;

                    case "Int32":
                    case "UInt32":
                        args_size += 4;
                        va_args[j].write = (i) => Marshal.WriteInt32(arguments, va_args[i].arg_offset, (Int32)args[i]);
                        break;

                    case "Single":
                        args_size += 8;
                        va_args[j].write = (i) => Marshal.Copy(BitConverter.GetBytes((Double)(Single)args[i]), 0, (IntPtr)(arguments.ToInt64() + va_args[i].arg_offset), 8);
                        break;

                    case "mp_bitcnt_t":
                        args_size += 4;
                        va_args[j].write = (i) => Marshal.WriteInt32(arguments, va_args[i].arg_offset, (Int32)(mp_bitcnt_t)args[i]);
                        break;

                    case "mp_size_t":
                        args_size += 4;
                        va_args[j].write = (i) => Marshal.WriteInt32(arguments, va_args[i].arg_offset, (Int32)(mp_size_t)args[i]);
                        break;

                    case "mp_exp_t":
                        args_size += 4;
                        va_args[j].write = (i) => Marshal.WriteInt32(arguments, va_args[i].arg_offset, (Int32)(mp_exp_t)args[i]);
                        break;

                    case "Int64":
                    case "UInt64":
                        args_size += 8;
                        va_args[j].write = (i) => Marshal.WriteInt64(arguments, va_args[i].arg_offset, (Int64)args[i]);
                        break;

                    case "Double":
                        args_size += 8;
                        va_args[j].write = (i) => Marshal.Copy(BitConverter.GetBytes((Double)args[i]), 0, (IntPtr)(arguments.ToInt64() + va_args[i].arg_offset), 8);
                        break;

                    case "IntPtr":
                    case "UIntPtr":
                        args_size += IntPtr.Size;
                        va_args[j].write = (i) => Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, (IntPtr)args[i]);
                        break;

                    case "mpz_t":
                        args_size += IntPtr.Size;
                        va_args[j].write = (i) => Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, ((mpz_t)args[i]).ToIntPtr());
                        break;

                    case "mpq_t":
                        args_size += IntPtr.Size;
                        va_args[j].write = (i) => Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, ((mpq_t)args[i]).ToIntPtr());
                        break;

                    case "mpf_t":
                        args_size += IntPtr.Size;
                        va_args[j].write = (i) => Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, ((mpf_t)args[i]).ToIntPtr());
                        break;

                    case "mp_ptr":
                        args_size += IntPtr.Size;
                        va_args[j].write = (i) => Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, ((mp_ptr)args[i]).ToIntPtr());
                        break;

                    case "mp_limb_t":
                        args_size += IntPtr.Size;
                        va_args[j].write = (i) =>
                        {
                            if (IntPtr.Size == 4)
                                Marshal.WriteInt32(arguments, va_args[i].arg_offset, (Int32)(mp_limb_t)args[i]);
                            else
                                Marshal.WriteInt64(arguments, va_args[i].arg_offset, (Int64)(mp_limb_t)args[i]);
                        };
                        break;

                    case "char_ptr":
                        args_size += IntPtr.Size;
                        va_args[j].write = (i) => Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, ((char_ptr)args[i]).ToIntPtr());
                        break;

                    case "size_t":
                        args_size += IntPtr.Size;
                        va_args[j].write = (i) =>
                        {
                            if (IntPtr.Size == 4)
                                Marshal.WriteInt32(arguments, va_args[i].arg_offset, (Int32)(size_t)args[i]);
                            else
                                Marshal.WriteInt64(arguments, va_args[i].arg_offset, (Int64)(size_t)args[i]);
                        };
                        break;

                    case "String":
                        args_size += IntPtr.Size;
                        data_size += ((string)args[j]).Length + 1;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            string data = (string)args[i];
                            Marshal.Copy(Encoding.ASCII.GetBytes(data), 0, data_ptr, data.Length);
                            data_ptr = (IntPtr)((Int64)data_ptr + data.Length);
                            Marshal.Copy(new Byte[] { 0 }, 0, data_ptr, 1);
                        };
                        break;

                    case "StringBuilder":
                        args_size += IntPtr.Size;
                        data_size += ((StringBuilder)args[j]).Capacity + 1;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            StringBuilder data = (StringBuilder)args[i];
                            Marshal.Copy(Encoding.ASCII.GetBytes(data.ToString()), 0, data_ptr, data.Length);
                            Marshal.Copy(new Byte[] { 0 }, 0, (IntPtr)((Int64)data_ptr + data.Length), 1);
                            data_ptr = (IntPtr)((Int64)data_ptr + data.Capacity + 1);
                        };
                        va_args[j].read = (i) =>
                        {
                            StringBuilder data = (StringBuilder)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Remove(0, data.Length);
                            data.Append(Marshal.PtrToStringAnsi(data_ptr));
                        };
                        readables.Push(j);
                        break;

                    case "ptr<Char>":
                        args_size += IntPtr.Size;
                        data_size += 1;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            Char data = ((ptr<Char>)args[i]).Value;
                            Marshal.WriteByte(data_ptr, 0, Encoding.ASCII.GetBytes(new Char[] { data })[0]);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<Char> data = (ptr<Char>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = Encoding.ASCII.GetChars(new byte[] { Marshal.ReadByte(data_ptr) })[0];
                        };
                        readables.Push(j);
                        break;

                    case "ptr<Byte>":
                        args_size += IntPtr.Size;
                        data_size += 1;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            Byte data = ((ptr<Byte>)args[i]).Value;
                            Marshal.WriteByte(data_ptr, 0, data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<Byte> data = (ptr<Byte>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = (Byte)Marshal.ReadByte(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<SByte>":
                        args_size += IntPtr.Size;
                        data_size += 1;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            SByte data = ((ptr<SByte>)args[i]).Value;
                            Marshal.WriteByte(data_ptr, 0, (Byte)data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<SByte> data = (ptr<SByte>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = (SByte)Marshal.ReadByte(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<Int16>":
                        args_size += IntPtr.Size;
                        data_size += 2;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            Int16 data = ((ptr<Int16>)args[i]).Value;
                            Marshal.WriteInt16(data_ptr, 0, data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<Int16> data = (ptr<Int16>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = (Int16)Marshal.ReadInt16(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<UInt16>":
                        args_size += IntPtr.Size;
                        data_size += 2;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            UInt16 data = ((ptr<UInt16>)args[i]).Value;
                            Marshal.WriteInt16(data_ptr, 0, (Int16)data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<UInt16> data = (ptr<UInt16>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = (UInt16)Marshal.ReadInt16(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<Int32>":
                        args_size += IntPtr.Size;
                        data_size += 4;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            Int32 data = ((ptr<Int32>)args[i]).Value;
                            Marshal.WriteInt32(data_ptr, 0, data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<Int32> data = (ptr<Int32>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = (Int32)Marshal.ReadInt32(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<UInt32>":
                        args_size += IntPtr.Size;
                        data_size += 4;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            UInt32 data = ((ptr<UInt32>)args[i]).Value;
                            Marshal.WriteInt32(data_ptr, 0, (Int32)data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<UInt32> data = (ptr<UInt32>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = (UInt32)Marshal.ReadInt32(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<Single>":
                        args_size += IntPtr.Size;
                        data_size += 4;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            Single data = ((ptr<Single>)args[i]).Value;
                            Marshal.Copy(BitConverter.GetBytes((Single)data), 0, data_ptr, 4);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<Single> data = (ptr<Single>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Byte[] bytes = new Byte[4];
                            Marshal.Copy(data_ptr, bytes, 0, 4);
                            data.Value = BitConverter.ToSingle(bytes, 0);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<mp_bitcnt_t>":
                        args_size += IntPtr.Size;
                        data_size += 4;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            mp_bitcnt_t data = ((ptr<mp_bitcnt_t>)args[i]).Value;
                            Marshal.WriteInt32(data_ptr, 0, (Int32)data.Value);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<mp_bitcnt_t> data = (ptr<mp_bitcnt_t>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value.Value = (uint)Marshal.ReadInt32(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<mp_size_t>":
                        args_size += IntPtr.Size;
                        data_size += 4;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            mp_size_t data = ((ptr<mp_size_t>)args[i]).Value;
                            Marshal.WriteInt32(data_ptr, 0, data.Value);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<mp_size_t> data = (ptr<mp_size_t>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value.Value = Marshal.ReadInt32(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<mp_exp_t>":
                        args_size += IntPtr.Size;
                        data_size += 4;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            mp_exp_t data = ((ptr<mp_exp_t>)args[i]).Value;
                            Marshal.WriteInt32(data_ptr, 0, data.Value);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<mp_exp_t> data = (ptr<mp_exp_t>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value.Value = Marshal.ReadInt32(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<Int64>":
                        args_size += IntPtr.Size;
                        data_size += 8;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            Int64 data = ((ptr<Int64>)args[i]).Value;
                            Marshal.WriteInt64(data_ptr, 0, data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<Int64> data = (ptr<Int64>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = (Int64)Marshal.ReadInt64(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<UInt64>":
                        args_size += IntPtr.Size;
                        data_size += 8;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            UInt64 data = ((ptr<UInt64>)args[i]).Value;
                            Marshal.WriteInt64(data_ptr, 0, (Int64)data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<UInt64> data = (ptr<UInt64>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = (UInt64)Marshal.ReadInt64(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<Double>":
                        args_size += IntPtr.Size;
                        data_size += 8;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            Double data = ((ptr<Double>)args[i]).Value;
                            Marshal.Copy(BitConverter.GetBytes(data), 0, data_ptr, 8);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<Double> data = (ptr<Double>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Byte[] bytes = new Byte[8];
                            Marshal.Copy(data_ptr, bytes, 0, 8);
                            data.Value = BitConverter.ToDouble(bytes, 0);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<mp_limb_t>":
                        args_size += IntPtr.Size;
                        data_size += IntPtr.Size;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            mp_limb_t data = ((ptr<mp_limb_t>)args[i]).Value;
                            if (IntPtr.Size == 4)
                                Marshal.WriteInt32(data_ptr, 0, (Int32)data.Value);
                            else
                                Marshal.WriteInt64(data_ptr, 0, (Int64)data.Value);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<mp_limb_t> data = (ptr<mp_limb_t>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            if (IntPtr.Size == 4)
                                data.Value.Value = (UInt32)Marshal.ReadInt32(data_ptr);
                            else
                                data.Value.Value = (UInt64)Marshal.ReadInt64(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<size_t>":
                        args_size += IntPtr.Size;
                        data_size += IntPtr.Size;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            size_t data = ((ptr<size_t>)args[i]).Value;
                            if (IntPtr.Size == 4)
                                Marshal.WriteInt32(data_ptr, 0, (Int32)data.Value);
                            else
                                Marshal.WriteInt64(data_ptr, 0, (Int64)data.Value);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<size_t> data = (ptr<size_t>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            if (IntPtr.Size == 4)
                                data.Value.Value = (UInt32)Marshal.ReadInt32(data_ptr);
                            else
                                data.Value.Value = (UInt64)Marshal.ReadInt64(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    case "ptr<IntPtr>":
                        args_size += IntPtr.Size;
                        data_size += IntPtr.Size;
                        va_args[j].write = (i) =>
                        {
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            Marshal.WriteIntPtr(arguments, va_args[i].arg_offset, data_ptr);
                            IntPtr data = ((ptr<IntPtr>)args[i]).Value;
                            Marshal.WriteIntPtr(data_ptr, 0, data);
                        };
                        va_args[j].read = (i) =>
                        {
                            ptr<IntPtr> data = (ptr<IntPtr>)args[i];
                            IntPtr data_ptr = (IntPtr)(arguments.ToInt64() + args_size + va_args[i].data_offset);
                            data.Value = Marshal.ReadIntPtr(data_ptr);
                        };
                        readables.Push(j);
                        break;

                    default:
                        throw new System.InvalidOperationException("Unsupported variable argument type '" + name + "'");

                }
            }

            // Write arguments to unmanaged memory.
            arguments = gmp_lib.allocate((size_t)(args_size + data_size)).ToIntPtr();
            for (int i = 0; i < args.Length; i++)
                va_args[i].write(i);

        }

        /// <summary>
        /// Return the pointer to the list of arguments in unmanaged memory.
        /// </summary>
        /// <returns>The pointer to the list of arguments in unmanaged memory.</returns>
        public IntPtr ToIntPtr()
        {
            return arguments;
        }

        /// <summary>
        /// Retrieves argument values from unmanaged memory.
        /// </summary>
        public void RetrieveArgumentValues()
        {
            foreach (int i in readables)
                va_args[i].read(i);
            gmp_lib.free(arguments);
        }

    }

}