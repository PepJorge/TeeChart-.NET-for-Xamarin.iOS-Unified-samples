using System;
#if __UNIFIED__
using UIKit;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif
using System.Collections.Generic;
using Steema.TeeChart;
using Steema.TeeChart.Styles;


namespace TeeChartBuilder.SeriesData
{
	public class DataDelegate : UITableViewDelegate
	{		
		private DataControllerController _controller;

		public DataDelegate(DataControllerController controller)
		{
			_controller = controller;
		}			

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{		
			_controller.InsertData (_controller.chart.Series[0] is Steema.TeeChart.Styles.Custom3D, true,indexPath.Row);
		}		
	}	
}
