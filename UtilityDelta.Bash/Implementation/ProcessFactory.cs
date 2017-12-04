using UtilityDelta.Bash.Interface;

namespace UtilityDelta.Bash.Implementation
{
    public class ProcessFactory : IProcessFactory
    {
        public ProcessWrapper GetProcess()
        {
            return new ProcessWrapper();
        }
    }
}