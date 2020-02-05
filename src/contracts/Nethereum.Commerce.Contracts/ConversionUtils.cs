using Nethereum.ABI.Decoders;
using Nethereum.ABI.Encoders;

namespace Nethereum.Commerce.Contracts
{
    public static class ConversionUtils
    {
        private static Bytes32TypeEncoder _encoder;
        private static StringBytes32Decoder _decoder;

        static ConversionUtils()
        {
            _encoder = new Bytes32TypeEncoder();
            _decoder = new StringBytes32Decoder();
        }

        public static byte[] ConvertStringToBytes32Array(string s)
        {
            if (s == null) return null;
            return _encoder.Encode(s);
        }

        public static string ConvertBytes32ArrayToString(byte[] b)
        {
            if (b == null) return null;
            return _decoder.Decode(b);
        }
    }
}
