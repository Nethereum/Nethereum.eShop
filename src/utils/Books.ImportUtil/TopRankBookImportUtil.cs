using Nethereum.eShop.ApplicationCore.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Books.ImportUtil
{
    public partial class TopRankBookImportUtil
    {
        public const string HEADERS = "EAN|TITLE|PUB_NAME|PUB_DATE|AUTHOR|PAGE_NUM|SUBJECT_TIME|SUBJECT|WEIGHT|COVER_SMALL|COVER_MED|COVER_LARGE|AGE_MIN|AGE_MAX|ADULT_FLAG|DEWEW_NUM|DEPTH|WIDTH|LENGTH|PRICE|PUBLIC_DOMAIN_FLAG|PUB_COUNTRY_CD|US_RIGHTS_IND|BISAC_SUBJ_CD";
        public const char COLUMN_DELIMITER = '|';

        public static string BASE_FOLDER = @"C:\Users\davew\source\repos\forks\Nethereum.eShop\resources\data\TopRankBooks\ImportData";
        public static string IMPORT_FILE_FOLDER = BASE_FOLDER;
        public static string CATALOG_IMPORT_FILE_PATH = $@"{IMPORT_FILE_FOLDER}\TopRankBooksForCatalogImport.json";
        public static string RENAMED_COVER_OUTPUT_FOLDER = $@"{IMPORT_FILE_FOLDER}\covers";
        public static string COVER_CACHE_OUTPUT_FOLDER = $@"{IMPORT_FILE_FOLDER}\\covers\cache";

        public static string COVER_MAPPING_OUTPUT_FILE_PATH = @"C:\Users\davew\source\repos\forks\Nethereum.eShop\resources\data\TopRankBooks\BookCovers\BookCoverIndex.csv";

        public static string IPFS_URL = "https://ipfs.infura.io:5001";

        public static readonly string[] ImportFiles = new[] { 
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Part1.dat", 
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Part2.dat", 
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Part3.dat" };

        public static readonly string[] BookDescriptionFiles = new[] {
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Desc.Part1.dat",
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Desc.Part2.dat",
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Desc.Part3.dat" };

        public static WebClient webClient = new WebClient();

        private static int ipfsFilesLoaded = 0;

        public static async Task Main(string[] args)
        {
            CheckImportFiles(ImportFiles);

            /*

            await DownloadCoversAsync(books, COVER_CACHE_OUTPUT_FOLDER, RENAMED_COVER_OUTPUT_FOLDER);
            await UploadCoversToIpfsAsync(books, RENAMED_COVER_OUTPUT_FOLDER, COVER_MAPPING_OUTPUT_FILE_PATH, IPFS_URL);
            */

            var books = await LoadBooks(ImportFiles);
            var booksWithDescriptions = await AddBookDescriptions(books, BookDescriptionFiles);
            var bookCovers = GetBookCoverDictionary(COVER_MAPPING_OUTPUT_FILE_PATH);

            CatalogImport booksInCatalogForm = GenerateCatalogImport(booksWithDescriptions, bookCovers);

            using (var textWriter = File.CreateText(CATALOG_IMPORT_FILE_PATH))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(textWriter, booksInCatalogForm);
            }

            PrintSummary(booksWithDescriptions);


            throw new Exception("Check/change hardcoded config and uncomment code in Main method");
        }

        public class CatalogImport
        {
            public List<CatalogBrand> CatalogBrands { get; set; } = new List<CatalogBrand>();
            public List<CatalogType> CatalogTypes { get; set; } = new List<CatalogType>();
            public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
        }

        private static Dictionary<string, Dictionary<string, IpfsBookCoverMapping>> GetBookCoverDictionary(string bookCoverIndexFilePath)
        {
            var allMappings = new List<IpfsBookCoverMapping>();
            using (var textReader = File.OpenText(bookCoverIndexFilePath))
            {
                var csvHelper = new CsvHelper.CsvReader(textReader);
                csvHelper.Configuration.IgnoreQuotes = true;
                csvHelper.Configuration.HasHeaderRecord = true;
                allMappings.AddRange(csvHelper.GetRecords<IpfsBookCoverMapping>());
            }

            var dictionary = new Dictionary<string, Dictionary<string, IpfsBookCoverMapping>>(StringComparer.OrdinalIgnoreCase);

            foreach (var mapping in allMappings)
            {
                if (!dictionary.ContainsKey(mapping.Ean))
                {
                    dictionary.Add(mapping.Ean, new Dictionary<string, IpfsBookCoverMapping>());
                }

                dictionary[mapping.Ean][mapping.ImageSize] = mapping;
            }

            return dictionary;
        }

        private static CatalogImport GenerateCatalogImport(IEnumerable<BookWithDescription> books, Dictionary<string, Dictionary<string, IpfsBookCoverMapping>> bookCoverDictionary)
        {
            var excerpt = new CatalogImport();
            var bookCatalogType = new CatalogType { Id = 1, Type = "Book" };
            excerpt.CatalogTypes.Add(bookCatalogType);

            var brandDictionary = new Dictionary<string, CatalogBrand>(StringComparer.OrdinalIgnoreCase);

            int id = 0;
            int brandIdCounter = 0;
            foreach(var book in books)
            {
                id++;
                var item = Convert(bookCatalogType.Id, book, bookCoverDictionary, id, brandDictionary, ref brandIdCounter);
                excerpt.CatalogItems.Add(item);
            }

            excerpt.CatalogBrands = brandDictionary.Values.ToList();

            return excerpt;
        }

        private static CatalogItem Convert(
            int catalogTypeIdForBooks, 
            BookWithDescription book, 
            Dictionary<string, Dictionary<string, IpfsBookCoverMapping>> bookCoverDictionary, 
            int id, 
            Dictionary<string, CatalogBrand> brandDictionary, 
            ref int brandIdCounter)
        {

            if (!brandDictionary.ContainsKey(book.Book.PUB_NAME))
            {
                brandIdCounter++;
                brandDictionary[book.Book.PUB_NAME] = new CatalogBrand { Id = brandIdCounter, Brand = book.Book.PUB_NAME };
            }

            var brand = brandDictionary[book.Book.PUB_NAME];

            // a func to find the url for a book cover of a certain size
            Func<string, string, string> FindBookCoverUrl = (ean, size) =>
            {
                if (bookCoverDictionary.TryGetValue(book.Book.EAN, out var sizeMapping))
                {
                    if(sizeMapping.TryGetValue(size, out var details))
                    {
                        return details.IpfsUrl;
                    }
                }
                return string.Empty;
            };

            Func<string, int> ConvertInchesAsTextToMM = new Func<string, int>(i =>
            {
                return Decimal.TryParse(i, out var d) ? (int)(d * 25.4m) : 0;
            });

            var catalogItem = new CatalogItem {
                Id = id,
                Gtin = book.Book.EAN,
                CatalogTypeId = catalogTypeIdForBooks,
                CatalogBrandId = brand.Id,
                Name = book.Book.TITLE,
                Depth = ConvertInchesAsTextToMM(book.Book.DEPTH),
                Width = ConvertInchesAsTextToMM(book.Book.WIDTH),
                Height = ConvertInchesAsTextToMM(book.Book.LENGTH),
                Price = Decimal.TryParse(book.Book.PRICE, out var p) ? p : 0,

                PictureUri = FindBookCoverUrl(book.Book.EAN, "s"),
                PictureSmallUri = FindBookCoverUrl(book.Book.EAN, "s"),
                PictureMediumUri = FindBookCoverUrl(book.Book.EAN, "m"),
                PictureLargeUri = FindBookCoverUrl(book.Book.EAN, "l")
            };

            catalogItem.Description = book.BookDescription?.DESCRIPTION ?? string.Empty;

            var attributes = new Dictionary<string, string>();
            attributes.Add("Publication Date", book.Book.PUB_DATE);
            attributes.Add("Number Of Pages", book.Book.PAGE_NUM);
            attributes.Add("Subject Time", book.Book.SUBJECT_TIME);
            attributes.Add("Subject", book.Book.SUBJECT);
            attributes.Add("Age Min", book.Book.AGE_MIN);
            attributes.Add("Age Max", book.Book.AGE_MAX);
            attributes.Add("Adult Only", book.Book.ADULT_FLAG);
            attributes.Add("DEWEW_NUM Date", book.Book.DEWEW_NUM);
            attributes.Add("Public Domain", book.Book.PUBLIC_DOMAIN_FLAG);
            attributes.Add("Publication Country", book.Book.PUB_COUNTRY_CD);
            attributes.Add("US_RIGHTS_IND", book.Book.US_RIGHTS_IND);
            attributes.Add("BISAC_SUBJ_CD", book.Book.BISAC_SUBJ_CD);

            catalogItem.AttributeJson = JsonConvert.SerializeObject(attributes);


            return catalogItem;
            
        }

        private static void PrintSummary(IEnumerable<BookWithDescription> books)
        {
            Console.WriteLine($"Books:");

            foreach (var item in books)
            {
                var book = item.Book;
                Console.WriteLine($"EAN: {book.EAN}, COVER_S: {book.COVER_SMALL},  COVER_M: {book.COVER_MED},  COVER_L: {book.COVER_LARGE}");
                if (item.BookDescription != null)
                {
                    Console.WriteLine(item.BookDescription.DESCRIPTION?.Substring(0, Math.Min(item.BookDescription.DESCRIPTION.Length, 200)));
                }
            }

            Console.WriteLine($"Books: {books.Count()}");
        }

        /// <summary>
        /// Uploads book cover images to ipfs
        /// Writes a mapping csv file to relate ean, image size, ipfs hash etc
        /// Assumes the book cover images are held locally so....
        /// Ensure the cover images have been downloaded first! (DownloadCoversAsync)
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        public static async Task UploadCoversToIpfsAsync(IEnumerable<Book> books, string coverFolder, string indexOutputFilePath, string ipfsUrl)
        {
            ipfsFilesLoaded = 0;
            //indexOutputFilePath

            try
            {
                // we'll parallel load images - but don't want to write to the csv in parallel
                var semaphore = new SemaphoreSlim(1);

                using (var textWriter = File.CreateText(indexOutputFilePath))
                {
                    var csvWriter = new CsvHelper.CsvWriter(textWriter);
                    csvWriter.WriteHeader<IpfsBookCoverMapping>();
                    csvWriter.NextRecord();

                    try
                    {
                        var ipfsService = new IpfsService(ipfsUrl);
                        foreach (var book in books.AsParallel())
                        {
                            if (!string.IsNullOrWhiteSpace(book.COVER_SMALL))
                            {
                                var smallCover = await UploadCoverToIpfs(coverFolder, book.EAN, "s", book.COVER_SMALL, ipfsService);
                                await Save(smallCover);
                            }
                            if (!string.IsNullOrWhiteSpace(book.COVER_MED))
                            {
                                var mediumCover = await UploadCoverToIpfs(coverFolder, book.EAN, "m", book.COVER_MED, ipfsService);
                                await Save(mediumCover);
                            }
                            if (!string.IsNullOrWhiteSpace(book.COVER_LARGE))
                            {
                                var largeCover = await UploadCoverToIpfs(coverFolder, book.EAN, "l", book.COVER_LARGE, ipfsService);
                                await Save(largeCover);
                            }

                            async Task Save(IpfsBookCoverMapping mapping)
                            {
                                await semaphore.WaitAsync();
                                try
                                {
                                    csvWriter.WriteRecord(mapping);
                                    csvWriter.NextRecord();
                                }
                                finally
                                {
                                    semaphore.Release();
                                }
                            }
                        }

                    }
                    finally
                    {
                        await csvWriter.FlushAsync();
                        await textWriter.FlushAsync();
                    }
                }
            }
            finally
            {
                // consider changing file name on completion to avoid it being overwritten accidentally
            }
        }

        /// <summary>
        /// Finds the image in the local folder (using EAN and size) and uploads to ipfs
        /// </summary>
        /// <param name="localImageCacheFolder"></param>
        /// <param name="ean"></param>
        /// <param name="imageSize"></param>
        /// <param name="imageSourceUrl"></param>
        /// <param name="ipfsService"></param>
        /// <param name="retryNumber"></param>
        /// <returns></returns>
        private static async Task<IpfsBookCoverMapping> UploadCoverToIpfs(
            string localImageCacheFolder, string ean, string imageSize, string imageSourceUrl, IpfsService ipfsService, int retryNumber = 0)
        {
            string locallyCachedCopy = CreateLocalEanFilePath(ean, imageSize, imageSourceUrl, localImageCacheFolder);

            if (!File.Exists(locallyCachedCopy)) return null;

            ipfsFilesLoaded = Interlocked.Increment(ref ipfsFilesLoaded);

            Console.WriteLine($"uploading file to ipfs: {ean} {imageSize} ({ipfsFilesLoaded})");

            try
            {
                var node = await ipfsService.AddFileAsync(locallyCachedCopy);

                return new IpfsBookCoverMapping
                {
                    Ean = ean,
                    ImageSourceUrl = imageSourceUrl,
                    IpfsHash = node.Hash.ToString(),
                    IpfsUrl = ipfsService.GetUrl(node.Hash.ToString()),
                    ImageSize = imageSize,
                    EanUrl = Path.GetFileName(locallyCachedCopy),
                    FullEanUrl = locallyCachedCopy
                };
            }
            catch
            {
                retryNumber++;
                if (retryNumber == 3) throw;

                await Task.Delay(5000 * retryNumber);

                return await UploadCoverToIpfs(localImageCacheFolder, ean, imageSize, imageSourceUrl, ipfsService, retryNumber);
            }
        }

        /// <summary>
        /// Downloads the cover images for the books
        /// Saves a direct copy and a creates a replica with a new file name ({ean}_{size}{extension} e.g 9780007347742_l.jpg)
        /// </summary>
        /// <param name="books"></param>
        /// <param name="renamedImageOutputFolder"></param>
        /// <returns></returns>
        public static async Task DownloadCoversAsync(IEnumerable<Book> books, string localCacheOutputFolder, string renamedImageOutputFolder)
        {
            int count = 0;
            int total = books.Count();

            Console.WriteLine($"Downloading Covers For {total} books");

            foreach(var book in books.AsParallel())
            {
                count++;

                Console.WriteLine($"{count} of {total}");

                if (!string.IsNullOrWhiteSpace(book.COVER_SMALL))
                {
                    await DownloadBookCoverAsync(book.EAN, "s", book.COVER_SMALL, localCacheOutputFolder, renamedImageOutputFolder);
                }
                if (!string.IsNullOrWhiteSpace(book.COVER_MED))
                {
                    await DownloadBookCoverAsync(book.EAN, "m", book.COVER_MED, localCacheOutputFolder, renamedImageOutputFolder);
                }
                if (!string.IsNullOrWhiteSpace(book.COVER_LARGE))
                {
                    await DownloadBookCoverAsync(book.EAN, "l", book.COVER_LARGE, localCacheOutputFolder, renamedImageOutputFolder);
                }
            }
        }

        private static string CreateLocalEanFilePath(string ean, string size, string imageUrl, string outputFolder)
        {
            return Path.Combine(outputFolder, $"{ean}_{size}{Path.GetExtension(imageUrl)}");
        }

        private static async Task DownloadBookCoverAsync(string ean, string size, string imageUrl, string localCacheOutputFolder, string eanOutputFolder)
        {
            string renamedCopy = CreateLocalEanFilePath(ean, size, imageUrl, eanOutputFolder);

            if (File.Exists(renamedCopy)) return;

            string exactCopy = Path.Combine(localCacheOutputFolder, Path.GetFileName(imageUrl));

            if (!File.Exists(exactCopy))
            {
                await DownloadAsync(imageUrl, exactCopy);
            }

            File.Copy(exactCopy, renamedCopy, true);
        }


        private static async Task DownloadAsync(string imageUrl, string destinationFile, int retryNumber = 0)
        {

            try
            {
                await Task.Delay(500);
                Console.WriteLine($"Downloading Cover: {imageUrl}");
                await webClient.DownloadFileTaskAsync(imageUrl, destinationFile);
            }
            catch
            {
                if (retryNumber > 3) throw;
                retryNumber++;

                await Task.Delay(5000 * retryNumber);

                await DownloadAsync(imageUrl, destinationFile, retryNumber);
            }
        }

        /// <summary>
        /// Parses the pipe delimeted csv files of Top Ranking books
        /// </summary>
        /// <param name="importFiles"></param>
        /// <returns>An im-memory enumerable of books</returns>
        public static Task<IEnumerable<Book>> LoadBooks(string[] importFiles)
        {
            var books = new List<Book>();
            foreach (var file in importFiles)
            {
                using (var textReader = File.OpenText(file))
                {
                    var csvHelper = new CsvHelper.CsvReader(textReader);
                    csvHelper.Configuration.Delimiter = COLUMN_DELIMITER.ToString();
                    csvHelper.Configuration.IgnoreQuotes = true;
                    csvHelper.Configuration.HasHeaderRecord = true;
                    books.AddRange(csvHelper.GetRecords<Book>());
                }
            }

            return Task.FromResult((IEnumerable<Book>) books);
        }

        public static async Task<IEnumerable<BookWithDescription>> AddBookDescriptions(IEnumerable<Book> books, string[] descriptionImportFiles)
        {
            var bookWithDescriptions = new List<BookWithDescription>();
            var descriptions = await LoadBookDescriptions(descriptionImportFiles);

            foreach(var book in books)
            {
                bookWithDescriptions.Add(
                    new BookWithDescription(
                        book, 
                        descriptions.ContainsKey(book.EAN) ? descriptions[book.EAN] : null));
            }

            return bookWithDescriptions;
        }

        /// <summary>
        /// Parses the pipe delimeted csv files of Top Ranking books
        /// </summary>
        /// <param name="importFiles"></param>
        /// <returns>An im-memory enumerable of books</returns>
        public static Task<Dictionary<string, BookDescription>> LoadBookDescriptions(string[] importFiles)
        {
            var descriptions = new List<BookDescription>();
            foreach (var file in importFiles)
            {
                using (var textReader = File.OpenText(file))
                {
                    var csvHelper = new CsvHelper.CsvReader(textReader);
                    csvHelper.Configuration.Delimiter = COLUMN_DELIMITER.ToString();
                    csvHelper.Configuration.IgnoreQuotes = true;
                    csvHelper.Configuration.HasHeaderRecord = true;
                    descriptions.AddRange(csvHelper.GetRecords<BookDescription>());
                }
            }

            //there may be dupes in the import file
            //so we can't use the standard Linq ToDictionary extension
            //we'll just take the most recent record
            Dictionary<string, BookDescription> dictionary = new Dictionary<string, BookDescription>(StringComparer.OrdinalIgnoreCase);
            foreach(var description in descriptions)
            {
                dictionary[description.EAN] = description;
            }

            return Task.FromResult(dictionary);
        }

        /// <summary>
        /// Ensures the import files have the expected number of columns on each row
        /// Prints a report to the console
        /// </summary>
        public static void CheckImportFiles(string[] importFiles)
        {
            var columns = HEADERS.Split(COLUMN_DELIMITER);

            Console.WriteLine("Expected Columns");
            Console.WriteLine(HEADERS);
            Console.WriteLine($"Column Count: {columns.Length}");

            foreach (var file in importFiles)
            {
                Console.WriteLine();
                Console.WriteLine($"File: {Path.GetFileName(file)}");

                var problemLines = FindProblemsInImportFile(file, columns.Length, COLUMN_DELIMITER);

                Console.WriteLine($"Problem Lines: {problemLines.Count()}");

                foreach (var problemLine in problemLines)
                {
                    Console.WriteLine($"Line Number: {problemLine.lineNumber}, Column Count: {problemLine.columns}");
                    Console.WriteLine(problemLine.line);
                }
            }
        }

        public static IEnumerable<(int lineNumber, string line, int columns)> FindProblemsInImportFile(string importFile, int expectedColumnCount, char columnDelimeter)
        {
            var problemLines = new List<(int lineNumber, string line, int columns)>();

            int lineNumber = 0;
            using(var file = File.OpenText(importFile))
            {
                string line;
                while((line = file.ReadLine()) != null)
                {
                    lineNumber++;
                    var columnValues = line.Split(columnDelimeter);
                    if(columnValues.Length != expectedColumnCount)
                    {
                        problemLines.Add((lineNumber, line, columnValues.Length));
                    }
                }
            }

            return problemLines;
        }
    }

}
