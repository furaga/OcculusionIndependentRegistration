using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OculusionIndependent
{
    public class StrokePoint
    {
        public float x, y;
        public Color color;
        public float width;

        public StrokePoint(Point pt, Color color, float width)
        {
            this.x = pt.X;
            this.y = pt.Y;
            this.color = color;
            this.width = width;
        }
    }

    public class Sketch
    {
        public List<List<StrokePoint>> Strokes { get; set; }
        public Bitmap AnnotationBmp { get; set; }
        public Bitmap Bmp { get; set; }

        public Sketch(int w, int h)
        {
            Strokes = new List<List<StrokePoint>>();
            Bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            AnnotationBmp = new Bitmap(Bmp);
            NewStroke();
        }

        public void Clear()
        {
            Strokes.Clear();
            using (var g = Graphics.FromImage(Bmp))
            {
                g.Clear(Color.Transparent);
            }
            using (var g = Graphics.FromImage(AnnotationBmp))
            {
                g.Clear(Color.Transparent);
            }
            NewStroke();
        }

        public void NewStroke()
        {
            Strokes.Add(new List<StrokePoint>());
        }

        public void AddPoint(StrokePoint pt)
        {
            System.Diagnostics.Debug.Assert(Strokes.Count >= 1);
            if (Strokes.Last().Count >= 1 && Global.Distance(new PointF(pt.x, pt.y), new PointF(Strokes.Last().Last().x, Strokes.Last().Last().y)) <= 2) return;
            Strokes.Last().Add(pt);
        }

        public void DrawLastLine()
        {
            if (Strokes.Count <= 0) return;
            if (Strokes.Last().Count <= 1) return;
            StrokePoint prevPt = Strokes.Last()[Strokes.Last().Count - 2];
            StrokePoint pt = Strokes.Last()[Strokes.Last().Count - 1];
            using (var g = Graphics.FromImage(Bmp))
            {
                DrawOneLine(g, pt.color, prevPt.x, prevPt.y, pt.x, pt.y, pt.width);
            }
        }

        public void RedrawStrokes()
        {
            using (var g = Graphics.FromImage(Bmp))
            {
                g.Clear(Color.Transparent);
                for (int i = 0; i < Strokes.Count; i++)
                {
                    StrokePoint prevPt = null;
                    for (int j = 0; j < Strokes[i].Count; j++)
                    {
                        StrokePoint pt = Strokes[i][j];
                        if (prevPt != null)
                        {
                            DrawOneLine(g, pt.color, prevPt.x, prevPt.y, pt.x, pt.y, pt.width);
                        }
                        prevPt = pt;
                    }
                }
            }
            using (var g = Graphics.FromImage(AnnotationBmp))
            {
                g.Clear(Color.Transparent);
            }
        }

        void DrawOneLine(Graphics g, Color color, float x0, float y0, float x1, float y1, float width)
        {
            g.DrawLine(new Pen(new SolidBrush(color), 2 * width), x0, y0, x1, y1);
            g.FillEllipse(new SolidBrush(color), x1 - width, y1 - width, 2 * width, 2 * width);
        }
    }
}

