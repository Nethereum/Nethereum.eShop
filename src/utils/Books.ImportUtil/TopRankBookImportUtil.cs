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

        public static string BASE_FOLDER = @"C:\Users\davew\Downloads\TopRankBooks_Fixed";
        public static string IMPORT_FILE_FOLDER = BASE_FOLDER;
        public static string RENAMED_COVER_OUTPUT_FOLDER = $@"{IMPORT_FILE_FOLDER}\covers";
        public static string COVER_CACHE_OUTPUT_FOLDER = $@"{IMPORT_FILE_FOLDER}\\covers\cache";
        public static string COVER_MAPPING_OUTPUT_FILE_PATH = $@"{IMPORT_FILE_FOLDER}\BookCoverIndex.csv";

        public static string IPFS_URL = "https://ipfs.infura.io:5001";

        public static readonly string[] ImportFiles = new[] { 
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Part1.dat", 
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Part2.dat", 
            $@"{IMPORT_FILE_FOLDER}\Top_Rank_Books.Part3.dat" };

        public static WebClient webClient = new WebClient();

        private static int ipfsFilesLoaded = 0;

        public static async Task Main(string[] args)
        {
            /*
            CheckImportFiles(ImportFiles);
            var books = await LoadBooks(ImportFiles);

            PrintSummary(books);

            await DownloadCoversAsync(books, COVER_CACHE_OUTPUT_FOLDER, RENAMED_COVER_OUTPUT_FOLDER);
            await UploadCoversToIpfsAsync(books, RENAMED_COVER_OUTPUT_FOLDER, COVER_MAPPING_OUTPUT_FILE_PATH, IPFS_URL);
            */

            throw new Exception("Check/change hardcoded config and uncomment code in Main method");
        }

        private static void PrintSummary(IEnumerable<Book> books)
        {
            Console.WriteLine($"Books:");

            foreach (var book in books)
            {
                Console.WriteLine($"EAN: {book.EAN}, COVER_S: {book.COVER_SMALL},  COVER_M: {book.COVER_MED},  COVER_L: {book.COVER_LARGE}");
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
