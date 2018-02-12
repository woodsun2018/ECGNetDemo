using System;
using Android.Widget;
using Android.Graphics;

namespace AndroidApp
{
    public class DrawBioWave
    {
        //绘图控件
        private ImageView imgWave = null;

        //画布
        private Canvas cavWave = null;

        //位图
        private Bitmap bmpWave = null;

        //生理参数数组
        public byte[] BioBuf { get; set; }

        //生理参数基线
        public int BioBaselien { get; set; }

        public DrawBioWave(ImageView imageWave, byte[] bioBuf, int bioBaselien = 0)
        {
            this.imgWave = imageWave;
            this.BioBuf = bioBuf;
            this.BioBaselien = bioBaselien;
        }

        //创建画布
        private void CreateCanvas()
        {
            if (imgWave == null)
                return;

            if (cavWave != null)
                return;

            //获取图片控件大小
            int width = imgWave.Width;
            int height = imgWave.Height;

            System.Diagnostics.Debug.Print($"{DateTime.Now}, CreateCanvas: width={width}, height={height}");

            //根据控件创建位图
            bmpWave = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);

            //根据位图创建画布
            cavWave = new Canvas(bmpWave);
        }

        //绘制生理参数波形
        public void DrawWave()
        {
            //创建画布
            CreateCanvas();

            if (cavWave == null)
                return;

            //首先清屏
            cavWave.DrawColor(Color.White);

            if (BioBuf.Length > 0)
            {
                //创建画笔
                Paint patWave = CreatePaint(Color.Blue, Paint.Style.Fill);

                //画曲线
                int x = 0;
                int y1 = 0;
                int y2 = 0;
                for (int i = 0; i < BioBuf.Length; i++)
                {
                    x = i;
                    y1 = y2;
                    y2 = WaveHeight(BioBuf[i]);

                    cavWave.DrawLine(x, y1, x + 1, y2, patWave);
                }

                //画基线
                if (BioBaselien > 0)
                {
                    //创建画笔
                    Paint patBaseline = CreatePaint(Color.Red, Paint.Style.Fill);

                    y2 = WaveHeight(BioBaselien);
                    cavWave.DrawLine(0, y2, BioBuf.Length, y2, patBaseline);
                }
            }

            //创建画笔，画空心长方形
            Paint patBorder = CreatePaint(Color.Gray, Paint.Style.Stroke);

            //画边界
            cavWave.DrawRect(0, 0, bmpWave.Width - patBorder.StrokeWidth, bmpWave.Height - patBorder.StrokeWidth, patBorder);

            //将绘图内容刷新到控件
            imgWave.SetImageBitmap(bmpWave);

            System.Diagnostics.Debug.Print($"{DateTime.Now}, DrawWave");
        }

        //计算波形高度
        private int WaveHeight(int waveData)
        {
            int height = bmpWave.Height - (waveData * bmpWave.Height / 255);

            return height;
        }

        //创建画笔
        private Paint CreatePaint(Color color, Paint.Style style, int strokeWidth = 3)
        {
            Paint paint = new Paint();
            paint.Color = color;
            paint.SetStyle(style);
            paint.StrokeWidth = strokeWidth;

            return paint;
        }

    }
}