using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nethereum.Web3;
using Ardalis.GuardClauses;

namespace Nethereum.Commerce.Contracts.Deployment
{
    public class ContractDeploymentBase
    {
        public string Owner { get; internal set; }

        protected bool _isNewDeployment;
        protected readonly Web3.Web3 _web3;
        protected readonly ILogger _logger;

        public ContractDeploymentBase(IWeb3 web3, ILogger logger)
        {
            Guard.Against.Null(web3, nameof(web3));
            _web3 = (Web3.Web3)web3; // code-genned classes require web3, not an iweb3
            _logger = logger;
        }

        protected void LogHeader(string s)
        {
            Log();
            Log($"--------------  {s}  --------------");
            Log();
        }

        protected void Log() => Log(string.Empty);

        protected void Log(string message)
        {
            if (_logger != null)
            {
                _logger.LogInformation(message);
            }
        }
    }
}
