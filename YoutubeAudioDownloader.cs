using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos.Streams;

namespace YoutubeMp3Downloader
{
    /// <summary>
    /// Downloads audio tracks from YouTube using the YoutubeExplode library.
    /// Implements <see cref="IYoutubeAudioDownloader"/>.
    /// </summary>
    public class YoutubeAudioDownloader : IYoutubeAudioDownloader
    {
        private readonly YoutubeClient _youtubeClient;
        private readonly ILogger _logger;

        public YoutubeAudioDownloader(YoutubeClient youtubeClient, ILogger logger)
        {
            _youtubeClient = youtubeClient;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task DownloadPlaylistAsync(string playlistUrl, string outputFolder)
        {
            Directory.CreateDirectory(outputFolder);

            var videos = await _youtubeClient.Playlists.GetVideosAsync(playlistUrl);
            int count = 0;

            foreach (var video in videos)
            {
                count++;
                _logger.Info($"[{count}] Processing: {video.Title}");
                await DownloadVideoByIdAsync(video.Id, video.Title, outputFolder);
            }

            _logger.Success($"Playlist completed. {count} track(s) processed.");
        }

        /// <inheritdoc />
        public async Task DownloadSingleAsync(string videoUrl, string outputFolder)
        {
            Directory.CreateDirectory(outputFolder);

            var video = await _youtubeClient.Videos.GetAsync(videoUrl);
            _logger.Info($"Processing: {video.Title}");
            await DownloadVideoByIdAsync(video.Id, video.Title, outputFolder);
        }

        /// <inheritdoc />
        public async Task DownloadFromFileAsync(string filePath, string outputFolder)
        {
            if (!File.Exists(filePath))
            {
                _logger.Error($"File not found: {filePath}");
                return;
            }

            Directory.CreateDirectory(outputFolder);

            var lines = File.ReadAllLines(filePath)
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();

            if (lines.Length == 0)
            {
                _logger.Warning("The file is empty. No URLs to process.");
                return;
            }

            _logger.Info($"Found {lines.Length} URL(s) in file.");

            for (int i = 0; i < lines.Length; i++)
            {
                string url = lines[i];
                _logger.Info($"[{i + 1}/{lines.Length}] Processing URL: {url}");

                try
                {
                    if (IsPlaylistUrl(url))
                    {
                        await DownloadPlaylistAsync(url, outputFolder);
                    }
                    else
                    {
                        await DownloadSingleAsync(url, outputFolder);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"Failed to process URL: {url} — {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Downloads the highest bitrate audio stream for a given video and saves it as MP3.
        /// </summary>
        private async Task DownloadVideoByIdAsync(string videoId, string title, string outputFolder)
        {
            var manifest = await _youtubeClient.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            if (streamInfo == null)
            {
                _logger.Warning($"No audio stream found for: {title}");
                return;
            }

            string fileName = SanitizeFileName(title) + ".mp3";
            string filePath = Path.Combine(outputFolder, fileName);

            await _youtubeClient.Videos.Streams.DownloadAsync(streamInfo, filePath);
            _logger.Success($"Downloaded: {title}");
        }

        /// <summary>
        /// Removes invalid file name characters from the given title.
        /// </summary>
        private static string SanitizeFileName(string title)
        {
            return string.Concat(title.Split(Path.GetInvalidFileNameChars()));
        }

        /// <summary>
        /// Determines whether the given URL is a YouTube playlist URL.
        /// </summary>
        private static bool IsPlaylistUrl(string url)
        {
            return url.Contains("playlist") || url.Contains("list=");
        }
    }
}
