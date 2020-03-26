using AutoMapper;
using Nethereum.ABI;
using Nethereum.ABI.Decoders;
using Nethereum.ABI.Encoders;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Numerics;
using Buyer = Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition;
using Purchase = Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Seller = Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.Contracts
{
    /// <summary>
    /// Extension methods for:
    ///   Converting byte[] to string and back
    ///   Converting POs defined in one namespace to another namespace, eg WalletBuyer PO to PoStorage PO (uses automap).
    /// </summary>
    public static class PurchasingExtensions
    {
        private static Bytes32TypeEncoder _encoder;
        private static StringBytes32Decoder _decoder;
        private static Mapper _mapper;

        static PurchasingExtensions()
        {
            _encoder = new Bytes32TypeEncoder();
            _decoder = new StringBytes32Decoder();

            // Mapping POs
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PurchasingProfile>();
            });
            _mapper = new Mapper(config);
        }

        public static byte[] ConvertToBytes32(this string s)
        {
            if (s == null) return null;
            return _encoder.Encode(s);
        }

        public static string ConvertToString(this byte[] b)
        {
            if (b == null) return null;
            return _decoder.Decode(b);
        }

        public static BigInteger GetTotalCurrencyValue(this Buyer.Po po)
        {
            if (po == null) return 0;
            if (po.PoItems == null) return 0;
            if (po.PoItems.Count == 0) return 0;

            BigInteger total = 0;
            for (int i = 0; i < po.PoItems.Count; i++)
            {
                total += po.PoItems[i].CurrencyValue;
            }
            return total;
        }

        public static byte[] GetSignatureBytes(this Buyer.Po po, IWeb3 web3)
        {
            if (!(web3.TransactionManager.Account is Account a)) throw new NotSupportedException("Account object needed to expose private key");
            return po.GetSignatureBytes(a.PrivateKey);
        }

        public static byte[] GetSignatureBytes(this Buyer.Po po, string privateKeyHex)
        {
            privateKeyHex = privateKeyHex.EnsureHexPrefix();
            var signature = GetSignatureHexString(po, privateKeyHex);
            return signature.HexToByteArray();
        }

        public static string GetSignatureHexString(this Buyer.Po po, string privateKeyHex)
        {
            privateKeyHex = privateKeyHex.EnsureHexPrefix();
            var hashEncoded = new ABIEncode().GetSha3ABIEncoded(new ABIValue(new TupleType(), po));
            var signature = new EthereumMessageSigner().Sign(hashEncoded, privateKeyHex);
            return signature;
        }        

        // PoStorage <=> WalletBuyer        
        public static Storage.Po ToStoragePo(this Buyer.Po po) { return _mapper.Map<Storage.Po>(po); }
        public static Buyer.Po ToBuyerPo(this Storage.Po po) { return _mapper.Map<Buyer.Po>(po); }

        // PoStorage <=> WalletSeller
        public static Storage.Po ToStoragePo(this Seller.Po po) { return _mapper.Map<Storage.Po>(po); }
        public static Seller.Po ToSellerPo(this Storage.Po po) { return _mapper.Map<Seller.Po>(po); }

        // PoStorage <=> Purchasing
        public static Storage.Po ToStoragePo(this Purchase.Po po) { return _mapper.Map<Storage.Po>(po); }
        public static Purchase.Po ToPurchasingPo(this Storage.Po po) { return _mapper.Map<Purchase.Po>(po); }
        public static Storage.PoItem ToStoragePoItem(this Purchase.PoItem poItem) { return _mapper.Map<Storage.PoItem>(poItem); }
        public static Purchase.PoItem ToPurchasingPoItem(this Storage.PoItem poItem) { return _mapper.Map<Purchase.PoItem>(poItem); }

        // WalletBuyer <=> WalletSeller
        public static Buyer.Po ToBuyerPo(this Seller.Po po) { return _mapper.Map<Buyer.Po>(po); }
        public static Seller.Po ToSellerPo(this Buyer.Po po) { return _mapper.Map<Seller.Po>(po); }

        // WalletBuyer <=> Purchasing
        public static Buyer.Po ToBuyerPo(this Purchase.Po po) { return _mapper.Map<Buyer.Po>(po); }
        public static Purchase.Po ToPurchasingPo(this Buyer.Po po) { return _mapper.Map<Purchase.Po>(po); }
    }
}
