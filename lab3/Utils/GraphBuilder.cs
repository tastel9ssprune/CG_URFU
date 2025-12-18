using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System;
using Lab3.Parsers;

namespace Lab3.Utils
{
    // Класс для построения графиков функций
    // Создает серии точек и добавляет их на график
    public static class GraphBuilder
    {
        // Добавляет график функции на модель графика
        // model - модель графика куда добавлять
        // formula - формула функции (например, "x * x")
        // color - цвет линии графика
        // marker - тип маркера для точек (круг, квадрат, треугольник и т.д.)
        // xMin, xMax - диапазон значений x для построения
        // step - шаг между точками (чем меньше, тем плавнее график)
        public static void AddFunction(
            PlotModel model, 
            string formula, 
            OxyColor color, 
            MarkerType marker,
            double xMin = -5.0,
            double xMax = 5.0,
            double step = 0.2)
        {
            // Создаем серию точек для графика
            var series = new LineSeries
            {
                Title = formula,           // Название функции (показывается в легенде)
                Color = color,             // Цвет линии
                MarkerType = marker,        // Тип маркера в точках
                MarkerSize = 4,            // Размер маркера
                MarkerFill = color,        // Цвет заливки маркера
                MarkerStroke = OxyColors.Black,  // Цвет обводки маркера
                MarkerStrokeThickness = 1.5      // Толщина обводки
            };

            // Проходим по всем значениям x в заданном диапазоне
            for (double x = xMin; x <= xMax; x += step)
            {
                // Вычисляем значение функции в точке x
                double y = FormulaParser.Evaluate(formula, x);
                
                // Проверяем что получилось нормальное число (не NaN и не бесконечность)
                if (!double.IsNaN(y) && !double.IsInfinity(y))
                {
                    // Добавляем точку на график
                    series.Points.Add(new DataPoint(x, y));
                }
            }

            // Добавляем серию на график
            model.Series.Add(series);
        }

        // Настраивает оси графика
        // model - модель графика
        // xTitle - название оси X
        // yTitle - название оси Y
        public static void SetupAxes(PlotModel model, string xTitle = "X", string yTitle = "Y")
        {
            // Добавляем ось X снизу
            model.Axes.Add(new LinearAxis 
            { 
                Position = AxisPosition.Bottom, 
                Title = xTitle 
            });
            
            // Добавляем ось Y слева
            model.Axes.Add(new LinearAxis 
            { 
                Position = AxisPosition.Left, 
                Title = yTitle 
            });
        }
    }
}

