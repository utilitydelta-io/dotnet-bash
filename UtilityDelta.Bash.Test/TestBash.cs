using System.Diagnostics;
using Moq;
using UtilityDelta.Bash.Implementation;
using UtilityDelta.Bash.Interface;
using Xunit;

namespace UtilityDelta.Bash.Test
{
    public class TestBash
    {
        [Fact]
        public void TestFailure()
        {
            var processFactory = new Mock<IProcessFactory>();
            var mockProcess = new Mock<ProcessWrapper>();
            mockProcess.Setup(x => x.ExitCode).Returns(0);
            var startInfo = new ProcessStartInfo();
            mockProcess.Setup(x => x.StartInfo).Returns(startInfo);
            processFactory.Setup(x => x.GetProcess()).Returns(mockProcess.Object);

            mockProcess.Setup(x => x.ExitCode).Returns(1);

            var service = new BashRunner(processFactory.Object);
            var processResult = service.RunCommand("test-command blah -k \"zz\"", "/working/path", true, 33, 10);


            Assert.Equal("/bin/bash", startInfo.FileName);
            Assert.Equal("/working/path", startInfo.WorkingDirectory);
            Assert.Equal("-c \"test-command blah -k \\\"zz\\\"\"", startInfo.Arguments);

            Assert.True(startInfo.RedirectStandardError);
            Assert.True(startInfo.RedirectStandardOutput);
            Assert.True(startInfo.CreateNoWindow);

            Assert.False(startInfo.RedirectStandardInput);
            Assert.False(startInfo.UseShellExecute);

            mockProcess.Verify(x => x.Start(), Times.Exactly(11));
            mockProcess.Verify(x => x.WaitForExit(33), Times.Exactly(11));
        }

        [Fact]
        public void TestFireAndForget()
        {
            var processFactory = new Mock<IProcessFactory>();
            var mockProcess = new Mock<ProcessWrapper>();
            mockProcess.Setup(x => x.ExitCode).Returns(0);
            var startInfo = new ProcessStartInfo();
            mockProcess.Setup(x => x.StartInfo).Returns(startInfo);
            processFactory.Setup(x => x.GetProcess()).Returns(mockProcess.Object);

            mockProcess.Setup(x => x.ExitCode).Returns(0);

            var service = new BashRunner(processFactory.Object);
            var processResult = service.RunCommand("test-command blah -k \"zz\"", "/working/path", false, null, null);

            Assert.Equal("/bin/bash", startInfo.FileName);
            Assert.Equal("/working/path", startInfo.WorkingDirectory);
            Assert.Equal("-c \"test-command blah -k \\\"zz\\\"\"", startInfo.Arguments);

            Assert.True(startInfo.RedirectStandardError);
            Assert.True(startInfo.RedirectStandardOutput);
            Assert.True(startInfo.CreateNoWindow);

            Assert.False(startInfo.RedirectStandardInput);
            Assert.False(startInfo.UseShellExecute);

            mockProcess.Verify(x => x.Start(), Times.Exactly(1));
            mockProcess.Verify(x => x.WaitForExit(It.IsAny<int>()), Times.Never);
            mockProcess.Verify(x => x.WaitForExit(), Times.Never);
        }

        [Fact]
        public void TestNormalExecution()
        {
            var processFactory = new Mock<IProcessFactory>();
            var mockProcess = new Mock<ProcessWrapper>();
            mockProcess.Setup(x => x.ExitCode).Returns(0);
            var startInfo = new ProcessStartInfo();
            mockProcess.Setup(x => x.StartInfo).Returns(startInfo);
            processFactory.Setup(x => x.GetProcess()).Returns(mockProcess.Object);


            var service = new BashRunner(processFactory.Object);
            var processResult = service.RunCommand("test-command blah -k \"zz\"", "/working/path", true, 33, 10);


            Assert.Equal("/bin/bash", startInfo.FileName);
            Assert.Equal("/working/path", startInfo.WorkingDirectory);
            Assert.Equal("-c \"test-command blah -k \\\"zz\\\"\"", startInfo.Arguments);

            Assert.True(startInfo.RedirectStandardError);
            Assert.True(startInfo.RedirectStandardOutput);
            Assert.True(startInfo.CreateNoWindow);

            Assert.False(startInfo.RedirectStandardInput);
            Assert.False(startInfo.UseShellExecute);

            mockProcess.Verify(x => x.Start(), Times.Once);
            mockProcess.Verify(x => x.WaitForExit(33), Times.Once);
        }
    }
}