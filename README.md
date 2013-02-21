Gnuplot.C#
========

Overview
--------
Most scientific publications with graphs use gnuplot.  It is extremely well documented and handles 2D, 3D (surface and pointcloud), heatmap, png, jpg, and much more using simple math syntax as well as simple text-based data.  And it supports many output formats, as well interactive zooming/rotating.

It only takes about 10 lines of code to wire up C# to send commands to gnuplot.  But as soon as you try to send arrays of data, overlay multiple graphs, and generally work with gnuplot from C#, you'll immediately see that you have to add all kinds of utility functions to not clutter up your code.

Gnuplot.C# provides a nice set of functions to make it easy to use all the power of gnuplot to visualize your data in C#.  Check out the examples of how easy it is to visualize functions and data.

Installation
----------
Just put gnuplot.cs in your project, change the first line from C:\gnuplot\bin to the location of gnuplot.exe on your system.  

If you haven't installed gnuplot on your system, download it at http://sourceforge.net/projects/gnuplot/files/ or http://www.gnuplot.info 

Inspiration
-----------
In 2012, I completed the excellent Machine Learning course by Andrew Ng (Coursera).  We used Octave/Matlab, and Octave uses gnuplot for its graphs. I wanted to recreate all the class projects in C# for practice.  Microsoft has a cloud numerics library, so C# is a good choice for machine learning if you want to for example, train your machine learning algorithm on a large dataset and scale across many computers in the Azure cloud.

I believe this project will be helpful to anyone who wants to visualize data/functions in C#, so I have released it here.

Examples
========
To see various demos in action, download the files, open the solution (Visual Studio) and run Demo.cs (make sure you've installed gnuplot first)

If you are not familiar with gnuplot, I recommend you visit www.gnuplot.info and see all the demos there.  Then come back for how to do it all in C#.

Plot
--------

**Plot a function**
```C#
GnuPlot.Plot("sin(x) + 2");
```
![Plot a function](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/plotf.png)
-------
<br><br>

**Plot a function with custom color and line width** (see gnuplot documentation)
```C#
GnuPlot.Plot("sin(x) + 2", "lc rgb \"magenta\" lw 5");
```
![Plot a function](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/plotf2.png)
-------
<br><br>

**Plot an array of y values**
```C#
double[] Y = new double[] { -4, 6.5, -2, 3, -8, -5, 11, 4, -5, 10 };
GnuPlot.Plot(Y);
```
![Plot data](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/plotY.png)
-------
<br><br>

**Plot an array of x and y values**  (notice the x values vs previous graph)
```C#
double[] X = new double[] { -10, -8.5, -2, 1, 6, 9, 10, 14, 15, 19 };
double[] Y = new double[] { -4, 6.5, -2, 3, -8, -5, 11, 4, -5, 10 };
GnuPlot.Plot(X, Y);
```
![Plot data](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/plotXY.png)
-------
<br><br>

**Overlay multiple graphs**  (HoldOn causes future plots to be overlayed. HoldOff make future plots replace previous.
```C#
GnuPlot.HoldOn();
GnuPlot.Plot("cos(x) + x");
GnuPlot.Plot("cos(2*x)", "with points pt 3");
```
![Plot data](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/plotOverlay.png)
-------
<br><br>

Splot  (3D data)
--------

**plot a 3D function**
```C#
GnuPlot.SPlot("1 / (.05*x*x + .05*y*y + 1)");
```
![Plot data](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/splot1.png)
-------
<br>
Set the number of lines with **isosamples** to control "detail" (see gnuplot documentation)
```C#
GnuPlot.Set("isosamples 30");
GnuPlot.SPlot("1 / (.05*x*x + .05*y*y + 1)");
```
![Plot data](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/splot2.png)
-------
<br>
Hide back faces with **hidden3d** to make it "solid" (see gnuplot documentation)
```C#
GnuPlot.Set("isosamples 30", "hidden3d");
GnuPlot.SPlot("1 / (.05*x*x + .05*y*y + 1)");
```
![Plot data](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/splot3.png)
-------
<br><br>

**plot a file with 3D data**
__data.txt__  (this can be in any format accepted by gnuplot.  Here it's x,y,z points for two separate polygons)
```
-1 -1 -1
-1 1 -1
-.1 .1 1
-.1 -.1 1
-1 -1 -1


.1 .1 1
.1 -.1 1
1 -1 -1
1 1 -1
.1 .1 1

```

**to plot the above file**
```C#
splot ("data.txt");
```
![Plot data](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/splotfile.png)
-------
<br><br>

plot an array of z values (specify y size to break the array into a square grid)
```C#
double[] Z = new double[] { -4, -2.5, 1, 3,    -3, -2, 3, 4,    -1, 2, 6, 8 };
GnuPlot.Set("pm3d"); //color planes by z value
GnuPlot.SPlot(4, Z); //split the 12 z values into rows of 4 points
```
![Plot data](https://raw.github.com/AwokeKnowing/GnuplotCSharp/master/ReadmeImages/splotSZ.png)
-------
<br><br>

plot a grid of z values
```C#

```

plot a grid of x, y, and z values
```C#

```


Contour (3D data as "top view" with contour lines)
--------
Contour maps are very useful to make it easier to see the peaks and valleys, and see what areas have the same range of values.  Gnuplot lets you show contour maps, but you can't draw 2d points and lines on them.  The Contour methods generate a 2D contour map, allowing you to plot further data points on it.

```C#

```


Heatmap  (intensity/z-scale map)
--------

```C#

```

Set and Unset
--------

```C#

```

Replot
--------

```C#

```

Write and WriteLine
--------

```C#

```

SaveData
--------

```C#

```

PointStyles
--------

```C#

```

StoredPlot  
--------

```C#


```

API Reference
---
Plot

Splot

Contour

Heatmap

Set

Unset

HoldOn

HoldOff

SaveData

WriteLine

Write

Replot

License
-------
You are free to use this code as you wish.  Please mention you got it on GitHub from James Morris aka AwokeKnowing. Also, email me at james david morris a/t g mail .com (no spaces) and let me know about your project.

Disclaimer
----------
Use this code at your own risk.  The author cannot guarantee that it is free of defects or that it will not damage your system or corrupt your data.  It's less than 1000 lines of code, so just look at it, and decide if it works for you.

Happy coding!!