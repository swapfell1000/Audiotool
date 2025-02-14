using System.Collections.ObjectModel;
using System.IO;
using Audiotool.model;
using FFMpegCore;

namespace Audiotool.Converters;

public static class WavConverter
{
    public static void ConvertToWav(ObservableCollection<Audio> audioFiles, string outputFolder)
    {   
        foreach (Audio audio in audioFiles)
        {
            if (audio.FileExtension != "wav")
            {
                string outputPath = Path.Combine(outputFolder, $"{audio.FileName}.wav");
                FFMpegArguments ff = FFMpegArguments
                    .FromFileInput(audio.FilePath);
                _ = ff.OutputToFile(outputPath, true, opt =>
                {
                    opt.WithAudioSamplingRate(audio.SampleRate)
                        .WithoutMetadata()
                        .WithCustomArgument("-fflags +bitexact -flags:v +bitexact -flags:a +bitexact")
                        .WithAudioCodec("pcm_s16le")
                        .ForceFormat("wav")
                        .UsingMultithreading(true);
                    if (audio.Channels != 1)
                        opt.WithCustomArgument("-ac 1");
                }).ProcessSynchronously();

                audio.FileSize = (ulong)new FileInfo(outputPath).Length; //; (long)(info.PrimaryAudioStream.BitRate * info.Duration.TotalSeconds * info.PrimaryAudioStream.Channels);
            }
        }
    }
}