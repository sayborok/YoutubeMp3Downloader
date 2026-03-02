using System.Threading.Tasks;

namespace YoutubeMp3Downloader
{
    /// <summary>
    /// Defines the contract for downloading audio tracks from YouTube.
    /// </summary>
    public interface IYoutubeAudioDownloader
    {
        /// <summary>
        /// Downloads all audio tracks from the specified playlist.
        /// </summary>
        /// <param name="playlistUrl">The YouTube playlist URL.</param>
        /// <param name="outputFolder">The directory path where downloaded files will be saved.</param>
        Task DownloadPlaylistAsync(string playlistUrl, string outputFolder);

        /// <summary>
        /// Downloads audio from a single YouTube video.
        /// </summary>
        /// <param name="videoUrl">The YouTube video URL.</param>
        /// <param name="outputFolder">The directory path where the downloaded file will be saved.</param>
        Task DownloadSingleAsync(string videoUrl, string outputFolder);

        /// <summary>
        /// Downloads audio from multiple YouTube URLs listed in a text file.
        /// Each line in the file should contain a single YouTube video or playlist URL.
        /// </summary>
        /// <param name="filePath">The path to the .txt file containing URLs.</param>
        /// <param name="outputFolder">The directory path where downloaded files will be saved.</param>
        Task DownloadFromFileAsync(string filePath, string outputFolder);
    }
}
