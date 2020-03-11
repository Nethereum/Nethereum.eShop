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
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516119b83803806119b883398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b6118de806100da6000396000f3fe608060405234801561001057600080fd5b50600436106101005760003560e01c8063abefab8711610097578063c8d303f811610066578063c8d303f8146101fc578063cb4c86b71461020f578063f2fde38b14610217578063f3ad65f41461022a57610100565b8063abefab87146101b9578063c016d9b6146101ce578063c076cfbf146101d6578063c76ea978146101e957610100565b806377ceb677116100d357806377ceb67714610176578063802706cb146101895780638da5cb5b1461019c5780638f32d59b146101b157610100565b8063150e99f9146101055780634f0dfe5b1461011a5780636b00e9d81461012d5780636fee6fec14610156575b600080fd5b610118610113366004610cc7565b610232565b005b610118610128366004610f9a565b61030d565b61014061013b366004610cc7565b610374565b60405161014d919061130e565b60405180910390f35b610169610164366004610d8b565b610389565b60405161014d91906115ea565b610118610184366004610dd5565b610420565b610118610197366004610d22565b6105f5565b6101a4610799565b60405161014d91906112e1565b6101406107a8565b6101c16107b9565b60405161014d9190611319565b6101a46107bf565b6101186101e4366004610f9a565b6107ce565b6101186101f7366004610cc7565b6107e6565b61016961020a366004610f82565b6108bb565b6101a461094c565b610118610225366004610cc7565b61095b565b6101a4610988565b61023a6107a8565b61025f5760405162461bcd60e51b815260040161025690611430565b60405180910390fd5b6001600160a01b03811660009081526001602052604090205460ff16156102d5576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a261030a565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b60048054604051630d9192ef60e01b81526001600160a01b0390911691630d9192ef9161033e918691869101611718565b600060405180830381600087803b15801561035857600080fd5b505af115801561036c573d6000803e3d6000fd5b505050505050565b60016020526000908152604090205460ff1681565b6103916109f2565b60048054604051631bfb9bfb60e21b81526001600160a01b0390911691636fee6fec916103c49188918891889101611336565b60006040518083038186803b1580156103dc57600080fd5b505afa1580156103f0573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526104189190810190610e0f565b949350505050565b600080826101a081018035601e193684900301811261043e57600080fd5b9091016020810191503567ffffffffffffffff81111561045d57600080fd5b6102208102360382131561047057600080fd5b9150600090505b818110156104f357836101a081018035601e193684900301811261049a57600080fd5b9091016020810191503567ffffffffffffffff8111156104b957600080fd5b610220810236038213156104cc57600080fd5b828181106104d657fe5b905061022002016101200135830192508080600101915050610477565b50600061050660c0850160a08601610cc7565b60055460405163095ea7b360e01b81529192506001600160a01b038084169263095ea7b39261053b92169087906004016112f5565b602060405180830381600087803b15801561055557600080fd5b505af1158015610569573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061058d9190810190610d06565b50600480546040516377ceb67760e01b81526001600160a01b03909116916377ceb677916105bd91889101611465565b600060405180830381600087803b1580156105d757600080fd5b505af11580156105eb573d6000803e3d6000fd5b5050505050505050565b6105fd6107a8565b6106195760405162461bcd60e51b815260040161025690611430565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061064b9087908790600401611322565b60206040518083038186803b15801561066357600080fd5b505afa158015610677573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061069b9190810190610cea565b600480546001600160a01b0319166001600160a01b039283161790819055166106d65760405162461bcd60e51b8152600401610256906113ad565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906107089085908590600401611322565b60206040518083038186803b15801561072057600080fd5b505afa158015610734573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506107589190810190610cea565b600580546001600160a01b0319166001600160a01b039283161790819055166107935760405162461bcd60e51b81526004016102569061135a565b50505050565b6000546001600160a01b031690565b6000546001600160a01b0316331490565b60025481565b6004546001600160a01b031681565b60405162461bcd60e51b815260040161025690611403565b6107ee6107a8565b61080a5760405162461bcd60e51b815260040161025690611430565b6001600160a01b03811660009081526001602052604090205460ff1615610864576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a261030a565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b6108c36109f2565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f8916108f291869101611319565b60006040518083038186803b15801561090a57600080fd5b505afa15801561091e573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526109469190810190610e0f565b92915050565b6005546001600160a01b031681565b6109636107a8565b61097f5760405162461bcd60e51b815260040161025690611430565b61030a81610997565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b6040805161020081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff16815260200160608152602001600060ff168152602001606081525090565b80516109468161184f565b600082601f830112610a95578081fd5b8151610aa8610aa382611750565b611729565b818152915060208083019084810181840286018201871015610ac957600080fd5b60005b84811015610ae857815184529282019290820190600101610acc565b505050505092915050565b600082601f830112610b03578081fd5b8151610b11610aa382611750565b818152915060208083019084810161022080850287018301881015610b3557600080fd5b60005b85811015610c3d5781838a031215610b4f57600080fd5b610b5882611729565b83518152610b688a868601610cbc565b8582015260408401516040820152606084015160608201526080840151608082015260a084015160a082015260c084015160c082015260e084015160e0820152610100610bb78b828701610a7a565b908201526101208481015190820152610140610bd58b828701610c5f565b90820152610160848101519082015261018080850151908201526101a080850151908201526101c080850151908201526101e0610c148b828701610c49565b90820152610200610c278b868301610c54565b9082015285529383019391810191600101610b38565b50505050505092915050565b805161094681611864565b805161094681611872565b80516109468161187f565b80516109468161188c565b60008083601f840112610c86578182fd5b50813567ffffffffffffffff811115610c9d578182fd5b602083019150836020828501011115610cb557600080fd5b9250929050565b805161094681611899565b600060208284031215610cd8578081fd5b8135610ce38161184f565b9392505050565b600060208284031215610cfb578081fd5b8151610ce38161184f565b600060208284031215610d17578081fd5b8151610ce381611864565b60008060008060408587031215610d37578283fd5b843567ffffffffffffffff80821115610d4e578485fd5b610d5a88838901610c75565b90965094506020870135915080821115610d72578384fd5b50610d7f87828801610c75565b95989497509550505050565b600080600060408486031215610d9f578081fd5b833567ffffffffffffffff811115610db5578182fd5b610dc186828701610c75565b909790965060209590950135949350505050565b600060208284031215610de6578081fd5b813567ffffffffffffffff811115610dfc578182fd5b8083016102008186031215610418578283fd5b600060208284031215610e20578081fd5b815167ffffffffffffffff80821115610e37578283fd5b610200918401808603831315610e4b578384fd5b610e5483611729565b81518152610e658760208401610a7a565b6020820152610e778760408401610a7a565b6040820152610e898760608401610a7a565b606082015260808201516080820152610ea58760a08401610a7a565b60a082015260c082015160c082015260e082015160e08201526101009350610ecf87858401610a7a565b848201526101209350610ee487858401610c6a565b938101939093526101408181015190840152610160808201519084015261018092610f1187858401610cbc565b848201526101a093508382015183811115610f2a578586fd5b610f3688828501610af3565b85830152506101c09350610f4c87858401610cbc565b848201526101e093508382015183811115610f65578586fd5b610f7188828501610a85565b948201949094529695505050505050565b600060208284031215610f93578081fd5b5035919050565b60008060408385031215610fac578182fd5b823591506020830135610fbe81611899565b809150509250929050565b6001600160a01b03169052565b81835260006001600160fb1b03831115610fee578081fd5b6020830280836020870137939093016020019283525090919050565b6000815180845260208401935060208301825b8281101561103b57815186526020958601959091019060010161101d565b5093949350505050565b600082845260208401935081815b8481101561103b578135865261106c6020830183611842565b61107960208801826112da565b5060408201356040870152606082013560608701526080820135608087015260a082013560a087015260c082013560c087015260e082013560e08701526101006110c581840184611770565b6110d182890182610fc9565b505061012082810135908701526101406110ed81840184611828565b6110f98289018261129c565b5050610160828101359087015261018080830135908701526101a080830135908701526101c080830135908701526101e06111368184018461180e565b61114282890182611288565b50506102006111538184018461181b565b61115f8289018261128e565b5050610220958601959190910190600101611053565b6000815180845260208401935060208301825b8281101561103b5781518051875260208101516111a860208901826112da565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e0880152610100808201516111f8828a0182610fc9565b5050610120818101519088015261014080820151611218828a018261129c565b5050610160818101519088015261018080820151908801526101a080820151908801526101c080820151908801526101e080820151611259828a0182611288565b50506102008082015161126e828a018261128e565b505050610220959095019460209190910190600101611188565b15159052565b6004811061129857fe5b9052565b6009811061129857fe5b6003811061129857fe5b60008284528282602086013780602084860101526020601f19601f85011685010190509392505050565b60ff169052565b6001600160a01b0391909116815260200190565b6001600160a01b03929092168252602082015260400190565b901515815260200190565b90815260200190565b6000602082526104186020830184866112b0565b60006040825261134a6040830185876112b0565b9050826020830152949350505050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b602080825260139082015272139bdd081a5b5c1b195b595b9d1959081e595d606a1b604082015260600190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b6000602082528235602083015261147f6020840184611770565b61148c6040840182610fc9565b5061149a6040840184611770565b6114a76060840182610fc9565b506114b56060840184611770565b6114c26080840182610fc9565b50608083013560a08301526114da60a0840184611770565b6114e760c0840182610fc9565b5060c083013560e083015261010060e08401358184015261150a81850185611770565b610120915061151b82850182610fc9565b5061152881850185611835565b6101409150611539828501826112a6565b506101608185013581850152610180915080850135828501525061155f81850185611842565b6101a09150611570828501826112da565b5061157d818501856117c5565b61020092506101c0838187015261159961022087018385611045565b6115a582890189611842565b93506101e092506115b8838801856112da565b6115c48389018961177d565b888303601f1901878a0152945091506115de818584610fd6565b98975050505050505050565b6000602082528251602083015260208301516116096040840182610fc9565b50604083015161161c6060840182610fc9565b50606083015161162f6080840182610fc9565b50608083015160a083015260a083015161164c60c0840182610fc9565b5060c083015160e083015260e083015161010081818501528085015191505061012061167a81850183610fc9565b840151905061014061168e848201836112a6565b840151610160848101919091528401516101808085019190915284015190506101a06116bc818501836112da565b808501519150506102006101c081818601526116dc610220860184611175565b9086015192506101e0906116f2868301856112da565b86820151868203601f190184880152935061170d818561100a565b979650505050505050565b91825260ff16602082015260400190565b60405181810167ffffffffffffffff8111828210171561174857600080fd5b604052919050565b600067ffffffffffffffff821115611766578081fd5b5060209081020190565b60008235610ce38161184f565b6000808335601e19843603018112611793578283fd5b830160208101925035905067ffffffffffffffff8111156117b357600080fd5b602081023603831315610cb557600080fd5b6000808335601e198436030181126117db578283fd5b830160208101925035905067ffffffffffffffff8111156117fb57600080fd5b61022081023603831315610cb557600080fd5b60008235610ce381611864565b60008235610ce381611872565b60008235610ce38161187f565b60008235610ce38161188c565b60008235610ce381611899565b6001600160a01b038116811461030a57600080fd5b801515811461030a57600080fd5b6004811061030a57600080fd5b6009811061030a57600080fd5b6003811061030a57600080fd5b60ff8116811461030a57600080fdfea2646970667358221220b0bb580a48db47a6e2d4f833d3d02710169de2c27cf19910a0eafa1b1a16622864736f6c63430006010033";
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
