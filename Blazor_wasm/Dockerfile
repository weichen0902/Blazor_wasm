#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
#WORKDIR /app
#COPY . ./
#RUN dotnet publish -c Release -o /app/output
#
#FROM nginx:alpine
#WORKDIR /usr/share/nginx/html
#COPY --from=build-env /app/output/wwwroot .

# 建置階段：使用 .NET 8 SDK 來建置 Blazor WebAssembly 專案
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Blazor_wasm.csproj", "./"]
RUN dotnet restore "Blazor_wasm.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish \
    -p:BlazorWebAssemblyEnableLinking=false \
    -p:PublishTrimmed=false \
    -p:UseAppHost=false

# 發佈階段：部署靜態檔案到 nginx
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=build /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]