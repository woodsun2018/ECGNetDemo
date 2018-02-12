using CoreGraphics;
using System;
using UIKit;

namespace iOSApp
{
    public class DrawBioWave : UIView
    {
        private int MaxHeight = 100;

        //生理参数数组
        public byte[] BioBuf { get; set; }

        //生理参数基线
        public int BioBaselien { get; set; }

        public DrawBioWave(byte[] bioBuf, int bioBaselien = 0)
        {
            this.BioBuf = bioBuf;
            this.BioBaselien = bioBaselien;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            DrawWave(rect);
        }

        //绘图
        private void DrawWave(CGRect rect)
        {
            //获取图片控件大小
            int width = (int)rect.Width;
            int height = (int)rect.Height;

            System.Diagnostics.Debug.Print($"{DateTime.Now}, DrawWave: width={width}, height={height}");

            MaxHeight = height;

            CGContext context = UIGraphics.GetCurrentContext();

            if (BioBuf.Length > 0)
            {
                //创建画笔
                context.SetLineWidth(1);//画笔线宽
                context.SetStrokeColor(UIColor.Blue.CGColor);//画笔颜色

                //画曲线
                int x = 0;
                int y1 = 0;
                int y2 = 0;
                context.MoveTo(x, y1);//起点

                for (int i = 0; i < BioBuf.Length; i++)
                {
                    x = i;
                    y1 = y2;
                    y2 = WaveHeight(BioBuf[i]);

                    context.AddLineToPoint(x + 1, y2);//终点
                }

                context.StrokePath();//画线

                //画基线
                if (BioBaselien > 0)
                {
                    //创建画笔
                    context.SetStrokeColor(UIColor.Red.CGColor);//画笔颜色

                    y2 = WaveHeight(BioBaselien);
                    context.MoveTo(0, y2);//起点
                    context.AddLineToPoint(BioBuf.Length, y2);//终点

                    context.StrokePath();//画线}
                }
            }

            //创建画笔
            context.SetStrokeColor(UIColor.Gray.CGColor);//画笔颜色

            //画边界
            context.AddRect(new CGRect(0, 0, width, height));
            context.StrokePath();//画线

            System.Diagnostics.Debug.Print($"{DateTime.Now}, DrawWave");
        }

        //计算波形高度
        private int WaveHeight(int waveData)
        {
            int height = MaxHeight - (waveData * MaxHeight / 255);

            return height;
        }

    }

}