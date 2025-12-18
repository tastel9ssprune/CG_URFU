using NCalc;
using System;

namespace Lab3.Parsers
{
    // Класс для парсинга и вычисления математических формул
    // Использует библиотеку NCalc для преобразования строки в формулу
    public class FormulaParser
    {
        // Вычисляет значение функции в заданной точке
        // formula - строка с формулой (например, "x * x + 1")
        // x - значение переменной x
        // Возвращает значение функции или NaN если произошла ошибка
        public static double Evaluate(string formula, double x)
        {
            try
            {
                // Создаем выражение из строки
                var expression = new Expression(formula);
                
                // Подставляем значение x в формулу
                expression.Parameters["x"] = x;
                
                // Вычисляем результат
                var result = expression.Evaluate();
                
                // Преобразуем результат в число
                return Convert.ToDouble(result);
            }
            catch
            {
                // Если произошла ошибка (например, деление на ноль или неверная формула)
                // возвращаем NaN (Not a Number)
                return double.NaN;
            }
        }

        // Проверяет корректность формулы
        // Возвращает true если формула корректна, false если есть ошибки
        public static bool IsValid(string formula)
        {
            try
            {
                // Пробуем вычислить формулу с тестовым значением
                var expression = new Expression(formula);
                expression.Parameters["x"] = 0.0;
                expression.Evaluate();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

