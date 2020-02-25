using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Nethereum.Commerce.Contracts.IPurchasing.ContractDefinition
{


    public partial class IPurchasingDeployment : IPurchasingDeploymentBase
    {
        public IPurchasingDeployment() : base(BYTECODE) { }
        public IPurchasingDeployment(string byteCode) : base(byteCode) { }
    }

    public class IPurchasingDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public IPurchasingDeploymentBase() : base(BYTECODE) { }
        public IPurchasingDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CancelPurchaseOrderItemFunction : CancelPurchaseOrderItemFunctionBase { }

    [Function("cancelPurchaseOrderItem")]
    public class CancelPurchaseOrderItemFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class ConfigureFunction : ConfigureFunctionBase { }

    [Function("configure")]
    public class ConfigureFunctionBase : FunctionMessage
    {
        [Parameter("string", "nameOfPoStorage", 1)]
        public virtual string NameOfPoStorage { get; set; }
        [Parameter("string", "nameOfBusinessPartnerStorage", 2)]
        public virtual string NameOfBusinessPartnerStorage { get; set; }
        [Parameter("string", "nameOfFunding", 3)]
        public virtual string NameOfFunding { get; set; }
    }

    public partial class CreatePurchaseOrderFunction : CreatePurchaseOrderFunctionBase { }

    [Function("createPurchaseOrder")]
    public class CreatePurchaseOrderFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class GetPoFunction : GetPoFunctionBase { }

    [Function("getPo", typeof(GetPoOutputDTO))]
    public class GetPoFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
    }

    public partial class GetPoBySellerAndQuoteFunction : GetPoBySellerAndQuoteFunctionBase { }

    [Function("getPoBySellerAndQuote", typeof(GetPoBySellerAndQuoteOutputDTO))]
    public class GetPoBySellerAndQuoteFunctionBase : FunctionMessage
    {
        [Parameter("string", "sellerIdString", 1)]
        public virtual string SellerIdString { get; set; }
        [Parameter("uint256", "quoteId", 2)]
        public virtual BigInteger QuoteId { get; set; }
    }

    public partial class SetPoItemAcceptedFunction : SetPoItemAcceptedFunctionBase { }

    [Function("setPoItemAccepted")]
    public class SetPoItemAcceptedFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
        [Parameter("bytes32", "soNumber", 3)]
        public virtual byte[] SoNumber { get; set; }
        [Parameter("bytes32", "soItemNumber", 4)]
        public virtual byte[] SoItemNumber { get; set; }
    }

    public partial class SetPoItemCompletedFunction : SetPoItemCompletedFunctionBase { }

    [Function("setPoItemCompleted")]
    public class SetPoItemCompletedFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class SetPoItemGoodsIssuedFunction : SetPoItemGoodsIssuedFunctionBase { }

    [Function("setPoItemGoodsIssued")]
    public class SetPoItemGoodsIssuedFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class SetPoItemGoodsReceivedBuyerFunction : SetPoItemGoodsReceivedBuyerFunctionBase { }

    [Function("setPoItemGoodsReceivedBuyer")]
    public class SetPoItemGoodsReceivedBuyerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class SetPoItemGoodsReceivedSellerFunction : SetPoItemGoodsReceivedSellerFunctionBase { }

    [Function("setPoItemGoodsReceivedSeller")]
    public class SetPoItemGoodsReceivedSellerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class SetPoItemReadyForGoodsIssueFunction : SetPoItemReadyForGoodsIssueFunctionBase { }

    [Function("setPoItemReadyForGoodsIssue")]
    public class SetPoItemReadyForGoodsIssueFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class SetPoItemRejectedFunction : SetPoItemRejectedFunctionBase { }

    [Function("setPoItemRejected")]
    public class SetPoItemRejectedFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class PurchaseItemAcceptedLogEventDTO : PurchaseItemAcceptedLogEventDTOBase { }

    [Event("PurchaseItemAcceptedLog")]
    public class PurchaseItemAcceptedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemCancelledLogEventDTO : PurchaseItemCancelledLogEventDTOBase { }

    [Event("PurchaseItemCancelledLog")]
    public class PurchaseItemCancelledLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemCompletedLogEventDTO : PurchaseItemCompletedLogEventDTOBase { }

    [Event("PurchaseItemCompletedLog")]
    public class PurchaseItemCompletedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemEscrowReleasedLogEventDTO : PurchaseItemEscrowReleasedLogEventDTOBase { }

    [Event("PurchaseItemEscrowReleasedLog")]
    public class PurchaseItemEscrowReleasedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemGoodsIssuedLogEventDTO : PurchaseItemGoodsIssuedLogEventDTOBase { }

    [Event("PurchaseItemGoodsIssuedLog")]
    public class PurchaseItemGoodsIssuedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemGoodsReceivedLogEventDTO : PurchaseItemGoodsReceivedLogEventDTOBase { }

    [Event("PurchaseItemGoodsReceivedLog")]
    public class PurchaseItemGoodsReceivedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemReadyForGoodsIssueLogEventDTO : PurchaseItemReadyForGoodsIssueLogEventDTOBase { }

    [Event("PurchaseItemReadyForGoodsIssueLog")]
    public class PurchaseItemReadyForGoodsIssueLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemRejectedLogEventDTO : PurchaseItemRejectedLogEventDTOBase { }

    [Event("PurchaseItemRejectedLog")]
    public class PurchaseItemRejectedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseOrderCreateRequestLogEventDTO : PurchaseOrderCreateRequestLogEventDTOBase { }

    [Event("PurchaseOrderCreateRequestLog")]
    public class PurchaseOrderCreateRequestLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
    }

    public partial class PurchaseOrderCreatedLogEventDTO : PurchaseOrderCreatedLogEventDTOBase { }

    [Event("PurchaseOrderCreatedLog")]
    public class PurchaseOrderCreatedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
    }







    public partial class GetPoOutputDTO : GetPoOutputDTOBase { }

    [FunctionOutput]
    public class GetPoOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class GetPoBySellerAndQuoteOutputDTO : GetPoBySellerAndQuoteOutputDTOBase { }

    [FunctionOutput]
    public class GetPoBySellerAndQuoteOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }














}
