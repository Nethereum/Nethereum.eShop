using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities
{
    public class CatalogItem : BaseEntity, IAggregateRoot
    {
        /// <summary>
        /// identififer for a product outside of the eShop
        /// in some cases it could be a globally understood id
        /// https://en.wikipedia.org/wiki/Global_Trade_Item_Number
        /// in others it may be specific to a particular product registry via the GtinRegistryId
        /// Max 14 chars
        /// </summary>
        public string Gtin { get; set; }

        /// <summary>
        /// The registry that relates to the GTIN
        /// Different products may have different registries
        /// </summary>
        public int? GtinRegistryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// The price in the eShop's base currency
        /// </summary>
        public decimal Price { get; set; }
        public int CatalogTypeId { get; set; }
        public CatalogType CatalogType { get; set; }
        public int CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }

        public string PictureUri { get; set; }

        /// <summary>
        /// The ipfs hash for the source product metadata
        /// Assume the product metadata might be in a JSON file
        /// </summary>
        public string ProductMetadataIpfsHash { get; set; }

        public string PictureSmallUri { get; set; }

        public string PictureMediumUri { get; set; }

        public string PictureLargeUri { get; set; }

        /// <summary>
        /// more detailed product info, likely to be key value pairs in JSON
        /// </summary>
        public string AttributeJson { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
    }
}