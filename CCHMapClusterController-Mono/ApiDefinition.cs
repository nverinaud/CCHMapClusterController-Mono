//
//  Author:
//       Nicolas VERINAUD <n.verinaud@gmail.com>
//
//  Copyright (c) 2014 Nicolas VERINAUD. All Rights Reserved.
//
using System;
using System.Runtime.InteropServices;
using CoreLocation;
using Foundation;
using MapKit;
using ObjCRuntime;
using UIKit;

namespace CCH.MapClusterController 
{
	[BaseType (typeof (NSObject))]
	[Protocol]
	public partial interface CCHMapClusterer 
	{
		[Abstract]
		[Export ("mapClusterController:coordinateForAnnotations:inMapRect:")]
		CLLocationCoordinate2D MapClusterCoordinateForAnnotations (CCHMapClusterController mapClusterController, NSSet annotations, MKMapRect mapRect);
	}

	public interface ICCHMapClusterer {}

	[BaseType (typeof (NSObject))]
	public partial interface CCHCenterOfMassMapClusterer : CCHMapClusterer
	{
	}

	[BaseType (typeof (NSObject))]
	public partial interface CCHNearCenterMapClusterer : CCHMapClusterer
	{
	}

	[BaseType (typeof (NSOperation))]
	public partial interface CCHMapClusterOperation {

		[Export ("allAnnotationsMapTree")]
		CCHMapTree AllAnnotationsMapTree { get; set; }

		[Export ("visibleAnnotationsMapTree")]
		CCHMapTree VisibleAnnotationsMapTree { get; set; }

		[Export ("clusterer")]
		CCHMapClusterer Clusterer { get; set; }

		[Export ("animator")]
		CCHMapAnimator Animator { get; set; }

		[Export ("clusterControllerDelegate", ArgumentSemantic.Assign)]
		CCHMapClusterControllerDelegate ClusterControllerDelegate { get; set; }

		[Export ("clusterController", ArgumentSemantic.Assign)]
		CCHMapClusterController ClusterController { get; set; }

		[Export ("initWithMapView:cellSize:marginFactor:reuseExistingClusterAnnotations:maxZoomLevelForClustering:minUniqueLocationsForClustering:")]
		IntPtr Constructor (MKMapView mapView, double cellSize, double marginFactor, bool reuseExistingClusterAnnotation, double maxZoomLevelForClustering, nuint minUniqueLocationsForClustering);

		[Static, Export ("cellMapSizeForCellSize:withMapView:")]
		double CellMapSizeForCellSize (double cellSize, MKMapView mapView);

		[Static, Export ("gridMapRectForMapRect:withCellMapSize:marginFactor:")]
		MKMapRect GridMapRectForMapRect (MKMapRect mapRect, double cellMapSize, double marginFactor);
	}

	[BaseType (typeof (MKPolygon))]
	public partial interface CCHMapClusterControllerDebugPolygon 
	{
		[Export ("mapClusterController", ArgumentSemantic.Assign)]
		CCHMapClusterController MapClusterController { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[Protocol]
	public partial interface CCHMapAnimator 
	{
		[Abstract]
		[Export ("mapClusterController:didAddAnnotationViews:")]
		void MapClusterDidAddAnnotationViews (CCHMapClusterController mapClusterController, MKAnnotationView [] annotationViews);

		[Abstract]
		[Export ("mapClusterController:willRemoveAnnotations:withCompletionHandler:")]
		void MapClusterWillRemoveAnnotations (CCHMapClusterController mapClusterController, IMKAnnotation [] annotations, [NullAllowed] Action completionHandler);
	}

	public interface ICCHMapAnimator {}

	[BaseType (typeof (NSObject))]
	public partial interface CCHFadeInOutMapAnimator : CCHMapAnimator 
	{
		[Export ("duration")]
		double Duration { get; set; }
	}

	[BaseType (typeof (NSObject))]
	public partial interface CCHMapClusterAnnotation : IMKAnnotation
	{
		[Export ("mapClusterController", ArgumentSemantic.Assign)]
		CCHMapClusterController MapClusterController { get; set; }

		[Export ("title", ArgumentSemantic.Copy)]
		string Title { get; set; }

		[Export ("subtitle", ArgumentSemantic.Copy)]
		string Subtitle { get; set; }

		[Export ("coordinate", ArgumentSemantic.Assign)]
		CLLocationCoordinate2D Coordinate { get; }

		[Bind ("setCoordinate:")] 
		void SetCoordinate(CLLocationCoordinate2D value);

		[Export ("delegate", ArgumentSemantic.Assign)]
		CCHMapClusterControllerDelegate Delegate { get; set; }

		[Export ("annotations", ArgumentSemantic.Copy)]
		NSSet Annotations { get; set; }

		[Export ("isCluster")]
		bool IsCluster { get; }

		[Export ("isOneLocation")]
		bool IsOneLocation { get; }

		[Export ("mapRect")]
		MKMapRect MapRect { get; }
	}

	[Protocol, Model, BaseType (typeof (NSObject))]
	public partial interface CCHMapClusterControllerDelegate 
	{
		[Export ("mapClusterController:titleForMapClusterAnnotation:")]
		string MapClusterTitleForAnnotation (CCHMapClusterController mapClusterController, CCHMapClusterAnnotation mapClusterAnnotation);

		[Export ("mapClusterController:subtitleForMapClusterAnnotation:")]
		string MapClusterSubtitleForAnnotation (CCHMapClusterController mapClusterController, CCHMapClusterAnnotation mapClusterAnnotation);

		[Export ("mapClusterController:willReuseMapClusterAnnotation:")]
		void MapClusterWillReuseAnnotation (CCHMapClusterController mapClusterController, CCHMapClusterAnnotation mapClusterAnnotation);
	}

	[BaseType (typeof (NSObject))]
	public partial interface CCHMapClusterController 
	{
		[Export ("annotations", ArgumentSemantic.Copy)]
		NSSet Annotations { get; }

		[Export ("mapView", ArgumentSemantic.Retain)]
		MKMapView MapView { get; }

		[Export ("marginFactor")]
		double MarginFactor { get; set; }

		[Export ("cellSize")]
		double CellSize { get; set; }

		[Export ("zoomLevel")]
		double ZoomLevel { get; }

		[Export ("maxZoomLevelForClustering")]
		double MaxZoomLevelForClustering { get; set; }

		[Export ("minUniqueLocationsForClustering")]
		nuint MinUniqueLocationsForClustering { get; set; }

		[Export ("debuggingEnabled")]
		bool DebuggingEnabled { [Bind ("isDebuggingEnabled")] get; set; }

		[Wrap ("WeakDelegate")]
		CCHMapClusterControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		[Export ("clusterer", ArgumentSemantic.Assign)]
		ICCHMapClusterer Clusterer { get; set; }

		[Export ("reuseExistingClusterAnnotations")]
		bool ReuseExistingClusterAnnotations { get; set; }

		[Export ("animator", ArgumentSemantic.Assign)]
		ICCHMapAnimator Animator { get; set; }

		[Export ("initWithMapView:")]
		IntPtr Constructor (MKMapView mapView);

		[Export ("addAnnotations:withCompletionHandler:")]
		void AddAnnotations (IMKAnnotation [] annotations, [NullAllowed] Action completionHandler);

		[Export ("removeAnnotations:withCompletionHandler:")]
		void RemoveAnnotations (IMKAnnotation [] annotations, [NullAllowed] Action completionHandler);

		[Export ("selectAnnotation:andZoomToRegionWithLatitudinalMeters:longitudinalMeters:")]
		void SelectAnnotation (IMKAnnotation annotation, double latitudinalMeters, double longitudinalMeters);
	}

//	public partial interface CCHMapClusterAnnotation 
//	{
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapClusterControllerUtils.h")]
//		MKMapRect CCHMapClusterControllerAlignMapRectToCellSize (MKMapRect mapRect, double cellSize);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapClusterControllerUtils.h")]
//		CCHMapClusterAnnotation CCHMapClusterControllerFindVisibleAnnotation (NSSet annotations, NSSet visibleAnnotations);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapClusterControllerUtils.h")]
//		double CCHMapClusterControllerMapLengthForLength (MKMapView mapView, UIView view, double length);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapClusterControllerUtils.h")]
//		double CCHMapClusterControllerAlignMapLengthToWorldWidth (double mapLength);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapClusterControllerUtils.h")]
//		bool CCHMapClusterControllerCoordinateEqualToCoordinate (CLLocationCoordinate2D coordinate1, CLLocationCoordinate2D coordinate2);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapClusterControllerUtils.h")]
//		CCHMapClusterAnnotation CCHMapClusterControllerClusterAnnotationForAnnotation (MKMapView mapView, MKAnnotation annotation, MKMapRect mapRect);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapClusterControllerUtils.h")]
//		void CCHMapClusterControllerEnumerateCells (MKMapRect mapRect, double cellSize, Delegate block);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapClusterControllerUtils.h")]
//		MKMapRect CCHMapClusterControllerMapRectForCoordinateRegion (MKCoordinateRegion coordinateRegion);
//	}

	[BaseType (typeof (NSObject))]
	public partial interface CCHMapTree 
	{
		[Export ("annotations", ArgumentSemantic.Copy)]
		NSSet Annotations { get; }

//		[Export ("init")]
//		IntPtr Constructor ();

		[Export ("initWithNodeCapacity:minLatitude:maxLatitude:minLongitude:maxLongitude:")]
		IntPtr Constructor (nuint nodeCapacity, double minLatitude, double maxLatitude, double minLongitude, double maxLongitude);

		[Export ("addAnnotations:")]
		bool AddAnnotations (NSObject [] annotations);

		[Export ("removeAnnotations:")]
		bool RemoveAnnotations (NSObject [] annotations);

		[Export ("annotationsInMapRect:")]
		NSSet AnnotationsInMapRect (MKMapRect mapRect);
	}

	[BaseType (typeof (NSObject))]
	public partial interface CCHMapTreeUnsafeMutableArray 
	{
		[Export ("objects", ArgumentSemantic.Assign)]
		NSObject Objects { get; }

		[Export ("numObjects")]
		nuint NumObjects { get; }

		[Export ("initWithCapacity:")]
		IntPtr Constructor (nuint capacity);

		[Export ("addObject:")]
		void AddObject (NSObject obj);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		IntPtr CCHMapTreeNodeMake (CCHMapTreeBoundingBox boundary, uint bucketCapacity);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		void CCHMapTreeFreeQuadTreeNode (ref CCHMapTreeNode node);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		void CCHMapTreeTraverse (ref CCHMapTreeNode node, CCHMapTreeTraverseBlock block);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		void CCHMapTreeGatherDataInRange (ref CCHMapTreeNode node, CCHMapTreeBoundingBox range, TBDataReturnBlock block);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		void CCHMapTreeGatherDataInRange2 (ref CCHMapTreeNode node, CCHMapTreeBoundingBox range, NSMutableSet annotations);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		void CCHMapTreeGatherDataInRange3 (ref CCHMapTreeNode node, CCHMapTreeBoundingBox range, CCHMapTreeUnsafeMutableArray annotations);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		IntPtr CCHMapTreeBuildWithData (ref CCHMapTreeNodeData data, uint count, CCHMapTreeBoundingBox boundingBox, uint bucketCapacity);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		bool CCHMapTreeNodeInsertData (ref CCHMapTreeNode node, CCHMapTreeNodeData data, uint bucketCapacity);
//
//		[Static]
//		[DllImport ("/Users/nicolas/Developement/github/CCHMapClusterController-Mono/CCHMapClusterController/CCHMapClusterController/CCHMapTreeUtils.h")]
//		bool CCHMapTreeNodeRemoveData (ref CCHMapTreeNode node, CCHMapTreeNodeData data);
	}

	[BaseType (typeof (NSObject))]
	public partial interface CCHMapViewDelegateProxy : IMKMapViewDelegate 
	{
		[Export ("delegate", ArgumentSemantic.Assign)]
		MKMapViewDelegate Delegate { get; }

		[Export ("target", ArgumentSemantic.Assign)]
		MKMapViewDelegate Target { get; }

		[Export ("initWithMapView:delegate:")]
		IntPtr Constructor (MKMapView mapView, MKMapViewDelegate mapDelegate);
	}
}