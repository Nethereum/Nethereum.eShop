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
        public static string BYTECODE = "608060405234801561001057600080fd5b50604051610ef0380380610ef083398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600480546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b610e16806100da6000396000f3fe608060405234801561001057600080fd5b50600436106101005760003560e01c806398ff9c5411610097578063cf319e2811610066578063cf319e28146101fc578063cfb519281461020f578063f2fde38b14610222578063f3ad65f41461023557610100565b806398ff9c54146101b9578063abefab87146101c1578063af0577f3146101d6578063c76ea978146101e957610100565b806385057a09116100d357806385057a09146101765780638da5cb5b146101895780638e5fc30b146101915780638f32d59b146101b157610100565b8063150e99f914610105578063206d54db1461011a5780633e849905146101435780636b00e9d814610156575b600080fd5b610118610113366004610ac4565b61023d565b005b61012d610128366004610b03565b610318565b60405161013a9190610c27565b60405180910390f35b610118610151366004610b33565b61040b565b610169610164366004610ac4565b610535565b60405161013a9190610c3b565b610118610184366004610b83565b61054a565b61012d610628565b6101a461019f366004610b62565b610637565b60405161013a9190610c89565b61016961075f565b61012d610770565b6101c961077f565b60405161013a9190610c1e565b6101186101e4366004610b62565b610785565b6101186101f7366004610ac4565b610856565b6101c961020a366004610b03565b61092b565b6101c961021d366004610b83565b610a0f565b610118610230366004610ac4565b610a2d565b61012d610a5a565b61024561075f565b61026a5760405162461bcd60e51b815260040161026190610cdc565b60405180910390fd5b6001600160a01b03811660009081526001602052604090205460ff16156102e0576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a2610315565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b6000808260405160200161032c9190610c1e565b60408051601f198184030181528282528051602091820120600354848401909352601a84527f6d617053797374656d4964546f57616c6c6574416464726573730000000000009184019190915292506001600160a01b03169063f527d1669061039490610a0f565b836040518363ffffffff1660e01b81526004016103b2929190610c46565b60206040518083038186803b1580156103ca57600080fd5b505afa1580156103de573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104029190810190610ae7565b9150505b919050565b610413610628565b6001600160a01b0316336001600160a01b0316148061044157503360009081526001602052604090205460ff165b15610519576000826040516020016104599190610c1e565b60408051601f198184030181528282528051602091820120600354848401909352601a84527f6d617053797374656d4964546f57616c6c6574416464726573730000000000009184019190915292506001600160a01b0316906391f45480906104c190610a0f565b83856040518463ffffffff1660e01b81526004016104e193929190610c54565b600060405180830381600087803b1580156104fb57600080fd5b505af115801561050f573d6000803e3d6000fd5b5050505050610531565b60405162461bcd60e51b815260040161026190610d62565b5050565b60016020526000908152604090205460ff1681565b61055261075f565b61056e5760405162461bcd60e51b815260040161026190610cdc565b6004805460405163d9c4c15360e01b81526001600160a01b039091169163d9c4c1539161059d91859101610c89565b60206040518083038186803b1580156105b557600080fd5b505afa1580156105c9573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506105ed9190810190610ae7565b600380546001600160a01b0319166001600160a01b039283161790819055166103155760405162461bcd60e51b815260040161026190610d11565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b60208110156106b5576008810260020a86026001600160f81b03198116156106ac578084848151811061068e57fe5b60200101906001600160f81b031916908160001a9053506001909201915b5060010161065f565b506000818511806106c4575084155b156106d05750806106d7565b5060001984015b6060816040519080825280601f01601f191660200182016040528015610704576020820181803883390190505b50905060005b828110156107545784818151811061071e57fe5b602001015160f81c60f81b82828151811061073557fe5b60200101906001600160f81b031916908160001a90535060010161070a565b509695505050505050565b6000546001600160a01b0316331490565b6003546001600160a01b031681565b60025481565b61078d610628565b6001600160a01b0316336001600160a01b031614806107bb57503360009081526001602052604090205460ff165b15610519576000826040516020016107d39190610c1e565b60408051601f198184030181528282528051602091820120600354848401909352601884527736b0b829bcb9ba32b6a4b22a37a232b9b1b934b83a34b7b760411b9184019190915292506001600160a01b03169063f8f1aefc9061083690610a0f565b83856040518463ffffffff1660e01b81526004016104e193929190610c73565b61085e61075f565b61087a5760405162461bcd60e51b815260040161026190610cdc565b6001600160a01b03811660009081526001602052604090205460ff16156108d4576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a2610315565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b6000808260405160200161093f9190610c1e565b60408051601f198184030181528282528051602091820120600354848401909352601884527736b0b829bcb9ba32b6a4b22a37a232b9b1b934b83a34b7b760411b9184019190915292506001600160a01b03169062f2678e906109a190610a0f565b836040518363ffffffff1660e01b81526004016109bf929190610c46565b60206040518083038186803b1580156109d757600080fd5b505afa1580156109eb573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104029190810190610b1b565b80516000908290610a24575060009050610406565b50506020015190565b610a3561075f565b610a515760405162461bcd60e51b815260040161026190610cdc565b61031581610a69565b6004546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b600060208284031215610ad5578081fd5b8135610ae081610dcb565b9392505050565b600060208284031215610af8578081fd5b8151610ae081610dcb565b600060208284031215610b14578081fd5b5035919050565b600060208284031215610b2c578081fd5b5051919050565b60008060408385031215610b45578081fd5b823591506020830135610b5781610dcb565b809150509250929050565b60008060408385031215610b74578182fd5b50508035926020909101359150565b600060208284031215610b94578081fd5b813567ffffffffffffffff80821115610bab578283fd5b81840185601f820112610bbc578384fd5b8035925081831115610bcc578384fd5b604051601f8401601f191681016020018381118282101715610bec578586fd5b604052838152818401602001871015610c03578485fd5b610c14846020830160208501610dbf565b9695505050505050565b90815260200190565b6001600160a01b0391909116815260200190565b901515815260200190565b918252602082015260400190565b92835260208301919091526001600160a01b0316604082015260600190565b9283526020830191909152604082015260600190565b6000602082528251806020840152815b81811015610cb65760208186018101516040868401015201610c99565b81811115610cc75782604083860101525b50601f01601f19169190910160400192915050565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b60208082526031908201527f436f756c64206e6f742066696e6420457465726e616c53746f72616765206164604082015270647265737320696e20726567697374727960781b606082015260800190565b6020808252603e908201527f4f6e6c7920636f6e7472616374206f776e6572206f72206120626f756e64206160408201527f646472657373206d61792063616c6c20746869732066756e6374696f6e2e0000606082015260800190565b82818337506000910152565b6001600160a01b038116811461031557600080fdfea2646970667358221220fe3f632926c1a658169e456efd29410a4e48311456256cc7e7e976e854aa007964736f6c63430006010033";
        public BusinessPartnerStorageDeploymentBase() : base(BYTECODE) { }
        public BusinessPartnerStorageDeploymentBase(string byteCode) : base(byteCode) { }
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
