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

namespace Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition
{


    public partial class EternalStorageDeployment : EternalStorageDeploymentBase
    {
        public EternalStorageDeployment() : base(BYTECODE) { }
        public EternalStorageDeployment(string byteCode) : base(byteCode) { }
    }

    public class EternalStorageDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040819052600080546001600160a01b03191633178082556001600160a01b0316917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a36110f3806100576000396000f3fe608060405234801561001057600080fd5b50600436106101ce5760003560e01c80638da5cb5b11610104578063d57df4da116100a2578063f2fde38b11610071578063f2fde38b146105cb578063f527d166146105f1578063f586606614610614578063f8f1aefc146106c1576101ce565b8063d57df4da14610530578063dde5b9d51461054d578063e0f5bc2314610570578063e24c2788146105a2576101ce565b8063a209a29c116100de578063a209a29c14610453578063abefab87146104e5578063ac094553146104ed578063c76ea9781461050a576101ce565b80638da5cb5b146104115780638f32d59b1461041957806391f4548014610421576101ce565b80634c77e5ba116101715780635a2bf25a1161014b5780635a2bf25a14610370578063623cd16d1461039c57806367d96412146103c85780636b00e9d8146103eb576101ce565b80634c77e5ba146102f1578063508e3e2f1461032a57806358360cc01461034d576101ce565b806317e7dd22116101ad57806317e7dd221461024d57806325cf512d1461027e5780633eba9ed2146102a15780634345e099146102c6576101ce565b8062f2678e146101d3578063025ec81a14610208578063150e99f914610225575b600080fd5b6101f6600480360360408110156101e957600080fd5b50803590602001356106ea565b60408051918252519081900360200190f35b6101f66004803603602081101561021e57600080fd5b5035610707565b61024b6004803603602081101561023b57600080fd5b50356001600160a01b0316610719565b005b61026a6004803603602081101561026357600080fd5b5035610820565b604080519115158252519081900360200190f35b61024b6004803603604081101561029457600080fd5b5080359060200135610835565b61024b600480360360408110156102b757600080fd5b508035906020013515156108c1565b61024b600480360360608110156102dc57600080fd5b5080359060208101359060400135151561091b565b61030e6004803603602081101561030757600080fd5b5035610981565b604080516001600160a01b039092168252519081900360200190f35b61024b6004803603604081101561034057600080fd5b508035906020013561099c565b61026a6004803603604081101561036357600080fd5b50803590602001356109ed565b61024b6004803603604081101561038657600080fd5b50803590602001356001600160a01b0316610a0d565b6101f6600480360360408110156103b257600080fd5b50803590602001356001600160a01b0316610a74565b6101f6600480360360408110156103de57600080fd5b5080359060200135610a9c565b61026a6004803603602081101561040157600080fd5b50356001600160a01b0316610ab9565b61030e610ace565b61026a610ade565b61024b6004803603606081101561043757600080fd5b50803590602081013590604001356001600160a01b0316610aef565b6104706004803603602081101561046957600080fd5b5035610b61565b6040805160208082528351818301528351919283929083019185019080838360005b838110156104aa578181015183820152602001610492565b50505050905090810190601f1680156104d75780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b6101f6610c02565b6101f66004803603602081101561050357600080fd5b5035610c08565b61024b6004803603602081101561052057600080fd5b50356001600160a01b0316610c1a565b6101f66004803603602081101561054657600080fd5b5035610d24565b61024b6004803603604081101561056357600080fd5b5080359060200135610d36565b61024b6004803603606081101561058657600080fd5b508035906001600160a01b036020820135169060400135610d87565b61024b600480360360608110156105b857600080fd5b5080359060208101359060400135610dec565b61024b600480360360208110156105e157600080fd5b50356001600160a01b0316610e48565b61030e6004803603604081101561060757600080fd5b5080359060200135610eaa565b61024b6004803603604081101561062a57600080fd5b8135919081019060408101602082013564010000000081111561064c57600080fd5b82018360208201111561065e57600080fd5b8035906020019184600183028401116401000000008311171561068057600080fd5b91908080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250929550610ed0945050505050565b61024b600480360360608110156106d757600080fd5b5080359060208101359060400135610f30565b6000918252600a6020908152604080842092845291905290205490565b60009081526007602052604090205490565b610721610ade565b610772576040805162461bcd60e51b815260206004820181905260248201527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604482015290519081900360640190fd5b6001600160a01b03811660009081526001602052604090205460ff16156107e8576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a261081d565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b60009081526008602052604090205460ff1690565b61083d610ace565b6001600160a01b0316336001600160a01b0316148061086b57503360009081526001602052604090205460ff165b156108865760008281526007602052604090208190556108bd565b60405162461bcd60e51b815260040180806020018281038252603e815260200180611080603e913960400191505060405180910390fd5b5050565b6108c9610ace565b6001600160a01b0316336001600160a01b031614806108f757503360009081526001602052604090205460ff165b15610886576000828152600860205260409020805460ff19168215151790556108bd565b610923610ace565b6001600160a01b0316336001600160a01b0316148061095157503360009081526001602052604090205460ff165b15610886576000838152600c602090815260408083208584529091529020805460ff19168215151790555b505050565b6000908152600660205260409020546001600160a01b031690565b6109a4610ace565b6001600160a01b0316336001600160a01b031614806109d257503360009081526001602052604090205460ff165b156108865760008281526004602052604090208190556108bd565b6000918252600c6020908152604080842092845291905290205460ff1690565b610a15610ace565b6001600160a01b0316336001600160a01b03161480610a4357503360009081526001602052604090205460ff165b1561088657600082815260066020526040902080546001600160a01b0319166001600160a01b0383161790556108bd565b6000918252600d602090815260408084206001600160a01b0393909316845291905290205490565b600091825260096020908152604080842092845291905290205490565b60016020526000908152604090205460ff1681565b6000546001600160a01b03165b90565b6000546001600160a01b0316331490565b610af7610ace565b6001600160a01b0316336001600160a01b03161480610b2557503360009081526001602052604090205460ff165b15610886576000838152600b60209081526040808320858452909152902080546001600160a01b0319166001600160a01b03831617905561097c565b60008181526005602090815260409182902080548351601f6002600019610100600186161502019093169290920491820184900484028101840190945280845260609392830182828015610bf65780601f10610bcb57610100808354040283529160200191610bf6565b820191906000526020600020905b815481529060010190602001808311610bd957829003601f168201915b50505050509050919050565b60025481565b60009081526003602052604090205490565b610c22610ade565b610c73576040805162461bcd60e51b815260206004820181905260248201527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604482015290519081900360640190fd5b6001600160a01b03811660009081526001602052604090205460ff1615610ccd576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a261081d565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b60009081526004602052604090205490565b610d3e610ace565b6001600160a01b0316336001600160a01b03161480610d6c57503360009081526001602052604090205460ff165b156108865760008281526003602052604090208190556108bd565b610d8f610ace565b6001600160a01b0316336001600160a01b03161480610dbd57503360009081526001602052604090205460ff165b15610886576000838152600d602090815260408083206001600160a01b0386168452909152902081905561097c565b610df4610ace565b6001600160a01b0316336001600160a01b03161480610e2257503360009081526001602052604090205460ff165b15610886576000838152600960209081526040808320858452909152902081905561097c565b610e50610ade565b610ea1576040805162461bcd60e51b815260206004820181905260248201527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604482015290519081900360640190fd5b61081d81610f8c565b6000918252600b602090815260408084209284529190529020546001600160a01b031690565b610ed8610ace565b6001600160a01b0316336001600160a01b03161480610f0657503360009081526001602052604090205460ff165b156108865760008281526005602090815260409091208251610f2a92840190610fe7565b506108bd565b610f38610ace565b6001600160a01b0316336001600160a01b03161480610f6657503360009081526001602052604090205460ff165b15610886576000838152600a60209081526040808320858452909152902081905561097c565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061102857805160ff1916838001178555611055565b82800160010185558215611055579182015b8281111561105557825182559160200191906001019061103a565b50611061929150611065565b5090565b610adb91905b80821115611061576000815560010161106b56fe4f6e6c7920636f6e7472616374206f776e6572206f72206120626f756e642061646472657373206d61792063616c6c20746869732066756e6374696f6e2ea26469706673582212200569d98a6b945c6b93f49e0b93773e6d7486820ade8866454f3fd1bdea5640ce64736f6c63430006010033";
        public EternalStorageDeploymentBase() : base(BYTECODE) { }
        public EternalStorageDeploymentBase(string byteCode) : base(byteCode) { }

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

    public partial class BindAddressFunction : BindAddressFunctionBase { }

    [Function("bindAddress")]
    public class BindAddressFunctionBase : FunctionMessage
    {
        [Parameter("address", "a", 1)]
        public virtual string A { get; set; }
    }

    public partial class GetAddressValueFunction : GetAddressValueFunctionBase { }

    [Function("getAddressValue", "address")]
    public class GetAddressValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
    }

    public partial class GetBooleanValueFunction : GetBooleanValueFunctionBase { }

    [Function("getBooleanValue", "bool")]
    public class GetBooleanValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
    }

    public partial class GetBytes32ValueFunction : GetBytes32ValueFunctionBase { }

    [Function("getBytes32Value", "bytes32")]
    public class GetBytes32ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
    }

    public partial class GetInt256ValueFunction : GetInt256ValueFunctionBase { }

    [Function("getInt256Value", "int256")]
    public class GetInt256ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
    }

    public partial class GetMappingAddressToUint256ValueFunction : GetMappingAddressToUint256ValueFunctionBase { }

    [Function("getMappingAddressToUint256Value", "uint256")]
    public class GetMappingAddressToUint256ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("address", "mappingKey", 2)]
        public virtual string MappingKey { get; set; }
    }

    public partial class GetMappingBytes32ToAddressValueFunction : GetMappingBytes32ToAddressValueFunctionBase { }

    [Function("getMappingBytes32ToAddressValue", "address")]
    public class GetMappingBytes32ToAddressValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("bytes32", "mappingKey", 2)]
        public virtual byte[] MappingKey { get; set; }
    }

    public partial class GetMappingBytes32ToBoolValueFunction : GetMappingBytes32ToBoolValueFunctionBase { }

    [Function("getMappingBytes32ToBoolValue", "bool")]
    public class GetMappingBytes32ToBoolValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("bytes32", "mappingKey", 2)]
        public virtual byte[] MappingKey { get; set; }
    }

    public partial class GetMappingBytes32ToBytes32ValueFunction : GetMappingBytes32ToBytes32ValueFunctionBase { }

    [Function("getMappingBytes32ToBytes32Value", "bytes32")]
    public class GetMappingBytes32ToBytes32ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("bytes32", "mappingKey", 2)]
        public virtual byte[] MappingKey { get; set; }
    }

    public partial class GetMappingBytes32ToUint256ValueFunction : GetMappingBytes32ToUint256ValueFunctionBase { }

    [Function("getMappingBytes32ToUint256Value", "uint256")]
    public class GetMappingBytes32ToUint256ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("bytes32", "mappingKey", 2)]
        public virtual byte[] MappingKey { get; set; }
    }

    public partial class GetStringValueFunction : GetStringValueFunctionBase { }

    [Function("getStringValue", "string")]
    public class GetStringValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
    }

    public partial class GetUint256ValueFunction : GetUint256ValueFunctionBase { }

    [Function("getUint256Value", "uint256")]
    public class GetUint256ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
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

    public partial class SetAddressValueFunction : SetAddressValueFunctionBase { }

    [Function("setAddressValue")]
    public class SetAddressValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
        [Parameter("address", "value", 2)]
        public virtual string Value { get; set; }
    }

    public partial class SetBooleanValueFunction : SetBooleanValueFunctionBase { }

    [Function("setBooleanValue")]
    public class SetBooleanValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
        [Parameter("bool", "value", 2)]
        public virtual bool Value { get; set; }
    }

    public partial class SetBytes32ValueFunction : SetBytes32ValueFunctionBase { }

    [Function("setBytes32Value")]
    public class SetBytes32ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
        [Parameter("bytes32", "value", 2)]
        public virtual byte[] Value { get; set; }
    }

    public partial class SetInt256ValueFunction : SetInt256ValueFunctionBase { }

    [Function("setInt256Value")]
    public class SetInt256ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
        [Parameter("int256", "value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class SetMappingAddressToUint256ValueFunction : SetMappingAddressToUint256ValueFunctionBase { }

    [Function("setMappingAddressToUint256Value")]
    public class SetMappingAddressToUint256ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("address", "mappingKey", 2)]
        public virtual string MappingKey { get; set; }
        [Parameter("uint256", "value", 3)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class SetMappingBytes32ToAddressValueFunction : SetMappingBytes32ToAddressValueFunctionBase { }

    [Function("setMappingBytes32ToAddressValue")]
    public class SetMappingBytes32ToAddressValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("bytes32", "mappingKey", 2)]
        public virtual byte[] MappingKey { get; set; }
        [Parameter("address", "value", 3)]
        public virtual string Value { get; set; }
    }

    public partial class SetMappingBytes32ToBoolValueFunction : SetMappingBytes32ToBoolValueFunctionBase { }

    [Function("setMappingBytes32ToBoolValue")]
    public class SetMappingBytes32ToBoolValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("bytes32", "mappingKey", 2)]
        public virtual byte[] MappingKey { get; set; }
        [Parameter("bool", "value", 3)]
        public virtual bool Value { get; set; }
    }

    public partial class SetMappingBytes32ToBytes32ValueFunction : SetMappingBytes32ToBytes32ValueFunctionBase { }

    [Function("setMappingBytes32ToBytes32Value")]
    public class SetMappingBytes32ToBytes32ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("bytes32", "mappingKey", 2)]
        public virtual byte[] MappingKey { get; set; }
        [Parameter("bytes32", "value", 3)]
        public virtual byte[] Value { get; set; }
    }

    public partial class SetMappingBytes32ToUint256ValueFunction : SetMappingBytes32ToUint256ValueFunctionBase { }

    [Function("setMappingBytes32ToUint256Value")]
    public class SetMappingBytes32ToUint256ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "storageKey", 1)]
        public virtual byte[] StorageKey { get; set; }
        [Parameter("bytes32", "mappingKey", 2)]
        public virtual byte[] MappingKey { get; set; }
        [Parameter("uint256", "value", 3)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class SetStringValueFunction : SetStringValueFunctionBase { }

    [Function("setStringValue")]
    public class SetStringValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
        [Parameter("string", "value", 2)]
        public virtual string Value { get; set; }
    }

    public partial class SetUint256ValueFunction : SetUint256ValueFunctionBase { }

    [Function("setUint256Value")]
    public class SetUint256ValueFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
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



    public partial class GetAddressValueOutputDTO : GetAddressValueOutputDTOBase { }

    [FunctionOutput]
    public class GetAddressValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetBooleanValueOutputDTO : GetBooleanValueOutputDTOBase { }

    [FunctionOutput]
    public class GetBooleanValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GetBytes32ValueOutputDTO : GetBytes32ValueOutputDTOBase { }

    [FunctionOutput]
    public class GetBytes32ValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class GetInt256ValueOutputDTO : GetInt256ValueOutputDTOBase { }

    [FunctionOutput]
    public class GetInt256ValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetMappingAddressToUint256ValueOutputDTO : GetMappingAddressToUint256ValueOutputDTOBase { }

    [FunctionOutput]
    public class GetMappingAddressToUint256ValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetMappingBytes32ToAddressValueOutputDTO : GetMappingBytes32ToAddressValueOutputDTOBase { }

    [FunctionOutput]
    public class GetMappingBytes32ToAddressValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetMappingBytes32ToBoolValueOutputDTO : GetMappingBytes32ToBoolValueOutputDTOBase { }

    [FunctionOutput]
    public class GetMappingBytes32ToBoolValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GetMappingBytes32ToBytes32ValueOutputDTO : GetMappingBytes32ToBytes32ValueOutputDTOBase { }

    [FunctionOutput]
    public class GetMappingBytes32ToBytes32ValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class GetMappingBytes32ToUint256ValueOutputDTO : GetMappingBytes32ToUint256ValueOutputDTOBase { }

    [FunctionOutput]
    public class GetMappingBytes32ToUint256ValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetStringValueOutputDTO : GetStringValueOutputDTOBase { }

    [FunctionOutput]
    public class GetStringValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetUint256ValueOutputDTO : GetUint256ValueOutputDTOBase { }

    [FunctionOutput]
    public class GetUint256ValueOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
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
