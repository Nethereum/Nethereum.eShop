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
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516109db3803806109db83398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b610901806100da6000396000f3fe608060405234801561001057600080fd5b50600436106100cf5760003560e01c80638f32d59b1161008c578063c76ea97811610066578063c76ea9781461017d578063c8d303f814610190578063f2fde38b146101b0578063f3ad65f4146101c3576100cf565b80638f32d59b14610160578063abefab8714610168578063c076cfbf146100e9576100cf565b8063150e99f9146100d45780634f0dfe5b146100e95780635b9b0e26146100fc5780636b00e9d81461010f578063802706cb146101385780638da5cb5b1461014b575b600080fd5b6100e76100e2366004610512565b6101cb565b005b6100e76100f7366004610603565b6102a6565b6100e761010a3660046105a9565b6102a3565b61012261011d366004610512565b6102aa565b60405161012f91906107b9565b60405180910390f35b6100e7610146366004610540565b6102bf565b6101536102c5565b60405161012f91906107a5565b6101226102d4565b6101706102e5565b60405161012f91906107c4565b6100e761018b366004610512565b6102eb565b6101a361019e3660046105eb565b6103c0565b60405161012f9190610802565b6100e76101be366004610512565b6103cd565b6101536103fa565b6101d36102d4565b6101f85760405162461bcd60e51b81526004016101ef906107cd565b60405180910390fd5b6001600160a01b03811660009081526001602052604090205460ff161561026e576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26102a3565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b5050565b60016020526000908152604090205460ff1681565b50505050565b6000546001600160a01b031690565b6000546001600160a01b0316331490565b60025481565b6102f36102d4565b61030f5760405162461bcd60e51b81526004016101ef906107cd565b6001600160a01b03811660009081526001602052604090205460ff1615610369576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26102a3565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b6103c8610464565b919050565b6103d56102d4565b6103f15760405162461bcd60e51b81526004016101ef906107cd565b6102a381610409565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b6040805161018081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c081018290529060e082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b60008083601f8401126104dc578182fd5b50813567ffffffffffffffff8111156104f3578182fd5b60208301915083602082850101111561050b57600080fd5b9250929050565b600060208284031215610523578081fd5b81356001600160a01b0381168114610539578182fd5b9392505050565b60008060008060408587031215610555578283fd5b843567ffffffffffffffff8082111561056c578485fd5b610578888389016104cb565b90965094506020870135915080821115610590578384fd5b5061059d878288016104cb565b95989497509550505050565b6000602082840312156105ba578081fd5b813567ffffffffffffffff8111156105d0578182fd5b80830161018081860312156105e3578283fd5b949350505050565b6000602082840312156105fc578081fd5b5035919050565b60008060408385031215610615578182fd5b82359150602083013560ff8116811461062c578182fd5b809150509250929050565b6001600160a01b03169052565b6000815180845260208401935060208301825b8281101561076c578151805187526020810151610677602089018261079e565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e0880152610100808201516106c7828a0182610637565b505061012081810151908801526101408082015190880152610160808201516106f2828a0182610637565b505061018080820151610707828a018261078a565b50506101a081810151908801526101c080820151908801526101e080820151908801526102008082015161073d828a0182610776565b505061022080820151610752828a018261077c565b505050610240959095019460209190910190600101610657565b5093949350505050565b15159052565b6004811061078657fe5b9052565b6009811061078657fe5b6003811061078657fe5b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b6000602082528251602083015260208301516108216040840182610637565b5060408301516108346060840182610637565b5060608301516108476080840182610637565b50608083015160a083015260a083015160c083015260c083015161086e60e0840182610637565b5060e083015161010061088381850183610794565b840151610120848101919091528401516101408085019190915284015190506101606108b18185018361079e565b8401516101808481015290506105e36101a084018261064456fea26469706673582212207b3f0ccc2a8e73111003df31592d429c497f990c9eb003937aa8edbb6147837364736f6c63430006010033";
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






}
