using System.Diagnostics;

namespace UtilityDelta.Bash.Implementation
{
    public class BashOutput
    {
        public Process Process { get; set; }
        public bool IsSuccessful => Process.ExitCode == 0;
        public string StandardOutputText => Process.StandardOutput.ReadToEnd();
        public string StandardErrorText => Process.StandardError.ReadToEnd();
    }
}