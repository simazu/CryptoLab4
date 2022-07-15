using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CryptoLab4.Lib
{
    public static class MD4Tester
    {
        static string fileName = "text.txt";
        public static (int[], int[]) AvalancheEffectTest(bool doSecondRound = true, bool doThirdRound = true,
                                                            uint A = 0x67452301, uint B = 0xefcdab89,
                                                            uint C = 0x98badcfe, uint D = 0x10325476)
        {
            byte[] message = Converter.FileToBytes(fileName);
            int[] numMessageChangedBits = new int[message.Length * 8];
            int[] numHashChangedBits = new int[message.Length * 8];
            MD4 md4 = new MD4(doSecondRound, doThirdRound, A, B, C, D);
            byte[] originalHash = md4.GetByteHashFromBytes(message);
            for (int bitNdx = 0; bitNdx < message.Length * 8; bitNdx++)
            {
                numMessageChangedBits[bitNdx] = bitNdx + 1;
                byte[] changedMessage = message;
                for (int j = 0; j < bitNdx / 8; j++)
                    changedMessage[j] = (byte)~changedMessage[j];
                for (int j = 0; j < bitNdx % 8; j++)
                    changedMessage[bitNdx / 8] = (byte)(changedMessage[bitNdx / 8]^(1 << (7 - j)));

                byte[] changedHash = md4.GetByteHashFromBytes(changedMessage);

                for (int i = 0;i < originalHash.Length; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        byte hashDifference = (byte)(originalHash[i] ^ changedHash[i]);
                        byte hashDifferencej = (byte)((hashDifference >> j) & 1);
                        numHashChangedBits[bitNdx] += (hashDifferencej == 1 ? 1 : 0);
                    }
                }
            }
            return (numMessageChangedBits, numHashChangedBits);
        }

        public static ((string, string), string, int) FindCollision(int messageLengthInBytes, int numHashBits)
        {
            (string, string) collision;
            string collisionHash;
            int numGeneratedMessages = 0;

            Dictionary<uint, string> messageShortHashes = new Dictionary<uint, string>();
            string message;
            MD4 md4 = new MD4();

            while (true)
            {
                numGeneratedMessages++;
                message = RandomString(messageLengthInBytes);
                byte[] hash = md4.GetByteHashFromString(message);

                byte[] shortHash = new byte[4];
                for (int j = 0; j < numHashBits / 8; j++)
                    shortHash[j] = hash[j];
                for (int j = 0; j < numHashBits % 8; j++)
                    shortHash[numHashBits / 8] = (byte)(hash[numHashBits / 8] & (1 << (7 - j)));

                uint a = BitConverter.ToUInt32(shortHash);
                if (messageShortHashes.Keys.Contains(a))
                {
                    collision = (messageShortHashes[a], message);
                    collisionHash = md4.GetHexHashFromString(message);
                    break;
                }
                else
                    messageShortHashes.Add(a, message);

            }
            return (collision, collisionHash, numGeneratedMessages);
        }

        public static (int[], (string, string)[], string[], int[]) CollisionsTest(int messageLengthInBytes)
        {
            int[] numHashBits = new int[32]; 
            (string, string)[] collisions = new (string, string)[32];
            string[] collisionHashes = new string[32];
            int[] numGeneratedMessages = new int[32];

            for (int k = 0; k < 32; k++)
            {
                ((string, string) collision, string hash, int numGenerated) = FindCollision(messageLengthInBytes, k + 1);
                numHashBits[k] = k + 1;
                collisions[k] = collision;
                collisionHashes[k] = hash;
                numGeneratedMessages[k] = numGenerated;
            }
            return (numHashBits, collisions, collisionHashes, numGeneratedMessages);
        }

        public static (string, string, int) FindPrototype(string message, int numHashBits)
        {
            string prototype;
            int numGeneratedMessages = 0;

            Random random = new Random();
            MD4 md4 = new MD4();
            byte[] hash = md4.GetByteHashFromString(message);
            byte[] shortOriginalHash = new byte[4];

            for (int j = 0; j < numHashBits / 8; j++)
                shortOriginalHash[j] = hash[j];
            for (int j = 0; j < numHashBits % 8; j++)
                shortOriginalHash[numHashBits / 8] = (byte)(hash[numHashBits / 8] & (1 << (7 - j)));

            while (true)
            {
                numGeneratedMessages++;
                string randomMessage = RandomString(random.Next(100));
                byte[] randomsHash = md4.GetByteHashFromString(randomMessage);

                byte[] shortRandomsHash = new byte[4];
                for (int j = 0; j < numHashBits / 8; j++)
                    shortRandomsHash[j] = randomsHash[j];
                for (int j = 0; j < numHashBits % 8; j++)
                    shortRandomsHash[numHashBits / 8] = (byte)(randomsHash[numHashBits / 8] & (1 << (7 - j)));

                uint a = BitConverter.ToUInt32(shortOriginalHash);
                uint b = BitConverter.ToUInt32(shortRandomsHash);
                if (a == b)
                {
                    prototype = randomMessage;
                    return (prototype, md4.GetHexHashFromString(randomMessage), numGeneratedMessages);
                }

            }
        }

        public static (int[], string[], int[]) PrototypesTest(string message)
        {
            int[] numHashBits = new int[24]; 
            string[] prototypes = new string[24];
            int[] numGeneratedMessages = new int[24];

            for (int k = 0; k < 24; k++)
            {
                numHashBits[k] = k + 1;
                (string prototype, string hash, int numGeneratedM) = FindPrototype(message, numHashBits[k]);
                prototypes[k] = prototype;
                numGeneratedMessages[k] = numGeneratedM;
            }
            return (numHashBits, prototypes, numGeneratedMessages);
        }

        public static (int[], int[]) HashingTimeFromMessageLength()
        {
            int numSamples = 200;
            int[] messageLengthInBits = new int[numSamples];
            int[] hashingTimes = new int[numSamples];
            Stopwatch watch = new Stopwatch();
            watch.Start();
            MD4 md4 = new MD4();
            byte[] message = Converter.FileToBytes(fileName);
            for (int i = 0; i < numSamples; i++)
            {
                byte[] shortMessage = new byte[(i + 1) * message.Length / numSamples];
                for (int j = 0; j < shortMessage.Length; j++)
                    shortMessage[j] = message[i];
                GC.Collect();
                watch.Restart();
                md4.GetByteHashFromBytes(shortMessage);
                watch.Stop();
                hashingTimes[i] = (int)watch.ElapsedTicks;
                messageLengthInBits[i] = shortMessage.Length * 8;
            }
            return (messageLengthInBits, hashingTimes);
        }

        static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnoprstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
