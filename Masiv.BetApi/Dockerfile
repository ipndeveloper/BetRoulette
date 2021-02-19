#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

WORKDIR /build
COPY *.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c $BUILDCONFIG -o out


FROM mcr.microsoft.com/dotnet/core/sdk:3.1



ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_ENVIRONMENT="Development"
EXPOSE 5000

WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "Masiv.BetApi.dll"] 

