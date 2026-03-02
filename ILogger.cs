namespace YoutubeMp3Downloader
{
    /// <summary>
    /// Defines a contract for logging messages at different severity levels.
    /// </summary>
    public interface ILogger
    {
        void Info(string message);
        void Success(string message);
        void Error(string message);
        void Warning(string message);
    }
}
