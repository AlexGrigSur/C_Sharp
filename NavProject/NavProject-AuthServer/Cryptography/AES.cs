﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES_256
{
    // [1] https://bit.nmu.org.ua/ua/student/metod/cryptology/%D0%BB%D0%B5%D0%BA%D1%86%D0%B8%D1%8F%209.pdf AES lecture
    // [2] https://sites.math.washington.edu/~morrow/336_12/papers/juan.pdf Galois Field in Cryptography
    // [3] https://habr.com/ru/post/112733/ AES realization via C#
    // [4] https://www.lirmm.fr/arith18/papers/kobayashi-AlgorithmInversionUsingPolynomialMultiplyInstruction.pdf galois inverted algo
    // [5] http://tratliff.webspace.wheatoncollege.edu/2016_Fall/math202/inclass/sep21_inclass.pdf inverse table for GF(2^8)
    // [7] https://kavaliro.com/wp-content/uploads/2014/03/AES.pdf aes encryption example
    // [8] https://nvlpubs.nist.gov/nistpubs/FIPS/NIST.FIPS.197.pdf FIPS 197

    // Mode: AES-256
    // Current single core encode speed: 
    // Decode speed: 
    public class AES
    {
        private const int ROUND_COUNT = 14;
        private const int ROUND_KEY_SIZE = 32;
        private readonly byte[] subBytesTransformTable = new byte[] {    0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76,
                                                                0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0,
                                                                0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15,
                                                                0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75,
                                                                0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84,
                                                                0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf,
                                                                0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8,
                                                                0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2,
                                                                0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73,
                                                                0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb,
                                                                0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79,
                                                                0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08,
                                                                0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a,
                                                                0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e,
                                                                0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf,
                                                                0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 };
        private readonly byte[] invSubBytesTransformTable = new byte[] { 0x52,0x09,0x6a,0xd5,0x30,0x36,0xa5,0x38,0xbf,0x40,0xa3,0x9e,0x81,0xf3,0xd7,0xfb,
                                                                0x7c,0xe3,0x39,0x82,0x9b,0x2f,0xff,0x87,0x34,0x8e,0x43,0x44,0xc4,0xde,0xe9,0xcb,
                                                                0x54,0x7b,0x94,0x32,0xa6,0xc2,0x23,0x3d,0xee,0x4c,0x95,0x0b,0x42,0xfa,0xc3,0x4e,
                                                                0x08,0x2e,0xa1,0x66,0x28,0xd9,0x24,0xb2,0x76,0x5b,0xa2,0x49,0x6d,0x8b,0xd1,0x25,
                                                                0x72,0xf8,0xf6,0x64,0x86,0x68,0x98,0x16,0xd4,0xa4,0x5c,0xcc,0x5d,0x65,0xb6,0x92,
                                                                0x6c,0x70,0x48,0x50,0xfd,0xed,0xb9,0xda,0x5e,0x15,0x46,0x57,0xa7,0x8d,0x9d,0x84,
                                                                0x90,0xd8,0xab,0x00,0x8c,0xbc,0xd3,0x0a,0xf7,0xe4,0x58,0x05,0xb8,0xb3,0x45,0x06,
                                                                0xd0,0x2c,0x1e,0x8f,0xca,0x3f,0x0f,0x02,0xc1,0xaf,0xbd,0x03,0x01,0x13,0x8a,0x6b,
                                                                0x3a,0x91,0x11,0x41,0x4f,0x67,0xdc,0xea,0x97,0xf2,0xcf,0xce,0xf0,0xb4,0xe6,0x73,
                                                                0x96,0xac,0x74,0x22,0xe7,0xad,0x35,0x85,0xe2,0xf9,0x37,0xe8,0x1c,0x75,0xdf,0x6e,
                                                                0x47,0xf1,0x1a,0x71,0x1d,0x29,0xc5,0x89,0x6f,0xb7,0x62,0x0e,0xaa,0x18,0xbe,0x1b,
                                                                0xfc,0x56,0x3e,0x4b,0xc6,0xd2,0x79,0x20,0x9a,0xdb,0xc0,0xfe,0x78,0xcd,0x5a,0xf4,
                                                                0x1f,0xdd,0xa8,0x33,0x88,0x07,0xc7,0x31,0xb1,0x12,0x10,0x59,0x27,0x80,0xec,0x5f,
                                                                0x60,0x51,0x7f,0xa9,0x19,0xb5,0x4a,0x0d,0x2d,0xe5,0x7a,0x9f,0x93,0xc9,0x9c,0xef,
                                                                0xa0,0xe0,0x3b,0x4d,0xae,0x2a,0xf5,0xb0,0xc8,0xeb,0xbb,0x3c,0x83,0x53,0x99,0x61,
                                                                0x17,0x2b,0x04,0x7e,0xba,0x77,0xd6,0x26,0xe1,0x69,0x14,0x63,0x55,0x21,0x0c,0x7d,};

        private readonly byte[] mult2 = new byte[] { 0x00,0x02,0x04,0x06,0x08,0x0a,0x0c,0x0e,0x10,0x12,0x14,0x16,0x18,0x1a,0x1c,0x1e,
                                                    0x20,0x22,0x24,0x26,0x28,0x2a,0x2c,0x2e,0x30,0x32,0x34,0x36,0x38,0x3a,0x3c,0x3e,
                                                    0x40,0x42,0x44,0x46,0x48,0x4a,0x4c,0x4e,0x50,0x52,0x54,0x56,0x58,0x5a,0x5c,0x5e,
                                                    0x60,0x62,0x64,0x66,0x68,0x6a,0x6c,0x6e,0x70,0x72,0x74,0x76,0x78,0x7a,0x7c,0x7e,
                                                    0x80,0x82,0x84,0x86,0x88,0x8a,0x8c,0x8e,0x90,0x92,0x94,0x96,0x98,0x9a,0x9c,0x9e,
                                                    0xa0,0xa2,0xa4,0xa6,0xa8,0xaa,0xac,0xae,0xb0,0xb2,0xb4,0xb6,0xb8,0xba,0xbc,0xbe,
                                                    0xc0,0xc2,0xc4,0xc6,0xc8,0xca,0xcc,0xce,0xd0,0xd2,0xd4,0xd6,0xd8,0xda,0xdc,0xde,
                                                    0xe0,0xe2,0xe4,0xe6,0xe8,0xea,0xec,0xee,0xf0,0xf2,0xf4,0xf6,0xf8,0xfa,0xfc,0xfe,
                                                    0x1b,0x19,0x1f,0x1d,0x13,0x11,0x17,0x15,0x0b,0x09,0x0f,0x0d,0x03,0x01,0x07,0x05,
                                                    0x3b,0x39,0x3f,0x3d,0x33,0x31,0x37,0x35,0x2b,0x29,0x2f,0x2d,0x23,0x21,0x27,0x25,
                                                    0x5b,0x59,0x5f,0x5d,0x53,0x51,0x57,0x55,0x4b,0x49,0x4f,0x4d,0x43,0x41,0x47,0x45,
                                                    0x7b,0x79,0x7f,0x7d,0x73,0x71,0x77,0x75,0x6b,0x69,0x6f,0x6d,0x63,0x61,0x67,0x65,
                                                    0x9b,0x99,0x9f,0x9d,0x93,0x91,0x97,0x95,0x8b,0x89,0x8f,0x8d,0x83,0x81,0x87,0x85,
                                                    0xbb,0xb9,0xbf,0xbd,0xb3,0xb1,0xb7,0xb5,0xab,0xa9,0xaf,0xad,0xa3,0xa1,0xa7,0xa5,
                                                    0xdb,0xd9,0xdf,0xdd,0xd3,0xd1,0xd7,0xd5,0xcb,0xc9,0xcf,0xcd,0xc3,0xc1,0xc7,0xc5,
                                                    0xfb,0xf9,0xff,0xfd,0xf3,0xf1,0xf7,0xf5,0xeb,0xe9,0xef,0xed,0xe3,0xe1,0xe7,0xe5
        };
        private readonly byte[] mult9 = new byte[] { 0x00,0x09,0x12,0x1b,0x24,0x2d,0x36,0x3f,0x48,0x41,0x5a,0x53,0x6c,0x65,0x7e,0x77,
                                            0x90,0x99,0x82,0x8b,0xb4,0xbd,0xa6,0xaf,0xd8,0xd1,0xca,0xc3,0xfc,0xf5,0xee,0xe7,
                                            0x3b,0x32,0x29,0x20,0x1f,0x16,0x0d,0x04,0x73,0x7a,0x61,0x68,0x57,0x5e,0x45,0x4c,
                                            0xab,0xa2,0xb9,0xb0,0x8f,0x86,0x9d,0x94,0xe3,0xea,0xf1,0xf8,0xc7,0xce,0xd5,0xdc,
                                            0x76,0x7f,0x64,0x6d,0x52,0x5b,0x40,0x49,0x3e,0x37,0x2c,0x25,0x1a,0x13,0x08,0x01,
                                            0xe6,0xef,0xf4,0xfd,0xc2,0xcb,0xd0,0xd9,0xae,0xa7,0xbc,0xb5,0x8a,0x83,0x98,0x91,
                                            0x4d,0x44,0x5f,0x56,0x69,0x60,0x7b,0x72,0x05,0x0c,0x17,0x1e,0x21,0x28,0x33,0x3a,
                                            0xdd,0xd4,0xcf,0xc6,0xf9,0xf0,0xeb,0xe2,0x95,0x9c,0x87,0x8e,0xb1,0xb8,0xa3,0xaa,
                                            0xec,0xe5,0xfe,0xf7,0xc8,0xc1,0xda,0xd3,0xa4,0xad,0xb6,0xbf,0x80,0x89,0x92,0x9b,
                                            0x7c,0x75,0x6e,0x67,0x58,0x51,0x4a,0x43,0x34,0x3d,0x26,0x2f,0x10,0x19,0x02,0x0b,
                                            0xd7,0xde,0xc5,0xcc,0xf3,0xfa,0xe1,0xe8,0x9f,0x96,0x8d,0x84,0xbb,0xb2,0xa9,0xa0,
                                            0x47,0x4e,0x55,0x5c,0x63,0x6a,0x71,0x78,0x0f,0x06,0x1d,0x14,0x2b,0x22,0x39,0x30,
                                            0x9a,0x93,0x88,0x81,0xbe,0xb7,0xac,0xa5,0xd2,0xdb,0xc0,0xc9,0xf6,0xff,0xe4,0xed,
                                            0x0a,0x03,0x18,0x11,0x2e,0x27,0x3c,0x35,0x42,0x4b,0x50,0x59,0x66,0x6f,0x74,0x7d,
                                            0xa1,0xa8,0xb3,0xba,0x85,0x8c,0x97,0x9e,0xe9,0xe0,0xfb,0xf2,0xcd,0xc4,0xdf,0xd6,
                                            0x31,0x38,0x23,0x2a,0x15,0x1c,0x07,0x0e,0x79,0x70,0x6b,0x62,0x5d,0x54,0x4f,0x46};
        private readonly byte[] mult11 = new byte[] { 0x00,0x0b,0x16,0x1d,0x2c,0x27,0x3a,0x31,0x58,0x53,0x4e,0x45,0x74,0x7f,0x62,0x69,
                                            0xb0,0xbb,0xa6,0xad,0x9c,0x97,0x8a,0x81,0xe8,0xe3,0xfe,0xf5,0xc4,0xcf,0xd2,0xd9,
                                            0x7b,0x70,0x6d,0x66,0x57,0x5c,0x41,0x4a,0x23,0x28,0x35,0x3e,0x0f,0x04,0x19,0x12,
                                            0xcb,0xc0,0xdd,0xd6,0xe7,0xec,0xf1,0xfa,0x93,0x98,0x85,0x8e,0xbf,0xb4,0xa9,0xa2,
                                            0xf6,0xfd,0xe0,0xeb,0xda,0xd1,0xcc,0xc7,0xae,0xa5,0xb8,0xb3,0x82,0x89,0x94,0x9f,
                                            0x46,0x4d,0x50,0x5b,0x6a,0x61,0x7c,0x77,0x1e,0x15,0x08,0x03,0x32,0x39,0x24,0x2f,
                                            0x8d,0x86,0x9b,0x90,0xa1,0xaa,0xb7,0xbc,0xd5,0xde,0xc3,0xc8,0xf9,0xf2,0xef,0xe4,
                                            0x3d,0x36,0x2b,0x20,0x11,0x1a,0x07,0x0c,0x65,0x6e,0x73,0x78,0x49,0x42,0x5f,0x54,
                                            0xf7,0xfc,0xe1,0xea,0xdb,0xd0,0xcd,0xc6,0xaf,0xa4,0xb9,0xb2,0x83,0x88,0x95,0x9e,
                                            0x47,0x4c,0x51,0x5a,0x6b,0x60,0x7d,0x76,0x1f,0x14,0x09,0x02,0x33,0x38,0x25,0x2e,
                                            0x8c,0x87,0x9a,0x91,0xa0,0xab,0xb6,0xbd,0xd4,0xdf,0xc2,0xc9,0xf8,0xf3,0xee,0xe5,
                                            0x3c,0x37,0x2a,0x21,0x10,0x1b,0x06,0x0d,0x64,0x6f,0x72,0x79,0x48,0x43,0x5e,0x55,
                                            0x01,0x0a,0x17,0x1c,0x2d,0x26,0x3b,0x30,0x59,0x52,0x4f,0x44,0x75,0x7e,0x63,0x68,
                                            0xb1,0xba,0xa7,0xac,0x9d,0x96,0x8b,0x80,0xe9,0xe2,0xff,0xf4,0xc5,0xce,0xd3,0xd8,
                                            0x7a,0x71,0x6c,0x67,0x56,0x5d,0x40,0x4b,0x22,0x29,0x34,0x3f,0x0e,0x05,0x18,0x13,
                                            0xca,0xc1,0xdc,0xd7,0xe6,0xed,0xf0,0xfb,0x92,0x99,0x84,0x8f,0xbe,0xb5,0xa8,0xa3};
        private readonly byte[] mult13 = new byte[] { 0x00,0x0d,0x1a,0x17,0x34,0x39,0x2e,0x23,0x68,0x65,0x72,0x7f,0x5c,0x51,0x46,0x4b,
                                            0xd0,0xdd,0xca,0xc7,0xe4,0xe9,0xfe,0xf3,0xb8,0xb5,0xa2,0xaf,0x8c,0x81,0x96,0x9b,
                                            0xbb,0xb6,0xa1,0xac,0x8f,0x82,0x95,0x98,0xd3,0xde,0xc9,0xc4,0xe7,0xea,0xfd,0xf0,
                                            0x6b,0x66,0x71,0x7c,0x5f,0x52,0x45,0x48,0x03,0x0e,0x19,0x14,0x37,0x3a,0x2d,0x20,
                                            0x6d,0x60,0x77,0x7a,0x59,0x54,0x43,0x4e,0x05,0x08,0x1f,0x12,0x31,0x3c,0x2b,0x26,
                                            0xbd,0xb0,0xa7,0xaa,0x89,0x84,0x93,0x9e,0xd5,0xd8,0xcf,0xc2,0xe1,0xec,0xfb,0xf6,
                                            0xd6,0xdb,0xcc,0xc1,0xe2,0xef,0xf8,0xf5,0xbe,0xb3,0xa4,0xa9,0x8a,0x87,0x90,0x9d,
                                            0x06,0x0b,0x1c,0x11,0x32,0x3f,0x28,0x25,0x6e,0x63,0x74,0x79,0x5a,0x57,0x40,0x4d,
                                            0xda,0xd7,0xc0,0xcd,0xee,0xe3,0xf4,0xf9,0xb2,0xbf,0xa8,0xa5,0x86,0x8b,0x9c,0x91,
                                            0x0a,0x07,0x10,0x1d,0x3e,0x33,0x24,0x29,0x62,0x6f,0x78,0x75,0x56,0x5b,0x4c,0x41,
                                            0x61,0x6c,0x7b,0x76,0x55,0x58,0x4f,0x42,0x09,0x04,0x13,0x1e,0x3d,0x30,0x27,0x2a,
                                            0xb1,0xbc,0xab,0xa6,0x85,0x88,0x9f,0x92,0xd9,0xd4,0xc3,0xce,0xed,0xe0,0xf7,0xfa,
                                            0xb7,0xba,0xad,0xa0,0x83,0x8e,0x99,0x94,0xdf,0xd2,0xc5,0xc8,0xeb,0xe6,0xf1,0xfc,
                                            0x67,0x6a,0x7d,0x70,0x53,0x5e,0x49,0x44,0x0f,0x02,0x15,0x18,0x3b,0x36,0x21,0x2c,
                                            0x0c,0x01,0x16,0x1b,0x38,0x35,0x22,0x2f,0x64,0x69,0x7e,0x73,0x50,0x5d,0x4a,0x47,
                                            0xdc,0xd1,0xc6,0xcb,0xe8,0xe5,0xf2,0xff,0xb4,0xb9,0xae,0xa3,0x80,0x8d,0x9a,0x97};
        private readonly byte[] mult14 = new byte[] { 0x00,0x0e,0x1c,0x12,0x38,0x36,0x24,0x2a,0x70,0x7e,0x6c,0x62,0x48,0x46,0x54,0x5a,
                                            0xe0,0xee,0xfc,0xf2,0xd8,0xd6,0xc4,0xca,0x90,0x9e,0x8c,0x82,0xa8,0xa6,0xb4,0xba,
                                            0xdb,0xd5,0xc7,0xc9,0xe3,0xed,0xff,0xf1,0xab,0xa5,0xb7,0xb9,0x93,0x9d,0x8f,0x81,
                                            0x3b,0x35,0x27,0x29,0x03,0x0d,0x1f,0x11,0x4b,0x45,0x57,0x59,0x73,0x7d,0x6f,0x61,
                                            0xad,0xa3,0xb1,0xbf,0x95,0x9b,0x89,0x87,0xdd,0xd3,0xc1,0xcf,0xe5,0xeb,0xf9,0xf7,
                                            0x4d,0x43,0x51,0x5f,0x75,0x7b,0x69,0x67,0x3d,0x33,0x21,0x2f,0x05,0x0b,0x19,0x17,
                                            0x76,0x78,0x6a,0x64,0x4e,0x40,0x52,0x5c,0x06,0x08,0x1a,0x14,0x3e,0x30,0x22,0x2c,
                                            0x96,0x98,0x8a,0x84,0xae,0xa0,0xb2,0xbc,0xe6,0xe8,0xfa,0xf4,0xde,0xd0,0xc2,0xcc,
                                            0x41,0x4f,0x5d,0x53,0x79,0x77,0x65,0x6b,0x31,0x3f,0x2d,0x23,0x09,0x07,0x15,0x1b,
                                            0xa1,0xaf,0xbd,0xb3,0x99,0x97,0x85,0x8b,0xd1,0xdf,0xcd,0xc3,0xe9,0xe7,0xf5,0xfb,
                                            0x9a,0x94,0x86,0x88,0xa2,0xac,0xbe,0xb0,0xea,0xe4,0xf6,0xf8,0xd2,0xdc,0xce,0xc0,
                                            0x7a,0x74,0x66,0x68,0x42,0x4c,0x5e,0x50,0x0a,0x04,0x16,0x18,0x32,0x3c,0x2e,0x20,
                                            0xec,0xe2,0xf0,0xfe,0xd4,0xda,0xc8,0xc6,0x9c,0x92,0x80,0x8e,0xa4,0xaa,0xb8,0xb6,
                                            0x0c,0x02,0x10,0x1e,0x34,0x3a,0x28,0x26,0x7c,0x72,0x60,0x6e,0x44,0x4a,0x58,0x56,
                                            0x37,0x39,0x2b,0x25,0x0f,0x01,0x13,0x1d,0x47,0x49,0x5b,0x55,0x7f,0x71,0x63,0x6d,
                                            0xd7,0xd9,0xcb,0xc5,0xef,0xe1,0xf3,0xfd,0xa7,0xa9,0xbb,0xb5,0x9f,0x91,0x83,0x8d};

        private byte[] StateTable;
        private byte[] byteKey;
        SHA_3 sha3;
        public AES()
        {
            //CalcMults();
            //CalcSubBytesTransTable();
            sha3 = new SHA_3();
        }

        private byte[] hexStringToByteArray(string hexString) => Enumerable.Range(0, hexString.Length / 2).Select(x => Convert.ToByte(hexString.Substring(x * 2, 2), 16)).ToArray();
        private string byteArrayToString(byte[] byteArray) => Encoding.ASCII.GetString(byteArray);
        private string byteArrayToHexString(ref byte[] byteArray) => String.Join("", byteArray.Select(x => Convert.ToString(x, 16).PadLeft(2, '0')));
        private byte[] PadMessage(byte[] byteMessage)
        {
            byte[] result = new byte[(byteMessage.Length / 16 + 1) * 16];
            byteMessage.CopyTo(result, 0);
            byte difference = (byte)(result.Length - byteMessage.Length);
            //result.Where(x => x >= byteMessage.Length).Append(difference);
            for (int i = byteMessage.Length; i < result.Length; ++i)
                result[i] = difference;
            return result;
        }

        public string Encrypt(string baseText, string key)
        {
            StateTable = PadMessage(Encoding.ASCII.GetBytes(baseText));
            byteKey = new byte[(ROUND_COUNT + 1) * ROUND_KEY_SIZE / 2];
            Stopwatch sw = new Stopwatch();
            sw.Start();
            byte[] tempArray = sha3.GetHashSHA3(key);
            sw.Stop();
            Console.WriteLine($"Hash code time: {sw.ElapsedMilliseconds}");
            for (int i = 0; i < tempArray.Length; ++i)
                byteKey[i] = tempArray[i];
            return EncryptProcess();
        }
        public string Encrypt(string baseText, byte[] key)
        {
            StateTable = PadMessage(Encoding.ASCII.GetBytes(baseText));
            byteKey = new byte[(ROUND_COUNT + 1) * ROUND_KEY_SIZE / 2];
            for (int i = 0; i < key.Length; ++i)
                byteKey[i] = key[i];
            return EncryptProcess();
        }
        public string Encrypt(byte[] baseText, byte[] key)
        {
            StateTable = PadMessage(baseText);
            byteKey = new byte[(ROUND_COUNT + 1) * ROUND_KEY_SIZE / 2];
            for (int i = 0; i < key.Length; ++i)
                byteKey[i] = key[i];
            return EncryptProcess();
        }
        private string EncryptProcess()
        {
            void SubShift()
            {
                byte[] tempArray = new byte[4];
                for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
                {
                    StateTable[currentBlock * 16] = subBytesTransformTable[StateTable[currentBlock * 16]];
                    StateTable[currentBlock * 16 + 4] = subBytesTransformTable[StateTable[currentBlock * 16 + 4]];
                    StateTable[currentBlock * 16 + 8] = subBytesTransformTable[StateTable[currentBlock * 16 + 8]];
                    StateTable[currentBlock * 16 + 12] = subBytesTransformTable[StateTable[currentBlock * 16 + 12]];
                    for (int j = 1; j < 4; ++j)
                    {
                        int i;
                        for (i = 0; i < 4; ++i)
                            tempArray[i] = subBytesTransformTable[StateTable[currentBlock * 16 + i * 4 + j]];

                        for (i = 0; i < j; ++i)
                            StateTable[currentBlock * 16 + 16 - 4 * j + j + i * 4] = tempArray[i];

                        for (i = j; i < 4; ++i)
                            StateTable[currentBlock * 16 + j + (i - j) * 4] = tempArray[i];
                    }
                }
            }
            void MixAdd(int AddOffset)
            {
                byte[] Cx = new byte[] { 2, 3, 1, 1, 1, 2, 3, 1, 1, 1, 2, 3, 3, 1, 1, 2 };
                byte[] resultColumn = new byte[4];
                byte sum;
                for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
                {
                    for (int i = 0; i < 4; ++i) // MixColumns and AddRoundKey
                    {
                        for (int j = 0; j < 4; ++j)
                        {
                            sum = 0;
                            for (int z = 0; z < 4; ++z)
                            {
                                if (Cx[j * 4 + z] >> 1 == 1)
                                    sum ^= mult2[StateTable[currentBlock * 16 + i * 4 + z]];
                                //sum ^= (StateTable[currentBlock * 16 + i * 4 + z] >> 7 == 0) ? (byte)(StateTable[currentBlock * 16 + i * 4 + z] << 1) : (byte)(StateTable[currentBlock * 16 + i * 4 + z] << 1 ^ 283);
                                if ((Cx[j * 4 + z] & 1) == 1)
                                    sum ^= StateTable[currentBlock * 16 + i * 4 + z];
                            }
                            resultColumn[j] = sum;
                        }
                        for (int j = 0; j < 4; ++j)
                            StateTable[currentBlock * 16 + i * 4 + j] = (byte)(resultColumn[j] ^ byteKey[AddOffset + i * 4 + j]);
                    }
                }
            }
            void SubShiftMixAdd(int AddOffset)
            {
                SubShift();
                MixAdd(AddOffset);
            }

            UpdateRoundKeys();

            AddRoundKey(0);// first round

            for (int i = 1; i < ROUND_COUNT; ++i) // main rounds
                SubShiftMixAdd(i * 16);

            SubShift();
            AddRoundKey(ROUND_COUNT * 16);

            return byteArrayToHexString(ref StateTable);
        }

        public string Decrypt(byte[] EncryptedText, byte[] key)
        {
            StateTable = EncryptedText;
            byteKey = new byte[(ROUND_COUNT + 1) * ROUND_KEY_SIZE];
            for (int i = 0; i < key.Length; ++i)
                byteKey[i] = key[i];
            return DecryptProcess();
        }
        public string Decrypt(byte[] EncryptedText, string key)
        {
            StateTable = EncryptedText;
            byteKey = new byte[(ROUND_COUNT + 1) * ROUND_KEY_SIZE];
            byte[] tempArray = sha3.GetHashSHA3(key);

            for (int i = 0; i < tempArray.Length; ++i)
                byteKey[i] = tempArray[i];

            return DecryptProcess();
        }
        public string Decrypt(string EncryptedText, string key)
        {
            StateTable = hexStringToByteArray(EncryptedText);
            byteKey = new byte[(ROUND_COUNT + 1) * ROUND_KEY_SIZE];
            byte[] tempArray = sha3.GetHashSHA3(key);

            for (int i = 0; i < tempArray.Length; ++i)
                byteKey[i] = tempArray[i];

            return DecryptProcess();
        }
        private string DecryptProcess()
        {
            void InvShiftSubAdd(int AddOffset)
            {
                byte[] tempArray = new byte[4];
                for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
                {
                    for (int i = 0; i < 4; ++i) // ShiftRows, SubBytes And AddRoundKey
                        StateTable[currentBlock * 16 + i * 4] = (byte)(invSubBytesTransformTable[StateTable[currentBlock * 16 + i * 4]] ^ byteKey[AddOffset + i * 4]);

                    for (int j = 1; j < 4; ++j)
                    {
                        for (int i = 0; i < 4; ++i)
                            tempArray[i] = invSubBytesTransformTable[StateTable[currentBlock * 16 + i * 4 + j]];

                        for (int i = 0; i < j; ++i)
                            StateTable[currentBlock * 16 + (i + 1) * 4 - 4 + j] = (byte)(tempArray[4 - j + i] ^ byteKey[AddOffset + (i + 1) * 4 - 4 + j]);

                        for (int i = j; i < 4; ++i)
                            StateTable[currentBlock * 16 + 4 * i + j] = (byte)(tempArray[i - j] ^ byteKey[AddOffset + 4 * i + j]);
                    }
                }
            }
            void InvShiftSubAddMix(int AddOffset)
            {
                byte[] Cx = new byte[] { 14, 11, 13, 9, 9, 14, 11, 13, 13, 9, 14, 11, 11, 13, 9, 14 };
                byte[] resultColumn = new byte[4];

                InvShiftSubAdd(AddOffset);
                for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
                {
                    for (int i = 0; i < 4; ++i) // MixColumns
                    {
                        for (int j = 0; j < 4; ++j)
                        {
                            byte sum = 0;
                            byte currentSTvalue;
                            for (int z = 0; z < 4; ++z)
                            {
                                currentSTvalue = StateTable[currentBlock * 16 + i * 4 + z];
                                sum ^= Cx[j * 4 + z] switch
                                {
                                    9 => mult9[currentSTvalue], // poly x9  ==>  (((x*2)*2)*2)+x
                                    11 => mult11[currentSTvalue],// poly x11 ==> ((((x*2)*2)+x)*2)+x
                                    13 => mult13[currentSTvalue], // poly x13 ==> ((((x*2)+x)*2)*2)+x 
                                    14 => mult14[currentSTvalue] // poly x14 ==> ((((x*2)+x)*2)+x)*2
                                };
                            }
                            resultColumn[j] = sum;
                        }
                        for (int j = 0; j < 4; ++j)
                            StateTable[currentBlock * 16 + i * 4 + j] = resultColumn[j];
                    }
                }
            }


            UpdateRoundKeys();
            AddRoundKey(ROUND_COUNT * 16); // first round
            for (int i = ROUND_COUNT - 1; i > 0; --i)
                InvShiftSubAddMix(i * 16);

            InvShiftSubAdd(0);

            try
            {
                return byteArrayToString(Enumerable.Range(0, StateTable.Length - StateTable.Last()).Select(x => StateTable[x]).ToArray());
            }
            catch
            {
                return "Wrong Password";
            }
        }

        private void AddRoundKey(int offset)
        {
            for (int i = 0; i < StateTable.Length; ++i)
                StateTable[i] ^= byteKey[offset + (i % 16)];
        }

        private void UpdateRoundKeys() // done
        {
            byte roundConst = 1;

            for (int i = 1; i < (ROUND_COUNT + 1) / 2; ++i) // 0..224
            {
                if (i - 1 != 0) // Calc Round Const
                    roundConst = (roundConst < 128) ? (byte)(roundConst << 1) : (byte)(roundConst << 1 ^ 283);

                byteKey[i * 32] = (byte)(byteKey[i * 32] ^ (subBytesTransformTable[byteKey[i * 32 - 3]] ^ roundConst));
                byteKey[i * 32 + 1] = (byte)(byteKey[i * 32 + 1] ^ subBytesTransformTable[byteKey[i * 32 - 2]]);
                byteKey[i * 32 + 2] = (byte)(byteKey[i * 32 + 2] ^ subBytesTransformTable[byteKey[i * 32 - 1]]);
                byteKey[i * 32 + 3] = (byte)(byteKey[i * 32 + 2] ^ subBytesTransformTable[byteKey[i * 32 - 4]]);

                for (int j = 4; j < 16; ++j) // calc others
                    byteKey[i * 32 + j] ^= (byte)(byteKey[(i - 1) * 32 + j] ^ byteKey[i * 32 + j - 4]);

                // subwords every 4-th keyword
                for (int j = 16; j < 20; ++j)
                    byteKey[i * 32 + j] ^= (byte)(byteKey[(i - 1) * 32 + j] ^ subBytesTransformTable[byteKey[i * 32 + j - 4]]);

                for (int j = 20; j < 32; ++j)
                    byteKey[i * 32 + j] ^= (byte)(byteKey[(i - 1) * 32 + j] ^ byteKey[i * 32 + j - 4]);
            }
            // 224..240
            roundConst = (roundConst < 128) ? (byte)(roundConst << 1) : (byte)(roundConst << 1 ^ 283); // Calc Round Const

            // Calc first key word
            byteKey[7 * 32] = (byte)(byteKey[7 * 32] ^ (subBytesTransformTable[byteKey[7 * 32 - 3]] ^ roundConst));
            byteKey[7 * 32 + 1] = (byte)(byteKey[7 * 32 + 1] ^ subBytesTransformTable[byteKey[7 * 32 - 2]]);
            byteKey[7 * 32 + 2] = (byte)(byteKey[7 * 32 + 2] ^ subBytesTransformTable[byteKey[7 * 32 - 1]]);
            byteKey[7 * 32 + 3] = (byte)(byteKey[7 * 32 + 2] ^ subBytesTransformTable[byteKey[7 * 32 - 4]]);

            for (int j = 4; j < 16; ++j) // calc others
                byteKey[7 * 32 + j] ^= (byte)(byteKey[(7 - 1) * 32 + j] ^ byteKey[7 * 32 + j - 4]);
        }
    }
    /* 
    Забери на память То что есть сейчас Поцелуемся В последний раз
    */
}
#region obsolete

//private void CalcMults()
//{
//    byte mult(byte value)
//    {
//        return (value >> 7 == 0) ? (byte)(value << 1) : (byte)(value << 1 ^ 283);
//    }
//    for (byte i = 0; i < 255; ++i)
//    {
//        mult9[i] = (byte)(mult(mult(mult(i))) ^ i);
//        mult11[i] = (byte)(mult((byte)(mult(mult(i)) ^ i)) ^ i);
//        mult13[i] = (byte)(mult(mult((byte)(mult(i) ^ i))) ^ i);
//        mult14[i] = mult((byte)(mult((byte)(mult(i) ^ i)) ^ i));
//    }
//    mult9[255] = (byte)(mult(mult(mult(255))) ^ 255);
//    mult11[255] = (byte)(mult((byte)(mult(mult(255)) ^ 255)) ^ 255);
//    mult13[255] = (byte)(mult(mult((byte)(mult(255) ^ 255))) ^ 255);
//    mult14[255] = mult((byte)(mult((byte)(mult(255) ^ 255)) ^ 255));
//}

//private void CalcSubBytesTransTable() // done
//{
//    void CalcGaloisInverted(ref byte?[] GaloisInvertedTable)//Dictionary<byte, byte> GaloisInvertedTable) // done
//    {
//        // Gx - неприводимый полином степени m ==> Sx = Gx
//        // Ax - полиномиальное представление нашего числа ==> Rx = Ax
//        byte GaloisInverted(byte baseValue, int irreduciblePoly = 283) // done [4]
//        {
//            int deg(int polynom)
//            {
//                for (byte i = 8; i >= 1; --i)
//                    if ((polynom >> i & 1) == 1)
//                        return i;
//                return 0;
//            }
//            if (baseValue == 0) return 0;
//            int Sx = irreduciblePoly;// x^8 + x^4 + x^3 + x + 1  ==> 100011011
//            int Vx = 0;
//            int Rx = baseValue;
//            int Ux = 1;
//            int delta;
//            int temp;

//            while (Rx >= 2)
//            {
//                delta = deg(Sx) - deg(Rx);
//                if (delta < 0)
//                {
//                    temp = Sx;
//                    Sx = Rx;
//                    Rx = temp;

//                    temp = Vx;
//                    Vx = Ux;
//                    Ux = temp;

//                    delta *= -1;
//                }
//                Sx ^= (Rx << delta);
//                Vx ^= (Ux << delta);
//            }
//            return Convert.ToByte(Ux);
//        }
//        byte result;
//        GaloisInvertedTable[0] = 0;
//        GaloisInvertedTable[1] = 1;
//        for (int i = 2; i < 256; ++i)
//        {
//            if (GaloisInvertedTable[i].HasValue)
//                continue;
//            result = GaloisInverted((byte)i);
//            GaloisInvertedTable[i] = result;
//            GaloisInvertedTable[result] = (byte)i;
//        }
//    }
//    byte?[] galoisInvertedTable = new byte?[256];
//    CalcGaloisInverted(ref galoisInvertedTable);
//    // Ax ==> a(x) = x^4 + x^3 + x^2 + x + 1
//    byte[] Ax = new byte[] { 0b10001111,
//                                     0b11000111,
//                                     0b11100011,
//                                     0b11110001,
//                                     0b11111000,
//                                     0b01111100,
//                                     0b00111110,
//                                     0b00011111 };
//    // Bx ==> b(x) = x^6 + x^5 + x + 1 ==> 63 in base16 ==> 99 in base10
//    byte Bx = 0b01100011;
//    byte X;
//    byte sum;
//    for (int i = 0; i <= 255; ++i)
//    {
//        X = galoisInvertedTable[i].Value;
//        byte result = 0;
//        byte multiply = 1;
//        for (int j = 0; j < 8; ++j)
//        {
//            sum = 0;
//            for (int z = 0; z < 8; ++z)
//                sum ^= (byte)(Ax[j] >> (7 - z) & 1 & (X >> z & 1));
//            result += (byte)((sum ^ (Bx >> j & 1)) * multiply);
//            multiply <<= 1;
//        }
//        subBytesTransformTable[i] = result;
//        invSubBytesTransformTable[result] = (byte)i;
//    }
//}

//private void SubBytesPerformer(Dictionary<byte, byte> subByteTable) // done
//{
//    for (int i = 0; i < StateTable.Length; ++i)
//        StateTable[i] = subByteTable[StateTable[i]];
//}
//private void MixColumns() // done
//{
//    byte[] Cx = new byte[] { 2, 3, 1, 1, 1, 2, 3, 1, 1, 1, 2, 3, 3, 1, 1, 2 };
//    byte[] resultColumn = new byte[4];
//    byte sum;
//    for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
//    {
//        for (int i = 0; i < 4; ++i)
//        {
//            for (int j = 0; j < 4; ++j)
//            {
//                sum = 0;
//                for (int z = 0; z < 4; ++z)
//                {
//                    if ((Cx[j * 4 + z] >> 1 & 1) == 1)
//                        sum ^= (StateTable[currentBlock * 16 + i * 4 + z] < 128) ? (byte)(StateTable[currentBlock * 16 + i * 4 + z] << 1) : (byte)(StateTable[currentBlock * 16 + i * 4 + z] << 1 ^ 283);
//                    if ((Cx[j * 4 + z] & 1) == 1)
//                        sum ^= StateTable[currentBlock * 16 + i * 4 + z];
//                }
//                resultColumn[j] = sum;
//            }
//            for (int j = 0; j < 4; ++j)
//                StateTable[currentBlock * 16 + i * 4 + j] = resultColumn[j];
//        }
//    }
//}
//private void InvSubBytes() => SubBytesPerformer(invSubBytesTransformTable);

//public void ShiftRows() // Done
//{
//    for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
//    {
//        byte[] tempArray = new byte[4];
//        for (int j = 1; j < 4; ++j)
//        {
//            int i;
//            for (i = 0; i < 4; ++i)
//                tempArray[i] = StateTable[currentBlock * 16 + i * 4 + j];

//            for (i = 0; i < j; ++i)
//                StateTable[currentBlock * 16 + (4 - j) * 4 + j + i * 4] = tempArray[i];

//            for (i = j; i < 4; ++i)
//                StateTable[currentBlock * 16 + j + (i - j) * 4] = tempArray[i];
//        }
//    }
//}
//public void InvShiftRows() // done
//{
//    for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
//    {
//        byte[] tempArray = new byte[4];
//        for (int j = 1; j < 4; ++j)
//        {
//            int i;
//            for (i = 0; i < 4; ++i)
//                tempArray[i] = StateTable[currentBlock * 16 + i * 4 + j];

//            for (i = 0; i < j; ++i)
//                StateTable[currentBlock * 16 + (i + 1) * 4 - 4 + j] = tempArray[4 - j + i];

//            for (i = j; i < 4; ++i)
//                StateTable[currentBlock * 16 + 4 * i + j] = tempArray[i - j];
//        }
//    }
//}

//private void SubShiftAdd(int AddOffset)
//{
//    byte[] tempArray = new byte[4];
//    for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
//    {
//        StateTable[currentBlock * 16] = (byte)(subBytesTransformTable[StateTable[currentBlock * 16]] ^ byteKey[AddOffset]);
//        StateTable[currentBlock * 16 + 4] = (byte)(subBytesTransformTable[StateTable[currentBlock * 16 + 4]] ^ byteKey[AddOffset + 4]);
//        StateTable[currentBlock * 16 + 8] = (byte)(subBytesTransformTable[StateTable[currentBlock * 16 + 8]] ^ byteKey[AddOffset + 8]);
//        StateTable[currentBlock * 16 + 12] = (byte)(subBytesTransformTable[StateTable[currentBlock * 16 + 12]] ^ byteKey[AddOffset + 12]);
//        for (int j = 1; j < 4; ++j)
//        {
//            int i;
//            for (i = 0; i < 4; ++i)
//                tempArray[i] = (byte)(subBytesTransformTable[StateTable[currentBlock * 16 + i * 4 + j]] ^ byteKey[AddOffset + i * 4 + j]);

//            for (i = 0; i < j; ++i)
//                StateTable[currentBlock * 16 + 16 - 4 * j + j + i * 4] = tempArray[i];

//            for (i = j; i < 4; ++i)
//                StateTable[currentBlock * 16 + j + (i - j) * 4] = tempArray[i];
//        }
//    }
//}

//private void InvMixColumns() // done
//{
//    byte mult(byte value)
//    {
//        return (value < 128) ? (byte)(value << 1) : (byte)(value << 1 ^ 283);
//    }

//    byte[] Cx = new byte[] { 14, 11, 13, 9, 9, 14, 11, 13, 13, 9, 14, 11, 11, 13, 9, 14 };
//    byte[] resultColumn = new byte[4];
//    for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
//    {
//        for (int i = 0; i < 4; ++i)
//        {
//            for (int j = 0; j < 4; ++j)
//            {
//                byte sum = 0;
//                byte currentSTvalue;
//                for (int z = 0; z < 4; ++z)
//                {
//                    currentSTvalue = StateTable[currentBlock * 16 + i * 4 + z];
//                    switch (Cx[j * 4 + z])
//                    {
//                        case 9: // poly x9  ==>  (((x*2)*2)*2)+x
//                            {
//                                sum ^= (byte)(mult(mult(mult(currentSTvalue))) ^ currentSTvalue);
//                                break;
//                            }
//                        case 11: // poly x11 ==> ((((x*2)*2)+x)*2)+x
//                            {
//                                sum ^= (byte)(mult((byte)(mult(mult(currentSTvalue)) ^ currentSTvalue)) ^ currentSTvalue);
//                                break;
//                            }
//                        case 13: // poly x13 ==> ((((x*2)+x)*2)*2)+x
//                            {
//                                sum ^= (byte)(mult(mult((byte)(mult(currentSTvalue) ^ currentSTvalue))) ^ currentSTvalue);
//                                break;
//                            }
//                        case 14: // poly x14 ==> ((((x*2)+x)*2)+x)*2
//                            {
//                                sum ^= mult((byte)(mult((byte)(mult(currentSTvalue) ^ currentSTvalue)) ^ currentSTvalue));
//                                break;
//                            }
//                    }
//                }
//                resultColumn[j] = sum;
//            }
//            for (int j = 0; j < 4; ++j)
//                StateTable[currentBlock * 16 + i * 4 + j] = resultColumn[j];
//        }
//    }
//}

//private void MixAdd(int AddOffset)
//{
//    byte[] Cx = new byte[] { 2, 3, 1, 1, 1, 2, 3, 1, 1, 1, 2, 3, 3, 1, 1, 2 };
//    byte[] resultColumn = new byte[4];
//    byte sum;
//    for (int currentBlock = 0; currentBlock < StateTable.Length / 16; ++currentBlock)
//    {
//        for (int i = 0; i < 4; ++i)
//        {
//            for (int j = 0; j < 4; ++j)
//            {
//                sum = 0;
//                for (int z = 0; z < 4; ++z)
//                {
//                    if (Cx[j * 4 + z] >> 1 == 1)
//                        sum ^= (StateTable[currentBlock * 16 + i * 4 + z] < 128) ? (byte)(StateTable[currentBlock * 16 + i * 4 + z] << 1) : (byte)(StateTable[currentBlock * 16 + i * 4 + z] << 1 ^ 283);
//                    if ((Cx[j * 4 + z] & 1) == 1)
//                        sum ^= StateTable[currentBlock * 16 + i * 4 + z];
//                }
//                resultColumn[j] = sum;
//            }
//            for (int j = 0; j < 4; ++j)
//                StateTable[currentBlock * 16 + i * 4 + j] = (byte)(resultColumn[j] ^ byteKey[AddOffset + i * 4 + j]);
//        }
//    }
//}
#endregion