FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as build-image
WORKDIR /
EXPOSE 80
COPY *.sln .
COPY /TestCsharpSolution/*.csproj ./TestCsharpSolution/
COPY /SharedArea/*.csproj ./SharedArea/
RUN dotnet restore
COPY . .
WORKDIR /
RUN dir
RUN dotnet publish -c Release TestCsharpSolution
WORKDIR /TestCsharpSolution/bin/Release/netcoreapp2.2/publish
RUN dir
ENTRYPOINT ["dotnet", "TestCsharpSolution.dll"]
