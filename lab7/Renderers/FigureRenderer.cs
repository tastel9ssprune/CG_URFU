using System;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Lab7.Renderers
{
    // Класс для рисования различных 3D фигур
    public static class FigureRenderer
    {
        // ID текстуры для земли (травы)
        public static int GroundTextureId;

        // Рисует координатные оси
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
        public static void DrawCylinder(Vector3 center, float radius, float height, int segments, 
            PrimitiveType drawType, Color color, float rotateX, float rotateY)
        {
            GL.PushMatrix();
            GL.Translate(center);
            GL.Color3(color);

            GL.Rotate(rotateX, 1, 0, 0);
            GL.Rotate(rotateY, 0, 1, 0);

            for (int i = 0; i < segments; i++)
            {
                float angle1 = 2f * (float)Math.PI * i / segments;
                float angle2 = 2f * (float)Math.PI * (i + 1) / segments;

                Vector3 p1 = new Vector3((float)Math.Cos(angle1) * radius, (float)Math.Sin(angle1) * radius, 0);
                Vector3 p2 = new Vector3((float)Math.Cos(angle2) * radius, (float)Math.Sin(angle2) * radius, 0);

                Vector3 p3 = new Vector3(p2.X, p2.Y, height);
                Vector3 p4 = new Vector3(p1.X, p1.Y, height);

                GL.Begin(drawType);
                GL.Vertex3(p1);
                GL.Vertex3(p2);
                GL.Vertex3(p3);
                GL.Vertex3(p4);
                GL.End();
            }

            GL.PopMatrix();
        }

        // Рисует куб
        public static void DrawCube(Vector3 center, float size, PrimitiveType drawType, Color color)
        {
            GL.PushMatrix();
            GL.Translate(center);
            GL.Color4(color);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            var vertices = new Vector3[]
            {
                new Vector3(-size, -size, -size),
                new Vector3( size, -size, -size),
                new Vector3( size,  size, -size),
                new Vector3(-size,  size, -size),
                new Vector3(-size, -size,  size),
                new Vector3( size, -size,  size),
                new Vector3( size,  size,  size),
                new Vector3(-size,  size,  size)
            };

            var faces = new int[][]
            {
                new int[] {0, 1, 2, 3},
                new int[] {4, 5, 6, 7},
                new int[] {0, 1, 5, 4},
                new int[] {2, 3, 7, 6},
                new int[] {1, 2, 6, 5},
                new int[] {0, 3, 7, 4}
            };

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

        // Рисует сетку из текстур (землю)
        // gridSize - размер сетки (сколько плиток в каждую сторону)
        // tileSize - размер одной плитки
        public static void DrawTextureGrid(int gridSize, float tileSize)
        {
            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, GroundTextureId);

            // Рисуем сетку из квадратов с текстурой
            for (int x = -gridSize; x <= gridSize; x++)
            {
                for (int y = -gridSize; y <= gridSize; y++)
                {
                    float worldX = x * tileSize;
                    float worldY = y * tileSize;

                    GL.Begin(PrimitiveType.Quads);

                    // Задаем координаты текстуры и вершины
                    GL.TexCoord2(0.0f, 0.0f);
                    GL.Vertex3(worldX, worldY, 0f);

                    GL.TexCoord2(1.0f, 0.0f);
                    GL.Vertex3(worldX + tileSize, worldY, 0f);

                    GL.TexCoord2(1.0f, 1.0f);
                    GL.Vertex3(worldX + tileSize, worldY + tileSize, 0f);

                    GL.TexCoord2(0.0f, 1.0f);
                    GL.Vertex3(worldX, worldY + tileSize, 0f);

                    GL.End();
                }
            }

            GL.Disable(EnableCap.Texture2D);
        }
    }
}

