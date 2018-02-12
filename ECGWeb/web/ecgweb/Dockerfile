FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY . /app
RUN mkdir -p /app/db
EXPOSE 5000
ENTRYPOINT ["dotnet", "ECGWeb.dll"]
