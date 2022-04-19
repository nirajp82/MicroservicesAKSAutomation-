#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#https://alex-v.blog/2018/04/08/what-are-all-the-sections-in-the-dockerfile-generated-by-visual-studio/
#State #1:
#
#As you can see there's four build stage there. The base, build, publish, and final build
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#
#State #2:
#
#
#The SDK image is signifcantly bigger (1.74GB VS 219MB) because it has everything required to build our code. The size comparison should make it clear why we want 
#	our final image to be based on the 2.0-runtime image and not the 2.0-sdk one.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR does create the directory, but nothing is being copied to it
WORKDIR /src
COPY ["Shopping.WebClient/Shopping.WebClient.csproj", "Shopping.WebClient/"]
#Copy all projects that current project depends on in the solution.  one COPY line per .csproj file.
COPY ["Shopping.SharedKernel/Shopping.SharedKernel.csproj", "Shopping.SharedKernel/"]
COPY ["Shopping.Model/Shopping.Model.csproj", "Shopping.Model/"]
COPY ["Shoping.Infrastructure/Shoping.Infrastructure.csproj", "Shoping.Infrastructure/"]
RUN dotnet restore "Shopping.WebClient/Shopping.WebClient.csproj"
#The COPY . . copies the entire project, recursively into the container for the build.
COPY . .
WORKDIR "/src/Shopping.WebClient"
RUN dotnet build "Shopping.WebClient.csproj" -c Release -o /app/build
#
#State #3:
#
#
#The first line (FROM build AS publish) indicates that this stage depends on the intermediate state/image "build".
FROM build AS publish
# The build stage has a copy of our application’s source code, and so RUNning dotnet publish in this stage does exactly what it sounds like: 
#	it builds our code (using the Release configuration specified with the -c parameter) and publishes the output to the /app directory in the image 
#	(specified with the -o parameter).
RUN dotnet publish "Shopping.WebClient.csproj" -c Release -o /app/publish
#
#State #4:
#
#
FROM base AS final
WORKDIR /app
#We only need the compiled/published app and no other go dependencies anymore. 
#	But the compiled files are still in our publish image and not part of the image we want to start. So we need to copy files from the publish image.
#Publish build stage creating something in the /app/publish directory. So, in order to access that directory and copy the content inside it, 
#	our final build is copying from that directory using --from=publish so you can access the directory from the previous build.
COPY --from=publish /app/publish .
#define the command to be executed when starting a container based on this image.
ENTRYPOINT ["dotnet", "Shopping.WebClient.dll"]