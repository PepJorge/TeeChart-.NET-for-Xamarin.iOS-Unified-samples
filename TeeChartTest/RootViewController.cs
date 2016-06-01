using System;
using System.Drawing;

using Foundation;
using UIKit;
using Steema.TeeChart;

namespace TeeChartTest
{
    public partial class RootViewController : UIViewController
    {
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle
        TChart chart1;
		TChart chart2;
		TChart chart3;
		TChart chart4;
        //UIButton button;
		//UIImage logo;
		UIImageView imageview;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			chart1 = new TChart();
			chart2 = new TChart();
			chart3 = new TChart();
			chart4 = new TChart();

            CoreGraphics.CGRect r1 = this.View.Frame;
			r1.Height = r1.Height / 4;
			r1.Y = (r1.Height*3)-50;
            chart1.Frame = r1 ;

			CoreGraphics.CGRect r4 = this.View.Frame;
			r4.Height = r4.Height / 4;
			r4.Y = (r4.Height*2)-50;		
			chart4.Frame = r4 ;
        
			CoreGraphics.CGRect r2 = this.View.Frame;
			r2.Height = r2.Height / 2 - 50;
			r2.Width = r2.Width / 2;
			r2.Y = 0;
			chart2.Frame = r2 ;

			CoreGraphics.CGRect r3 = this.View.Frame;
			r3.Height = r3.Height / 2 - 50;
			r3.Width = r3.Width / 2;
			r3.X = r3.Width;
			r3.Y = 0;
			chart3.Frame = r3 ;

			chart1.Series.Add (new Steema.TeeChart.Styles.Volume ());
			chart1.Series [0].FillSampleValues ();
			chart1.Aspect.View3D = false;
			chart1.Axes.Bottom.Grid.Visible = false;
			chart1.Legend.Visible = false;
			//chart1.Axes.Bottom.Labels.Font.Color = UIColor.FromRGB (255, 255, 255).CGColor;
			chart1.Axes.Left.Visible = false;



			chart4.Series.Add (new Steema.TeeChart.Styles.Line ());
			chart4.Series.Add (new Steema.TeeChart.Styles.Line ());
			chart4.Series.Add (new Steema.TeeChart.Styles.Line ());
			chart4.Series [0].FillSampleValues (20);
			chart4.Series [1].FillSampleValues (20);
			chart4.Series [2].FillSampleValues (20);
			chart4.Series [0].Marks.Visible = false;
			chart4.Series [1].Marks.Visible = false;
			chart4.Series [2].Marks.Visible = false;
			(chart4.Series [0] as Steema.TeeChart.Styles.Line).LinePen.Width = 3;
			(chart4.Series [1] as Steema.TeeChart.Styles.Line).LinePen.Width = 3;
			(chart4.Series [2] as Steema.TeeChart.Styles.Line).LinePen.Width = 3;
			chart4.Aspect.View3D = false;
			//chart4.Axes.Bottom.Labels.Font.Color = UIColor.FromRGB (220, 220, 220).CGColor;
			chart4.Axes.Bottom.Increment = 3;

			Steema.TeeChart.Styles.CircularGauge gauge1 = new Steema.TeeChart.Styles.CircularGauge ();
			Steema.TeeChart.Styles.CircularGauge gauge2 = new Steema.TeeChart.Styles.CircularGauge ();

			chart2.Series.Add (gauge1);

			chart3.Series.Add (gauge2);

			this.View.AddSubview(chart1);
			this.View.AddSubview(chart2);
			this.View.AddSubview(chart3);
			this.View.AddSubview(chart4);

			chart3.Axes.Left.Labels.Font.Size = 5;
			chart2.Axes.Left.Labels.Font.Size = 5;

			gauge1.Frame.Width = 15;
			gauge1.Ticks.VertSize = 3;
			gauge1.Center.Shadow.Visible = false;
			gauge1.Axis.AxisPen.Visible = false;
			gauge1.FaceBrush.Gradient.Visible = false;
			gauge1.FaceBrush.Color = Color.FromArgb(220,220,220);
            gauge1.Hand.Color = Color.FromArgb(255, 65, 56);
			gauge1.Hand.Gradient.Visible = false;
			gauge1.Hand.Shadow.Visible = false;
			gauge1.Axis.AxisPen.Visible = false;
			gauge1.Value = 75;
			gauge1.Ticks.VertSize = 3;

			gauge2.Frame.Width = 15;
			gauge2.Ticks.VertSize = 3;
			gauge2.Axis.AxisPen.Visible = false;
			gauge2.Center.Shadow.Visible = false;
			gauge2.FaceBrush.Gradient.Visible = false;
            gauge2.FaceBrush.Color = Color.FromArgb(220, 220, 220);
            gauge2.Hand.Color = Color.FromArgb(255, 65, 56);
			gauge2.Hand.Gradient.Visible = false;
			gauge2.Hand.Shadow.Visible = false;
			gauge2.Axis.AxisPen.Visible = false;
			gauge2.Value = 50;
			gauge2.Ticks.VertSize = 3;


			chart1.Header.Visible = false;
			chart2.Header.Visible = false;
			chart3.Header.Visible = false;
			chart4.Header.Visible = false;

			chart1.Panel.Gradient.Visible = false;
			chart1.Panel.Color = Color.Black;
			chart1.Walls.Back.Transparent = true;
			chart1.Legend.Visible = false;
            chart1.Axes.Bottom.Labels.Font.Color = Color.FromArgb(220, 220, 220);


			chart4.Panel.Gradient.Visible = false;
			chart4.Panel.Color = Color.Black;
			chart4.Walls.Back.Transparent = true;
			chart4.Legend.Visible = false;
			chart4.Axes.Left.Visible = false;
            chart4.Axes.Bottom.Labels.Font.Color = Color.FromArgb(220, 220, 220);

			chart2.Panel.Gradient.Visible = false;
			chart2.Panel.Color = Color.Black;

			chart3.Panel.Gradient.Visible = false;
			chart3.Panel.Color = Color.Black;

			chart2.Panel.MarginTop = 0;
			chart2.Panel.MarginLeft = 0;
			chart2.Panel.MarginBottom = 0;
			chart2.Panel.MarginRight = 0;

			chart3.Panel.MarginTop = 0;
			chart3.Panel.MarginLeft = 0;
			chart3.Panel.MarginBottom = 0;
			chart3.Panel.MarginRight = 0;


			this.View.BackgroundColor = UIColor.Black;

			imageview = new UIImageView (UIImage.FromBundle ("logo.png"));
			CoreGraphics.CGRect rimage = new CoreGraphics.CGRect(95,this.View.Frame.Height-50,this.View.Frame.Width/2-30,50);

			imageview.Frame = rimage;
			this.View.AddSubview (imageview);

            //button = new UIButton(new CoreGraphics.CGRect(0, 0, 300, 300));
            //this.View.AddSubview(button);
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);


        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}