SubChart Tool Demo
==================
 
In the demo we use the SubChart Tool available with TChart for Xamarin.iOS. This Tool lets us create a limitless number of Charts within the area of the main Chart. Each and everyone of these Charts can be personalised to our own taste, as if it were the base Chart, by placing it in the position we like and determining its size. We can use any type of DataSeries to populate the Chart.

Example code to create and use of the SubChart Tool:

            tChart1.Tools.Add(subChartTool1 = new Steema.TeeChart.Tools.SubChartTool());
 	 
 
 	            tChart1.Panning.Active = true;
 	 
 
 	            subchart1 = subChartTool1.Charts.AddChart("Chart0");
 	            subchart1.Panel.Color = Color.Transparent;
 	            subchart1.Series.Add(new Bar());
 	            subchart1.Aspect.View3D = false;
 	            subchart1.Series[0].FillSampleValues();
 	            subchart1.Series[0].ColorEach = true;
 	            (subchart1.Series[0] as Bar).Pen.Visible = false;
 	            subchart1[0].Marks.Visible = false;
 	            subchart1[0].Chart.Title.Visible = false;
 	            subchart1[0].Chart.Walls.Back.Transparent = true;
 	            subchart1[0].Chart.Panel.Transparent = true;
 	            subchart1[0].Chart.Axes.Left.Grid.Visible = false;
 	 
 
 	            subChartTool1.Charts[0].Chart.Walls.Visible = false;
 	 
 
 	            for (int j = 0; j < subChartTool1.Charts.Count; j++)
 	            {
 	                subchart1 = subChartTool1.Charts[j].Chart;
 	                for (int i = 0; i < line1.Count; i++)
 	                {
 	                    subchart1[0].Add(line1.XValues[i], line1.YValues[i]);
 	                }
 	            }
 
            subChartTool1.Charts[0].Left = 50;
 	            subChartTool1.Charts[0].Top = 390;
 	            subChartTool1.Charts[0].Width = 325;

![screenshot](https://github.com/Steema/TeeChart-.NET-for-Xamarin.iOS-Unified-samples/blob/master/SubChartTool/Screenshots/SubChartTool.png "TeeChart.Net for Xamarin.iOS")
---------------------

------
### Author
------
Josep Lluis Jorge @joseplluisjorge

The Chart_Scroll demo shows you how to easily add Scroll/Pan functionality to your application with only a few lines of code.
TeeChart for Xamarin.iOS offers two ways to add this functionality.

1) By assigning the specific number of points we want to be visible on the screen and then move through all the data of the Chart one page at a time. In order to achieve this we must use the MaxPointPerPage property:
```
   // Before doing anything else we need to make sure that the Scroll/Pan function is activated.
	Chart.Panning.Active = true;

   // In the case that we only want to allow Scrolling in one direction, we can use the following line of cade:
	Chart.Panning.Allow = ScrollModes.Horizontal;

   // We can now assign the number of points per page we would like to appear.
	Chart.Page.MaxPointsPerPage = 10;	
```
2) Another way of visual moving through the data is by making use of the UISlider control, also shown in the example. The code to use is the following:

```
   // Scroll by using UISlider and SetMinMax
   // Assign an inicial min and max value
   	Chart.Axes.Bottom.SetMinMax(0, 10);
   // Create the UISlider, to which we will assign some inicial values
   	UISlider slider1 = new UISlider(new CoreGraphics.CGRect(0,this.View.Frame.Height - 25,this.View.Frame.Width, 20));
   	slider1.MinValue = 5;
   	slider1.MaxValue = 95;
   	slider1.Value = 5.0f; // the current value
   	slider1.ValueChanged += (sender, e) => Chart.Axes.Bottom.SetMinMax(((UISlider)sender).Value - 5, ((UISlider)sender).Value + 5);
            
   // Add the Chart and the Slider to our View
   	View.AddSubview(Chart);
   	View.AddSubview(slider1);
```
![screenshot](https://raw.githubusercontent.com/Steema/TeeChart-.NET-for-Xamarin.iOS-Unified-samples/master/Chart_Scroll/Screenshots/Chart_Scroll.png "TeeChart.Net for Xamarin.iOS")


This example also shows how to modify the size of the labels on the left axis of the Chart, so that we can choose the space to leave between the axis title and the labels.

------
### Author
------
Josep Lluis Jorge @joseplluisjorge