using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AwokeKnowing.GnuplotCSharp
{
    class GnuPlot
    {
        private static Process ExtPro;
        private static StreamWriter GnupStWr;
        private static List<StoredPlot> PlotBuffer;
        private static List<StoredPlot> SPlotBuffer;

        public static bool Hold { get; private set; }
        
        static GnuPlot()
        {
            string Pgm = @"C:\gnuplot\bin\gnuplot.exe";
            ExtPro = new Process();
            ExtPro.StartInfo.FileName = Pgm;
            ExtPro.StartInfo.UseShellExecute = false;
            ExtPro.StartInfo.RedirectStandardInput = true;
            ExtPro.Start();
            GnupStWr = ExtPro.StandardInput;
            PlotBuffer = new List<StoredPlot>();
            SPlotBuffer = new List<StoredPlot>();
        }

        public static void WriteLine(string gnuplotcommands)
        {

            GnupStWr.WriteLine(gnuplotcommands);
            GnupStWr.Flush();
        }

        public static void Write(string gnuplotcommands)
        {
            GnupStWr.Write(gnuplotcommands);
            GnupStWr.Flush();
        }

        public static void Set(string options)
        {
            GnupStWr.WriteLine("set "+options);
        }

        public static void Unset(string options)
        {
            GnupStWr.WriteLine("unset " + options);
        }

        public static bool SaveData(double[] X,double[] Y, string path="",string filename="temp.data")
        { 
            StreamWriter data = new StreamWriter(path + filename,false);
            int m = Math.Min(X.Length, Y.Length);
            for(int i =0;i<m;i++)
                data.WriteLine(X[i].ToString()+ " " + Y[i].ToString());
            data.Close();

            return true;
        }

        public static void Plot(string path, string filename, string options="")
        {
            if (Hold)
            {
                PlotBuffer.Add(new StoredPlot(path,filename, options));
                Plot(PlotBuffer);
            }
            else
            {
                path = path.Replace(@"\", @"\\");
                GnupStWr.WriteLine(@"plot """ +path+filename +@""" " + options);
                GnupStWr.Flush();
            }
            
        }

        public static void Plot(string formula, string options="")
        {
            if (Hold)
            {
                PlotBuffer.Add(new StoredPlot(formula,options));
                Plot(PlotBuffer);
            }
            else
            {
                GnupStWr.WriteLine("plot " +formula + " " + options);
                GnupStWr.Flush();
            }
                
        }

        public static void Plot(double[] x, double[] y, string options="")
        {
            if (Hold)
            {
                PlotBuffer.Add(new StoredPlot(x,y,options));
                Plot(PlotBuffer);
            }
            else
            {
                GnupStWr.WriteLine(@"plot ""-"" "+ options);
                int m = Math.Min(x.Length, y.Length);
                for (int i = 0; i < m; i++)
                    GnupStWr.WriteLine(x[i] + " " + y[i]);
                GnupStWr.WriteLine("e");
                GnupStWr.Flush();
            }
                
        }

        public static void SPlot(int sizeX, int sizeY,double[] z, string options="with lines")
        {
            if (Hold)
            {
                SPlotBuffer.Add(new StoredPlot(sizeX, sizeY, z, options));
                SPlot(SPlotBuffer);
                //http://gnuplot-tricks.blogspot.com/2009/07/maps-contour-plots-with-labels.html
            }
            else
            {
                GnupStWr.WriteLine(@"splot ""-"" " + options);
                int i = 0;
                for (int x = 0; x < sizeX; x++)
                {
                    for (int y = 0; y < sizeY; y++, i++)
                        GnupStWr.WriteLine(z[i]);
                    GnupStWr.WriteLine();
                }
                GnupStWr.WriteLine("e");
                GnupStWr.Flush();
            }
        }

        public static void Plot(List<StoredPlot> storedPlots)
        {
            var plot = "plot ";
            
            for (int i=0;i<storedPlots.Count;i++)
            {
                if (storedPlots[i].File!="")
                    GnupStWr.Write(plot + storedPlots[i].File +" "+ storedPlots[i].Options);
                if(storedPlots[i].Formula!="")
                    GnupStWr.Write(plot + storedPlots[i].Formula + " " + storedPlots[i].Options);
                if(storedPlots[i].Y!=null && storedPlots[i].Y.Length>0 && storedPlots[i].X.Length>0)
                    GnupStWr.Write(plot + @"""-"" " + storedPlots[i].Options);
                if (i == 0) plot = ",";
            }
            GnupStWr.WriteLine("");
            for (int i = 0; i < storedPlots.Count; i++)
            {
                if (storedPlots[i].Y != null && storedPlots[i].Y.Length > 0 && storedPlots[i].X.Length > 0)
                {
                    int m = Math.Min(storedPlots[i].X.Length, storedPlots[i].Y.Length);
                    for (int di = 0; di < m; di++)
                        GnupStWr.WriteLine(storedPlots[i].X[di] + " " + storedPlots[i].Y[di]);
                    GnupStWr.WriteLine("e");
                }
            }
                
            GnupStWr.Flush();
        }

        public static void SPlot(List<StoredPlot> storedPlots)
        {

            var splot = "splot ";
            for (int i = 0; i < storedPlots.Count; i++)
            {
                if (storedPlots[i].File != "")
                    GnupStWr.Write(splot + storedPlots[i].File + " " + storedPlots[i].Options);
                if (storedPlots[i].Formula != "")
                    GnupStWr.Write(splot + storedPlots[i].Formula + " " + storedPlots[i].Options);
                if (storedPlots[i].YSize != null && storedPlots[i].XSize !=null && storedPlots[i].Z.Length > 0)
                    GnupStWr.Write(splot + @"""-"" " + storedPlots[i].Options);
                if (storedPlots[i].Y !=null && storedPlots[i].X.Length > 0 && storedPlots[i].Y.Length > 0 && storedPlots[i].Z.Length > 0)
                    GnupStWr.Write(splot + @"""-"" " + storedPlots[i].Options);
                if (i == 0) splot = ",";
            }
            GnupStWr.WriteLine("");

            for (int i = 0; i < storedPlots.Count; i++)
            {
                if (storedPlots[i].YSize != null && storedPlots[i].XSize != null && storedPlots[i].Z.Length > 0)
                {
                    int sizeX = (int)storedPlots[i].XSize;
                    int sizeY = (int)storedPlots[i].YSize;
                    
                    int zi = 0;
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++, zi++)
                            GnupStWr.WriteLine(storedPlots[i].Z[zi]);
                        GnupStWr.WriteLine();
                    }
                    GnupStWr.WriteLine("e");
                    GnupStWr.Flush();
                }

                if (storedPlots[i].Y != null && storedPlots[i].X.Length > 0 && storedPlots[i].Y.Length > 0 && storedPlots[i].Z.Length > 0)
                {
                    int m = storedPlots[i].Y.Length;
                    for (int di = 0; di < m; di++)
                        GnupStWr.WriteLine(storedPlots[i].X[di] + " " + storedPlots[i].Y[di] + " " + storedPlots[i].Z[di]);
                    GnupStWr.WriteLine("e");
                }
            }
        }

        public static void HoldOn()
        {
            Hold = true;
            
        }
        public static void HoldOff()
        {
            Hold = false;
            PlotBuffer.Clear();
            SPlotBuffer.Clear();
        }

        public static void Close()
        { 
            ExtPro.CloseMainWindow();
        }


       
    }

    enum PointStyles
    {
        Dot = 0,
        Plus = 1,
        X = 2,
        Star = 3,
        DotSquare = 4,
        SolidSquare = 5,
        DotCircle = 6,
        SolidCircle = 7,
        DotTriangleUp = 8,
        SolidTriangleUp = 9,
        DotTriangleDown = 10,
        SolidTriangleDown = 11,
        DotDiamond = 12,
        SolidDiamond = 13
    }
    
    class StoredPlot
    {
        public string File="";
        public string Formula = "";
        public double[] X;
        public double[] Y;
        public double[] Z;
        public int? XSize;
        public int? YSize;
        public string Options;

        public StoredPlot()
        { 
        }
        public StoredPlot(string path, string filename, string options)
        {
            if (path == null) 
                path = "";
            else
                path = path.Replace(@"\", @"\\");
            File = "\"" +path + filename+"\"";
            Options = options;
        }
        public StoredPlot(string formula, string options)
        {
            Formula = formula;
            Options = options;
        }
        public StoredPlot(double[] x, double[] y, string options)
        {
            X = x;
            Y = y;
            Options = options;
        }

        public StoredPlot(int sizeX, int sizeY, double[] z,string options)
        {
            XSize = sizeX;
            YSize = sizeY;
            Z = z;
            Options = options;
        }


        
    }
        
    
}
