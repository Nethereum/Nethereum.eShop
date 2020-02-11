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

namespace Nethereum.Commerce.Contracts.Purchasing.ContractDefinition
{


    public partial class PurchasingDeployment : PurchasingDeploymentBase
    {
        public PurchasingDeployment() : base(BYTECODE) { }
        public PurchasingDeployment(string byteCode) : base(byteCode) { }
    }

    public class PurchasingDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50604051611f5b380380611f5b83398101604081905261002f9161009d565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cb565b6000602082840312156100ae578081fd5b81516001600160a01b03811681146100c4578182fd5b9392505050565b611e81806100da6000396000f3fe608060405234801561001057600080fd5b50600436106101585760003560e01c80638f32d59b116100c3578063c8d303f81161007c578063c8d303f81461027c578063cb4c86b71461028f578063cd882a0014610172578063cfb5192814610297578063f2fde38b146102aa578063f3ad65f4146102bd57610158565b80638f32d59b1461023157806391aa0f3014610239578063abefab871461024c578063b4bf807f14610261578063c076cfbf14610172578063c76ea9781461026957610158565b80634ac5042f116101155780634ac5042f146101725780636b00e9d8146101b65780636cc2c111146101d65780636fee6fec146101e95780638da5cb5b146102095780638e5fc30b1461021157610158565b806306ac2d3d1461015d5780630d9192ef14610172578063150e99f9146101855780633dafca6e1461017257806346885b5b146101725780634ab2930a14610198575b600080fd5b61017061016b366004611450565b6102c5565b005b6101706101803660046118b2565b61050d565b6101706101933660046113f0565b610511565b6101a06105e3565b6040516101ad9190611a76565b60405180910390f35b6101c96101c43660046113f0565b6105f2565b6040516101ad9190611a8a565b6101706101e43660046118e1565b610607565b6101fc6101f73660046114e6565b61060d565b6040516101ad9190611cb7565b6101a061076b565b61022461021f36600461142f565b61077a565b6040516101ad9190611adb565b6101c96108a4565b6101706102473660046115ba565b6108b5565b610254610d9a565b6040516101ad9190611a95565b6101a0610da0565b6101706102773660046113f0565b610daf565b6101fc61028a366004611882565b610e84565b6101a0610f17565b6102546102a5366004611530565b610f26565b6101706102b83660046113f0565b610f44565b6101a0610f71565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906102f79089908990600401611aac565b60206040518083038186803b15801561030f57600080fd5b505afa158015610323573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103479190810190611413565b600480546001600160a01b0319166001600160a01b0392831617908190551661038b5760405162461bcd60e51b815260040161038290611b81565b60405180910390fd5b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906103bd9087908790600401611aac565b60206040518083038186803b1580156103d557600080fd5b505afa1580156103e9573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061040d9190810190611413565b600580546001600160a01b0319166001600160a01b039283161790819055166104485760405162461bcd60e51b815260040161038290611bd7565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061047a9085908590600401611aac565b60206040518083038186803b15801561049257600080fd5b505afa1580156104a6573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104ca9190810190611413565b600680546001600160a01b0319166001600160a01b039283161790819055166105055760405162461bcd60e51b815260040161038290611b2e565b505050505050565b5050565b6105196108a4565b6105355760405162461bcd60e51b815260040161038290611c41565b6001600160a01b03811660009081526001602052604090205460ff16156105ab576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26105e0565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b6004546001600160a01b031681565b60016020526000908152604090205460ff1681565b50505050565b610615610fdb565b600061065685858080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250610f2692505050565b60048054604051631edee79b60e21b81529293506000926001600160a01b0390911691637b7b9e6c9161068d918691899101611a9e565b60206040518083038186803b1580156106a557600080fd5b505afa1580156106b9573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506106dd919081019061189a565b6004805460405163191a607f60e31b81529293506001600160a01b03169163c8d303f89161070d91859101611a95565b60006040518083038186803b15801561072557600080fd5b505afa158015610739573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f1916820160405261076191908101906116f2565b9695505050505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b60208110156107f8576008810260020a86026001600160f81b03198116156107ef57808484815181106107d157fe5b60200101906001600160f81b031916908160001a9053506001909201915b506001016107a2565b50600081851180610807575084155b1561081357508061081a565b5060001984015b6060816040519080825280601f01601f191660200182016040528015610847576020820181803883390190505b50905060005b828110156108975784818151811061086157fe5b602001015160f81c60f81b82828151811061087857fe5b60200101906001600160f81b031916908160001a90535060010161084d565b5093505050505b92915050565b6000546001600160a01b0316331490565b600081610140015182602001516001600160a01b03167f3abd43c7a604af572fc5cf0446d9fbf8c3df782be6ef917226df288f58a5db39846040516108fa9190611cb7565b60405180910390a461090a611052565b600554610140830151604051631f91394160e21b81526001600160a01b0390921691637e44e5049161093e91600401611a95565b60a06040518083038186803b15801561095657600080fd5b505afa15801561096a573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061098e9190810190611819565b60608101519091506001600160a01b03166109bb5760405162461bcd60e51b815260040161038290611c76565b600480546040805163e82f815f60e01b815290516001600160a01b039092169263e82f815f92828201926000929082900301818387803b1580156109fe57600080fd5b505af1158015610a12573d6000803e3d6000fd5b50506004805460408051632d77100760e11b815290516001600160a01b039092169450635aee200e93508083019260209291829003018186803b158015610a5857600080fd5b505afa158015610a6c573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610a90919081019061189a565b825260608101516001600160a01b03166101008301526101a08201515160ff811661018084015260005b83610180015160ff16811015610c545783516101a0850151805183908110610ade57fe5b6020026020010151600001818152505080600101846101a001518281518110610b0357fe5b60200260200101516020019060ff16908160ff16815250506001846101a001518281518110610b2e57fe5b602002602001015161014001906008811115610b4657fe5b90816008811115610b5357fe5b815250506000846101a001518281518110610b6a57fe5b60200260200101516101600181815250506000846101a001518281518110610b8e57fe5b60200260200101516101800181815250506000846101a001518281518110610bb257fe5b60200260200101516101a00181815250506000846101a001518281518110610bd657fe5b60200260200101516101c00181815250506000846101a001518281518110610bfa57fe5b60200260200101516101e00190151590811515815250506000846101a001518281518110610c2457fe5b602002602001015161020001906003811115610c3c57fe5b90816003811115610c4957fe5b905250600101610aba565b5060048054604051636ed4e2d360e11b81526001600160a01b039091169163dda9c5a691610c8491879101611cb7565b600060405180830381600087803b158015610c9e57600080fd5b505af1158015610cb2573d6000803e3d6000fd5b50505050610cbe610fdb565b60048054855160405163191a607f60e31b81526001600160a01b039092169263c8d303f892610cee929101611a95565b60006040518083038186803b158015610d0657600080fd5b505afa158015610d1a573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f19168201604052610d4291908101906116f2565b9050806000015181610140015182602001516001600160a01b03167f1bedbc3174124c2e9950123708cb0e8a0b79513f17c4e109361efd0acb1ed97884604051610d8c9190611cb7565b60405180910390a450505050565b60025481565b6005546001600160a01b031681565b610db76108a4565b610dd35760405162461bcd60e51b815260040161038290611c41565b6001600160a01b03811660009081526001602052604090205460ff1615610e2d576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26105e0565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b610e8c610fdb565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f891610ebb91869101611a95565b60006040518083038186803b158015610ed357600080fd5b505afa158015610ee7573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f19168201604052610f0f91908101906116f2565b90505b919050565b6006546001600160a01b031681565b80516000908290610f3b575060009050610f12565b50506020015190565b610f4c6108a4565b610f685760405162461bcd60e51b815260040161038290611c41565b6105e081610f80565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b6040805160a08101825260008082526020820181905291810182905260608101829052608081019190915290565b803561089e81611df2565b805161089e81611df2565b600082601f8301126110a6578081fd5b81356110b96110b482611dd2565b611dab565b8181529150602080830190848101610220808502870183018810156110dd57600080fd5b60005b858110156111e55781838a0312156110f757600080fd5b61110082611dab565b833581526111108a8686016113da565b8582015260408401356040820152606084013560608201526080840135608082015260a084013560a082015260c084013560c082015260e084013560e082015261010061115f8b828701611080565b90820152610120848101359082015261014061117d8b828701611367565b90820152610160848101359082015261018080850135908201526101a080850135908201526101c080850135908201526101e06111bc8b82870161133b565b908201526102006111cf8b868301611351565b90820152855293830193918101916001016110e0565b50505050505092915050565b600082601f830112611201578081fd5b815161120f6110b482611dd2565b81815291506020808301908481016102208085028701830188101561123357600080fd5b60005b858110156111e55781838a03121561124d57600080fd5b61125682611dab565b835181526112668a8686016113e5565b8582015260408401516040820152606084015160608201526080840151608082015260a084015160a082015260c084015160c082015260e084015160e08201526101006112b58b82870161108b565b9082015261012084810151908201526101406112d38b828701611372565b90820152610160848101519082015261018080850151908201526101a080850151908201526101c080850151908201526101e06113128b828701611346565b908201526102006113258b86830161135c565b9082015285529383019391810191600101611236565b803561089e81611e07565b805161089e81611e07565b803561089e81611e15565b805161089e81611e15565b803561089e81611e22565b805161089e81611e22565b803561089e81611e2f565b805161089e81611e2f565b60008083601f8401126113a4578182fd5b50813567ffffffffffffffff8111156113bb578182fd5b6020830191508360208285010111156113d357600080fd5b9250929050565b803561089e81611e3c565b805161089e81611e3c565b600060208284031215611401578081fd5b813561140c81611df2565b9392505050565b600060208284031215611424578081fd5b815161140c81611df2565b60008060408385031215611441578081fd5b50508035926020909101359150565b60008060008060008060608789031215611468578182fd5b863567ffffffffffffffff8082111561147f578384fd5b61148b8a838b01611393565b909850965060208901359150808211156114a3578384fd5b6114af8a838b01611393565b909650945060408901359150808211156114c7578384fd5b506114d489828a01611393565b979a9699509497509295939492505050565b6000806000604084860312156114fa578081fd5b833567ffffffffffffffff811115611510578182fd5b61151c86828701611393565b909790965060209590950135949350505050565b60006020808385031215611542578182fd5b823567ffffffffffffffff80821115611559578384fd5b81850186601f82011261156a578485fd5b803592508183111561157a578485fd5b61158c601f8401601f19168501611dab565b915082825286848483010111156115a1578485fd5b8284820185840137509081019091019190915292915050565b6000602082840312156115cb578081fd5b813567ffffffffffffffff808211156115e2578283fd5b6101c09184018086038313156115f6578384fd5b6115ff83611dab565b813581526116108760208401611080565b60208201526116228760408401611080565b60408201526116348760608401611080565b6060820152608082013560808201526116508760a08401611080565b60a082015260c082013560c082015260e082013560e0820152610100935061167a87858401611080565b84820152610120935061168f8785840161137d565b9381019390935261014081810135908401526101608082013590840152610180926116bc878584016113da565b848201526101a0935083820135838111156116d5578586fd5b6116e188828501611096565b948201949094529695505050505050565b600060208284031215611703578081fd5b815167ffffffffffffffff8082111561171a578283fd5b6101c091840180860383131561172e578384fd5b61173783611dab565b81518152611748876020840161108b565b602082015261175a876040840161108b565b604082015261176c876060840161108b565b6060820152608082015160808201526117888760a0840161108b565b60a082015260c082015160c082015260e082015160e082015261010093506117b28785840161108b565b8482015261012093506117c787858401611388565b9381019390935261014081810151908401526101608082015190840152610180926117f4878584016113e5565b848201526101a09350838201518381111561180d578586fd5b6116e1888285016111f1565b600060a0828403121561182a578081fd5b61183460a0611dab565b8251815260208301516020820152604083015161185081611df2565b6040820152606083015161186381611df2565b6060820152608083015161187681611e07565b60808201529392505050565b600060208284031215611893578081fd5b5035919050565b6000602082840312156118ab578081fd5b5051919050565b600080604083850312156118c4578182fd5b8235915060208301356118d681611e3c565b809150509250929050565b600080600080608085870312156118f6578182fd5b84359350602085013561190881611e3c565b93969395505050506040820135916060013590565b6001600160a01b03169052565b6000815180845260208401935060208301825b82811015611a3d57815180518752602081015161195d6020890182611a6f565b5060408101516040880152606081015160608801526080810151608088015260a081015160a088015260c081015160c088015260e081015160e0880152610100808201516119ad828a018261191d565b50506101208181015190880152610140808201516119cd828a0182611a5b565b5050610160818101519088015261018080820151908801526101a080820151908801526101c080820151908801526101e080820151611a0e828a0182611a47565b505061020080820151611a23828a0182611a4d565b50505061022095909501946020919091019060010161193d565b5093949350505050565b15159052565b60048110611a5757fe5b9052565b60098110611a5757fe5b60038110611a5757fe5b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b918252602082015260400190565b60006020825282602083015282846040840137818301604090810191909152601f909201601f19160101919050565b6000602082528251806020840152815b81811015611b085760208186018101516040868401015201611aeb565b81811115611b195782604083860101525b50601f01601f19169190910160400192915050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b60208082526044908201527f436f756c64206e6f742066696e6420427573696e65737320506172746e65722060408201527f53746f7261676520636f6e7472616374206164647265737320696e20726567696060820152637374727960e01b608082015260a00190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b60208082526021908201527f53656c6c657220496420686173206e6f20617070726f766572206164647265736040820152607360f81b606082015260800190565b600060208252825160208301526020830151611cd6604084018261191d565b506040830151611ce9606084018261191d565b506060830151611cfc608084018261191d565b50608083015160a083015260a0830151611d1960c084018261191d565b5060c083015160e083015260e0830151610100818185015280850151915050610120611d478185018361191d565b8401519050610140611d5b84820183611a65565b840151610160848101919091528401516101808085019190915284015190506101a0611d8981850183611a6f565b8401516101c0848101529050611da36101e084018261192a565b949350505050565b60405181810167ffffffffffffffff81118282101715611dca57600080fd5b604052919050565b600067ffffffffffffffff821115611de8578081fd5b5060209081020190565b6001600160a01b03811681146105e057600080fd5b80151581146105e057600080fd5b600481106105e057600080fd5b600981106105e057600080fd5b600381106105e057600080fd5b60ff811681146105e057600080fdfea2646970667358221220ad04eb12939a8b858690202ae37480008ba1e2aef8f0dabd411756d5ec5cc43764736f6c63430006010033";
        public PurchasingDeploymentBase() : base(BYTECODE) { }
        public PurchasingDeploymentBase(string byteCode) : base(byteCode) { }
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

    public partial class BpStorageFunction : BpStorageFunctionBase { }

    [Function("bpStorage", "address")]
    public class BpStorageFunctionBase : FunctionMessage
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
        [Parameter("string", "nameOfPoStorage", 1)]
        public virtual string NameOfPoStorage { get; set; }
        [Parameter("string", "nameOfBusinessPartnerStorage", 2)]
        public virtual string NameOfBusinessPartnerStorage { get; set; }
        [Parameter("string", "nameOfFunding", 3)]
        public virtual string NameOfFunding { get; set; }
    }

    public partial class CreatePurchaseOrderFunction : CreatePurchaseOrderFunctionBase { }

    [Function("createPurchaseOrder")]
    public class CreatePurchaseOrderFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class FundingFunction : FundingFunctionBase { }

    [Function("funding", "address")]
    public class FundingFunctionBase : FunctionMessage
    {

    }

    public partial class GetPoFunction : GetPoFunctionBase { }

    [Function("getPo", typeof(GetPoOutputDTO))]
    public class GetPoFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
    }

    public partial class GetPoBySellerAndQuoteFunction : GetPoBySellerAndQuoteFunctionBase { }

    [Function("getPoBySellerAndQuote", typeof(GetPoBySellerAndQuoteOutputDTO))]
    public class GetPoBySellerAndQuoteFunctionBase : FunctionMessage
    {
        [Parameter("string", "sellerIdString", 1)]
        public virtual string SellerIdString { get; set; }
        [Parameter("uint256", "quoteId", 2)]
        public virtual BigInteger QuoteId { get; set; }
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

    public partial class PoStorageFunction : PoStorageFunctionBase { }

    [Function("poStorage", "address")]
    public class PoStorageFunctionBase : FunctionMessage
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

    public partial class SetPoItemGoodsReceivedBuyerFunction : SetPoItemGoodsReceivedBuyerFunctionBase { }

    [Function("setPoItemGoodsReceivedBuyer")]
    public class SetPoItemGoodsReceivedBuyerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
    }

    public partial class SetPoItemGoodsReceivedSellerFunction : SetPoItemGoodsReceivedSellerFunctionBase { }

    [Function("setPoItemGoodsReceivedSeller")]
    public class SetPoItemGoodsReceivedSellerFunctionBase : FunctionMessage
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

    public partial class PurchaseItemAcceptedLogEventDTO : PurchaseItemAcceptedLogEventDTOBase { }

    [Event("PurchaseItemAcceptedLog")]
    public class PurchaseItemAcceptedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemCancelledLogEventDTO : PurchaseItemCancelledLogEventDTOBase { }

    [Event("PurchaseItemCancelledLog")]
    public class PurchaseItemCancelledLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemCompletedLogEventDTO : PurchaseItemCompletedLogEventDTOBase { }

    [Event("PurchaseItemCompletedLog")]
    public class PurchaseItemCompletedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemCreatedLogEventDTO : PurchaseItemCreatedLogEventDTOBase { }

    [Event("PurchaseItemCreatedLog")]
    public class PurchaseItemCreatedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemEscrowFailedLogEventDTO : PurchaseItemEscrowFailedLogEventDTOBase { }

    [Event("PurchaseItemEscrowFailedLog")]
    public class PurchaseItemEscrowFailedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemEscrowReleasedLogEventDTO : PurchaseItemEscrowReleasedLogEventDTOBase { }

    [Event("PurchaseItemEscrowReleasedLog")]
    public class PurchaseItemEscrowReleasedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemGoodsIssuedLogEventDTO : PurchaseItemGoodsIssuedLogEventDTOBase { }

    [Event("PurchaseItemGoodsIssuedLog")]
    public class PurchaseItemGoodsIssuedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemReadyForGoodsIssueLogEventDTO : PurchaseItemReadyForGoodsIssueLogEventDTOBase { }

    [Event("PurchaseItemReadyForGoodsIssueLog")]
    public class PurchaseItemReadyForGoodsIssueLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemReceivedLogEventDTO : PurchaseItemReceivedLogEventDTOBase { }

    [Event("PurchaseItemReceivedLog")]
    public class PurchaseItemReceivedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseItemRejectedLogEventDTO : PurchaseItemRejectedLogEventDTOBase { }

    [Event("PurchaseItemRejectedLog")]
    public class PurchaseItemRejectedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "poItem", 4, false )]
        public virtual PoItem PoItem { get; set; }
    }

    public partial class PurchaseOrderCreateRequestLogEventDTO : PurchaseOrderCreateRequestLogEventDTOBase { }

    [Event("PurchaseOrderCreateRequestLog")]
    public class PurchaseOrderCreateRequestLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
    }

    public partial class PurchaseOrderCreatedLogEventDTO : PurchaseOrderCreatedLogEventDTOBase { }

    [Event("PurchaseOrderCreatedLog")]
    public class PurchaseOrderCreatedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
    }

    public partial class PurchaseOrderNotCreatedLogEventDTO : PurchaseOrderNotCreatedLogEventDTOBase { }

    [Event("PurchaseOrderNotCreatedLog")]
    public class PurchaseOrderNotCreatedLogEventDTOBase : IEventDTO
    {
        [Parameter("address", "buyerAddress", 1, true )]
        public virtual string BuyerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 2, true )]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poNumber", 3, true )]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
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



    public partial class BpStorageOutputDTO : BpStorageOutputDTOBase { }

    [FunctionOutput]
    public class BpStorageOutputDTOBase : IFunctionOutputDTO 
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







    public partial class FundingOutputDTO : FundingOutputDTOBase { }

    [FunctionOutput]
    public class FundingOutputDTOBase : IFunctionOutputDTO 
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

    public partial class GetPoBySellerAndQuoteOutputDTO : GetPoBySellerAndQuoteOutputDTOBase { }

    [FunctionOutput]
    public class GetPoBySellerAndQuoteOutputDTOBase : IFunctionOutputDTO 
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

    public partial class PoStorageOutputDTO : PoStorageOutputDTOBase { }

    [FunctionOutput]
    public class PoStorageOutputDTOBase : IFunctionOutputDTO 
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
