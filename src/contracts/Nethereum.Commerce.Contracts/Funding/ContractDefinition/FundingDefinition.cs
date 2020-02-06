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
        public static string BYTECODE = "608060405234801561001057600080fd5b50604051610c84380380610c848339818101604052602081101561003357600080fd5b5051600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b0392909216919091179055610bd7806100ad6000396000f3fe608060405234801561001057600080fd5b50600436106101165760003560e01c8063908ce87f116100a2578063c016d9b611610071578063c016d9b614610396578063c76ea9781461039e578063cfb51928146103c4578063f2fde38b1461046a578063f3ad65f41461049057610116565b8063908ce87f146103395780639cf3037a14610143578063abefab8714610356578063ac764f2e1461037057610116565b80636b6a291a116100e95780636b6a291a146101c7578063802706cb146101cf5780638da5cb5b146102915780638e5fc30b146102995780638f32d59b1461033157610116565b8063150e99f91461011b57806340a0a2dd146101435780634360beb5146101695780636b00e9d81461018d575b600080fd5b6101416004803603602081101561013157600080fd5b50356001600160a01b0316610498565b005b6101416004803603604081101561015957600080fd5b508035906020013560ff1661059f565b6101716105a3565b604080516001600160a01b039092168252519081900360200190f35b6101b3600480360360208110156101a357600080fd5b50356001600160a01b03166105b2565b604080519115158252519081900360200190f35b6101716105c7565b610141600480360360408110156101e557600080fd5b81019060208101813564010000000081111561020057600080fd5b82018360208201111561021257600080fd5b8035906020019184600183028401116401000000008311171561023457600080fd5b91939092909160208101903564010000000081111561025257600080fd5b82018360208201111561026457600080fd5b8035906020019184600183028401116401000000008311171561028657600080fd5b5090925090506105d6565b6101716107e0565b6102bc600480360360408110156102af57600080fd5b50803590602001356107ef565b6040805160208082528351818301528351919283929083019185019080838360005b838110156102f65781810151838201526020016102de565b50505050905090810190601f1680156103235780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b6101b3610917565b6101416004803603602081101561034f57600080fd5b503561059c565b61035e610928565b60408051918252519081900360200190f35b61035e6004803603602081101561038657600080fd5b50356001600160a01b031661092e565b610171610934565b610141600480360360208110156103b457600080fd5b50356001600160a01b0316610943565b61035e600480360360208110156103da57600080fd5b8101906020810181356401000000008111156103f557600080fd5b82018360208201111561040757600080fd5b8035906020019184600183028401116401000000008311171561042957600080fd5b91908080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250929550610a4d945050505050565b6101416004803603602081101561048057600080fd5b50356001600160a01b0316610a6f565b610171610ad1565b6104a0610917565b6104f1576040805162461bcd60e51b815260206004820181905260248201527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604482015290519081900360640190fd5b6001600160a01b03811660009081526001602052604090205460ff1615610567576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a261059c565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b5050565b6004546001600160a01b031681565b60016020526000908152604090205460ff1681565b6006546001600160a01b031681565b60035460405163d9c4c15360e01b8152602060048201908152602482018490526001600160a01b039092169163d9c4c15391859185918190604401848480828437600083820152604051601f909101601f191690920195506020945090925050508083038186803b15801561064a57600080fd5b505afa15801561065e573d6000803e3d6000fd5b505050506040513d602081101561067457600080fd5b5051600480546001600160a01b0319166001600160a01b039283161790819055166106d05760405162461bcd60e51b8152600401808060200182810382526039815260200180610b3c6039913960400191505060405180910390fd5b60035460405163d9c4c15360e01b8152602060048201908152602482018690526001600160a01b039092169163d9c4c15391879187918190604401848480828437600083820152604051601f909101601f191690920195506020945090925050508083038186803b15801561074457600080fd5b505afa158015610758573d6000803e3d6000fd5b505050506040513d602081101561076e57600080fd5b5051600680546001600160a01b03199081166001600160a01b0393841617918290556005805492909316911681179091556107da5760405162461bcd60e51b815260040180806020018281038252602d815260200180610b75602d913960400191505060405180910390fd5b50505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b602081101561086d576008810260020a86026001600160f81b0319811615610864578084848151811061084657fe5b60200101906001600160f81b031916908160001a9053506001909201915b50600101610817565b5060008185118061087c575084155b1561088857508061088f565b5060001984015b6060816040519080825280601f01601f1916602001820160405280156108bc576020820181803883390190505b50905060005b8281101561090c578481815181106108d657fe5b602001015160f81c60f81b8282815181106108ed57fe5b60200101906001600160f81b031916908160001a9053506001016108c2565b509695505050505050565b6000546001600160a01b0316331490565b60025481565b50600090565b6005546001600160a01b031681565b61094b610917565b61099c576040805162461bcd60e51b815260206004820181905260248201527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604482015290519081900360640190fd5b6001600160a01b03811660009081526001602052604090205460ff16156109f6576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a261059c565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b80516000908290610a62575060009050610a6a565b505060208101515b919050565b610a77610917565b610ac8576040805162461bcd60e51b815260206004820181905260248201527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604482015290519081900360640190fd5b61059c81610ae0565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b039290921691909117905556fe436f756c64206e6f742066696e6420427573696e657373506172746e657253746f72616765206164647265737320696e207265676973747279436f756c64206e6f742066696e642050757263686173696e67206164647265737320696e207265676973747279a264697066735822122086b474c286ca45d40965768c51a79de0b82c0ef2b557575d415e619708949e1b64736f6c63430006010033";
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

    public partial class GetBalanceOfThisFunction : GetBalanceOfThisFunctionBase { }

    [Function("getBalanceOfThis", "uint256")]
    public class GetBalanceOfThisFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenAddress", 1)]
        public virtual string TokenAddress { get; set; }
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

    public partial class TransferInFundsForPoFromBuyerFunction : TransferInFundsForPoFromBuyerFunctionBase { }

    [Function("transferInFundsForPoFromBuyer")]
    public class TransferInFundsForPoFromBuyerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
    }

    public partial class TransferOutFundsForPoItemToBuyerFunction : TransferOutFundsForPoItemToBuyerFunctionBase { }

    [Function("transferOutFundsForPoItemToBuyer")]
    public class TransferOutFundsForPoItemToBuyerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class TransferOutFundsForPoItemToSellerFunction : TransferOutFundsForPoItemToSellerFunctionBase { }

    [Function("transferOutFundsForPoItemToSeller")]
    public class TransferOutFundsForPoItemToSellerFunctionBase : FunctionMessage
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



    public partial class GetBalanceOfThisOutputDTO : GetBalanceOfThisOutputDTOBase { }

    [FunctionOutput]
    public class GetBalanceOfThisOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "balance", 1)]
        public virtual BigInteger Balance { get; set; }
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
