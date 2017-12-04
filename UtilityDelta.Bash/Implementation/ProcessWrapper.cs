using System.Diagnostics;

namespace UtilityDelta.Bash.Implementation
{
    public class ProcessWrapper
    {
        private readonly Process _process = new Process();

        public virtual Process Process => _process;
        public virtual int ExitCode => _process.ExitCode;
        public virtual ProcessStartInfo StartInfo => _process.StartInfo;

        public virtual bool Start()
        {
            return _process.Start();
        }

        public virtual void WaitForExit()
        {
            _process.WaitForExit();
        }

        public virtual void WaitForExit(int milliseconds)
        {
            _process.WaitForExit(milliseconds);
        }
    }
}