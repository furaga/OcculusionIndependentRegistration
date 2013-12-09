using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OculusionIndependent
{
    public partial class SketchControl : UserControl
    {
        Sketch sketch;

        public Action<Sketch> actionOK;
        public Action<Sketch> actionCancel;
        public bool DrawSketch = true;
        public bool DrawAnnotation = true;


        public SketchControl()
        {
            InitializeComponent();

            canvas.MouseWheel += canvas_MouseWheel;
        }

        void canvas_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void SketchControl_Load(object sender, EventArgs e)
        {
            sketch = new Sketch(canvas.Width, canvas.Height);
            clear_Click(null, null);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (actionOK != null) actionOK(sketch);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (actionCancel != null) actionCancel(sketch);
        }
        
        private void clear_Click(object sender, EventArgs e)
        {
            sketch.Clear();
            canvas.Invalidate();
        }

        private void repaintButton_Click(object sender, EventArgs e)
        {
            sketch.RedrawStrokes();
            canvas.Invalidate();
        }



        //--------------------------------------------------
        // マウスによるスケッチ
        //--------------------------------------------------

        float strokeWidth = 1;
        Color strokeColor = Color.Black;
        bool strokeDrawing = false;

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            StrokePoint pt = new StrokePoint(e.Location, strokeColor, strokeWidth);
            sketch.AddPoint(pt);
      //      canvas.Focus();
            strokeDrawing = true;
            canvas.Invalidate();
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            sketch.NewStroke();
     //       canvas.Focus();
            strokeDrawing = false;
            canvas.Invalidate();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!strokeDrawing) return;
            StrokePoint pt = new StrokePoint(e.Location, strokeColor, strokeWidth);
            sketch.AddPoint(pt);
 //           canvas.Focus();
            canvas.Invalidate();
        }

        System.Drawing.Drawing2D.Matrix transform = new System.Drawing.Drawing2D.Matrix();

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            sketch.DrawLastLine();
            e.Graphics.Transform = transform;
            e.Graphics.Clear(Color.White);
            if (DrawSketch) e.Graphics.DrawImage(sketch.Bmp, Point.Empty);
            if (DrawAnnotation) e.Graphics.DrawImage(sketch.AnnotationBmp, Point.Empty);
            e.Graphics.Transform = new System.Drawing.Drawing2D.Matrix();
        }


        private void colorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                strokeColor = colorDialog.Color;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            strokeWidth = trackBar1.Value;
            label1.Text = "" + trackBar1.Value;
        }


        public void Repaint()
        {
            canvas.Invalidate();
        }
    }
}
