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
        public static string BYTECODE = "608060405234801561001057600080fd5b5060405161177f38038061177f83398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b6116a5806100da6000396000f3fe608060405234801561001057600080fd5b50600436106101375760003560e01c80638da5cb5b116100b8578063c76ea9781161007c578063c76ea9781461026e578063c8d303f814610281578063cb4c86b714610294578063cfb519281461029c578063f2fde38b146102af578063f3ad65f4146102c257610137565b80638da5cb5b146102215780638e5fc30b146102365780638f32d59b14610256578063abefab871461025e578063c016d9b61461026657610137565b80634ac5042f116100ff5780634ac5042f146101a85780634f0dfe5b146101bb5780636b00e9d8146101ce5780636cc2c111146101ee5780636fee6fec1461020157610137565b806306ac2d3d1461013c578063150e99f914610151578063261d7555146101645780633dafca6e1461018257806346885b5b14610195575b600080fd5b61014f61014a366004610ed0565b6102ca565b005b61014f61015f366004610e70565b6104bb565b61016c61058d565b6040516101799190611397565b60405180910390f35b61014f61019036600461118a565b610593565b61014f6101a336600461118a565b610616565b61014f6101b636600461118a565b61066b565b61014f6101c936600461118a565b6106c0565b6101e16101dc366004610e70565b610714565b604051610179919061138c565b61014f6101fc3660046111b9565b610729565b61021461020f366004610f66565b6107ba565b6040516101799190611509565b610229610851565b6040516101799190611378565b610249610244366004610eaf565b610860565b60405161017991906113d8565b6101e161098a565b61016c61099b565b6102296109a1565b61014f61027c366004610e70565b6109b0565b61021461028f366004611172565b610a85565b610229610b18565b61016c6102aa366004610fb0565b610b27565b61014f6102bd366004610e70565b610b45565b610229610b72565b6102d261098a565b6102f75760405162461bcd60e51b81526004016102ee906114d4565b60405180910390fd5b61033686868080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250610b2792505050565b60065560035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061036b90879087906004016113a0565b60206040518083038186803b15801561038357600080fd5b505afa158015610397573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103bb9190810190610e93565b600480546001600160a01b0319166001600160a01b039283161790819055166103f65760405162461bcd60e51b81526004016102ee9061147e565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061042890859085906004016113a0565b60206040518083038186803b15801561044057600080fd5b505afa158015610454573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104789190810190610e93565b600580546001600160a01b0319166001600160a01b039283161790819055166104b35760405162461bcd60e51b81526004016102ee9061142b565b505050505050565b6104c361098a565b6104df5760405162461bcd60e51b81526004016102ee906114d4565b6001600160a01b03811660009081526001602052604090205460ff1615610555576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a261058a565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b60065481565b61059b61098a565b6105b75760405162461bcd60e51b81526004016102ee906114d4565b60048054604051631ed7e53760e11b81526001600160a01b0390911691633dafca6e916105e89186918691016115f5565b600060405180830381600087803b15801561060257600080fd5b505af11580156104b3573d6000803e3d6000fd5b61061e61098a565b61063a5760405162461bcd60e51b81526004016102ee906114d4565b600480546040516346885b5b60e01b81526001600160a01b03909116916346885b5b916105e89186918691016115f5565b61067361098a565b61068f5760405162461bcd60e51b81526004016102ee906114d4565b60048054604051634ac5042f60e01b81526001600160a01b0390911691634ac5042f916105e89186918691016115f5565b6106c861098a565b6106e45760405162461bcd60e51b81526004016102ee906114d4565b600480546040516266c41560e91b81526001600160a01b039091169163cd882a00916105e89186918691016115f5565b60016020526000908152604090205460ff1681565b61073161098a565b61074d5760405162461bcd60e51b81526004016102ee906114d4565b60048054604051636cc2c11160e01b81526001600160a01b0390911691636cc2c1119161078291889188918891889101611606565b600060405180830381600087803b15801561079c57600080fd5b505af11580156107b0573d6000803e3d6000fd5b5050505050505050565b6107c2610bdc565b60048054604051631bfb9bfb60e21b81526001600160a01b0390911691636fee6fec916107f591889188918891016113b4565b60006040518083038186803b15801561080d57600080fd5b505afa158015610821573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f19168201604052610849919081019061103a565b949350505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b60208110156108de576008810260020a86026001600160f81b03198116156108d557808484815181106108b757fe5b60200101906001600160f81b031916908160001a9053506001909201915b50600101610888565b506000818511806108ed575084155b156108f9575080610900565b5060001984015b6060816040519080825280601f01601f19166020018201604052801561092d576020820181803883390190505b50905060005b8281101561097d5784818151811061094757fe5b602001015160f81c60f81b82828151811061095e57fe5b60200101906001600160f81b031916908160001a905350600101610933565b5093505050505b92915050565b6000546001600160a01b0316331490565b60025481565b6004546001600160a01b031681565b6109b861098a565b6109d45760405162461bcd60e51b81526004016102ee906114d4565b6001600160a01b03811660009081526001602052604090205460ff1615610a2e576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a261058a565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b610a8d610bdc565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f891610abc91869101611397565b60006040518083038186803b158015610ad457600080fd5b505afa158015610ae8573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f19168201604052610b10919081019061103a565b90505b919050565b6005546001600160a01b031681565b80516000908290610b3c575060009050610b13565b50506020015190565b610b4d61098a565b610b695760405162461bcd60e51b81526004016102ee906114d4565b61058a81610b81565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b80516109848161164b565b600082601f830112610c6e578081fd5b815167ffffffffffffffff811115610c84578182fd5b6020610c938182840201611624565b828152925080830184820161022080850287018401881015610cb457600080fd5b60005b85811015610cdb57610cc98984610d6b565b84529284019291810191600101610cb7565b50505050505092915050565b8051801515811461098457600080fd5b80516004811061098457600080fd5b80516009811061098457600080fd5b80516003811061098457600080fd5b60008083601f840112610d35578182fd5b50813567ffffffffffffffff811115610d4c578182fd5b602083019150836020828501011115610d6457600080fd5b9250929050565b6000610220808385031215610d7e578182fd5b610d8781611624565b91505081518152610d9b8360208401610e65565b602082015260408201516040820152606082015160608201526080820151608082015260a082015160a082015260c082015160c082015260e082015160e0820152610100610deb84828501610c53565b908201526101208281015190820152610140610e0984828501610d06565b90820152610160828101519082015261018080830151908201526101a080830151908201526101c080830151908201526101e0610e4884828501610ce7565b90820152610200610e5b84848301610cf7565b9082015292915050565b805161098481611660565b600060208284031215610e81578081fd5b8135610e8c8161164b565b9392505050565b600060208284031215610ea4578081fd5b8151610e8c8161164b565b60008060408385031215610ec1578081fd5b50508035926020909101359150565b60008060008060008060608789031215610ee8578182fd5b863567ffffffffffffffff80821115610eff578384fd5b610f0b8a838b01610d24565b90985096506020890135915080821115610f23578384fd5b610f2f8a838b01610d24565b90965094506040890135915080821115610f47578384fd5b50610f5489828a01610d24565b979a9699509497509295939492505050565b600080600060408486031215610f7a578081fd5b833567ffffffffffffffff811115610f90578182fd5b610f9c86828701610d24565b909790965060209590950135949350505050565b60006020808385031215610fc2578182fd5b823567ffffffffffffffff80821115610fd9578384fd5b81850186601f820112610fea578485fd5b8035925081831115610ffa578485fd5b61100c601f8401601f19168501611624565b91508282528684848301011115611021578485fd5b8284820185840137509081019091019190915292915050565b60006020828403121561104b578081fd5b815167ffffffffffffffff80821115611062578283fd5b6101c0918401808603831315611076578384fd5b61107f83611624565b815181526110908760208401610c53565b60208201526110a28760408401610c53565b60408201526110b48760608401610c53565b6060820152608082015160808201526110d08760a08401610c53565b60a082015260c082015160c082015260e082015160e082015261010093506110fa87858401610c53565b84820152610120935061110f87858401610d15565b93810193909352610140818101519084015261016080820151908401526101809261113c87858401610e65565b848201526101a093508382015183811115611155578586fd5b61116188828501610c5e565b948201949094529695505050505050565b600060208284031215611183578081fd5b5035919050565b6000806040838503121561119c578182fd5b8235915060208301356111ae81611660565b809150509250929050565b600080600080608085870312156111ce578182fd5b8435935060208501356111e081611660565b93969395505050506040820135916060013590565b6001600160a01b03169052565b6000815180845260208401935060208301825b828110156113155781518051875260208101516112356020890182611371565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e088015261010080820151611285828a01826111f5565b50506101208181015190880152610140808201516112a5828a0182611333565b5050610160818101519088015261018080820151908801526101a080820151908801526101c080820151908801526101e0808201516112e6828a018261131f565b5050610200808201516112fb828a0182611325565b505050610220959095019460209190910190600101611215565b5093949350505050565b15159052565b6004811061132f57fe5b9052565b6009811061132f57fe5b6003811061132f57fe5b60008284528282602086013780602084860101526020601f19601f85011685010190509392505050565b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b600060208252610849602083018486611347565b6000604082526113c8604083018587611347565b9050826020830152949350505050565b6000602082528251806020840152815b8181101561140557602081860181015160408684010152016113e8565b818111156114165782604083860101525b50601f01601f19169190910160400192915050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b60006020825282516020830152602083015161152860408401826111f5565b50604083015161153b60608401826111f5565b50606083015161154e60808401826111f5565b50608083015160a083015260a083015161156b60c08401826111f5565b5060c083015160e083015260e0830151610100818185015280850151915050610120611599818501836111f5565b84015190506101406115ad8482018361133d565b840151610160848101919091528401516101808085019190915284015190506101a06115db81850183611371565b8401516101c08481015290506108496101e0840182611202565b91825260ff16602082015260400190565b93845260ff9290921660208401526040830152606082015260800190565b60405181810167ffffffffffffffff8111828210171561164357600080fd5b604052919050565b6001600160a01b038116811461058a57600080fd5b60ff8116811461058a57600080fdfea26469706673582212206126a9ad76d1248df78df55f348d1eacec04b8e9b210f743a33aa4bca9212aed64736f6c63430006010033";
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
