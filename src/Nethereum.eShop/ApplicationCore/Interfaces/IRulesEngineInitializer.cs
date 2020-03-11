namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRulesEngineInitializer
    {
        string GetQuoteBizRulesFileUrl();

        string GetQuoteItemBizRulesFileUrl();

        int GetBizEngineRetriesOnFailure();
    }
}
