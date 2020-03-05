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

namespace Nethereum.Commerce.Contracts.MockDai.ContractDefinition
{


    public partial class MockDaiDeployment : MockDaiDeploymentBase
    {
        public MockDaiDeployment() : base(BYTECODE) { }
        public MockDaiDeployment(string byteCode) : base(byteCode) { }
    }

    public class MockDaiDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50600080546001600160a01b031916331790556040805180820190915260038082526244414960e81b602090920191825261004d91600291610103565b5060408051808201909152600f8082526e44414920666f722074657374696e6760881b602090920191825261008491600391610103565b5060048054601260ff19909116179081905560ff16600a0a6302faf080026005819055600080546001600160a01b0390811682526006602090815260408084208590558354815195865290519216937fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef929081900390910190a361019e565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061014457805160ff1916838001178555610171565b82800160010185558215610171579182015b82811115610171578251825591602001919060010190610156565b5061017d929150610181565b5090565b61019b91905b8082111561017d5760008155600101610187565b90565b610a09806101ad6000396000f3fe6080604052600436106100e15760003560e01c80638da5cb5b1161007f578063d4ee1d9011610059578063d4ee1d901461039f578063dc39d06d146103b4578063dd62ed3e146103ed578063f2fde38b1461042857610128565b80638da5cb5b1461032057806395d89b4114610351578063a9059cbb1461036657610128565b806323b872dd116100bb57806323b872dd14610268578063313ce567146102ab57806370a08231146102d657806379ba50971461030957610128565b806306fdde031461016a578063095ea7b3146101f457806318160ddd1461024157610128565b36610128576040805162461bcd60e51b8152602060048201526012602482015271086c2dcdcdee840e4cac6cad2ecca408aa8960731b604482015290519081900360640190fd5b6040805162461bcd60e51b8152602060048201526012602482015271086c2dcdcdee840e4cac6cad2ecca408aa8960731b604482015290519081900360640190fd5b34801561017657600080fd5b5061017f61045b565b6040805160208082528351818301528351919283929083019185019080838360005b838110156101b95781810151838201526020016101a1565b50505050905090810190601f1680156101e65780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34801561020057600080fd5b5061022d6004803603604081101561021757600080fd5b506001600160a01b0381351690602001356104e9565b604080519115158252519081900360200190f35b34801561024d57600080fd5b5061025661054f565b60408051918252519081900360200190f35b34801561027457600080fd5b5061022d6004803603606081101561028b57600080fd5b506001600160a01b03813581169160208101359091169060400135610581565b3480156102b757600080fd5b506102c06106db565b6040805160ff9092168252519081900360200190f35b3480156102e257600080fd5b50610256600480360360208110156102f957600080fd5b50356001600160a01b03166106e4565b34801561031557600080fd5b5061031e6106ff565b005b34801561032c57600080fd5b5061033561077a565b604080516001600160a01b039092168252519081900360200190f35b34801561035d57600080fd5b5061017f610789565b34801561037257600080fd5b5061022d6004803603604081101561038957600080fd5b506001600160a01b0381351690602001356107e1565b3480156103ab57600080fd5b5061033561089d565b3480156103c057600080fd5b5061022d600480360360408110156103d757600080fd5b506001600160a01b0381351690602001356108ac565b3480156103f957600080fd5b506102566004803603604081101561041057600080fd5b506001600160a01b038135811691602001351661094e565b34801561043457600080fd5b5061031e6004803603602081101561044b57600080fd5b50356001600160a01b0316610979565b6003805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156104e15780601f106104b6576101008083540402835291602001916104e1565b820191906000526020600020905b8154815290600101906020018083116104c457829003601f168201915b505050505081565b3360008181526007602090815260408083206001600160a01b038716808552908352818420869055815186815291519394909390927f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925928290030190a350600192915050565b6000805260066020527f54cdd369e4e8a8515e52ca72ec816c2101831ad1f18bf44102ed171459c9b4f8546005540390565b6001600160a01b0383166000908152600660205260408120548211156105d85760405162461bcd60e51b81526004018080602001828103825260218152602001806109b36021913960400191505060405180910390fd5b6001600160a01b0384166000908152600660209081526040808320805486900390556007825280832033845290915290205482111561065e576040805162461bcd60e51b815260206004820181905260248201527f455243323020746f6b656e20616c6c6f77656420636865636b206661696c6564604482015290519081900360640190fd5b6001600160a01b03808516600081815260076020908152604080832033845282528083208054889003905593871680835260068252918490208054870190558351868152935191937fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef929081900390910190a35060019392505050565b60045460ff1681565b6001600160a01b031660009081526006602052604090205490565b6001546001600160a01b0316331461071657600080fd5b600154600080546040516001600160a01b0393841693909116917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a360018054600080546001600160a01b03199081166001600160a01b03841617909155169055565b6000546001600160a01b031681565b6002805460408051602060018416156101000260001901909316849004601f810184900484028201840190925281815292918301828280156104e15780601f106104b6576101008083540402835291602001916104e1565b3360009081526006602052604081205482111561082f5760405162461bcd60e51b81526004018080602001828103825260218152602001806109b36021913960400191505060405180910390fd5b336000818152600660209081526040808320805487900390556001600160a01b03871680845292819020805487019055805186815290519293927fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef929181900390910190a350600192915050565b6001546001600160a01b031681565b600080546001600160a01b031633146108c457600080fd5b600080546040805163a9059cbb60e01b81526001600160a01b0392831660048201526024810186905290519186169263a9059cbb926044808401936020939083900390910190829087803b15801561091b57600080fd5b505af115801561092f573d6000803e3d6000fd5b505050506040513d602081101561094557600080fd5b50519392505050565b6001600160a01b03918216600090815260076020908152604080832093909416825291909152205490565b6000546001600160a01b0316331461099057600080fd5b600180546001600160a01b0319166001600160a01b039290921691909117905556fe455243323020746f6b656e2062616c616e63657320636865636b206661696c6564a26469706673582212206fc6c4a143404feabb00fa859398e82708b6d8c1b84ffce539b2354a74c2356a64736f6c63430006010033";
        public MockDaiDeploymentBase() : base(BYTECODE) { }
        public MockDaiDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AcceptOwnershipFunction : AcceptOwnershipFunctionBase { }

    [Function("acceptOwnership")]
    public class AcceptOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class AllowanceFunction : AllowanceFunctionBase { }

    [Function("allowance", "uint256")]
    public class AllowanceFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenOwner", 1)]
        public virtual string TokenOwner { get; set; }
        [Parameter("address", "spender", 2)]
        public virtual string Spender { get; set; }
    }

    public partial class ApproveFunction : ApproveFunctionBase { }

    [Function("approve", "bool")]
    public class ApproveFunctionBase : FunctionMessage
    {
        [Parameter("address", "spender", 1)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "tokens", 2)]
        public virtual BigInteger Tokens { get; set; }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenOwner", 1)]
        public virtual string TokenOwner { get; set; }
    }

    public partial class DecimalsFunction : DecimalsFunctionBase { }

    [Function("decimals", "uint8")]
    public class DecimalsFunctionBase : FunctionMessage
    {

    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
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

    public partial class SymbolFunction : SymbolFunctionBase { }

    [Function("symbol", "string")]
    public class SymbolFunctionBase : FunctionMessage
    {

    }

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class TransferFunction : TransferFunctionBase { }

    [Function("transfer", "bool")]
    public class TransferFunctionBase : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokens", 2)]
        public virtual BigInteger Tokens { get; set; }
    }

    public partial class TransferAnyERC20TokenFunction : TransferAnyERC20TokenFunctionBase { }

    [Function("transferAnyERC20Token", "bool")]
    public class TransferAnyERC20TokenFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenAddress", 1)]
        public virtual string TokenAddress { get; set; }
        [Parameter("uint256", "tokens", 2)]
        public virtual BigInteger Tokens { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom", "bool")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokens", 3)]
        public virtual BigInteger Tokens { get; set; }
    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "_newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class ApprovalEventDTO : ApprovalEventDTOBase { }

    [Event("Approval")]
    public class ApprovalEventDTOBase : IEventDTO
    {
        [Parameter("address", "tokenOwner", 1, true )]
        public virtual string TokenOwner { get; set; }
        [Parameter("address", "spender", 2, true )]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "tokens", 3, false )]
        public virtual BigInteger Tokens { get; set; }
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

    public partial class TransferEventDTO : TransferEventDTOBase { }

    [Event("Transfer")]
    public class TransferEventDTOBase : IEventDTO
    {
        [Parameter("address", "from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2, true )]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokens", 3, false )]
        public virtual BigInteger Tokens { get; set; }
    }



    public partial class AllowanceOutputDTO : AllowanceOutputDTOBase { }

    [FunctionOutput]
    public class AllowanceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "remaining", 1)]
        public virtual BigInteger Remaining { get; set; }
    }



    public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

    [FunctionOutput]
    public class BalanceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "balance", 1)]
        public virtual BigInteger Balance { get; set; }
    }

    public partial class DecimalsOutputDTO : DecimalsOutputDTOBase { }

    [FunctionOutput]
    public class DecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
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

    public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

    [FunctionOutput]
    public class SymbolOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }








}
