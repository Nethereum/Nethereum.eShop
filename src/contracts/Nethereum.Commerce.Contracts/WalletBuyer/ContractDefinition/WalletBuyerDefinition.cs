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
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516115d83803806115d883398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b6114fe806100da6000396000f3fe608060405234801561001057600080fd5b50600436106100d05760003560e01c8063150e99f9146100d55780634f0dfe5b146100ea5780636b00e9d8146100fd5780636fee6fec14610126578063802706cb146101465780638da5cb5b146101595780638f32d59b1461016e57806391aa0f3014610176578063abefab8714610189578063c016d9b61461019e578063c076cfbf146100ea578063c76ea978146101a6578063c8d303f8146101b9578063cb4c86b7146101cc578063f2fde38b146101d4578063f3ad65f4146101e7575b600080fd5b6100e86100e3366004610af4565b6101ef565b005b6100e86100f8366004610d6c565b6102ca565b61011061010b366004610af4565b6102ce565b60405161011d9190611062565b60405180910390f35b610139610134366004610b9b565b6102e3565b60405161011d91906112c7565b6100e8610154366004610b33565b61037a565b6101616104fa565b60405161011d919061104e565b610110610509565b6100e8610184366004610be4565b61051a565b610191610652565b60405161011d919061106d565b610161610658565b6100e86101b4366004610af4565b610667565b6101396101c7366004610d54565b61073c565b6101616107cd565b6100e86101e2366004610af4565b6107dc565b610161610809565b6101f7610509565b61021c5760405162461bcd60e51b815260040161021390611157565b60405180910390fd5b6001600160a01b03811660009081526001602052604090205460ff1615610292576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26102c7565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b5050565b60016020526000908152604090205460ff1681565b6102eb610873565b60048054604051631bfb9bfb60e21b81526001600160a01b0390911691636fee6fec9161031e918891889188910161108a565b60006040518083038186803b15801561033657600080fd5b505afa15801561034a573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526103729190810190610c1d565b949350505050565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906103ac9087908790600401611076565b60206040518083038186803b1580156103c457600080fd5b505afa1580156103d8573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103fc9190810190610b17565b600480546001600160a01b0319166001600160a01b039283161790819055166104375760405162461bcd60e51b815260040161021390611101565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906104699085908590600401611076565b60206040518083038186803b15801561048157600080fd5b505afa158015610495573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104b99190810190610b17565b600580546001600160a01b0319166001600160a01b039283161790819055166104f45760405162461bcd60e51b8152600401610213906110ae565b50505050565b6000546001600160a01b031690565b6000546001600160a01b0316331490565b600080826101a081018035601e193684900301811261053857600080fd5b909101602081019150356001600160401b0381111561055657600080fd5b6102208102360382131561056957600080fd5b9150600090505b818110156105eb57836101a081018035601e193684900301811261059357600080fd5b909101602081019150356001600160401b038111156105b157600080fd5b610220810236038213156105c457600080fd5b828181106105ce57fe5b905061022002016101200135830192508080600101915050610570565b506004805460405163091aa0f360e41b81526001600160a01b03909116916391aa0f309161061b9187910161118c565b600060405180830381600087803b15801561063557600080fd5b505af1158015610649573d6000803e3d6000fd5b50505050505050565b60025481565b6004546001600160a01b031681565b61066f610509565b61068b5760405162461bcd60e51b815260040161021390611157565b6001600160a01b03811660009081526001602052604090205460ff16156106e5576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26102c7565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b610744610873565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f8916107739186910161106d565b60006040518083038186803b15801561078b57600080fd5b505afa15801561079f573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526107c79190810190610c1d565b92915050565b6005546001600160a01b031681565b6107e4610509565b6108005760405162461bcd60e51b815260040161021390611157565b6102c781610818565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b80516107c78161146f565b600082601f830112610905578081fd5b81516001600160401b0381111561091a578182fd5b602061092981828402016113b3565b82815292508083018482016102208085028701840188101561094a57600080fd5b60005b858110156109715761095f89846109ef565b8452928401929181019160010161094d565b50505050505092915050565b80516107c781611484565b80516107c781611492565b80516107c78161149f565b80516107c7816114ac565b60008083601f8401126109ba578182fd5b5081356001600160401b038111156109d0578182fd5b6020830191508360208285010111156109e857600080fd5b9250929050565b6000610220808385031215610a02578182fd5b610a0b816113b3565b91505081518152610a1f8360208401610ae9565b602082015260408201516040820152606082015160608201526080820151608082015260a082015160a082015260c082015160c082015260e082015160e0820152610100610a6f848285016108ea565b908201526101208281015190820152610140610a8d84828501610993565b90820152610160828101519082015261018080830151908201526101a080830151908201526101c080830151908201526101e0610acc8482850161097d565b90820152610200610adf84848301610988565b9082015292915050565b80516107c7816114b9565b600060208284031215610b05578081fd5b8135610b108161146f565b9392505050565b600060208284031215610b28578081fd5b8151610b108161146f565b60008060008060408587031215610b48578283fd5b84356001600160401b0380821115610b5e578485fd5b610b6a888389016109a9565b90965094506020870135915080821115610b82578384fd5b50610b8f878288016109a9565b95989497509550505050565b600080600060408486031215610baf578081fd5b83356001600160401b03811115610bc4578182fd5b610bd0868287016109a9565b909790965060209590950135949350505050565b600060208284031215610bf5578081fd5b81356001600160401b03811115610c0a578182fd5b8083016101c08186031215610372578283fd5b600060208284031215610c2e578081fd5b81516001600160401b0380821115610c44578283fd5b6101c0918401808603831315610c58578384fd5b610c61836113b3565b81518152610c7287602084016108ea565b6020820152610c8487604084016108ea565b6040820152610c9687606084016108ea565b606082015260808201516080820152610cb28760a084016108ea565b60a082015260c082015160c082015260e082015160e08201526101009350610cdc878584016108ea565b848201526101209350610cf18785840161099e565b938101939093526101408181015190840152610160808201519084015261018092610d1e87858401610ae9565b848201526101a093508382015183811115610d37578586fd5b610d43888285016108f5565b948201949094529695505050505050565b600060208284031215610d65578081fd5b5035919050565b60008060408385031215610d7e578182fd5b823591506020830135610d90816114b9565b809150509250929050565b6001600160a01b03169052565b600082845260208401935081815b84811015610ed85781358652610dcf6020830183611462565b610ddc6020880182611047565b5060408201356040870152606082013560608701526080820135608087015260a082013560a087015260c082013560c087015260e082013560e0870152610100610e28818401846113d9565b610e3482890182610d9b565b50506101208281013590870152610140610e5081840184611448565b610e5c82890182611009565b5050610160828101359087015261018080830135908701526101a080830135908701526101c080830135908701526101e0610e998184018461142e565b610ea582890182610ff5565b5050610200610eb68184018461143b565b610ec282890182610ffb565b5050610220958601959190910190600101610db6565b5093949350505050565b6000815180845260208401935060208301825b82811015610ed8578151805187526020810151610f156020890182611047565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e088015261010080820151610f65828a0182610d9b565b5050610120818101519088015261014080820151610f85828a0182611009565b5050610160818101519088015261018080820151908801526101a080820151908801526101c080820151908801526101e080820151610fc6828a0182610ff5565b505061020080820151610fdb828a0182610ffb565b505050610220959095019460209190910190600101610ef5565b15159052565b6004811061100557fe5b9052565b6009811061100557fe5b6003811061100557fe5b60008284528282602086013780602084860101526020601f19601f85011685010190509392505050565b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b60006020825261037260208301848661101d565b60006040825261109e60408301858761101d565b9050826020830152949350505050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b600060208252823560208301526111a660208401846113d9565b6111b36040840182610d9b565b506111c160408401846113d9565b6111ce6060840182610d9b565b506111dc60608401846113d9565b6111e96080840182610d9b565b50608083013560a083015261120160a08401846113d9565b61120e60c0840182610d9b565b5060c083013560e083015261010060e084013581840152611231818501856113d9565b610120915061124282850182610d9b565b5061124f81850185611455565b610140915061126082850182611013565b506101608185013581850152610180915080850135828501525061128681850185611462565b6101a0915061129782850182611047565b506112a4818501856113e6565b6101c0925082838601526112bd6101e086018284610da8565b9695505050505050565b6000602082528251602083015260208301516112e66040840182610d9b565b5060408301516112f96060840182610d9b565b50606083015161130c6080840182610d9b565b50608083015160a083015260a083015161132960c0840182610d9b565b5060c083015160e083015260e083015161010081818501528085015191505061012061135781850183610d9b565b840151905061014061136b84820183611013565b840151610160848101919091528401516101808085019190915284015190506101a061139981850183611047565b8401516101c08481015290506103726101e0840182610ee2565b6040518181016001600160401b03811182821017156113d157600080fd5b604052919050565b60008235610b108161146f565b6000808335601e198436030181126113fc578283fd5b83016020810192503590506001600160401b0381111561141b57600080fd5b610220810236038313156109e857600080fd5b60008235610b1081611484565b60008235610b1081611492565b60008235610b108161149f565b60008235610b10816114ac565b60008235610b10816114b9565b6001600160a01b03811681146102c757600080fd5b80151581146102c757600080fd5b600481106102c757600080fd5b600981106102c757600080fd5b600381106102c757600080fd5b60ff811681146102c757600080fdfea2646970667358221220980d0d8e168808dc9ad79456abf1cb193a17fd963bae4de6b3bd1a879ecde93964736f6c63430006010033";
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






}
