using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using CCH.MapClusterController;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.MapKit;
using MonoTouch.UIKit;

namespace MapClusterSample
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
			
			_mapClusterController = new CCHMapClusterController(MapView);
			_mapClusterController.Delegate =  new ClusterDelegate();

			var location = new CLLocationCoordinate2D(52.516221, 13.377829);
			var region = MKCoordinateRegion.FromDistance(location, 45000, 45000);
			MapView.Region = region;
			MapView.Delegate = new MapViewDelegate();

			LoadData();
		}

		private void LoadData()
		{
			var jsonPath = NSBundle.MainBundle.PathForResource("Berlin-Data", "json");
			var inputStream = NSInputStream.FromFile(jsonPath);
			inputStream.Open();
			NSError err = null;
			var json = NSJsonSerialization.Deserialize(inputStream, 0, out err) as NSArray;

			MKPointAnnotation annotation = null;
			List<MKPointAnnotation> annotations = new List<MKPointAnnotation>();
			NSNumber lat = null;
			NSNumber lng = null;
			NSDictionary annotationAsJSON = null;

			for (int i = 0; i < json.Count; i++) 
			{
				annotationAsJSON = json.GetItem<NSDictionary>(i);
				annotation = new MKPointAnnotation();
				lat = annotationAsJSON.ValueForKeyPath(new NSString("location.coordinates.latitude")) as NSNumber;
				lng = annotationAsJSON.ValueForKeyPath(new NSString("location.coordinates.longitude")) as NSNumber;
				annotation.Coordinate = new CLLocationCoordinate2D(lat.DoubleValue, lng.DoubleValue);
				annotation.Title = annotationAsJSON.ValueForKeyPath(new NSString("person.lastName")).ToString();
				annotations.Add(annotation);
			}

			_mapClusterController.AddAnnotations(annotations.ToArray(), null);
		}

		internal class ClusterDelegate : CCHMapClusterControllerDelegate
		{
			public ClusterDelegate() : base()
			{
			}

			public override string MapClusterTitleForAnnotation(CCHMapClusterController mapClusterController, CCHMapClusterAnnotation mapClusterAnnotation)
			{
				return string.Format("{0} annotations", mapClusterAnnotation.Annotations.Count);
			}
		}

		internal class MapViewDelegate : MKMapViewDelegate
		{
			public MapViewDelegate() : base()
			{
			}

			public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, NSObject annotation)
			{
				MKAnnotationView annotationView = null;

				if (annotation is CCHMapClusterAnnotation)
				{
					var mapClusterAnnotation = annotation as CCHMapClusterAnnotation;
					var pin = mapView.DequeueReusableAnnotation("Pin") as MKPinAnnotationView;
					if (pin == null)
						pin = new MKPinAnnotationView(null, "Pin");

					pin.Annotation = mapClusterAnnotation;
					pin.CanShowCallout = true;
					annotationView = pin;
				}

				return annotationView;
			}
		}
	}
}

