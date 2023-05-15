FROM bancopichinchaec.azurecr.io/bp/cross-iac/dotnet/dotnet-6-runtime-debian
COPY ./publish/. .
USER 1001
CMD ["dotnet", "WSMovimientos.API.dll"]