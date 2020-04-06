using System;

namespace Nethereum.Commerce.Contracts.Deployment
{
    public class ContractDeploymentException : Exception
    {
        public ContractDeploymentException()
        {
        }

        public ContractDeploymentException(string message)
            : base(message)
        {
        }

        public ContractDeploymentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}