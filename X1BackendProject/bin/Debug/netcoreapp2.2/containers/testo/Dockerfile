FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as base
WORKDIR /
EXPOSE 80
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
COPY *.sln .
COPY /TestCsharpSolution/*.csproj ./TestCsharpSolution/
COPY /SharedArea/*.csproj ./SharedArea/
RUN dotnet restore
COPY . .
RUN dir
RUN dotnet publish -c Release -o output
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /
COPY . .
RUN dir
FROM base AS final
WORKDIR /TestCsharpSolution
COPY --from=publish /TestCsharpSolution .
RUN dir
ENTRYPOINT ["dotnet", "TestCsharpSolution.dll"]
