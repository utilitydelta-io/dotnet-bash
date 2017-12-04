using System.Diagnostics;
using UtilityDelta.Bash.Interface;

namespace UtilityDelta.Bash.Implementation
{
    public class BashRunner : IBashRunner
    {
        private readonly IProcessFactory _process;

        public BashRunner(IProcessFactory process)
        {
            _process = process;
        }

        public Process RunCommand(string command, string workingDirectory, bool waitForFinish, int? waitingTimout,
            int? retryCountOnFailure)
        {
            var escapedArgs = command.Replace("\"", "\\\"");
            var process = StartProcess(escapedArgs, workingDirectory);

            if (!waitForFinish) return process.Process;

            WaitForExit(waitingTimout, process);

            var retryCount = retryCountOnFailure;
            while (process.ExitCode != 0 && retryCount > 0)
            {
                process = StartProcess(escapedArgs, workingDirectory);
                WaitForExit(waitingTimout, process);
                retryCount--;
            }

            return process.Process;
        }

        private static void WaitForExit(int? waitingTimout, ProcessWrapper process)
        {
            if (waitingTimout == null)
                process.WaitForExit();
            else
                process.WaitForExit(waitingTimout.Value);
        }

        private ProcessWrapper StartProcess(string command, string workingDirectory)
        {
            var process = _process.GetProcess();

            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"-c \"{command}\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WorkingDirectory = workingDirectory;

            process.Start();
            return process;
        }
    }
}