using System;
using System.Drawing;
using Lab8.Utils;

namespace Lab8.Models
{
    // Класс для куба (параллелепипеда)
    public class Cube : Shape3D
    {
        // Центр куба
        public Vector3 center;
        
        // Минимальные координаты (левый нижний задний угол)
        public Vector3 min;
        
        // Максимальные координаты (правый верхний передний угол)
        public Vector3 max;

        // Конструктор - создает куб с заданными параметрами
        public Cube(Vector3 center, float size, Color color, 
            float specularStrength = 0.7f, int shininess = 16, 
            float transparency = 0, float reflectivity = 0)
        {
            this.center = center;
            
            // Вычисляем границы куба
            float half = size / 2;
            this.min = new Vector3(center.X - half, center.Y - half, center.Z - half);
            this.max = new Vector3(center.X + half, center.Y + half, center.Z + half);

            this.color = color;
            this.specularStrength = specularStrength;
            this.shininess = shininess;
            this.transparency = transparency;
            this.reflectivity = reflectivity;
        }

        // Вычисляет пересечение луча с кубом
        // Использует алгоритм пересечения луча с параллелепипедом
        public override float Intersect(Vector3 origin, Vector3 direction)
        {
            // Вычисляем точки пересечения с гранями по оси X
            float tMin = (min.X - origin.X) / direction.X;
            float tMax = (max.X - origin.X) / direction.X;
            if (tMin > tMax) (tMin, tMax) = (tMax, tMin);

            // Вычисляем точки пересечения с гранями по оси Y
            float tyMin = (min.Y - origin.Y) / direction.Y;
            float tyMax = (max.Y - origin.Y) / direction.Y;
            if (tyMin > tyMax) (tyMin, tyMax) = (tyMax, tyMin);

            // Проверяем пересечение интервалов по X и Y
            if ((tMin > tyMax) || (tyMin > tMax))
                return 0;

            // Объединяем интервалы
            if (tyMin > tMin) tMin = tyMin;
            if (tyMax < tMax) tMax = tyMax;

            // Вычисляем точки пересечения с гранями по оси Z
            float tzMin = (min.Z - origin.Z) / direction.Z;
            float tzMax = (max.Z - origin.Z) / direction.Z;
            if (tzMin > tzMax) (tzMin, tzMax) = (tzMax, tzMin);

            // Проверяем пересечение интервалов по X, Y и Z
            if ((tMin > tzMax) || (tzMin > tMax))
                return 0;

            // Объединяем интервалы
            if (tzMin > tMin) tMin = tzMin;
            if (tzMax < tMax) tMax = tzMax;

            // Возвращаем ближайшую точку пересечения
            return tMin > 0.001f ? tMin : tMax > 0.001f ? tMax : 0;
        }

        // Вычисляет нормаль в точке попадания
        // Нормаль зависит от того, в какую грань попал луч
        public override Vector3 NormalHitPoint(Vector3 hitPoint)
        {
            // Проверяем какая грань ближе всего к точке попадания
            if (Math.Abs(hitPoint.X - min.X) < 0.001f) return new Vector3(-1, 0, 0);  // Левая грань
            if (Math.Abs(hitPoint.X - max.X) < 0.001f) return new Vector3(1, 0, 0);   // Правая грань
            if (Math.Abs(hitPoint.Y - min.Y) < 0.001f) return new Vector3(0, -1, 0);    // Нижняя грань
            if (Math.Abs(hitPoint.Y - max.Y) < 0.001f) return new Vector3(0, 1, 0);    // Верхняя грань
            if (Math.Abs(hitPoint.Z - min.Z) < 0.001f) return new Vector3(0, 0, -1);   // Задняя грань
            if (Math.Abs(hitPoint.Z - max.Z) < 0.001f) return new Vector3(0, 0, 1);     // Передняя грань

            return new Vector3(0, 0, 0);
        }
    }
}

