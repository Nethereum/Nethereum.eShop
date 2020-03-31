using AutoMapper;
using Buyer = Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition;
using Seller = Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;
using Purchase = Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.Contracts
{
    public class PurchasingProfile : Profile
    {
        public PurchasingProfile()
        {
            // PoStorage <=> Buyer            
            this.CreateMap<Storage.Po, Buyer.Po>();
            this.CreateMap<Storage.PoItem, Buyer.PoItem>();
            this.CreateMap<Buyer.Po, Storage.Po>();
            this.CreateMap<Buyer.PoItem, Storage.PoItem>();

            // PoStorage <=> Seller
            this.CreateMap<Storage.Po, Seller.Po>();
            this.CreateMap<Storage.PoItem, Seller.PoItem>();
            this.CreateMap<Seller.Po, Storage.Po>();
            this.CreateMap<Seller.PoItem, Storage.PoItem>();

            // PoStorage <=> Purchasing
            this.CreateMap<Storage.Po, Purchase.Po>();
            this.CreateMap<Storage.PoItem, Purchase.PoItem>();
            this.CreateMap<Purchase.Po, Storage.Po>();
            this.CreateMap<Purchase.PoItem, Storage.PoItem>();

            // Buyer <=> Seller
            this.CreateMap<Buyer.Po, Seller.Po>();
            this.CreateMap<Buyer.PoItem, Seller.PoItem>();
            this.CreateMap<Seller.Po, Buyer.Po>();
            this.CreateMap<Seller.PoItem, Buyer.PoItem>();

            // Buyer <=> Purchasing
            this.CreateMap<Buyer.Po, Purchase.Po>();
            this.CreateMap<Buyer.PoItem, Purchase.PoItem>();
            this.CreateMap<Purchase.Po, Buyer.Po>();
            this.CreateMap<Purchase.PoItem, Buyer.PoItem>();
        }
    }
}