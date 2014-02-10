# CCHMapClusterController Xamarin

Mono port of the excellent [CCHMapClusterController library](https://github.com/choefele/CCHMapClusterController).

## Guide

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