using System.Collections.Generic;
using System.Diagnostics;

namespace X1BackendProject
{
    public static class ShellHelper
    {
        public static IEnumerable<char> Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            while (!process.HasExited)
            {
                yield return (char)process.StandardOutput.Read();   
            }
        }
    }
}