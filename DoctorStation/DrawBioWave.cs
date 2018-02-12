using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoctorStation
{
    public class DrawBioWave
    {
        //绘图控件
        private PictureBox imgWave = null;

        //画布
        private Graphics cavWave = null;

        //位图
        private Bitmap bmpWave = null;

        //生理参数数组
        public byte[] BioBuf { get; set; }

        //生理参数基线
        public int BioBaselien { get; set; }

        public DrawBioWave(PictureBox imageWave, byte[] bioBuf, int bioBaselien = 0)
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

            //创建位图
            bmpWave = new Bitmap(width, height);

            //从位图创建画布
            cavWave = Graphics.FromImage(bmpWave);
        }

        //绘制生理参数波形
        public void DrawWave()
        {
            //创建画布
            CreateCanvas();

            if (cavWave == null)
                return;

            //首先清屏
            cavWave.Clear(Color.White);

            if (BioBuf.Length > 0)
            {
                //创建画笔
                Pen penWave = CreatePen(Color.Black);

                //画曲线
                int x = 0;
                int y1 = 0;
                int y2 = 0;
                for (int i = 0; i < BioBuf.Length; i++)
                {
                    x = i;
                    y1 = y2;
                    y2 = WaveHeight(BioBuf[i]);

                    cavWave.DrawLine(penWave, x, y1, x + 1, y2);
                }

                //画基线
                if (BioBaselien > 0)
                {
                    y2 = WaveHeight(BioBaselien);
                    cavWave.DrawLine(Pens.Red, 0, y2, BioBuf.Length, y2);
                }
            }

            //将绘图内容刷新到控件
            imgWave.Image = bmpWave;
            imgWave.Refresh();

            System.Diagnostics.Debug.Print($"{DateTime.Now}, DrawWave");
        }

        //计算波形高度
        private int WaveHeight(int waveData)
        {
            int height = bmpWave.Height - (waveData * bmpWave.Height / 255);

            return height;
        }

        //创建画笔
        private Pen CreatePen(Color color, int width = 1)
        {
            Pen pen = new Pen(color);
            pen.Width = width;

            return pen;
        }

    }//DrawBioWave
}
