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
        public static string BYTECODE = "60806040523480156200001157600080fd5b506040516200267b3803806200267b8339810160408190526200003491620000a3565b600080546001600160a01b03191633178082556040516001600160a01b039190911691907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a3600380546001600160a01b0319166001600160a01b0392909216919091179055620000d3565b600060208284031215620000b5578081fd5b81516001600160a01b0381168114620000cc578182fd5b9392505050565b61259880620000e36000396000f3fe608060405234801561001057600080fd5b50600436106101585760003560e01c80638f32d59b116100c3578063c8d303f81161007c578063c8d303f8146102a2578063cb4c86b7146102b5578063cd882a0014610172578063cfb51928146102bd578063f2fde38b146102d0578063f3ad65f4146102e357610158565b80638f32d59b1461025757806391aa0f301461025f578063abefab8714610272578063b4bf807f14610287578063c076cfbf14610172578063c76ea9781461028f57610158565b80634ac5042f116101155780634ac5042f146101c95780636b00e9d8146101dc5780636cc2c111146101fc5780636fee6fec1461020f5780638da5cb5b1461022f5780638e5fc30b1461023757610158565b806306ac2d3d1461015d5780630d9192ef14610172578063150e99f9146101855780633dafca6e1461017257806346885b5b146101985780634ab2930a146101ab575b600080fd5b61017061016b366004611a63565b6102eb565b005b610170610180366004611ec5565b610533565b610170610193366004611a03565b610537565b6101706101a6366004611ec5565b610609565b6101b36107b7565b6040516101c0919061208f565b60405180910390f35b6101706101d7366004611ec5565b6107c6565b6101ef6101ea366004611a03565b610951565b6040516101c091906120a3565b61017061020a366004611ef4565b610966565b61022261021d366004611af9565b610b5a565b6040516101c091906123ce565b6101b3610cb8565b61024a610245366004611a42565b610cc7565b6040516101c091906120f4565b6101ef610df1565b61017061026d366004611bcd565b610e02565b61027a6112e0565b6040516101c091906120ae565b6101b36112e6565b61017061029d366004611a03565b6112f5565b6102226102b0366004611e95565b6113ca565b6101b361145d565b61027a6102cb366004611b43565b61146c565b6101706102de366004611a03565b61148a565b6101b36114b7565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061031d90899089906004016120c5565b60206040518083038186803b15801561033557600080fd5b505afa158015610349573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061036d9190810190611a26565b600480546001600160a01b0319166001600160a01b039283161790819055166103b15760405162461bcd60e51b81526004016103a89061219a565b60405180910390fd5b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906103e390879087906004016120c5565b60206040518083038186803b1580156103fb57600080fd5b505afa15801561040f573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104339190810190611a26565b600580546001600160a01b0319166001600160a01b0392831617908190551661046e5760405162461bcd60e51b81526004016103a89061225d565b60035460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906104a090859085906004016120c5565b60206040518083038186803b1580156104b857600080fd5b505afa1580156104cc573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104f09190810190611a26565b600680546001600160a01b0319166001600160a01b0392831617908190551661052b5760405162461bcd60e51b81526004016103a890612147565b505050505050565b5050565b61053f610df1565b61055b5760405162461bcd60e51b81526004016103a8906122c7565b6001600160a01b03811660009081526001602052604090205460ff16156105d1576001600160a01b038116600081815260016020526040808220805460ff1916905560028054600019019055517f8ddb5a2efd673581f97000ec107674428dc1ed37e8e7944654e48fb0688114779190a2610606565b6040516001600160a01b038216907f21aa6b3368eceb61c9fc1bdfd2cb6337c87cc1510c1afcc7d5a45371551b43b890600090a25b50565b6106116115ee565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f891610640918791016120ae565b60006040518083038186803b15801561065857600080fd5b505afa15801561066c573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526106949190810190611d05565b90506106a2818360026114c6565b60006001830360ff1690506004826101a0015182815181106106c057fe5b6020026020010151610140019060088111156106d857fe5b908160088111156106e557fe5b90525060048054604051636ed4e2d360e11b81526001600160a01b039091169163dda9c5a691610717918691016123ce565b600060405180830381600087803b15801561073157600080fd5b505af1158015610745573d6000803e3d6000fd5b50505050816000015182610140015183602001516001600160a01b03167f5583432a11b39212bbaac1dfe2b6663b738540347c5a20985086ff84bbfce49b856101a00151858151811061079457fe5b60200260200101516040516107a991906123bf565b60405180910390a450505050565b6004546001600160a01b031681565b6107ce6115ee565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f8916107fd918791016120ae565b60006040518083038186803b15801561081557600080fd5b505afa158015610829573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526108519190810190611d05565b905061085f818360046114c6565b60006001830360ff1690506005826101a00151828151811061087d57fe5b60200260200101516101400190600881111561089557fe5b908160088111156108a257fe5b90525060048054604051636ed4e2d360e11b81526001600160a01b039091169163dda9c5a6916108d4918691016123ce565b600060405180830381600087803b1580156108ee57600080fd5b505af1158015610902573d6000803e3d6000fd5b50505050816000015182610140015183602001516001600160a01b03167faecb2768bab522e9333853428741da856be35e153f679854f1e7a79a7d63c252856101a00151858151811061079457fe5b60016020526000908152604090205460ff1681565b61096e6115ee565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f89161099d918991016120ae565b60006040518083038186803b1580156109b557600080fd5b505afa1580156109c9573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526109f19190810190611d05565b90506109ff818560016114c6565b60006001850360ff16905083826101a001518281518110610a1c57fe5b6020026020010151604001818152505082826101a001518281518110610a3e57fe5b602002602001015160600181815250506002826101a001518281518110610a6157fe5b602002602001015161014001906008811115610a7957fe5b90816008811115610a8657fe5b90525060048054604051636ed4e2d360e11b81526001600160a01b039091169163dda9c5a691610ab8918691016123ce565b600060405180830381600087803b158015610ad257600080fd5b505af1158015610ae6573d6000803e3d6000fd5b50505050816000015182610140015183602001516001600160a01b03167feacd69d950288f165b5a4149ff60b90bf02a2dbb2d32ee983ce33569924bebc1856101a001518581518110610b3557fe5b6020026020010151604051610b4a91906123bf565b60405180910390a4505050505050565b610b626115ee565b6000610ba385858080601f01602080910402602001604051908101604052809392919081815260200183838082843760009201919091525061146c92505050565b60048054604051631edee79b60e21b81529293506000926001600160a01b0390911691637b7b9e6c91610bda9186918991016120b7565b60206040518083038186803b158015610bf257600080fd5b505afa158015610c06573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610c2a9190810190611ead565b6004805460405163191a607f60e31b81529293506001600160a01b03169163c8d303f891610c5a918591016120ae565b60006040518083038186803b158015610c7257600080fd5b505afa158015610c86573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f19168201604052610cae9190810190611d05565b9695505050505050565b6000546001600160a01b031690565b6040805160208082528183019092526060918291906020820181803883390190505090506000805b6020811015610d45576008810260020a86026001600160f81b0319811615610d3c5780848481518110610d1e57fe5b60200101906001600160f81b031916908160001a9053506001909201915b50600101610cef565b50600081851180610d54575084155b15610d60575080610d67565b5060001984015b6060816040519080825280601f01601f191660200182016040528015610d94576020820181803883390190505b50905060005b82811015610de457848181518110610dae57fe5b602001015160f81c60f81b828281518110610dc557fe5b60200101906001600160f81b031916908160001a905350600101610d9a565b5093505050505b92915050565b6000546001600160a01b0316331490565b600081610140015182602001516001600160a01b03167f3abd43c7a604af572fc5cf0446d9fbf8c3df782be6ef917226df288f58a5db3984604051610e4791906123ce565b60405180910390a4610e57611665565b600554610140830151604051631f91394160e21b81526001600160a01b0390921691637e44e50491610e8b916004016120ae565b60a06040518083038186803b158015610ea357600080fd5b505afa158015610eb7573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610edb9190810190611e2c565b60608101519091506001600160a01b0316610f085760405162461bcd60e51b81526004016103a89061237e565b600480546040805163e82f815f60e01b815290516001600160a01b039092169263e82f815f92828201926000929082900301818387803b158015610f4b57600080fd5b505af1158015610f5f573d6000803e3d6000fd5b50506004805460408051632d77100760e11b815290516001600160a01b039092169450635aee200e93508083019260209291829003018186803b158015610fa557600080fd5b505afa158015610fb9573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610fdd9190810190611ead565b825260608101516001600160a01b0316610100830152426101608301526101a08201515160ff811661018084015260005b83610180015160ff168110156111a85783516101a085015180518390811061103257fe5b6020026020010151600001818152505080600101846101a00151828151811061105757fe5b60200260200101516020019060ff16908160ff16815250506001846101a00151828151811061108257fe5b60200260200101516101400190600881111561109a57fe5b908160088111156110a757fe5b815250506000846101a0015182815181106110be57fe5b60200260200101516101600181815250506000846101a0015182815181106110e257fe5b60200260200101516101800181815250506000846101a00151828151811061110657fe5b60200260200101516101a00181815250506000846101a00151828151811061112a57fe5b60200260200101516101c00181815250506000846101a00151828151811061114e57fe5b60200260200101516101e00190151590811515815250506000846101a00151828151811061117857fe5b60200260200101516102000190600381111561119057fe5b9081600381111561119d57fe5b90525060010161100e565b5060048054604051636ed4e2d360e11b81526001600160a01b039091169163dda9c5a6916111d8918791016123ce565b600060405180830381600087803b1580156111f257600080fd5b505af1158015611206573d6000803e3d6000fd5b505050506112126115ee565b60048054855160405163191a607f60e31b81526001600160a01b039092169263c8d303f8926112429291016120ae565b60006040518083038186803b15801561125a57600080fd5b505afa15801561126e573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526112969190810190611d05565b9050806000015181610140015182602001516001600160a01b03167f1bedbc3174124c2e9950123708cb0e8a0b79513f17c4e109361efd0acb1ed978846040516107a991906123ce565b60025481565b6005546001600160a01b031681565b6112fd610df1565b6113195760405162461bcd60e51b81526004016103a8906122c7565b6001600160a01b03811660009081526001602052604090205460ff1615611373576040516001600160a01b038216907ff6831fd5f976003f3acfcf6b2784b2f81e3abb43d161884873a310d6beadf38090600090a2610606565b6001600160a01b0381166000818152600160208190526040808320805460ff19168317905560028054909201909155517f3c4dcdfdb789d0f39b8a520a4f83ab2599db1d2ececebe4db2385f360c0d3ce19190a250565b6113d26115ee565b6004805460405163191a607f60e31b81526001600160a01b039091169163c8d303f891611401918691016120ae565b60006040518083038186803b15801561141957600080fd5b505afa15801561142d573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526114559190810190611d05565b90505b919050565b6006546001600160a01b031681565b80516000908290611481575060009050611458565b50506020015190565b611492610df1565b6114ae5760405162461bcd60e51b81526004016103a8906122c7565b61060681611593565b6003546001600160a01b031681565b82516114e45760405162461bcd60e51b81526004016103a8906121f0565b82610180015160ff168260ff16111561150f5760405162461bcd60e51b81526004016103a89061221b565b60018260ff1610156115335760405162461bcd60e51b81526004016103a8906122fc565b60ff60001983011681600881111561154757fe5b846101a00151828151811061155857fe5b60200260200101516101400151600881111561157057fe5b1461158d5760405162461bcd60e51b81526004016103a89061233d565b50505050565b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604080516101c081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e0810182905261010081018290529061012082019081526020016000801916815260200160008152602001600060ff168152602001606081525090565b6040805160a08101825260008082526020820181905291810182905260608101829052608081019190915290565b8035610deb81612509565b8051610deb81612509565b600082601f8301126116b9578081fd5b81356116cc6116c7826124e9565b6124c2565b8181529150602080830190848101610220808502870183018810156116f057600080fd5b60005b858110156117f85781838a03121561170a57600080fd5b611713826124c2565b833581526117238a8686016119ed565b8582015260408401356040820152606084013560608201526080840135608082015260a084013560a082015260c084013560c082015260e084013560e08201526101006117728b828701611693565b9082015261012084810135908201526101406117908b82870161197a565b90820152610160848101359082015261018080850135908201526101a080850135908201526101c080850135908201526101e06117cf8b82870161194e565b908201526102006117e28b868301611964565b90820152855293830193918101916001016116f3565b50505050505092915050565b600082601f830112611814578081fd5b81516118226116c7826124e9565b81815291506020808301908481016102208085028701830188101561184657600080fd5b60005b858110156117f85781838a03121561186057600080fd5b611869826124c2565b835181526118798a8686016119f8565b8582015260408401516040820152606084015160608201526080840151608082015260a084015160a082015260c084015160c082015260e084015160e08201526101006118c88b82870161169e565b9082015261012084810151908201526101406118e68b828701611985565b90820152610160848101519082015261018080850151908201526101a080850151908201526101c080850151908201526101e06119258b828701611959565b908201526102006119388b86830161196f565b9082015285529383019391810191600101611849565b8035610deb8161251e565b8051610deb8161251e565b8035610deb8161252c565b8051610deb8161252c565b8035610deb81612539565b8051610deb81612539565b8035610deb81612546565b8051610deb81612546565b60008083601f8401126119b7578182fd5b50813567ffffffffffffffff8111156119ce578182fd5b6020830191508360208285010111156119e657600080fd5b9250929050565b8035610deb81612553565b8051610deb81612553565b600060208284031215611a14578081fd5b8135611a1f81612509565b9392505050565b600060208284031215611a37578081fd5b8151611a1f81612509565b60008060408385031215611a54578081fd5b50508035926020909101359150565b60008060008060008060608789031215611a7b578182fd5b863567ffffffffffffffff80821115611a92578384fd5b611a9e8a838b016119a6565b90985096506020890135915080821115611ab6578384fd5b611ac28a838b016119a6565b90965094506040890135915080821115611ada578384fd5b50611ae789828a016119a6565b979a9699509497509295939492505050565b600080600060408486031215611b0d578081fd5b833567ffffffffffffffff811115611b23578182fd5b611b2f868287016119a6565b909790965060209590950135949350505050565b60006020808385031215611b55578182fd5b823567ffffffffffffffff80821115611b6c578384fd5b81850186601f820112611b7d578485fd5b8035925081831115611b8d578485fd5b611b9f601f8401601f191685016124c2565b91508282528684848301011115611bb4578485fd5b8284820185840137509081019091019190915292915050565b600060208284031215611bde578081fd5b813567ffffffffffffffff80821115611bf5578283fd5b6101c0918401808603831315611c09578384fd5b611c12836124c2565b81358152611c238760208401611693565b6020820152611c358760408401611693565b6040820152611c478760608401611693565b606082015260808201356080820152611c638760a08401611693565b60a082015260c082013560c082015260e082013560e08201526101009350611c8d87858401611693565b848201526101209350611ca287858401611990565b938101939093526101408181013590840152610160808201359084015261018092611ccf878584016119ed565b848201526101a093508382013583811115611ce8578586fd5b611cf4888285016116a9565b948201949094529695505050505050565b600060208284031215611d16578081fd5b815167ffffffffffffffff80821115611d2d578283fd5b6101c0918401808603831315611d41578384fd5b611d4a836124c2565b81518152611d5b876020840161169e565b6020820152611d6d876040840161169e565b6040820152611d7f876060840161169e565b606082015260808201516080820152611d9b8760a0840161169e565b60a082015260c082015160c082015260e082015160e08201526101009350611dc58785840161169e565b848201526101209350611dda8785840161199b565b938101939093526101408181015190840152610160808201519084015261018092611e07878584016119f8565b848201526101a093508382015183811115611e20578586fd5b611cf488828501611804565b600060a08284031215611e3d578081fd5b611e4760a06124c2565b82518152602083015160208201526040830151611e6381612509565b60408201526060830151611e7681612509565b60608201526080830151611e898161251e565b60808201529392505050565b600060208284031215611ea6578081fd5b5035919050565b600060208284031215611ebe578081fd5b5051919050565b60008060408385031215611ed7578182fd5b823591506020830135611ee981612553565b809150509250929050565b60008060008060808587031215611f09578182fd5b843593506020850135611f1b81612553565b93969395505050506040820135916060013590565b6001600160a01b03169052565b6000815180845260208401935060208301825b82811015611f7a57611f63868351611fac565b610220959095019460209190910190600101611f50565b5093949350505050565b15159052565b60048110611f9457fe5b9052565b60098110611f9457fe5b60038110611f9457fe5b805182526020810151611fc26020840182612088565b5060408101516040830152606081015160608301526080810151608083015260a081015160a083015260c081015160c083015260e081015160e08301526101008082015161201282850182611f30565b505061012081810151908301526101408082015161203282850182611f98565b5050610160818101519083015261018080820151908301526101a080820151908301526101c080820151908301526101e08082015161207382850182611f84565b50506102008082015161158d82850182611f8a565b60ff169052565b6001600160a01b0391909116815260200190565b901515815260200190565b90815260200190565b918252602082015260400190565b60006020825282602083015282846040840137818301604090810191909152601f909201601f19160101919050565b6000602082528251806020840152815b818110156121215760208186018101516040868401015201612104565b818111156121325782604083860101525b50601f01601f19169190910160400192915050565b60208082526033908201527f436f756c64206e6f742066696e642046756e64696e6720636f6e7472616374206040820152726164647265737320696e20726567697374727960681b606082015260800190565b60208082526036908201527f436f756c64206e6f742066696e642050757263686173696e6720636f6e7472616040820152756374206164647265737320696e20726567697374727960501b606082015260800190565b6020808252601190820152701413c8191bd95cc81b9bdd08195e1a5cdd607a1b604082015260600190565b60208082526022908201527f504f206974656d20646f6573206e6f742065786973742028746f6f206c617267604082015261652960f01b606082015260800190565b60208082526044908201527f436f756c64206e6f742066696e6420427573696e65737320506172746e65722060408201527f53746f7261676520636f6e7472616374206164647265737320696e20726567696060820152637374727960e01b608082015260a00190565b6020808252818101527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e6572604082015260600190565b60208082526021908201527f504f206974656d20646f6573206e6f7420657869737420286d696e20697320316040820152602960f81b606082015260800190565b60208082526021908201527f4578697374696e6720504f206974656d2073746174757320696e636f727265636040820152601d60fa1b606082015260800190565b60208082526021908201527f53656c6c657220496420686173206e6f20617070726f766572206164647265736040820152607360f81b606082015260800190565b6102208101610deb8284611fac565b6000602082528251602083015260208301516123ed6040840182611f30565b5060408301516124006060840182611f30565b5060608301516124136080840182611f30565b50608083015160a083015260a083015161243060c0840182611f30565b5060c083015160e083015260e083015161010081818501528085015191505061012061245e81850183611f30565b840151905061014061247284820183611fa2565b840151610160848101919091528401516101808085019190915284015190506101a06124a081850183612088565b8401516101c08481015290506124ba6101e0840182611f3d565b949350505050565b60405181810167ffffffffffffffff811182821017156124e157600080fd5b604052919050565b600067ffffffffffffffff8211156124ff578081fd5b5060209081020190565b6001600160a01b038116811461060657600080fd5b801515811461060657600080fd5b6004811061060657600080fd5b6009811061060657600080fd5b6003811061060657600080fd5b60ff8116811461060657600080fdfea2646970667358221220053eea823fe5423af45d8304eefa136b67fb2cf768731444409449ea6451f5c564736f6c63430006010033";
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
