using System;
using System.Drawing;
#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

namespace TeeChartBuilder
{
	public class TypesTableCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("TypesTableCell");
		
		public TypesTableCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			//TextLabel.Text = "TextLabel";
		}
	}
}

