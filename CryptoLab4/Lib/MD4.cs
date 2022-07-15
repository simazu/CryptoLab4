using System;
using System.Text;

namespace CryptoLab4.Lib
{
    public class MD4
    {
        private const int BLOCK_LENGTH = 64; // = 512 / 8

        private readonly uint[] X = new uint[16];

        private readonly uint[] context = new uint[4];

        private byte[] buffer = new byte[BLOCK_LENGTH];

        private long count;

        private bool doSecondRound, doThirdRound;

        private uint A, B, C, D;


        public MD4(bool doSecondRound = true, bool doThirdRound = true, 
            uint a = 0x67452301, uint b = 0xEFCDAB89, uint c = 0x98BADCFE, uint d = 0x10325476)
        {
            this.doSecondRound = doSecondRound;
            this.doThirdRound = doThirdRound;
            A = a;
            B = b;
            C = c;
            D = d;
            EngineReset();
        }

        private void EngineReset()
        {
            context[0] = A;
            context[1] = B;
            context[2] = C;
            context[3] = D;
            count = 0L;
            for (int i = 0; i < BLOCK_LENGTH; i++)
                buffer[i] = 0;
        }

        private void EngineUpdate(byte[] input, int offset, int len)
        {
            int bufferNdx = (int)(count % BLOCK_LENGTH);
            count += len; 
            int partLen = BLOCK_LENGTH - bufferNdx;
            int i = 0;
            if (len >= partLen)
            {
                Array.Copy(input, offset + i, buffer, bufferNdx, partLen);

                Transform(ref buffer, 0);

                for (i = partLen; i + BLOCK_LENGTH - 1 < len; i += BLOCK_LENGTH)
                    Transform(ref input, offset + i);
                bufferNdx = 0;
            }

            if (i < len)
                Array.Copy(input, offset + i, buffer, bufferNdx, len - i);
        }

        private byte[] EngineDigest()
        {
            int bufferNdx = (int)(count % BLOCK_LENGTH);
            int padLen = (bufferNdx < 56) ? (56 - bufferNdx) : (120 - bufferNdx);

            byte[] tail = new byte[padLen + 8];
            tail[0] = 0x80;

            for (int i = 0; i < 8; i++)
                tail[padLen + i] = (byte)((count * 8) >> (8 * i));

            EngineUpdate(tail, 0, tail.Length);

            var result = new byte[16];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    result[i * 4 + j] = (byte)(context[i] >> (8 * j));

            EngineReset();
            return result;
        }

        public byte[] GetByteHashFromString(string s)
        {
            byte[] b = Encoding.UTF8.GetBytes(s);
            MD4 md4 = new MD4();

            md4.EngineUpdate(b, 0, b.Length);

            return md4.EngineDigest();
        }

        public byte[] GetByteHashFromBytes(byte[] b)
        {
            MD4 md4 = this;

            md4.EngineUpdate(b, 0, b.Length);

            return md4.EngineDigest();
        }

        public string GetHexHashFromString(string s)
        {
            byte[] b = GetByteHashFromString(s);
            return BytesToHex(b, b.Length);
        }

        private static string BytesToHex(byte[] a, int len)
        {
            string temp = BitConverter.ToString(a);

            StringBuilder sb = new StringBuilder((len - 2) / 2); 

            for (int i = 0; i < temp.Length; i++)
                if (temp[i] != '-')
                    sb.Append(temp[i]);

            return sb.ToString();
        }


        private void Transform(ref byte[] block, int offset)
        {
            for (int i = 0; i < 16; i++)
                X[i] = ((uint)block[offset++] & 0xFF) |
                       (((uint)block[offset++] & 0xFF) << 8) |
                       (((uint)block[offset++] & 0xFF) << 16) |
                       (((uint)block[offset++] & 0xFF) << 24);


            uint A = context[0];
            uint B = context[1];
            uint C = context[2];
            uint D = context[3];

            A = FF(A, B, C, D, X[0], 3);
            D = FF(D, A, B, C, X[1], 7);
            C = FF(C, D, A, B, X[2], 11);
            B = FF(B, C, D, A, X[3], 19);
            A = FF(A, B, C, D, X[4], 3);
            D = FF(D, A, B, C, X[5], 7);
            C = FF(C, D, A, B, X[6], 11);
            B = FF(B, C, D, A, X[7], 19);
            A = FF(A, B, C, D, X[8], 3);
            D = FF(D, A, B, C, X[9], 7);
            C = FF(C, D, A, B, X[10], 11);
            B = FF(B, C, D, A, X[11], 19);
            A = FF(A, B, C, D, X[12], 3);
            D = FF(D, A, B, C, X[13], 7);
            C = FF(C, D, A, B, X[14], 11);
            B = FF(B, C, D, A, X[15], 19);

            if (doSecondRound)
            {
                A = GG(A, B, C, D, X[0], 3);
                D = GG(D, A, B, C, X[4], 5);
                C = GG(C, D, A, B, X[8], 9);
                B = GG(B, C, D, A, X[12], 13);
                A = GG(A, B, C, D, X[1], 3);
                D = GG(D, A, B, C, X[5], 5);
                C = GG(C, D, A, B, X[9], 9);
                B = GG(B, C, D, A, X[13], 13);
                A = GG(A, B, C, D, X[2], 3);
                D = GG(D, A, B, C, X[6], 5);
                C = GG(C, D, A, B, X[10], 9);
                B = GG(B, C, D, A, X[14], 13);
                A = GG(A, B, C, D, X[3], 3);
                D = GG(D, A, B, C, X[7], 5);
                C = GG(C, D, A, B, X[11], 9);
                B = GG(B, C, D, A, X[15], 13);
            }
            
            if (doThirdRound)
            {
                A = HH(A, B, C, D, X[0], 3);
                D = HH(D, A, B, C, X[8], 9);
                C = HH(C, D, A, B, X[4], 11);
                B = HH(B, C, D, A, X[12], 15);
                A = HH(A, B, C, D, X[2], 3);
                D = HH(D, A, B, C, X[10], 9);
                C = HH(C, D, A, B, X[6], 11);
                B = HH(B, C, D, A, X[14], 15);
                A = HH(A, B, C, D, X[1], 3);
                D = HH(D, A, B, C, X[9], 9);
                C = HH(C, D, A, B, X[5], 11);
                B = HH(B, C, D, A, X[13], 15);
                A = HH(A, B, C, D, X[3], 3);
                D = HH(D, A, B, C, X[11], 9);
                C = HH(C, D, A, B, X[7], 11);
                B = HH(B, C, D, A, X[15], 15);
            }
            
            context[0] += A;
            context[1] += B;
            context[2] += C;
            context[3] += D;
        }


        private uint FF(uint a, uint b, uint c, uint d, uint x, int s)
        {
            uint t = a + ((b & c) | (~b & d)) + x;
            return t << s | t >> (32 - s);
        }

        private uint GG(uint a, uint b, uint c, uint d, uint x, int s)
        {
            uint t = a + ((b & (c | d)) | (c & d)) + x + 0x5A827999;
            return t << s | t >> (32 - s);
        }

        private uint HH(uint a, uint b, uint c, uint d, uint x, int s)
        {
            uint t = a + (b ^ c ^ d) + x + 0x6ED9EBA1;
            return t << s | t >> (32 - s);
        }

    }
}
