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

namespace Nethereum.Commerce.Contracts.Purchasing.ContractDefinition
{


    public partial class PurchasingDeployment : PurchasingDeploymentBase
    {
        public PurchasingDeployment() : base(BYTECODE) { }
        public PurchasingDeployment(string byteCode) : base(byteCode) { }
    }

    public class PurchasingDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516116d13803806116d183398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b6115f7806100da6000396000f3fe608060405234801561001057600080fd5b50600436106101285760003560e01c806306ac2d3d1461012d5780630d9192ef14610142578063150e99f9146101555780633dafca6e1461014257806346885b5b146101425780634ab2930a146101685780634ac5042f146101425780636b00e9d8146101865780636cc2c111146101a65780636fee6fec146101b95780638da5cb5b146101d95780638e5fc30b146101e15780638f32d59b1461020157806391aa0f3014610209578063abefab871461021c578063b4bf807f14610231578063c076cfbf14610142578063c76ea97814610239578063c8d303f81461024c578063cb4c86b71461025f578063cd882a0014610142578063cfb5192814610267578063f2fde38b1461027a578063f3ad65f41461028d575b600080fd5b61014061013b366004610db8565b610295565b005b6101406101503660046110c7565b6104dd565b610140610163366004610d58565b6104e1565b6101706105b3565b60405161017d919061128b565b60405180910390f35b610199610194366004610d58565b6105c2565b60405161017d919061129f565b6101406101b43660046110f6565b6105d7565b6101cc6101c7366004610e4d565b6105dd565b60405161017d919061148b565b61017061073b565b6101f46101ef366004610d97565b61074a565b60405161017d91906112f0565b610199610874565b610140610217366004610f1f565b6105b0565b610224610885565b60405161017d91906112aa565b61017061088b565b610140610247366004610d58565b61089a565b6101cc61025a366004611097565b61096f565b610170610a02565b610224610275366004610e96565b610a11565b610140610288366004610d58565b610a2f565b610170610a5c565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906102c790899089906004016112c1565b60206040518083038186803b1580156102df57600080fd5b505afa1580156102f3573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103179190810190610d7b565b600480546001600160a01b0319166001600160a01b0392831617908190551661035b5760405162461bcd60e51b815260040161035290611396565b60405180910390fd5b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061038d90879087906004016112c1565b60206040518083038186803b1580156103a557600080fd5b505afa1580156103b9573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103dd9190810190610d7b565b600580546001600160a01b0319166001600160a01b039283161790819055166104185760405162461bcd60e51b8152600401610352906113ec565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061044a90859085906004016112c1565b60206040518083038186803b15801561046257600080fd5b505afa158015610476573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061049a9190810190610d7b565b600680546001600160a01b0319166001600160a01b039283161790819055166104d55760405162461bcd60e51b815260040161035290611343565b505050505050565b5050565b6104e9610874565b6105055760405162461bcd60e51b815260040161035290611456565b6001600160a01b03811660009081526001602052604090205460ff161561057b576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26105b0565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b6004546001600160a01b031681565b60016020526000908152604090205460ff1681565b50505050565b6105e5610ac6565b600061062685858080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250610a1192505050565b60048054604051631edee79b60e21b81529293506000926001600160a01b0390911691637b7b9e6c9161065d9186918991016112b3565b60206040518083038186803b15801561067557600080fd5b505afa158015610689573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506106ad91908101906110af565b6004805460405163191a607f60e31b81529293506001600160a01b03169163c8d303f8916106dd918591016112aa565b60006040518083038186803b1580156106f557600080fd5b505afa158015610709573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526107319190810190610f60565b9695505050505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b60208110156107c8576008810260020a86026001600160f81b03198116156107bf57808484815181106107a157fe5b60200101906001600160f81b031916908160001a9053506001909201915b50600101610772565b506000818511806107d7575084155b156107e35750806107ea565b5060001984015b6060816040519080825280601f01601f191660200182016040528015610817576020820181803883390190505b50905060005b828110156108675784818151811061083157fe5b602001015160f81c60f81b82828151811061084857fe5b60200101906001600160f81b031916908160001a90535060010161081d565b5093505050505b92915050565b6000546001600160a01b0316331490565b60025481565b6005546001600160a01b031681565b6108a2610874565b6108be5760405162461bcd60e51b815260040161035290611456565b6001600160a01b03811660009081526001602052604090205460ff1615610918576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26105b0565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b610977610ac6565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f8916109a6918691016112aa565b60006040518083038186803b1580156109be57600080fd5b505afa1580156109d2573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526109fa9190810190610f60565b90505b919050565b6006546001600160a01b031681565b80516000908290610a265750600090506109fd565b50506020015190565b610a37610874565b610a535760405162461bcd60e51b815260040161035290611456565b6105b081610a6b565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b805161086e8161159d565b600082601f830112610b58578081fd5b81516001600160401b03811115610b6d578182fd5b6020610b7c8182840201611577565b828152925080830184820161022080850287018401881015610b9d57600080fd5b60005b85811015610bc457610bb28984610c53565b84529284019291810191600101610ba0565b50505050505092915050565b8051801515811461086e57600080fd5b80516004811061086e57600080fd5b80516009811061086e57600080fd5b80516003811061086e57600080fd5b60008083601f840112610c1e578182fd5b5081356001600160401b03811115610c34578182fd5b602083019150836020828501011115610c4c57600080fd5b9250929050565b6000610220808385031215610c66578182fd5b610c6f81611577565b91505081518152610c838360208401610d4d565b602082015260408201516040820152606082015160608201526080820151608082015260a082015160a082015260c082015160c082015260e082015160e0820152610100610cd384828501610b3d565b908201526101208281015190820152610140610cf184828501610bef565b90820152610160828101519082015261018080830151908201526101a080830151908201526101c080830151908201526101e0610d3084828501610bd0565b90820152610200610d4384848301610be0565b9082015292915050565b805161086e816115b2565b600060208284031215610d69578081fd5b8135610d748161159d565b9392505050565b600060208284031215610d8c578081fd5b8151610d748161159d565b60008060408385031215610da9578081fd5b50508035926020909101359150565b60008060008060008060608789031215610dd0578182fd5b86356001600160401b0380821115610de6578384fd5b610df28a838b01610c0d565b90985096506020890135915080821115610e0a578384fd5b610e168a838b01610c0d565b90965094506040890135915080821115610e2e578384fd5b50610e3b89828a01610c0d565b979a9699509497509295939492505050565b600080600060408486031215610e61578081fd5b83356001600160401b03811115610e76578182fd5b610e8286828701610c0d565b909790965060209590950135949350505050565b60006020808385031215610ea8578182fd5b82356001600160401b0380821115610ebe578384fd5b81850186601f820112610ecf578485fd5b8035925081831115610edf578485fd5b610ef1601f8401601f19168501611577565b91508282528684848301011115610f06578485fd5b8284820185840137509081019091019190915292915050565b600060208284031215610f30578081fd5b81356001600160401b03811115610f45578182fd5b8083016101c08186031215610f58578283fd5b949350505050565b600060208284031215610f71578081fd5b81516001600160401b0380821115610f87578283fd5b6101c0918401808603831315610f9b578384fd5b610fa483611577565b81518152610fb58760208401610b3d565b6020820152610fc78760408401610b3d565b6040820152610fd98760608401610b3d565b606082015260808201516080820152610ff58760a08401610b3d565b60a082015260c082015160c082015260e082015160e0820152610100935061101f87858401610b3d565b84820152610120935061103487858401610bfe565b93810193909352610140818101519084015261016080820151908401526101809261106187858401610d4d565b848201526101a09350838201518381111561107a578586fd5b61108688828501610b48565b948201949094529695505050505050565b6000602082840312156110a8578081fd5b5035919050565b6000602082840312156110c0578081fd5b5051919050565b600080604083850312156110d9578182fd5b8235915060208301356110eb816115b2565b809150509250929050565b6000806000806080858703121561110b578182fd5b84359350602085013561111d816115b2565b93969395505050506040820135916060013590565b6001600160a01b03169052565b6000815180845260208401935060208301825b828110156112525781518051875260208101516111726020890182611284565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e0880152610100808201516111c2828a0182611132565b50506101208181015190880152610140808201516111e2828a0182611270565b5050610160818101519088015261018080820151908801526101a080820151908801526101c080820151908801526101e080820151611223828a018261125c565b505061020080820151611238828a0182611262565b505050610220959095019460209190910190600101611152565b5093949350505050565b15159052565b6004811061126c57fe5b9052565b6009811061126c57fe5b6003811061126c57fe5b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b918252602082015260400190565b60006020825282602083015282846040840137818301604090810191909152601f909201601f19160101919050565b6000602082528251806020840152815b8181101561131d5760208186018101516040868401015201611300565b8181111561132e5782604083860101525b50601f01601f19169190910160400192915050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b60208082526044908201527f436f756c64206e6f742066696e6420427573696e65737320506172746e65722060408201527f53746f7261676520636f6e7472616374206164647265737320696e20726567696060820152637374727960e01b608082015260a00190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b6000602082528251602083015260208301516114aa6040840182611132565b5060408301516114bd6060840182611132565b5060608301516114d06080840182611132565b50608083015160a083015260a08301516114ed60c0840182611132565b5060c083015160e083015260e083015161010081818501528085015191505061012061151b81850183611132565b840151905061014061152f8482018361127a565b840151610160848101919091528401516101808085019190915284015190506101a061155d81850183611284565b8401516101c0848101529050610f586101e084018261113f565b6040518181016001600160401b038111828210171561159557600080fd5b604052919050565b6001600160a01b03811681146105b057600080fd5b60ff811681146105b057600080fdfea264697066735822122040dcc86a4fabf904e48a6dd019225f1a51eced278166867f8c52472d841d5dc164736f6c63430006010033";
        public PurchasingDeploymentBase() : base(BYTECODE) { }
        public PurchasingDeploymentBase(string byteCode) : base(byteCode) { }
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

    public partial class BpStorageFunction : BpStorageFunctionBase { }

    [Function("bpStorage", "address")]
    public class BpStorageFunctionBase : FunctionMessage
    {

    }

    public partial class Bytes32ToStringFunction : Bytes32ToStringFunctionBase { }

    [Function("bytes32ToString", "string")]
    public class Bytes32ToStringFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "x", 1)]
        public virtual byte[] X { get; set; }
        [Parameter("uint256", "truncateToLength", 2)]
        public virtual BigInteger TruncateToLength { get; set; }
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

    public partial class FundingFunction : FundingFunctionBase { }

    [Function("funding", "address")]
    public class FundingFunctionBase : FunctionMessage
    {

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

    public partial class PoStorageFunction : PoStorageFunctionBase { }

    [Function("poStorage", "address")]
    public class PoStorageFunctionBase : FunctionMessage
    {

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

    public partial class StringToBytes32Function : StringToBytes32FunctionBase { }

    [Function("stringToBytes32", "bytes32")]
    public class StringToBytes32FunctionBase : FunctionMessage
    {
        [Parameter("string", "source", 1)]
        public virtual string Source { get; set; }
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

    public partial class PurchaseItemAcceptedLogEventDTO : PurchaseItemAcceptedLogEventDTOBase { }

    [Event("PurchaseItemAcceptedLog")]
    public class PurchaseItemAcceptedLogEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
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
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
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
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemCreatedLogEventDTO : PurchaseItemCreatedLogEventDTOBase { }

    [Event("PurchaseItemCreatedLog")]
    public class PurchaseItemCreatedLogEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemEscrowFailedLogEventDTO : PurchaseItemEscrowFailedLogEventDTOBase { }

    [Event("PurchaseItemEscrowFailedLog")]
    public class PurchaseItemEscrowFailedLogEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
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
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
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
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
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
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemReceivedLogEventDTO : PurchaseItemReceivedLogEventDTOBase { }

    [Event("PurchaseItemReceivedLog")]
    public class PurchaseItemReceivedLogEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
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
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
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
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
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
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
    }

    public partial class PurchaseOrderNotCreatedLogEventDTO : PurchaseOrderNotCreatedLogEventDTOBase { }

    [Event("PurchaseOrderNotCreatedLog")]
    public class PurchaseOrderNotCreatedLogEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "buyerAddress", 1, true )]
        public virtual byte[] BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
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



    public partial class BpStorageOutputDTO : BpStorageOutputDTOBase { }

    [FunctionOutput]
    public class BpStorageOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class Bytes32ToStringOutputDTO : Bytes32ToStringOutputDTOBase { }

    [FunctionOutput]
    public class Bytes32ToStringOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }







    public partial class FundingOutputDTO : FundingOutputDTOBase { }

    [FunctionOutput]
    public class FundingOutputDTOBase : IFunctionOutputDTO 
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

    public partial class GetPoBySellerAndQuoteOutputDTO : GetPoBySellerAndQuoteOutputDTOBase { }

    [FunctionOutput]
    public class GetPoBySellerAndQuoteOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
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

    public partial class PoStorageOutputDTO : PoStorageOutputDTOBase { }

    [FunctionOutput]
    public class PoStorageOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }













    public partial class StringToBytes32OutputDTO : StringToBytes32OutputDTOBase { }

    [FunctionOutput]
    public class StringToBytes32OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "result", 1)]
        public virtual byte[] Result { get; set; }
    }




}
