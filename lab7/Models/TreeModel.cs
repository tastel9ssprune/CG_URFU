using System;
using System.Drawing;
using OpenTK.Mathematics;

namespace Lab7.Models
{
    // Структура для хранения сегмента дерева (цилиндра)
    // Каждый сегмент - это часть ствола или ветки
    public struct CylinderSegment
    {
        public Vector3 Position;  // Позиция сегмента
        public float Radius;      // Радиус сегмента
        public float Height;      // Высота сегмента
        public float RotateX;     // Угол поворота вокруг оси X
        public float RotateY;     // Угол поворота вокруг оси Y
        public float RotateZ;     // Угол поворота вокруг оси Z
        public Color Color;       // Цвет сегмента
    }
}

