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

namespace Nethereum.Commerce.Contracts.SimpleStorage.ContractDefinition
{


    public partial class SimpleStorageDeployment : SimpleStorageDeploymentBase
    {
        public SimpleStorageDeployment() : base(BYTECODE) { }
        public SimpleStorageDeployment(string byteCode) : base(byteCode) { }
    }

    public class SimpleStorageDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052348015600f57600080fd5b5060ab8061001e6000396000f3fe6080604052348015600f57600080fd5b506004361060325760003560e01c806360fe47b11460375780636d4ce63c146053575b600080fd5b605160048036036020811015604b57600080fd5b5035606b565b005b60596070565b60408051918252519081900360200190f35b600055565b6000549056fea265627a7a72305820eeea4d134950e604e06261975ec182d643a468ed67201d6ce1a412ea5d12c5b064736f6c634300050a0032";
        public SimpleStorageDeploymentBase() : base(BYTECODE) { }
        public SimpleStorageDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class SetFunction : SetFunctionBase { }

    [Function("set")]
    public class SetFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "x", 1)]
        public virtual BigInteger X { get; set; }
    }

    public partial class GetFunction : GetFunctionBase { }

    [Function("get", "uint256")]
    public class GetFunctionBase : FunctionMessage
    {

    }



    public partial class GetOutputDTO : GetOutputDTOBase { }

    [FunctionOutput]
    public class GetOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
