# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /server
COPY . .
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80
ENTRYPOINT ["dotnet", "estate_emporium.dll"] #Change to name of actual backend