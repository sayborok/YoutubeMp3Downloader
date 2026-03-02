using System;
using System.Threading.Tasks;
using YoutubeExplode;

namespace YoutubeMp3Downloader
{
    internal class Program
    {
        private const string OutputFolder = "DownloadedMusic";

        static async Task Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            var youtubeClient = new YoutubeClient();
            IYoutubeAudioDownloader downloader = new YoutubeAudioDownloader(youtubeClient, logger);

            ShowMenu();
            string choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1":
                        await HandleSingleDownload(downloader, logger);
                        break;

                    case "2":
                        await HandlePlaylistDownload(downloader, logger);
                        break;

                    case "3":
                        await HandleFileDownload(downloader, logger);
                        break;

                    default:
                        logger.Error("Invalid option. Please select 1, 2, or 3.");
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred: {ex.Message}");
            }

            WaitForExit();
        }

        private static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║     YouTube Mp3 Downloader v1.0     ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  [1] Download single video          ║");
            Console.WriteLine("║  [2] Download playlist              ║");
            Console.WriteLine("║  [3] Download from .txt file        ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("\nSelect an option: ");
        }

        private static async Task HandleSingleDownload(IYoutubeAudioDownloader downloader, ILogger logger)
        {
            Console.Write("Enter video URL: ");
            string url = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                logger.Error("URL cannot be empty.");
                return;
            }

            await downloader.DownloadSingleAsync(url, OutputFolder);
            logger.Success("Download completed.");
        }

        private static async Task HandlePlaylistDownload(IYoutubeAudioDownloader downloader, ILogger logger)
        {
            Console.Write("Enter playlist URL: ");
            string url = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                logger.Error("URL cannot be empty.");
                return;
            }

            await downloader.DownloadPlaylistAsync(url, OutputFolder);
            logger.Success("All downloads completed.");
        }

        private static async Task HandleFileDownload(IYoutubeAudioDownloader downloader, ILogger logger)
        {
            Console.Write("Enter .txt file path: ");
            string filePath = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                logger.Error("File path cannot be empty.");
                return;
            }

            await downloader.DownloadFromFileAsync(filePath, OutputFolder);
            logger.Success("All downloads completed.");
        }

        private static void WaitForExit()
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
