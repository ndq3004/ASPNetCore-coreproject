WORKDIR "/src/backend/src/NDQUAN.Auth.API"
RUN dotnet build "NDQUAN.Auth.API.csproj" -c Release -o  /app/build

FROM build as publish
RUN dotnet publish "NDQUAN.Auth.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:5.0 as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NDQUAN.Auth.API.dll"]