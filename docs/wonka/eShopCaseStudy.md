# Usage of Wonka in Nethereum.eShop: A Case Study

The [Wonka project](https://github.com/Nethereum/Wonka) is a rules engine that exists somewhat bifurcated.  The .NET portion of Wonka (i.e., Wonka.NET) 
is a rules engine that can be initialized with proprietary XML and then used within the .NET domain; the Ethereum/Solidity portion of Wonka (i.e., Wonka.ETH) 
is a rules engine that exists and executes on the chain.  The Wonka.NET engine can serialize rules to the Ethereum chain, but the Wonka.ETH engine cannot
serialize rules back into the .NET domain.  In this scenario, we will discuss how both Wonka.NET and Wonka.ETH can be utilized together in order
to make the eShop more transparent and more productive.

## Benefits of Using Wonka within the eShop
+ The business rules that can instantiate Wonka.NET, defined with XML markup, can be saved into IPFS and transparently show what business logic 
is invoked before and after a purchase order.  Calculations (like of a VAT tax) or validations (like whether appropriate sales rights exist in the buyer's
country) used by the eShop can be captured and shown for all to see.
+ Third-party sellers (i.e., trusted partners to the eShop) would also be required to post any supplemental business logic in the same manner, 
making them transparent as well.
+ Wonka.NET can be used to simulate the rules during the editing/debugging process.
+ Once the rules are deemed satisfactory, the Wonka.NET package can serialize the rules to the Wonka.ETH engine.  In that way, the owner
of the eShop (and, to some degree, its trusted third-party sellers) can deploy simple code and execute it within the Ethereum chain.  Such code
will perform simple tasks, like calling a contract method to obtain the buyer's resident country and then calculating (and storing) the appropriate
VAT amount for the purchase.  All of this can be done without the owner or anyone else knowing how to write Solidity or deploy anything to Ethereum.

## Requirements for using Wonka within eShop
* Proper awareness of data domain
* Adequate plan for purchase rules
* Proper awareness of third-party contracts and their methods that are necessary for purchases

## Functionality Intended to be Supported with Wonka in the eShop
- [ ] Validate a potential purchase
- [ ] Calculate supplemental amounts for a purchase (VAT amount, etc.) 
- [ ] Orchestrate supplemental but necessary actions on the chain that are important for the eShop.
- [ ] Provide a door for plugin functionality to the eShop's owner or trusted associates
