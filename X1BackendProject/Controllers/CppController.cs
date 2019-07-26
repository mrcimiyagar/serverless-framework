using System;
using System.IO;

namespace X1BackendProject.Controllers
{
    public class CppController : BaseController
    {
        public CppController(string appName, string sourceDirPath, string executableName) : base(appName, sourceDirPath)
        {
            File.AppendAllLines(
                $"{AppPath}/Dockerfile", 
                new string[] 
                {
                    "FROM gcc:4.9",
                    @"RUN apt-get update && apt-get install -y \
                      cmake \
                      dpkg-dev \
                      gcc \
                      make \
                      && rm -rf /var/lib/apt/lists/*",
                    "RUN apt remove -y --purge --auto-remove cmake",
                    @"RUN version=3.14; build=5; mkdir ~/temp; cd ~/temp; \
                    wget https://cmake.org/files/v$version/cmake-$version.$build.tar.gz; \
                    tar -xzvf cmake-$version.$build.tar.gz; \
                    cd cmake-$version.$build/; \
                    ./bootstrap; \
                    make -j4; \
                    make install; \
                    cmake --version;",
                    $"WORKDIR /{appName}",
                    "COPY . .",
                    $"RUN cmake -DCMAKE_BUILD_TYPE=Release -G \"CodeBlocks - Unix Makefiles\" /{appName}",
                    $"CMD [\"cmake-build-debug/{executableName}\"]"
                });
            
            foreach (var res in $"cd {AppPath}; docker build -t {AppName} .;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }
    }
}