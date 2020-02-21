using System;
using System.Collections.Generic;
using System.Text;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRecord
    {
        void AddData(string attrName, string attrValue);

        string GetData(string attrName);
    }
}