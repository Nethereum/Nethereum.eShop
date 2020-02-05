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

namespace Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition
{


    public partial class WalletBuyerDeployment : WalletBuyerDeploymentBase
    {
        public WalletBuyerDeployment() : base(BYTECODE) { }
        public WalletBuyerDeployment(string byteCode) : base(byteCode) { }
    }

    public class WalletBuyerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50604051610a5c380380610a5c83398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b610982806100da6000396000f3fe608060405234801561001057600080fd5b50600436106100ea5760003560e01c8063abefab871161008c578063c8d303f811610066578063c8d303f8146101ab578063cc6e8a94146101cb578063f2fde38b146101de578063f3ad65f4146101f1576100ea565b8063abefab8714610183578063c076cfbf14610104578063c76ea97814610198576100ea565b80636b00e9d8116100c85780636b00e9d81461012a578063802706cb146101535780638da5cb5b146101665780638f32d59b1461017b576100ea565b8063150e99f9146100ef5780634f0dfe5b146101045780635b9b0e2614610117575b600080fd5b6101026100fd366004610549565b6101f9565b005b610102610112366004610684565b6102d4565b61010261012536600461062a565b6102d1565b61013d610138366004610549565b6102d8565b60405161014a919061083a565b60405180910390f35b610102610161366004610577565b6102ed565b61016e6102f3565b60405161014a9190610826565b61013d610302565b61018b610313565b60405161014a9190610845565b6101026101a6366004610549565b610319565b6101be6101b936600461066c565b6103ee565b60405161014a9190610883565b61018b6101d93660046105e0565b6103fb565b6101026101ec366004610549565b610404565b61016e610431565b610201610302565b6102265760405162461bcd60e51b815260040161021d9061084e565b60405180910390fd5b6001600160a01b03811660009081526001602052604090205460ff161561029c576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26102d1565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b5050565b60016020526000908152604090205460ff1681565b50505050565b6000546001600160a01b031690565b6000546001600160a01b0316331490565b60025481565b610321610302565b61033d5760405162461bcd60e51b815260040161021d9061084e565b6001600160a01b03811660009081526001602052604090205460ff1615610397576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26102d1565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b6103f661049b565b919050565b60009392505050565b61040c610302565b6104285760405162461bcd60e51b815260040161021d9061084e565b6102d181610440565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b6040805161018081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c081018290529060e082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b60008083601f840112610513578182fd5b50813567ffffffffffffffff81111561052a578182fd5b60208301915083602082850101111561054257600080fd5b9250929050565b60006020828403121561055a578081fd5b81356001600160a01b0381168114610570578182fd5b9392505050565b6000806000806040858703121561058c578283fd5b843567ffffffffffffffff808211156105a3578485fd5b6105af88838901610502565b909650945060208701359150808211156105c7578384fd5b506105d487828801610502565b95989497509550505050565b6000806000604084860312156105f4578283fd5b833567ffffffffffffffff81111561060a578384fd5b61061686828701610502565b909790965060209590950135949350505050565b60006020828403121561063b578081fd5b813567ffffffffffffffff811115610651578182fd5b8083016101808186031215610664578283fd5b949350505050565b60006020828403121561067d578081fd5b5035919050565b60008060408385031215610696578182fd5b82359150602083013560ff811681146106ad578182fd5b809150509250929050565b6001600160a01b03169052565b6000815180845260208401935060208301825b828110156107ed5781518051875260208101516106f8602089018261081f565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e088015261010080820151610748828a01826106b8565b50506101208181015190880152610140808201519088015261016080820151610773828a01826106b8565b505061018080820151610788828a018261080b565b50506101a081810151908801526101c080820151908801526101e08082015190880152610200808201516107be828a01826107f7565b5050610220808201516107d3828a01826107fd565b5050506102409590950194602091909101906001016106d8565b5093949350505050565b15159052565b6004811061080757fe5b9052565b6009811061080757fe5b6003811061080757fe5b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b6000602082528251602083015260208301516108a260408401826106b8565b5060408301516108b560608401826106b8565b5060608301516108c860808401826106b8565b50608083015160a083015260a083015160c083015260c08301516108ef60e08401826106b8565b5060e083015161010061090481850183610815565b840151610120848101919091528401516101408085019190915284015190506101606109328185018361081f565b8401516101808481015290506106646101a08401826106c556fea2646970667358221220315d54b837a272c3f20be97f5d0449a048d28489dfba448fc13f34dc0920787b64736f6c63430006010033";
        public WalletBuyerDeploymentBase() : base(BYTECODE) { }
        public WalletBuyerDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "contractAddressOfRegistry", 1)]
        public virtual string ContractAddressOfRegistry { get; set; }
    }

    public partial class BoundAddressCountFunction : BoundAddressCountFunctionBase { }

    [Function("BoundAddressCount", "int256")]
    public class BoundAddressCountFunctionBase : FunctionMessage
    {

    }

    public partial class BoundAddressesFunction : BoundAddressesFunctionBase { }

    [Function("BoundAddresses", "bool")]
    public class BoundAddressesFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AddressRegistryFunction : AddressRegistryFunctionBase { }

    [Function("addressRegistry", "address")]
    public class AddressRegistryFunctionBase : FunctionMessage
    {

    }

    public partial class BindAddressFunction : BindAddressFunctionBase { }

    [Function("bindAddress")]
    public class BindAddressFunctionBase : FunctionMessage
    {
        [Parameter("address", "a", 1)]
        public virtual string A { get; set; }
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
        [Parameter("string", "nameOfPurchasing", 1)]
        public virtual string NameOfPurchasing { get; set; }
        [Parameter("string", "nameOfFunding", 2)]
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

    public partial class GetPoNumberBySellerAndQuoteFunction : GetPoNumberBySellerAndQuoteFunctionBase { }

    [Function("getPoNumberBySellerAndQuote", "uint256")]
    public class GetPoNumberBySellerAndQuoteFunctionBase : FunctionMessage
    {
        [Parameter("string", "sellerIdString", 1)]
        public virtual string SellerIdString { get; set; }
        [Parameter("uint256", "quoteId", 2)]
        public virtual BigInteger QuoteId { get; set; }
    }

    public partial class IsOwnerFunction : IsOwnerFunctionBase { }

    [Function("isOwner", "bool")]
    public class IsOwnerFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class SetPoItemGoodsReceivedFunction : SetPoItemGoodsReceivedFunctionBase { }

    [Function("setPoItemGoodsReceived")]
    public class SetPoItemGoodsReceivedFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class UnBindAddressFunction : UnBindAddressFunctionBase { }

    [Function("unBindAddress")]
    public class UnBindAddressFunctionBase : FunctionMessage
    {
        [Parameter("address", "a", 1)]
        public virtual string A { get; set; }
    }

    public partial class AddressAlreadyBoundEventDTO : AddressAlreadyBoundEventDTOBase { }

    [Event("AddressAlreadyBound")]
    public class AddressAlreadyBoundEventDTOBase : IEventDTO
    {
        [Parameter("address", "a", 1, true )]
        public virtual string A { get; set; }
    }

    public partial class AddressAlreadyUnBoundEventDTO : AddressAlreadyUnBoundEventDTOBase { }

    [Event("AddressAlreadyUnBound")]
    public class AddressAlreadyUnBoundEventDTOBase : IEventDTO
    {
        [Parameter("address", "a", 1, true )]
        public virtual string A { get; set; }
    }

    public partial class AddressBoundEventDTO : AddressBoundEventDTOBase { }

    [Event("AddressBound")]
    public class AddressBoundEventDTOBase : IEventDTO
    {
        [Parameter("address", "a", 1, true )]
        public virtual string A { get; set; }
    }

    public partial class AddressUnBoundEventDTO : AddressUnBoundEventDTOBase { }

    [Event("AddressUnBound")]
    public class AddressUnBoundEventDTOBase : IEventDTO
    {
        [Parameter("address", "a", 1, true )]
        public virtual string A { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true )]
        public virtual string PreviousOwner { get; set; }
        [Parameter("address", "newOwner", 2, true )]
        public virtual string NewOwner { get; set; }
    }

    public partial class BoundAddressCountOutputDTO : BoundAddressCountOutputDTOBase { }

    [FunctionOutput]
    public class BoundAddressCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class BoundAddressesOutputDTO : BoundAddressesOutputDTOBase { }

    [FunctionOutput]
    public class BoundAddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class AddressRegistryOutputDTO : AddressRegistryOutputDTOBase { }

    [FunctionOutput]
    public class AddressRegistryOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }









    public partial class GetPoOutputDTO : GetPoOutputDTOBase { }

    [FunctionOutput]
    public class GetPoOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class GetPoNumberBySellerAndQuoteOutputDTO : GetPoNumberBySellerAndQuoteOutputDTOBase { }

    [FunctionOutput]
    public class GetPoNumberBySellerAndQuoteOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
    }

    public partial class IsOwnerOutputDTO : IsOwnerOutputDTOBase { }

    [FunctionOutput]
    public class IsOwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }






}
