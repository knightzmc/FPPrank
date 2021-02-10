FROM fsharp
FROM mcr.microsoft.com/dotnet/sdk:5.0
EXPOSE 8080
COPY . /app
WORKDIR /app/
RUN dotnet run FPPrank.fsproj