# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /server
COPY . .
EXPOSE 5114
ENV ASPNETCORE_URLS=http://*:5114
ENTRYPOINT ["dotnet", "ToDoApi.dll"] #Change to name of actual backend