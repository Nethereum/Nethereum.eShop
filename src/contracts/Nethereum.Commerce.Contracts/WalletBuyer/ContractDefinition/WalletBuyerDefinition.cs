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
        public static string BYTECODE = "60806040819052600080546001600160a01b03191633178082556001600160a01b0316917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a36105dc806100576000396000f3fe608060405234801561001057600080fd5b50600436106100a95760003560e01c80638da5cb5b116100715780638da5cb5b146101255780638f32d59b1461013a578063abefab8714610142578063c076cfbf146100c3578063c76ea97814610157578063f2fde38b1461016a576100a9565b8063150e99f9146100ae5780634f0dfe5b146100c35780635b9b0e26146100d65780636b00e9d8146100e9578063802706cb14610112575b600080fd5b6100c16100bc36600461043c565b61017d565b005b6100c16100d1366004610515565b610258565b6100c16100e43660046104d3565b610255565b6100fc6100f736600461043c565b61025c565b604051610109919061055d565b60405180910390f35b6100c161012036600461046a565b610271565b61012d610277565b6040516101099190610549565b6100fc610286565b61014a610297565b6040516101099190610568565b6100c161016536600461043c565b61029d565b6100c161017836600461043c565b610372565b610185610286565b6101aa5760405162461bcd60e51b81526004016101a190610571565b60405180910390fd5b6001600160a01b03811660009081526001602052604090205460ff1615610220576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a2610255565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b5050565b60016020526000908152604090205460ff1681565b50505050565b6000546001600160a01b031690565b6000546001600160a01b0316331490565b60025481565b6102a5610286565b6102c15760405162461bcd60e51b81526004016101a190610571565b6001600160a01b03811660009081526001602052604090205460ff161561031b576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a2610255565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b61037a610286565b6103965760405162461bcd60e51b81526004016101a190610571565b61025581600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b60008083601f840112610406578182fd5b50813567ffffffffffffffff81111561041d578182fd5b60208301915083602082850101111561043557600080fd5b9250929050565b60006020828403121561044d578081fd5b81356001600160a01b0381168114610463578182fd5b9392505050565b6000806000806040858703121561047f578283fd5b843567ffffffffffffffff80821115610496578485fd5b6104a2888389016103f5565b909650945060208701359150808211156104ba578384fd5b506104c7878288016103f5565b95989497509550505050565b6000602082840312156104e4578081fd5b813567ffffffffffffffff8111156104fa578182fd5b808301610180818603121561050d578283fd5b949350505050565b60008060408385031215610527578182fd5b82359150602083013560ff8116811461053e578182fd5b809150509250929050565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e657260408201526060019056fea2646970667358221220320b032be1c0cddb89c77335b415c0097012c7bc6a383a73e0a54a4fce6f9f3264736f6c63430006010033";
        public WalletBuyerDeploymentBase() : base(BYTECODE) { }
        public WalletBuyerDeploymentBase(string byteCode) : base(byteCode) { }

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
