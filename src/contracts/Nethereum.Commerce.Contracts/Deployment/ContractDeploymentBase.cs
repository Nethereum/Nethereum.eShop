using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Web3;

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
            if (web3 == null)
            {
                throw new ContractDeploymentException($"Failed to set up {GetType().Name} web3 must be specified.");
            }
            // code-genned classes require web3, not an iweb3
            if (!(web3 is Web3.Web3 web3Cast))
            {
                throw new ContractDeploymentException($"Failed to set up {GetType().Name}. web3 must be castable to Web3.");
            }
            _web3 = web3Cast;
            _logger = logger;
        }

        /// <summary>
        /// Check that the passed address is valid and points to a valid business partner storage contract. Throws if not.
        /// </summary>
        protected async Task ValidateBusinessPartnerStorageAddressAsync(string businessPartnerStorageContractAddress)
        {
            if (string.IsNullOrWhiteSpace(businessPartnerStorageContractAddress) || businessPartnerStorageContractAddress.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {GetType().Name}. Global business partner storage address must have a value.");
            }
            // If business partner storage contract is valid, it will have an owner
            var bpss = new BusinessPartnerStorageService(_web3, businessPartnerStorageContractAddress);
            var businessPartnerStorageOwnerAddress = await bpss.OwnerQueryAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(businessPartnerStorageOwnerAddress) || businessPartnerStorageOwnerAddress.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {GetType().Name}. Fault with global business partner storage contract.");
            }
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
