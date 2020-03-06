namespace Nethereum.eShop.ApplicationCore.Queries
{
    public class PaginationArgs
    {
        public int Offset { get; set; } = 0;

        public int Fetch { get; set; } = 50;

        public string SortBy { get; set; }

        public bool SortDescending { get; set; }
    }
}
