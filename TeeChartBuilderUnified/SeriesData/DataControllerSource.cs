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
using Steema.TeeChart;

namespace TeeChartBuilder.SeriesData
{
	public class DataControllerSource : UITableViewSource
	{
		public List<SeriesData> seriesData;
		public TChart c;

		public DataControllerSource (TChart chart)
		{
			this.c = chart;

			seriesData = new List<SeriesData> ();
			// Load SeriesData with Values of the Chart
			for (int i=0;i<c.Series[0].Count;i++)
			{
				if (!(c.Series[0] is Steema.TeeChart.Styles.Custom3D)) 
				{
					seriesData.Add( new SeriesData(false, (int)c.Series[0].XValues[i], c.Series[0].YValues[i],0.0,c.Series[0].Labels[i]));
				}
			else
				{
					seriesData.Add( new SeriesData(true,(int)c.Series[0].XValues[i], c.Series[0].YValues[i],(c.Series[0] as Steema.TeeChart.Styles.Custom3D).ZValues[i],c.Series[0].Labels[i]));
				}
			}
		}

#if __UNIFIED__
		public override nint NumberOfSections (UITableView tableView)
#else
		public override int NumberOfSections (UITableView tableView)
#endif
        {
			// TODO: return the actual number of sections
			return 1;
		}

#if __UNIFIED__
		public override nint RowsInSection (UITableView tableview, nint section)
#else
		public override int RowsInSection (UITableView tableview, int section)
#endif
		{
			// TODO: return the actual number of items in the section
			return seriesData.Count;
		}

#if __UNIFIED__
		public override string TitleForHeader (UITableView tableView, nint section)
#else
		public override string TitleForHeader (UITableView tableView, int section)
#endif
        {
			return "Label and Value";
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
			var cell = tableView.DequeueReusableCell (DataControllerCell.Key) as DataControllerCell;
			if (cell == null)
				cell = new DataControllerCell ();

			// TODO: populate the cell with the appropriate data based on the indexPath
			cell.DetailTextLabel.Text = seriesData [indexPath.Row].YValue.ToString("0.00"); // "DetailsTextLabel";
			cell.TextLabel.Text = seriesData [indexPath.Row].Label.ToString ();

			return cell;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:

				// remove the item from the underlying data source
				seriesData.RemoveAt (indexPath.Row);
				// delete the row from the table
				tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				this.c.Series [0].Delete (indexPath.Row);

				break;
			default:
				break;
			}
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}

		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you don't allow re-ordering
		}

		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete; // this example doesn't support Insert
		}

	}
}

