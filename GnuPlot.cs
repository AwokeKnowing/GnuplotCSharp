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

        public static void HoldOn()
        {
            Hold = true;
            
        }
        public static void HoldOff()
        {
            Hold = false;
            PlotBuffer.Clear();
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


        
    }
        
    
}
