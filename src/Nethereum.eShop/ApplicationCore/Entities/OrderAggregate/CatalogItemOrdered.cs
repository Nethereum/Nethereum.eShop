using Ardalis.GuardClauses;

namespace Nethereum.eShop.ApplicationCore.Entities.OrderAggregate
{
    /// <summary>
    /// Represents a snapshot of the item that was ordered. If catalog item details change, details of
    /// the item that was part of a completed order should not change.
    /// </summary>
    public class CatalogItemOrdered // ValueObject
    {
        public int CatalogItemId { get; private set; }
        public string Gtin { get; private set; }
        public int? GtinRegistryId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }

        public CatalogItemOrdered(int catalogItemId, string gtin, int? gtinRegistryId, string productName, string pictureUri)
        {
            Guard.Against.OutOfRange(catalogItemId, nameof(catalogItemId), 1, int.MaxValue);
            Guard.Against.NullOrEmpty(productName, nameof(productName));
            Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));

            CatalogItemId = catalogItemId;
            ProductName = productName;
            PictureUri = pictureUri;
            Gtin = gtin;
            GtinRegistryId = gtinRegistryId;
        }

        private CatalogItemOrdered()
        {
            // required by EF
        }
    }
}
