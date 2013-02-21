using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwokeKnowing.GnuplotCSharp;
using System.Threading;

class Demo
{
    static void Main(string[] args)
    {
        //GnuPlot.Plot("sin(x) + 2", "lc rgb \"magenta\" lw 5");
        //Thread.Sleep(2000);
        
        //GnuPlot.HoldOn();
        //GnuPlot.Plot("cos(x) + x");
        //GnuPlot.Plot("cos(2*x)", "with points pt 3");
        //Thread.Sleep(2000);
                
        //GnuPlot.HoldOff();
        //GnuPlot.Plot("atan(x)");
        //Thread.Sleep(2000);

        //double[] X = new double[] { -10, -8.5, -2, 1, 6, 9, 10, 14, 15, 19 };
        //double[] Y = new double[] { -4, 6.5, -2, 3, -8, -5, 11, 4, -5, 10 };
        //GnuPlot.Plot(X, Y);
        //Thread.Sleep(2000);

        //string tempfolder=System.IO.Path.GetTempPath();
        //GnuPlot.SaveData(X, Y, tempfolder+ "plot1.data");
        //GnuPlot.Plot(tempfolder+ "plot1.data", "with linespoints pt " + (int)PointStyles.SolidDiamond);
        //Thread.Sleep(2000);

        //GnuPlot.HoldOn();
        //GnuPlot.Set("xrange [-25:40]");
        //GnuPlot.Set("yrange [-15:15]");
        //var r = new Random();
        //for (int i = 0; i < 14; i++)
        //{
        //    var Xr = new double[10];
        //    var Yr = new double[10];
        //    double rx=r.Next(-20,20);
        //    double ry = r.Next(-10, 10);
        //    for (int di = 0; di < 10; di++)
        //    {
        //        Xr[di] = rx+r.Next(-5, 5);
        //        Yr[di] = ry+r.Next(-3, 3);
        //    }

        //    GnuPlot.Plot(Xr, Yr, "title 'point style "+i+"' pt " + i);
        //    Thread.Sleep(200);
        //}
        //GnuPlot.HoldOff();
        //Thread.Sleep(3000);


        ////splot demos
        //double[] z = new double[31 * 31];
        //for (int x = 0; x < 31; x++)
        //    for (int y = 0; y < 31; y++)
        //        z[31 * x + y] = (x - 15) * (x - 15) + (y - 15) * (y - 15);
        //GnuPlot.Set("pm3d");
        //GnuPlot.Set("autoscale");
        //GnuPlot.Set("contour base");
        //GnuPlot.SPlot(31,z);
        //Thread.Sleep(2000);

        //GnuPlot.HoldOn();
        //GnuPlot.Set("view map");
        //GnuPlot.Unset("surface");
        //GnuPlot.Set("cntrparam levels 10");
        //GnuPlot.Set("palette gray");
        //GnuPlot.SPlot(31, z);
        //Thread.Sleep(2000);

        //Console.WriteLine("End of demo");





        //GnuPlot.Plot("sin(x) + 2");

        //GnuPlot.Plot("sin(x) + 2", "lc rgb \"magenta\" lw 5");

        //double[] Y = new double[] { -4, 6.5, -2, 3, -8, -5, 11, 4, -5, 10 };
        //GnuPlot.Plot(Y);

        //double[] X = new double[] { -10, -8.5, -2, 1, 6, 9, 10, 14, 15, 19 };
        //double[] Y = new double[] { -4, 6.5, -2, 3, -8, -5, 11, 4, -5, 10 };
        //GnuPlot.Plot(X, Y);

        //GnuPlot.HoldOn();
        //GnuPlot.Plot("cos(x) + x");
        //GnuPlot.Plot("cos(2*x)", "with points pt 3");

        
        //GnuPlot.SPlot("1 / (.05*x*x + .05*y*y + 1)");

        //GnuPlot.Set("isosamples 30");
        //GnuPlot.SPlot("1 / (.05*x*x + .05*y*y + 1)");

        //GnuPlot.Set("isosamples 30", "hidden3d");
        //GnuPlot.SPlot("1 / (.05*x*x + .05*y*y + 1)");

        //GnuPlot.SPlot("splotexampledata.txt");

        double[] Z = new double[] { -4, -2.5, 1, 3,    -3, -2, 3, 4,    -1, 2, 6, 8 };
        GnuPlot.Set("pm3d");
        GnuPlot.SPlot(4, Z);

        Console.ReadKey();

    }
}

