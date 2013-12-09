using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OculusionIndependent
{
    public partial class Form1 : Form
    {
        Sketch sketch = null;

        public Form1()
        {
            InitializeComponent();

            PointF pt;

            System.Diagnostics.Debug.Assert(Global.Distance(
                new PointF(0, 0),
                new PointF(-1, -1),
                new PointF(1, 1),
                out pt) <= 1);


            sketchControl1.actionOK += SketchControl_ActionOK;
        }

        private void TJunctionCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        class Circle
        {
            public float x = 0;
            public float y = 0;
            public float r = 1;
            public float k { get { return 1 / r; } }
            public Circle(float x, float y, float r)
            {
                System.Diagnostics.Debug.Assert(r > 0);
                this.x = x;
                this.y = y;
                this.r = r;
            }
        }

        void SketchControl_ActionOK(Sketch sketch)
        {
            this.sketch = sketch;

            List<Tuple<int, int>>  TJunctions = GetTJunctions(sketch);
            List<List<StrokePoint>> salientCurves = GetSalientCurves(TJunctions, sketch);
            List<Circle> extrapolatedCircles = GetExtrapolatedCircles(salientCurves);
            Dictionary<int, int> curveMatchings = GetCurveMatchings(salientCurves, extrapolatedCircles);

            using (var g = Graphics.FromImage(sketch.AnnotationBmp))
            {
                g.Clear(Color.Transparent);

                if (bridgeCheckBox.Checked)
                {
                    foreach (var kv in curveMatchings)
                    {
                        var curve0 = new List<Point>(salientCurves[kv.Key].Select(pt => new Point((int)pt.x, (int)pt.y)).Reverse());
                        var curve1 = new List<Point>(salientCurves[kv.Value].Select(pt => new Point((int)pt.x, (int)pt.y)));
                        Point r = new Point(
                            (int)(salientCurves[kv.Key][0].x + salientCurves[kv.Value][0].x) / 2,
                            (int)(salientCurves[kv.Key][0].y + salientCurves[kv.Value][0].y) / 2);

                        List<Point> bridgeCurve = new List<Point>();
                        bridgeCurve.Add(curve0[0]);
                        bridgeCurve.Add(curve0.Last());
//                        bridgeCurve.Add(r);
                        bridgeCurve.Add(curve1[0]);
                        bridgeCurve.Add(curve1.Last());
                        
                        g.DrawCurve(new Pen(Brushes.Yellow, 3), bridgeCurve.ToArray());
                    }
                }

                if (salientCurveCheckBox.Checked)
                {
                    foreach (var curve in salientCurves)
                    {
                        if (curve.Count <= 2) continue;
                        g.DrawCurve(new Pen(Brushes.Blue, 3), curve.Select(pt => new Point((int)pt.x, (int)pt.y)).ToArray());
                    }
                }
                if (circleCheckBox.Checked)
                {
                    foreach (var circle in extrapolatedCircles)
                    {
                        g.DrawEllipse(new Pen(Brushes.Green, 3), circle.x - circle.r, circle.y - circle.r, circle.r * 2, circle.r * 2);
                    }
                }
                if (TJunctionCheckBox.Checked)
                {
                    foreach (var j in TJunctions.Select(pos => sketch.Strokes[pos.Item1][pos.Item2]))
                    {
                        int r = 3;
                        g.FillEllipse(Brushes.Red, new Rectangle((int)j.x - r, (int)j.y - r, 2 * r, 2 * r));
                    }
                }

            }

            sketchControl1.Repaint();
        }

        List<Tuple<int, int>> GetTJunctions(Sketch sketch)
        {
            List<Tuple<int, int>> TJunctions = new List<Tuple<int, int>>();

            for (int i = 0; i < sketch.Strokes.Count; i++)
            {
                if ( sketch.Strokes[i].Count < 2) continue;
                foreach (int idx in new [] { 0, sketch.Strokes[i].Count - 1 })
                {
                    var e = sketch.Strokes[i][idx];
                    for (int j = 0; j < sketch.Strokes.Count; j++)
                    {
                        if (i == j) continue;

                        float minDist = float.MaxValue;
                        PointF minCrossPoint = PointF.Empty;

                        for (int k = 0; k < sketch.Strokes[j].Count - 1; k++)
                        {
                            StrokePoint pt0 = sketch.Strokes[j][k];
                            StrokePoint pt1 = sketch.Strokes[j][k + 1];
                            PointF crossPoint;
                            float dist = Global.Distance(new PointF(e.x, e.y), new PointF(pt0.x, pt0.y), new PointF(pt1.x, pt1.y), out crossPoint);
                            if (dist < minDist)
                            {
                                minDist = dist;
                                minCrossPoint = crossPoint;
                            }
                        }

                        if (minDist <= Global.TJunctionDistThreshold)
                        {
                            TJunctions.Add(new Tuple<int, int>(i, idx));
                        }
                    }
                }
            }

            return TJunctions;
        }

        public List<List<StrokePoint>> GetSalientCurves(List<Tuple<int, int>> TJunctions, Sketch sketch)
        {
            List<List<StrokePoint>> curves = new List<List<StrokePoint>>(); ;
            foreach (var pos in TJunctions)
            {
                curves.Add(new List<StrokePoint>());
                float curveLength = 0;
                var stroke = sketch.Strokes[pos.Item1];
                int cur = pos.Item2;
                int seekDir = cur <= 0 ? 1 : -1;
                while (true)
                {
                    curves.Last().Add(stroke[cur]);
                    cur += seekDir * Global.SalientCurveSkip;
                    if (curves.Last().Count >= 2)
                    {
                        var pt0 = curves.Last()[curves.Last().Count - 1];
                        var pt1 = curves.Last()[curves.Last().Count - 2];
                        var dx = pt0.x - pt1.x;
                        var dy = pt0.y - pt1.y;
                        float length = dx * dx + dy* dy;
                        curveLength +=length;
                        if (curveLength >= Global.SalientCurveLength) break;
                    }
                    if (cur < 0 || stroke.Count <= cur) break;
                }
            }
            return curves;
        }

        private void sketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            sketchControl1.DrawSketch = sketchCheckBox.Checked;
            sketchControl1.Repaint();
        }

        private List<Circle> GetExtrapolatedCircles(List<List<StrokePoint>> salientCurves)
        {
            List<Circle> circles = new List<Circle>();

            for (int i = 0; i < salientCurves.Count; i++)
            {
                float[] dxs = new float[salientCurves[i].Count - 1];
                float[] dys = new float[salientCurves[i].Count - 1];
                float[] ddxs = new float[salientCurves[i].Count - 2];
                float[] ddys = new float[salientCurves[i].Count - 2];
                for (int j = 0; j < dxs.Length; j++)
                {
                    dxs[j] = salientCurves[i][j + 1].x - salientCurves[i][j].x;
                    dys[j] = salientCurves[i][j + 1].y - salientCurves[i][j].y;
                    float dx = salientCurves[i][j + 1].x - salientCurves[i][j].x;
                    float dy = salientCurves[i][j + 1].y - salientCurves[i][j].y;
                    float len = (float)Math.Sqrt(dx * dx + dy * dy);
                    dxs[j] /= len;
                    dys[j] /= len;
                }
                for (int j = 0; j < ddxs.Length; j++)
                {
                    ddxs[j] = dxs[j + 1] - dxs[j];
                    ddys[j] = dys[j + 1] - dys[j];
                    float dx = salientCurves[i][j + 1].x - salientCurves[i][j].x;
                    float dy = salientCurves[i][j + 1].y - salientCurves[i][j].y;
                    float len = (float)Math.Sqrt(dx * dx + dy * dy);
                    ddxs[j] /= len;
                    ddys[j] /= len;
                }
                // 曲率計算
                float k = 0;
                for (int j = 0; j < ddxs.Length; j++)
                {
                    k += (float)Math.Sqrt(ddxs[j] * ddxs[j] + ddys[j] * ddys[j]);
                }
                k /= ddxs.Length;

                float r = 1 / k;
                float vx = salientCurves[i][0].x - salientCurves[i][1].x;
                float vy = salientCurves[i][0].y - salientCurves[i][1].y;
                float length = (float)Math.Sqrt( vx * vx + vy * vy);
                float nx = -vy / length;
                float ny = vx / length;


//                float dir = (float)((dxs[0] * dxs[1] + dys[0] * dys[1] >= 0 ? -1 : 1) * (vx == 0 ? 1 : vx / Math.Abs(vx)));

                float dir = 0;
                for (int j = 0; j < dxs.Length - 1; j++)
                {
                    dir += dxs[j] * dxs[j + 1] + dys[j] * dys[j + 1];
                }
                dir = (float)(dir >= 0 ? -1 : 1) * (vx == 0 ? 1 : vx / Math.Abs(vx));

                float cx = salientCurves[i][0].x + dir * nx * r;
                float cy = salientCurves[i][0].y + dir * ny * r;

                circles.Add(new Circle(cx, cy, r));
            }

            return circles;
        }

        private Dictionary<int, int> GetCurveMatchings(List<List<StrokePoint>> salientCurves, List<Circle> extrapolatedCircles)
        {
            Dictionary<int, int> matchings = new Dictionary<int, int>();

            Dictionary<Tuple<int, int>, float> matchingValues = new Dictionary<Tuple<int, int>, float>();

            // 全探索して、カーブの
            for (int i = 0; i < salientCurves.Count - 1; i++)
            {
                for (int j = i + 1; j < salientCurves.Count; j++)
                {
                    float rx = (salientCurves[i][0].x + salientCurves[j][0].x) / 2;
                    float ry = (salientCurves[i][0].y + salientCurves[j][0].y) / 2;

                    float match = 0;
                    List<PointF> boundDirs = new List<PointF>();
                    foreach (int idx in new [] {i, j})
                    {
                        float ex = salientCurves[idx][0].x;
                        float ey = salientCurves[idx][0].y;

                        float vx = rx - extrapolatedCircles[idx].x;
                        float vy = ry - extrapolatedCircles[idx].y;
                        float vlen = (float)Math.Sqrt(vx * vx + vy * vy);
                        float ax = extrapolatedCircles[idx].x + vx / vlen * extrapolatedCircles[idx].r;
                        float ay = extrapolatedCircles[idx].y + vy / vlen * extrapolatedCircles[idx].r;

                        float dx0 = rx - ax;
                        float dy0 = ry - ay;
                        float dx1 = rx - ex;
                        float dy1 = ry - ey;

                        match += (float)(
                            Math.Exp(-(dx0 * dx0 + dy0 * dy0) / (0.02 * 0.02)) * 
                            Math.Exp(-(dx1 * dx1 + dy1 * dy1) / (0.02 * 0.02)));

                        boundDirs.Add(new PointF(salientCurves[idx][0].x - salientCurves[idx][2].x, salientCurves[idx][0].y - salientCurves[idx][2].y));
                    }
                    match /= 2;

                    float cos = boundDirs[0].X * boundDirs[1].X + boundDirs[0].Y * boundDirs[1].Y;
                    if (cos < 0)
                    {
                        matchingValues.Add(new Tuple<int, int>(i, j), match);
                    }
                }
            }

            var sortedMatchings = matchingValues.OrderBy(kv => kv.Value).ToArray();
            HashSet<int> check = new HashSet<int>();

            foreach (var kv in sortedMatchings)
            {
                int i = kv.Key.Item1;
                int j = kv.Key.Item2;
                if (!check.Contains(i) && !check.Contains(j) && kv.Value >= Global.CurveMatchingThreshold)
                {
                    check.Add(i);
                    check.Add(j);
                    matchings.Add(i, j);
                }
            }

            return matchings;
        }

        private void bridgeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sketch != null) SketchControl_ActionOK(sketch);
        }
    }
}
