# CCHMapClusterController Xamarin

Mono port of the excellent [CCHMapClusterController library](https://github.com/choefele/CCHMapClusterController).

## Guide

### Installation

#### Using NuGet

* A NuGet package is available in nuget.org. Simply search for `CCHMapClusterController`.

#### Downloading the DLL

* The DLL is released using Github Release. Follow [this link](https://github.com/nverinaud/CCHMapClusterController-Mono/releases) to find the version you want.

#### Building the DLL

1. Open the `CCHMapClusterController-Mono.sln` solution.
2. Build in Release (or Debug if you want the symbolication).
3. Copy the created DLL in the `CCHMapClusterController-Mono/bin/{Debug|Release}` folder to your project.
4. You're done !

### Sample usage

```c#
using CCH.MapClusterController;

namespace MyNamespace
{
	public partial class MapViewController : UIViewController
	{
		CCHMapClusterController _mapClusterController;

		public MapViewController() : base("MapViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			var annotations = /* Get annotations */;
			_mapClusterController = new CCHMapClusterController(MapView);
			_mapClusterController.AddAnnotations(annotations, null);
		}
	}
}
```

## Authors

* Nicolas VERINAUD ([@nverinaud](https://twitter.com/nverinaud))
* [Claus Höfele](https://github.com/choefele) (original author of the Objective-C library)

## License

Released under the MIT License. For more see `LICENSE.md`.