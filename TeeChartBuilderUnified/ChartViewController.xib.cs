using System;
using System.Collections.Generic;
using System.Linq;
#if __UNIFIED__
using Foundation;
using UIKit;
using AddressBook;
using CoreTelephony;
using ObjCRuntime;
using MessageUI;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AddressBook;
using MonoTouch.CoreTelephony;
using MonoTouch.ObjCRuntime;
using MonoTouch.MessageUI;
#endif
#if !__UNIFIED__
using MonoTouch.Dialog;
#endif
using Steema.TeeChart;
using System.Drawing;
using TeeChartBuilder.SeriesData;

namespace TeeChartBuilder
{
	public partial class ChartViewController : UIViewController
	{
		public TChart chart= new Steema.TeeChart.TChart();
#if __UNIFIED__
        public CoreGraphics.CGRect mainChartFrame;
#else
        public System.Drawing.RectangleF mainChartFrame;
#endif
        SettingsController controller;
		DataControllerController datacontroller;

		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public ChartViewController (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public ChartViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public ChartViewController () : base("ChartViewController", null)
		{
			Initialize ();
		}

		void Initialize ()
		{		

#if __UNIFIED__
			BottomBar = new UIToolbar (new CoreGraphics.CGRect (0,this.View.Frame.Height-85,
				this.View.Frame.Width,50));

			mainChartFrame = new CoreGraphics.CGRect(0,0,this.View.Bounds.Width,this.View.Bounds.Height);						
#else
			BottomBar = new UIToolbar (new RectangleF (0,this.View.Frame.Height-85,
				this.View.Frame.Width,50));

			mainChartFrame = new System.Drawing.RectangleF(0,0,this.View.Bounds.Width,this.View.Bounds.Height);						
#endif
			chart.Frame = mainChartFrame;
			chart.ClipsToBounds = true;
			
			UIDevice.CurrentDevice.BatteryMonitoringEnabled=true;
			float bLevel = UIDevice.CurrentDevice.BatteryLevel;			
			
			Steema.TeeChart.Styles.NumericGauge series1 = new Steema.TeeChart.Styles.NumericGauge();
			series1.Value = bLevel*100;
			chart.Series.Add(series1);
			series1.Markers[2].AllowEdit=true;
			series1.Markers[2].Text= "Battery Level";
			series1.Markers[1].AllowEdit=true;
			series1.Markers[1].Text= "Percentage";
			
			Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(chart.Chart);
			//theme.Apply();
			Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(chart.Chart,Steema.TeeChart.Themes.Theme.OnBlackPalette);		
			chart.Aspect.ClipPoints=true;
			chart.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal;
							
			this.View.AddSubview(chart);	
		


			UIBarButtonItem btn1 = new UIBarButtonItem();
			btn1.Style = UIBarButtonItemStyle.Bordered;
			btn1.Title = "Data";
			btn1.Clicked += delegate(object sender, EventArgs e) {
				//Console.WriteLine( "Data" );

				datacontroller = new SeriesData.DataControllerController(chart,this,chart.Series[0] is Steema.TeeChart.Styles.Custom3D);			  
				NavigationController.PushViewController(datacontroller,true);

			};

			UIBarButtonItem btnSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

			UIBarButtonItem btn2 = new UIBarButtonItem(UIBarButtonSystemItem.Action);
			btn2.Style = UIBarButtonItemStyle.Bordered;
			btn2.Title = "";
			btn2.Clicked += delegate(object sender, EventArgs e) {

				var actionSheet = new UIActionSheet ("Options", null, "Cancel", "Camera roll", "Send by mail", "Print"){
					Style = UIActionSheetStyle.Default
				};
				actionSheet.Clicked += delegate (object sender1, UIButtonEventArgs args){
					//Console.WriteLine ("Clicked on item {0}", args.ButtonIndex);

					switch (args.ButtonIndex)
					{
					case 0:
						SaveToCameraRoll();
						break;
					case 1:
						SendByMail();
						break;
					case 2: 
						CallPrint();
						break;
					}
				};

				actionSheet.ShowInView (View);
			};

			BottomBar.SetItems( new UIBarButtonItem[] { btn1, btnSpace, btn2 }, false );

			BottomBar.TintColor = UIColor.Black;
			this.View.AddSubview (BottomBar);

			UIDevice.CurrentDevice.BatteryMonitoringEnabled=false;

			/*
			// Grab The Context
			UIGraphics.BeginImageContext ( this.View.Frame.Size);
			var ctx = UIGraphics.GetCurrentContext ();
			
			// Render in the context
			this.View.Layer.RenderInContext(ctx);
			 
			// Lets grab a UIImage of the current graphics context
			UIImage i = UIGraphics.GetImageFromCurrentImageContext();
			
			// Set this to your desktop, ie change the martinbowling part
			string png = "/Users/steema/Desktop/chartxx.png";
			// Get the Image as a PNG
			NSData imgData = i.AsPNG();
			NSError err = null;
			if (imgData.Save(png, false, out err))
			{
				// Console.WriteLine("saved as " + png);
			} 
			else 
			{
			 	// Console.WriteLine("NOT saved as" + png + 
			    //                " because" + err.LocalizedDescription);
			}
			
			UIGraphics.EndImageContext ();			
			*/			
		}
		
		#endregion

		public UIToolbar BottomBar;

		private void SaveToCameraRoll()
		{
			UIGraphics.BeginImageContext(chart.Frame.Size); 
			var ctx = UIGraphics.GetCurrentContext();
			if (ctx != null)
			{
				chart.Layer.RenderInContext(ctx);
				UIImage img = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();

				// Save to Photos
				img.SaveToPhotosAlbum(
					(sender, args)=>{
					Console.WriteLine("image saved to Photos");
					//AlertCenter.Default.PostMessage ("Chart saved!", "The Chart has been saved to CameraRoll.",
					  //                               UIImage.FromFile ("SeriesIcons/bar.png"), delegate {
//					});
					}

				);
			}
			else
			{
				Console.WriteLine("ctx null - doesn't seem to happen");
			}
		}

		MFMailComposeViewController _mail;

		private void SendByMail()
		{
			UIGraphics.BeginImageContext(chart.Frame.Size); 
			var ctx = UIGraphics.GetCurrentContext();
			if (ctx != null)
			{
				chart.Layer.RenderInContext(ctx);
				UIImage img = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();

				if (MFMailComposeViewController.CanSendMail) {
					_mail = new MFMailComposeViewController ();
				
					_mail.AddAttachmentData (img.AsPNG (), "image/png", "image.png");
					_mail.SetSubject ("Chart from TeeChart Office for iPhone");

					_mail.SetMessageBody ("This is the Chart sent through TeeChart app.", 
				                      false);
					_mail.Finished += HandleMailFinished;

					this.PresentModalViewController (_mail, true);

				} else {
				// handle not being able to send mail
				}
			}
		}

		void HandleMailFinished (object sender, MFComposeResultEventArgs e)
		{
			if (e.Result == MFMailComposeResult.Sent) {
				UIAlertView alert = new UIAlertView ("Mail Alert", "Mail Sent",
				                                     null, "Yippie", null);
				alert.Show ();

				// you should handle other values that could be returned 
				// in e.Result and also in e.Error 
			}
            e.Controller.DismissViewController(true, () => { });
		}

		private void CallPrint() 
		{
			var printInfo = UIPrintInfo.PrintInfo;
			printInfo.OutputType = UIPrintInfoOutputType.General;
			printInfo.JobName = "Chart";

			var textFormatter = new UISimpleTextPrintFormatter ("Once upon a time...") {
				StartPage = 0,
				ContentInsets = new UIEdgeInsets (72, 72, 72, 72),
				MaximumContentWidth = 6 * 72,
			};

			var printer = UIPrintInteractionController.SharedPrintController;
			printer.PrintInfo = printInfo;
			printer.PrintFormatter = textFormatter;
			printer.ShowsPageRange = true;
			printer.Present (true, (handler, completed, err) => {
				if (!completed && err != null){
					Console.WriteLine ("error");
				}
			});
		}

		//private NSObject notificationObserver;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//notificationObserver  = NSNotificationCenter.DefaultCenter
			//	.AddObserver("UIDeviceOrientationDidChangeNotification", DeviceRotated );

			UIBarButtonItem button= new UIBarButtonItem();
			button.Title = "Settings";
			this.Title="Chart";
		
            button.Clicked += delegate(object sender, EventArgs e) {			
	            controller = new SettingsController(chart,this,UITableViewStyle.Grouped);			  
	            NavigationController.PushViewController(controller,true);
            };
			
  	        this.NavigationItem.SetRightBarButtonItem(button,true);	
		}

		public override void ViewDidAppear (bool animated)
		{
			UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications();
			CheckPositions ();
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return base.GetSupportedInterfaceOrientations ();
		}

		private void DeviceRotated(NSNotification notification)
		{
			CheckPositions();
		}

		//iOS 5 support
		//if I don't put it it doesn't work for iOS 5 device but works on iOS 6 simulator
		[Obsolete ("Deprecated in iOS6. Replace it with both GetSupportedInterfaceOrientations and PreferredInterfaceOrientationForPresentation")]
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		private void CheckPositions()
		{
			// Refresh Chart rotating the device
			chart.RemoveFromSuperview ();
#if __UNIFIED__
			CoreGraphics.CGRect f = new CoreGraphics.CGRect(View.Frame.X,50,View.Frame.Width,View.Frame.Height-BottomBar.Frame.Height-50);
#else
			RectangleF f = new RectangleF(View.Frame.X,50,View.Frame.Width,View.Frame.Height-BottomBar.Frame.Height-50);
#endif
			chart.Frame = f;
			View.AddSubview (chart);			
			chart.DoInvalidate ();		

#if __UNIFIED__
            BottomBar.Frame  = new CoreGraphics.CGRect (0,View.Frame.Height-50,
#else
            BottomBar.Frame  = new RectangleF (0,View.Frame.Height-50,
#endif
				this.chart.Frame.Width,50);
		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			CheckPositions ();
		}
	}
}