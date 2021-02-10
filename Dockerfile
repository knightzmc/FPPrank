FROM fsharp
FROM mcr.microsoft.com/dotnet/sdk:5.0
EXPOSE 8080
COPY . /app
WORKDIR /app/
CMD dotnet run FPPrank.fsproj