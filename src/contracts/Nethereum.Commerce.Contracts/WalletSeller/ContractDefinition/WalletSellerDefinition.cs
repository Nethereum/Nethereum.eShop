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

namespace Nethereum.Commerce.Contracts.WalletSeller.ContractDefinition
{


    public partial class WalletSellerDeployment : WalletSellerDeploymentBase
    {
        public WalletSellerDeployment() : base(BYTECODE) { }
        public WalletSellerDeployment(string byteCode) : base(byteCode) { }
    }

    public class WalletSellerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516117f23803806117f283398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b611718806100da6000396000f3fe608060405234801561001057600080fd5b50600436106101425760003560e01c80638da5cb5b116100b8578063c76ea9781161007c578063c76ea9781461028c578063c8d303f81461029f578063cb4c86b7146102b2578063cfb51928146102ba578063f2fde38b146102cd578063f3ad65f4146102e057610142565b80638da5cb5b1461023f5780638e5fc30b146102545780638f32d59b14610274578063abefab871461027c578063c016d9b61461028457610142565b80634ac5042f1161010a5780634ac5042f146101b35780634f0dfe5b146101c65780636b00e9d8146101d95780636bcc4e3c146101f95780636cc2c1111461020c5780636fee6fec1461021f57610142565b806306ac2d3d14610147578063150e99f91461015c578063261d75551461016f5780633dafca6e1461018d57806346885b5b146101a0575b600080fd5b61015a610155366004610f43565b6102e8565b005b61015a61016a366004610ee3565b6104d9565b6101776105ab565b604051610184919061140a565b60405180910390f35b61015a61019b3660046111fd565b6105b1565b61015a6101ae3660046111fd565b610634565b61015a6101c13660046111fd565b610689565b61015a6101d43660046111fd565b6106de565b6101ec6101e7366004610ee3565b610732565b60405161018491906113ff565b61015a6102073660046111fd565b610747565b61015a61021a36600461122c565b61079c565b61023261022d366004610fd9565b61082d565b604051610184919061157c565b6102476108c4565b60405161018491906113eb565b610267610262366004610f22565b6108d3565b604051610184919061144b565b6101ec6109fd565b610177610a0e565b610247610a14565b61015a61029a366004610ee3565b610a23565b6102326102ad3660046111e5565b610af8565b610247610b8b565b6101776102c8366004611023565b610b9a565b61015a6102db366004610ee3565b610bb8565b610247610be5565b6102f06109fd565b6103155760405162461bcd60e51b815260040161030c90611547565b60405180910390fd5b61035486868080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250610b9a92505050565b60065560035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906103899087908790600401611413565b60206040518083038186803b1580156103a157600080fd5b505afa1580156103b5573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103d99190810190610f06565b600480546001600160a01b0319166001600160a01b039283161790819055166104145760405162461bcd60e51b815260040161030c906114f1565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906104469085908590600401611413565b60206040518083038186803b15801561045e57600080fd5b505afa158015610472573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104969190810190610f06565b600580546001600160a01b0319166001600160a01b039283161790819055166104d15760405162461bcd60e51b815260040161030c9061149e565b505050505050565b6104e16109fd565b6104fd5760405162461bcd60e51b815260040161030c90611547565b6001600160a01b03811660009081526001602052604090205460ff1615610573576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26105a8565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b60065481565b6105b96109fd565b6105d55760405162461bcd60e51b815260040161030c90611547565b60048054604051631ed7e53760e11b81526001600160a01b0390911691633dafca6e91610606918691869101611668565b600060405180830381600087803b15801561062057600080fd5b505af11580156104d1573d6000803e3d6000fd5b61063c6109fd565b6106585760405162461bcd60e51b815260040161030c90611547565b600480546040516346885b5b60e01b81526001600160a01b03909116916346885b5b91610606918691869101611668565b6106916109fd565b6106ad5760405162461bcd60e51b815260040161030c90611547565b60048054604051634ac5042f60e01b81526001600160a01b0390911691634ac5042f91610606918691869101611668565b6106e66109fd565b6107025760405162461bcd60e51b815260040161030c90611547565b600480546040516266c41560e91b81526001600160a01b039091169163cd882a0091610606918691869101611668565b60016020526000908152604090205460ff1681565b61074f6109fd565b61076b5760405162461bcd60e51b815260040161030c90611547565b60048054604051631af3138f60e21b81526001600160a01b0390911691636bcc4e3c91610606918691869101611668565b6107a46109fd565b6107c05760405162461bcd60e51b815260040161030c90611547565b60048054604051636cc2c11160e01b81526001600160a01b0390911691636cc2c111916107f591889188918891889101611679565b600060405180830381600087803b15801561080f57600080fd5b505af1158015610823573d6000803e3d6000fd5b5050505050505050565b610835610c4f565b60048054604051631bfb9bfb60e21b81526001600160a01b0390911691636fee6fec916108689188918891889101611427565b60006040518083038186803b15801561088057600080fd5b505afa158015610894573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526108bc91908101906110ad565b949350505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b6020811015610951576008810260020a86026001600160f81b0319811615610948578084848151811061092a57fe5b60200101906001600160f81b031916908160001a9053506001909201915b506001016108fb565b50600081851180610960575084155b1561096c575080610973565b5060001984015b6060816040519080825280601f01601f1916602001820160405280156109a0576020820181803883390190505b50905060005b828110156109f0578481815181106109ba57fe5b602001015160f81c60f81b8282815181106109d157fe5b60200101906001600160f81b031916908160001a9053506001016109a6565b5093505050505b92915050565b6000546001600160a01b0316331490565b60025481565b6004546001600160a01b031681565b610a2b6109fd565b610a475760405162461bcd60e51b815260040161030c90611547565b6001600160a01b03811660009081526001602052604090205460ff1615610aa1576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26105a8565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b610b00610c4f565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f891610b2f9186910161140a565b60006040518083038186803b158015610b4757600080fd5b505afa158015610b5b573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f19168201604052610b8391908101906110ad565b90505b919050565b6005546001600160a01b031681565b80516000908290610baf575060009050610b86565b50506020015190565b610bc06109fd565b610bdc5760405162461bcd60e51b815260040161030c90611547565b6105a881610bf4565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b80516109f7816116be565b600082601f830112610ce1578081fd5b815167ffffffffffffffff811115610cf7578182fd5b6020610d068182840201611697565b828152925080830184820161022080850287018401881015610d2757600080fd5b60005b85811015610d4e57610d3c8984610dde565b84529284019291810191600101610d2a565b50505050505092915050565b805180151581146109f757600080fd5b8051600481106109f757600080fd5b8051600981106109f757600080fd5b8051600381106109f757600080fd5b60008083601f840112610da8578182fd5b50813567ffffffffffffffff811115610dbf578182fd5b602083019150836020828501011115610dd757600080fd5b9250929050565b6000610220808385031215610df1578182fd5b610dfa81611697565b91505081518152610e0e8360208401610ed8565b602082015260408201516040820152606082015160608201526080820151608082015260a082015160a082015260c082015160c082015260e082015160e0820152610100610e5e84828501610cc6565b908201526101208281015190820152610140610e7c84828501610d79565b90820152610160828101519082015261018080830151908201526101a080830151908201526101c080830151908201526101e0610ebb84828501610d5a565b90820152610200610ece84848301610d6a565b9082015292915050565b80516109f7816116d3565b600060208284031215610ef4578081fd5b8135610eff816116be565b9392505050565b600060208284031215610f17578081fd5b8151610eff816116be565b60008060408385031215610f34578081fd5b50508035926020909101359150565b60008060008060008060608789031215610f5b578182fd5b863567ffffffffffffffff80821115610f72578384fd5b610f7e8a838b01610d97565b90985096506020890135915080821115610f96578384fd5b610fa28a838b01610d97565b90965094506040890135915080821115610fba578384fd5b50610fc789828a01610d97565b979a9699509497509295939492505050565b600080600060408486031215610fed578081fd5b833567ffffffffffffffff811115611003578182fd5b61100f86828701610d97565b909790965060209590950135949350505050565b60006020808385031215611035578182fd5b823567ffffffffffffffff8082111561104c578384fd5b81850186601f82011261105d578485fd5b803592508183111561106d578485fd5b61107f601f8401601f19168501611697565b91508282528684848301011115611094578485fd5b8284820185840137509081019091019190915292915050565b6000602082840312156110be578081fd5b815167ffffffffffffffff808211156110d5578283fd5b6101c09184018086038313156110e9578384fd5b6110f283611697565b815181526111038760208401610cc6565b60208201526111158760408401610cc6565b60408201526111278760608401610cc6565b6060820152608082015160808201526111438760a08401610cc6565b60a082015260c082015160c082015260e082015160e0820152610100935061116d87858401610cc6565b84820152610120935061118287858401610d88565b9381019390935261014081810151908401526101608082015190840152610180926111af87858401610ed8565b848201526101a0935083820151838111156111c8578586fd5b6111d488828501610cd1565b948201949094529695505050505050565b6000602082840312156111f6578081fd5b5035919050565b6000806040838503121561120f578182fd5b823591506020830135611221816116d3565b809150509250929050565b60008060008060808587031215611241578182fd5b843593506020850135611253816116d3565b93969395505050506040820135916060013590565b6001600160a01b03169052565b6000815180845260208401935060208301825b828110156113885781518051875260208101516112a860208901826113e4565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e0880152610100808201516112f8828a0182611268565b5050610120818101519088015261014080820151611318828a01826113a6565b5050610160818101519088015261018080820151908801526101a080820151908801526101c080820151908801526101e080820151611359828a0182611392565b50506102008082015161136e828a0182611398565b505050610220959095019460209190910190600101611288565b5093949350505050565b15159052565b600481106113a257fe5b9052565b600981106113a257fe5b600381106113a257fe5b60008284528282602086013780602084860101526020601f19601f85011685010190509392505050565b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b6000602082526108bc6020830184866113ba565b60006040825261143b6040830185876113ba565b9050826020830152949350505050565b6000602082528251806020840152815b81811015611478576020818601810151604086840101520161145b565b818111156114895782604083860101525b50601f01601f19169190910160400192915050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b60006020825282516020830152602083015161159b6040840182611268565b5060408301516115ae6060840182611268565b5060608301516115c16080840182611268565b50608083015160a083015260a08301516115de60c0840182611268565b5060c083015160e083015260e083015161010081818501528085015191505061012061160c81850183611268565b8401519050610140611620848201836113b0565b840151610160848101919091528401516101808085019190915284015190506101a061164e818501836113e4565b8401516101c08481015290506108bc6101e0840182611275565b91825260ff16602082015260400190565b93845260ff9290921660208401526040830152606082015260800190565b60405181810167ffffffffffffffff811182821017156116b657600080fd5b604052919050565b6001600160a01b03811681146105a857600080fd5b60ff811681146105a857600080fdfea2646970667358221220b1b07c49eb55de178892c51baa0f8fc34a0b9dace3a6fcd6360e19675dad590164736f6c63430006010033";
        public WalletSellerDeploymentBase() : base(BYTECODE) { }
        public WalletSellerDeploymentBase(string byteCode) : base(byteCode) { }
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

    public partial class Bytes32ToStringFunction : Bytes32ToStringFunctionBase { }

    [Function("bytes32ToString", "string")]
    public class Bytes32ToStringFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "x", 1)]
        public virtual byte[] X { get; set; }
        [Parameter("uint256", "truncateToLength", 2)]
        public virtual BigInteger TruncateToLength { get; set; }
    }

    public partial class ConfigureFunction : ConfigureFunctionBase { }

    [Function("configure")]
    public class ConfigureFunctionBase : FunctionMessage
    {
        [Parameter("string", "sellerIdString", 1)]
        public virtual string SellerIdString { get; set; }
        [Parameter("string", "nameOfPurchasing", 2)]
        public virtual string NameOfPurchasing { get; set; }
        [Parameter("string", "nameOfFunding", 3)]
        public virtual string NameOfFunding { get; set; }
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

    public partial class PurchasingFunction : PurchasingFunctionBase { }

    [Function("purchasing", "address")]
    public class PurchasingFunctionBase : FunctionMessage
    {

    }

    public partial class SellerIdFunction : SellerIdFunctionBase { }

    [Function("sellerId", "bytes32")]
    public class SellerIdFunctionBase : FunctionMessage
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

    public partial class SetPoItemGoodsReceivedFunction : SetPoItemGoodsReceivedFunctionBase { }

    [Function("setPoItemGoodsReceived")]
    public class SetPoItemGoodsReceivedFunctionBase : FunctionMessage
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

    public partial class PurchasingOutputDTO : PurchasingOutputDTOBase { }

    [FunctionOutput]
    public class PurchasingOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class SellerIdOutputDTO : SellerIdOutputDTOBase { }

    [FunctionOutput]
    public class SellerIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }













    public partial class StringToBytes32OutputDTO : StringToBytes32OutputDTOBase { }

    [FunctionOutput]
    public class StringToBytes32OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "result", 1)]
        public virtual byte[] Result { get; set; }
    }




}
