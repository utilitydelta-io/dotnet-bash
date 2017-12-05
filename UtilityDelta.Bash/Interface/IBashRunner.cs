using UtilityDelta.Bash.Implementation;

namespace UtilityDelta.Bash.Interface
{
    public interface IBashRunner
    {
        ProcessWrapper RunCommand(string command, string workingDirectory, bool waitForFinish, int? waitingTimout,
            int? retryCountOnFailure);
    }
}