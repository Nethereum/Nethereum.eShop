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
        public static string BYTECODE = "608060405234801561001057600080fd5b5060405162002304380380620023048339810160408190526100319161009f565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b03929092169190911790556100cd565b6000602082840312156100b0578081fd5b81516001600160a01b03811681146100c6578182fd5b9392505050565b61222780620000dd6000396000f3fe608060405234801561001057600080fd5b50600436106101585760003560e01c80638f32d59b116100c3578063c8d303f81161007c578063c8d303f81461027c578063cb4c86b71461028f578063cd882a0014610172578063cfb5192814610297578063f2fde38b146102aa578063f3ad65f4146102bd57610158565b80638f32d59b1461023157806391aa0f3014610239578063abefab871461024c578063b4bf807f14610261578063c076cfbf14610172578063c76ea9781461026957610158565b80634ac5042f116101155780634ac5042f146101725780636b00e9d8146101b65780636cc2c111146101d65780636fee6fec146101e95780638da5cb5b146102095780638e5fc30b1461021157610158565b806306ac2d3d1461015d5780630d9192ef14610172578063150e99f9146101855780633dafca6e1461017257806346885b5b146101725780634ab2930a14610198575b600080fd5b61017061016b3660046116ec565b6102c5565b005b610170610180366004611b4e565b61050d565b61017061019336600461168c565b610511565b6101a06105e3565b6040516101ad9190611d1e565b60405180910390f35b6101c96101c436600461168c565b6105f2565b6040516101ad9190611d32565b6101706101e4366004611b7d565b610607565b6101fc6101f7366004611782565b6108a2565b6040516101ad919061205d565b6101a0610a00565b61022461021f3660046116cb565b610a0f565b6040516101ad9190611d83565b6101c9610b39565b610170610247366004611856565b610b4a565b610254611036565b6040516101ad9190611d3d565b6101a061103c565b61017061027736600461168c565b61104b565b6101fc61028a366004611b1e565b611120565b6101a06111b3565b6102546102a53660046117cc565b6111c2565b6101706102b836600461168c565b6111e0565b6101a061120d565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906102f79089908990600401611d54565b60206040518083038186803b15801561030f57600080fd5b505afa158015610323573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061034791908101906116af565b600480546001600160a01b0319166001600160a01b0392831617908190551661038b5760405162461bcd60e51b815260040161038290611e29565b60405180910390fd5b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906103bd9087908790600401611d54565b60206040518083038186803b1580156103d557600080fd5b505afa1580156103e9573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061040d91908101906116af565b600580546001600160a01b0319166001600160a01b039283161790819055166104485760405162461bcd60e51b815260040161038290611eec565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061047a9085908590600401611d54565b60206040518083038186803b15801561049257600080fd5b505afa1580156104a6573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104ca91908101906116af565b600680546001600160a01b0319166001600160a01b039283161790819055166105055760405162461bcd60e51b815260040161038290611dd6565b505050505050565b5050565b610519610b39565b6105355760405162461bcd60e51b815260040161038290611f56565b6001600160a01b03811660009081526001602052604090205460ff16156105ab576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a26105e0565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b6004546001600160a01b031681565b60016020526000908152604090205460ff1681565b61060f611277565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f89161063e91899101611d3d565b60006040518083038186803b15801561065657600080fd5b505afa15801561066a573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f19168201604052610692919081019061198e565b80519091506106b35760405162461bcd60e51b815260040161038290611e7f565b80610180015160ff168460ff1611156106de5760405162461bcd60e51b815260040161038290611eaa565b60018460ff1610156107025760405162461bcd60e51b815260040161038290611f8b565b60ff6000198501166001826101a00151828151811061071d57fe5b60200260200101516101400151600881111561073557fe5b146107525760405162461bcd60e51b815260040161038290611fcc565b83826101a00151828151811061076457fe5b6020026020010151604001818152505082826101a00151828151811061078657fe5b602002602001015160600181815250506002826101a0015182815181106107a957fe5b6020026020010151610140019060088111156107c157fe5b908160088111156107ce57fe5b90525060048054604051636ed4e2d360e11b81526001600160a01b039091169163dda9c5a6916108009186910161205d565b600060405180830381600087803b15801561081a57600080fd5b505af115801561082e573d6000803e3d6000fd5b50505050816000015182610140015183602001516001600160a01b03167feacd69d950288f165b5a4149ff60b90bf02a2dbb2d32ee983ce33569924bebc1856101a00151858151811061087d57fe5b6020026020010151604051610892919061204e565b60405180910390a4505050505050565b6108aa611277565b60006108eb85858080601f0160208091040260200160405190810160405280939291908181526020018383808284376000920191909152506111c292505050565b60048054604051631edee79b60e21b81529293506000926001600160a01b0390911691637b7b9e6c91610922918691899101611d46565b60206040518083038186803b15801561093a57600080fd5b505afa15801561094e573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506109729190810190611b36565b6004805460405163191a607f60e31b81529293506001600160a01b03169163c8d303f8916109a291859101611d3d565b60006040518083038186803b1580156109ba57600080fd5b505afa1580156109ce573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526109f6919081019061198e565b9695505050505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b6020811015610a8d576008810260020a86026001600160f81b0319811615610a845780848481518110610a6657fe5b60200101906001600160f81b031916908160001a9053506001909201915b50600101610a37565b50600081851180610a9c575084155b15610aa8575080610aaf565b5060001984015b6060816040519080825280601f01601f191660200182016040528015610adc576020820181803883390190505b50905060005b82811015610b2c57848181518110610af657fe5b602001015160f81c60f81b828281518110610b0d57fe5b60200101906001600160f81b031916908160001a905350600101610ae2565b5093505050505b92915050565b6000546001600160a01b0316331490565b600081610140015182602001516001600160a01b03167f3abd43c7a604af572fc5cf0446d9fbf8c3df782be6ef917226df288f58a5db3984604051610b8f919061205d565b60405180910390a4610b9f6112ee565b600554610140830151604051631f91394160e21b81526001600160a01b0390921691637e44e50491610bd391600401611d3d565b60a06040518083038186803b158015610beb57600080fd5b505afa158015610bff573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610c239190810190611ab5565b60608101519091506001600160a01b0316610c505760405162461bcd60e51b81526004016103829061200d565b600480546040805163e82f815f60e01b815290516001600160a01b039092169263e82f815f92828201926000929082900301818387803b158015610c9357600080fd5b505af1158015610ca7573d6000803e3d6000fd5b50506004805460408051632d77100760e11b815290516001600160a01b039092169450635aee200e93508083019260209291829003018186803b158015610ced57600080fd5b505afa158015610d01573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610d259190810190611b36565b825260608101516001600160a01b0316610100830152426101608301526101a08201515160ff811661018084015260005b83610180015160ff16811015610ef05783516101a0850151805183908110610d7a57fe5b6020026020010151600001818152505080600101846101a001518281518110610d9f57fe5b60200260200101516020019060ff16908160ff16815250506001846101a001518281518110610dca57fe5b602002602001015161014001906008811115610de257fe5b90816008811115610def57fe5b815250506000846101a001518281518110610e0657fe5b60200260200101516101600181815250506000846101a001518281518110610e2a57fe5b60200260200101516101800181815250506000846101a001518281518110610e4e57fe5b60200260200101516101a00181815250506000846101a001518281518110610e7257fe5b60200260200101516101c00181815250506000846101a001518281518110610e9657fe5b60200260200101516101e00190151590811515815250506000846101a001518281518110610ec057fe5b602002602001015161020001906003811115610ed857fe5b90816003811115610ee557fe5b905250600101610d56565b5060048054604051636ed4e2d360e11b81526001600160a01b039091169163dda9c5a691610f209187910161205d565b600060405180830381600087803b158015610f3a57600080fd5b505af1158015610f4e573d6000803e3d6000fd5b50505050610f5a611277565b60048054855160405163191a607f60e31b81526001600160a01b039092169263c8d303f892610f8a929101611d3d565b60006040518083038186803b158015610fa257600080fd5b505afa158015610fb6573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f19168201604052610fde919081019061198e565b9050806000015181610140015182602001516001600160a01b03167f1bedbc3174124c2e9950123708cb0e8a0b79513f17c4e109361efd0acb1ed97884604051611028919061205d565b60405180910390a450505050565b60025481565b6005546001600160a01b031681565b611053610b39565b61106f5760405162461bcd60e51b815260040161038290611f56565b6001600160a01b03811660009081526001602052604090205460ff16156110c9576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a26105e0565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b611128611277565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f89161115791869101611d3d565b60006040518083038186803b15801561116f57600080fd5b505afa158015611183573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526111ab919081019061198e565b90505b919050565b6006546001600160a01b031681565b805160009082906111d75750600090506111ae565b50506020015190565b6111e8610b39565b6112045760405162461bcd60e51b815260040161038290611f56565b6105e08161121c565b6003546001600160a01b031681565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b6040805160a08101825260008082526020820181905291810182905260608101829052608081019190915290565b8035610b3381612198565b8051610b3381612198565b600082601f830112611342578081fd5b813561135561135082612178565b612151565b81815291506020808301908481016102208085028701830188101561137957600080fd5b60005b858110156114815781838a03121561139357600080fd5b61139c82612151565b833581526113ac8a868601611676565b8582015260408401356040820152606084013560608201526080840135608082015260a084013560a082015260c084013560c082015260e084013560e08201526101006113fb8b82870161131c565b9082015261012084810135908201526101406114198b828701611603565b90820152610160848101359082015261018080850135908201526101a080850135908201526101c080850135908201526101e06114588b8287016115d7565b9082015261020061146b8b8683016115ed565b908201528552938301939181019160010161137c565b50505050505092915050565b600082601f83011261149d578081fd5b81516114ab61135082612178565b8181529150602080830190848101610220808502870183018810156114cf57600080fd5b60005b858110156114815781838a0312156114e957600080fd5b6114f282612151565b835181526115028a868601611681565b8582015260408401516040820152606084015160608201526080840151608082015260a084015160a082015260c084015160c082015260e084015160e08201526101006115518b828701611327565b90820152610120848101519082015261014061156f8b82870161160e565b90820152610160848101519082015261018080850151908201526101a080850151908201526101c080850151908201526101e06115ae8b8287016115e2565b908201526102006115c18b8683016115f8565b90820152855293830193918101916001016114d2565b8035610b33816121ad565b8051610b33816121ad565b8035610b33816121bb565b8051610b33816121bb565b8035610b33816121c8565b8051610b33816121c8565b8035610b33816121d5565b8051610b33816121d5565b60008083601f840112611640578182fd5b50813567ffffffffffffffff811115611657578182fd5b60208301915083602082850101111561166f57600080fd5b9250929050565b8035610b33816121e2565b8051610b33816121e2565b60006020828403121561169d578081fd5b81356116a881612198565b9392505050565b6000602082840312156116c0578081fd5b81516116a881612198565b600080604083850312156116dd578081fd5b50508035926020909101359150565b60008060008060008060608789031215611704578182fd5b863567ffffffffffffffff8082111561171b578384fd5b6117278a838b0161162f565b9098509650602089013591508082111561173f578384fd5b61174b8a838b0161162f565b90965094506040890135915080821115611763578384fd5b5061177089828a0161162f565b979a9699509497509295939492505050565b600080600060408486031215611796578081fd5b833567ffffffffffffffff8111156117ac578182fd5b6117b88682870161162f565b909790965060209590950135949350505050565b600060208083850312156117de578182fd5b823567ffffffffffffffff808211156117f5578384fd5b81850186601f820112611806578485fd5b8035925081831115611816578485fd5b611828601f8401601f19168501612151565b9150828252868484830101111561183d578485fd5b8284820185840137509081019091019190915292915050565b600060208284031215611867578081fd5b813567ffffffffffffffff8082111561187e578283fd5b6101c0918401808603831315611892578384fd5b61189b83612151565b813581526118ac876020840161131c565b60208201526118be876040840161131c565b60408201526118d0876060840161131c565b6060820152608082013560808201526118ec8760a0840161131c565b60a082015260c082013560c082015260e082013560e082015261010093506119168785840161131c565b84820152610120935061192b87858401611619565b93810193909352610140818101359084015261016080820135908401526101809261195887858401611676565b848201526101a093508382013583811115611971578586fd5b61197d88828501611332565b948201949094529695505050505050565b60006020828403121561199f578081fd5b815167ffffffffffffffff808211156119b6578283fd5b6101c09184018086038313156119ca578384fd5b6119d383612151565b815181526119e48760208401611327565b60208201526119f68760408401611327565b6040820152611a088760608401611327565b606082015260808201516080820152611a248760a08401611327565b60a082015260c082015160c082015260e082015160e08201526101009350611a4e87858401611327565b848201526101209350611a6387858401611624565b938101939093526101408181015190840152610160808201519084015261018092611a9087858401611681565b848201526101a093508382015183811115611aa9578586fd5b61197d8882850161148d565b600060a08284031215611ac6578081fd5b611ad060a0612151565b82518152602083015160208201526040830151611aec81612198565b60408201526060830151611aff81612198565b60608201526080830151611b12816121ad565b60808201529392505050565b600060208284031215611b2f578081fd5b5035919050565b600060208284031215611b47578081fd5b5051919050565b60008060408385031215611b60578182fd5b823591506020830135611b72816121e2565b809150509250929050565b60008060008060808587031215611b92578182fd5b843593506020850135611ba4816121e2565b93969395505050506040820135916060013590565b6001600160a01b03169052565b6000815180845260208401935060208301825b82811015611c0357611bec868351611c35565b610220959095019460209190910190600101611bd9565b5093949350505050565b15159052565b60048110611c1d57fe5b9052565b60098110611c1d57fe5b60038110611c1d57fe5b805182526020810151611c4b6020840182611d17565b5060408101516040830152606081015160608301526080810151608083015260a081015160a083015260c081015160c083015260e081015160e083015261010080820151611c9b82850182611bb9565b5050610120818101519083015261014080820151611cbb82850182611c21565b5050610160818101519083015261018080820151908301526101a080820151908301526101c080820151908301526101e080820151611cfc82850182611c0d565b505061020080820151611d1182850182611c13565b50505050565b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b918252602082015260400190565b60006020825282602083015282846040840137818301604090810191909152601f909201601f19160101919050565b6000602082528251806020840152815b81811015611db05760208186018101516040868401015201611d93565b81811115611dc15782604083860101525b50601f01601f19169190910160400192915050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b6020808252601190820152701413c8191bd95cc81b9bdd08195e1a5cdd607a1b604082015260600190565b60208082526022908201527f504f206974656d20646f6573206e6f742065786973742028746f6f206c617267604082015261652960f01b606082015260800190565b60208082526044908201527f436f756c64206e6f742066696e6420427573696e65737320506172746e65722060408201527f53746f7261676520636f6e7472616374206164647265737320696e20726567696060820152637374727960e01b608082015260a00190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b60208082526021908201527f504f206974656d20646f6573206e6f7420657869737420286d696e20697320316040820152602960f81b606082015260800190565b60208082526021908201527f4578697374696e6720504f206974656d2073746174757320696e636f727265636040820152601d60fa1b606082015260800190565b60208082526021908201527f53656c6c657220496420686173206e6f20617070726f766572206164647265736040820152607360f81b606082015260800190565b6102208101610b338284611c35565b60006020825282516020830152602083015161207c6040840182611bb9565b50604083015161208f6060840182611bb9565b5060608301516120a26080840182611bb9565b50608083015160a083015260a08301516120bf60c0840182611bb9565b5060c083015160e083015260e08301516101008181850152808501519150506101206120ed81850183611bb9565b840151905061014061210184820183611c2b565b840151610160848101919091528401516101808085019190915284015190506101a061212f81850183611d17565b8401516101c08481015290506121496101e0840182611bc6565b949350505050565b60405181810167ffffffffffffffff8111828210171561217057600080fd5b604052919050565b600067ffffffffffffffff82111561218e578081fd5b5060209081020190565b6001600160a01b03811681146105e057600080fd5b80151581146105e057600080fd5b600481106105e057600080fd5b600981106105e057600080fd5b600381106105e057600080fd5b60ff811681146105e057600080fdfea2646970667358221220def7ca344f07138100b9e8ea19556331f4758b3ffbc8084d3384c85bd62d6bee64736f6c63430006010033";
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
