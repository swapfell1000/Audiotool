using FFMpegCore;
using System.IO;
using System.Windows;

namespace Audiotool;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App() {
        string ffmpegPath = Path.Combine(AppContext.BaseDirectory, "FFmpeg");

        GlobalFFOptions.Configure(new FFOptions { BinaryFolder = ffmpegPath, TemporaryFilesFolder = Path.Combine(ffmpegPath, "temp") });
    }
}