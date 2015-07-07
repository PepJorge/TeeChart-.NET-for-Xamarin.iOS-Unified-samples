using System;
#if __UNIFIED__
using UIKit;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif
using System.Collections.Generic;
using System.Drawing;

namespace TeeChartBuilder
{
	public class LegendDelegate : UITableViewDelegate
	{		
		public LegendDelegate(LegendController controller)
		{
		}
		
		public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
		{
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
		}
	}
}