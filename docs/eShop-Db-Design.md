# Nethereum eShop Db Design

The eShop takes a hybrid approach to decentralisation.  It attempts to balance Blockchain benefits with some requirements for off-chain processing and storage.  For instance, data which is subject to privacy rules may not belong on chain (e.g. personal postal addresses) whilst some documents (e.g. proof of purchase) may suit on chain storage perfectly.

## Data Layer

The core business services in the Nethereum.eShop are agnostic of the actual data persistence provider.  A mixture of the repository and CQRS patterns are present.  The repositories are typically involved in business unit transactions whilst queries are for highly optimised, performance intensive requirements which may require a provider specific implementation or additional resources such as data warehouses, external API's, search services etc.   As a basic example, Entity Framework Core can provide a repository layer implementation whilst Dapper can help provide optimised queries which in some organisations remain under DBA control.

One of the primary reasons for this was that some entities would be likely to move from one storage solution to another over time.   Some entities which can't be stored on chain at present may be able to go on chain in the future once specific privacy concerns have been addressed in core Blockchain technology.

Both the repository or query interfaces are agnostic of provider.
