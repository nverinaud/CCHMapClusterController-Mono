MDTOOL ?= /Applications/Xamarin\ Studio.app/Contents/MacOS/mdtool

all:
	$(MDTOOL) build -c:NuGet ./CCHMapClusterController-Mono.sln
	nuget pack NuGet/CCHMapClusterController/CCHMapClusterController.nuspec -o NuGet/CCHMapClusterController/

clean:
	$(MDTOOL) build -t:Clean -c:NuGet ./CCHMapClusterController-Mono.sln
