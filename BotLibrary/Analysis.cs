using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;


namespace BotLibrary
{
    /// <summary>
    /// Класс для реализации анализа списка покупок и перевода его в круговую диаграмму
    /// Или график сумм покупок по дням.
    /// </summary>
    public class Analysis
    {
        // Пути для сохранения данных в папке работы.
        public static string graphicPath = "../../../data/graphics/";
        public static string piePath = "../../../data/pies/";

        // Генератор рандомных чисел.
        public static Random random = new Random();

        /// <summary>
        /// Метод для создания графика на основе значений покупок и дат.
        /// </summary>
        /// <param name="purchases"></param>
        /// <param name="dates"></param>
        /// <param name="id"></param>
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
                graphics.DrawLine(new Pen(Color.Blue, 3),
                                  new Point(20, 0),
                                  new Point(20, maxHeight));
                graphics.DrawLine(new Pen(Color.Blue, 3),
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
                    graphics.DrawEllipse(new Pen(Color.Black, 6),
                        new RectangleF(curve[i].X - 2, curve[i].Y - 2, 3, 3));
                    graphics.FillEllipse(new SolidBrush(Color.Black),
                        new RectangleF(curve[i].X - 2, curve[i].Y - 2, 3, 3));
                }

                // Сохраняем Bitmap в виде PNG-изображения.
                map.Save($"{graphicPath}{id}.png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        /// <summary>
        /// Метод для реализации создания круговой диаграммы с помощью класса
        /// Graphics и Bitmap.
        /// </summary>
        /// <param name="categoriesCount"></param>
        /// <param name="categoriesGroup"></param>
        /// <param name="id"></param>
        public static void PieAnalysis(List<int> categoriesCount, List<string> categoriesGroup, long id)
        {
            // Размеры изображения с Pie.
            int maxWidth = 800;
            int maxHeight = 500;

            // Размеры Pie.
            int pieX = 100;
            int pieY = 125;

            // Координаты точки.
            int pointX = 450;
            int pointY = 125;

            // Координаты точки.
            int pointString = pointX + 20;

            int allCount = categoriesCount.Sum();
            float angle = 360 / (float)allCount;
            float percent = 100 / (float)allCount;

            // Списки углов и процентов, которые будут отображаться
            // в диаграмме.
            List<float> angles = new List<float>();
            List<float> percents = new List<float>();

            // Заполнение списка для вывода.
            for (int i = 0; i < categoriesGroup.Count; i++)
            {
                angles.Add(categoriesCount[i] * angle);
                percents.Add(categoriesCount[i] * percent);
            }

            var categoriesPercentage = categoriesGroup.Zip(percents, (a, b) => new { a, b }).OrderBy(e => e.a);

            // Инкапсулирует точечный рисунок GDI+, состоящий из данных пикселей графического 
            // изображения и атрибутов рисунка. 
            // Объект Bitmap используется для работы с изображениями, определяемыми данными пикселей.
            var map = new Bitmap(maxWidth, maxHeight);

            using (Graphics graphics = Graphics.FromImage(map))
            {
                // Делаем белый фон для графика.
                graphics.Clear(Color.FromArgb(237, 238, 240));

                graphics.DrawString($"Круговая Диаграмма По Категориям",
                        new Font("Arial", 25, FontStyle.Bold),
                        Brushes.Black,
                        100, 10);

                graphics.DrawString($"TOP:",
                        new Font("Arial", 20, FontStyle.Bold),
                        Brushes.Black,
                        pointX + 18, pointY - 40);

                float previousAngle = 0;

                for (int i = 0; i < categoriesGroup.Count; i++)
                {
                    graphics.DrawPie(new Pen(Color.Black, 4), new RectangleF(new Point(pieX, pieY),
                    new Size(250, 250)), previousAngle, angles[i]);

                    // Генерация рандомного цвета для заполнения куска диаграммы.
                    int r = random.Next(255);
                    int g = random.Next(255);
                    int b = random.Next(255);
                    Color color = Color.FromArgb(r, g, b);

                    // Заполнение куска диаграммы.
                    graphics.FillPie(
                        new SolidBrush(color),
                        new Rectangle(new Point(pieX, pieY), new Size(250, 250)), previousAngle, angles[i]);

                    // Вывод ТОП12 категорий.
                    if (i <= 12)
                    {
                        graphics.DrawEllipse(new Pen(Color.Black, 2),
                        new RectangleF(pointX, pointY, 15, 15));
                        graphics.FillEllipse(new SolidBrush(color),
                            new RectangleF(pointX, pointY, 15, 15));

                        graphics.DrawString($"{categoriesGroup[i]} - {percents[i]:F1}%",
                            new Font("Arial", 12, FontStyle.Bold),
                            Brushes.Black,
                            pointString, pointY);

                        pointY += 25;
                    }
                    previousAngle += angles[i];
                }

                // Сохраняем Bitmap в виде PNG-изображения.
                map.Save($"{piePath}{id}.png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
