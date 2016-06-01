using System;

using UIKit;
using Steema.TeeChart;
using System.Drawing;
using Steema.TeeChart.Drawing;
using Foundation;

namespace ImageCanvas
{
    public partial class ViewController : UIViewController
    {
        TChart chart1;
        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            chart1 = new TChart();
            chart1.Frame = this.View.Frame;


            var tSeries = new Steema.TeeChart.Styles.Candle();
            tSeries.FillSampleValues(30);
            chart1.Series.Add(tSeries);
            chart1.Aspect.View3D = false;

            chart1.Aspect.ZoomScrollStyle = Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Manual;
            chart1.Panning.Active = true;

            chart1.BeforeDrawSeries += chart1_BeforeDrawSeries;



            this.View.AddSubview(chart1);
        }

        void chart1_BeforeDrawSeries(object sender, Graphics3D g)
        {
            Rectangle chartRect = chart1.Chart.ChartRect;
            var Image = UIImage.FromBundle("teechart.jpg");

           
            g.Draw(chartRect.Left, chartRect.Top, Image.CGImage);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}