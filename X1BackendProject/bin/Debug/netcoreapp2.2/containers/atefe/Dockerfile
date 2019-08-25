FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as base
WORKDIR /
EXPOSE 80
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /
COPY *.sln .
COPY /TestCsharpSolution/*.csproj ./TestCsharpSolution/
COPY /SharedArea/*.csproj ./SharedArea/
RUN dotnet restore
COPY . .
RUN dir
RUN dotnet publish -c Release TestCsharpSolution
RUN dir
WORKDIR /
FROM base AS final
COPY --from=build-env /TestCsharpSolution/bin/Release/netcoreapp2.2/publish .
RUN dir
ENTRYPOINT ["dotnet", "TestCsharpSolution.dll"]
