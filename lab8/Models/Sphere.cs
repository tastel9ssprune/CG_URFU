using System;
using System.Drawing;
using Lab8.Utils;

namespace Lab8.Models
{
    // Класс для сферы - простейший 3D объект
    public class Sphere : Shape3D
    {
        // Центр сферы
        public Vector3 center;
        
        // Радиус сферы
        public float radius;

        // Конструктор - создает сферу с заданными параметрами
        public Sphere(Vector3 center, float radius, Color color, 
            float specularStrength = 0.7f, int shininess = 16, 
            float transparency = 0, float reflectivity = 0)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.specularStrength = specularStrength;
            this.shininess = shininess;
            this.transparency = transparency;
            this.reflectivity = reflectivity;
        }

        // Вычисляет пересечение луча со сферой
        // Использует квадратное уравнение для нахождения точек пересечения
        public override float Intersect(Vector3 origin, Vector3 direction)
        {
            // Вектор от центра сферы до начала луча
            Vector3 oc = origin - center;
            
            // Коэффициенты квадратного уравнения
            float a = direction.Dot(direction);
            float b = 2.0f * oc.Dot(direction);
            float c = oc.Dot(oc) - radius * radius;
            
            // Дискриминант - определяет есть ли пересечение
            float discriminant = b * b - 4 * a * c;

            // Если дискриминант отрицательный - луч не пересекает сферу
            if (discriminant < 0)
                return 0;

            // Возвращаем ближайшую точку пересечения
            return (-b - (float)Math.Sqrt(discriminant)) / (2.0f * a);
        }

        // Вычисляет нормаль в точке попадания
        // Для сферы нормаль - это вектор от центра к точке попадания
        public override Vector3 NormalHitPoint(Vector3 hitPoint)
        {
            return (hitPoint - center).Normalize();
        }
    }
}

