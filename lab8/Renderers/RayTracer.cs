using System;
using System.Collections.Generic;
using System.Drawing;
using Lab8.Models;
using Lab8.Utils;
using Lab8.Controllers;

namespace Lab8.Renderers
{
    // Класс для трассировки лучей (raycasting)
    // Рендерит 3D сцену, испуская лучи из камеры и вычисляя их пересечения с объектами
    public class RayTracer
    {
        // Камера для просмотра сцены
        public CameraController camera;
        
        // Цвет фона (неба)
        public Color backgroundColor = Color.Blue;
        
        // Размеры изображения
        private int width, height;
        
        // Фоновое освещение (минимальная яркость всех объектов)
        private const float ambient = 0.2f;
        
        // Список объектов в сцене
        private List<Shape3D> objects;
        
        // Направления источников света
        private List<Vector3> lightDirs;

        // Конструктор - создает рендерер и инициализирует сцену
        public RayTracer(int width, int height)
        {
            this.width = width;
            this.height = height;

            // Создаем камеру
            camera = new CameraController(width, height);

            // Создаем объекты в сцене
            objects = new List<Shape3D>();
            objects.Add(new Sphere(new Vector3(0, 0, 3), 1.0f, Color.Green, 0.7f, 64));
            objects.Add(new Sphere(new Vector3(2, 3, 5), 1.0f, Color.Red, 0.9f, 64));
            objects.Add(new Sphere(new Vector3(6, 1, 5), 2.0f, Color.Blue, 0.7f, 32, 0f, 0.8f));
            objects.Add(new Sphere(new Vector3(2, 3, 3), 1.0f, Color.Blue, 0.7f, 32, 0.5f));
            objects.Add(new Cube(new Vector3(2, 0, 1), 1.0f, Color.White, 0.99f, 128));
            objects.Add(new Cube(new Vector3(3, 0, 2), 1.0f, Color.Gray, 0.9f, 32));
            objects.Add(new Plane(new Vector3(0, 0, 0), new Vector3(0, 1, 0), Color.DarkCyan, 0.8f, 32));

            // Создаем источники света
            lightDirs = new List<Vector3>();
            lightDirs.Add(new Vector3(-1, -1, 1).Normalize());
        }

        // Рендерит всю сцену
        // Возвращает готовое изображение
        public Bitmap RenderScene()
        {
            // Создаем новое изображение
            var bmp = new Bitmap(width, height);

            // Обновляем камеру
            camera.Update();

            // Проходим по всем пикселям экрана
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Получаем направление луча для текущего пикселя
                    var rayGlobal = camera.GetRay(x, y, width, height);
                    
                    // Трассируем луч и получаем цвет пикселя
                    bmp.SetPixel(x, y, TraceRay(camera.Position, rayGlobal, 0));
                }
            }

            return bmp;
        }

        // Находит ближайшее пересечение луча с объектами сцены
        // rayOrigin - начало луча
        // rayDir - направление луча
        // closestT - выходной параметр, расстояние до ближайшего пересечения
        // Возвращает объект с которым произошло пересечение или null
        public Shape3D FindNearestIntersection(Vector3 rayOrigin, Vector3 rayDir, out float closestT)
        {
            closestT = float.MaxValue;
            Shape3D closestObject = null;

            // Проверяем пересечение со всеми объектами
            foreach (var obj in objects)
            {
                float t = obj.Intersect(rayOrigin, rayDir);

                // Если нашли более близкое пересечение - сохраняем его
                if (t < closestT && t > 0.001f)
                {
                    closestT = t;
                    closestObject = obj;
                }
            }

            return closestObject;
        }

        // Проверяет находится ли точка в тени
        // point - точка для проверки
        // lightDirection - направление к источнику света
        // objects - список объектов
        // currentObj - текущий объект (чтобы не проверять пересечение с самим собой)
        // Возвращает true если точка в тени
        public bool InShadow(Vector3 point, Vector3 lightDirection, List<Shape3D> objects, Shape3D currentObj)
        {
            // Смещаем точку немного от поверхности чтобы избежать самопересечения
            var shadowPoint = point + lightDirection * 0.001f;

            // Проверяем пересечение луча от точки к источнику света с объектами
            foreach (var obj in objects)
            {
                // Если луч пересекает какой-то объект - точка в тени
                if (obj.Intersect(shadowPoint, lightDirection) > 0.001f)
                    return true;
            }

            return false;
        }

        // Гамма-коррекция цвета
        // Делает изображение более реалистичным, корректируя яркость
        public static Color GammaCorrection(Color color, float intensity, float gamma = 0.45f)
        {
            // Преобразуем цвет в диапазон 0-1
            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;

            // Применяем гамма-коррекцию к каждому каналу
            int red = CorrectChannel(r, intensity, gamma);
            int green = CorrectChannel(g, intensity, gamma);
            int blue = CorrectChannel(b, intensity, gamma);

            return Color.FromArgb(red, green, blue);
        }

        // Корректирует один канал цвета
        private static int CorrectChannel(float channel, float intensity, float gamma)
        {
            // Применяем интенсивность и гамма-коррекцию
            float corrected = (float)Math.Pow(Math.Clamp(channel * intensity, 0f, 1f), 1.0 / gamma);
            return (int)(corrected * 255);
        }

        // Трассирует луч и вычисляет цвет пикселя
        // origin - начало луча
        // rayGlobal - направление луча
        // depth - глубина рекурсии (для отражений и прозрачности)
        // Возвращает цвет пикселя
        private Color TraceRay(Vector3 origin, Vector3 rayGlobal, int depth)
        {
            // Ограничиваем глубину рекурсии чтобы избежать бесконечного цикла
            if (depth > 2) return Color.Black;

            // Находим ближайшее пересечение
            var obj = FindNearestIntersection(origin, rayGlobal, out float closestT);

            // Если пересечения нет - возвращаем цвет фона
            if (closestT == 0 || obj == null)
                return backgroundColor;

            // Вычисляем точку попадания и нормаль
            Vector3 hitPoint = origin + rayGlobal * closestT;
            Vector3 normal = obj.NormalHitPoint(hitPoint);

            // Вычисляем освещение
            float lightIntensity = 0.0f;
            foreach (var lightDir in lightDirs)
            {
                // Проверяем находится ли точка в тени
                if (InShadow(hitPoint, -lightDir, objects, obj))
                {
                    // Если в тени - добавляем только фоновое освещение
                    if (lightIntensity == 0)
                        lightIntensity += ambient;
                    continue;
                }

                // Диффузное освещение (зависит от угла между нормалью и направлением света)
                float diffuse = Math.Max(0, normal.Dot(-lightDir));

                // Зеркальное отражение (блики)
                Vector3 viewDir = (origin - hitPoint).Normalize();
                Vector3 reflectDir = (normal.Dot(-lightDir) * 2 * normal + lightDir).Normalize();
                float specular = (float)Math.Pow(Math.Max(0, viewDir.Dot(reflectDir)), obj.shininess);

                // Итоговое освещение = фоновое + диффузное + зеркальное
                lightIntensity += ambient + 0.6f * diffuse + obj.specularStrength * specular;
            }
            lightIntensity /= lightDirs.Count;

            // Ограничиваем интенсивность в диапазоне 0-1
            lightIntensity = Math.Clamp(lightIntensity, 0, 1);

            // Применяем гамма-коррекцию
            var CorrectionColor = GammaCorrection(obj.color, lightIntensity, 0.9f);

            // Обрабатываем прозрачность
            var transparency = obj.transparency;
            if (transparency > 0)
            {
                // Трассируем луч через прозрачный объект
                var transparencyColor = TraceRay(hitPoint, rayGlobal - normal * 0.001f, depth + 1);

                // Смешиваем цвета с учетом прозрачности
                int r = (int)(CorrectionColor.R * (1 - transparency) + transparencyColor.R * transparency);
                int g = (int)(CorrectionColor.G * (1 - transparency) + transparencyColor.G * transparency);
                int b = (int)(CorrectionColor.B * (1 - transparency) + transparencyColor.B * transparency);

                CorrectionColor = Color.FromArgb(r, g, b);
            }

            // Обрабатываем отражения
            var reflectivity = obj.reflectivity;
            if (reflectivity > 0)
            {
                // Вычисляем направление отраженного луча
                Vector3 reflectDir = Reflect(rayGlobal, normal).Normalize();
                
                // Трассируем отраженный луч
                var reflectColor = TraceRay(hitPoint + normal * 0.001f, reflectDir, depth + 1);

                // Смешиваем цвета с учетом отражательной способности
                int r = (int)(CorrectionColor.R * (1 - reflectivity) + reflectColor.R * reflectivity);
                int g = (int)(CorrectionColor.G * (1 - reflectivity) + reflectColor.G * reflectivity);
                int b = (int)(CorrectionColor.B * (1 - reflectivity) + reflectColor.B * reflectivity);

                CorrectionColor = Color.FromArgb(r, g, b);
            }

            return CorrectionColor;
        }
    
        // Вычисляет направление отраженного луча
        // incident - падающий луч
        // normal - нормаль поверхности
        // Возвращает направление отраженного луча
        public static Vector3 Reflect(Vector3 incident, Vector3 normal)
        {
            return incident - 2 * incident.Dot(normal) * normal;
        }
    }
}

