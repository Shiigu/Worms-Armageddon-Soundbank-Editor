using NAudio.Vorbis;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Worms_Soundbank_Editor.Utils
{
    public static class WavFileUtils
    {
        public static void PlaySound(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                SystemSounds.Beep.Play();
            }
            else
            {
                using (var soundPlayer = new SoundPlayer())
                {
                    try
                    {
                        using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                        { 
                            soundPlayer.Stream = fileStream;
                            soundPlayer.Play();
                            fileStream.Close();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Whoops!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        public static TimeSpan GetWavFileDuration(string path)
        {
            try
            {
                using (WaveFileReader wf = new WaveFileReader(path))
                    return wf.TotalTime;
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }

        public static void ConvertToWav(string inPath, string outPath)
        {
            switch (Path.GetExtension(inPath).ToLowerInvariant())
            {
                case ".wav":
                    File.Copy(inPath, outPath, true);
                    break;
                case ".mp3":
                    _convertMp3ToWav(inPath, outPath);
                    break;
                case ".aiff":
                    _convertAiffToWav(inPath, outPath);
                    break;
                case ".ogg":
                    _convertOggToWav(inPath, outPath);
                    break;
            }
        }

        public static void To16Bit(string path)
        {
            using (var waveFileReader = new WaveFileReader(path))
            {
                var convertedSound = string.Concat(Path.GetDirectoryName(path), "/placeholder.wav");
                var waveFormat = new WaveFormat(44100, 16, 1);
                WaveFileWriter.CreateWaveFile(convertedSound, new WaveFormatConversionStream(waveFormat, waveFileReader));
                waveFileReader.Close();
                File.Copy(convertedSound, path, true);
                File.Delete(convertedSound);
            }
        }

        public static void Concatenate(string outputFile, IEnumerable<string> sourceFiles)
        {
            byte[] buffer = new byte[1024];
            WaveFileWriter waveFileWriter = null;

            try
            {
                foreach (string sourceFile in sourceFiles)
                {
                    using (WaveFileReader reader = new WaveFileReader(sourceFile))
                    {
                        if (waveFileWriter == null)
                        {
                            waveFileWriter = new WaveFileWriter(outputFile, reader.WaveFormat);
                        }
                        else
                        {
                            if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                            {
                                throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                            }
                        }

                        int read;
                        while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            waveFileWriter.Write(buffer, 0, read);
                        }
                    }
                }
            }
            finally
            {
                if (waveFileWriter != null)
                {
                    waveFileWriter.Dispose();
                }
            }
        }

        private static void _convertMp3ToWav(string inPath, string outPath)
        {
            using (var reader = new Mp3FileReader(inPath))
            {
                using (var pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                {
                    WaveFileWriter.CreateWaveFile(outPath, pcmStream);
                }
            }
        }

        private static void _convertAiffToWav(string inPath, string outPath)
        {
            using (var reader = new AiffFileReader(inPath))
            {
                using (var pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                {
                    WaveFileWriter.CreateWaveFile(outPath, pcmStream);
                }
            }
        }

        private static void _convertOggToWav(string inPath, string outPath)
        {
            using (var reader = new VorbisWaveReader(inPath))
            {
                var waveProvider16 = reader.ToWaveProvider16();
                WaveFileWriter.CreateWaveFile(outPath, waveProvider16);
            }
        }
    }
}