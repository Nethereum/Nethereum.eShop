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

namespace Nethereum.Commerce.Contracts.Owned.ContractDefinition
{


    public partial class OwnedDeployment : OwnedDeploymentBase
    {
        public OwnedDeployment() : base(BYTECODE) { }
        public OwnedDeployment(string byteCode) : base(byteCode) { }
    }

    public class OwnedDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50600080546001600160a01b031916331790556101b5806100326000396000f3fe608060405234801561001057600080fd5b506004361061004c5760003560e01c806379ba5097146100515780638da5cb5b1461005b578063d4ee1d901461007f578063f2fde38b14610087575b600080fd5b6100596100ad565b005b610063610128565b604080516001600160a01b039092168252519081900360200190f35b610063610137565b6100596004803603602081101561009d57600080fd5b50356001600160a01b0316610146565b6001546001600160a01b031633146100c457600080fd5b600154600080546040516001600160a01b0393841693909116917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a360018054600080546001600160a01b03199081166001600160a01b03841617909155169055565b6000546001600160a01b031681565b6001546001600160a01b031681565b6000546001600160a01b0316331461015d57600080fd5b600180546001600160a01b0319166001600160a01b039290921691909117905556fea264697066735822122058920668bcf9be448b4be71f72ba0ff3e21f9061b13674146cde8af272a742ac64736f6c63430006010033";
        public OwnedDeploymentBase() : base(BYTECODE) { }
        public OwnedDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AcceptOwnershipFunction : AcceptOwnershipFunctionBase { }

    [Function("acceptOwnership")]
    public class AcceptOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class NewOwnerFunction : NewOwnerFunctionBase { }

    [Function("newOwner", "address")]
    public class NewOwnerFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "_newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "_from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "_to", 2, true )]
        public virtual string To { get; set; }
    }



    public partial class NewOwnerOutputDTO : NewOwnerOutputDTOBase { }

    [FunctionOutput]
    public class NewOwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }


}
