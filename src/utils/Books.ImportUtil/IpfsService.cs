using Ipfs;
using Ipfs.CoreApi;
using Ipfs.Http;
using System.Threading.Tasks;

namespace Books.ImportUtil
{

    public class IpfsService
    {
        private string getUrl;
        private IpfsClient ipfsClient;

        public IpfsService(string host)
        {
            ipfsClient = new IpfsClient(host);
            getUrl = $"{ipfsClient.ApiUri.Scheme}://{ipfsClient.ApiUri.Host}/ipfs/".ToString();
        }

        public string GetUrl(string hash)
        {
            return getUrl + hash;
        }

        public async Task<Cid> AddFileAsync(string filePath, bool pin = true)
        {
            var cid = await ipfsClient
                .FileSystem
                .AddFileAsync(
                filePath, new AddFileOptions() { Pin = pin }).ConfigureAwait(false);

            return cid.Id;
        }
    }
}
