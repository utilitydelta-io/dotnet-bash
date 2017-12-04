using UtilityDelta.Bash.Implementation;

namespace UtilityDelta.Bash.Interface
{
    public interface IProcessFactory
    {
        ProcessWrapper GetProcess();
    }
}