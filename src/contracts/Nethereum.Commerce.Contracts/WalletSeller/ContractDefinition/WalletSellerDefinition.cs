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
        public static string BYTECODE = "608060405234801561001057600080fd5b50604051610d42380380610d4283398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b610c68806100da6000396000f3fe608060405234801561001057600080fd5b50600436106101165760003560e01c80638da5cb5b116100a2578063c76ea97811610071578063c76ea978146101ec578063c8d303f8146101ff578063cfb519281461021f578063f2fde38b14610232578063f3ad65f41461024557610116565b80638da5cb5b146101a75780638e5fc30b146101bc5780638f32d59b146101dc578063abefab87146101e457610116565b806346885b5b116100e957806346885b5b146101615780634ac5042f146101615780634f0dfe5b146101615780636b00e9d8146101745780636cc2c1111461019457610116565b806306ac2d3d1461011b578063150e99f914610130578063261d7555146101435780633dafca6e14610161575b600080fd5b61012e610129366004610779565b61024d565b005b61012e61013e36600461072a565b610297565b61014b610372565b6040516101589190610ac4565b60405180910390f35b61012e61016f3660046108c2565b610378565b61018761018236600461072a565b61037c565b6040516101589190610ab9565b61012e6101a23660046108f6565b610391565b6101af610397565b6040516101589190610aa5565b6101cf6101ca366004610758565b6103a6565b6040516101589190610acd565b6101876104ce565b61014b6104df565b61012e6101fa36600461072a565b6104e5565b61021261020d3660046108aa565b6105ba565b6040516101589190610b55565b61014b61022d36600461080f565b6105c7565b61012e61024036600461072a565b6105e5565b6101af610612565b61028c86868080601f0160208091040260200160405190810160405280939291908181526020018383808284376000920191909152506105c792505050565b600455505050505050565b61029f6104ce565b6102c45760405162461bcd60e51b81526004016102bb90610b20565b60405180910390fd5b6001600160a01b03811660009081526001602052604090205460ff161561033a576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a261036f565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b60045481565b5050565b60016020526000908152604090205460ff1681565b50505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b6020811015610424576008810260020a86026001600160f81b031981161561041b57808484815181106103fd57fe5b60200101906001600160f81b031916908160001a9053506001909201915b506001016103ce565b50600081851180610433575084155b1561043f575080610446565b5060001984015b6060816040519080825280601f01601f191660200182016040528015610473576020820181803883390190505b50905060005b828110156104c35784818151811061048d57fe5b602001015160f81c60f81b8282815181106104a457fe5b60200101906001600160f81b031916908160001a905350600101610479565b509695505050505050565b6000546001600160a01b0316331490565b60025481565b6104ed6104ce565b6105095760405162461bcd60e51b81526004016102bb90610b20565b6001600160a01b03811660009081526001602052604090205460ff1615610563576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a261036f565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b6105c261067c565b919050565b805160009082906105dc5750600090506105c2565b50506020015190565b6105ed6104ce565b6106095760405162461bcd60e51b81526004016102bb90610b20565b61036f81610621565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b6040805161018081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c081018290529060e082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b60008083601f8401126106f4578182fd5b50813567ffffffffffffffff81111561070b578182fd5b60208301915083602082850101111561072357600080fd5b9250929050565b60006020828403121561073b578081fd5b81356001600160a01b0381168114610751578182fd5b9392505050565b6000806040838503121561076a578081fd5b50508035926020909101359150565b60008060008060008060608789031215610791578182fd5b863567ffffffffffffffff808211156107a8578384fd5b6107b48a838b016106e3565b909850965060208901359150808211156107cc578384fd5b6107d88a838b016106e3565b909650945060408901359150808211156107f0578384fd5b506107fd89828a016106e3565b979a9699509497509295939492505050565b600060208284031215610820578081fd5b813567ffffffffffffffff80821115610837578283fd5b81840185601f820112610848578384fd5b8035925081831115610858578384fd5b604051601f8401601f191681016020018381118282101715610878578586fd5b60405283815281840160200187101561088f578485fd5b6108a0846020830160208501610c26565b9695505050505050565b6000602082840312156108bb578081fd5b5035919050565b600080604083850312156108d4578182fd5b82359150602083013560ff811681146108eb578182fd5b809150509250929050565b6000806000806080858703121561090b578384fd5b84359350602085013560ff81168114610922578384fd5b93969395505050506040820135916060013590565b6001600160a01b03169052565b6000815180845260208401935060208301825b82811015610a6c5781518051875260208101516109776020890182610a9e565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e0880152610100808201516109c7828a0182610937565b505061012081810151908801526101408082015190880152610160808201516109f2828a0182610937565b505061018080820151610a07828a0182610a8a565b50506101a081810151908801526101c080820151908801526101e0808201519088015261020080820151610a3d828a0182610a76565b505061022080820151610a52828a0182610a7c565b505050610240959095019460209190910190600101610957565b5093949350505050565b15159052565b60048110610a8657fe5b9052565b60098110610a8657fe5b60038110610a8657fe5b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b6000602082528251806020840152815b81811015610afa5760208186018101516040868401015201610add565b81811115610b0b5782604083860101525b50601f01601f19169190910160400192915050565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b600060208252825160208301526020830151610b746040840182610937565b506040830151610b876060840182610937565b506060830151610b9a6080840182610937565b50608083015160a083015260a083015160c083015260c0830151610bc160e0840182610937565b5060e0830151610100610bd681850183610a94565b84015161012084810191909152840151610140808501919091528401519050610160610c0481850183610a9e565b840151610180848101529050610c1e6101a0840182610944565b949350505050565b8281833750600091015256fea2646970667358221220172bad92fa7785efdef2128c6383bf13fc4ceaa088f8f8e031cd76f4026f243264736f6c63430006010033";
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

    public partial class GetPoFunction : GetPoFunctionBase { }

    [Function("getPo", typeof(GetPoOutputDTO))]
    public class GetPoFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
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



    public partial class GetPoOutputDTO : GetPoOutputDTOBase { }

    [FunctionOutput]
    public class GetPoOutputDTOBase : IFunctionOutputDTO 
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
