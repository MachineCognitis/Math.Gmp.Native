
using System;
using NUnit.Framework;
using System.Runtime.InteropServices;
using MathGmp.Native;
using System.Linq;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest
    {

        #region "Utility functions."

        private delegate TResult _Func<out TResult>();

        private string Test(_Func<object> func)
        {
            object result = null;
            try
            {
                result = func();
            }
            catch (System.Exception ex)
            {
                return ex.GetType().Name;
            }
            return result.ToString();
        }

        #endregion

        #region "Types."

        [Test]
        public void char_ptr_test()
        {
            // Create new string in unmanaged memory.
            char_ptr s = new char_ptr("Test");
            Assert.IsTrue(s.ToString() == "Test");

            // Set string pointer to zero.
            s = char_ptr.Zero;
            Assert.IsTrue(s.ToIntPtr() == IntPtr.Zero);

            // Assert that obj and s are the same pointer.
            object obj = s;
            Assert.IsTrue(s.Equals(obj) == true);

            // Create new unmanaged string.
            char_ptr t = new char_ptr("Test");

            // Assert that s and t are distinct string pointers.
            Assert.IsTrue(s.Equals(t) == false);

            // Check inequality and equality of s and t pointers.
            Assert.IsTrue(s != t);
            gmp_lib.free(t);
            t = s;
            Assert.IsTrue(s == t);
            gmp_lib.free(t);
        }

        [Test]
        public void gmp_randstate_t_test()
        {
            // Allocate and release random number generator state.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.free(state);
        }

        [Test]
        public void mp_bitcnt_t_test()
        {
            // uint
            mp_bitcnt_t v;

            byte zero = 0;
            sbyte minusOne = -1;

            // Check implicit and explict conversions to mp_bitcnt_t.
            v = (byte)zero;
            v = (mp_bitcnt_t)(sbyte)zero;
            v = (ushort)zero;
            v = (mp_bitcnt_t)(short)zero;
            v = (uint)zero;
            v = (mp_bitcnt_t)(int)zero;
            v = (mp_bitcnt_t)(ulong)zero;
            v = (mp_bitcnt_t)(long)zero;

            // Check implicit and explict conversions from mp_bitcnt_t.
            byte b = (byte)v;
            sbyte sb = (sbyte)v;
            ushort us = (ushort)v;
            short s = (short)v;
            uint ui = v;
            int i = (int)v;
            ulong ul = v;
            long l = v;

            // Check OverflowException conversions to mp_bitcnt_t.
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(byte)byte.MaxValue) == byte.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(sbyte)minusOne) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(sbyte)sbyte.MaxValue) == sbyte.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(ushort)ushort.MaxValue) == ushort.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(short)minusOne) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(short)short.MaxValue) == short.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(uint)uint.MaxValue) == uint.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(int)minusOne) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(int)int.MaxValue) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(ulong)ulong.MaxValue) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(long)minusOne) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_bitcnt_t)(long)long.MaxValue) == typeof(OverflowException).Name);

            // Check OverflowException conversions from mp_bitcnt_t.
            Assert.IsTrue(Test(() => b = (byte)(new mp_bitcnt_t(uint.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => sb = (sbyte)(new mp_bitcnt_t(uint.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => us = (ushort)(new mp_bitcnt_t(uint.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => s = (short)(new mp_bitcnt_t(uint.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ui = (uint)(new mp_bitcnt_t(uint.MaxValue))) == uint.MaxValue.ToString());
            Assert.IsTrue(Test(() => i = (int)(new mp_bitcnt_t(uint.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ul = (ulong)(new mp_bitcnt_t(uint.MaxValue))) == uint.MaxValue.ToString());
            Assert.IsTrue(Test(() => l = (long)(new mp_bitcnt_t(uint.MaxValue))) == uint.MaxValue.ToString());

            // Check equality and inequality.
            Object obj = new mp_bitcnt_t(8);
            Assert.IsTrue((new mp_bitcnt_t(8)).Equals(obj));
            Assert.IsTrue(!(new mp_bitcnt_t(8)).Equals(new int()));
            Assert.IsTrue((new mp_bitcnt_t(8)).Equals(new mp_bitcnt_t(8)));
            Assert.IsTrue(!(new mp_bitcnt_t(8)).Equals(new mp_bitcnt_t(9)));
            Assert.IsTrue((new mp_bitcnt_t(8)) == (new mp_bitcnt_t(8)));
            Assert.IsTrue((new mp_bitcnt_t(8)) != (new mp_bitcnt_t(9)));
        }

        //[Test]
        //public void mp_exp_ptr_test()
        //{
        //    // Create new exponent pointer.
        //    mp_exp_ptr i = new mp_exp_ptr(8);

        //    // Assert that obj and i are the same pointer.
        //    object obj = i;
        //    Assert.IsTrue(i.Equals(obj) == true);

        //    // Create new exponent pointer.
        //    mp_exp_ptr j = new mp_exp_ptr(8);

        //    // Assert that i and j are distinct exponent pointers.
        //    Assert.IsTrue(i.Equals(j) == false);

        //    // Check equality of i and j values.
        //    Assert.IsTrue(i.Value == j.Value);
        //    i.Value += 4;
        //    Assert.IsTrue(i.Value != j.Value);

        //    // Check inequality and equality of i and j pointers.
        //    Assert.IsTrue(i != j);
        //    gmp_lib.free(j);
        //    j = i;
        //    Assert.IsTrue(i == j);

        //    // Check equality of i and j values.
        //    Assert.IsTrue(i.Value == j.Value);
        //    i.Value += 4;
        //    Assert.IsTrue(i.Value == j.Value);

        //    gmp_lib.free(j);
        //}

        [Test]
        public void mp_exp_t_test()
        {
            // int
            mp_exp_t v;

            byte zero = 0;

            // Check implicit and explict conversions to mp_exp_t.
            v = (byte)zero;
            v = (sbyte)zero;
            v = (ushort)zero;
            v = (short)zero;
            v = (mp_exp_t)(uint)zero;
            v = (int)zero;
            v = (mp_exp_t)(ulong)zero;
            v = (mp_exp_t)(long)zero;

            // Check implicit and explict conversions from mp_exp_t.
            byte b = (byte)v;
            sbyte sb = (sbyte)v;
            ushort us = (ushort)v;
            short s = (short)v;
            uint ui = (uint)v;
            int i = v;
            ulong ul = (ulong)v;
            long l = v;

            // Check OverflowException conversions to mp_exp_t.
            Assert.IsTrue(Test(() => v = (mp_exp_t)(byte)byte.MaxValue) == byte.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_exp_t)(sbyte)sbyte.MinValue) == sbyte.MinValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_exp_t)(sbyte)sbyte.MaxValue) == sbyte.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_exp_t)(ushort)ushort.MaxValue) == ushort.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_exp_t)(short)short.MinValue) == short.MinValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_exp_t)(short)short.MaxValue) == short.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_exp_t)(uint)uint.MaxValue) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_exp_t)(int)int.MinValue) == int.MinValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_exp_t)(int)int.MaxValue) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_exp_t)(ulong)ulong.MaxValue) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_exp_t)(long)long.MinValue) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_exp_t)(long)long.MaxValue) == typeof(OverflowException).Name);

            // Check OverflowException conversions from mp_exp_t.
            Assert.IsTrue(Test(() => b = (byte)(new mp_exp_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => b = (byte)(new mp_exp_t(int.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => sb = (sbyte)(new mp_exp_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => sb = (sbyte)(new mp_exp_t(int.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => us = (ushort)(new mp_exp_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => us = (ushort)(new mp_exp_t(int.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => s = (short)(new mp_exp_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => s = (short)(new mp_exp_t(int.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ui = (uint)(new mp_exp_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ui = (uint)(new mp_exp_t(int.MaxValue))) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => i = (int)(new mp_exp_t(int.MinValue))) == int.MinValue.ToString());
            Assert.IsTrue(Test(() => i = (int)(new mp_exp_t(int.MaxValue))) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => ul = (ulong)(new mp_exp_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ul = (ulong)(new mp_exp_t(int.MaxValue))) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => l = (long)(new mp_exp_t(int.MinValue))) == int.MinValue.ToString());
            Assert.IsTrue(Test(() => l = (long)(new mp_exp_t(int.MaxValue))) == int.MaxValue.ToString());

            // Check equality and inequality.
            Object obj = new mp_exp_t(8);
            Assert.IsTrue((new mp_exp_t(8)).Equals(obj));
            Assert.IsTrue(!(new mp_exp_t(8)).Equals(new byte()));
            Assert.IsTrue((new mp_exp_t(8)).Equals(new mp_exp_t(8)));
            Assert.IsTrue(!(new mp_exp_t(8)).Equals(new mp_exp_t(9)));
            Assert.IsTrue((new mp_exp_t(8)) == (new mp_exp_t(8)));
            Assert.IsTrue((new mp_exp_t(8)) != (new mp_exp_t(9)));
        }

        [Test]
        public void mp_limb_t_test()
        {
            //ulong
            mp_limb_t v;

            byte zero = 0;
            sbyte minusOne = -1;

            // Check implicit and explict conversions to mp_limb_t.
            v = (byte)zero;
            v = (mp_limb_t)(sbyte)zero;
            v = (ushort)zero;
            v = (mp_limb_t)(short)zero;
            v = (uint)zero;
            v = (mp_limb_t)(int)zero;
            v = (ulong)zero;
            v = (mp_limb_t)(long)zero;

            // Check implicit and explict conversions from mp_limb_t.
            byte b = (byte)v;
            sbyte sb = (sbyte)v;
            ushort us = (ushort)v;
            short s = (short)v;
            uint ui = (uint)v;
            int i = (int)v;
            ulong ul = v;
            long l = (long)v;

            // Check OverflowException conversions to mp_limb_t.
            Assert.AreEqual(Test(() => v = (mp_limb_t)(byte)byte.MaxValue), "0x00000000000000ff");
            Assert.IsTrue(Test(() => v = (mp_limb_t)(sbyte)minusOne) == typeof(OverflowException).Name);
            Assert.AreEqual(Test(() => v = (mp_limb_t)(sbyte)sbyte.MaxValue), "0x000000000000007f");
            Assert.AreEqual(Test(() => v = (mp_limb_t)(ushort)ushort.MaxValue), "0x000000000000ffff");
            Assert.IsTrue(Test(() => v = (mp_limb_t)(short)minusOne) == typeof(OverflowException).Name);
            Assert.AreEqual(Test(() => v = (mp_limb_t)(short)short.MaxValue), "0x0000000000007fff");
            Assert.AreEqual(Test(() => v = (mp_limb_t)(uint)uint.MaxValue), "0x00000000ffffffff");
            Assert.IsTrue(Test(() => v = (mp_limb_t)(int)minusOne) == typeof(OverflowException).Name);
            Assert.AreEqual(Test(() => v = (mp_limb_t)(int)int.MaxValue), "0x000000007fffffff");
            Assert.AreEqual(Test(() => v = (mp_limb_t)(ulong)ulong.MaxValue), "0xffffffffffffffff");
            Assert.IsTrue(Test(() => v = (mp_limb_t)(long)minusOne) == typeof(OverflowException).Name);
            Assert.AreEqual(Test(() => v = (mp_limb_t)(long)long.MaxValue), "0x7fffffffffffffff");

            // Check OverflowException conversions from uintmax_t.
            Assert.IsTrue(Test(() => b = (byte)(new mp_limb_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => sb = (sbyte)(new mp_limb_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => us = (ushort)(new mp_limb_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => s = (short)(new mp_limb_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ui = (uint)(new mp_limb_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => i = (int)(new mp_limb_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ul = (ulong)(new mp_limb_t(ulong.MaxValue))) == ulong.MaxValue.ToString());
            Assert.IsTrue(Test(() => l = (long)(new mp_limb_t(ulong.MaxValue))) == typeof(OverflowException).Name);

            // Check equality and inequality.
            Object obj = new mp_limb_t(8);
            Assert.IsTrue((new mp_limb_t(8)).Equals(obj));
            Assert.IsTrue(!(new mp_limb_t(8)).Equals(new byte()));
            Assert.IsTrue((new mp_limb_t(8)).Equals(new mp_limb_t(8)));
            Assert.IsTrue(!(new mp_limb_t(8)).Equals(new mp_limb_t(9)));
            Assert.IsTrue((new mp_limb_t(8)) == (new mp_limb_t(8)));
            Assert.IsTrue((new mp_limb_t(8)) != (new mp_limb_t(9)));
        }

        [Test]
        public void mp_ptr_test()
        {
            mp_ptr bytes0 = new mp_ptr(new byte[0]);
            Assert.IsTrue(bytes0.Size == 0);
            gmp_lib.free(bytes0);

            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            mp_ptr bytes1 = new mp_ptr(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 });
            gmp_lib.mpz_roinit_n(z, bytes1, bytes1.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0x00000088 }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x0000000000000088 }));
            gmp_lib.free(bytes1);

            mp_ptr bytes2 = new mp_ptr(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99 });
            gmp_lib.mpz_roinit_n(z, bytes2, bytes2.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0x00009988 }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x0000000000009988 }));
            gmp_lib.free(bytes2);

            mp_ptr bytes3 = new mp_ptr(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa });
            gmp_lib.mpz_roinit_n(z, bytes3, bytes3.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0x00aa9988 }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x0000000000aa9988 }));
            gmp_lib.free(bytes3);

            mp_ptr bytes4 = new mp_ptr(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb });
            gmp_lib.mpz_roinit_n(z, bytes4, bytes4.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988 }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x00000000bbaa9988 }));
            gmp_lib.free(bytes4);

            mp_ptr bytes5 = new mp_ptr(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc });
            gmp_lib.mpz_roinit_n(z, bytes5, bytes5.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988, 0x000000cc }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x000000ccbbaa9988 }));
            gmp_lib.free(bytes5);

            mp_ptr bytes6 = new mp_ptr(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd });
            gmp_lib.mpz_roinit_n(z, bytes6, bytes6.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988, 0x0000ddcc }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x0000ddccbbaa9988 }));
            gmp_lib.free(bytes6);

            mp_ptr bytes7 = new mp_ptr(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee });
            gmp_lib.mpz_roinit_n(z, bytes7, bytes7.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988, 0x00eeddcc }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x00eeddccbbaa9988 }));
            gmp_lib.free(bytes7);

            mp_ptr bytes8 = new mp_ptr(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff });
            gmp_lib.mpz_roinit_n(z, bytes8, bytes8.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988, 0xffeeddcc }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0xffeeddccbbaa9988 }));
            gmp_lib.free(bytes8);

            mp_ptr ushort1 = new mp_ptr(new ushort[] { 0x1100, 0x3322, 0x5544, 0x7766, 0x9988 });
            gmp_lib.mpz_roinit_n(z, ushort1, ushort1.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0x00009988 }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x0000000000009988 }));
            gmp_lib.free(ushort1);

            mp_ptr ushort2 = new mp_ptr(new ushort[] { 0x1100, 0x3322, 0x5544, 0x7766, 0x9988, 0xbbaa });
            gmp_lib.mpz_roinit_n(z, ushort2, ushort2.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988 }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x00000000bbaa9988 }));
            gmp_lib.free(ushort2);

            mp_ptr ushort3 = new mp_ptr(new ushort[] { 0x1100, 0x3322, 0x5544, 0x7766, 0x9988, 0xbbaa, 0xddcc });
            gmp_lib.mpz_roinit_n(z, ushort3, ushort3.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988, 0x0000ddcc }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x0000ddccbbaa9988 }));
            gmp_lib.free(ushort3);

            mp_ptr ushort4 = new mp_ptr(new ushort[] { 0x1100, 0x3322, 0x5544, 0x7766, 0x9988, 0xbbaa, 0xddcc, 0xffee });
            gmp_lib.mpz_roinit_n(z, ushort4, ushort4.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988, 0xffeeddcc }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0xffeeddccbbaa9988 }));
            gmp_lib.free(ushort4);

            mp_ptr uint1 = new mp_ptr(new uint[] { 0x33221100, 0x77665544, 0xbbaa9988 });
            gmp_lib.mpz_roinit_n(z, uint1, uint1.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988 }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0x00000000bbaa9988 }));
            gmp_lib.free(uint1);

            mp_ptr uint2 = new mp_ptr(new uint[] { 0x33221100, 0x77665544, 0xbbaa9988, 0xffeeddcc });
            gmp_lib.mpz_roinit_n(z, uint2, uint2.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988, 0xffeeddcc }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0xffeeddccbbaa9988 }));
            gmp_lib.free(uint2);

            mp_ptr ulong1 = new mp_ptr(new ulong[] { 0x7766554433221100, 0xffeeddccbbaa9988 });
            gmp_lib.mpz_roinit_n(z, ulong1, ulong1.Size);
            if (gmp_lib.mp_bits_per_limb == 32)
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x33221100, 0x77665544, 0xbbaa9988, 0xffeeddcc }));
            else
                Assert.IsTrue(gmp_lib.mpz_limbs_read(z).SequenceEqual(new mp_limb_t[] { 0x7766554433221100, 0xffeeddccbbaa9988 }));
            gmp_lib.free(ulong1);
        }

        [Test]
        public void mp_size_t_test()
        {
            // int
            mp_size_t v;

            byte zero = 0;

            // Check implicit and explict conversions to mp_size_t.
            v = (byte)zero;
            v = (sbyte)zero;
            v = (ushort)zero;
            v = (short)zero;
            v = (mp_size_t)(uint)zero;
            v = (int)zero;
            v = (mp_size_t)(ulong)zero;
            v = (mp_size_t)(long)zero;

            // Check implicit and explict conversions from mp_size_t.
            byte b = (byte)v;
            sbyte sb = (sbyte)v;
            ushort us = (ushort)v;
            short s = (short)v;
            uint ui = (uint)v;
            int i = v;
            ulong ul = (ulong)v;
            long l = v;

            // Check OverflowException conversions to mp_size_t.
            Assert.IsTrue(Test(() => v = (mp_size_t)(byte)byte.MaxValue) == byte.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_size_t)(sbyte)sbyte.MinValue) == sbyte.MinValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_size_t)(sbyte)sbyte.MaxValue) == sbyte.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_size_t)(ushort)ushort.MaxValue) == ushort.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_size_t)(short)short.MinValue) == short.MinValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_size_t)(short)short.MaxValue) == short.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_size_t)(uint)uint.MaxValue) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_size_t)(int)int.MinValue) == int.MinValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_size_t)(int)int.MaxValue) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (mp_size_t)(ulong)ulong.MaxValue) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_size_t)(long)long.MinValue) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (mp_size_t)(long)long.MaxValue) == typeof(OverflowException).Name);

            // Check OverflowException conversions from mp_size_t.
            Assert.IsTrue(Test(() => b = (byte)(new mp_size_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => b = (byte)(new mp_size_t(int.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => sb = (sbyte)(new mp_size_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => sb = (sbyte)(new mp_size_t(int.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => us = (ushort)(new mp_size_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => us = (ushort)(new mp_size_t(int.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => s = (short)(new mp_size_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => s = (short)(new mp_size_t(int.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ui = (uint)(new mp_size_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ui = (uint)(new mp_size_t(int.MaxValue))) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => i = (int)(new mp_size_t(int.MinValue))) == int.MinValue.ToString());
            Assert.IsTrue(Test(() => i = (int)(new mp_size_t(int.MaxValue))) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => ul = (ulong)(new mp_size_t(int.MinValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ul = (ulong)(new mp_size_t(int.MaxValue))) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => l = (long)(new mp_size_t(int.MinValue))) == int.MinValue.ToString());
            Assert.IsTrue(Test(() => l = (long)(new mp_size_t(int.MaxValue))) == int.MaxValue.ToString());

            // Check equality and inequality.
            Object obj = new mp_size_t(8);
            Assert.IsTrue((new mp_size_t(8)).Equals(obj));
            Assert.IsTrue(!(new mp_size_t(8)).Equals(new byte()));
            Assert.IsTrue((new mp_size_t(8)).Equals(new mp_size_t(8)));
            Assert.IsTrue(!(new mp_size_t(8)).Equals(new mp_size_t(9)));
            Assert.IsTrue((new mp_size_t(8)) == (new mp_size_t(8)));
            Assert.IsTrue((new mp_size_t(8)) != (new mp_size_t(9)));
        }

        [Test]
        public void size_t_test()
        {
            // ulong
            size_t v;

            byte zero = 0;
            sbyte minusOne = -1;

            // Check implicit and explict conversions to size_t.
            v = (byte)zero;
            v = (size_t)(sbyte)zero;
            v = (ushort)zero;
            v = (size_t)(short)zero;
            v = (uint)zero;
            v = (size_t)(int)zero;
            v = (ulong)zero;
            v = (size_t)(long)zero;

            // Check implicit and explict conversions from size_t.
            byte b = (byte)v;
            sbyte sb = (sbyte)v;
            ushort us = (ushort)v;
            short s = (short)v;
            uint ui = (uint)v;
            int i = (int)v;
            ulong ul = v;
            long l = (long)v;

            // Check OverflowException conversions to size_t.
            Assert.IsTrue(Test(() => v = (size_t)(byte)byte.MaxValue) == byte.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (size_t)(sbyte)minusOne) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (size_t)(sbyte)sbyte.MaxValue) == sbyte.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (size_t)(ushort)ushort.MaxValue) == ushort.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (size_t)(short)minusOne) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (size_t)(short)short.MaxValue) == short.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (size_t)(uint)uint.MaxValue) == uint.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (size_t)(int)minusOne) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (size_t)(int)int.MaxValue) == int.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (size_t)(ulong)ulong.MaxValue) == ulong.MaxValue.ToString());
            Assert.IsTrue(Test(() => v = (size_t)(long)minusOne) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => v = (size_t)(long)long.MaxValue) == long.MaxValue.ToString());

            // Check OverflowException conversions from size_t.
            Assert.IsTrue(Test(() => b = (byte)(new size_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => sb = (sbyte)(new size_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => us = (ushort)(new size_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => s = (short)(new size_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ui = (uint)(new size_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => i = (int)(new size_t(ulong.MaxValue))) == typeof(OverflowException).Name);
            Assert.IsTrue(Test(() => ul = (ulong)(new size_t(ulong.MaxValue))) == ulong.MaxValue.ToString());
            Assert.IsTrue(Test(() => l = (long)(new size_t(ulong.MaxValue))) == typeof(OverflowException).Name);

            // Check equality and inequality.
            Object obj = new size_t(8);
            Assert.IsTrue((new size_t(8)).Equals(obj));
            Assert.IsTrue(!(new size_t(8)).Equals(new byte()));
            Assert.IsTrue((new size_t(8)).Equals(new size_t(8)));
            Assert.IsTrue(!(new size_t(8)).Equals(new size_t(9)));
            Assert.IsTrue((new size_t(8)) == (new size_t(8)));
            Assert.IsTrue((new size_t(8)) != (new size_t(9)));
        }

        [Test]
        public void void_ptr_test()
        {
            // Set block pointer to zero.
            void_ptr s = void_ptr.Zero;
            Assert.IsTrue(s.ToIntPtr() == IntPtr.Zero);

            // Create new block in unmanaged memory.
            s = gmp_lib.allocate(10);

            // Assert that obj and s are the same pointer.
            object obj = s;
            Assert.IsTrue(s.Equals(obj) == true);

            // Create new unmanaged string.
            void_ptr t = gmp_lib.allocate(10);

            // Assert that s and t are distinct block pointers.
            Assert.IsTrue(s.Equals(t) == false);

            // Check inequality and equality of s and t pointers.
            Assert.IsTrue(s != t);
            gmp_lib.free(t);
            t = s;
            Assert.IsTrue(s == t);
            gmp_lib.free(t);
        }

        [Test]
        public void mpz_t_test()
        {
            // Create new multiple-precision integers.
            mpz_t x = "123 456 789 012 345 678 901 234 567 890";
            mpz_t y = "123 456 789 012 345 678 901 234 567 890";
            mpz_t z = new mpz_t();
            mpz_t result = "246 913 578 024 691 357 802 469 135 780";
            gmp_lib.mpz_init(z);

            // Add integers, and assert result.
            gmp_lib.mpz_add(z, x, y);
            Assert.IsTrue(gmp_lib.mpz_cmp(z, result) == 0);

            // Release allocated memory for integers.
            gmp_lib.mpz_clears(x, y, z, result, null);
        }

        [Test]
        public void mpq_t_test()
        {
            // Create new multiple-precision rationals.
            mpq_t x = "123 456 789 012 345 / 0x2 6975 02EC 8AD2";
            mpq_t y = "03 404 420 603 357 571 / 678 901 234 567 890";
            mpq_t z = new mpq_t();
            mpq_t result = "246 913 578 024 690 / 0b10 0110 1001 0111 0101 0000 0010 1110 1100 1000 1010 1101 0010";
            gmp_lib.mpq_init(z);

            // Add rationals, and assert result.
            gmp_lib.mpq_add(z, x, y);
            Assert.IsTrue(gmp_lib.mpq_cmp(z, result) == 0);

            // Release allocated memory for rationals.
            gmp_lib.mpq_clears(x, y, z, result, null);
        }

        [Test]
        public void mpf_t_test()
        {
            // Set default precision to 84 bits.
            gmp_lib.mpf_set_default_prec(32U);

            // Create new multiple-precision floating-point numbers.
            mpf_t x = "B-16 7048 860D DF79@0";
            mpf_t y = "123 456 789 012 345e0";
            mpf_t z = new mpf_t();
            mpf_t result = "246 913 578 024 690e0";
            gmp_lib.mpf_init(z);

            // Add floating-point numbers, and assert result.
            gmp_lib.mpf_add(z, x, y);
            Assert.IsTrue(gmp_lib.mpf_cmp(z, result) == 0);

            // Release allocated memory for floating-point numbers.
            gmp_lib.mpf_clears(x, y, z, result, null);
        }

        [Test]
        public void va_list()
        {
            object[] args;
            va_list va_args;

            args = new object[] { new ptr<Char>('A') };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<Char>)args[0]).Value == 'A');

            args = new object[] { new ptr<Byte>(Byte.MinValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<Byte>)args[0]).Value == Byte.MinValue);

            args = new object[] { new ptr<SByte>(SByte.MaxValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<SByte>)args[0]).Value == SByte.MaxValue);

            args = new object[] { new ptr<Int16>(Int16.MinValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<Int16>)args[0]).Value == Int16.MinValue);

            args = new object[] { new ptr<UInt16>(UInt16.MaxValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<UInt16>)args[0]).Value == UInt16.MaxValue);

            args = new object[] { new ptr<Int32>(Int32.MinValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<Int32>)args[0]).Value == Int32.MinValue);

            args = new object[] { new ptr<UInt32>(UInt32.MaxValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<UInt32>)args[0]).Value == UInt32.MaxValue);

            args = new object[] { new ptr<Int64>(Int64.MinValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<Int64>)args[0]).Value == Int64.MinValue);

            args = new object[] { new ptr<UInt64>(UInt64.MaxValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<UInt64>)args[0]).Value == UInt64.MaxValue);

            args = new object[] { new ptr<mp_bitcnt_t>(UInt32.MaxValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<mp_bitcnt_t>)args[0]).Value == UInt32.MaxValue);

            args = new object[] { new ptr<mp_size_t>(Int32.MinValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<mp_size_t>)args[0]).Value == Int32.MinValue);

            args = new object[] { new ptr<Single>(Single.MinValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<Single>)args[0]).Value == Single.MinValue);

            args = new object[] { new ptr<Double>(Double.MaxValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<Double>)args[0]).Value == Double.MaxValue);

            args = new object[] { new ptr<mp_limb_t>(IntPtr.Size == 4 ? UInt32.MaxValue : UInt64.MaxValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<mp_limb_t>)args[0]).Value == (IntPtr.Size == 4 ? UInt32.MaxValue : UInt64.MaxValue));

            args = new object[] { new ptr<size_t>(IntPtr.Size == 4 ? UInt32.MaxValue : UInt64.MaxValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<size_t>)args[0]).Value == (IntPtr.Size == 4 ? UInt32.MaxValue : UInt64.MaxValue));

            args = new object[] { new ptr<mp_exp_t>(Int32.MinValue) };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((ptr<mp_exp_t>)args[0]).Value == Int32.MinValue);

            args = new object[] { new StringBuilder("ABCDEFGHIJ") };
            va_args = new va_list(args);
            va_args.RetrieveArgumentValues();
            Assert.IsTrue(((StringBuilder)args[0]).ToString() == "ABCDEFGHIJ");
        }

        #endregion

        #region "Global variables."

        [Test]
        public void mp_bits_per_limb()
        {
            int bitsPerLimb = gmp_lib.mp_bits_per_limb;
            Assert.AreEqual(bitsPerLimb, IntPtr.Size * 8);
        }

        [Test]
        public void mp_bytes_per_limb()
        {
            mp_size_t bytesPerLimb = gmp_lib.mp_bytes_per_limb;
            Assert.AreEqual(bytesPerLimb, (mp_size_t)IntPtr.Size);
        }

        [Test]
        public void mp_uint_per_limb()
        {
            mp_size_t uintsPerLimb = gmp_lib.mp_uint_per_limb;
            Assert.AreEqual(uintsPerLimb, (mp_size_t)(IntPtr.Size / 4));
        }

        [Test]
        public void gmp_errno()
        {
            int errno = gmp_lib.gmp_errno;
            Assert.AreEqual(errno, 0);
            gmp_lib.gmp_errno = 100;
            errno = gmp_lib.gmp_errno;
            Assert.AreEqual(errno, 100);
        }

        [Test]
        public void gmp_version()
        {
            string version = gmp_lib.gmp_version;
            Assert.AreEqual(version, "6.2.1");
        }

        #endregion

        #region "Memory allocation functions."

        [Test]
        [Category("Memory allocation functions")]
        public void mp_get_memory_functions()
        {
            allocate_function allocate;
            reallocate_function reallocate;
            free_function free;

            // Retrieve the GMP memory allocation functions.
            allocate = null; reallocate = null; free = null;
            gmp_lib.mp_get_memory_functions(ref allocate, ref reallocate, ref free);
            Assert.IsTrue(allocate != null && reallocate != null && free != null);

            // Allocate and free memory.
            void_ptr p = allocate(100);
            free(p, 100);
        }

        [Test(Description = "This test need to run first based on gmp spec.")]
        [Order(1)]
        public void mp_set_memory_functions()
        {
            // Retrieve GMP default memory allocation functions.
            allocate_function default_allocate = null;
            reallocate_function default_reallocate = null;
            free_function default_free = null;
            gmp_lib.mp_get_memory_functions(ref default_allocate, ref default_reallocate, ref default_free);

            // Create and set new memory allocation functions that count the number of times they are called.
            int counter = 0;
            allocate_function new_allocate = (size_t alloc_size) => { counter++; return default_allocate(alloc_size); };
            reallocate_function new_reallocate = (void_ptr ptr, size_t old_size, size_t new_size) => { counter++; return default_reallocate(ptr, old_size, new_size); };
            free_function new_free = (void_ptr ptr, size_t size) => { counter++; default_free(ptr, size); };
            gmp_lib.mp_set_memory_functions(new_allocate, new_reallocate, new_free);

            // Retrieve GMP memory allocation functions.
            allocate_function allocate = null;
            reallocate_function reallocate = null;
            free_function free = null;
            gmp_lib.mp_get_memory_functions(ref allocate, ref reallocate, ref free);

            // Call memory function and assert calls count.
            void_ptr p = allocate(10);
            Assert.IsTrue(counter == 1);

            reallocate(p, 10, 20);
            Assert.IsTrue(counter == 2);

            free(p, 20);
            Assert.IsTrue(counter == 3);

            // Restore default memory allocation functions.
            gmp_lib.mp_set_memory_functions(null, null, null);
        }

        #endregion

        #region "Random number routines."

        [Test]
        public void gmp_randinit_default()
        {
            // Create new random number generator state.
            gmp_randstate_t state = new gmp_randstate_t();

            // Initialize state with default random number generator algorithm.
            gmp_lib.gmp_randinit_default(state);

            // Free all memory occupied by state.
            gmp_lib.gmp_randclear(state);
        }

        [Test]
        public void gmp_randinit_lc_2exp()
        {
            // Create new random number generator state.
            gmp_randstate_t state = new gmp_randstate_t();

            // Initialize state with a linear congruential random number generator algorithm.
            mpz_t a = new mpz_t();
            gmp_lib.mpz_init_set_ui(a, 100000U);
            gmp_lib.gmp_randinit_lc_2exp(state, a, 13, 300);

            // Free all memory occupied by state and a.
            gmp_lib.gmp_randclear(state);
            gmp_lib.mpz_clear(a);
        }

        [Test]
        public void gmp_randinit_lc_2exp_size()
        {
            // Create new random number generator state.
            gmp_randstate_t state = new gmp_randstate_t();

            // Initialize state with a linear congruential random number generator algorithm.
            gmp_lib.gmp_randinit_lc_2exp_size(state, 30);

            // Free all memory occupied by state.
            gmp_lib.gmp_randclear(state);
        }

        [Test]
        public void gmp_randinit_mt()
        {
            // Create new random number generator state.
            gmp_randstate_t state = new gmp_randstate_t();

            // Initialize state with Mersenne Twister random number generator algorithm.
            gmp_lib.gmp_randinit_mt(state);

            // Free all memory occupied by state.
            gmp_lib.gmp_randclear(state);
        }

        [Test]
        public void gmp_randinit_set()
        {
            // Create new random number generator state, and initialize state with the Mersenne Twister algorithm.
            gmp_randstate_t op = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(op);

            // Create new random number generator state, and initialize it with the state op.
            gmp_randstate_t rop = new gmp_randstate_t();
            gmp_lib.gmp_randinit_set(rop, op);

            // Free all memory occupied by op and rop.
            gmp_lib.gmp_randclear(op);
            gmp_lib.gmp_randclear(rop);
        }

        [Test]
        public void gmp_randseed()
        {
            // Create new random number generator state, and initialize state with the Mersenne Twister algorithm.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(state);

            // Seed random number generator.
            mpz_t seed = new mpz_t();
            gmp_lib.mpz_init_set_ui(seed, 100000U);
            gmp_lib.gmp_randseed(state, seed);

            // Free all memory occupied by state and seed.
            gmp_lib.gmp_randclear(state);
            gmp_lib.mpz_clear(seed);
        }

        [Test]
        public void gmp_randseed_ui()
        {
            // Create new random number generator state, and initialize state with the Mersenne Twister algorithm.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(state);

            // Seed random number generator.
            gmp_lib.gmp_randseed_ui(state, 100000U);

            // Free all memory occupied by state.
            gmp_lib.gmp_randclear(state);
        }

        [Test]
        public void gmp_urandomb_ui()
        {
            // Create, initialize, and seed a new random number generator.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(state);
            gmp_lib.gmp_randseed_ui(state, 100000U);

            // Generate a random integer in the range [0, 2^8-1].
            uint rand = gmp_lib.gmp_urandomb_ui(state, 8);

            // Free all memory occupied by state.
            gmp_lib.gmp_randclear(state);
        }

        [Test]
        public void gmp_urandomm_ui()
        {
            // Create, initialize, and seed a new random number generator.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(state);
            gmp_lib.gmp_randseed_ui(state, 1000U);

            // Generate a random integer in the range [0, 8-1].
            uint rand = gmp_lib.gmp_urandomm_ui(state, 8);

            // Free all memory occupied by state.
            gmp_lib.gmp_randclear(state);
        }

        #endregion

        #region "Formatted output routines."

        [Test]
        public void gmp_asprintf()
        {
            // Create pointer to unmanaged character string pointer.
            ptr<char_ptr> str = new ptr<char_ptr>();

            mpz_t z = "123456";
            mpq_t q = "123/456";
            mpf_t f = "12345e6";
            mp_limb_t m = 123456;

            // Print to newly allocated unmanaged memory string.
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Zd - %QX - %Fa - %Mo", z, q, f, m) == 42);
            Assert.IsTrue(str.Value.ToString() == "123456 - 7B/1C8 - 0x2.dfd1c04p+32 - 361100");

            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Zd", z) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Zi", z) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%ZX", z) == 5);
            Assert.IsTrue(str.Value.ToString() == "1E240");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Zo", z) == 6);
            Assert.IsTrue(str.Value.ToString() == "361100");
            gmp_lib.free(str.Value);
            gmp_lib.mpz_clear(z);

            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Qd", q) == 7);
            Assert.IsTrue(str.Value.ToString() == "123/456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Qi", q) == 7);
            Assert.IsTrue(str.Value.ToString() == "123/456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%QX", q) == 6);
            Assert.IsTrue(str.Value.ToString() == "7B/1C8");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Qo", q) == 7);
            Assert.IsTrue(str.Value.ToString() == "173/710");
            gmp_lib.free(str.Value);
            gmp_lib.mpq_clear(q);

            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Fe", f) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.234500e+10");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Ff", f) == 18);
            Assert.IsTrue(str.Value.ToString() == "12345000000.000000");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Fg", f) == 10);
            Assert.IsTrue(str.Value.ToString() == "1.2345e+10");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Fa", f) == 15);
            Assert.IsTrue(str.Value.ToString() == "0x2.dfd1c04p+32");
            gmp_lib.free(str.Value);
            gmp_lib.mpf_clear(f);

            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Md", m) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Mi", m) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%MX", m) == 5);
            Assert.IsTrue(str.Value.ToString() == "1E240");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Mo", m) == 6);
            Assert.IsTrue(str.Value.ToString() == "361100");
            gmp_lib.free(str.Value);

            mp_ptr n = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Nd", n, n.Size) == 11);
            Assert.IsTrue(str.Value.ToString() == "11111111111");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Ni", n, n.Size) == 11);
            Assert.IsTrue(str.Value.ToString() == "11111111111");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%NX", n, n.Size) == 9);
            Assert.IsTrue(str.Value.ToString() == "2964619C7");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%No", n, n.Size) == 12);
            Assert.IsTrue(str.Value.ToString() == "122621414707");
            gmp_lib.free(str.Value);

            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%hd", (short)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%hhd", (byte)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%hhc", 'A') == 1);
            Assert.IsTrue(str.Value.ToString() == "A");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%ld", (Int32)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%lld", (Int64)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);

            // Instead of %z, use %M.
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%Md", (size_t)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%d", (mp_bitcnt_t)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%d", (mp_size_t)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%d", (mp_exp_t)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);

            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%f", (Double)1.0) == 8);
            Assert.IsTrue(str.Value.ToString() == "1.000000");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%f", (Single)1.0) == 8);
            Assert.IsTrue(str.Value.ToString() == "1.000000");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%e", (Double)1.0) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.000000e+00");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%e", (Single)1.0) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.000000e+00");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%g", (Double)1.0) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%g", (Single)1.0) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);

            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%E", (Double)1.0) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.000000E+00");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%E", (Single)1.0) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.000000E+00");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%G", (Double)1.0) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%G", (Single)1.0) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);

            //Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%a", (Double)1.0) == 13);
            //Assert.IsTrue(str.Value.ToString() == "0x1.000000p+0");
            //gmp_lib.free(str.Value);
            //Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%a", (Single)1.0) == 13);
            //Assert.IsTrue(str.Value.ToString() == "0x1.000004p+0");
            //gmp_lib.free(str.Value);

            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%s", "Hello World!") == 12);
            Assert.IsTrue(str.Value.ToString() == "Hello World!");
            gmp_lib.free(str.Value);

            ptr<int> p = new ptr<int>(12);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "123456%n", p) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            Assert.IsTrue(p.Value == 6);
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%p", p) == 2 * IntPtr.Size);
            gmp_lib.free(str.Value);
        }

        [Test]
        public void gmp_vasprintf()
        {
            ptr<char_ptr> str = new ptr<char_ptr>();

            mpz_t z = "123456";
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Zd", z) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Zi", z) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%ZX", z) == 5);
            Assert.IsTrue(str.Value.ToString() == "1E240");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Zo", z) == 6);
            Assert.IsTrue(str.Value.ToString() == "361100");
            gmp_lib.free(str.Value);
            gmp_lib.mpz_clear(z);

            mpq_t q = "123/456";
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Qd", q) == 7);
            Assert.IsTrue(str.Value.ToString() == "123/456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Qi", q) == 7);
            Assert.IsTrue(str.Value.ToString() == "123/456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%QX", q) == 6);
            Assert.IsTrue(str.Value.ToString() == "7B/1C8");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Qo", q) == 7);
            Assert.IsTrue(str.Value.ToString() == "173/710");
            gmp_lib.free(str.Value);
            gmp_lib.mpq_clear(q);

            mpf_t f = "12345e6";
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Fe", f) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.234500e+10");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Ff", f) == 18);
            Assert.IsTrue(str.Value.ToString() == "12345000000.000000");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Fg", f) == 10);
            Assert.IsTrue(str.Value.ToString() == "1.2345e+10");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Fa", f) == 15);
            Assert.IsTrue(str.Value.ToString() == "0x2.dfd1c04p+32");
            gmp_lib.free(str.Value);
            gmp_lib.mpf_clear(f);

            mp_limb_t m = 123456;
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Md", m) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Mi", m) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%MX", m) == 5);
            Assert.IsTrue(str.Value.ToString() == "1E240");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Mo", m) == 6);
            Assert.IsTrue(str.Value.ToString() == "361100");
            gmp_lib.free(str.Value);

            mp_ptr n = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Nd", n, n.Size) == 11);
            Assert.IsTrue(str.Value.ToString() == "11111111111");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Ni", n, n.Size) == 11);
            Assert.IsTrue(str.Value.ToString() == "11111111111");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%NX", n, n.Size) == 9);
            Assert.IsTrue(str.Value.ToString() == "2964619C7");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%No", n, n.Size) == 12);
            Assert.IsTrue(str.Value.ToString() == "122621414707");
            gmp_lib.free(str.Value);

            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%hd", (short)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%hhd", (byte)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%hhc", 'A') == 1);
            Assert.IsTrue(str.Value.ToString() == "A");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%ld", (Int32)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%lld", (Int64)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);

            // Instead of %z, use %M.
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%Md", (size_t)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%d", (mp_bitcnt_t)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%d", (mp_size_t)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%d", (mp_exp_t)1) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);

            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%f", (Double)1.0) == 8);
            Assert.IsTrue(str.Value.ToString() == "1.000000");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%f", (Single)1.0) == 8);
            Assert.IsTrue(str.Value.ToString() == "1.000000");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%e", (Double)1.0) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.000000e+00");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%e", (Single)1.0) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.000000e+00");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%g", (Double)1.0) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%g", (Single)1.0) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);

            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%E", (Double)1.0) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.000000E+00");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%E", (Single)1.0) == 12);
            Assert.IsTrue(str.Value.ToString() == "1.000000E+00");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%G", (Double)1.0) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%G", (Single)1.0) == 1);
            Assert.IsTrue(str.Value.ToString() == "1");
            gmp_lib.free(str.Value);

            //Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%a", (Double)1.0) == 13);
            //Assert.IsTrue(str.Value.ToString() == "0x1.000000p+0");
            //gmp_lib.free(str.Value);
            //Assert.IsTrue(gmp_lib.gmp_asprintf(str, "%a", (Single)1.0) == 13);
            //Assert.IsTrue(str.Value.ToString() == "0x1.000004p+0");
            //gmp_lib.free(str.Value);

            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%s", "Hello World!") == 12);
            Assert.IsTrue(str.Value.ToString() == "Hello World!");
            gmp_lib.free(str.Value);

            ptr<int> p = new ptr<int>(12);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "123456%n", p) == 6);
            Assert.IsTrue(str.Value.ToString() == "123456");
            Assert.IsTrue(p.Value == 6);
            gmp_lib.free(str.Value);
            Assert.IsTrue(gmp_lib.gmp_vasprintf(str, "%p", p) == 2 * IntPtr.Size);
            gmp_lib.free(str.Value);
        }

        [Test]
        public void gmp_fprintf()
        {
            // Create unique file pathname and a file pointer.
            string pathname = System.IO.Path.GetTempFileName();
            ptr<FILE> stream = new ptr<FILE>();

            mpz_t z = "123456";
            mpq_t q = "123/456";
            mpf_t f = "12345e6";
            mp_limb_t m = 123456;

            // Open file stream and print to it.
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Zd - %QX - %Fa - %Mo", z, q, f, m) == 42);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456 - 7B/1C8 - 0x2.dfd1c04p+32 - 361100");
        
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Zd", z) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Zi", z) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%ZX", z) == 5);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1E240");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Zo", z) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "361100");
            gmp_lib.mpz_clear(z);

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Qd", q) == 7);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123/456");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Qi", q) == 7);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123/456");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%QX", q) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "7B/1C8");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Qo", q) == 7);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "173/710");
            gmp_lib.mpq_clear(q);

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Fe", f) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.234500e+10");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Ff", f) == 18);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "12345000000.000000");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Fg", f) == 10);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.2345e+10");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Fa", f) == 15);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "0x2.dfd1c04p+32");
            gmp_lib.mpf_clear(f);

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Md", m) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Mi", m) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%MX", m) == 5);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1E240");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Mo", m) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "361100");

            mp_ptr n = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Nd", n, n.Size) == 11);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "11111111111");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Ni", n, n.Size) == 11);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "11111111111");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%NX", n, n.Size) == 9);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "2964619C7");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%No", n, n.Size) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "122621414707");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%hd", (short)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%hhd", (byte)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%hhc", 'A') == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "A");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%ld", (Int32)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%lld", (Int64)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            // Instead of %z, use %M.
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%Md", (size_t)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%d", (mp_bitcnt_t)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%d", (mp_size_t)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%d", (mp_exp_t)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%f", (Double)1.0) == 8);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%f", (Single)1.0) == 8);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%e", (Double)1.0) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000e+00");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%e", (Single)1.0) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000e+00");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%g", (Double)1.0) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%g", (Single)1.0) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%E", (Double)1.0) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000E+00");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%E", (Single)1.0) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000E+00");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%G", (Double)1.0) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%G", (Single)1.0) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%a", (Double)1.0) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "0x8p-3");
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%a", (Single)1.0) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "0x8p-3");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%s", "Hello World!") == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "Hello World!");

            ptr<int> p = new ptr<int>(12);
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "123456%n", p) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");

            Assert.IsTrue(p.Value == 6);
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_fprintf(stream, "%p", p) == 2 * IntPtr.Size);
            fclose(stream.Value.Value);
        }

        [Test]
        public void gmp_vfprintf()
        {
            string pathname = System.IO.Path.GetTempFileName();
            ptr<FILE> stream = new ptr<FILE>();

            mpz_t z = "123456";
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Zd", z) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Zi", z) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%ZX", z) == 5);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1E240");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Zo", z) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "361100");
            gmp_lib.mpz_clear(z);

            mpq_t q = "123/456";
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Qd", q) == 7);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123/456");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Qi", q) == 7);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123/456");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%QX", q) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "7B/1C8");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Qo", q) == 7);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "173/710");
            gmp_lib.mpq_clear(q);

            mpf_t f = "12345e6";
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Fe", f) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.234500e+10");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Ff", f) == 18);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "12345000000.000000");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Fg", f) == 10);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.2345e+10");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Fa", f) == 15);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "0x2.dfd1c04p+32");
            gmp_lib.mpf_clear(f);

            mp_limb_t m = 123456;
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Md", m) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Mi", m) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%MX", m) == 5);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1E240");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Mo", m) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "361100");

            mp_ptr n = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Nd", n, n.Size) == 11);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "11111111111");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Ni", n, n.Size) == 11);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "11111111111");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%NX", n, n.Size) == 9);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "2964619C7");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%No", n, n.Size) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "122621414707");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%hd", (short)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%hhd", (byte)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%hhc", 'A') == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "A");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%ld", (Int32)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%lld", (Int64)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            // Instead of %z, use %M.
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%Md", (size_t)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%d", (mp_bitcnt_t)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%d", (mp_size_t)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%d", (mp_exp_t)1) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%f", (Double)1.0) == 8);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%f", (Single)1.0) == 8);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%e", (Double)1.0) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000e+00");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%e", (Single)1.0) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000e+00");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%g", (Double)1.0) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%g", (Single)1.0) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%E", (Double)1.0) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000E+00");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%E", (Single)1.0) == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1.000000E+00");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%G", (Double)1.0) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%G", (Single)1.0) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "1");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%a", (Double)1.0) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "0x8p-3");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%a", (Single)1.0) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "0x8p-3");

            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%s", "Hello World!") == 12);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "Hello World!");

            ptr<int> p = new ptr<int>(12);
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "123456%n", p) == 6);
            fclose(stream.Value.Value);
            Assert.IsTrue(System.IO.File.ReadAllText(pathname) == "123456");

            Assert.IsTrue(p.Value == 6);
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.gmp_vfprintf(stream, "%p", p) == 2 * IntPtr.Size);
            fclose(stream.Value.Value);
        }

        [Test]
        public void gmp_snprintf()
        {
            // Allocate unmanaged string with 50 characters.
            char_ptr str = new char_ptr(".................................................");

            mpz_t z = "123456";
            mpq_t q = "123/456";
            mpf_t f = "12345e6";
            mp_limb_t m = 123456;

            // Print to string.
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 50, "%Zd - %QX - %Fa - %Mo", z, q, f, m) == 42);
            Assert.IsTrue(str.ToString() == "123456 - 7B/1C8 - 0x2.dfd1c04p+32 - 361100");
        
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Zd", z) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Zi", z) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%ZX", z) == 5);
            Assert.IsTrue(str.ToString() == "1E240");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Zo", z) == 6);
            Assert.IsTrue(str.ToString() == "361100");
            gmp_lib.mpz_clear(z);

            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Qd", q) == 7);
            Assert.IsTrue(str.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Qi", q) == 7);
            Assert.IsTrue(str.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%QX", q) == 6);
            Assert.IsTrue(str.ToString() == "7B/1C8");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Qo", q) == 7);
            Assert.IsTrue(str.ToString() == "173/710");
            gmp_lib.mpq_clear(q);

            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Fe", f) == 12);
            Assert.IsTrue(str.ToString() == "1.234500e+10");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Ff", f) == 18);
            Assert.IsTrue(str.ToString() == "12345000000.000000");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Fg", f) == 10);
            Assert.IsTrue(str.ToString() == "1.2345e+10");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Fa", f) == 15);
            Assert.IsTrue(str.ToString() == "0x2.dfd1c04p+32");
            gmp_lib.mpf_clear(f);

            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Md", m) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Mi", m) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%MX", m) == 5);
            Assert.IsTrue(str.ToString() == "1E240");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Mo", m) == 6);
            Assert.IsTrue(str.ToString() == "361100");

            mp_ptr n = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Nd", n, n.Size) == 11);
            Assert.IsTrue(str.ToString() == "11111111111");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Ni", n, n.Size) == 11);
            Assert.IsTrue(str.ToString() == "11111111111");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%NX", n, n.Size) == 9);
            Assert.IsTrue(str.ToString() == "2964619C7");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%No", n, n.Size) == 12);
            Assert.IsTrue(str.ToString() == "122621414707");

            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%hd", (short)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%hhd", (byte)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%hhc", 'A') == 1);
            Assert.IsTrue(str.ToString() == "A");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%ld", (Int32)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%lld", (Int64)1) == 1);
            Assert.IsTrue(str.ToString() == "1");

            // Instead of %z, use %M.
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%Md", (size_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%d", (mp_bitcnt_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%d", (mp_size_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%d", (mp_exp_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%f", (Double)1.0) == 8);
            Assert.IsTrue(str.ToString() == "1.000000");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%f", (Single)1.0) == 8);
            Assert.IsTrue(str.ToString() == "1.000000");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%e", (Double)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000e+00");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%e", (Single)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000e+00");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%g", (Double)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%g", (Single)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%E", (Double)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000E+00");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%E", (Single)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000E+00");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%G", (Double)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%G", (Single)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");

            //Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%a", (Double)1.0) == 13);
            //Assert.IsTrue(str.ToString() == "0x1.000000p+0");
            //Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%a", (Single)1.0) == 13);
            //Assert.IsTrue(str.ToString() == "0x1.000000p+0");

            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%s", "Hello World!") == 12);
            Assert.IsTrue(str.ToString() == "Hello World!");

            ptr<int> p = new ptr<int>(12);
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "123456%n", p) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(p.Value == 6);
            Assert.IsTrue(gmp_lib.gmp_snprintf(str, 41, "%p", p) == 2 * IntPtr.Size);

            gmp_lib.free(str);
        }

        [Test]
        public void gmp_vsnprintf()
        {
            char_ptr str = new char_ptr(".........................................");

            mpz_t z = "123456";
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Zd", z) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Zi", z) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%ZX", z) == 5);
            Assert.IsTrue(str.ToString() == "1E240");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Zo", z) == 6);
            Assert.IsTrue(str.ToString() == "361100");
            gmp_lib.mpz_clear(z);

            mpq_t q = "123/456";
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Qd", q) == 7);
            Assert.IsTrue(str.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Qi", q) == 7);
            Assert.IsTrue(str.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%QX", q) == 6);
            Assert.IsTrue(str.ToString() == "7B/1C8");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Qo", q) == 7);
            Assert.IsTrue(str.ToString() == "173/710");
            gmp_lib.mpq_clear(q);

            mpf_t f = "12345e6";
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Fe", f) == 12);
            Assert.IsTrue(str.ToString() == "1.234500e+10");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Ff", f) == 18);
            Assert.IsTrue(str.ToString() == "12345000000.000000");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Fg", f) == 10);
            Assert.IsTrue(str.ToString() == "1.2345e+10");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Fa", f) == 15);
            Assert.IsTrue(str.ToString() == "0x2.dfd1c04p+32");
            gmp_lib.mpf_clear(f);

            mp_limb_t m = 123456;
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Md", m) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Mi", m) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%MX", m) == 5);
            Assert.IsTrue(str.ToString() == "1E240");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Mo", m) == 6);
            Assert.IsTrue(str.ToString() == "361100");

            mp_ptr n = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Nd", n, n.Size) == 11);
            Assert.IsTrue(str.ToString() == "11111111111");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Ni", n, n.Size) == 11);
            Assert.IsTrue(str.ToString() == "11111111111");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%NX", n, n.Size) == 9);
            Assert.IsTrue(str.ToString() == "2964619C7");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%No", n, n.Size) == 12);
            Assert.IsTrue(str.ToString() == "122621414707");

            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%hd", (short)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%hhd", (byte)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%hhc", 'A') == 1);
            Assert.IsTrue(str.ToString() == "A");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%ld", (Int32)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%lld", (Int64)1) == 1);
            Assert.IsTrue(str.ToString() == "1");

            // Instead of %z, use %M.
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%Md", (size_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%d", (mp_bitcnt_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%d", (mp_size_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%d", (mp_exp_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%f", (Double)1.0) == 8);
            Assert.IsTrue(str.ToString() == "1.000000");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%f", (Single)1.0) == 8);
            Assert.IsTrue(str.ToString() == "1.000000");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%e", (Double)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000e+00");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%e", (Single)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000e+00");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%g", (Double)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%g", (Single)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%E", (Double)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000E+00");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%E", (Single)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000E+00");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%G", (Double)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%G", (Single)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");

            //Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%a", (Double)1.0) == 13);
            //Assert.IsTrue(str.ToString() == "0x1.000000p+0");
            //Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%a", (Single)1.0) == 13);
            //Assert.IsTrue(str.ToString() == "0x1.000000p+0");

            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%s", "Hello World!") == 12);
            Assert.IsTrue(str.ToString() == "Hello World!");

            ptr<int> p = new ptr<int>(12);
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "123456%n", p) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(p.Value == 6);
            Assert.IsTrue(gmp_lib.gmp_vsnprintf(str, 41, "%p", p) == 2 * IntPtr.Size);

            gmp_lib.free(str);
        }

        [Test]
        public void gmp_sprintf()
        {
            // Allocate unmanaged string with 50 characters.
            char_ptr str = new char_ptr(".................................................");

            mpz_t z = "123456";
            mpq_t q = "123/456";
            mpf_t f = "12345e6";
            mp_limb_t m = 123456;

            // Print to string.
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Zd - %QX - %Fa - %Mo", z, q, f, m) == 42);
            Assert.IsTrue(str.ToString() == "123456 - 7B/1C8 - 0x2.dfd1c04p+32 - 361100");

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Zd", z) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Zi", z) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%ZX", z) == 5);
            Assert.IsTrue(str.ToString() == "1E240");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Zo", z) == 6);
            Assert.IsTrue(str.ToString() == "361100");
            gmp_lib.mpz_clear(z);

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Qd", q) == 7);
            Assert.IsTrue(str.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Qi", q) == 7);
            Assert.IsTrue(str.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%QX", q) == 6);
            Assert.IsTrue(str.ToString() == "7B/1C8");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Qo", q) == 7);
            Assert.IsTrue(str.ToString() == "173/710");
            gmp_lib.mpq_clear(q);

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Fe", f) == 12);
            Assert.IsTrue(str.ToString() == "1.234500e+10");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Ff", f) == 18);
            Assert.IsTrue(str.ToString() == "12345000000.000000");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Fg", f) == 10);
            Assert.IsTrue(str.ToString() == "1.2345e+10");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Fa", f) == 15);
            Assert.IsTrue(str.ToString() == "0x2.dfd1c04p+32");
            gmp_lib.mpf_clear(f);

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Md", m) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Mi", m) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%MX", m) == 5);
            Assert.IsTrue(str.ToString() == "1E240");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Mo", m) == 6);
            Assert.IsTrue(str.ToString() == "361100");

            mp_ptr n = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Nd", n, n.Size) == 11);
            Assert.IsTrue(str.ToString() == "11111111111");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Ni", n, n.Size) == 11);
            Assert.IsTrue(str.ToString() == "11111111111");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%NX", n, n.Size) == 9);
            Assert.IsTrue(str.ToString() == "2964619C7");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%No", n, n.Size) == 12);
            Assert.IsTrue(str.ToString() == "122621414707");

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%hd", (short)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%hhd", (byte)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%hhc", 'A') == 1);
            Assert.IsTrue(str.ToString() == "A");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%ld", (Int32)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%lld", (Int64)1) == 1);
            Assert.IsTrue(str.ToString() == "1");

            // Instead of %z, use %M.
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%Md", (size_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%d", (mp_bitcnt_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%d", (mp_size_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%d", (mp_exp_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%f", (Double)1.0) == 8);
            Assert.IsTrue(str.ToString() == "1.000000");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%f", (Single)1.0) == 8);
            Assert.IsTrue(str.ToString() == "1.000000");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%e", (Double)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000e+00");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%e", (Single)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000e+00");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%g", (Double)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%g", (Single)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%E", (Double)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000E+00");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%E", (Single)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000E+00");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%G", (Double)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%G", (Single)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%a", (Double)1.0) == 6);
            Assert.IsTrue(str.ToString() == "0x8p-3");
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%a", (Single)1.0) == 6);
            Assert.IsTrue(str.ToString() == "0x8p-3");

            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%s", "Hello World!") == 12);
            Assert.IsTrue(str.ToString() == "Hello World!");

            ptr<int> p = new ptr<int>(12);
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "123456%n", p) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(p.Value == 6);
            Assert.IsTrue(gmp_lib.gmp_sprintf(str, "%p", p) == 2 * IntPtr.Size);

            gmp_lib.free(str);
        }

        [Test]
        public void gmp_vsprintf()
        {
            // Create string.
            char_ptr str = new char_ptr(".........................................");

            mpz_t z = "123456";
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Zd", z) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Zi", z) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%ZX", z) == 5);
            Assert.IsTrue(str.ToString() == "1E240");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Zo", z) == 6);
            Assert.IsTrue(str.ToString() == "361100");
            gmp_lib.mpz_clear(z);

            mpq_t q = "123/456";
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Qd", q) == 7);
            Assert.IsTrue(str.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Qi", q) == 7);
            Assert.IsTrue(str.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%QX", q) == 6);
            Assert.IsTrue(str.ToString() == "7B/1C8");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Qo", q) == 7);
            Assert.IsTrue(str.ToString() == "173/710");
            gmp_lib.mpq_clear(q);

            mpf_t f = "12345e6";
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Fe", f) == 12);
            Assert.IsTrue(str.ToString() == "1.234500e+10");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Ff", f) == 18);
            Assert.IsTrue(str.ToString() == "12345000000.000000");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Fg", f) == 10);
            Assert.IsTrue(str.ToString() == "1.2345e+10");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Fa", f) == 15);
            Assert.IsTrue(str.ToString() == "0x2.dfd1c04p+32");
            gmp_lib.mpf_clear(f);

            mp_limb_t m = 123456;
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Md", m) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Mi", m) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%MX", m) == 5);
            Assert.IsTrue(str.ToString() == "1E240");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Mo", m) == 6);
            Assert.IsTrue(str.ToString() == "361100");

            mp_ptr n = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Nd", n, n.Size) == 11);
            Assert.IsTrue(str.ToString() == "11111111111");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Ni", n, n.Size) == 11);
            Assert.IsTrue(str.ToString() == "11111111111");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%NX", n, n.Size) == 9);
            Assert.IsTrue(str.ToString() == "2964619C7");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%No", n, n.Size) == 12);
            Assert.IsTrue(str.ToString() == "122621414707");

            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%hd", (short)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%hhd", (byte)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%hhc", 'A') == 1);
            Assert.IsTrue(str.ToString() == "A");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%ld", (Int32)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%lld", (Int64)1) == 1);
            Assert.IsTrue(str.ToString() == "1");

            // Instead of %z, use %M.
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%Md", (size_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%d", (mp_bitcnt_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%d", (mp_size_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%d", (mp_exp_t)1) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%f", (Double)1.0) == 8);
            Assert.IsTrue(str.ToString() == "1.000000");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%f", (Single)1.0) == 8);
            Assert.IsTrue(str.ToString() == "1.000000");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%e", (Double)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000e+00");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%e", (Single)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000e+00");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%g", (Double)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%g", (Single)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%E", (Double)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000E+00");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%E", (Single)1.0) == 12);
            Assert.IsTrue(str.ToString() == "1.000000E+00");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%G", (Double)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%G", (Single)1.0) == 1);
            Assert.IsTrue(str.ToString() == "1");

            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%a", (Double)1.0) == 6);
            Assert.IsTrue(str.ToString() == "0x8p-3");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%a", (Single)1.0) == 6);
            Assert.IsTrue(str.ToString() == "0x8p-3");

            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%s", "Hello World!") == 12);
            Assert.IsTrue(str.ToString() == "Hello World!");

            ptr<int> p = new ptr<int>(12);
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "123456%n", p) == 6);
            Assert.IsTrue(str.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_vsprintf(str, "%p", p) == 2 * IntPtr.Size);
            //Assert.IsTrue(str.ToString() == p.ToIntPtr().ToString("X0" + (2 * IntPtr.Size).ToString()));

            // Free allocated unmanaged memory.
            gmp_lib.free(str);
        }

        #endregion

        #region "Formatted input routines."

        [Test]
        public void gmp_fscanf()
        {
            // Create unique filename and stream pointer.
            string pathname = System.IO.Path.GetTempFileName();
            ptr<FILE> stream = new ptr<FILE>();

            mpz_t z = "0";
            mpq_t q = "0";
            mpf_t f = "0";
            ptr<Char> c = new ptr<Char>('0');
            ptr<mp_size_t> zt = new ptr<mp_size_t>(0);
            ptr<Double> dbl = new ptr<Double>(0);

            // Write string to file, and then read values from it.
            System.IO.File.WriteAllText(pathname, "123456 7B/1C8 1.234500e+10 A 10 1.000000");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Zd %QX %Fe %hhc %d %lf", z, q, f, c, zt, dbl) == 6);
            fclose(stream.Value.Value);

            // Assert values read.
            Assert.IsTrue(z.ToString() == "123456");
            Assert.IsTrue(q.ToString() == "123/456");
            Assert.IsTrue(f.ToString() == "0.12345e11");
            Assert.IsTrue(c.Value == 'A');
            Assert.IsTrue(zt.Value == 10);
            Assert.IsTrue(dbl.Value == 1.0);
        
            System.IO.File.WriteAllText(pathname, "123456");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Zd", z) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(z.ToString() == "123456");
            System.IO.File.WriteAllText(pathname, "123456");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Zi", z) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(z.ToString() == "123456");
            System.IO.File.WriteAllText(pathname, "1E240");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%ZX", z) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(z.ToString() == "123456");
            System.IO.File.WriteAllText(pathname, "361100");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, " %Zo", z) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(z.ToString() == "123456");
            gmp_lib.mpz_clear(z);

            System.IO.File.WriteAllText(pathname, "123/456");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Qd", q) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(q.ToString() == "123/456");
            System.IO.File.WriteAllText(pathname, "123/456");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Qi", q) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(q.ToString() == "123/456");
            System.IO.File.WriteAllText(pathname, "7B/1C8");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%QX", q) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(q.ToString() == "123/456");
            System.IO.File.WriteAllText(pathname, "173/710");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Qo", q) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(q.ToString() == "123/456");
            gmp_lib.mpq_clear(q);

            System.IO.File.WriteAllText(pathname, "1.234500e+10");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Fe", f) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(f.ToString() == "0.12345e11");
            System.IO.File.WriteAllText(pathname, "12345000000.000000");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Ff", f) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(f.ToString() == "0.12345e11");
            System.IO.File.WriteAllText(pathname, "1.2345e+10");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Fg", f) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(f.ToString() == "0.12345e11");
            //System.IO.File.WriteAllText(pathname, "0x2.dfd1c04p+32");
            //_wfopen_s(out stream.Value.Value, pathname, "r");
            //Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%Fa", f) == 1);
            //fclose(stream.Value.Value);
            //Assert.IsTrue(f.ToString() == "0.12345e11");
            gmp_lib.mpf_clear(f);

            ptr<short> s = new ptr<short>(0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%hd", s) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(s.Value == 1);
            ptr<byte> b = new ptr<byte>(0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%hhd", b) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(b.Value == 1);
            System.IO.File.WriteAllText(pathname, "A");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%hhc", c) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(c.Value == 'A');
            ptr<Int32> i = new ptr<Int32>(0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%ld", i) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(i.Value == 1);
            ptr<Int64> l = new ptr<Int64>(0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%lld", l) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(l.Value == 1);

            ptr<size_t> st = new ptr<size_t>(0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, IntPtr.Size == 4 ? "%d" : "%ld", st) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(st.Value == 1);
            ptr<mp_bitcnt_t> bt = new ptr<mp_bitcnt_t>(0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%d", bt) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(bt.Value == 1);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%d", zt) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(zt.Value == 1);
            ptr<mp_exp_t> et = new ptr<mp_exp_t>(0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%d", et) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(et.Value == 1);

            ptr<Single> flt = new ptr<Single>(0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%lf", dbl) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(dbl.Value == 1.0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%f", flt) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(flt.Value == 1.0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%le", dbl) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(dbl.Value == 1.0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%e", flt) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(flt.Value == 1.0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%lg", dbl) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(dbl.Value == 1.0);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%g", flt) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(flt.Value == 1.0);

            System.IO.File.WriteAllText(pathname, "1.000000e+00");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%lE", dbl) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(dbl.Value == 1);
            System.IO.File.WriteAllText(pathname, "1.000000e+00");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%E", flt) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(flt.Value == 1);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%lG", dbl) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(dbl.Value == 1);
            System.IO.File.WriteAllText(pathname, "1");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%G", flt) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(flt.Value == 1);

            //System.IO.File.WriteAllText(pathname, "0x1.000000p+0");
            //_wfopen_s(out stream.Value.Value, pathname, "r");
            //Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%a", dbl) == 1);
            //fclose(stream.Value.Value);
            //Assert.IsTrue(dbl.Value == 1);
            //System.IO.File.WriteAllText(pathname, "0x1.000000p+0");
            //_wfopen_s(out stream.Value.Value, pathname, "r");
            //Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%a", flt) == 1);
            //fclose(stream.Value.Value);
            //Assert.IsTrue(flt.Value == 1);

            System.IO.File.WriteAllText(pathname, "Hello World!");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            char_ptr str = new char_ptr("________________________");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%s", str) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue(str.ToString() == "Hello");
            gmp_lib.free(str);

            ptr<int> p = new ptr<int>(12);
            System.IO.File.WriteAllText(pathname, "123456");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "123456%n", p) == 0);
            fclose(stream.Value.Value);
            Assert.IsTrue(p.Value == 6);
            ptr<IntPtr> ptr = new ptr<IntPtr>();
            System.IO.File.WriteAllText(pathname, IntPtr.Size == 4 ? "0060F0F4" : "000000000060F0F4");
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.gmp_fscanf(stream, "%p", ptr) == 1);
            fclose(stream.Value.Value);
            Assert.IsTrue((UInt64)ptr.Value == 6353140);

            System.IO.File.Delete(pathname);
        }

        [Test]
        public void gmp_sscanf()
        {
            mpz_t z = "0";
            mpq_t q = "0";
            mpf_t f = "0";
            ptr<Char> c = new ptr<Char>('0');
            ptr<mp_size_t> zt = new ptr<mp_size_t>(0);
            ptr<Double> dbl = new ptr<Double>(0);

            Assert.IsTrue(gmp_lib.gmp_sscanf("123456 7B/1C8 1.234500e+10 A 10 1.000000", "%Zd %QX %Fe %hhc %d %lf", z, q, f, c, zt, dbl) == 6);

            Assert.IsTrue(z.ToString() == "123456");
            Assert.IsTrue(q.ToString() == "123/456");
            Assert.IsTrue(f.ToString() == "0.12345e11");
            Assert.IsTrue(c.Value == 'A');
            Assert.IsTrue(zt.Value == 10);
            Assert.IsTrue(dbl.Value == 1.0);
        
            Assert.IsTrue(gmp_lib.gmp_sscanf("123456", "%Zd", z) == 1);
            Assert.IsTrue(z.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_sscanf("123456", "%Zi", z) == 1);
            Assert.IsTrue(z.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_sscanf("1E240", "%ZX", z) == 1);
            Assert.IsTrue(z.ToString() == "123456");
            Assert.IsTrue(gmp_lib.gmp_sscanf("361100", "%Zo", z) == 1);
            Assert.IsTrue(z.ToString() == "123456");
            gmp_lib.mpz_clear(z);

            Assert.IsTrue(gmp_lib.gmp_sscanf("123/456", "%Qd", q) == 1);
            Assert.IsTrue(q.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_sscanf("123/456", "%Qi", q) == 1);
            Assert.IsTrue(q.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_sscanf("7B/1C8", "%QX", q) == 1);
            Assert.IsTrue(q.ToString() == "123/456");
            Assert.IsTrue(gmp_lib.gmp_sscanf("173/710", "%Qo", q) == 1);
            Assert.IsTrue(q.ToString() == "123/456");
            gmp_lib.mpq_clear(q);

            Assert.IsTrue(gmp_lib.gmp_sscanf("1.234500e+10", "%Fe", f) == 1);
            Assert.IsTrue(f.ToString() == "0.12345e11");
            Assert.IsTrue(gmp_lib.gmp_sscanf("12345000000.000000", "%Ff", f) == 1);
            Assert.IsTrue(f.ToString() == "0.12345e11");
            Assert.IsTrue(gmp_lib.gmp_sscanf("1.2345e+10", "%Fg", f) == 1);
            Assert.IsTrue(f.ToString() == "0.12345e11");
            //Assert.IsTrue(gmp_lib.gmp_sscanf("0x2.dfd1c04p+32", "%Fa", f) == 1);
            //Assert.IsTrue(f.ToString() == "0.12345e11");
            gmp_lib.mpf_clear(f);

            ptr<short> s = new ptr<short>(0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%hd", s) == 1);
            Assert.IsTrue(s.Value == 1);
            ptr<byte> b = new ptr<byte>(0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%hhd", b) == 1);
            Assert.IsTrue(b.Value == 1);
            Assert.IsTrue(gmp_lib.gmp_sscanf("A", "%hhc", c) == 1);
            Assert.IsTrue(c.Value == 'A');
            ptr<Int32> i = new ptr<Int32>(0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%ld", i) == 1);
            Assert.IsTrue(i.Value == 1);
            ptr<Int64> l = new ptr<Int64>(0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%lld", l) == 1);
            Assert.IsTrue(l.Value == 1);

            ptr<size_t> st = new ptr<size_t>(0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", IntPtr.Size == 4 ? "%d" : "%ld", st) == 1);
            Assert.IsTrue(st.Value == 1);
            ptr<mp_bitcnt_t> bt = new ptr<mp_bitcnt_t>(0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%d", bt) == 1);
            Assert.IsTrue(bt.Value == 1);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%d", zt) == 1);
            Assert.IsTrue(zt.Value == 1);
            ptr<mp_exp_t> et = new ptr<mp_exp_t>(0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%d", et) == 1);
            Assert.IsTrue(et.Value == 1);

            ptr<Single> flt = new ptr<Single>(0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1.000000", "%lf", dbl) == 1);
            Assert.IsTrue(dbl.Value == 1.0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1.000000", "%f", flt) == 1);
            Assert.IsTrue(flt.Value == 1.0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1.000000e+00", "%le", dbl) == 1);
            Assert.IsTrue(dbl.Value == 1.0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1.000000e+00", "%e", flt) == 1);
            Assert.IsTrue(flt.Value == 1.0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%lg", dbl) == 1);
            Assert.IsTrue(dbl.Value == 1.0);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%g", flt) == 1);
            Assert.IsTrue(flt.Value == 1.0);

            Assert.IsTrue(gmp_lib.gmp_sscanf("1.000000e+00", "%lE", dbl) == 1);
            Assert.IsTrue(dbl.Value == 1);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1.000000e+00", "%E", flt) == 1);
            Assert.IsTrue(flt.Value == 1);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%lG", dbl) == 1);
            Assert.IsTrue(dbl.Value == 1);
            Assert.IsTrue(gmp_lib.gmp_sscanf("1", "%G", flt) == 1);
            Assert.IsTrue(flt.Value == 1);

            //Assert.IsTrue(gmp_lib.gmp_sscanf("0x1.000000p+0", "%a", dbl) == 1);
            //Assert.IsTrue(dbl.Value == 1);
            //Assert.IsTrue(gmp_lib.gmp_sscanf("0x1.000000p+0", "%a", flt) == 1);
            //Assert.IsTrue(flt.Value == 1);

            char_ptr str = new char_ptr("________________________");
            Assert.IsTrue(gmp_lib.gmp_sscanf("Hello World!", "%s", str) == 1);
            Assert.IsTrue(str.ToString() == "Hello");
            gmp_lib.free(str);

            ptr<int> p = new ptr<int>(12);
            Assert.IsTrue(gmp_lib.gmp_sscanf("123456", "123456%n", p) == 0);
            Assert.IsTrue(p.Value == 6);
            ptr<IntPtr> ptr = new ptr<IntPtr>();
            Assert.IsTrue(gmp_lib.gmp_sscanf(IntPtr.Size == 4 ? "0060F0F4" : "000000000060F0F4", "%p", ptr) == 1);
            Assert.IsTrue((UInt64)ptr.Value == 6353140);
        }

        #endregion

        #region "Integer (i.e. Z) routines."

        [Test]
        public void _mpz_realloc()
        {
            // Create and initialize new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Set the value of x to a 77-bit integer.
            char_ptr value = new char_ptr("1000 0000 0000 0000 0000");
            gmp_lib.mpz_set_str(x, value, 16);

            // Resize x to 50 limbs, and assert that its value has not changed.
            gmp_lib._mpz_realloc(x, 50);
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, 16, x);
            Assert.IsTrue(s.ToString() == "1000 0000 0000 0000 0000".Replace(" ", ""));

            // Resize x to 1 limb, and assert that its value has changed to 0.
            gmp_lib._mpz_realloc(x, 1);
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == 0);

            // Release unmanaged memory allocated for x and string values.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(value);
            gmp_lib.free(s);
        }

        [Test]
        public void mpz_abs()
        {
            // Create, initialize, and set the value of x to -10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -10000);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = |x|.
            gmp_lib.mpz_abs(z, x);

            // Assert that z is |x|.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 10000);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_add()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of y to 12222.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_ui(y, 12222U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x + y.
            gmp_lib.mpz_add(z, x, y);

            // Assert that z is the sum of x and y.
            Assert.IsTrue(gmp_lib.mpz_get_ui(z) == 22222U);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpz_clears(x, y, z, null);
        }

        [Test]
        public void mpz_add_ui()
        {
            // Create, initialize, and set the value of x to 0.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Increment x twice by 101999.
            gmp_lib.mpz_add_ui(x, x, 101999U);
            gmp_lib.mpz_add_ui(x, x, 101999U);

            // Assert that x is 203998.
            Assert.IsTrue(gmp_lib.mpz_get_ui(x) == 203998U);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_addmul()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of y to 12222.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_ui(y, 12222U);

            // Create, initialize, and set the value of z to 20000.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init_set_ui(z, 20000U);

            // Set z += x * y.
            gmp_lib.mpz_addmul(z, x, y);

            // Assert that z has been incremented by 10000 * 12222.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 20000U + 10000 * 12222);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, y, z, null);
        }

        [Test]
        public void mpz_addmul_ui()
        {
            // Create, initialize, and set the value of x to -10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -10000);

            // Create, initialize, and set the value of z to 20000.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init_set_si(z, 20000);

            // Set z += x * 12222.
            gmp_lib.mpz_addmul_ui(z, x, 12222U);

            // Assert that z has been incremented by -10000 * 12222.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 20000 + -10000 * 12222);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_and()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Create, initialize, and set the value of op2 to 70.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 70U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the bitwise and of op1 and op2.
            gmp_lib.mpz_and(rop, op1, op2);

            // Assert that rop is 6.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 6);

            // Release unmanaged memory allocated for rop, op1, and op2.
            gmp_lib.mpz_clears(rop, op1, op2, null);
        }

        [Test]
        public void mpz_bin_ui()
        {
            // Create, initialize, and set the value of n to 4.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 4);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the binomial coefficient (n:2).
            gmp_lib.mpz_bin_ui(rop, n, 2U);

            // Assert that rop is 6.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 6);

            // Release unmanaged memory allocated for n and rop.
            gmp_lib.mpz_clears(n, rop, null);
        }

        [Test]
        public void mpz_bin_uiui()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the binomial coefficient (4:2).
            gmp_lib.mpz_bin_uiui(rop, 4U, 2U);

            // Assert that rop is 6.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 6);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_cdiv_q()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = ceiling(n / d).
            gmp_lib.mpz_cdiv_q(q, n, d);

            // Assert that q is ceiling(10000 / 3).
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3334);

            // Release unmanaged memory allocated for n, d, and q.
            gmp_lib.mpz_clears(n, d, q, null);
        }

        [Test]
        public void mpz_cdiv_q_2exp()
        {
            // Create, initialize, and set the value of n to 10001.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10001);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = ceiling(n / 2^2).
            gmp_lib.mpz_cdiv_q_2exp(q, n, 2U);

            // Assert that q is ceiling(10001 / 4).
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 2501);

            // Release unmanaged memory allocated for n and q.
            gmp_lib.mpz_clears(n, q, null);
        }

        [Test]
        public void mpz_cdiv_q_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = ceiling(n / 3) and return r = n - 3 * q.
            // Assert q and r values.
            Assert.IsTrue(gmp_lib.mpz_cdiv_q_ui(q, n, 3U) == 2U);
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3334);

            // Release unmanaged memory allocated for n and q.
            gmp_lib.mpz_clears(n, q, null);
        }

        [Test]
        public void mpz_cdiv_qr()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the values of q and r to 0.
            mpz_t q = new mpz_t();
            mpz_t r = new mpz_t();
            gmp_lib.mpz_inits(q, r, null);

            // Set q = ceiling(n / 3) and r = n - d * q.
            gmp_lib.mpz_cdiv_qr(q, r, n, d);

            // Assert that q is 3334, and that r is -2.
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3334);
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == -2);

            // Release unmanaged memory allocated for n, d, q, and r.
            gmp_lib.mpz_clears(n, d, q, r, null);
        }

        [Test]
        public void mpz_cdiv_qr_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the values of q and r to 0.
            mpz_t q = new mpz_t();
            mpz_t r = new mpz_t();
            gmp_lib.mpz_inits(q, r, null);

            // Set q = ceiling(n / 3), r = n - d * q, and return r.
            Assert.IsTrue(gmp_lib.mpz_cdiv_qr_ui(q, r, n, 3U) == 2U);

            // Assert that q is 3334, and that r is -2.
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3334);
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == -2);

            // Release unmanaged memory allocated for n, q, and r.
            gmp_lib.mpz_clears(n, q, r, null);
        }

        [Test]
        public void mpz_cdiv_r()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - d * ceiling(n / d).
            gmp_lib.mpz_cdiv_r(r, n, d);

            // Assert that r is -2.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == -2);

            // Release unmanaged memory allocated for n, d, and r.
            gmp_lib.mpz_clears(n, d, r, null);
        }

        [Test]
        public void mpz_cdiv_r_2exp()
        {
            // Create, initialize, and set the value of n to 10001.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10001);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - 2^2 * ceiling(n / 2^2)
            gmp_lib.mpz_cdiv_r_2exp(r, n, 2U);

            // Assert that r is -3.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == -3);

            // Release unmanaged memory allocated for n and r.
            gmp_lib.mpz_clears(n, r, null);
        }

        [Test]
        public void mpz_cdiv_r_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - 3 * ceiling(n / 3), and return |r|.
            Assert.IsTrue(gmp_lib.mpz_cdiv_r_ui(r, n, 3U) == 2U);

            // Assert that r is -2.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == -2);

            // Release unmanaged memory allocated for n and r.
            gmp_lib.mpz_clears(n, r, null);
        }

        [Test]
        public void mpz_cdiv_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Assert that returned value is |n - 3 * ceiling(n / 3)|.
            Assert.IsTrue(gmp_lib.mpz_cdiv_ui(n, 3U) == 2U);

            // Release unmanaged memory allocated for n.
            gmp_lib.mpz_clear(n);
        }

        [Test]
        public void mpz_clear()
        {
            // Create and initialize a new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Assert that the value of x is 0.
            Assert.IsTrue(gmp_lib.mpz_get_ui(x) == 0U);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_clears()
        {
            // Create new integers x1, x2 and x3.
            mpz_t x1 = new mpz_t();
            mpz_t x2 = new mpz_t();
            mpz_t x3 = new mpz_t();

            // Initialize the integers.
            gmp_lib.mpz_inits(x1, x2, x3, null);

            // Assert that their value is 0.
            Assert.IsTrue(gmp_lib.mpz_get_si(x1) == 0);
            Assert.IsTrue(gmp_lib.mpz_get_si(x2) == 0);
            Assert.IsTrue(gmp_lib.mpz_get_si(x3) == 0);

            // Release unmanaged memory allocated for the integers.
            gmp_lib.mpz_clears(x1, x2, x3, null);
        }

        [Test]
        public void mpz_clrbit()
        {
            // Create, initialize, and set the value of rop to 70.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init_set_si(rop, 70);

            // Clear bit 3 of rop.
            gmp_lib.mpz_clrbit(rop, 3U);

            // Assert that rop is 70.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 70);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_cmp()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Create, initialize, and set the value of op2 to 70.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 70U);

            // Assert that op1 < op2.
            Assert.IsTrue(gmp_lib.mpz_cmp(op1, op2) < 0);

            // Release unmanaged memory allocated for op1 and op2.
            gmp_lib.mpz_clears(op1, op2, null);
        }

        [Test]
        public void mpz_cmp_d()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Assert that op1 < 70.0.
            Assert.IsTrue(gmp_lib.mpz_cmp_d(op1, 70.0) < 0);

            // Release unmanaged memory allocated for op1.
            gmp_lib.mpz_clear(op1);
        }

        [Test]
        public void mpz_cmp_si()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Assert that op1 < 70.
            Assert.IsTrue(gmp_lib.mpz_cmp_si(op1, 70) < 0);

            // Release unmanaged memory allocated for op1.
            gmp_lib.mpz_clear(op1);
        }

        [Test]
        public void mpz_cmp_ui()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Assert that op1 < 70.
            Assert.IsTrue(gmp_lib.mpz_cmp_ui(op1, 70U) < 0);

            // Release unmanaged memory allocated for op1.
            gmp_lib.mpz_clear(op1);
        }

        [Test]
        public void mpz_cmpabs()
        {
            // Create, initialize, and set the value of op1 to -63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_si(op1, -63);

            // Create, initialize, and set the value of op2 to 70.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 70U);

            // Assert that |op1| < |op2|.
            Assert.IsTrue(gmp_lib.mpz_cmp(op1, op2) < 0);

            // Release unmanaged memory allocated for op1 and op2.
            gmp_lib.mpz_clears(op1, op2, null);
        }

        [Test]
        public void mpz_cmpabs_d()
        {
            // Create, initialize, and set the value of op1 to -63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_si(op1, -63);

            // Assert that |op1| < |-70.0|.
            Assert.IsTrue(gmp_lib.mpz_cmpabs_d(op1, -70.0) < 0);

            // Release unmanaged memory allocated for op1.
            gmp_lib.mpz_clear(op1);
        }

        [Test]
        public void mpz_cmpabs_ui()
        {
            // Create, initialize, and set the value of op1 to -63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_si(op1, -63);

            // Assert that |op1| < |70|.
            Assert.IsTrue(gmp_lib.mpz_cmpabs_ui(op1, 70U) < 0);

            // Release unmanaged memory allocated for op1.
            gmp_lib.mpz_clear(op1);
        }

        [Test]
        public void mpz_com()
        {
            // Create, initialize, and set the value of op to 63.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 63U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the one's complement of op.
            gmp_lib.mpz_com(rop, op);

            // Assert that rop is -64.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == -64);

            // Release unmanaged memory allocated for rop and op.
            gmp_lib.mpz_clears(rop, op, null);
        }

        [Test]
        public void mpz_combit()
        {
            // Create, initialize, and set the value of rop to 70.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init_set_si(rop, 70);

            // Complement bit 3 of rop.
            gmp_lib.mpz_combit(rop, 3U);

            // Assert that rop is 78.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 78);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_congruent_p()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_ui(n, 10000U);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_ui(d, 3U);

            // Create, initialize, and set the value of c to 1.
            mpz_t c = new mpz_t();
            gmp_lib.mpz_init_set_ui(c, 1U);

            // Assert that n is congruent to c mod d.
            Assert.IsTrue(gmp_lib.mpz_congruent_p(n, c, d) > 0);

            // Release unmanaged memory allocated for n, d, and c.
            gmp_lib.mpz_clears(n, d, c, null);
        }

        [Test]
        public void mpz_congruent_2exp_p()
        {
            // Create, initialize, and set the value of n to 10001.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_ui(n, 10001U);

            // Create, initialize, and set the value of b to 1.
            mpz_t c = new mpz_t();
            gmp_lib.mpz_init_set_ui(c, 1U);

            // Assert that n is congruent to c mod 2^3.
            Assert.IsTrue(gmp_lib.mpz_congruent_2exp_p(n, c, 3U) > 0);

            // Release unmanaged memory allocated for n and c.
            gmp_lib.mpz_clears(n, c, null);
        }

        [Test]
        public void mpz_congruent_ui_p()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_ui(n, 10000U);

            // Assert that n is congruent to 1 mod 3.
            Assert.IsTrue(gmp_lib.mpz_congruent_ui_p(n, 1U, 3U) > 0);

            // Release unmanaged memory allocated for n.
            gmp_lib.mpz_clear(n);
        }

        [Test]
        public void mpz_divexact()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of y to 5.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_ui(y, 5U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x / y.
            gmp_lib.mpz_divexact(z, x, y);

            // Assert that z is 2000.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 2000);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpz_clears(x, y, z, null);
        }

        [Test]
        public void mpz_divexact_ui()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x / 5.
            gmp_lib.mpz_divexact_ui(z, x, 5U);

            // Assert that z is 2000.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 2000);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_divisible_p()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of y to 5.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_ui(y, 5U);

            // Assert that x is divisible by y.
            Assert.IsTrue(gmp_lib.mpz_divisible_p(x, y) > 0);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpz_clears(x, y, null);
        }

        [Test]
        public void mpz_divisible_ui_p()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Assert that x is divisible by 5.
            Assert.IsTrue(gmp_lib.mpz_divisible_ui_p(x, 5U) > 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_divisible_2exp_p()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            Assert.IsTrue(gmp_lib.mpz_divisible_2exp_p(x, 2U) > 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_even()
        {
            // Create, initialize, and set the value of op to 427295.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 427295);

            // Assert that op is not even but odd.
            Assert.IsTrue(gmp_lib.mpz_even_p(op) == 0);
            Assert.IsTrue(gmp_lib.mpz_odd_p(op) > 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_export_2()
        {
            // Create, initialize, and set the value of op to 0x800000000000000000000001.
            mpz_t op = new mpz_t();
            char_ptr value = new char_ptr("800000000000000000000001");
            gmp_lib.mpz_init_set_str(op, value, 16);

            // Export op as 3 words of 4 bytes each, first word is lsb, and first byte in each word is msb.
            void_ptr data = gmp_lib.allocate(12);
            ptr<size_t> countp = new ptr<size_t>(0);
            gmp_lib.mpz_export(data, countp, -1, 4, 1, 0, op);

            // Assert the result.
            byte[] result = new byte[12];
            Marshal.Copy(data.ToIntPtr(), result, 0, 12);
            Assert.IsTrue(result[0] == 0x00);
            Assert.IsTrue(result[1] == 0x00);
            Assert.IsTrue(result[2] == 0x00);
            Assert.IsTrue(result[3] == 0x01);
            Assert.IsTrue(result[4] == 0x00);
            Assert.IsTrue(result[5] == 0x00);
            Assert.IsTrue(result[6] == 0x00);
            Assert.IsTrue(result[7] == 0x00);
            Assert.IsTrue(result[8] == 0x80);
            Assert.IsTrue(result[9] == 0x00);
            Assert.IsTrue(result[10] == 0x00);
            Assert.IsTrue(result[11] == 0x00);

            // Release unmanaged memory allocated for rop, data, and value.
            gmp_lib.mpz_clear(op);
            gmp_lib.free(data);
            gmp_lib.free(value);
        }

        [Test]
        public void mpz_export()
        {
            // Create, initialize, and set the value of op to 0x800000000000000000000001.
            mpz_t op = new mpz_t();
            char_ptr value = new char_ptr("800000000000000000000001");
            gmp_lib.mpz_init_set_str(op, value, 16);

            // Export op as 3 words of 4 bytes each, first word is lsb, and first byte in each word is msb.
            void_ptr data = gmp_lib.allocate(12);
            size_t countp = 0;
            gmp_lib.mpz_export(data, ref countp, -1, 4, 1, 0, op);

            // Assert the result.
            byte[] result = new byte[12];
            Marshal.Copy(data.ToIntPtr(), result, 0, 12);
            Assert.IsTrue(result[0] == 0x00);
            Assert.IsTrue(result[1] == 0x00);
            Assert.IsTrue(result[2] == 0x00);
            Assert.IsTrue(result[3] == 0x01);
            Assert.IsTrue(result[4] == 0x00);
            Assert.IsTrue(result[5] == 0x00);
            Assert.IsTrue(result[6] == 0x00);
            Assert.IsTrue(result[7] == 0x00);
            Assert.IsTrue(result[8] == 0x80);
            Assert.IsTrue(result[9] == 0x00);
            Assert.IsTrue(result[10] == 0x00);
            Assert.IsTrue(result[11] == 0x00);

            // Release unmanaged memory allocated for rop, data, and value.
            gmp_lib.mpz_clear(op);
            gmp_lib.free(data);
            gmp_lib.free(value);
        }

        [Test]
        public void mpz_fac_ui()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = 3!.
            gmp_lib.mpz_fac_ui(rop, 3U);

            // Assert that rop is 6.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 6);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_2fac_ui()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = 9!!.
            gmp_lib.mpz_2fac_ui(rop, 9U);

            // Assert that rop is 945.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 945);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_mfac_uiui()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = 10!^(4).
            gmp_lib.mpz_mfac_uiui(rop, 10U, 4U);

            // Assert that rop is 945.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 120);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_primorial_ui()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = 7 * 5 * 3 * 2 = 210.
            gmp_lib.mpz_primorial_ui(rop, 9U);

            // Assert that rop is 210.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 210);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_fdiv_q()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = floor(n / d).
            gmp_lib.mpz_fdiv_q(q, n, d);

            // Assert that q is floor(10000 / 3).
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3333);

            // Release unmanaged memory allocated for n, d, and q.
            gmp_lib.mpz_clears(n, d, q, null);
        }

        [Test]
        public void mpz_fdiv_q_2exp()
        {
            // Create, initialize, and set the value of n to 10001.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10001);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = floor(n / 2^2).
            gmp_lib.mpz_fdiv_q_2exp(q, n, 2U);

            // Assert that q is floor(10001 / 4).
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 2500);

            // Release unmanaged memory allocated for n and q.
            gmp_lib.mpz_clears(n, q, null);
        }

        [Test]
        public void mpz_fdiv_q_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = floor(n / 3) and return r = n - 3 * q.
            // Assert q and r values.
            Assert.IsTrue(gmp_lib.mpz_fdiv_q_ui(q, n, 3U) == 1U);
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3333);

            // Release unmanaged memory allocated for n and q.
            gmp_lib.mpz_clears(n, q, null);
        }

        [Test]
        public void mpz_fdiv_qr()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the values of q and r to 0.
            mpz_t q = new mpz_t();
            mpz_t r = new mpz_t();
            gmp_lib.mpz_inits(q, r, null);

            // Set q = floor(n / 3) and r = n - d * q.
            gmp_lib.mpz_fdiv_qr(q, r, n, d);

            // Assert that q is 3333, and that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3333);
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n, d, q, and r.
            gmp_lib.mpz_clears(n, d, q, r, null);
        }

        [Test]
        public void mpz_fdiv_qr_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the values of q and r to 0.
            mpz_t q = new mpz_t();
            mpz_t r = new mpz_t();
            gmp_lib.mpz_inits(q, r, null);

            // Set q = floor(n / 3), r = n - d * q, and return r.
            Assert.IsTrue(gmp_lib.mpz_fdiv_qr_ui(q, r, n, 3U) == 1U);

            // Assert that q is 3333, and that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3333);
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n, q, and r.
            gmp_lib.mpz_clears(n, q, r, null);
        }

        [Test]
        public void mpz_fdiv_r()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - d * floor(n / d).
            gmp_lib.mpz_fdiv_r(r, n, d);

            // Assert that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n, d, and r.
            gmp_lib.mpz_clears(n, d, r, null);
        }

        [Test]
        public void mpz_fdiv_r_2exp()
        {
            // Create, initialize, and set the value of n to 10001.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10001);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - 2^2 * floor(n / 2^2)
            gmp_lib.mpz_fdiv_r_2exp(r, n, 2U);

            // Assert that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n and r.
            gmp_lib.mpz_clears(n, r, null);
        }

        [Test]
        public void mpz_fdiv_r_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - 3 * floor(n / 3), and return |r|.
            Assert.IsTrue(gmp_lib.mpz_fdiv_r_ui(r, n, 3U) == 1U);

            // Assert that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n and r.
            gmp_lib.mpz_clears(n, r, null);
        }

        [Test]
        public void mpz_fdiv_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Assert that returned value is |n - 3 * floor(n / 3)|.
            Assert.IsTrue(gmp_lib.mpz_fdiv_ui(n, 3U) == 1U);

            // Release unmanaged memory allocated for n.
            gmp_lib.mpz_clear(n);
        }

        [Test]
        public void mpz_fib_ui()
        {
            // Create, initialize, and set the value of fn to 0.
            mpz_t fn = new mpz_t();
            gmp_lib.mpz_init(fn);

            // Set fn to the n'th Fibonacci number.
            gmp_lib.mpz_fib_ui(fn, 20U);

            // Assert that fn is 6765.
            Assert.IsTrue(gmp_lib.mpz_get_si(fn) == 6765);

            // Release unmanaged memory allocated for fn.
            gmp_lib.mpz_clear(fn);
        }

        [Test]
        public void mpz_fib2_ui()
        {
            // Create, initialize, and set the values of fn and fnsub1 to 0.
            mpz_t fn = new mpz_t();
            mpz_t fnsub1 = new mpz_t();
            gmp_lib.mpz_inits(fn, fnsub1, null);

            // Set fnsub1 and fn to the 19'th and 20'th Fibonacci numbers respectively.
            gmp_lib.mpz_fib2_ui(fn, fnsub1, 20U);

            // Assert that fnsub1 and fn are respectively 4181 and 6765.
            Assert.IsTrue(gmp_lib.mpz_get_si(fnsub1) == 4181);
            Assert.IsTrue(gmp_lib.mpz_get_si(fn) == 6765);

            // Release unmanaged memory allocated for fn and fnsub1.
            gmp_lib.mpz_clears(fn, fnsub1, null);
        }

        [Test]
        public void mpz_fits_sint_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in int.
            Assert.IsTrue(gmp_lib.mpz_fits_sint_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_fits_slong_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in long.
            Assert.IsTrue(gmp_lib.mpz_fits_slong_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_fits_sshort_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in short.
            Assert.IsTrue(gmp_lib.mpz_fits_sshort_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_fits_uint_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in uint.
            Assert.IsTrue(gmp_lib.mpz_fits_uint_p(op) > 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_fits_ulong_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, uint.MaxValue);

            // Assert that op fits in ulong.
            Assert.IsTrue(gmp_lib.mpz_fits_ulong_p(op) > 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_fits_ushort_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in ushort.
            Assert.IsTrue(gmp_lib.mpz_fits_ushort_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_gcd()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Create, initialize, and set the value of op2 to 70.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 70U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the greatest common divisor of op1 and op2.
            gmp_lib.mpz_gcd(rop, op1, op2);

            // Assert that rop is 7.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 7);

            // Release unmanaged memory allocated for rop, op1, and op2.
            gmp_lib.mpz_clears(rop, op1, op2, null);
        }

        [Test]
        public void mpz_gcd_ui()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Return the greatest common divisor of op1 and 70.
            Assert.IsTrue(gmp_lib.mpz_gcd_ui(null, op1, 70U) == 7);

            // Release unmanaged memory allocated for op1.
            gmp_lib.mpz_clear(op1);
        }

        [Test]
        public void mpz_gcdext()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t a = new mpz_t();
            gmp_lib.mpz_init_set_ui(a, 63U);

            // Create, initialize, and set the value of op2 to 70.
            mpz_t b = new mpz_t();
            gmp_lib.mpz_init_set_ui(b, 70U);

            // Create, initialize, and set the values of g, s, and t to 0.
            mpz_t g = new mpz_t();
            mpz_t s = new mpz_t();
            mpz_t t = new mpz_t();
            gmp_lib.mpz_inits(g, s, t, null);

            // Set g to the the greatest common divisor of a and b, and set s and t such that a * s + b * t = g.
            gmp_lib.mpz_gcdext(g, s, t, a, b);

            // Assert that g is 7, and that s and t are respectively -1 and 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(g) == 7);
            Assert.IsTrue(gmp_lib.mpz_get_si(s) == -1);
            Assert.IsTrue(gmp_lib.mpz_get_si(t) == 1);

            // Release unmanaged memory allocated for g, s, t, a, and b.
            gmp_lib.mpz_clears(g, s, t, a, b, null);
        }

        [Test]
        public void mpz_get_d()
        {
            // Create, initialize, and set the value of x to 10.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_d(x, 10.7D);

            // Assert that the value of x is 10.0.
            Assert.IsTrue(gmp_lib.mpz_get_d(x) == 10.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_get_d_2exp()
        {
            // Create, initialize, and set the value of x to 2^20.
            mpz_t x = new mpz_t();
            char_ptr value = new char_ptr("100000000000000000000");
            gmp_lib.mpz_init_set_str(x, value, 2);

            // Assert that x is equal to 0.5^21.
            int exp = 0;
            Assert.IsTrue(gmp_lib.mpz_get_d_2exp(ref exp, x) == 0.5D);
            Assert.IsTrue(exp == 21);

            // Release unmanaged memory allocated for x and the string value.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(value);
        }

        [Test]
        public void mpz_get_si()
        {
            // Create, initialize, and set the value of x to -10.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -10);

            // Retrieve the value of x, and assert that it is -10.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == -10);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_get_str()
        {
            // Create, initialize, and set the value of x to -210.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -210);

            // Retrieve the string value of x, and assert that it is "-210".
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, 10, x);
            Assert.IsTrue(s.ToString() == "-210");

            // Release unmanaged memory allocated for x and the string value.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(s);
        }

        [Test]
        public void mpz_get_ui()
        {
            // Create, initialize, and set the value of x to 10.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10U);

            // Retrieve the value of x, and assert that it is 10.
            Assert.IsTrue(gmp_lib.mpz_get_ui(x) == 10U);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_getlimbn()
        {
            // Create and initialize new integer x.
            mpz_t op = new mpz_t();
            char_ptr value = new char_ptr("1000 ABCD 1234 7AB8 24FD");
            gmp_lib.mpz_init_set_str(op, value, 16);

            // Assert the value of the limbs of op.
            if (gmp_lib.mp_bytes_per_limb == 4)
            {
                Assert.IsTrue(gmp_lib.mpz_getlimbn(op, 0) == 0x7AB824FD);
                Assert.IsTrue(gmp_lib.mpz_getlimbn(op, 1) == 0xABCD1234);
                Assert.IsTrue(gmp_lib.mpz_getlimbn(op, 2) == 0x00001000);
            }
            else // gmp_lib.mp_bytes_per_limb == 8
            {
                Assert.IsTrue(gmp_lib.mpz_getlimbn(op, 0) == 0xABCD12347AB824FD);
                Assert.IsTrue(gmp_lib.mpz_getlimbn(op, 1) == 0x0000000000001000);
            }

            // Release unmanaged memory allocated for op and value.
            gmp_lib.mpz_clear(op);
            gmp_lib.free(value);
        }

        [Test]
        public void mpz_hamdist()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Create, initialize, and set the value of op2 to 70.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 70U);

            // Assert that the Hamming distance between op1 and op2 is 5.
            Assert.IsTrue(gmp_lib.mpz_hamdist(op1, op2) == 5U);

            // Release unmanaged memory allocated for op1 and op2.
            gmp_lib.mpz_clears(op1, op2, null);
        }

        [Test]
        public void mpz_import()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Copy 0x800000000000000000000001, 3 words of 4 bytes each, first word is lsb, and first byte in each word is msb.
            void_ptr data = gmp_lib.allocate(12);
            Marshal.Copy(new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00 }, 0, data.ToIntPtr(), 12);

            // Import value into rop.
            gmp_lib.mpz_import(rop, 3, -1, 4, 1, 0, data);

            // Assert the value of rop.
            char_ptr value = gmp_lib.mpz_get_str(char_ptr.Zero, 16, rop);
            Assert.IsTrue(value.ToString() == "800000000000000000000001");

            // Release unmanaged memory allocated for rop, data, and value.
            gmp_lib.mpz_clear(rop);
            gmp_lib.free(data);
            gmp_lib.free(value);
        }

        [Test]
        public void mpz_init()
        {
            // Create and initialize a new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Assert that the value of x is 0.
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, 10, x);
            Assert.IsTrue(s.ToString() == "0");

            // Release unmanaged memory allocated for x and its string value.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(s);
        }

        [Test]
        public void mpz_init2()
        {
            // Create a new integer x, and initialize its size to 300 bits.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init2(x, 300);

            // Assert that the value of x is 0.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_inits()
        {
            // Create new integers x1, x2 and x3.
            mpz_t x1 = new mpz_t();
            mpz_t x2 = new mpz_t();
            mpz_t x3 = new mpz_t();

            // Initialize the integers.
            gmp_lib.mpz_inits(x1, x2, x3, null);

            // Assert that their value is 0.
            Assert.IsTrue(gmp_lib.mpz_get_si(x1) == 0);
            Assert.IsTrue(gmp_lib.mpz_get_si(x2) == 0);
            Assert.IsTrue(gmp_lib.mpz_get_si(x3) == 0);

            // Release unmanaged memory allocated for the integers.
            gmp_lib.mpz_clears(x1, x2, x3, null);
        }

        [Test]
        public void mpz_init_set()
        {
            // Create, initialize, and set a new integer y to -210.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init(y);
            gmp_lib.mpz_set_si(y, -210);

            // Create, initialize, and set a new integer x to the value of y.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set(x, y);

            // Assert that x is equal to the value of y.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == -210);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpz_clears(x, y, null);
        }

        [Test]
        public void mpz_init_set_d()
        {
            // Create, initialize, and set the value of x to the truncation of 10.7.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_d(x, 10.7D);

            // Assert that the value of x is 10.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == 10);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_init_set_si()
        {
            // Create, initialize, and set the value of x to 10.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, 10);

            // Assert that the value of x is 10.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == 10);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_init_set_str()
        {
            // Create, initialize, and set the value of x.
            mpz_t x = new mpz_t();
            char_ptr value = new char_ptr("  1 234 567 890 876 543 211 234 567 890 987 654 321  ");
            gmp_lib.mpz_init_set_str(x, value, 10);

            // Assert the value of x.
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, 10, x);
            Assert.IsTrue(s.ToString() == value.ToString().Replace(" ", ""));

            // Release unmanaged memory allocated for x and string values.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(value);
            gmp_lib.free(s);
        }

        [Test]
        public void mpz_init_set_ui()
        {
            // Create, initialize, and set the value of x to 10.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10U);

            // Assert that the value of x is 10.
            Assert.IsTrue(gmp_lib.mpz_get_ui(x) == 10U);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_inp_raw()
        {
            // Create, initialize, and set the value of op to 123456.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 123456U);

            // Write op to a temporary file.
            string pathname = System.IO.Path.GetTempFileName();
            ptr<FILE> stream = new ptr<FILE>();
            _wfopen_s(out stream.Value.Value, pathname, "w");
            Assert.IsTrue(gmp_lib.mpz_out_raw(stream, op) == 7);
            fclose(stream.Value.Value);

            // Read op from the temporary file, and assert that the number of bytes read is 6.
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.mpz_inp_raw(op, stream) == 7);
            fclose(stream.Value.Value);

            // Assert that op is 123456.
            Assert.IsTrue(gmp_lib.mpz_get_ui(op) == 123456U);

            // Delete temporary file.
            System.IO.File.Delete(pathname);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_inp_str()
        {
            // Create and initialize op.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init(op);

            // Write op to a temporary file.
            string pathname = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(pathname, "123456");

            // Read op from the temporary file, and assert that the number of bytes read is 6.
            ptr<FILE> stream = new ptr<FILE>();
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.mpz_inp_str(op, stream, 10) == 6);
            fclose(stream.Value.Value);

            // Assert that op is 123456.
            Assert.IsTrue(gmp_lib.mpz_get_ui(op) == 123456U);

            // Delete temporary file.
            System.IO.File.Delete(pathname);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_invert()
        {
            // Create, initialize, and set the value of op1 to 3.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 3U);

            // Create, initialize, and set the value of op2 to 11.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 11U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the modular inverse of op1 mod op2, i.e. b, where op1 * b mod op1 = 1.
            gmp_lib.mpz_invert(rop, op1, op2);

            // Assert that rop is 4,
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 4);

            // Release unmanaged memory allocated for rop, op1, and op2.
            gmp_lib.mpz_clears(rop, op1, op2, null);
        }

        [Test]
        public void mpz_ior()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Create, initialize, and set the value of op2 to 70.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 70U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the bitwise inclusive or of op1 and op2.
            gmp_lib.mpz_ior(rop, op1, op2);

            // Assert that rop is 127.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 127);

            // Release unmanaged memory allocated for rop, op1, and op2.
            gmp_lib.mpz_clears(rop, op1, op2, null);
        }

        [Test]
        public void mpz_jacobi()
        {
            // Create, initialize, and set the value of a to 11.
            mpz_t a = new mpz_t();
            gmp_lib.mpz_init_set_ui(a, 11U);

            // Create, initialize, and set the value of b to 9.
            mpz_t b = new mpz_t();
            gmp_lib.mpz_init_set_ui(b, 9U);

            // Assert that the Jacobi symbol of (a/b) is 1.
            Assert.IsTrue(gmp_lib.mpz_jacobi(a, b) == 1);

            // Release unmanaged memory allocated for a and b.
            gmp_lib.mpz_clears(a, b, null);
        }

        [Test]
        public void mpz_kronecker()
        {
            // Create, initialize, and set the value of a to 15.
            mpz_t a = new mpz_t();
            gmp_lib.mpz_init_set_ui(a, 15U);

            // Create, initialize, and set the value of b to 4.
            mpz_t b = new mpz_t();
            gmp_lib.mpz_init_set_ui(b, 4U);

            // Assert that the Kronecker symbol of (a/b) is 1.
            Assert.IsTrue(gmp_lib.mpz_kronecker(a, b) == 1);

            // Release unmanaged memory allocated for a and b.
            gmp_lib.mpz_clears(a, b, null);
        }

        [Test]
        public void mpz_kronecker_si()
        {
            // Create, initialize, and set the value of a to 15.
            mpz_t a = new mpz_t();
            gmp_lib.mpz_init_set_ui(a, 15U);

            // Assert that the Kronecker symbol of (a/4) is 1.
            Assert.IsTrue(gmp_lib.mpz_kronecker_si(a, 4) == 1);

            // Release unmanaged memory allocated for a.
            gmp_lib.mpz_clear(a);
        }

        [Test]
        public void mpz_kronecker_ui()
        {
            // Create, initialize, and set the value of a to 15.
            mpz_t a = new mpz_t();
            gmp_lib.mpz_init_set_ui(a, 15U);

            // Assert that the Kronecker symbol of (a/4) is 1.
            Assert.IsTrue(gmp_lib.mpz_kronecker_ui(a, 4U) == 1);

            // Release unmanaged memory allocated for a.
            gmp_lib.mpz_clear(a);
        }

        [Test]
        public void mpz_si_kronecker()
        {
            // Create, initialize, and set the value of b to 4.
            mpz_t b = new mpz_t();
            gmp_lib.mpz_init_set_ui(b, 4U);

            // Assert that the Kronecker symbol of (15/b) is 1.
            Assert.IsTrue(gmp_lib.mpz_si_kronecker(15, b) == 1);

            // Release unmanaged memory allocated for b.
            gmp_lib.mpz_clear(b);
        }

        [Test]
        public void mpz_ui_kronecker()
        {
            // Create, initialize, and set the value of b to 4.
            mpz_t b = new mpz_t();
            gmp_lib.mpz_init_set_ui(b, 4U);

            // Assert that the Kronecker symbol of (15/b) is 1.
            Assert.IsTrue(gmp_lib.mpz_ui_kronecker(15U, b) == 1);

            // Release unmanaged memory allocated for b.
            gmp_lib.mpz_clear(b);
        }

        [Test]
        public void mpz_lcm()
        {
            // Create, initialize, and set the value of op1 to 2.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 2U);

            // Create, initialize, and set the value of op2 to 3.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 3U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the least common multiple of op1 and op2.
            gmp_lib.mpz_lcm(rop, op1, op2);

            // Assert that rop is 6.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 6);

            // Release unmanaged memory allocated for rop, op1, and op2.
            gmp_lib.mpz_clears(rop, op1, op2, null);
        }

        [Test]
        public void mpz_lcm_ui()
        {
            // Create, initialize, and set the value of op1 to 2.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 2U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the least common multiple of op1 and 3.
            gmp_lib.mpz_lcm_ui(rop, op1, 3U);

            // Assert that rop is 6.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 6);

            // Release unmanaged memory allocated for rop and op1.
            gmp_lib.mpz_clears(rop, op1, null);
        }

        [Test]
        public void mpz_legendre()
        {
            // Create, initialize, and set the value of a to 20.
            mpz_t a = new mpz_t();
            gmp_lib.mpz_init_set_ui(a, 20U);

            // Create, initialize, and set the value of p to 11.
            mpz_t p = new mpz_t();
            gmp_lib.mpz_init_set_ui(p, 11U);

            // Assert that the Legendre symbol of (a/p) is 1.
            Assert.IsTrue(gmp_lib.mpz_legendre(a, p) == 1);

            // Release unmanaged memory allocated for a and p.
            gmp_lib.mpz_clears(a, p, null);
        }

        [Test]
        public void mpz_lucnum_ui()
        {
            // Create, initialize, and set the value of ln to 0.
            mpz_t ln = new mpz_t();
            gmp_lib.mpz_init(ln);

            // Set ln to the 9'th Lucas number.
            gmp_lib.mpz_lucnum_ui(ln, 9U);

            // Assert that ln is 76.
            Assert.IsTrue(gmp_lib.mpz_get_si(ln) == 76);

            // Release unmanaged memory allocated for ln.
            gmp_lib.mpz_clear(ln);
        }

        [Test]
        public void mpz_lucnum2_ui()
        {
            // Create, initialize, and set the values of lnsub1 and ln to 0.
            mpz_t ln = new mpz_t();
            mpz_t lnsub1 = new mpz_t();
            gmp_lib.mpz_inits(ln, lnsub1, null);

            // Set lnsub1 and ln to the 8'th and 9'th Lucas nunbers respectively.
            gmp_lib.mpz_lucnum2_ui(ln, lnsub1, 9U);

            // Assert that lnsub1 and ln are respectively 47 and 76.
            Assert.IsTrue(gmp_lib.mpz_get_si(lnsub1) == 47);
            Assert.IsTrue(gmp_lib.mpz_get_si(ln) == 76);

            // Release unmanaged memory allocated for ln and lnsub1.
            gmp_lib.mpz_clears(ln, lnsub1, null);
        }

        [Test]
        public void mpz_millerrabin()
        {
            // Create, initialize, and set the value of n to 12.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_ui(n, 12U);

            // Assert that n is a composite number.
            Assert.IsTrue(gmp_lib.mpz_millerrabin(n, 25) == 0);

            // Release unmanaged memory allocated for n.
            gmp_lib.mpz_clear(n);
        }

        [Test]
        public void mpz_mod()
        {
            // Create, initialize, and set the value of x to 12222.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 12222U);

            // Create, initialize, and set the value of y to 10000.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_ui(y, 10000U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x mod y.
            gmp_lib.mpz_mod(z, x, y);

            // Assert that z is 12222 mod 10000.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 12222 % 10000);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpz_clears(x, y, z, null);
        }

        [Test]
        public void mpz_mod_ui()
        {
            // Create, initialize, and set the value of x to 12222.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 12222U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x mod y, and return z.
            Assert.IsTrue(gmp_lib.mpz_mod_ui(z, x, 10000U) == 12222 % 10000);

            // Assert that z is 12222 mod 10000.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 12222 % 10000);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_mul()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of y to 12222.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_ui(y, 12222U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x * y.
            gmp_lib.mpz_mul(z, x, y);

            // Assert that z is the product of x and y.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 10000 * 12222);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpz_clears(x, y, z, null);
        }

        [Test]
        public void mpz_mul_2exp()
        {
            // Create, initialize, and set the value of x to -10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -10000);

            // Create, initialize, and set the value of x to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = -10000 * 2^2.
            gmp_lib.mpz_mul_2exp(z, x, 2U);

            // Assert that z is -40000.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == -10000 * 4);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_mul_si()
        {
            // Create, initialize, and set the value of x to -10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -10000);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x * 12222.
            gmp_lib.mpz_mul_si(z, x, 12222);

            // Assert that z is the product of x and 12222.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == -10000 * 12222);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_mul_ui()
        {
            // Create, initialize, and set the value of x to -10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -10000);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x * 12222.
            gmp_lib.mpz_mul_ui(z, x, 12222);

            // Assert that z is the product of x and 12222.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == -10000 * 12222);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_neg()
        {
            // Create, initialize, and set the value of x to -10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -10000);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = -x.
            gmp_lib.mpz_neg(z, x);

            // Assert that z is -x.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 10000);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_nextprime()
        {
            // Create, initialize, and set the value of n to 12.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 12U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the next following op.
            gmp_lib.mpz_nextprime(rop, op);

            // Assert that rop is 13.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 13);

            // Release unmanaged memory allocated for rop and op.
            gmp_lib.mpz_clears(rop, op, null);
        }

        [Test]
        public void mpz_odd()
        {
            // Create, initialize, and set the value of op to 427294.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 427294);

            // Assert that op is not odd but even.
            Assert.IsTrue(gmp_lib.mpz_even_p(op) > 0);
            Assert.IsTrue(gmp_lib.mpz_odd_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_out_raw()
        {
            // Create, initialize, and set the value of op to 123456 (0x1E240).
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 0x1E240);

            // Get a temporary file.
            string pathname = System.IO.Path.GetTempFileName();

            // Open temporary file for writing.
            ptr<FILE> stream = new ptr<FILE>();
            _wfopen_s(out stream.Value.Value, pathname, "w");

            // Write op to temporary file, and assert that the number of bytes written is 7.
            Assert.IsTrue(gmp_lib.mpz_out_raw(stream, op) == 7);

            // Close temporary file.
            fclose(stream.Value.Value);

            // Assert that the content of the temporary file.
            byte[] r = System.IO.File.ReadAllBytes(pathname);
            Assert.IsTrue(r[0] == 0 && r[1] == 0 && r[2] == 0 && r[3] == 3 && r[4] == 0x01 && r[5] == 0xE2 && r[6] == 0x40);

            // Delete temporary file.
            System.IO.File.Delete(pathname);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern Int32 _wfopen_s(out IntPtr pFile, String filename, String mode);

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern Int32 fclose(IntPtr stream);

        [Test]
        public void mpz_out_str()
        {
            // Create, initialize, and set the value of op to 123456.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 123456U);

            // Get a temporary file.
            string pathname = System.IO.Path.GetTempFileName();

            // Open temporary file for writing.
            ptr<FILE> stream = new ptr<FILE>();
            _wfopen_s(out stream.Value.Value, pathname, "w");

            // Write op to temporary file, and assert that the number of bytes written is 6.
            Assert.IsTrue(gmp_lib.mpz_out_str(stream, 10, op) == 6);

            // Close temporary file.
            fclose(stream.Value.Value);

            // Assert that the content of the temporary file is "123456".
            string result = System.IO.File.ReadAllText(pathname);
            Assert.IsTrue(result == "123456");

            // Delete temporary file.
            System.IO.File.Delete(pathname);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_perfect_power_p()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_si(op, 10000);

            // Assert that op is a perfect power.
            Assert.IsTrue(gmp_lib.mpz_perfect_power_p(op) > 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_perfect_square_p()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_si(op, 10000);

            // Assert that op is a perfect square.
            Assert.IsTrue(gmp_lib.mpz_perfect_square_p(op) > 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_popcount()
        {
            // Create, initialize, and set the value of op to 63.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 63U);

            // Assert that op has 6 one bits.
            Assert.IsTrue(gmp_lib.mpz_popcount(op) == 6U);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clears(op);
        }

        [Test]
        public void mpz_pow_ui()
        {
            // Create, initialize, and set the value of base to 2.
            mpz_t @base = new mpz_t();
            gmp_lib.mpz_init_set_ui(@base, 2U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = base^4.
            gmp_lib.mpz_pow_ui(rop, @base, 4U);

            // Assert that rop is 16.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 16);

            // Release unmanaged memory allocated for rop and base.
            gmp_lib.mpz_clears(rop, @base, null);
        }

        [Test]
        public void mpz_powm()
        {
            // Create, initialize, and set the value of base to 2.
            mpz_t @base = new mpz_t();
            gmp_lib.mpz_init_set_ui(@base, 2U);

            // Create, initialize, and set the value of exp to 4.
            mpz_t exp = new mpz_t();
            gmp_lib.mpz_init_set_ui(exp, 4U);

            // Create, initialize, and set the value of mod to 3.
            mpz_t mod = new mpz_t();
            gmp_lib.mpz_init_set_ui(mod, 3U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = base^exp mod mod.
            gmp_lib.mpz_powm(rop, @base, exp, mod);

            // Assert that rop is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 1);

            // Release unmanaged memory allocated for rop, base, exp, and mod.
            gmp_lib.mpz_clears(rop, @base, exp, mod, null);
        }

        [Test]
        public void mpz_powm_sec()
        {
            // Create, initialize, and set the value of base to 2.
            mpz_t @base = new mpz_t();
            gmp_lib.mpz_init_set_ui(@base, 2U);

            // Create, initialize, and set the value of exp to 4.
            mpz_t exp = new mpz_t();
            gmp_lib.mpz_init_set_ui(exp, 4U);

            // Create, initialize, and set the value of mod to 3.
            mpz_t mod = new mpz_t();
            gmp_lib.mpz_init_set_ui(mod, 3U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = base^exp mod mod.
            gmp_lib.mpz_powm_sec(rop, @base, exp, mod);

            // Assert that rop is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 1);

            // Release unmanaged memory allocated for rop, base, exp, and mod.
            gmp_lib.mpz_clears(rop, @base, exp, mod, null);
        }

        [Test]
        public void mpz_powm_ui()
        {
            // Create, initialize, and set the value of base to 2.
            mpz_t @base = new mpz_t();
            gmp_lib.mpz_init_set_ui(@base, 2U);

            mpz_t mod = new mpz_t();
            gmp_lib.mpz_init_set_ui(mod, 3U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = base^4 mod mod.
            gmp_lib.mpz_powm_ui(rop, @base, 4U, mod);

            // Assert that rop is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 1);

            // Release unmanaged memory allocated for rop, base, and mod.
            gmp_lib.mpz_clears(rop, @base, mod, null);
        }

        [Test]
        public void mpz_probab_prime_p()
        {
            // Create, initialize, and set the value of n to 12.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_ui(n, 12U);

            // Assert that n is a composite number.
            Assert.IsTrue(gmp_lib.mpz_probab_prime_p(n, 25) == 0);

            // Release unmanaged memory allocated for n.
            gmp_lib.mpz_clear(n);
        }

        [Test]
        public void mpz_random()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Generate a random integer.
            gmp_lib.mpz_random(rop, 500);

            // Free all memory occupied by state and rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_random2()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Generate a random integer.
            gmp_lib.mpz_random(rop, 100);

            // Free all memory occupied by rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_realloc2()
        {
            // Create and initialize new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Set the value of x to a 77-bit integer.
            char_ptr value = new char_ptr("1000 0000 0000 0000 0000");
            gmp_lib.mpz_set_str(x, value, 16);

            // Resize x to 512 bits, and assert that its value has not changed.
            gmp_lib.mpz_realloc2(x, 512U);
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, 16, x);
            Assert.IsTrue(s.ToString() == "1000 0000 0000 0000 0000".Replace(" ", ""));

            // Resize x to 2 bits, and assert that its value has changed to 0.
            gmp_lib.mpz_realloc2(x, 2U);
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == 0);

            // Release unmanaged memory allocated for x and string values.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(value);
            gmp_lib.free(s);
        }

        [Test]
        public void mpz_remove()
        {
            // Create, initialize, and set the value of op to 45.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 45U);

            // Create, initialize, and set the value of f to 3.
            mpz_t f = new mpz_t();
            gmp_lib.mpz_init_set_ui(f, 3U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = op / f^n, and return n, the largest integer greater than or equal to 0, such that f^n divides op.
            Assert.IsTrue(gmp_lib.mpz_remove(rop, op, f) == 2);

            // Assert that rop is 5.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 5);

            // Release unmanaged memory allocated for rop, op, and f.
            gmp_lib.mpz_clears(rop, op, f, null);
        }

        [Test]
        public void mpz_root()
        {
            // Create, initialize, and set the value of op to 10000.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_si(op, 10000);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = trunc(cbrt(10000)).
            gmp_lib.mpz_root(rop, op, 3U);

            // Assert that rop is 21.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 21);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clears(rop, op, null);
        }

        [Test]
        public void mpz_rootrem()
        {
            // Create, initialize, and set the value of u to 10000.
            mpz_t u = new mpz_t();
            gmp_lib.mpz_init_set_si(u, 10000);

            // Create, initialize, and set the values of root and rem to 0.
            mpz_t root = new mpz_t();
            mpz_t rem = new mpz_t();
            gmp_lib.mpz_inits(root, rem, null);

            // Set root = trunc(cbrt(10000)) and rem = u - root.
            gmp_lib.mpz_rootrem(root, rem, u, 3U);

            // Assert that root is 21, and rem is 739.
            Assert.IsTrue(gmp_lib.mpz_get_si(root) == 21);
            Assert.IsTrue(gmp_lib.mpz_get_si(rem) == 739);

            // Release unmanaged memory allocated for root, rem, and u.
            gmp_lib.mpz_clears(root, rem, u, null);
        }

        [Test]
        public void mpz_rrandomb()
        {
            // Create, initialize, and seed a new random number generator.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(state);
            gmp_lib.gmp_randseed_ui(state, 100000U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Generate a random integer in the range [2^(50-1), (2^50)-1].
            gmp_lib.mpz_rrandomb(rop, state, 50);

            // Free all memory occupied by state and rop.
            gmp_lib.gmp_randclear(state);
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_scan0()
        {
            // Create, initialize, and set the value of op to 70.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 70U);

            // Assert that the first 0 bit starting from bit 1 in op is bit 3.
            Assert.IsTrue(gmp_lib.mpz_scan0(op, 1U) == 3U);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_scan1()
        {
            // Create, initialize, and set the value of op to 70.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_ui(op, 70U);

            // Assert that the first 1 bit starting from bit 3 in op is bit 6.
            Assert.IsTrue(gmp_lib.mpz_scan1(op, 3U) == 6U);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_set()
        {
            // Create, initialize, and set a new integer x to 10.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);
            gmp_lib.mpz_set_si(x, 10);

            // Create, initialize, and set a new integer y to -210.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init(y);
            gmp_lib.mpz_set_si(y, -210);

            // Assign the value of y to x.
            gmp_lib.mpz_set(x, y);

            // Assert that the value of x is -210.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == -210);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpz_clears(x, y, null);
        }

        [Test]
        public void mpz_set_d()
        {
            // Create and initialize a new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Set the value of x to the truncation of 10.7.
            gmp_lib.mpz_set_d(x, 10.7D);

            // Assert that the value of x is 10.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == 10);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_set_f()
        {
            // Create and initialize new integer x, and float y.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);
            mpf_t y = "1.7007e3";

            // Set the value of x to the truncation of 1700.7.
            gmp_lib.mpz_set_f(x, y);

            // Assert that the value of x is 1700.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == 1700);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpz_clear(x);
            gmp_lib.mpf_clear(y);
        }

        [Test]
        public void mpz_set_q()
        {
            // Create and initialize new integer x, and rational y.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);
            mpq_t y = "100/3";

            // Set the value of x to the truncation of 100/3.
            gmp_lib.mpz_set_q(x, y);

            // Assert that the value of x is 33.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == 33);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpz_clear(x);
            gmp_lib.mpq_clear(y);
        }

        [Test]
        public void mpz_set_si()
        {
            // Create and initialize a new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Set the value of x to -10.
            gmp_lib.mpz_set_si(x, -10);

            // Assert that the value of x is -10.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == -10);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_set_str()
        {
            // Create and initialize a new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Set the value of x.
            char_ptr value = new char_ptr("12 345 678 909 876 543 211 234 567 890 987 654 321");
            gmp_lib.mpz_set_str(x, value, 10);

            // Assert the value of x.
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, 10, x);
            Assert.IsTrue(s.ToString() == value.ToString().Replace(" ", ""));

            // Release unmanaged memory allocated for x and string values.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(value);
            gmp_lib.free(s);
        }

        [Test]
        public void mpz_set_ui()
        {
            // Create and initialize a new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Set the value of x to 10.
            gmp_lib.mpz_set_ui(x, 10U);

            // Assert that the value of x is 10.
            Assert.IsTrue(gmp_lib.mpz_get_ui(x) == 10U);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpz_clear(x);
        }

        [Test]
        public void mpz_setbit()
        {
            // Create, initialize, and set the value of rop to 70.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init_set_si(rop, 70);

            // Set bit 3 of rop.
            gmp_lib.mpz_setbit(rop, 3U);

            // Assert that rop is 78.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 78);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_sgn()
        {
            // Create, initialize, and set the value of op to -10.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_si(op, -10);

            // Assert that the sign of op is -1.
            Assert.IsTrue(gmp_lib.mpz_sgn(op) == -1);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_size()
        {
            // Create and initialize new integer x.
            mpz_t op = new mpz_t();
            char_ptr value = new char_ptr("1000 ABCD 1234 7AB8 24FD");
            gmp_lib.mpz_init_set_str(op, value, 16);

            // Assert the value of the limbs of op.
            if (gmp_lib.mp_bytes_per_limb == 4)
                Assert.IsTrue(gmp_lib.mpz_size(op) == 3);
            else // gmp_lib.mp_bytes_per_limb == 8
                Assert.IsTrue(gmp_lib.mpz_size(op) == 2);

            // Release unmanaged memory allocated for op and value.
            gmp_lib.mpz_clear(op);
            gmp_lib.free(value);
        }

        [Test]
        public void mpz_sizeinbase()
        {
            // Create, initialize, and set the value of op to 10000.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_si(op, 10000);

            // Assert size in different bases.
            Assert.IsTrue(gmp_lib.mpz_sizeinbase(op, 2) == 14);
            Assert.IsTrue(gmp_lib.mpz_sizeinbase(op, 8) == 5);
            Assert.IsTrue(gmp_lib.mpz_sizeinbase(op, 10) == 5);
            Assert.IsTrue(gmp_lib.mpz_sizeinbase(op, 16) == 4);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpz_clear(op);
        }

        [Test]
        public void mpz_sqrt()
        {
            // Create, initialize, and set the value of op to 10000.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_si(op, 10000);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = trunc(sqrt(op)).
            gmp_lib.mpz_sqrt(rop, op);

            // Assert that rop is 100.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 100);

            // Release unmanaged memory allocated for rop and op.
            gmp_lib.mpz_clears(rop, op, null);
        }

        [Test]
        public void mpz_sqrtrem()
        {
            // Create, initialize, and set the value of op to 10000.
            mpz_t op = new mpz_t();
            gmp_lib.mpz_init_set_si(op, 10000);

            // Create, initialize, and set the values of root and rem to 0.
            mpz_t root = new mpz_t();
            mpz_t rem = new mpz_t();
            gmp_lib.mpz_inits(root, rem);

            // Set root = trunc(sqrt(op)), and rem = op - root.
            gmp_lib.mpz_sqrtrem(root, rem, op);

            // Assert that root is 100, and rem is 0.
            Assert.IsTrue(gmp_lib.mpz_get_si(root) == 100);
            Assert.IsTrue(gmp_lib.mpz_get_si(rem) == 0);

            // Release unmanaged memory allocated for root, rem, and op.
            gmp_lib.mpz_clears(root, rem, op, null);
        }

        [Test]
        public void mpz_sub()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of y to 12222.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_ui(y, 12222U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x - y.
            gmp_lib.mpz_sub(z, x, y);

            // Assert that z = x - y.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == -2222);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpz_clears(x, y, z, null);
        }

        [Test]
        public void mpz_sub_ui()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = x - 12222.
            gmp_lib.mpz_sub_ui(z, x, 12222U);

            // Assert that z = x - 12222.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == -2222);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_ui_sub()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of z to 0.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init(z);

            // Set z = 12222 - x.
            gmp_lib.mpz_ui_sub(z, 12222U, x);

            // Assert that z = 12222 - x.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 2222);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_submul()
        {
            // Create, initialize, and set the value of x to 10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 10000U);

            // Create, initialize, and set the value of y to 12222.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_ui(y, 12222U);

            // Create, initialize, and set the value of z to 20000.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init_set_si(z, 20000);

            // Set z -= x * y.
            gmp_lib.mpz_submul(z, x, y);

            // Assert that z has been decremented by 10000 * 12222.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 20000 - 10000 * 12222);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpz_clears(x, y, z, null);
        }

        [Test]
        public void mpz_submul_ui()
        {
            // Create, initialize, and set the value of x to -10000.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, -10000);

            // Create, initialize, and set the value of z to 20000.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init_set_si(z, 20000);

            // Set z -= x * 12222U.
            gmp_lib.mpz_submul_ui(z, x, 12222U);

            // Assert that z has been decremented by -10000 * 12222.
            Assert.IsTrue(gmp_lib.mpz_get_si(z) == 20000 - -10000 * 12222);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpz_clears(x, z, null);
        }

        [Test]
        public void mpz_swap()
        {
            // Create, initialize, and set a new integer x to 10.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_si(x, 10);

            // Create, initialize, and set a new integer x to -210.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init_set_si(y, -210);

            // Swap the values of x and y.
            gmp_lib.mpz_swap(x, y);

            // Assert that the values have been swapped.
            Assert.IsTrue(gmp_lib.mpz_get_si(x) == -210);
            Assert.IsTrue(gmp_lib.mpz_get_si(y) == 10);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpz_clears(x, y, null);
        }

        [Test]
        public void mpz_tdiv_q()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = trunc(n / d).
            gmp_lib.mpz_tdiv_q(q, n, d);

            // Assert that q is trunc(10000 / 3).
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3333);

            // Release unmanaged memory allocated for n, d, and q.
            gmp_lib.mpz_clears(n, d, q, null);
        }

        [Test]
        public void mpz_tdiv_q_2exp()
        {
            // Create, initialize, and set the value of n to 10001.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10001);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = trunc(n / 2^2).
            gmp_lib.mpz_tdiv_q_2exp(q, n, 2U);

            // Assert that q is trunc(10001 / 4).
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 2500);

            // Release unmanaged memory allocated for n and q.
            gmp_lib.mpz_clears(n, q, null);
        }

        [Test]
        public void mpz_tdiv_q_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of q to 0.
            mpz_t q = new mpz_t();
            gmp_lib.mpz_init(q);

            // Set q = trunc(n / 3) and return r = n - 3 * q.
            // Assert q and r values.
            Assert.IsTrue(gmp_lib.mpz_tdiv_q_ui(q, n, 3U) == 1U);
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3333);

            // Release unmanaged memory allocated for n and q.
            gmp_lib.mpz_clears(n, q, null);
        }

        [Test]
        public void mpz_tdiv_qr()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the values of q and r to 0.
            mpz_t q = new mpz_t();
            mpz_t r = new mpz_t();
            gmp_lib.mpz_inits(q, r, null);

            // Set q = trunc(n / 3) and r = n - d * q.
            gmp_lib.mpz_tdiv_qr(q, r, n, d);

            // Assert that q is 3333, and that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3333);
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n, d, q, and r.
            gmp_lib.mpz_clears(n, d, q, r, null);
        }

        [Test]
        public void mpz_tdiv_qr_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the values of q and r to 0.
            mpz_t q = new mpz_t();
            mpz_t r = new mpz_t();
            gmp_lib.mpz_inits(q, r, null);

            // Set q = trunc(n / 3), r = n - d * q, and return r.
            Assert.IsTrue(gmp_lib.mpz_tdiv_qr_ui(q, r, n, 3U) == 1U);

            // Assert that q is 3333, and that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(q) == 3333);
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n, q, and r.
            gmp_lib.mpz_clears(n, q, r, null);
        }

        [Test]
        public void mpz_tdiv_r()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of d to 3.
            mpz_t d = new mpz_t();
            gmp_lib.mpz_init_set_si(d, 3);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - d * trunc(n / d).
            gmp_lib.mpz_tdiv_r(r, n, d);

            // Assert that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n, d, and r.
            gmp_lib.mpz_clears(n, d, r, null);
        }

        [Test]
        public void mpz_tdiv_r_2exp()
        {
            // Create, initialize, and set the value of n to 10001.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10001);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - 2^2 * trunc(n / 2^2)
            gmp_lib.mpz_tdiv_r_2exp(r, n, 2U);

            // Assert that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n and r.
            gmp_lib.mpz_clears(n, r, null);
        }

        [Test]
        public void mpz_tdiv_r_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Create, initialize, and set the value of r to 0.
            mpz_t r = new mpz_t();
            gmp_lib.mpz_init(r);

            // Set r = n - 3 * trunc(n / 3), and return |r|.
            Assert.IsTrue(gmp_lib.mpz_tdiv_r_ui(r, n, 3U) == 1U);

            // Assert that r is 1.
            Assert.IsTrue(gmp_lib.mpz_get_si(r) == 1);

            // Release unmanaged memory allocated for n and r.
            gmp_lib.mpz_clears(n, r, null);
        }

        [Test]
        public void mpz_tdiv_ui()
        {
            // Create, initialize, and set the value of n to 10000.
            mpz_t n = new mpz_t();
            gmp_lib.mpz_init_set_si(n, 10000);

            // Assert that returned value is |n - 3 * trunc(n / 3)|.
            Assert.IsTrue(gmp_lib.mpz_tdiv_ui(n, 3U) == 1U);

            // Release unmanaged memory allocated for n.
            gmp_lib.mpz_clear(n);
        }

        [Test]
        public void mpz_tstbit()
        {
            // Create, initialize, and set the value of rop to 70.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init_set_si(rop, 70);

            // Assert that bit 3 of rop is 0.
            Assert.IsTrue(gmp_lib.mpz_tstbit(rop, 3U) == 0);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_ui_pow_ui()
        {
            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop = 2^4.
            gmp_lib.mpz_ui_pow_ui(rop, 2U, 4U);

            // Assert that rop is 16.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 16);

            // Release unmanaged memory allocated for rop.
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_urandomb()
        {
            // Create, initialize, and seed a new random number generator.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(state);
            gmp_lib.gmp_randseed_ui(state, 100000U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Generate a random integer in the range [0, (2^50)-1].
            gmp_lib.mpz_urandomb(rop, state, 50);

            // Free all memory occupied by state and rop.
            gmp_lib.gmp_randclear(state);
            gmp_lib.mpz_clear(rop);
        }

        [Test]
        public void mpz_urandomm()
        {
            // Create, initialize, and seed a new random number generator.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(state);
            gmp_lib.gmp_randseed_ui(state, 100000U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Create, initialize, and set a large integer.
            mpz_t n = new mpz_t();
            char_ptr value = new char_ptr("123 456 789 012 345 678 901");
            gmp_lib.mpz_init_set_str(n, value, 10);

            // Generate a random integer in the range [0, n-1].
            gmp_lib.mpz_urandomm(rop, state, n);

            // Free all memory occupied by state, rop, and n.
            gmp_lib.gmp_randclear(state);
            gmp_lib.mpz_clears(rop, n, null);
        }

        [Test]
        public void mpz_xor()
        {
            // Create, initialize, and set the value of op1 to 63.
            mpz_t op1 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op1, 63U);

            // Create, initialize, and set the value of op2 to 70.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init_set_ui(op2, 70U);

            // Create, initialize, and set the value of rop to 0.
            mpz_t rop = new mpz_t();
            gmp_lib.mpz_init(rop);

            // Set rop to the bitwise exclusive or of op1 and op2.
            gmp_lib.mpz_xor(rop, op1, op2);

            // Assert that rop is 121.
            Assert.IsTrue(gmp_lib.mpz_get_si(rop) == 121);

            // Release unmanaged memory allocated for rop, op1, and op2.
            gmp_lib.mpz_clears(rop, op1, op2, null);
        }

        [Test]
        public void mpz_limbs_read()
        {
            // Create and initialize new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Set the value of x.
            char_ptr value = new char_ptr("10000 00000000000000000000000000000000");
            gmp_lib.mpz_set_str(x, value, gmp_lib.mp_bytes_per_limb == 4 ? 2 : 4);

            // Get pointer to the limbs of x.
            mp_ptr limbs = gmp_lib.mpz_limbs_read(x);

            // Assert the values of the limbs based on current architecture (x86 or x64).
            Assert.IsTrue(limbs[0] == 0);
            Assert.IsTrue(limbs[1] == (gmp_lib.mp_bytes_per_limb == 4 ? 16U : 256U));

            // Release unmanaged memory allocated for x and value.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(value);
        }

        [Test]
        public void mpz_limbs_write()
        {
            // Create and initialize new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Resize x to 3 limbs, and get pointer to the limbs.
            gmp_lib.mpz_set_ui(x, 2U);
            mp_ptr limbs = gmp_lib.mpz_limbs_write(x, 3);

            // Set the values of the limbs.
            limbs[0] = 0U;
            limbs[1] = 0U;
            limbs[2] = (gmp_lib.mp_bytes_per_limb == 4 ? 2U : 4U);
            gmp_lib.mpz_limbs_finish(x, -3);

            // Assert the value of x based on current architecture (x86 or x64).
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, gmp_lib.mp_bytes_per_limb == 4 ? 2 : 4, x);
            Assert.IsTrue(s.ToString() == "-10 00000000000000000000000000000000 00000000000000000000000000000000".Replace(" ", ""));

            // Release unmanaged memory allocated for x and s.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(s);
        }

        [Test]
        public void mpz_limbs_modify()
        {
            // Create, initialize, and set the value of x to 2.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init_set_ui(x, 2U);

            // Resize x to 3 limbs, and get pointer to the limbs.
            mp_ptr limbs = gmp_lib.mpz_limbs_modify(x, 3);

            // Set the value of x.
            limbs[0] = 0;
            limbs[1] = 0;
            limbs[2] = (IntPtr.Size == 4 ? 8U : 64U);
            gmp_lib.mpz_limbs_finish(x, -3);

            // Assert the value of x based on current architecture (x86 or x64).
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, gmp_lib.mp_bytes_per_limb == 4 ? 2 : 4, x);
            Assert.IsTrue(s.ToString() == "-1000 00000000000000000000000000000000 00000000000000000000000000000000".Replace(" ", ""));

            // Release unmanaged memory allocated for x and s.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(s);
        }

        [Test]
        public void mpz_roinit_n()
        {
            // Create and initialize new integer x.
            mpz_t x = new mpz_t();
            gmp_lib.mpz_init(x);

            // Prepare new limbs for x.
            mp_ptr limbs;
            if (gmp_lib.mp_bytes_per_limb == 4)
                limbs = new mp_ptr(new uint[] { 0U, 0U, 2U });
            else
                limbs = new mp_ptr(new ulong[] { 0UL, 0UL, 4UL });

            // Assign new limbs to x, and make x negative.
            x = gmp_lib.mpz_roinit_n(x, limbs, -3);

            // Assert new value of x.
            char_ptr s = gmp_lib.mpz_get_str(char_ptr.Zero, gmp_lib.mp_bytes_per_limb == 4 ? 2 : 4, x);
            Assert.IsTrue(s.ToString() == "-10 00000000000000000000000000000000 00000000000000000000000000000000".Replace(" ", ""));

            // Release unmanaged memory allocated for x and s.
            gmp_lib.mpz_clear(x);
            gmp_lib.free(s);
        }

        #endregion

        #region "Rational (i.e. Q) routines."

        [Test]
        public void mpq_abs()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Create, initialize, and set the value of rop to 0.
            mpq_t rop = new mpq_t();
            gmp_lib.mpq_init(rop);

            // Set rop = |-1/3|.
            gmp_lib.mpq_abs(rop, op);

            // Assert that rop is 1 / 3.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(rop, 1, 3U) == 0);

            // Release unmanaged memory allocated for rop and op.
            gmp_lib.mpq_clears(rop, op, null);
        }

        [Test]
        public void mpq_add()
        {
            // Create, initialize, and set the value of x to 1 / 2.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, 1, 2U);

            // Create, initialize, and set the value of y to 1 / 3.
            mpq_t y = new mpq_t();
            gmp_lib.mpq_init(y);
            gmp_lib.mpq_set_si(y, 1, 3U);

            // Create, initialize, and set the value of z to 0 / 1.
            mpq_t z = new mpq_t();
            gmp_lib.mpq_init(z);

            // Set z = x + y.
            gmp_lib.mpq_add(z, x, y);

            // Assert that z is the sum of x and y.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(z, 5, 6U) == 0);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpq_clears(x, y, z, null);
        }

        [Test]
        public void mpq_canonicalize()
        {
            // Create, initialize, and set a new rational to 10 / 20.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, 10, 20U);

            // Reduce op to its canonical form.
            gmp_lib.mpq_canonicalize(op);

            // Assert that z is the sum of x and y.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(op, 1, 2U) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpq_clear(op);
        }

        [Test]
        public void mpq_clear()
        {
            // Create and initialize a new rational x.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);

            // Assert that the value of x is 0.0.
            Assert.IsTrue(gmp_lib.mpq_get_d(x) == 0.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpq_clear(x);
        }

        [Test]
        public void mpq_clears()
        {
            // Create new rationals x1, x2 and x3.
            mpq_t x1 = new mpq_t();
            mpq_t x2 = new mpq_t();
            mpq_t x3 = new mpq_t();

            // Initialize the rationals.
            gmp_lib.mpq_inits(x1, x2, x3, null);

            // Assert that their value is 0.0.
            Assert.IsTrue(gmp_lib.mpq_get_d(x1) == 0.0);
            Assert.IsTrue(gmp_lib.mpq_get_d(x2) == 0.0);
            Assert.IsTrue(gmp_lib.mpq_get_d(x3) == 0.0);

            // Release unmanaged memory allocated for the rationals.
            gmp_lib.mpq_clears(x1, x2, x3, null);
        }

        [Test]
        public void mpq_cmp()
        {
            // Create, initialize, and set the value of op1 to 1 / 2.
            mpq_t op1 = new mpq_t();
            gmp_lib.mpq_init(op1);
            gmp_lib.mpq_set_si(op1, 1, 2U);

            // Create, initialize, and set the value of op2 to 1 / 3.
            mpq_t op2 = new mpq_t();
            gmp_lib.mpq_init(op2);
            gmp_lib.mpq_set_si(op2, 1, 3U);

            // Assert that op1 > op2.
            Assert.IsTrue(gmp_lib.mpq_cmp(op1, op2) > 0);

            // Release unmanaged memory allocated for op1 and op2.
            gmp_lib.mpq_clears(op1, op2, null);
        }

        [Test]
        public void mpq_cmp_si()
        {
            // Create, initialize, and set the value of op1 to 1 / 2.
            mpq_t op1 = new mpq_t();
            gmp_lib.mpq_init(op1);
            gmp_lib.mpq_set_si(op1, 1, 2U);

            // Assert that op1 < 5/6.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(op1, 5, 6U) < 0);

            // Release unmanaged memory allocated for op1.
            gmp_lib.mpq_clear(op1);
        }

        [Test]
        public void mpq_cmp_ui()
        {
            // Create, initialize, and set the value of op1 to 1 / 2.
            mpq_t op1 = new mpq_t();
            gmp_lib.mpq_init(op1);
            gmp_lib.mpq_set_si(op1, 1, 2U);

            // Assert that op1 == 3/6.
            Assert.IsTrue(gmp_lib.mpq_cmp_ui(op1, 3, 6U) == 0);

            // Release unmanaged memory allocated for op1.
            gmp_lib.mpq_clear(op1);
        }

        [Test]
        public void mpq_cmp_z()
        {
            // Create, initialize, and set the value of op1 to 1 / 2.
            mpq_t op1 = new mpq_t();
            gmp_lib.mpq_init(op1);
            gmp_lib.mpq_set_si(op1, 1, 2U);

            // Create, initialize, and set the value of op2 to 3.
            mpz_t op2 = new mpz_t();
            gmp_lib.mpz_init(op2);
            gmp_lib.mpz_set_si(op2, 3);

            // Assert that op1 < op2.
            Assert.IsTrue(gmp_lib.mpq_cmp_z(op1, op2) < 0);

            // Release unmanaged memory allocated for op1 and op2.
            gmp_lib.mpq_clear(op1);
            gmp_lib.mpz_clear(op2);
        }

        [Test]
        public void mpq_denref()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Get reference to denominator, and increment it by 2.
            mpz_t num = gmp_lib.mpq_denref(op);
            gmp_lib.mpz_add_ui(num, num, 2U);

            // Assert that op is -1 / 5.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(op, -1, 5U) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpq_clear(op);
        }

        [Test]
        public void mpq_div()
        {
            // Create, initialize, and set the value of n to 1 / 2.
            mpq_t n = new mpq_t();
            gmp_lib.mpq_init(n);
            gmp_lib.mpq_set_si(n, 1, 2U);

            // Create, initialize, and set the value of d to 1 / 3.
            mpq_t d = new mpq_t();
            gmp_lib.mpq_init(d);
            gmp_lib.mpq_set_si(d, 1, 3U);

            // Create, initialize, and set the value of q to 0.
            mpq_t q = new mpq_t();
            gmp_lib.mpq_init(q);

            // Set q = n / d.
            gmp_lib.mpq_div(q, n, d);

            // Assert that q is 3 / 2.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(q, 3, 2U) == 0);

            // Release unmanaged memory allocated for n, d, and q.
            gmp_lib.mpq_clears(n, d, q, null);
        }

        [Test]
        public void mpq_div_2exp()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Create, initialize, and set the value of rop to 0.
            mpq_t rop = new mpq_t();
            gmp_lib.mpq_init(rop);

            // Set rop = (-1/3) / 2^3.
            gmp_lib.mpq_div_2exp(rop, op, 3U);

            // Assert that rop is -1 / 24.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(rop, -1, 24U) == 0);

            // Release unmanaged memory allocated for rop and op.
            gmp_lib.mpq_clears(rop, op, null);
        }

        [Test]
        public void mpq_equal()
        {
            // Create, initialize, and set the value of op1 to 1 / 2.
            mpq_t op1 = new mpq_t();
            gmp_lib.mpq_init(op1);
            gmp_lib.mpq_set_si(op1, 1, 2U);

            // Create, initialize, and set the value of op2 to 1 / 3.
            mpq_t op2 = new mpq_t();
            gmp_lib.mpq_init(op2);
            gmp_lib.mpq_set_si(op2, 1, 3U);

            // Assert that op1 != op2.
            Assert.IsTrue(gmp_lib.mpq_equal(op1, op2) == 0);

            // Release unmanaged memory allocated for op1 and op2.
            gmp_lib.mpq_clears(op1, op2, null);
        }

        [Test]
        public void mpq_get_num()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Create and initialize a new integer.
            mpz_t num = new mpz_t();
            gmp_lib.mpz_init(num);

            // Set integer to numerator of rational, and increment integer by 2.
            gmp_lib.mpq_get_num(num, op);
            gmp_lib.mpz_add_ui(num, num, 2U);

            // Assert that num is 1, and op is -1 / 3.
            Assert.IsTrue(gmp_lib.mpz_cmp_si(num, 1) == 0);
            Assert.IsTrue(gmp_lib.mpq_cmp_si(op, -1, 3U) == 0);

            // Release unmanaged memory allocated for op and num.
            gmp_lib.mpq_clear(op);
            gmp_lib.mpz_clear(num);
        }

        [Test]
        public void mpq_get_den()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Create and initialize a new integer.
            mpz_t den = new mpz_t();
            gmp_lib.mpz_init(den);

            // Set integer to numerator of rational, and increment integer by 2..
            gmp_lib.mpq_get_den(den, op);
            gmp_lib.mpz_add_ui(den, den, 2U);

            // Assert that num is 1, and op is -1 / 3.
            Assert.IsTrue(gmp_lib.mpz_cmp_si(den, 5) == 0);
            Assert.IsTrue(gmp_lib.mpq_cmp_si(op, -1, 3U) == 0);

            // Release unmanaged memory allocated for op and num.
            gmp_lib.mpq_clear(op);
            gmp_lib.mpz_clear(den);
        }

        [Test]
        public void mpq_get_d()
        {
            // Create, initialize, and set the value of x to 10 / 11.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, 10, 11U);

            // Assert that the value of x is 10.0.
            Assert.IsTrue(gmp_lib.mpq_get_d(x) == 10.0 / 11.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpq_clear(x);
        }

        [Test]
        public void mpq_get_str()
        {
            // Create, initialize, and set the value of x to -210 / 13.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, -210, 13U);

            // Retrieve the string value of x, and assert that it is "-210/13".
            char_ptr s = gmp_lib.mpq_get_str(char_ptr.Zero, 10, x);
            Assert.IsTrue(s.ToString() == "-210/13");

            // Release unmanaged memory allocated for x and the string value.
            gmp_lib.mpq_clear(x);
            gmp_lib.free(s);
        }

        [Test]
        public void mpq_init()
        {
            // Create and initialize a new rational x.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);

            // Assert that the value of x is 0.
            char_ptr s = gmp_lib.mpq_get_str(char_ptr.Zero, 10, x);
            Assert.IsTrue(s.ToString() == "0");

            // Release unmanaged memory allocated for x and its string value.
            gmp_lib.mpq_clear(x);
            gmp_lib.free(s);
        }

        [Test]
        public void mpq_inits()
        {
            // Create new rationals x1, x2 and x3.
            mpq_t x1 = new mpq_t();
            mpq_t x2 = new mpq_t();
            mpq_t x3 = new mpq_t();

            // Initialize the rationals.
            gmp_lib.mpq_inits(x1, x2, x3);

            // Assert that their value is 0.
            Assert.IsTrue(gmp_lib.mpq_get_d(x1) == 0.0);
            Assert.IsTrue(gmp_lib.mpq_get_d(x2) == 0.0);
            Assert.IsTrue(gmp_lib.mpq_get_d(x3) == 0.0);

            // Release unmanaged memory allocated for the rationals.
            gmp_lib.mpq_clears(x1, x2, x3, null);
        }

        [Test]
        public void mpq_inp_str()
        {
            // Create, initialize, and set the value of op to 123/456.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);

            // Write rational to a temporary file.
            string pathname = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(pathname, "123/456");

            // Read op from the temporary file, and assert that the number of bytes read is 7.
            ptr<FILE> stream = new ptr<FILE>();
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.mpq_inp_str(op, stream, 10) == 7);
            fclose(stream.Value.Value);

            // Assert that op is 123/456.
            Assert.IsTrue(gmp_lib.mpq_cmp_ui(op, 123, 456U) == 0);

            // Delete temporary file.
            System.IO.File.Delete(pathname);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpq_clear(op);
        }

        [Test]
        public void mpq_inv()
        {
            // Create, initialize, and set the value of number to -1 / 3.
            mpq_t number = new mpq_t();
            gmp_lib.mpq_init(number);
            gmp_lib.mpq_set_si(number, -1, 3U);

            // Create, initialize, and set the value of inverted_number to 0.
            mpq_t inverted_number = new mpq_t();
            gmp_lib.mpq_init(inverted_number);

            // Set inverted_number = 1/(-1/3).
            gmp_lib.mpq_inv(inverted_number, number);

            // Assert that inverted_number is -3 / 1.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(inverted_number, -3, 1U) == 0);

            // Release unmanaged memory allocated for inverted_number and number.
            gmp_lib.mpq_clears(inverted_number, number, null);
        }

        [Test]
        public void mpq_mul()
        {
            // Create, initialize, and set the value of x to 1 / 2.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, 1, 2U);

            // Create, initialize, and set the value of y to -1 / 3.
            mpq_t y = new mpq_t();
            gmp_lib.mpq_init(y);
            gmp_lib.mpq_set_si(y, -1, 3U);

            // Create, initialize, and set the value of z to 0.
            mpq_t z = new mpq_t();
            gmp_lib.mpq_init(z);

            // Set z = x * y.
            gmp_lib.mpq_mul(z, x, y);

            // Assert that z is the product of x and y.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(z, -1, 6U) == 0);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpq_clears(x, y, z, null);
        }

        [Test]
        public void mpq_mul_2exp()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Create, initialize, and set the value of rop to 0.
            mpq_t rop = new mpq_t();
            gmp_lib.mpq_init(rop);

            // Set rop = (-1/3) * 2^3.
            gmp_lib.mpq_mul_2exp(rop, op, 3U);

            // Assert that rop is -8 / 3.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(rop, -8, 3U) == 0);

            // Release unmanaged memory allocated for rop and op.
            gmp_lib.mpq_clears(rop, op, null);
        }

        [Test]
        public void mpq_neg()
        {
            // Create, initialize, and set the value of operand to -1 / 3.
            mpq_t operand = new mpq_t();
            gmp_lib.mpq_init(operand);
            gmp_lib.mpq_set_si(operand, -1, 3U);

            // Create, initialize, and set the value of negated_operand to 0.
            mpq_t negated_operand = new mpq_t();
            gmp_lib.mpq_init(negated_operand);

            // Set negated_operand = -(-1/3).
            gmp_lib.mpq_neg(negated_operand, operand);

            // Assert that negated_operand is -8 / 3.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(negated_operand, 1, 3U) == 0);

            // Release unmanaged memory allocated for negated_operand and operand.
            gmp_lib.mpq_clears(negated_operand, operand, null);
        }

        [Test]
        public void mpq_numref()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Get reference to numerator, and increment it by 2.
            mpz_t num = gmp_lib.mpq_numref(op);
            gmp_lib.mpz_add_ui(num, num, 2U);

            // Assert that op is 1 / 3.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(op, 1, 3U) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpq_clear(op);
        }

        [Test]
        public void mpq_out_str()
        {
            // Create, initialize, and set the value of op to 123/456.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_ui(op, 123, 456U);

            // Get a temporary file.
            string pathname = System.IO.Path.GetTempFileName();

            // Open temporary file for writing.
            ptr<FILE> stream = new ptr<FILE>();
            _wfopen_s(out stream.Value.Value, pathname, "w");

            // Write op to temporary file, and assert that the number of bytes written is 7.
            Assert.IsTrue(gmp_lib.mpq_out_str(stream, 10, op) == 7);

            // Close temporary file.
            fclose(stream.Value.Value);

            // Assert that the content of the temporary file is "123/456".
            string result = System.IO.File.ReadAllText(pathname);
            Assert.IsTrue(result == "123/456");

            // Delete temporary file.
            System.IO.File.Delete(pathname);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpq_clear(op);
        }

        [Test]
        public void mpq_set()
        {
            // Create, initialize, and set a new rational x to 10 / 11.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, 10, 11);

            // Create, initialize, and set a new rational y to -210 / 13.
            mpq_t y = new mpq_t();
            gmp_lib.mpq_init(y);
            gmp_lib.mpq_set_si(y, -210, 13);

            // Assign the value of y to x.
            gmp_lib.mpq_set(x, y);

            // Assert that the value of x is -210 / 13.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(x, -210, 13) == 0);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpq_clears(x, y, null);
        }

        [Test]
        public void mpq_set_d()
        {
            // Create and initialize a new rational.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);

            // Set the value of x to 10.0 / 11.0.
            gmp_lib.mpq_set_d(x, 10.0D / 11.0);

            // Assert that the value of x is 10.0 / 11.0.
            Assert.IsTrue(gmp_lib.mpq_get_d(x) == 10.0D / 11.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpq_clear(x);
        }

        [Test]
        public void mpq_set_den()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Create, initialize, and set the value of a new integer to 5.
            mpz_t den = new mpz_t();
            gmp_lib.mpz_init_set_ui(den, 5U);

            // Set the denominator of op.
            gmp_lib.mpq_set_den(op, den);

            // Assert that op is -1 / 5.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(op, -1, 5U) == 0);

            // Release unmanaged memory allocated for op and num.
            gmp_lib.mpq_clear(op);
            gmp_lib.mpz_clear(den);
        }

        [Test]
        public void mpq_set_f()
        {
            // Create, initialize, and set a new rational x to 10 / 11.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, 10, 11);

            // Create, initialize, and set a new float y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init(y);
            gmp_lib.mpf_set_si(y, -210);

            // Assign the value of y to x.
            gmp_lib.mpq_set_f(x, y);

            // Assert that the value of x is -210 / 1.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(x, -210, 1) == 0);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpq_clear(x);
            gmp_lib.mpf_clear(y);
        }

        [Test]
        public void mpq_set_num()
        {
            // Create, initialize, and set the value of op to -1 / 3.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -1, 3U);

            // Create, initialize, and set the value of a new integer to 5.
            mpz_t num = new mpz_t();
            gmp_lib.mpz_init_set_ui(num, 5U);

            // Set the numerator of op.
            gmp_lib.mpq_set_num(op, num);

            // Assert that op is 5 / 3.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(op, 5, 3U) == 0);

            // Release unmanaged memory allocated for op and num.
            gmp_lib.mpq_clear(op);
            gmp_lib.mpz_clear(num);
        }

        [Test]
        public void mpq_set_si()
        {
            // Create and initialize a new rational x.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);

            // Set the value of x to -10 / 11.
            gmp_lib.mpq_set_si(x, -10, 11);

            // Assert that the value of x is -10 / 1.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(x, -10, 11U) == 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpq_clear(x);
        }

        [Test]
        public void mpq_set_str()
        {
            // Create and initialize a new rational x.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);

            // Set the value of x.
            char_ptr value = new char_ptr("12 345 678 909 876 543 211 234 567 890 987 654 321 / 234 567 890");
            gmp_lib.mpq_set_str(x, value, 10);

            // Assert the value of x.
            char_ptr s = gmp_lib.mpq_get_str(char_ptr.Zero, 10, x);
            Assert.IsTrue(s.ToString() == value.ToString().Replace(" ", ""));

            // Release unmanaged memory allocated for x and string values.
            gmp_lib.mpq_clear(x);
            gmp_lib.free(value);
            gmp_lib.free(s);
        }

        [Test]
        public void mpq_set_ui()
        {
            // Create and initialize a new rational x.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);

            // Set the value of x to 10 / 11.
            gmp_lib.mpq_set_ui(x, 10U, 11U);

            // Assert that the value of x is 10 / 11.
            Assert.IsTrue(gmp_lib.mpq_cmp_ui(x, 10U, 11U) == 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpq_clear(x);
        }

        [Test]
        public void mpq_set_z()
        {
            // Create, initialize, and set a new rational x to 10 / 11.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, 10, 11);

            // Create, initialize, and set a new integer y to -210.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init(y);
            gmp_lib.mpz_set_si(y, -210);

            // Assign the value of y to x.
            gmp_lib.mpq_set_z(x, y);

            // Assert that the value of x is -210 / 1.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(x, -210, 1) == 0);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpq_clear(x);
            gmp_lib.mpz_clear(y);
        }

        [Test]
        public void mpq_sgn()
        {
            // Create, initialize, and set a new rational x to -10 / 11.
            mpq_t op = new mpq_t();
            gmp_lib.mpq_init(op);
            gmp_lib.mpq_set_si(op, -10, 11);

            // Assert that op is negative.
            Assert.IsTrue(gmp_lib.mpq_sgn(op) == -1);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpq_clear(op);
        }

        [Test]
        public void mpq_sub()
        {
            // Create, initialize, and set the value of x to 1 / 2.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, 1, 2U);

            // Create, initialize, and set the value of y to 1 / 3.
            mpq_t y = new mpq_t();
            gmp_lib.mpq_init(y);
            gmp_lib.mpq_set_si(y, 1, 3U);

            // Create, initialize, and set the value of z to 0.
            mpq_t z = new mpq_t();
            gmp_lib.mpq_init(z);

            // Set z = x - y.
            gmp_lib.mpq_sub(z, x, y);

            // Assert that z = x - y.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(z, 1, 6U) == 0);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpq_clears(x, y, z, null);
        }

        [Test]
        public void mpq_swap()
        {
            // Create, initialize, and set a new rational x to 10 / 11.
            mpq_t x = new mpq_t();
            gmp_lib.mpq_init(x);
            gmp_lib.mpq_set_si(x, 10, 11U);

            // Create, initialize, and set a new rational x to -210 / 13.
            mpq_t y = new mpq_t();
            gmp_lib.mpq_init(y);
            gmp_lib.mpq_set_si(y, -210, 13U);

            // Swap the values of x and y.
            gmp_lib.mpq_swap(x, y);

            // Assert that the values have been swapped.
            Assert.IsTrue(gmp_lib.mpq_cmp_si(x, -210, 13U) == 0);
            Assert.IsTrue(gmp_lib.mpq_cmp_si(y, 10, 11U) == 0);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpq_clears(x, y, null);
        }

        #endregion

        #region "Float (i.e. F) routines."

        [Test]
        public void mpf_abs()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to -10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, -10);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = |x|.
            gmp_lib.mpf_abs(z, x);

            // Assert that the value of z is 10.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 10.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_add()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init_set_si(y, -210);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = x + y.
            gmp_lib.mpf_add(z, x, y);

            // Assert that the value of z is -200.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == -200.0);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpf_clears(x, y, z, null);
        }

        [Test]
        public void mpf_add_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = x + 210.
            gmp_lib.mpf_add_ui(z, x, 210U);

            // Assert that the value of z is 220.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 220.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_ceil()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.4.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, 10.4);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = ceil(x).
            gmp_lib.mpf_ceil(z, x);

            // Assert that the value of z is 11.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 11.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_clear()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create and initialize a new floating-point number x.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init(x);

            // Assert that the value of x is 0.0.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == 0.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_clears()
        {
            // Create new floating-point numbers x1, x2 and x3.
            mpf_t x1 = new mpf_t();
            mpf_t x2 = new mpf_t();
            mpf_t x3 = new mpf_t();

            // Initialize the floating-point numbers.
            gmp_lib.mpf_inits(x1, x2, x3, null);

            // Assert that their value is 0.
            Assert.IsTrue(gmp_lib.mpf_get_d(x1) == 0.0);
            Assert.IsTrue(gmp_lib.mpf_get_d(x2) == 0.0);
            Assert.IsTrue(gmp_lib.mpf_get_d(x3) == 0.0);

            // Release unmanaged memory allocated for the floating-point numbers.
            gmp_lib.mpf_clears(x1, x2, x3, null);
        }

        [Test]
        public void mpf_cmp()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 512.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 512);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init_set_si(z, 128);

            // Assert that x > z.
            Assert.IsTrue(gmp_lib.mpf_cmp(x, z) > 0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_cmp_z()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 512.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 512);

            // Create and initialize a new integer z.
            mpz_t z = new mpz_t();
            gmp_lib.mpz_init_set_si(z, 128);

            // Assert that x > z.
            Assert.IsTrue(gmp_lib.mpf_cmp_z(x, z) > 0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clear(x);
            gmp_lib.mpz_clear(z);
        }

        [Test]
        public void mpf_cmp_d()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64);

            // Create, initialize, and set a new floating-point number x to 512.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 512);

            // Assert that x > 128.0.
            Assert.IsTrue(gmp_lib.mpf_cmp_d(x, 128.0) > 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_cmp_si()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 512.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 512);

            // Assert that x > 128.
            Assert.IsTrue(gmp_lib.mpf_cmp_si(x, 128) > 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_cmp_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 512.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 512);

            // Assert that x > 128.
            Assert.IsTrue(gmp_lib.mpf_cmp_ui(x, 128) > 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_div()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init_set_si(y, -210);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = y / x.
            gmp_lib.mpf_div(z, y, x);

            // Assert that the value of z is -21.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == -21.0);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpf_clears(x, y, z, null);
        }

        [Test]
        public void mpf_div_2exp()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 512.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 512);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = x / 2^8.
            gmp_lib.mpf_div_2exp(z, x, 8U);

            // Assert that the value of z is 2.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 2.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_div_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init_set_si(y, -210);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = y / 10.
            gmp_lib.mpf_div_ui(z, y, 10U);

            // Assert that the value of z is -21.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == -21.0);

            // Release unmanaged memory allocated for y and z.
            gmp_lib.mpf_clears(y, z, null);
        }

        [Test]
        public void mpf_fits_sint_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in int.
            Assert.IsTrue(gmp_lib.mpf_fits_sint_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_fits_slong_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in long.
            Assert.IsTrue(gmp_lib.mpf_fits_slong_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_fits_sshort_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in short.
            Assert.IsTrue(gmp_lib.mpf_fits_sshort_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_fits_uint_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in uint.
            Assert.IsTrue(gmp_lib.mpf_fits_uint_p(op) > 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_fits_ulong_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in int.
            Assert.IsTrue(gmp_lib.mpf_fits_sint_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_fits_ushort_p()
        {
            // Create, initialize, and set the value of op 4294967295.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init_set_ui(op, uint.MaxValue);

            // Assert that op does not fit in ushort.
            Assert.IsTrue(gmp_lib.mpf_fits_ushort_p(op) == 0);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_floor()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.4.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, 10.4);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = floor(x).
            gmp_lib.mpf_floor(z, x);

            // Assert that the value of z is 10.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 10.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_get_d()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number to -123.0
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, -123.0);

            // Assert that the value of x is -123.0.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == -123.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_get_d_2exp()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number to -8.0
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, -8.0);

            // Assert that the absolute value of x is 0.5 x 2^4.
            ptr<int> exp = new ptr<int>(0);
            Assert.AreEqual(gmp_lib.mpf_get_d_2exp(exp, x), -0.5);
            Assert.IsTrue(exp.Value == 4);

            // Release unmanaged memory allocated for x and exp.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_get_default_prec()
        {
            // Set default precision to 128 bits.
            gmp_lib.mpf_set_default_prec(128U);

            // Assert that the value of x is 128 bits.
            Assert.IsTrue(gmp_lib.mpf_get_default_prec() == 128U);
        }

        [Test]
        public void mpf_get_prec()
        {
            // Create and initialize a new floating-point number x with 64-bit precision.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init2(x, 64U);

            // Assert that the value of x is 0.0, and that its precision is 64 bits.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == 0.0);
            Assert.IsTrue(gmp_lib.mpf_get_prec(x) == 64U);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_get_si()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number to -8.0
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, -8.0);

            // Assert that the value of x is -8.
            Assert.IsTrue(gmp_lib.mpf_get_si(x) == -8);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_get_str()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number to -8.0
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, -8.0);

            // Assert that the value of x is -8.
            mp_exp_t exp = 0;
            char_ptr value = gmp_lib.mpf_get_str(char_ptr.Zero, ref exp, 10, 0, x);
            Assert.IsTrue(value.ToString() == "-8");
            Assert.IsTrue(exp == 1);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
            gmp_lib.free(value);
        }

        [Test]
        public void mpf_get_str_2()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number to -8.0
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, -8.0);

            // Assert that the value of x is -8.
            ptr<mp_exp_t> exp = new ptr<mp_exp_t>(0);
            char_ptr value = gmp_lib.mpf_get_str(char_ptr.Zero, exp, 10, 0, x);
            Assert.IsTrue(value.ToString() == "-8");
            Assert.IsTrue(exp.Value == 1);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
            gmp_lib.free(value);
        }

        [Test]
        public void mpf_get_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number to 8.0
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, 8.0);

            // Assert that the value of x is -8.
            Assert.IsTrue(gmp_lib.mpf_get_ui(x) == 8);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_init()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create and initialize a new floating-point number x.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init(x);

            // Assert that the value of x is 0.0.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == 0.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_init2()
        {
            // Create and initialize a new floating-point number x with 64-bit precision.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init2(x, 64U);

            // Assert that the value of x is 0.0, and that its precision is 64 bits.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == 0.0);
            uint p = gmp_lib.mpf_get_prec(x);
            Assert.IsTrue(gmp_lib.mpf_get_prec(x) == 64U);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_inits()
        {
            // Create new floating-point numbers x1, x2 and x3.
            mpf_t x1 = new mpf_t();
            mpf_t x2 = new mpf_t();
            mpf_t x3 = new mpf_t();

            // Initialize the floating-point numbers.
            gmp_lib.mpf_inits(x1, x2, x3, null);

            // Assert that their value is 0.
            Assert.IsTrue(gmp_lib.mpf_get_d(x1) == 0.0);
            Assert.IsTrue(gmp_lib.mpf_get_d(x2) == 0.0);
            Assert.IsTrue(gmp_lib.mpf_get_d(x3) == 0.0);

            // Release unmanaged memory allocated for the floating-point numbers.
            gmp_lib.mpf_clears(x1, x2, x3, null);
        }

        [Test]
        public void mpf_init_set()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create, initialize, and set a new floating-point number y to x.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init_set(y, x);

            // Assert that the value of y is 10.
            Assert.IsTrue(gmp_lib.mpf_get_d(y) == 10.0);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpf_clears(x, y, null);
        }

        [Test]
        public void mpf_init_set_d()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number to -123.0
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, -123.0);

            // Assert that the value of x is -123.0.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == -123.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_init_set_si()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize and set a new floating-point number to -123.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, -123);

            // Assert that the value of x is -123.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == -123.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_init_set_str()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 0.0234.
            char_ptr value = new char_ptr("234e-4");
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_str(x, value, 10);

            // Assert that x is 40.
            Assert.IsTrue(x.ToString() == "0.234e-1");

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpf_clear(x);
            gmp_lib.free(value);
        }

        [Test]
        public void mpf_init_set_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number to 100.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_ui(x, 100U);

            // Assert that the value of x is 100.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == 100.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_inp_str()
        {
            // Create and initialize op.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init(op);

            // Write op to a temporary file.
            string pathname = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(pathname, "0.123456e6");

            // Read op from the temporary file, and assert that the number of bytes read is 6.
            ptr<FILE> stream = new ptr<FILE>();
            _wfopen_s(out stream.Value.Value, pathname, "r");
            Assert.IsTrue(gmp_lib.mpf_inp_str(op, stream, 10) == 10);
            fclose(stream.Value.Value);

            // Assert that op is 123456.
            Assert.IsTrue(gmp_lib.mpf_get_ui(op) == 123456U);

            // Delete temporary file.
            System.IO.File.Delete(pathname);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_integer_p()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, 10);

            // Assert that s is an integer.
            Assert.IsTrue(gmp_lib.mpf_integer_p(x) != 0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_mul()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init_set_si(y, -210);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = x * y.
            gmp_lib.mpf_mul(z, x, y);

            // Assert that the value of z is -2100.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == -2100.0);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpf_clears(x, y, z, null);
        }

        [Test]
        public void mpf_mul_2exp()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 100.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 100);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = x * 2^8.
            gmp_lib.mpf_mul_2exp(z, x, 8U);

            // Assert that the value of z is 25600.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 25600.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_mul_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = x * 210.
            gmp_lib.mpf_mul_ui(z, x, 210U);

            // Assert that the value of z is 2100.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 2100.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_neg()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = -x.
            gmp_lib.mpf_neg(z, x);

            // Assert that the value of z is -10.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == -10.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_out_str()
        {
            // Create, initialize, and set the value of op to 123456.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init_set_ui(op, 123456U);

            // Get a temporary file.
            string pathname = System.IO.Path.GetTempFileName();

            // Open temporary file for writing.
            ptr<FILE> stream = new ptr<FILE>();
            _wfopen_s(out stream.Value.Value, pathname, "w");

            // Write op to temporary file, and assert that the number of bytes written is 10.
            Assert.IsTrue(gmp_lib.mpf_out_str(stream, 10, 0, op) == 10);

            // Close temporary file.
            fclose(stream.Value.Value);

            // Assert that the content of the temporary file is "123456".
            string result = System.IO.File.ReadAllText(pathname);
            Assert.IsTrue(result == "0.123456e6");

            // Delete temporary file.
            System.IO.File.Delete(pathname);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_pow_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = sqrt(x).
            gmp_lib.mpf_pow_ui(z, x, 3U);

            // Assert that the value of z is 1000.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 1000.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_random2()
        {
            // Create, initialize, and set the value of rop to 0.
            mpf_t rop = new mpf_t();
            gmp_lib.mpf_init(rop);

            // Generate a random floating-point number with at most 10 limbs and its exponent in [-5 5].
            gmp_lib.mpf_random2(rop, 10, 5);

            // Free all memory occupied by rop.
            gmp_lib.mpf_clear(rop);
        }

        [Test]
        public void mpf_reldiff()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init_set_si(y, -210);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = |x - y| / x.
            gmp_lib.mpf_reldiff(z, x, y);

            // Assert that the value of z is 22.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 22.0);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpf_clears(x, y, z, null);
        }

        [Test]
        public void mpf_set()
        {
            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init2(x, 128U);
            gmp_lib.mpf_set_si(x, 10);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init2(y, 128U);
            gmp_lib.mpf_set_si(y, -210);

            // Assign the value of y to x.
            gmp_lib.mpf_set(x, y);

            // Assert that the value of x is -210.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == -210.0);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpf_clears(x, y, null);
        }

        [Test]
        public void mpf_set_d()
        {
            // Create and initialize a new floating-point number.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init2(x, 128U);

            // Set x to -123.0.
            gmp_lib.mpf_set_d(x, -123.0);

            // Assert that the value of x is -123.0.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == -123.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_set_default_prec()
        {
            // Set default precision to 128 bits.
            gmp_lib.mpf_set_default_prec(128U);

            // Assert that the value of x is 128 bits.
            Assert.IsTrue(gmp_lib.mpf_get_default_prec() == 128U);
        }

        [Test]
        public void mpf_set_prec()
        {
            // Create and initialize a new floating-point number x.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init(x);

            // Set its precision to 64 bits.
            gmp_lib.mpf_set_prec(x, 64U);

            // Assert that the value of x is 0.0, and that its precision is 64 bits.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == 0.0);
            Assert.IsTrue(gmp_lib.mpf_get_prec(x) == 64U);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_set_prec_raw()
        {
            // Set default precision to 128 bits.
            gmp_lib.mpf_set_default_prec(128U);

            // Create, initialize, and set a new rational y to 200 / 3.
            mpq_t y = new mpq_t();
            gmp_lib.mpq_init(y);
            gmp_lib.mpq_set_ui(y, 200, 3U);

            // Create, initialize, and set a new floating-point number x to y.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init(x);
            gmp_lib.mpf_set_q(x, y);

            Assert.IsTrue(x.ToString() == "0.6666666666666666666666666666666666666667e2");

            // Change precision of x, and set its value to 10000 / 3.
            gmp_lib.mpf_set_prec_raw(x, 8U);
            gmp_lib.mpq_set_ui(y, 10000, 3U);
            gmp_lib.mpf_set_q(x, y);

            Assert.IsTrue(x.ToString() == "0.333333333333333333333e4");

            // Restore precision of x.
            gmp_lib.mpf_set_prec_raw(x, 128U);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpf_clear(x);
            gmp_lib.mpq_clear(y);
        }

        [Test]
        public void mpf_set_q()
        {
            // Create, initialize, and set a new rational y to 200 / 5.
            mpq_t y = new mpq_t();
            gmp_lib.mpq_init(y);
            gmp_lib.mpq_set_ui(y, 200, 5U);

            // Create, initialize, and set a new floating-point number x to y.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init(x);
            gmp_lib.mpf_set_q(x, y);

            // Assert that x is 40.
            Assert.IsTrue(x.ToString() == "0.4e2");

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpf_clear(x);
            gmp_lib.mpq_clear(y);
        }

        [Test]
        public void mpf_set_si()
        {
            // Create and initialize a new floating-point number.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init2(x, 128U);

            // Set x to -123.
            gmp_lib.mpf_set_si(x, -123);

            // Assert that the value of x is -123.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == -123.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_set_str()
        {
            // Create, initialize, and set a new floating-point number x to 0.0234.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init(x);
            char_ptr value = new char_ptr("234e-4");
            gmp_lib.mpf_set_str(x, value, 10);

            // Assert that x is 40.
            Assert.IsTrue(x.ToString() == "0.234e-1");

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpf_clear(x);
            gmp_lib.free(value);
        }

        [Test]
        public void mpf_set_ui()
        {
            // Create and initialize a new floating-point number.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init2(x, 128U);

            // Set x to 100.
            gmp_lib.mpf_set_ui(x, 100U);

            // Assert that the value of x is 100.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == 100.0);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_set_z()
        {
            // Create, initialize, and set a new integer y to 200.
            mpz_t y = new mpz_t();
            gmp_lib.mpz_init(y);
            gmp_lib.mpz_set_ui(y, 200U);

            // Create, initialize, and set a new floating-point number x to y.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init(x);
            gmp_lib.mpf_set_z(x, y);

            // Assert that x is 200.
            Assert.IsTrue(x.ToString() == "0.2e3");

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpf_clear(x);
            gmp_lib.mpz_clear(y);
        }

        [Test]
        public void mpf_sgn()
        {
            // Create, initialize, and set the value of op to -10.
            mpf_t op = new mpf_t();
            gmp_lib.mpf_init_set_si(op, -10);

            // Assert that the sign of op is -1.
            Assert.IsTrue(gmp_lib.mpf_sgn(op) == -1);

            // Release unmanaged memory allocated for op.
            gmp_lib.mpf_clear(op);
        }

        [Test]
        public void mpf_size()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x.
            mpf_t x = "1.00000000000000000000001";

            // Assert that the size of x is 1.
            Assert.AreEqual(gmp_lib.mpf_size(x), (size_t) 3);

            // Release unmanaged memory allocated for x.
            gmp_lib.mpf_clear(x);
        }

        [Test]
        public void mpf_sqrt()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 100.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 100);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = sqrt(x).
            gmp_lib.mpf_sqrt(z, x);

            // Assert that the value of z is 10.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 10.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_sqrt_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = sqrt(100).
            gmp_lib.mpf_sqrt_ui(z, 100U);

            // Assert that the value of z is 10.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 10.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clear(z);
        }

        [Test]
        public void mpf_sub()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init_set_si(y, -210);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = x - y.
            gmp_lib.mpf_sub(z, x, y);

            // Assert that the value of z is 220.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 220.0);

            // Release unmanaged memory allocated for x, y, and z.
            gmp_lib.mpf_clears(x, y, z, null);
        }

        [Test]
        public void mpf_sub_ui()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = x - 200.
            gmp_lib.mpf_sub_ui(z, x, 200U);

            // Assert that the value of z is -190.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == -190.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_swap()
        {
            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init2(x, 128U);
            gmp_lib.mpf_set_si(x, 10);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init2(y, 128U);
            gmp_lib.mpf_set_si(y, -210);

            // Swap the values of x and y.
            gmp_lib.mpf_swap(x, y);

            // Assert that the value of x is -210.
            Assert.IsTrue(gmp_lib.mpf_get_d(x) == -210.0);

            // Assert that the value of y is 10.
            Assert.IsTrue(gmp_lib.mpf_get_d(y) == 10.0);

            // Release unmanaged memory allocated for x and y.
            gmp_lib.mpf_clears(x, y, null);
        }

        [Test]
        public void mpf_trunc()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.4.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_d(x, 10.4);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = trunc(x).
            gmp_lib.mpf_trunc(z, x);

            // Assert that the value of z is 10.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 10.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_ui_div()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number x to 10.
            mpf_t x = new mpf_t();
            gmp_lib.mpf_init_set_si(x, 10);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = 210 / x.
            gmp_lib.mpf_ui_div(z, 210U, x);

            // Assert that the value of z is 21.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 21.0);

            // Release unmanaged memory allocated for x and z.
            gmp_lib.mpf_clears(x, z, null);
        }

        [Test]
        public void mpf_ui_sub()
        {
            // Set default precision to 64 bits.
            gmp_lib.mpf_set_default_prec(64U);

            // Create, initialize, and set a new floating-point number y to -210.
            mpf_t y = new mpf_t();
            gmp_lib.mpf_init_set_si(y, -210);

            // Create and initialize a new floating-point number z.
            mpf_t z = new mpf_t();
            gmp_lib.mpf_init(z);

            // Set z = 10 - y.
            gmp_lib.mpf_ui_sub(z, 10U, y);

            // Assert that the value of z is 220.
            Assert.IsTrue(gmp_lib.mpf_get_d(z) == 220.0);

            // Release unmanaged memory allocated for y, and z.
            gmp_lib.mpf_clears(y, z, null);
        }

        [Test]
        public void mpf_urandomb()
        {
            // Create, initialize, and seed a new random number generator.
            gmp_randstate_t state = new gmp_randstate_t();
            gmp_lib.gmp_randinit_mt(state);
            gmp_lib.gmp_randseed_ui(state, 100000U);

            // Create, initialize, and set the value of rop to 0.
            mpf_t rop = new mpf_t();
            gmp_lib.mpf_init(rop);

            // Generate a random integer in the range [0, 1) with 50 bits precision.
            gmp_lib.mpf_urandomb(rop, state, 50);

            // Free all memory occupied by state and rop.
            gmp_lib.gmp_randclear(state);
            gmp_lib.mpf_clear(rop);
        }

        #endregion

        #region "Low level positive-integer (i.e. N) routines."

        [Test]
        public void mpn_add()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });

            // Set rp = s1 + s2.
            mp_limb_t carry = gmp_lib.mpn_add(rp, s1p, s1p.Size, s2p, s2p.Size);

            // Assert result of operation.
            Assert.IsTrue(carry == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_add_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });

            // Set rp = s1 + 1.
            mp_limb_t carry = gmp_lib.mpn_add_1(rp, s1p, s1p.Size, 1);

            // Assert result of operation.
            Assert.IsTrue(carry == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, result);
        }

        [Test]
        public void mpn_add_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x00000000 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });

            // Set rp = s1 + s2.
            mp_limb_t carry = gmp_lib.mpn_add_n(rp, s1p, s2p, rp.Size);

            // Assert result of operation.
            Assert.IsTrue(carry == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_addmul_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[] { 0x00000002, 0x00000000 });
            mp_ptr result = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });

            // Set rp += s1 * 2.
            mp_limb_t carry = gmp_lib.mpn_addmul_1(rp, s1p, s1p.Size, 2);

            // Assert result of operation.
            Assert.IsTrue(carry == 0x02);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, result);
        }

        [Test]
        public void mpn_cmp()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x00000000 });

            // Assert s1p > s2p.
            Assert.IsTrue(gmp_lib.mpn_cmp(s1p, s2p, s1p.Size) > 0);

            // Release unmanaged memory.
            gmp_lib.free(s1p, s2p);
        }

        [Test]
        public void mpn_zero_p()
        {
            // Create multi-precision operand.
            mp_ptr sp = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });

            // Assert sp == 0.
            Assert.IsTrue(gmp_lib.mpn_zero_p(sp, sp.Size) == 1);

            // Release unmanaged memory.
            gmp_lib.free(sp);
        }

        [Test]
        public void mpn_divexact_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xffffffff, 0x0000ffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x55555555, 0x00005555 });

            // Set rp = sp / 3.
            gmp_lib.mpn_divexact_1(rp, sp, sp.Size, 0x3);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_divexact_by3()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xffffffff, 0x0000ffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x55555555, 0x00005555 });

            // Set rp = sp / 3.
            mp_limb_t remainder = gmp_lib.mpn_divexact_by3(rp, sp, sp.Size);

            // Assert result of operation.
            Assert.IsTrue(remainder == 0);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_divexact_by3c()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xffffffff, 0x0000ffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xaaaaaaaa, 0x5555aaaa });

            // Set rp = sp / 3.
            mp_limb_t remainder = gmp_lib.mpn_divexact_by3c(rp, sp, sp.Size, 1);

            // Assert result of operation.
            Assert.IsTrue(remainder == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_divrem_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s2p = new mp_ptr(new uint[] { 0xffffffff, 0x0000ffff });
            mp_ptr r1p = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x435e50d7, 0x00000d79 });

            // Set r1p = s2p / 19.
            mp_limb_t remainder = gmp_lib.mpn_divrem_1(r1p, 0, s2p, s2p.Size, 0x13);

            // Assert result of operation.
            Assert.IsTrue(remainder == 10);
            Assert.IsTrue(r1p.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(r1p, s2p, result);
        }

        [Test]
        public void mpn_divmod_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s2p = new mp_ptr(new uint[] { 0xffffffff, 0x0000ffff });
            mp_ptr r1p = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x435e50d7, 0x00000d79 });

            // Set r1p = s2p / 19.
            mp_limb_t remainder = gmp_lib.mpn_divmod_1(r1p, s2p, s2p.Size, 0x13);

            // Assert result of operation.
            Assert.IsTrue(remainder == 10);
            Assert.IsTrue(r1p.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(r1p, s2p, result);
        }

        [Test]
        public void mpn_gcd()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr xp = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });
            mp_ptr yp = new mp_ptr(new uint[] { 0xc2d24d55, 0x00000007 });
            mp_ptr rp = new mp_ptr(yp.Size);
            mp_ptr result = new mp_ptr(new uint[] { 0x964619c7, 0x00000002 });

            // Set rp = gcd(xp, yp).
            mp_size_t size = gmp_lib.mpn_gcd(rp, xp, xp.Size, yp, yp.Size);

            // Assert result of operation.
            Assert.IsTrue(size == result.Size);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, xp, yp, result);
        }

        [Test]
        public void mpn_gcd_1()
        {
            // Create multi-precision operand.
            mp_ptr xp = new mp_ptr(new uint[] { 0x00000000, 0x00000001 });

            // Assert result of operation.
            Assert.IsTrue(gmp_lib.mpn_gcd_1(xp, xp.Size, 1073741824) == 1073741824);

            // Release unmanaged memory.
            gmp_lib.free(xp);
        }

        [Test]
        public void mpn_gcdext()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr up = new mp_ptr(new uint[] { 0x40000000, 0x00000000 });
            mp_ptr vp = new mp_ptr(new uint[] { 0x00000000, 0x00000001 });
            mp_ptr gp = new mp_ptr(new uint[vp.Size * (IntPtr.Size / 4)]);
            mp_ptr sp = new mp_ptr(new uint[(vp.Size + 1) * (IntPtr.Size / 4)]);
            mp_ptr result = new mp_ptr(new uint[] { 0x40000000, 0x00000000 });
            mp_ptr cofactor = new mp_ptr(new uint[] { 0x00000001, 0x00000000, 0x00000000 });

            // Set gp = gcd(up, vp).
            mp_size_t sn = 0;
            mp_size_t size = gmp_lib.mpn_gcdext(gp, sp, ref sn, up, up.Size, vp, vp.Size);

            // Assert result.
            Assert.IsTrue(size == 1);
            Assert.IsTrue(gp.SequenceEqual(result));
            Assert.IsTrue(sn == 1);
            Assert.IsTrue(sp.SequenceEqual(cofactor));

            // Release unmanaged memory.
            gmp_lib.free(gp, up, vp, sp, result, cofactor);
        }

        [Test]
        public void mpn_gcdext_2()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr up = new mp_ptr(new uint[] { 0x40000000, 0x00000000 });
            mp_ptr vp = new mp_ptr(new uint[] { 0x00000000, 0x00000001 });
            mp_ptr gp = new mp_ptr(new uint[vp.Size * (IntPtr.Size / 4)]);
            mp_ptr sp = new mp_ptr(new uint[(vp.Size + 1) * (IntPtr.Size / 4)]);
            mp_ptr result = new mp_ptr(new uint[] { 0x40000000, 0x00000000 });
            mp_ptr cofactor = new mp_ptr(new uint[] { 0x00000001, 0x00000000, 0x00000000 });

            // Set gp = gcd(up, vp).
            ptr<mp_size_t> sn = new ptr<mp_size_t>(0);
            mp_size_t size = gmp_lib.mpn_gcdext(gp, sp, sn, up, up.Size, vp, vp.Size);

            // Assert result.
            Assert.IsTrue(size == 1);
            Assert.IsTrue(gp.SequenceEqual(result));
            Assert.IsTrue(sn.Value == 1);
            Assert.IsTrue(sp.SequenceEqual(cofactor));

            // Release unmanaged memory.
            gmp_lib.free(gp, up, vp, sp, result, cofactor);
        }

        [Test]
        public void mpn_get_str()
        {
            // Create multi-precision operands.
            mp_ptr s1p = new mp_ptr(new uint[] { 0x00000001, 0x00000001 });
            char_ptr str = new char_ptr("xxxxxxxxxxxxxxxxx");

            // Convert s1p to hex string.
            size_t count = gmp_lib.mpn_get_str(str, 16, s1p, s1p.Size);

            // Copy out str to bytes.
            byte[] s = new byte[count];
            Marshal.Copy(str.ToIntPtr(), s, 0, (int)count);

            // Assert the non-ASCII, hex representation of s1p.
            Assert.IsTrue(s.SequenceEqual(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 1 }));

            // Release unmanaged memory.
            gmp_lib.free(s1p);
            gmp_lib.free(str);
        }

        [Test]
        public void mpn_hamdist()
        {
            // Create multi-precision operands.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0xffffffff });

            // Assert hamming distance.
            Assert.IsTrue(gmp_lib.mpn_hamdist(s1p, s2p, s1p.Size) == 31);

            // Release unmanaged memory.
            gmp_lib.free(s1p, s2p);
        }

        [Test]
        public void mpn_lshift()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffc, 0xffffffff });

            // Set rp = sp << 1.
            mp_limb_t bits = gmp_lib.mpn_lshift(rp, sp, sp.Size, 1);

            // Assert result of operation.
            Assert.IsTrue(bits == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_mod_1()
        {
            // Create multi-precision operand.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xfffffffe, 0x0000ffff });

            // Assert s1p mod 3 is 2.
            Assert.IsTrue(gmp_lib.mpn_mod_1(s1p, s1p.Size, 3) == 2);

            // Release unmanaged memory.
            gmp_lib.free(s1p);
        }

        [Test]
        public void mpn_mul()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000002 });
            mp_ptr rp = new mp_ptr(new uint[3]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff, 0x00000001 });

            // Set rp = s1 * s2.
            gmp_lib.mpn_mul(rp, s1p, s1p.Size, s2p, s2p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_mul_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff });

            // Set rp = s1 * 2.
            mp_limb_t carry = gmp_lib.mpn_mul_1(rp, s1p, s1p.Size, 2);

            // Assert result of operation.
            Assert.IsTrue(carry == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, result);
        }

        [Test]
        public void mpn_mul_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000002, 0x00000000 });
            mp_ptr rp = new mp_ptr(new uint[4]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff, 0x00000001, 0x00000000 });

            // Set rp = s1 * s2.
            gmp_lib.mpn_mul_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_sqr()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[4]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000001, 0x00000000, 0xfffffffe, 0xffffffff });

            // Set rp = s1^2.
            gmp_lib.mpn_sqr(rp, s1p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, result);
        }

        [Test]
        public void mpn_neg()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x0000001, 0x00000000 });

            // Set rp = -sp.
            mp_limb_t borrow = gmp_lib.mpn_neg(rp, sp, sp.Size);

            // Assert result of operation.
            Assert.IsTrue(borrow == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_com()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xf0f0f0f0, 0xf0f0f0f0 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x0f0f0f0f, 0x0f0f0f0f });

            // Set rp = not(sp).
            gmp_lib.mpn_com(rp, sp, sp.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_perfect_square_p()
        {
            // Create multi-precision operand.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });

            // Assert s1p is not a perfect square.
            Assert.IsTrue(gmp_lib.mpn_perfect_square_p(s1p, s1p.Size) == 0);

            // Release unmanaged memory.
            gmp_lib.free(s1p);
        }

        [Test]
        public void mpn_perfect_power_p()
        {
            // Create multi-precision operand.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xd4a51000, 0x000000e8 });

            // Assert s1p is a perfect power.
            Assert.IsTrue(gmp_lib.mpn_perfect_power_p(s1p, s1p.Size) != 0);

            // Release unmanaged memory.
            gmp_lib.free(s1p);
        }

        [Test]
        public void mpn_popcount()
        {
            // Create multi-precision operand.
            mp_ptr s1p = new mp_ptr(new uint[] { 0x0000001, 0x00000001 });

            // Assert result of operation.
            Assert.IsTrue(gmp_lib.mpn_popcount(s1p, s1p.Size) == 2);

            // Release unmanaged memory.
            gmp_lib.free(s1p);
        }

        [Test]
        public void mpn_random()
        {
            // Create multi-precision operand.
            mp_ptr r1p = new mp_ptr(new uint[2]);

            // Generate random number.
            gmp_lib.mpn_random(r1p, gmp_lib.mp_bytes_per_limb == 4 ? 2 : 1);

            // Release unmanaged memory.
            gmp_lib.free(r1p);
        }

        [Test]
        public void mpn_random2()
        {
            // Create multi-precision operand.
            mp_ptr r1p = new mp_ptr(new uint[2]);

            // Generate random number.
            gmp_lib.mpn_random2(r1p, gmp_lib.mp_bytes_per_limb == 4 ? 2 : 1);

            // Release unmanaged memory.
            gmp_lib.free(r1p);
        }

        [Test]
        public void mpn_rshift()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xffffffff, 0x7fffffff });

            // Set rp = sp >> 1.
            mp_limb_t bits = gmp_lib.mpn_rshift(rp, sp, sp.Size, 1);

            // Assert result of operation.
            Assert.IsTrue(bits == (gmp_lib.mp_bytes_per_limb == 4 ? 0x80000000 : 0x8000000000000000));
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_scan0()
        {
            // Create multi-precision operand.
            mp_ptr s1p = new mp_ptr(new uint[] { 0x0000001, 0x00000001 });

            // Assert result of operation.
            Assert.IsTrue(gmp_lib.mpn_scan0(s1p, 0) == 1);

            // Release unmanaged memory.
            gmp_lib.free(s1p);
        }

        [Test]
        public void mpn_scan1()
        {
            // Create multi-precision operand.
            mp_ptr s1p = new mp_ptr(new uint[] { 0x0000001, 0x00000001 });

            // Assert result of operation.
            Assert.IsTrue(gmp_lib.mpn_scan1(s1p, 1) == 32);

            // Release unmanaged memory.
            gmp_lib.free(s1p);
        }

        [Test]
        public void mpn_set_str()
        {
            // Create multi-precision operands.
            mp_ptr rp = new mp_ptr(new uint[2]);
            byte[] s = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 1 };
            mp_ptr result = new mp_ptr(new uint[] { 0x00000001, 0x00000001 });
            char_ptr str = new char_ptr("xxxxxxxxxxxxxxxxx");
            Marshal.Copy(s, 0, str.ToIntPtr(), 9);

            // Convert rp from str in hex base.
            mp_size_t count = gmp_lib.mpn_set_str(rp, str, 9, 16);

            // Assert the non-ASCII, hex representation of s1p.
            Assert.IsTrue(count == rp.Size);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp);
            gmp_lib.free(str);
        }

        [Test]
        public void mpn_sizeinbase()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr xp = new mp_ptr(new uint[] { 0x00000001, 0x00000001 });

            // Assert that the number of bits required is 33.
            Assert.IsTrue(gmp_lib.mpn_sizeinbase(xp, xp.Size, 2) == 33);

            // Release unmanaged memory.
            gmp_lib.free(xp);
        }

        [Test]
        public void mpn_sqrtrem()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0x00000001, 0x00000001 });
            mp_ptr r1p = new mp_ptr(new uint[sp.Size * (gmp_lib.mp_bytes_per_limb / 4)]);
            mp_ptr r2p = new mp_ptr(new uint[sp.Size * (gmp_lib.mp_bytes_per_limb / 4)]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00010000, 0x00000000 });
            mp_ptr remainder = new mp_ptr(new uint[] { 0x00000001, 0x00000000 });

            // Set r1p = trunc(sqrt(sp)), r2p = sp - r1p^2
            mp_size_t r2n = gmp_lib.mpn_sqrtrem(r1p, r2p, sp, sp.Size);

            // Assert result.
            Assert.IsTrue(r2n == 1);
            Assert.IsTrue(r1p.SequenceEqual(result));
            Assert.IsTrue(r2p.SequenceEqual(remainder));

            // Release unmanaged memory.
            gmp_lib.free(sp, r1p, r2p, result);
        }

        [Test]
        public void mpn_sub()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff });

            // Set rp = s1 - s2.
            mp_limb_t borrow = gmp_lib.mpn_sub(rp, s1p, s1p.Size, s2p, s2p.Size);

            // Assert result of operation.
            Assert.IsTrue(borrow == 0);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_sub_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff });

            // Set rp = s1 - 1.
            mp_limb_t borrow = gmp_lib.mpn_sub_1(rp, s1p, s1p.Size, 1);

            // Assert result of operation.
            Assert.IsTrue(borrow == 0);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, result);
        }

        [Test]
        public void mpn_sub_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x00000000 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff });

            // Set rp = s1 - s2.
            mp_limb_t borrow = gmp_lib.mpn_sub_n(rp, s1p, s2p, rp.Size);

            // Assert result of operation.
            Assert.IsTrue(borrow == 0);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_submul_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr rp = new mp_ptr(new uint[] { 0x00000002, 0x00000000 });
            mp_ptr result = new mp_ptr(new uint[] { 0x00000004, 0x00000000 });

            // Set rp -= s1 * 2.
            mp_limb_t borrow = gmp_lib.mpn_submul_1(rp, s1p, s1p.Size, 2);

            // Assert result of operation.
            Assert.IsTrue(borrow == 0x02);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, result);
        }

        [Test]
        public void mpn_tdiv_qr()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr np = new mp_ptr(new uint[] { 0xffffffff, 0x0000ffff });
            mp_ptr dp = new mp_ptr(new uint[] { 0x00000013 });
            mp_ptr qp = new mp_ptr(new uint[np.Size - dp.Size + 1]);
            mp_ptr rp = new mp_ptr(new uint[dp.Size]);
            mp_ptr quotient = new mp_ptr(new uint[] { 0x435e50d7, 0x00000d79 });
            mp_ptr remainder = new mp_ptr(new uint[] { 0x0000000a });

            // Set rp = np / dp.
            gmp_lib.mpn_tdiv_qr(qp, rp, 0, np, np.Size, dp, dp.Size);

            // Assert result of operation.
            Assert.IsTrue(qp.SequenceEqual(quotient));
            Assert.IsTrue(rp.SequenceEqual(remainder));

            // Release unmanaged memory.
            gmp_lib.free(qp, rp, np, dp, quotient, remainder);
        }

        [Test]
        public void mpn_and_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });

            // Set rp = s1 and s2.
            gmp_lib.mpn_and_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_andn_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xedcba987 });

            // Set rp = s1 and not s2.
            gmp_lib.mpn_andn_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_nand_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });

            // Set rp = not(s1 and s2).
            gmp_lib.mpn_and_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_ior_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });

            // Set rp = s1 or s2.
            gmp_lib.mpn_ior_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_iorn_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });

            // Set rp = s1 or not s2.
            gmp_lib.mpn_iorn_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_nior_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });

            // Set rp = not (s1 or s2).
            gmp_lib.mpn_nior_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_xor_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xedcba987 });

            // Set rp = s1 xor s2.
            gmp_lib.mpn_xor_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_xnor_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000001, 0x12345678 });

            // Set rp = not(s1 xor s2).
            gmp_lib.mpn_xnor_n(rp, s1p, s2p, s1p.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_copyi()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xf0f0f0f0, 0xf0f0f0f0 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xf0f0f0f0, 0xf0f0f0f0 });

            // Set rp = sp.
            gmp_lib.mpn_copyi(rp, sp, sp.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_copyd()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr sp = new mp_ptr(new uint[] { 0xf0f0f0f0, 0xf0f0f0f0 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xf0f0f0f0, 0xf0f0f0f0 });

            // Set rp = sp.
            gmp_lib.mpn_copyd(rp, sp, sp.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, sp, result);
        }

        [Test]
        public void mpn_zero()
        {
            // Create multi-precision operand, and expected result.
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });

            // Set rp = sp.
            gmp_lib.mpn_zero(rp, rp.Size);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, result);
        }

        [Test]
        public void mpn_cnd_add_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x00000000 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });

            // Set rp = s1 + s2.
            mp_limb_t carry = gmp_lib.mpn_cnd_add_n(1, rp, s1p, s2p, rp.Size);

            // Assert result of operation.
            Assert.IsTrue(carry == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_cnd_sub_n()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr s1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr s2p = new mp_ptr(new uint[] { 0x00000001, 0x00000000 });
            mp_ptr rp = new mp_ptr(new uint[2]);
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff });

            // Set rp = s1 - s2.
            mp_limb_t borrow = gmp_lib.mpn_cnd_sub_n(1, rp, s1p, s2p, rp.Size);

            // Assert result of operation.
            Assert.IsTrue(borrow == 0);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, s1p, s2p, result);
        }

        [Test]
        public void mpn_sec_add_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr ap = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr result = new mp_ptr(new uint[] { 0x00000000, 0x00000000 });
            mp_ptr rp = new mp_ptr(result.Size);

            // Create scratch space.
            mp_size_t size = gmp_lib.mpn_sec_add_1_itch(ap.Size);
            mp_ptr tp = new mp_ptr(size);

            // Set rp = ap + 1.
            mp_limb_t carry = gmp_lib.mpn_sec_add_1(rp, ap, ap.Size, 1, tp);

            // Assert result of operation.
            Assert.IsTrue(carry == 1);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, ap, tp, result);
        }

        [Test]
        public void mpn_sec_sub_1()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr ap = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff });
            mp_ptr rp = new mp_ptr(result.Size);

            // Create scratch space.
            mp_size_t size = gmp_lib.mpn_sec_sub_1_itch(ap.Size);
            mp_ptr tp = new mp_ptr(size);

            // Set rp = ap - 1.
            mp_limb_t borrow = gmp_lib.mpn_sec_sub_1(rp, ap, ap.Size, 1, tp);

            // Assert result of operation.
            Assert.IsTrue(borrow == 0);
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, ap, tp, result);
        }

        [Test]
        public void mpn_cnd_swap()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr ap = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr bp = new mp_ptr(new uint[] { 0x00000001, 0x00000000 });
            mp_ptr a1p = new mp_ptr(new uint[] { 0x00000001, 0x00000000 });
            mp_ptr b1p = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });

            // Exchange ab and bp.
            gmp_lib.mpn_cnd_swap(1, ap, bp, ap.Size);

            // Assert result of operation.
            Assert.IsTrue(ap.SequenceEqual(a1p));
            Assert.IsTrue(bp.SequenceEqual(b1p));

            // Release unmanaged memory.
            gmp_lib.free(ap, bp, a1p, b1p);
        }

        [Test]
        public void mpn_sec_mul()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr ap = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr bp = new mp_ptr(new uint[] { 0x00000002 });
            mp_ptr result = new mp_ptr(new uint[] { 0xfffffffe, 0xffffffff, 0x00000001 });
            mp_ptr rp = new mp_ptr(ap.Size + bp.Size);

            // Create scratch space.
            mp_size_t size = gmp_lib.mpn_sec_mul_itch(ap.Size, bp.Size);
            mp_ptr tp = new mp_ptr(size);

            // Set rp = ap * bp.
            gmp_lib.mpn_sec_mul(rp, ap, ap.Size, bp, bp.Size, tp);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, ap, bp, tp, result);
        }

        [Test]
        public void mpn_sec_sqr()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr ap = new mp_ptr(new uint[] { 0xffffffff, 0xffffffff });
            mp_ptr result = new mp_ptr(new uint[] { 0x00000001, 0x00000000, 0xfffffffe, 0xffffffff });
            mp_ptr rp = new mp_ptr(2 * ap.Size);

            // Create scratch space.
            mp_size_t size = gmp_lib.mpn_sec_sqr_itch(ap.Size);
            mp_ptr tp = new mp_ptr(size);

            // Set rp = s1^2.
            gmp_lib.mpn_sec_sqr(rp, ap, ap.Size, tp);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, ap, tp, result);
        }

        [Test]
        public void mpn_sec_powm()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr bp = new mp_ptr(new uint[] { 0x00000002 });
            mp_ptr ep = new mp_ptr(new uint[] { 0x00000004 });
            mp_ptr mp = new mp_ptr(new uint[] { 0x00000003 });
            mp_ptr result = new mp_ptr(new uint[] { 0x00000001 });
            mp_ptr rp = new mp_ptr(bp.Size);

            // Create scratch space.
            mp_size_t size = gmp_lib.mpn_sec_powm_itch(bp.Size, 3, mp.Size);
            mp_ptr tp = new mp_ptr(size);

            // Set rp = bp^ep mod mp.
            gmp_lib.mpn_sec_powm(rp, bp, bp.Size, ep, 3, mp, mp.Size, tp);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(rp, bp, ep, mp, tp, result);
        }

        [Test]
        public void mpn_sec_tabselect()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr tab = new mp_ptr(new uint[] { 0x11111111, 0x22222222, 0x33333333, 0x44444444, 0x33333333, 0x00000000 });
            mp_ptr result = new mp_ptr(new uint[] { 0x33333333 });
            mp_ptr rp = new mp_ptr(result.Size);

            // Set rp to third entry in tab.
            gmp_lib.mpn_sec_tabselect(rp, tab, 1, tab.Size, 2);

            // Assert result of operation.
            Assert.IsTrue(rp.SequenceEqual(result));

            // Release unmanaged memory.
            gmp_lib.free(tab, result);
        }

        [Test]
        public void mpn_sec_div_qr()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr np = new mp_ptr(new uint[] { 0xffffffff, 0x0000ffff });
            mp_ptr dp = new mp_ptr(new uint[] { 0x00000003 });
            mp_ptr remainder = new mp_ptr(new uint[] { 0x00000000 });
            mp_ptr qp = new mp_ptr(new uint[np.Size]);

            // Create scratch space.
            mp_size_t size = gmp_lib.mpn_sec_div_qr_itch(np.Size, dp.Size);
            mp_ptr tp = new mp_ptr(size);

            // Set qp = floor(np / dp) and rp = np mod dp.
            mp_limb_t mslimb = gmp_lib.mpn_sec_div_qr(qp, np, np.Size, dp, dp.Size, tp);

            // Assert result of operation.
            Assert.IsTrue(mslimb == (ulong)(gmp_lib.mp_bytes_per_limb == 4 ? 0x00005555 : 0x0000555555555555));
            Assert.IsTrue(qp[0] == (ulong)(gmp_lib.mp_bytes_per_limb == 4 ? 0x55555555 : 0x0000000000000000));
            Assert.IsTrue(np[0] == remainder[0]);

            // Release unmanaged memory.
            gmp_lib.free(qp, np, dp, remainder, tp);
        }

        [Test]
        public void mpn_sec_div_r()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr np = new mp_ptr(new uint[] { 0xffffffff, 0x0000ffff });
            mp_ptr dp = new mp_ptr(new uint[] { 0x00000004 });

            // Create scratch space.
            mp_size_t size = gmp_lib.mpn_sec_div_r_itch(np.Size, dp.Size);
            mp_ptr tp = new mp_ptr(size);

            // Set np = np mod dp.
            gmp_lib.mpn_sec_div_r(np, np.Size, dp, dp.Size, tp);

            // Assert result of operation.
            Assert.IsTrue(np[0] == 3);

            // Release unmanaged memory.
            gmp_lib.free(np, dp, tp);
        }

        [Test]
        public void mpn_sec_invert()
        {
            // Create multi-precision operands, and expected result.
            mp_ptr ap = new mp_ptr(new uint[] { 3 });
            mp_ptr mp = new mp_ptr(new uint[] { 11 });
            mp_ptr rp = new mp_ptr(ap.Size);
            mp_ptr result = new mp_ptr(new uint[] { 4 });

            // Create scratch space.
            mp_size_t size = gmp_lib.mpn_sec_invert_itch(ap.Size);
            mp_ptr tp = new mp_ptr(size);

            // Set rp = ap^-1 mod mp.
            gmp_lib.mpn_sec_invert(rp, ap, mp, ap.Size, (uint)(2 * ap.Size * gmp_lib.mp_bits_per_limb), tp);

            // Assert result of operation.
            Assert.IsTrue(rp[0] == result[0]);

            // Release unmanaged memory.
            gmp_lib.free(ap, mp, rp, result, tp);
        }

        #endregion

    }
}
