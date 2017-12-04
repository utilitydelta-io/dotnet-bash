using System.Diagnostics;

namespace UtilityDelta.Bash.Interface
{
    public interface IBashRunner
    {
        Process RunCommand(string command, string workingDirectory, bool waitForFinish, int? waitingTimout,
            int? retryCountOnFailure);
    }
}