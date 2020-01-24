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

namespace Nethereum.Commerce.Contracts.AddressRegistry.ContractDefinition
{


    public partial class AddressRegistryDeployment : AddressRegistryDeploymentBase
    {
        public AddressRegistryDeployment() : base(BYTECODE) { }
        public AddressRegistryDeployment(string byteCode) : base(byteCode) { }
    }

    public class AddressRegistryDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060408190526000600381905580546001600160a01b03191633178082556001600160a01b0316917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3610a5d8061005c6000396000f3fe608060405234801561001057600080fd5b50600436106100a95760003560e01c80638f32d59b116100715780638f32d59b146101275780639516a1041461013c578063b7dfc97914610152578063cfb5192814610165578063d9c4c15314610185578063f2fde38b14610198576100a9565b806321f8a721146100ae5780632cc3b2fb146100d7578063662de379146100ec5780638da5cb5b146100ff5780638e5fc30b14610107575b600080fd5b6100c16100bc366004610769565b6101ab565b6040516100ce91906108bc565b60405180910390f35b6100ea6100e5366004610817565b6101c9565b005b6100ea6100fa366004610781565b610212565b6100c161036d565b61011a6101153660046107bb565b61037c565b6040516100ce9190610982565b61012f6104a6565b6040516100ce919061096e565b6101446104b7565b6040516100ce9291906108d0565b6100c1610160366004610769565b6105d7565b6101786101733660046107dc565b6105f2565b6040516100ce9190610979565b6100c16101933660046107dc565b610610565b6100ea6101a636600461074e565b61062e565b6000818152600160205260409020546001600160a01b03165b919050565b6101d16104a6565b6101f65760405162461bcd60e51b81526004016101ed90610995565b60405180910390fd5b6000610201836105f2565b905061020d8183610212565b505050565b61021a6104a6565b6102365760405162461bcd60e51b81526004016101ed90610995565b6001600160a01b03811661025c5760405162461bcd60e51b81526004016101ed906109ca565b6000828152600160205260409020546001600160a01b03168061030f57600083815260016020819052604080832080546001600160a01b0319166001600160a01b0387169081179091556002805480850182559085527f405787fa12a823e0f2b7631cc41b3ba8828b3321ca811111fa75cd3aa3bb5ace018790556003805490930190925551909185917f5eb4c6beca7ccf6f6394636d7109497abbd6c326e528c3efab53ae58031188959190a361020d565b60008381526001602052604080822080546001600160a01b0319166001600160a01b03868116918217909255915191929084169186917fd79605f6cff49b16d2f83477ad235178dbe333807ab20e95d695e875525ab98491a4505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b60208110156103fa576008810260020a86026001600160f81b03198116156103f157808484815181106103d357fe5b60200101906001600160f81b031916908160001a9053506001909201915b506001016103a4565b50600081851180610409575084155b1561041557508061041c565b5060001984015b6060816040519080825280601f01601f191660200182016040528015610449576020820181803883390190505b50905060005b828110156104995784818151811061046357fe5b602001015160f81c60f81b82828151811061047a57fe5b60200101906001600160f81b031916908160001a90535060010161044f565b5093505050505b92915050565b6000546001600160a01b0316331490565b6060806003546040519080825280602002602001820160405280156104f057816020015b60608152602001906001900390816104db5790505b50915060035460405190808252806020026020018201604052801561051f578160200160208202803883390190505b50905060005b6003548110156105d2576105516002828154811061053f57fe5b9060005260206000200154600061037c565b83828151811061055d57fe5b6020026020010181905250600160006002838154811061057957fe5b9060005260206000200154815260200190815260200160002060009054906101000a90046001600160a01b03168282815181106105b257fe5b6001600160a01b0390921660209283029190910190910152600101610525565b509091565b6001602052600090815260409020546001600160a01b031681565b805160009082906106075750600090506101c4565b50506020015190565b60008061061c836105f2565b9050610627816101ab565b9392505050565b6106366104a6565b6106525760405162461bcd60e51b81526004016101ed90610995565b61065b8161065e565b50565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b80356001600160a01b03811681146104a057600080fd5b600082601f8301126106e0578081fd5b813567ffffffffffffffff808211156106f7578283fd5b604051601f8301601f191681016020018281118282101715610717578485fd5b60405282815292508284830160200186101561073257600080fd5b8260208601602083013760006020848301015250505092915050565b60006020828403121561075f578081fd5b61062783836106b9565b60006020828403121561077a578081fd5b5035919050565b60008060408385031215610793578081fd5b8235915060208301356001600160a01b03811681146107b0578182fd5b809150509250929050565b600080604083850312156107cd578182fd5b50508035926020909101359150565b6000602082840312156107ed578081fd5b813567ffffffffffffffff811115610803578182fd5b61080f848285016106d0565b949350505050565b60008060408385031215610829578182fd5b823567ffffffffffffffff81111561083f578283fd5b61084b858286016106d0565b92505061085b84602085016106b9565b90509250929050565b6001600160a01b03169052565b60008151808452815b818110156108965760208185018101518683018201520161087a565b818111156108a75782602083870101525b50601f01601f19169290920160200192915050565b6001600160a01b0391909116815260200190565b600060408201604083528085516108e78184610979565b915081925060208082028301818901865b84811015610922578683038652610910838351610871565b958401959250908301906001016108f8565b5050868103828801528751808252908201945092508681019150845b8381101561096157610951858451610864565b938101939181019160010161093e565b5092979650505050505050565b901515815260200190565b90815260200190565b6000602082526106276020830184610871565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b60208082526038908201527f416464726573732063616e6e6f74206265203078302c2075736520307831207460408201527f6f2064652d726567697374657220616e2061646472657373000000000000000060608201526080019056fea2646970667358221220a36b6d5a2432a92f92d5e2a9557acb798d07e66f3b7a5047111ac20abec1684a64736f6c63430006010033";
        public AddressRegistryDeploymentBase() : base(BYTECODE) { }
        public AddressRegistryDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddressMapFunction : AddressMapFunctionBase { }

    [Function("addressMap", "address")]
    public class AddressMapFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
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

    public partial class GetAddressFunction : GetAddressFunctionBase { }

    [Function("getAddress", "address")]
    public class GetAddressFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "contractName", 1)]
        public virtual byte[] ContractName { get; set; }
    }

    public partial class GetAddressStringFunction : GetAddressStringFunctionBase { }

    [Function("getAddressString", "address")]
    public class GetAddressStringFunctionBase : FunctionMessage
    {
        [Parameter("string", "contractName", 1)]
        public virtual string ContractName { get; set; }
    }

    public partial class GetAllAddressesFunction : GetAllAddressesFunctionBase { }

    [Function("getAllAddresses", typeof(GetAllAddressesOutputDTO))]
    public class GetAllAddressesFunctionBase : FunctionMessage
    {

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

    public partial class RegisterAddressFunction : RegisterAddressFunctionBase { }

    [Function("registerAddress")]
    public class RegisterAddressFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "contractName", 1)]
        public virtual byte[] ContractName { get; set; }
        [Parameter("address", "a", 2)]
        public virtual string A { get; set; }
    }

    public partial class RegisterAddressStringFunction : RegisterAddressStringFunctionBase { }

    [Function("registerAddressString")]
    public class RegisterAddressStringFunctionBase : FunctionMessage
    {
        [Parameter("string", "contractName", 1)]
        public virtual string ContractName { get; set; }
        [Parameter("address", "a", 2)]
        public virtual string A { get; set; }
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

    public partial class ContractAddressChangedEventDTO : ContractAddressChangedEventDTOBase { }

    [Event("ContractAddressChanged")]
    public class ContractAddressChangedEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "contractName", 1, true )]
        public virtual byte[] ContractName { get; set; }
        [Parameter("address", "oldContractAddress", 2, true )]
        public virtual string OldContractAddress { get; set; }
        [Parameter("address", "newContractAddress", 3, true )]
        public virtual string NewContractAddress { get; set; }
    }

    public partial class ContractAddressRegisteredEventDTO : ContractAddressRegisteredEventDTOBase { }

    [Event("ContractAddressRegistered")]
    public class ContractAddressRegisteredEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "contractName", 1, true )]
        public virtual byte[] ContractName { get; set; }
        [Parameter("address", "contractAddress", 2, true )]
        public virtual string ContractAddress { get; set; }
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

    public partial class AddressMapOutputDTO : AddressMapOutputDTOBase { }

    [FunctionOutput]
    public class AddressMapOutputDTOBase : IFunctionOutputDTO 
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

    public partial class GetAddressOutputDTO : GetAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "a", 1)]
        public virtual string A { get; set; }
    }

    public partial class GetAddressStringOutputDTO : GetAddressStringOutputDTOBase { }

    [FunctionOutput]
    public class GetAddressStringOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "a", 1)]
        public virtual string A { get; set; }
    }

    public partial class GetAllAddressesOutputDTO : GetAllAddressesOutputDTOBase { }

    [FunctionOutput]
    public class GetAllAddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string[]", "contractNames", 1)]
        public virtual List<string> ContractNames { get; set; }
        [Parameter("address[]", "contractAddresses", 2)]
        public virtual List<string> ContractAddresses { get; set; }
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





    public partial class StringToBytes32OutputDTO : StringToBytes32OutputDTOBase { }

    [FunctionOutput]
    public class StringToBytes32OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "result", 1)]
        public virtual byte[] Result { get; set; }
    }


}
