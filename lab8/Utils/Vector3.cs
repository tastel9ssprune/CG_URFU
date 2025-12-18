using System;

namespace Lab8.Utils
{
    // Структура для работы с трехмерными векторами
    // Используется для координат, направлений и вычислений в 3D пространстве
    public struct Vector3
    {
        public float X, Y, Z;

        // Конструктор - создает вектор с заданными координатами
        public Vector3(float x, float y, float z) 
        { 
            X = x; 
            Y = y; 
            Z = z; 
        }

        // Оператор сложения векторов
        public static Vector3 operator +(Vector3 a, Vector3 b) =>
            new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        // Оператор вычитания векторов
        public static Vector3 operator -(Vector3 a, Vector3 b) =>
            new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        // Оператор умножения вектора на число
        public static Vector3 operator *(Vector3 a, float s) =>
            new Vector3(a.X * s, a.Y * s, a.Z * s);

        // Оператор умножения числа на вектор
        public static Vector3 operator *(float s, Vector3 a) =>
            new Vector3(a.X * s, a.Y * s, a.Z * s);

        // Оператор унарного минуса (обратный вектор)
        public static Vector3 operator -(Vector3 v) =>
            new Vector3(-v.X, -v.Y, -v.Z);

        // Скалярное произведение векторов
        // Возвращает число, которое показывает насколько векторы направлены в одну сторону
        public float Dot(Vector3 b) => X * b.X + Y * b.Y + Z * b.Z;

        // Длина вектора
        public float Length() => (float)Math.Sqrt(X * X + Y * Y + Z * Z);

        // Нормализация вектора - делает его длиной 1, сохраняя направление
        public Vector3 Normalize()
        {
            float len = Length();
            if (len < 0.0001f) return new Vector3(0, 0, 0);
            return new Vector3(X / len, Y / len, Z / len);
        }

        // Векторное произведение векторов
        // Возвращает вектор перпендикулярный обоим исходным векторам
        public Vector3 Cross(Vector3 other) =>
            new Vector3(
                Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X
            );
    }
}

