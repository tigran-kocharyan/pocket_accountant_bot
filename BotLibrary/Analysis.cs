using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;


namespace BotLibrary
{
    public class Analysis
    {
        public static void GraphicAnalysis(List<double> purchases, List<DateTime> dates, long id)
        {
            double maxPrice = purchases.Max();

            // Размеры изображения с графиком.
            int maxWidth = 20 + purchases.Count * 100 + 200;
            int maxHeight = 30 + (int)maxPrice / 10 + 200;

            // Инкапсулирует точечный рисунок GDI+, состоящий из данных пикселей графического 
            // изображения и атрибутов рисунка. 
            // Объект Bitmap используется для работы с изображениями, определяемыми данными пикселей.
            var map = new Bitmap(maxWidth, maxHeight);

            // Точка, откуда будет прорисовка после создания осей X и Y.
            int height = maxHeight - 30;

            List<double> moneys = new List<double>();
            for (int i = 0; i < purchases.Count; i++)
            {
                Console.WriteLine(purchases[i]);
                moneys.Add(height - purchases[i] / 10);
            }

            //// Список всех покупок, которые будут на графике.
            //List<int> moneys = new List<int>
            //{ height - 100, height - 150, height - 200, height - 150, height - 300, height - 400 }
            ;
            // Список всех покупок, которые будут на графике.
            List<PointF> curve = new List<PointF> { new Point(20, height) };
            for (int i = 0; i < moneys.Count; i++)
            {
                PointF temp = new PointF(curve.Last().X + 100, (float)moneys[i]);
                curve.Add(temp);
            }
            using (Graphics graphics = Graphics.FromImage(map))
            {
                // Делаем белый фон для графика.
                graphics.Clear(Color.FromArgb(237, 238, 240));

                // Чертим нашу функцию.
                graphics.DrawLines(new Pen(Color.Red, 5), curve.ToArray());

                // Чертим оси X и Y.
                graphics.DrawLine(new Pen(Color.Blue, 5),
                                  new Point(20, 0),
                                  new Point(20, maxHeight));
                graphics.DrawLine(new Pen(Color.Blue, 5),
                                  new Point(0, height),
                                  new Point(maxWidth, height));

                // Для каждой точки растрат строим ее описание ниже оси X 
                // и отмечаем точку на графике.
                for (int i = 1; i < moneys.Count + 1; i++)
                {
                    graphics.DrawString(dates[i - 1].ToShortDateString(),
                        new Font("Arial", 12, FontStyle.Bold),
                        Brushes.Black,
                        curve[i].X - 50, height + 5);
                    graphics.DrawString(purchases[i - 1].ToString(),
                        new Font("Arial", 12, FontStyle.Bold),
                        Brushes.Black,
                        curve[i].X - 50, curve[i].Y - 25);
                    graphics.DrawEllipse(new Pen(Color.Black, 8),
                        new RectangleF(curve[i].X - 2, curve[i].Y - 2, 4, 4));
                    graphics.FillEllipse(new SolidBrush(Color.Black),
                        new RectangleF(curve[i].X - 2, curve[i].Y - 2, 4, 4));
                }
            }


            map.Save($"../../../data/graphics/{id}.png", System.Drawing.Imaging.ImageFormat.Png);
            Console.ReadLine();
        }
    }
}
