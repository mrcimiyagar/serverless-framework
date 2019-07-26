using System;
using System.Diagnostics;
using System.IO;
using X1BackendProject.Controllers;

namespace X1BackendProject
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("enter app name : ");
                var appName = Console.ReadLine();
                if (string.IsNullOrEmpty(appName))
                {
                    Console.WriteLine("app name can not be empty.");
                    break;
                } 
                var appPath = $"{Directory.GetCurrentDirectory()}/containers/{appName}";
                if (Directory.Exists(appPath)) Directory.Delete(appPath, true);
                
                Console.Write("enter source folder path : ");
                var srcPath = Console.ReadLine();
                if (string.IsNullOrEmpty(srcPath))
                {
                    Console.WriteLine("source path can not be empty.");
                    break;
                }
                
                Console.Write("enter stack name : ");
                var stackName = Console.ReadLine();
                if (string.IsNullOrEmpty(stackName))
                {
                    Console.WriteLine("stack name can not be empty.");
                    break;
                }

                switch (stackName)
                {
                    case "java":
                    {
                        Console.Write("enter entry class name : ");
                        var entryClassName = Console.ReadLine();
                        if (string.IsNullOrEmpty(entryClassName))
                        {
                            Console.WriteLine("entry class name can not be empty.");
                            break;
                        }
                        var con = new JavaController(appName, srcPath, entryClassName);
                        Console.WriteLine(con.Run());
                        break;
                    }

                    case "dotnet":
                    {
                        Console.Write("enter project name : ");
                        var projectName = Console.ReadLine();
                        if (string.IsNullOrEmpty(projectName))
                        {
                            Console.WriteLine("project name can not be empty.");
                            break;
                        }
                        Console.Write("enter dll name : ");
                        var dllName = Console.ReadLine();
                        if (string.IsNullOrEmpty(dllName))
                        {
                            Console.WriteLine("dll name can not be empty.");
                            break;
                        }
                
                        var con = new DotnetController(appName, srcPath, projectName, dllName);
                        foreach (var res in con.Run()) Console.Write(res); 
                        break;
                    }

                    case "asp":
                    {
                        Console.Write("enter project name : ");
                        var projectName = Console.ReadLine();
                        if (string.IsNullOrEmpty(projectName))
                        {
                            Console.WriteLine("project name can not be empty.");
                            break;
                        }
                        Console.Write("enter dll name : ");
                        var dllName = Console.ReadLine();
                        if (string.IsNullOrEmpty(dllName))
                        {
                            Console.WriteLine("dll name can not be empty.");
                            break;
                        }
                
                        var con = new AspDotnetController(appName, srcPath, projectName, dllName);
                        foreach (var res in con.RunWebsite(4999)) Console.Write(res); 
                        break;
                    }

                    case "nodejs":
                    {
                        Console.Write("enter driver file path : ");
                        var driverFilePath = Console.ReadLine();
                        if (string.IsNullOrEmpty(driverFilePath))
                        {
                            Console.WriteLine("driver file name can not be empty.");
                            break;
                        }
                
                        var con = new NodejsController(appName, srcPath, driverFilePath);
                        foreach (var res in con.RunWebsite(3000)) Console.Write(res); 
                        break;
                    }

                    case "python":
                    {
                        Console.Write("enter driver file path : ");
                        var driverFilePath = Console.ReadLine();
                        if (string.IsNullOrEmpty(driverFilePath))
                        {
                            Console.WriteLine("driver file name can not be empty.");
                            break;
                        }
                        
                        Console.Write("enter library names separated by comma : ");
                        var libNames = Console.ReadLine() ?? "";

                        var libsArr = libNames.Split(",");
                        for (var counter = 0; counter < libsArr.Length; counter++)
                        {
                            libsArr[counter] = libsArr[counter].Trim();
                        }
                        
                        var con = new PythonController(appName, srcPath, libsArr, driverFilePath);
                        foreach (var res in con.Run()) Console.Write(res); 
                        break;
                    }

                    case "c":
                    {
                        Console.Write("enter executable file name : ");
                        var execFileName = Console.ReadLine();
                        if (string.IsNullOrEmpty(execFileName))
                        {
                            Console.WriteLine("executable file name can not be empty.");
                            break;
                        }
                        
                        var con = new CController(appName, srcPath, execFileName);
                        foreach (var res in con.Run()) Console.Write(res); 
                        break;
                    }

                    case "c++":
                    {
                        Console.Write("enter executable file name : ");
                        var execFileName = Console.ReadLine();
                        if (string.IsNullOrEmpty(execFileName))
                        {
                            Console.WriteLine("executable file name can not be empty.");
                            break;
                        }
                        
                        var con = new CppController(appName, srcPath, execFileName);
                        foreach (var res in con.Run()) Console.Write(res);
                        break;
                    }

                    case "php":
                    {
                        var con = new PhpController(appName, srcPath);
                        foreach (var res in con.RunWebsite(8085)) Console.Write(res);
                        break;
                    }

                    case "go":
                    {

                        break;
                    }

                    default:
                    {

                        break;
                    }
                }

                Console.WriteLine("process finished.");
            }
        }
    }
}