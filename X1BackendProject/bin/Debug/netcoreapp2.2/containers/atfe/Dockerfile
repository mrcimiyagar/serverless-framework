FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /
COPY *.sln .
COPY /TestCsharpSolution/*.csproj ./TestCsharpSolution/
COPY /SharedArea/*.csproj ./SharedArea/
RUN dotnet restore
COPY . .
RUN dir
RUN dotnet publish --self-contained --runtime ubuntu-x64 -c Release -o output
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /
COPY . .
RUN dir
WORKDIR /TestCsharpSolution/bin/Release/netcoreapp2.2
RUN dir
ENTRYPOINT ["dotnet", "TestCsharpSolution.dll"]
