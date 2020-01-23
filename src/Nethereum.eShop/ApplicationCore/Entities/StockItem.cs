namespace Nethereum.eShop.ApplicationCore.Entities
{
    public class StockItem: BaseEntity
    {
        public string GTIN { get; set; }
        /// <summary>
        /// warehouse, shop, shelf etc etc
        /// </summary>
        public string Location { get; set; }
        public int Quantity { get; set; }

    }
}
