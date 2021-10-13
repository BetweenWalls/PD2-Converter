﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D2SLib.IO
{
    public class BitReader : IDisposable
    {
        BitArray _bits;
        public int Position { get; private set; }

        public BitReader(byte[] bytes)
        {
            Position = 0;
            _bits = new BitArray(bytes);
        }
        public bool ReadBit()
        {
            return _bits[Position++];
        }

        public byte[] ReadBits(int numberOfBits)
        {
            byte[] bytes = new byte[(numberOfBits-1)/8+1];
            int byteIndex = 0;
            int bitIndex = 0;
            for(int i = 0; i < numberOfBits; i++)
            {
                if(_bits[Position + i])
                {
                    bytes[byteIndex] |= (byte)(1 << bitIndex);
                }
                bitIndex++;
                if(bitIndex == 8)
                {
                    byteIndex++;
                    bitIndex = 0;
                }
            }
            Position += numberOfBits;
            return bytes;
        }

        public byte[] ReadBytes(int numberOfBytes)
        {
            return ReadBits(numberOfBytes * 8);
        }

        public byte ReadByte(int bits)
        {
            byte[] bytes = ReadBits(bits);
            Array.Resize<byte>(ref bytes, 1);
            return bytes[0];
        }

        public byte ReadByte()
        {
            return ReadBytes(1)[0];
        }

        public UInt16 ReadUInt16(int bits)
        {
            byte[] bytes = ReadBits(bits);
            Array.Resize<byte>(ref bytes, 2);
            return BitConverter.ToUInt16(bytes, 0); ;
        }

        public UInt16 ReadUInt16()
        {
            return BitConverter.ToUInt16(ReadBytes(2), 0);
        }

        public UInt32 ReadUInt32(int bits)
        {
            byte[] bytes = ReadBits(bits);
            Array.Resize<byte>(ref bytes, 4);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public UInt32 ReadUInt32()
        {
            return BitConverter.ToUInt32(ReadBytes(4), 0);
        }

        public Int32 ReadInt32(int bits)
        {
            byte[] bytes = ReadBits(bits);
            Array.Resize<byte>(ref bytes, 4);
            return BitConverter.ToInt32(bytes, 0);
        }

        public Int32 ReadInt32()
        {
            return BitConverter.ToInt32(ReadBytes(4), 0);
        }

        public string ReadString(int bytes)
        {
            return System.Text.Encoding.ASCII.GetString(ReadBytes(bytes)).Trim('\0');
        }

        public void SeekBits(int bitPosition)
        {
            Position = bitPosition;
        }
        public void Seek(int bytePostion)
        {
            SeekBits(bytePostion * 8);
        }

        public void Align()
        {
            Position = (Position + 7) & ~7;
        }

        public void Dispose()
        {
            _bits = null;
        }
    }
}
