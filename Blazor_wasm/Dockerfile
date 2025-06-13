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

# 複製專案檔案並還原相依性
COPY ["Blazor_wasm/Blazor_wasm.csproj", "Blazor_wasm/"]
RUN dotnet restore "Blazor_wasm/Blazor_wasm.csproj"

# 複製全部原始碼
COPY . .

# 發佈專案（Release 模式），產出靜態檔案
WORKDIR "/src/Blazor_wasm"
RUN dotnet publish -c Debug -o /app/publish \
    -p:BlazorWebAssemblyEnableLinking=false \
    -p:PublishTrimmed=false \
    -p:UseAppHost=false

# 發佈階段：用輕量級 nginx 容器來部署靜態檔案
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# 刪除 nginx 預設檔案
RUN rm -rf ./*

# 從建置階段複製已發佈的靜態檔案（wwwroot）到 nginx 靜態資料夾
COPY --from=build /app/publish/wwwroot .

# 若需要 SPA 支援，請自行新增 nginx.conf，以下為範例（非必須）
# COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]