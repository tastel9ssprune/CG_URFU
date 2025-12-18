using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Lab6.Renderers
{
    // Класс для рисования различных 3D фигур
    // Здесь собраны все функции рисования геометрических объектов
    public static class FigureRenderer
    {
        // Рисует координатные оси
        // Оси помогают ориентироваться в 3D пространстве
        public static void DrawAxes()
        {
            GL.Begin(PrimitiveType.Lines);

            // Ось X - желтая
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Vertex3(-300.0f, 0.0f, 0.0f);
            GL.Vertex3(300.0f, 0.0f, 0.0f);

            // Ось Y - красная
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(0.0f, -300.0f, 0.0f);
            GL.Vertex3(0.0f, 300.0f, 0.0f);

            // Ось Z - голубая
            GL.Color3(0.2f, 0.9f, 1.0f);
            GL.Vertex3(0.0f, 0.0f, -300f);
            GL.Vertex3(0.0f, 0.0f, 300.0f);

            GL.End();
        }

        // Рисует цилиндр
        // center - центр основания цилиндра
        // radius - радиус цилиндра
        // height - высота цилиндра
        // segments - количество сегментов (чем больше, тем круглее)
        // drawType - тип примитива для рисования
        // color - цвет цилиндра
        // rotateX, rotateY - углы поворота вокруг осей X и Y
        public static void DrawCylinder(Vector3 center, float radius, float height, int segments, 
            PrimitiveType drawType, Color color, float rotateX, float rotateY)
        {
            // Сохраняем текущую матрицу
            GL.PushMatrix();
            
            // Перемещаем в центр цилиндра
            GL.Translate(center);
            GL.Color3(color);

            // Поворачиваем цилиндр
            GL.Rotate(rotateX, 1, 0, 0);
            GL.Rotate(rotateY, 0, 1, 0);

            // Рисуем цилиндр по сегментам
            for (int i = 0; i < segments; i++)
            {
                // Вычисляем углы для текущего и следующего сегмента
                float angle1 = 2f * (float)Math.PI * i / segments;
                float angle2 = 2f * (float)Math.PI * (i + 1) / segments;

                // Вычисляем координаты точек на окружности основания
                Vector3 p1 = new Vector3((float)Math.Cos(angle1) * radius, (float)Math.Sin(angle1) * radius, 0);
                Vector3 p2 = new Vector3((float)Math.Cos(angle2) * radius, (float)Math.Sin(angle2) * radius, 0);

                // Точки на верхнем основании
                Vector3 p3 = new Vector3(p2.X, p2.Y, height);
                Vector3 p4 = new Vector3(p1.X, p1.Y, height);

                // Рисуем четырехугольник (сторону цилиндра)
                GL.Begin(drawType);
                GL.Vertex3(p1);
                GL.Vertex3(p2);
                GL.Vertex3(p3);
                GL.Vertex3(p4);
                GL.End();
            }

            // Восстанавливаем матрицу
            GL.PopMatrix();
        }

        // Рисует куб
        // center - центр куба
        // size - размер стороны куба
        // drawType - тип примитива
        // color - цвет куба
        public static void DrawCube(Vector3 center, float size, PrimitiveType drawType, Color color)
        {
            GL.PushMatrix();
            GL.Translate(center);
            GL.Color3(color);

            // Определяем 8 вершин куба
            var vertices = new Vector3[]
            {
                new Vector3(-size, -size, -size),  // 0: левый нижний задний
                new Vector3( size, -size, -size),  // 1: правый нижний задний
                new Vector3( size,  size, -size),  // 2: правый верхний задний
                new Vector3(-size,  size, -size),  // 3: левый верхний задний
                new Vector3(-size, -size,  size),  // 4: левый нижний передний
                new Vector3( size, -size,  size),  // 5: правый нижний передний
                new Vector3( size,  size,  size),  // 6: правый верхний передний
                new Vector3(-size,  size,  size)   // 7: левый верхний передний
            };

            // Определяем 6 граней куба (каждая грань - 4 вершины)
            var faces = new int[][]
            {
                new int[] {0, 1, 2, 3},  // Задняя грань
                new int[] {4, 5, 6, 7},  // Передняя грань
                new int[] {0, 1, 5, 4},  // Нижняя грань
                new int[] {2, 3, 7, 6},  // Верхняя грань
                new int[] {1, 2, 6, 5},  // Правая грань
                new int[] {0, 3, 7, 4}   // Левая грань
            };

            // Рисуем каждую грань
            foreach (var face in faces)
            {
                GL.Begin(drawType);
                GL.Vertex3(vertices[face[0]]);
                GL.Vertex3(vertices[face[1]]);
                GL.Vertex3(vertices[face[2]]);
                GL.Vertex3(vertices[face[3]]);
                GL.End();
            }

            GL.PopMatrix();
        }

        // Рисует усеченную пирамиду
        // center - центр нижнего основания
        // h - высота пирамиды
        // r1 - радиус верхнего основания
        // r2 - радиус нижнего основания
        // drawType - тип примитива
        // color - цвет пирамиды
        public static void DrawTruncatedPyramid(Vector3 center, float h, float r1, float r2, 
            PrimitiveType drawType, Color color)
        {
            GL.PushMatrix();
            GL.Translate(center);
            GL.Color3(color);

            // Определяем 8 вершин усеченной пирамиды
            var vertices = new Vector3[]
            {
                new Vector3(-r2, -r2, 0),  // Нижнее основание
                new Vector3(r2, -r2, 0),
                new Vector3(r2, r2, 0),
                new Vector3(-r2, r2, 0),
                new Vector3(-r1, -r1, h),  // Верхнее основание
                new Vector3(r1, -r1, h),
                new Vector3(r1, r1, h),
                new Vector3(-r1, r1, h)
            };

            // Определяем грани пирамиды
            var faces = new int[][]
            {
                new int[] {0, 1, 2, 3},  // Нижнее основание
                new int[] {4, 5, 6, 7},  // Верхнее основание
                new int[] {0, 1, 5, 4},  // Передняя боковая грань
                new int[] {2, 3, 7, 6},  // Задняя боковая грань
                new int[] {1, 2, 6, 5},  // Правая боковая грань
                new int[] {0, 3, 7, 4}   // Левая боковая грань
            };

            // Рисуем каждую грань
            foreach (var face in faces)
            {
                GL.Begin(drawType);
                GL.Vertex3(vertices[face[0]]);
                GL.Vertex3(vertices[face[1]]);
                GL.Vertex3(vertices[face[2]]);
                GL.Vertex3(vertices[face[3]]);
                GL.End();
            }

            GL.PopMatrix();
        }

        // Рисует конус
        // center - центр основания
        // radius - радиус основания
        // height - высота конуса
        // segments - количество сегментов
        // drawType - тип примитива
        // color - цвет конуса
        public static void DrawCone(Vector3 center, float radius, float height, int segments, 
            PrimitiveType drawType, Color color)
        {
            GL.PushMatrix();
            GL.Translate(center);
            GL.Color3(color);

            // Вершина конуса
            var top = new Vector3(0, 0, height);
            
            // Рисуем конус по сегментам
            for (int i = 0; i < segments; i++)
            {
                // Вычисляем углы для текущего и следующего сегмента
                var a1 = 2 * Math.PI * i / segments;
                var a2 = 2 * Math.PI * (i + 1) / segments;
                
                // Точки на окружности основания
                var p1 = new Vector3((float)Math.Cos(a1) * radius, (float)Math.Sin(a1) * radius, 0);
                var p2 = new Vector3((float)Math.Cos(a2) * radius, (float)Math.Sin(a2) * radius, 0);

                // Рисуем треугольник (вершина и две точки на основании)
                GL.Begin(drawType);
                GL.Vertex3(top);
                GL.Vertex3(p1);
                GL.Vertex3(p2);
                GL.End();
            }

            GL.PopMatrix();
        }

        // Рисует поверхность трилистника (trefoil surface)
        // Это параметрическая поверхность с красивой формой
        // step - шаг для параметров (чем меньше, тем детальнее)
        // drawType - тип примитива
        public static void DrawTrefoilSurface(float step = 0.01f, PrimitiveType drawType = PrimitiveType.Points)
        {
            GL.Begin(drawType);
            float size = 20f;

            // Проходим по параметрам u и v
            for (float u = -2 * MathF.PI; u <= 2 * MathF.PI; u += step)
            {
                for (float v = -MathF.PI; v <= MathF.PI; v += step)
                {
                    // Вычисляем тригонометрические функции
                    float cosU = MathF.Cos(u);
                    float sinU = MathF.Sin(u);
                    float cosV = MathF.Cos(v);
                    float sinV = MathF.Sin(v);

                    // Параметрические уравнения для поверхности трилистника
                    float x = cosU * cosV + 3 * cosU * (1.5f + MathF.Sin(1.5f * u) / 2);
                    float y = sinU * cosV + 3 * sinU * (1.5f + MathF.Sin(1.5f * u) / 2);
                    float z = sinV + 2 * MathF.Cos(1.5f * u);
                    
                    // Цвет зависит от параметра v
                    GL.Color3(0, 0, v);
                    
                    // Рисуем точку
                    GL.Vertex3(x * size, y * size, z * size);
                }
            }

            GL.End();
        }
    }
}

