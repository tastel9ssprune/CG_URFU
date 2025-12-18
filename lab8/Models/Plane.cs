using System;
using System.Drawing;
using Lab8.Utils;

namespace Lab8.Models
{
    // Класс для плоскости (бесконечной плоскости)
    // Используется для пола, стен и других плоских поверхностей
    public class Plane : Shape3D
    {
        // Точка на плоскости
        public Vector3 point;
        
        // Нормаль плоскости (вектор перпендикулярный плоскости)
        public Vector3 normal;

        // Конструктор - создает плоскость с заданными параметрами
        public Plane(Vector3 point, Vector3 normal, Color color, 
            float specularStrength = 0.5f, int shininess = 8, 
            float transparency = 0, float reflectivity = 0)
        {
            this.point = point;
            this.normal = normal.Normalize();  // Нормализуем нормаль
            this.color = color;
            this.specularStrength = specularStrength;
            this.shininess = shininess;
            this.transparency = transparency;
            this.reflectivity = reflectivity;
        }

        // Вычисляет пересечение луча с плоскостью
        // Использует формулу пересечения луча с плоскостью
        public override float Intersect(Vector3 origin, Vector3 direction)
        {
            // Вычисляем знаменатель - скалярное произведение нормали и направления луча
            float denom = normal.Dot(direction);
            
            // Если луч параллелен плоскости - пересечения нет
            if (Math.Abs(denom) < 0.001f)
                return 0;

            // Вычисляем расстояние до точки пересечения
            float t = (point - origin).Dot(normal) / denom;
            
            // Если точка пересечения перед началом луча - возвращаем расстояние
            if (t > 0.001f)
                return t;

            return 0;
        }

        // Вычисляет нормаль в точке попадания
        // Для плоскости нормаль одинакова во всех точках
        public override Vector3 NormalHitPoint(Vector3 hitPoint)
        {
            return normal;
        }
    }
}

