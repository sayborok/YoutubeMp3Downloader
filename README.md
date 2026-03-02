# YouTube Mp3 Downloader 🎵

A lightweight console application that downloads audio tracks from YouTube and saves them as MP3 files.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-purple)
![License](https://img.shields.io/badge/License-MIT-green)

## Features

- 🎶 **Single Video** — Download audio from any YouTube video URL
- 📋 **Playlist** — Download an entire YouTube playlist at once
- 📄 **Batch (.txt)** — Load multiple URLs from a text file and download all
- 🔊 Automatically selects the highest bitrate audio stream
- 📁 Saves files with sanitized, clean filenames
- 🎨 Color-coded console output for easy progress tracking

## Prerequisites

- [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) or later
- [Visual Studio 2019+](https://visualstudio.microsoft.com/) (recommended) or MSBuild

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/sayborok/YoutubeMp3Downloader.git
cd YoutubeMp3Downloader
```

### Build

Open `YoutubeMp3Downloader.slnx` in Visual Studio and build, or use the command line:

```bash
msbuild YoutubeMp3Downloader.csproj /t:Restore;Build /p:Configuration=Release
```

### Run

```bash
.\bin\Release\YoutubeMp3Downloader.exe
```

## Usage

When you launch the application, you'll see an interactive menu:

```
╔══════════════════════════════════════╗
║     YouTube Mp3 Downloader v1.0     ║
╠══════════════════════════════════════╣
║  [1] Download single video          ║
║  [2] Download playlist              ║
║  [3] Download from .txt file        ║
╚══════════════════════════════════════╝

Select an option:
```

### Option 1 — Single Video
Paste a YouTube video URL to download its audio as MP3.

### Option 2 — Playlist
Paste a YouTube playlist URL to download all tracks.

### Option 3 — Batch from .txt
Provide the path to a `.txt` file where each line contains a YouTube URL (video or playlist). The app will process all of them sequentially.

**Example `urls.txt`:**
```
https://www.youtube.com/watch?v=dQw4w9WgXcQ
https://www.youtube.com/watch?v=abcdefghijk
https://www.youtube.com/playlist?list=PLxxxxxxx
```

Downloaded MP3 files will be saved to the `DownloadedMusic/` folder.

## Project Structure

```
YoutubeMp3Downloader/
├── Program.cs                    # Application entry point & menu
├── IYoutubeAudioDownloader.cs    # Download service interface
├── YoutubeAudioDownloader.cs     # Download service implementation
├── ILogger.cs                    # Logger interface
├── ConsoleLogger.cs              # Console logger implementation
├── Properties/
│   └── AssemblyInfo.cs
├── App.config
└── YoutubeMp3Downloader.csproj
```

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| [YoutubeExplode](https://github.com/Tyrrrz/YoutubeExplode) | 6.5.7 | YouTube data extraction and stream download |
| [AngleSharp](https://github.com/AngleSharp/AngleSharp) | 1.4.0 | HTML parsing (YoutubeExplode dependency) |

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

## Author

Developed by [sayborok](https://github.com/sayborok)
