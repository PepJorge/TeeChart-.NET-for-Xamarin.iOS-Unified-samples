using System;
using System.Drawing;
#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif
using System.Collections.Generic;

namespace TeeChartBuilder
{
	public class SettingsSource : UITableViewSource
	{
		private List<string> _items;
		private string _cellId;
		
		public SettingsSource ()
		{
			_cellId = "cellid";
			_items = new List<string>()
			{
				"Aspect",
				"Themes",
				"Color Palettes",
				"Legend",
				"Tools",
				"Functions",
			};
		}
		
#if __UNIFIED__
		public override nint NumberOfSections (UITableView tableView)
#else
		public override int NumberOfSections (UITableView tableView)
#endif
		{
			return 1;
		}
		
#if __UNIFIED__
		public override nint RowsInSection (UITableView tableview, nint section)
#else
		public override int RowsInSection (UITableView tableview, int section)
#endif
        {
			return _items.Count;
		}
		
#if __UNIFIED__
		public override string TitleForHeader (UITableView tableView, nint section)
#else
		public override string TitleForHeader (UITableView tableView, int section)
#endif
        {
			return "Settings";
		}
		
#if __UNIFIED__
		public override string TitleForFooter (UITableView tableView, nint section)
#else
		public override string TitleForFooter (UITableView tableView, int section)
#endif
        {
			return "";
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(_cellId); 
			
			if (cell == null )
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, _cellId);
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			
			cell.TextLabel.Text = _items[indexPath.Row];
			
			return cell; 
		}
	}
}

