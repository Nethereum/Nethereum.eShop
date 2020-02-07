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
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516114b43803806114b483398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b6113da806100da6000396000f3fe608060405234801561001057600080fd5b50600436106101075760003560e01c806306ac2d3d1461010c578063150e99f914610121578063261d7555146101345780633dafca6e1461015257806346885b5b146101525780634ac5042f146101525780634f0dfe5b146101525780636b00e9d8146101655780636cc2c111146101855780636fee6fec146101985780638da5cb5b146101b85780638e5fc30b146101cd5780638f32d59b146101ed578063abefab87146101f5578063c016d9b6146101fd578063c76ea97814610205578063c8d303f814610218578063cb4c86b71461022b578063cfb5192814610233578063f2fde38b14610246578063f3ad65f414610259575b600080fd5b61011f61011a366004610c39565b610261565b005b61011f61012f366004610bd9565b61042e565b61013c610500565b60405161014991906110fc565b60405180910390f35b61011f610160366004610eef565b610506565b610178610173366004610bd9565b61050a565b60405161014991906110f1565b61011f610193366004610f1e565b61051f565b6101ab6101a6366004610cce565b610525565b604051610149919061126e565b6101c06105bc565b60405161014991906110dd565b6101e06101db366004610c18565b6105cb565b604051610149919061113d565b6101786106f5565b61013c610706565b6101c061070c565b61011f610213366004610bd9565b61071b565b6101ab610226366004610ed7565b6107f0565b6101c0610883565b61013c610241366004610d17565b610892565b61011f610254366004610bd9565b6108b0565b6101c06108dd565b6102a086868080601f01602080910402602001604051908101604052809392919081815260200183838082843760009201919091525061089292505050565b60065560035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906102d59087908790600401611105565b60206040518083038186803b1580156102ed57600080fd5b505afa158015610301573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103259190810190610bfc565b600480546001600160a01b0319166001600160a01b039283161790819055166103695760405162461bcd60e51b8152600401610360906111e3565b60405180910390fd5b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061039b9085908590600401611105565b60206040518083038186803b1580156103b357600080fd5b505afa1580156103c7573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103eb9190810190610bfc565b600580546001600160a01b0319166001600160a01b039283161790819055166104265760405162461bcd60e51b815260040161036090611190565b505050505050565b6104366106f5565b6104525760405162461bcd60e51b815260040161036090611239565b6001600160a01b03811660009081526001602052604090205460ff16156104c8576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26104fd565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b60065481565b5050565b60016020526000908152604090205460ff1681565b50505050565b61052d610947565b60048054604051631bfb9bfb60e21b81526001600160a01b0390911691636fee6fec916105609188918891889101611119565b60006040518083038186803b15801561057857600080fd5b505afa15801561058c573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526105b49190810190610da0565b949350505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b6020811015610649576008810260020a86026001600160f81b0319811615610640578084848151811061062257fe5b60200101906001600160f81b031916908160001a9053506001909201915b506001016105f3565b50600081851180610658575084155b1561066457508061066b565b5060001984015b6060816040519080825280601f01601f191660200182016040528015610698576020820181803883390190505b50905060005b828110156106e8578481815181106106b257fe5b602001015160f81c60f81b8282815181106106c957fe5b60200101906001600160f81b031916908160001a90535060010161069e565b5093505050505b92915050565b6000546001600160a01b0316331490565b60025481565b6004546001600160a01b031681565b6107236106f5565b61073f5760405162461bcd60e51b815260040161036090611239565b6001600160a01b03811660009081526001602052604090205460ff1615610799576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26104fd565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b6107f8610947565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f891610827918691016110fc565b60006040518083038186803b15801561083f57600080fd5b505afa158015610853573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f1916820160405261087b9190810190610da0565b90505b919050565b6005546001600160a01b031681565b805160009082906108a757506000905061087e565b50506020015190565b6108b86106f5565b6108d45760405162461bcd60e51b815260040161036090611239565b6104fd816108ec565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b80516106ef81611380565b600082601f8301126109d9578081fd5b81516001600160401b038111156109ee578182fd5b60206109fd818284020161135a565b828152925080830184820161022080850287018401881015610a1e57600080fd5b60005b85811015610a4557610a338984610ad4565b84529284019291810191600101610a21565b50505050505092915050565b805180151581146106ef57600080fd5b8051600481106106ef57600080fd5b8051600981106106ef57600080fd5b8051600381106106ef57600080fd5b60008083601f840112610a9f578182fd5b5081356001600160401b03811115610ab5578182fd5b602083019150836020828501011115610acd57600080fd5b9250929050565b6000610220808385031215610ae7578182fd5b610af08161135a565b91505081518152610b048360208401610bce565b602082015260408201516040820152606082015160608201526080820151608082015260a082015160a082015260c082015160c082015260e082015160e0820152610100610b54848285016109be565b908201526101208281015190820152610140610b7284828501610a70565b90820152610160828101519082015261018080830151908201526101a080830151908201526101c080830151908201526101e0610bb184828501610a51565b90820152610200610bc484848301610a61565b9082015292915050565b80516106ef81611395565b600060208284031215610bea578081fd5b8135610bf581611380565b9392505050565b600060208284031215610c0d578081fd5b8151610bf581611380565b60008060408385031215610c2a578081fd5b50508035926020909101359150565b60008060008060008060608789031215610c51578182fd5b86356001600160401b0380821115610c67578384fd5b610c738a838b01610a8e565b90985096506020890135915080821115610c8b578384fd5b610c978a838b01610a8e565b90965094506040890135915080821115610caf578384fd5b50610cbc89828a01610a8e565b979a9699509497509295939492505050565b600080600060408486031215610ce2578081fd5b83356001600160401b03811115610cf7578182fd5b610d0386828701610a8e565b909790965060209590950135949350505050565b60006020808385031215610d29578182fd5b82356001600160401b0380821115610d3f578384fd5b81850186601f820112610d50578485fd5b8035925081831115610d60578485fd5b610d72601f8401601f1916850161135a565b91508282528684848301011115610d87578485fd5b8284820185840137509081019091019190915292915050565b600060208284031215610db1578081fd5b81516001600160401b0380821115610dc7578283fd5b6101c0918401808603831315610ddb578384fd5b610de48361135a565b81518152610df587602084016109be565b6020820152610e0787604084016109be565b6040820152610e1987606084016109be565b606082015260808201516080820152610e358760a084016109be565b60a082015260c082015160c082015260e082015160e08201526101009350610e5f878584016109be565b848201526101209350610e7487858401610a7f565b938101939093526101408181015190840152610160808201519084015261018092610ea187858401610bce565b848201526101a093508382015183811115610eba578586fd5b610ec6888285016109c9565b948201949094529695505050505050565b600060208284031215610ee8578081fd5b5035919050565b60008060408385031215610f01578182fd5b823591506020830135610f1381611395565b809150509250929050565b60008060008060808587031215610f33578182fd5b843593506020850135610f4581611395565b93969395505050506040820135916060013590565b6001600160a01b03169052565b6000815180845260208401935060208301825b8281101561107a578151805187526020810151610f9a60208901826110d6565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e088015261010080820151610fea828a0182610f5a565b505061012081810151908801526101408082015161100a828a0182611098565b5050610160818101519088015261018080820151908801526101a080820151908801526101c080820151908801526101e08082015161104b828a0182611084565b505061020080820151611060828a018261108a565b505050610220959095019460209190910190600101610f7a565b5093949350505050565b15159052565b6004811061109457fe5b9052565b6009811061109457fe5b6003811061109457fe5b60008284528282602086013780602084860101526020601f19601f85011685010190509392505050565b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b6000602082526105b46020830184866110ac565b60006040825261112d6040830185876110ac565b9050826020830152949350505050565b6000602082528251806020840152815b8181101561116a576020818601810151604086840101520161114d565b8181111561117b5782604083860101525b50601f01601f19169190910160400192915050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b60006020825282516020830152602083015161128d6040840182610f5a565b5060408301516112a06060840182610f5a565b5060608301516112b36080840182610f5a565b50608083015160a083015260a08301516112d060c0840182610f5a565b5060c083015160e083015260e08301516101008181850152808501519150506101206112fe81850183610f5a565b8401519050610140611312848201836110a2565b840151610160848101919091528401516101808085019190915284015190506101a0611340818501836110d6565b8401516101c08481015290506105b46101e0840182610f67565b6040518181016001600160401b038111828210171561137857600080fd5b604052919050565b6001600160a01b03811681146104fd57600080fd5b60ff811681146104fd57600080fdfea2646970667358221220bf43b35b813b890df2d5093be3e740a768f7d0d4cdc631c1e23ad15f5248452364736f6c63430006010033";
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
