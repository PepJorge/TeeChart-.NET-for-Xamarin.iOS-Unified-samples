using System;
using System.Drawing;
#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif
using Steema.TeeChart;

namespace TeeChartBuilder
{
	public class TypesTableController : UITableViewController
	{
		public TChart chart;
		public ChartViewController chartController;
		public Boolean newChart;

		public TypesTableController (Boolean newChart=false) : base (UITableViewStyle.Grouped)
		{
			this.chartController =new ChartViewController();
			this.chart = chartController.chart;
			this.newChart = newChart;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Register the TableView's data source
			TableView.Source = new TypesTableSource ();
			TableView.Delegate = new SeriesTypesDelegate(this,this.chartController);
		}
	}
}

