FROM fsharp
FROM mcr.microsoft.com/dotnet/sdk:5.0
EXPOSE 80
WORKDIR /app
COPY . /app
CMD dotnet run --project FPPrank.fsproj