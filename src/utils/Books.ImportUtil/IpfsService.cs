using Ipfs;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Books.ImportUtil
{

    public class IpfsService
    {

        private string ipfsUrl;
        private string getUrl;
        public IpfsService(string ipfsUrl)
        {
            this.ipfsUrl = ipfsUrl;
            var uri = new Uri(ipfsUrl);
            getUrl = $"{uri.Scheme}://{uri.Host}/ipfs/".ToString();
        }

        public string GetUrl(string hash)
        {
            return getUrl + hash;
        }

        public async Task<MerkleNode> AddFileAsync(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                return await AddAsync(fileName, fileStream).ConfigureAwait(false);
            }
        }

        public async Task<MerkleNode> AddAsync(string name, Stream stream)
        {
            using (var ipfs = new IpfsClient(ipfsUrl))
            {
                var inputStream = new IpfsStream(name, stream);

                var merkleNode = await ipfs.Add(inputStream).ConfigureAwait(false);
                var multiHash = ipfs.Pin.Add(merkleNode.Hash.ToString());
                return merkleNode;
            }
        }

        public async Task<HttpContent> PinListAsync()
        {
            using (var ipfs = new IpfsClient(ipfsUrl))
            {
                return await ipfs.Pin.Ls();
            }
        }
    }
}
