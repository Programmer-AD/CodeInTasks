using System.Diagnostics;
using System.Text;
using CodeInTasks.Builder.Infrastructure.Interfaces;

namespace CodeInTasks.Builder.Infrastructure.Docker
{
    internal class ProcessRunner : IProcessRunner
    {
        public async Task<ProcessRunnerResult> RunProcessAsync(ProcessRunnerArguments arguments)
        {
            var startInfo = GetStartInfo(arguments);
            using var process = new Process { StartInfo = startInfo };

            var exitedNormaly = await RunProcessAsync(process, arguments.Timeout);

            var result = await MakeResultAsync(process, exitedNormaly);
            return result;
        }

        private static ProcessStartInfo GetStartInfo(ProcessRunnerArguments arguments)
        {
            var startInfo = GetBaseStartInfo();

            startInfo.FileName = arguments.FileName;
            startInfo.Arguments = arguments.Arguments;

            if (arguments.WorkingDirectory != null)
            {
                startInfo.WorkingDirectory = arguments.WorkingDirectory;
            }

            return startInfo;
        }

        private static async Task<bool> RunProcessAsync(Process process, TimeSpan timeout)
        {
            using var cancellationSource = new CancellationTokenSource(timeout);

            process.Start();

            try
            {
                await process.WaitForExitAsync(cancellationSource.Token);

                return true;
            }
            catch (OperationCanceledException)
            {
                process.Kill(entireProcessTree: true);

                return false;
            }
        }

        private static async Task<ProcessRunnerResult> MakeResultAsync(Process process, bool exitedNormaly)
        {
            var outputStreamReadingTask = process.StandardOutput.ReadToEndAsync();
            var errorStreamReadingTask = process.StandardError.ReadToEndAsync();

            var streamTexts = await Task.WhenAll(outputStreamReadingTask, errorStreamReadingTask);
            var streamOutputText = string.Join(Environment.NewLine, streamTexts);

            var result = new ProcessRunnerResult()
            {
                ExitCode = process.ExitCode,
                IsKilled = !exitedNormaly,
                RunTime = process.TotalProcessorTime,
                StreamOutputText = streamOutputText
            };

            return result;
        }

        private static ProcessStartInfo GetBaseStartInfo()
        {
            return new()
            {
                CreateNoWindow = true,
                UseShellExecute = false,

                RedirectStandardError = true,
                StandardErrorEncoding = Encoding.UTF8,

                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8,
            };
        }
    }
}
