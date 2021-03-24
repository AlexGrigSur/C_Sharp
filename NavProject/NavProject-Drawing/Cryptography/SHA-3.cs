using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_Drawing.Cryptography
{
    //[1]https://nvlpubs.nist.gov/nistpubs/FIPS/NIST.FIPS.202.pdf SHA official documentation
    //[2]https://stackoverflow.com/questions/52922526/round-constants-in-keccak round constants
    //[3]https://keccak.team/files/Keccak-reference-3.0.pdf KECCAK team reference

    // SHA3-256
    class SHA_3
    {
        private const int BITSIZE = 1600; 
        private const int RATE = 1088;
        private const int CAPACITY = BITSIZE - RATE; // RATE+CAPACITY = 1600 ==> 5*5*(2^6) // 2^L // in [3] C=576
        private const int L = 6; // change to readonly
        private const int ROUNDS = 12 + 2 * L; // 12+2*L ==> 12+2*6==> 24 // change to readonly
        private const int W = 1 << L; // width // 2^6 // change to readonly

        private BitArray[] RoundConst = new BitArray[ROUNDS];
        public SHA_3() => CalcRoundConst();
        private void CalcRoundConst() 
        {
            int RC(int t, ref int previousValue) // round const
            {
                int r = 0x1;
                int i = 0;
                if (previousValue != -1)
                {
                    r = previousValue;
                    i = t - 1;
                }
                for (; i < t; ++i)
                {
                    r <<= 1;
                    if ((r & 0x100) > 0)
                        r ^= 0x171;
                }
                previousValue = r;
                return r & 1;
            }

            int PreviousValue;
            for (int i = 0; i < ROUNDS; ++i)
            {
                RoundConst[i] = new BitArray(L + 1);
                PreviousValue = -1;
                for (int j = 0; j <= L; ++j)
                    RoundConst[i][j] = RC(i * 7 + j, ref PreviousValue) == 1;
            }
        }
        string BitArrayToBinaryString(ref BitArray bitArray)
        {
            string result = "";
            foreach (var i in bitArray)
                result += ((bool)i ? "1" : "0");
            return result;
        }
        string ToBinaryString(string text) => string.Join("", Encoding.ASCII.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
        private byte[] FormResult(ref BitArray State, int limit)
        {
            Console.WriteLine();
            byte[] result = new byte[32];
            int sum = (State[0]) ? 128 : 0;
            for (int i = 1; i < limit; ++i)
            {
                if (i % 8 == 0)
                {
                    result[i / 8 -1] = (byte)sum;
                    sum = 0;
                }
                sum += (State[i]) ? (128 >> (i % 8)) : 0;
            }
            result[31] = (byte)sum;
            return result;
        }
        private BitArray BitArrayToStupidBitArray(ref BitArray State)
        {
            BitArray newState = new BitArray(State.Length);
            for (int i = 0; i < State.Length / 8; ++i)
                for (int j = 7; j >= 0; --j)
                    newState[i * 8 + (7 - j)] = State[i * 8 + j];
            return newState;
        }
        private int mod(int value, int div) => (value % div + div) % div;
        private void KECCAK_P(ref BitArray InputState)
        {
            void Teta(ref BitArray State)
            {
                BitArray Cxz = new BitArray(5 * W);

                for (int x = 0; x < 5; ++x)
                    for (int z = 0; z < W; ++z)
                        Cxz[x * W + z] = State[(5 * 0 + x) * W + z] ^ State[(5 * 1 + x) * W + z] ^ State[(5 * 2 + x) * W + z] ^ State[(5 * 3 + x) * W + z] ^ State[(5 * 4 + x) * W + z];

                bool Dxz = false;
                for (int x = 0; x < 5; ++x)
                    for (int z = 0; z < W; ++z)
                    {
                        Dxz = Cxz[mod(x - 1, 5) * W + z] ^ Cxz[mod(x + 1, 5) * W + mod(z - 1, W)];
                        State[(5 * 0 + x) * W + z] ^= Dxz;
                        State[(5 * 1 + x) * W + z] ^= Dxz;
                        State[(5 * 2 + x) * W + z] ^= Dxz;
                        State[(5 * 3 + x) * W + z] ^= Dxz;
                        State[(5 * 4 + x) * W + z] ^= Dxz;
                    }
            }
            void Rho(ref BitArray State)
            {
                BitArray A_New = new BitArray(State.Length);

                for (int z = 0; z < W; ++z)
                    A_New[z] = State[z];

                int x = 1;
                int y = 0;
                int newX;
                int u;
                for (int t = 0; t < 24; ++t)
                {
                    u = (t + 1) * (t + 2) / 2;
                    for (int z = 0; z < W; ++z)
                        A_New[(5 * y + x) * W + z] = State[(5 * y + x) * W + mod(z - u, W)];

                    newX = y;
                    y = mod(2 * x + 3 * y, 5);
                    x = newX;
                }
                State = A_New;
            }
            void Pi(ref BitArray State)
            {
                BitArray A_New = new BitArray(State.Length);

                for (int y = 0; y < 5; ++y)
                    for (int x = 0; x < 5; ++x)
                        for (int z = 0; z < W; ++z)
                            A_New[(5 * y + x) * W + z] = State[(5 * x + mod(x + 3 * y, 5)) * W + z]; // A`[x,y,z] = A[(x+3y)mod5,x,z]
                State = A_New;
            }
            void Khi(ref BitArray State)
            {
                BitArray A_New = new BitArray(State.Length);
                for (int y = 0; y < 5; ++y)
                    for (int x = 0; x < 5; ++x)
                        for (int z = 0; z < W; ++z)
                            A_New[(5 * y + x) * W + z] = State[(5 * y + x) * W + z] ^ ((State[(5 * y + mod(x + 1, 5)) * W + z] ^ true) & State[(5 * y + mod(x + 2, 5)) * W + z]);
                State = A_New;
            }
            void Iota(ref BitArray State, int roundIndex)
            {
                for (int i = 0; i <= L; ++i)
                    State[(1 << i) - 1] ^= RoundConst[roundIndex][i];
            }

            for (int i = 0; i < ROUNDS; ++i)
            {
                Teta(ref InputState);
                Rho(ref InputState);
                Pi(ref InputState);
                Khi(ref InputState);
                Iota(ref InputState, i);
            }
        }
        private byte[] SPONGE_BOB(string message, int OutputBits)
        {
            string PAD10_1(int x, int m)
            {
                StringBuilder stringBuilder = new StringBuilder();

                int zerosCount = mod(-m - 2, x);
                if (zerosCount <= 0) zerosCount = 0;
                stringBuilder.Append('0', zerosCount);
                return "1" + stringBuilder + "1";
            }

            string P = message + PAD10_1(RATE, message.Length);
            int n = P.Length / RATE;

            int equalPartSize = P.Length / n;

            StringBuilder sb = new StringBuilder();
            sb.Append('0', CAPACITY);

            BitArray state = new BitArray(BITSIZE, false);
            BitArray P_subArray;

            for (int i = 0; i < n; ++i)
            {
                P_subArray = new BitArray((P.Substring(i * RATE, RATE) + sb).Select(c => c == '1').ToArray());
                state.Xor(P_subArray);
                KECCAK_P(ref state);
            }

            state = BitArrayToStupidBitArray(ref state);
            return FormResult(ref state, OutputBits);
        }
        public byte[] GetHashSHA3(string Message, bool isRawInput = false)
        {
            string mes = (isRawInput) ? Message : ToBinaryString(Message);
            return SPONGE_BOB(mes + "01", 256);
        }
    }
}
