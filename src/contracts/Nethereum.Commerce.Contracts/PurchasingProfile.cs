using AutoMapper;
using Buyer = Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;
using Seller = Nethereum.Commerce.Contracts.WalletSeller.ContractDefinition;
using Purchase = Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.Contracts
{
    public class PurchasingProfile : Profile
    {
        public PurchasingProfile()
        {
            // PoStorage <=> WalletBuyer            
            this.CreateMap<Storage.Po, Buyer.Po>();
            this.CreateMap<Storage.PoItem, Buyer.PoItem>();
            this.CreateMap<Buyer.Po, Storage.Po>();
            this.CreateMap<Buyer.PoItem, Storage.PoItem>();

            // PoStorage <=> WalletSeller
            this.CreateMap<Storage.Po, Seller.Po>();
            this.CreateMap<Storage.PoItem, Seller.PoItem>();
            this.CreateMap<Seller.Po, Storage.Po>();
            this.CreateMap<Seller.PoItem, Storage.PoItem>();

            // PoStorage <=> Purchasing
            this.CreateMap<Storage.Po, Purchase.Po>();
            this.CreateMap<Storage.PoItem, Purchase.PoItem>();
            this.CreateMap<Purchase.Po, Storage.Po>();
            this.CreateMap<Purchase.PoItem, Storage.PoItem>();

            // WalletBuyer <=> WalletSeller
            this.CreateMap<Buyer.Po, Seller.Po>();
            this.CreateMap<Buyer.PoItem, Seller.PoItem>();
            this.CreateMap<Seller.Po, Buyer.Po>();
            this.CreateMap<Seller.PoItem, Buyer.PoItem>();
        }
    }
}