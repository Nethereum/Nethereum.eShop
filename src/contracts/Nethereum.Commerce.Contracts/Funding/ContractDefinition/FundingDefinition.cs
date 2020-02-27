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

namespace Nethereum.Commerce.Contracts.Funding.ContractDefinition
{


    public partial class FundingDeployment : FundingDeploymentBase
    {
        public FundingDeployment() : base(BYTECODE) { }
        public FundingDeployment(string byteCode) : base(byteCode) { }
    }

    public class FundingDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516111b93803806111b983398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b6110df806100da6000396000f3fe608060405234801561001057600080fd5b50600436106100f55760003560e01c80638e5fc30b11610097578063c76ea97811610066578063c76ea978146101c8578063cfb51928146101db578063f2fde38b146101ee578063f3ad65f414610201576100f5565b80638e5fc30b146101835780638f32d59b146101a3578063abefab87146101ab578063c016d9b6146101c0576100f5565b80636b6a291a116100d35780636b6a291a1461014d578063802706cb1461015557806387a211b5146101685780638da5cb5b1461017b576100f5565b8063150e99f9146100fa5780634360beb51461010f5780636b00e9d81461012d575b600080fd5b61010d610108366004610bb8565b610209565b005b6101176102e4565b6040516101249190610e7b565b60405180910390f35b61014061013b366004610bb8565b6102f3565b6040516101249190610eb3565b610117610308565b61010d610163366004610c38565b610317565b61010d610176366004610e63565b6104a7565b610117610631565b610196610191366004610c17565b610640565b6040516101249190610ef6565b61014061076a565b6101b361077b565b6040516101249190610ebe565b610117610781565b61010d6101d6366004610bb8565b610790565b6101b36101e9366004610ca1565b610865565b61010d6101fc366004610bb8565b610887565b6101176108b4565b61021161076a565b6102365760405162461bcd60e51b815260040161022d90610feb565b60405180910390fd5b6001600160a01b03811660009081526001602052604090205460ff16156102ac576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26102e1565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b6004546001600160a01b031681565b60016020526000908152604090205460ff1681565b6006546001600160a01b031681565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906103499085908590600401610ec7565b60206040518083038186803b15801561036157600080fd5b505afa158015610375573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103999190810190610bdb565b600480546001600160a01b0319166001600160a01b039283161790819055166103d45760405162461bcd60e51b815260040161022d90610f8e565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906104069087908790600401610ec7565b60206040518083038186803b15801561041e57600080fd5b505afa158015610432573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104569190810190610bdb565b600680546001600160a01b03199081166001600160a01b0393841617918290556005805492909316911681179091556104a15760405162461bcd60e51b815260040161022d90611020565b50505050565b6104af61091e565b60055460405163191a607f60e31b81526001600160a01b039091169063c8d303f8906104df908590600401610ebe565b60006040518083038186803b1580156104f757600080fd5b505afa15801561050b573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526105339190810190610d2b565b90506000805b82610180015160ff1681101561057857826101a00151818151811061055a57fe5b60200260200101516101200151820191508080600101915050610539565b5060a082015160608301516040516323b872dd60e01b81526000916001600160a01b038416916323b872dd916105b49130908890600401610e8f565b602060405180830381600087803b1580156105ce57600080fd5b505af11580156105e2573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506106069190810190610bf7565b905060018115151461062a5760405162461bcd60e51b815260040161022d90610f49565b5050505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b60208110156106be576008810260020a86026001600160f81b03198116156106b5578084848151811061069757fe5b60200101906001600160f81b031916908160001a9053506001909201915b50600101610668565b506000818511806106cd575084155b156106d95750806106e0565b5060001984015b6060816040519080825280601f01601f19166020018201604052801561070d576020820181803883390190505b50905060005b8281101561075d5784818151811061072757fe5b602001015160f81c60f81b82828151811061073e57fe5b60200101906001600160f81b031916908160001a905350600101610713565b5093505050505b92915050565b6000546001600160a01b0316331490565b60025481565b6005546001600160a01b031681565b61079861076a565b6107b45760405162461bcd60e51b815260040161022d90610feb565b6001600160a01b03811660009081526001602052604090205460ff161561080e576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26102e1565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b8051600090829061087a575060009050610882565b505060208101515b919050565b61088f61076a565b6108ab5760405162461bcd60e51b815260040161022d90610feb565b6102e1816108c3565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b805161076481611094565b600082601f8301126109b0578081fd5b815167ffffffffffffffff8111156109c6578182fd5b60206109d5818284020161106d565b8281529250808301848201610220808502870184018810156109f657600080fd5b60005b85811015610a1d57610a0b8984610aad565b845292840192918101916001016109f9565b50505050505092915050565b8051801515811461076457600080fd5b80516004811061076457600080fd5b80516009811061076457600080fd5b80516003811061076457600080fd5b60008083601f840112610a77578182fd5b50813567ffffffffffffffff811115610a8e578182fd5b602083019150836020828501011115610aa657600080fd5b9250929050565b6000610220808385031215610ac0578182fd5b610ac98161106d565b91505081518152610add8360208401610ba7565b602082015260408201516040820152606082015160608201526080820151608082015260a082015160a082015260c082015160c082015260e082015160e0820152610100610b2d84828501610995565b908201526101208281015190820152610140610b4b84828501610a48565b90820152610160828101519082015261018080830151908201526101a080830151908201526101c080830151908201526101e0610b8a84828501610a29565b90820152610200610b9d84848301610a39565b9082015292915050565b805160ff8116811461076457600080fd5b600060208284031215610bc9578081fd5b8135610bd481611094565b9392505050565b600060208284031215610bec578081fd5b8151610bd481611094565b600060208284031215610c08578081fd5b81518015158114610bd4578182fd5b60008060408385031215610c29578081fd5b50508035926020909101359150565b60008060008060408587031215610c4d578182fd5b843567ffffffffffffffff80821115610c64578384fd5b610c7088838901610a66565b90965094506020870135915080821115610c88578384fd5b50610c9587828801610a66565b95989497509550505050565b60006020808385031215610cb3578182fd5b823567ffffffffffffffff80821115610cca578384fd5b81850186601f820112610cdb578485fd5b8035925081831115610ceb578485fd5b610cfd601f8401601f1916850161106d565b91508282528684848301011115610d12578485fd5b8284820185840137509081019091019190915292915050565b600060208284031215610d3c578081fd5b815167ffffffffffffffff80821115610d53578283fd5b6101c0918401808603831315610d67578384fd5b610d708361106d565b81518152610d818760208401610995565b6020820152610d938760408401610995565b6040820152610da58760608401610995565b606082015260808201516080820152610dc18760a08401610995565b60a082015260c082015160c082015260e082015160e08201526101009350610deb87858401610995565b848201526101209350610e0087858401610a57565b938101939093526101408181015190840152610160808201519084015261018092610e2d87858401610ba7565b848201526101a093508382015183811115610e46578586fd5b610e52888285016109a0565b948201949094529695505050505050565b600060208284031215610e74578081fd5b5035919050565b6001600160a01b0391909116815260200190565b6001600160a01b039384168152919092166020820152604081019190915260600190565b901515815260200190565b90815260200190565b60006020825282602083015282846040840137818301604090810191909152601f909201601f19160101919050565b6000602082528251806020840152815b81811015610f235760208186018101516040868401015201610f06565b81811115610f345782604083860101525b50601f01601f19169190910160400192915050565b60208082526025908201527f496e73756666696369656e742066756e6473207472616e7366657272656420666040820152646f7220504f60d81b606082015260800190565b60208082526039908201527f436f756c64206e6f742066696e6420427573696e657373506172746e6572537460408201527f6f72616765206164647265737320696e20726567697374727900000000000000606082015260800190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b6020808252602d908201527f436f756c64206e6f742066696e642050757263686173696e672061646472657360408201526c7320696e20726567697374727960981b606082015260800190565b60405181810167ffffffffffffffff8111828210171561108c57600080fd5b604052919050565b6001600160a01b03811681146102e157600080fdfea2646970667358221220d8da6c4a4751aae9084461f14edbe1a1192e0438fe1deabc3d63e910fb8e504164736f6c63430006010033";
        public FundingDeploymentBase() : base(BYTECODE) { }
        public FundingDeploymentBase(string byteCode) : base(byteCode) { }
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

    public partial class BusinessPartnerStorageFunction : BusinessPartnerStorageFunctionBase { }

    [Function("businessPartnerStorage", "address")]
    public class BusinessPartnerStorageFunctionBase : FunctionMessage
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

    public partial class ConfigureFunction : ConfigureFunctionBase { }

    [Function("configure")]
    public class ConfigureFunctionBase : FunctionMessage
    {
        [Parameter("string", "nameOfPurchasing", 1)]
        public virtual string NameOfPurchasing { get; set; }
        [Parameter("string", "nameOfBusinessPartnerStorage", 2)]
        public virtual string NameOfBusinessPartnerStorage { get; set; }
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

    public partial class PurchasingContractAddressFunction : PurchasingContractAddressFunctionBase { }

    [Function("purchasingContractAddress", "address")]
    public class PurchasingContractAddressFunctionBase : FunctionMessage
    {

    }

    public partial class StringToBytes32Function : StringToBytes32FunctionBase { }

    [Function("stringToBytes32", "bytes32")]
    public class StringToBytes32FunctionBase : FunctionMessage
    {
        [Parameter("string", "source", 1)]
        public virtual string Source { get; set; }
    }

    public partial class TransferInFundsForPoFromBuyerWalletFunction : TransferInFundsForPoFromBuyerWalletFunctionBase { }

    [Function("transferInFundsForPoFromBuyerWallet")]
    public class TransferInFundsForPoFromBuyerWalletFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
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



    public partial class BusinessPartnerStorageOutputDTO : BusinessPartnerStorageOutputDTOBase { }

    [FunctionOutput]
    public class BusinessPartnerStorageOutputDTOBase : IFunctionOutputDTO 
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

    public partial class PurchasingContractAddressOutputDTO : PurchasingContractAddressOutputDTOBase { }

    [FunctionOutput]
    public class PurchasingContractAddressOutputDTOBase : IFunctionOutputDTO 
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
