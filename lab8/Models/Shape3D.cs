using System;
using System.Drawing;
using Lab8.Utils;

namespace Lab8.Models
{
    // Абстрактный класс для всех 3D объектов в сцене
    // Каждый объект должен уметь вычислять пересечение с лучом и нормаль в точке попадания
    public abstract class Shape3D
    {
        // Цвет объекта
        public Color color;
        
        // Сила зеркального отражения (блики)
        public float specularStrength;
        
        // Блеск объекта (чем больше, тем более резкие блики)
        public int shininess;
        
        // Прозрачность (0 - непрозрачный, 1 - полностью прозрачный)
        public float transparency;
        
        // Отражательная способность (0 - не отражает, 1 - зеркало)
        public float reflectivity;
        
        // Вычисляет пересечение луча с объектом
        // origin - начало луча
        // direction - направление луча
        // Возвращает расстояние до точки пересечения или 0 если пересечения нет
        public abstract float Intersect(Vector3 origin, Vector3 direction);
        
        // Вычисляет нормаль в точке попадания луча
        // hitPoint - точка попадания луча в объект
        // Возвращает нормализованный вектор нормали
        public abstract Vector3 NormalHitPoint(Vector3 hitPoint);
    }
}

