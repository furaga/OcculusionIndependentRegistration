using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OculusionIndependent
{
    class Global
    {
        public const float TJunctionDistThreshold = 10f;
        public const int SalientCurveSkip= 3;
        public const int SalientCurveLength = 800;//
        public const float CurveMatchingThreshold = float.MinValue;

        public static float Distance(PointF pt, PointF linePt0, PointF linePt1, out PointF crossPoint)
        {
            const double e = 0.01f;
            double linex = linePt1.X - linePt0.X;
            double liney = linePt1.Y - linePt0.Y;
            double length = Math.Sqrt(linex * linex + liney * liney);
            double nx = linex / length;
            double ny = liney / length;
            double t = nx * (pt.X - linePt0.X) + ny * (pt.Y - linePt0.Y);
            double crossx = linePt0.X + nx * t;
            double crossy = linePt0.Y + ny * t;
            double dx = crossx - pt.X;
            double dy = crossy - pt.Y;
            crossPoint = new PointF((float)crossx, (float)crossy);
            if (0 - e <= t && t <= length + e) return (float)(dx * dx + dy * dy);
            return float.MaxValue;
        }
        public static float Distance(PointF pt0, PointF pt1)
        {
            float dx = pt0.X - pt1.X;
            float dy = pt0.Y - pt1.Y;
            return (float) Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
