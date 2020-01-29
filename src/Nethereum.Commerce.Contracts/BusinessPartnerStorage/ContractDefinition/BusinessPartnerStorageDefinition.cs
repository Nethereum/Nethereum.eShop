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

namespace Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition
{


    public partial class BusinessPartnerStorageDeployment : BusinessPartnerStorageDeploymentBase
    {
        public BusinessPartnerStorageDeployment() : base(BYTECODE) { }
        public BusinessPartnerStorageDeployment(string byteCode) : base(byteCode) { }
    }

    public class BusinessPartnerStorageDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516109f33803806109f383398101604081905261002f91610054565b600180546001600160a01b0319166001600160a01b0392909216919091179055610082565b600060208284031215610065578081fd5b81516001600160a01b038116811461007b578182fd5b9392505050565b610962806100916000396000f3fe608060405234801561001057600080fd5b50600436106100935760003560e01c806398ff9c541161006657806398ff9c5414610109578063af0577f314610111578063cf319e2814610124578063cfb5192814610144578063f3ad65f41461015757610093565b8063206d54db146100985780633e849905146100c157806385057a09146100d65780638e5fc30b146100e9575b600080fd5b6100ab6100a63660046106ec565b61015f565b6040516100b89190610810565b60405180910390f35b6100d46100cf36600461071c565b610252565b005b6100d46100e436600461076c565b610324565b6100fc6100f736600461074b565b6103eb565b6040516100b89190610867565b6100ab610513565b6100d461011f36600461074b565b610522565b6101376101323660046106ec565b6105b8565b6040516100b89190610807565b61013761015236600461076c565b61069c565b6100ab6106ba565b600080826040516020016101739190610807565b60408051601f198184030181528282528051602091820120600054848401909352601a84527f6d617053797374656d4964546f57616c6c6574416464726573730000000000009184019190915292506001600160a01b03169063f527d166906101db9061069c565b836040518363ffffffff1660e01b81526004016101f9929190610824565b60206040518083038186803b15801561021157600080fd5b505afa158015610225573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061024991908101906106c9565b9150505b919050565b6000826040516020016102659190610807565b60408051601f198184030181528282528051602091820120600054848401909352601a84527f6d617053797374656d4964546f57616c6c6574416464726573730000000000009184019190915292506001600160a01b0316906391f45480906102cd9061069c565b83856040518463ffffffff1660e01b81526004016102ed93929190610832565b600060405180830381600087803b15801561030757600080fd5b505af115801561031b573d6000803e3d6000fd5b50505050505050565b60015460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c15390610354908490600401610867565b60206040518083038186803b15801561036c57600080fd5b505afa158015610380573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103a491908101906106c9565b600080546001600160a01b0319166001600160a01b039283161790819055166103e85760405162461bcd60e51b81526004016103df906108ba565b60405180910390fd5b50565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b6020811015610469576008810260020a86026001600160f81b0319811615610460578084848151811061044257fe5b60200101906001600160f81b031916908160001a9053506001909201915b50600101610413565b50600081851180610478575084155b1561048457508061048b565b5060001984015b6060816040519080825280601f01601f1916602001820160405280156104b8576020820181803883390190505b50905060005b82811015610508578481815181106104d257fe5b602001015160f81c60f81b8282815181106104e957fe5b60200101906001600160f81b031916908160001a9053506001016104be565b509695505050505050565b6000546001600160a01b031681565b6000826040516020016105359190610807565b60408051601f198184030181528282528051602091820120600054848401909352601884527736b0b829bcb9ba32b6a4b22a37a232b9b1b934b83a34b7b760411b9184019190915292506001600160a01b03169063f8f1aefc906105989061069c565b83856040518463ffffffff1660e01b81526004016102ed93929190610851565b600080826040516020016105cc9190610807565b60408051601f198184030181528282528051602091820120600054848401909352601884527736b0b829bcb9ba32b6a4b22a37a232b9b1b934b83a34b7b760411b9184019190915292506001600160a01b03169062f2678e9061062e9061069c565b836040518363ffffffff1660e01b815260040161064c929190610824565b60206040518083038186803b15801561066457600080fd5b505afa158015610678573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506102499190810190610704565b805160009082906106b157506000905061024d565b50506020015190565b6001546001600160a01b031681565b6000602082840312156106da578081fd5b81516106e581610917565b9392505050565b6000602082840312156106fd578081fd5b5035919050565b600060208284031215610715578081fd5b5051919050565b6000806040838503121561072e578081fd5b82359150602083013561074081610917565b809150509250929050565b6000806040838503121561075d578182fd5b50508035926020909101359150565b60006020828403121561077d578081fd5b813567ffffffffffffffff80821115610794578283fd5b81840185601f8201126107a5578384fd5b80359250818311156107b5578384fd5b604051601f8401601f1916810160200183811182821017156107d5578586fd5b6040528381528184016020018710156107ec578485fd5b6107fd84602083016020850161090b565b9695505050505050565b90815260200190565b6001600160a01b0391909116815260200190565b918252602082015260400190565b92835260208301919091526001600160a01b0316604082015260600190565b9283526020830191909152604082015260600190565b6000602082528251806020840152815b818110156108945760208186018101516040868401015201610877565b818111156108a55782604083860101525b50601f01601f19169190910160400192915050565b60208082526031908201527f436f756c64206e6f742066696e6420457465726e616c53746f72616765206164604082015270647265737320696e20726567697374727960781b606082015260800190565b82818337506000910152565b6001600160a01b03811681146103e857600080fdfea2646970667358221220598d1338dcba8ac95a4aac89fbe407021711e2e2b704189c7c22d3df8a107ec364736f6c63430006010033";
        public BusinessPartnerStorageDeploymentBase() : base(BYTECODE) { }
        public BusinessPartnerStorageDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "contractAddressOfRegistry", 1)]
        public virtual string ContractAddressOfRegistry { get; set; }
    }

    public partial class AddressRegistryFunction : AddressRegistryFunctionBase { }

    [Function("addressRegistry", "address")]
    public class AddressRegistryFunctionBase : FunctionMessage
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
        [Parameter("string", "nameOfEternalStorage", 1)]
        public virtual string NameOfEternalStorage { get; set; }
    }

    public partial class EternalStorageFunction : EternalStorageFunctionBase { }

    [Function("eternalStorage", "address")]
    public class EternalStorageFunctionBase : FunctionMessage
    {

    }

    public partial class GetSystemDescriptionFunction : GetSystemDescriptionFunctionBase { }

    [Function("getSystemDescription", "bytes32")]
    public class GetSystemDescriptionFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "systemId", 1)]
        public virtual byte[] SystemId { get; set; }
    }

    public partial class GetWalletAddressFunction : GetWalletAddressFunctionBase { }

    [Function("getWalletAddress", "address")]
    public class GetWalletAddressFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "systemId", 1)]
        public virtual byte[] SystemId { get; set; }
    }

    public partial class SetSystemDescriptionFunction : SetSystemDescriptionFunctionBase { }

    [Function("setSystemDescription")]
    public class SetSystemDescriptionFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "systemId", 1)]
        public virtual byte[] SystemId { get; set; }
        [Parameter("bytes32", "systemDescription", 2)]
        public virtual byte[] SystemDescription { get; set; }
    }

    public partial class SetWalletAddressFunction : SetWalletAddressFunctionBase { }

    [Function("setWalletAddress")]
    public class SetWalletAddressFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "systemId", 1)]
        public virtual byte[] SystemId { get; set; }
        [Parameter("address", "walletAddress", 2)]
        public virtual string WalletAddress { get; set; }
    }

    public partial class StringToBytes32Function : StringToBytes32FunctionBase { }

    [Function("stringToBytes32", "bytes32")]
    public class StringToBytes32FunctionBase : FunctionMessage
    {
        [Parameter("string", "source", 1)]
        public virtual string Source { get; set; }
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



    public partial class EternalStorageOutputDTO : EternalStorageOutputDTOBase { }

    [FunctionOutput]
    public class EternalStorageOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetSystemDescriptionOutputDTO : GetSystemDescriptionOutputDTOBase { }

    [FunctionOutput]
    public class GetSystemDescriptionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "systemDescription", 1)]
        public virtual byte[] SystemDescription { get; set; }
    }

    public partial class GetWalletAddressOutputDTO : GetWalletAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetWalletAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "walletAddress", 1)]
        public virtual string WalletAddress { get; set; }
    }





    public partial class StringToBytes32OutputDTO : StringToBytes32OutputDTOBase { }

    [FunctionOutput]
    public class StringToBytes32OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "result", 1)]
        public virtual byte[] Result { get; set; }
    }
}
