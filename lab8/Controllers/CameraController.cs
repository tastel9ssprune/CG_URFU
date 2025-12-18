using System;
using System.Windows.Forms;
using Lab8.Utils;

namespace Lab8.Controllers
{
    // Класс для управления камерой в raycasting
    // Камера определяет откуда и в каком направлении мы смотрим на сцену
    public class CameraController
    {
        // Позиция камеры в 3D пространстве
        public Vector3 Position = new Vector3(1, 1.3f, -2);
        
        // Направление взгляда камеры (нормализованный вектор)
        public Vector3 Forward = new Vector3(0, 0, 0);

        // Скорость движения камеры
        private float speed = 1f;
        
        // Скорость поворота камеры
        private float rotationSpeed = 0.2f;

        // Углы поворота камеры
        private float cameraYaw = 0f;    // Поворот влево-вправо
        private float cameraPitch = 0f;   // Поворот вверх-вниз

        // Параметры виртуальной камеры для raycasting
        float Dist;      // Расстояние до плоскости проекции
        float vWidth;   // Ширина виртуального экрана
        float vHeight;  // Высота виртуального экрана

        // Конструктор - инициализирует камеру
        public CameraController(int width, int height)
        {
            Dist = 1;  // Расстояние до плоскости проекции
            vWidth = 2.0f;  // Ширина виртуального экрана
            vHeight = 2.0f * height / width;  // Высота с учетом соотношения сторон
        }

        // Обновляет позицию и направление камеры в зависимости от нажатых клавиш
        public void Update()
        {
            // Вычисляем векторы направления
            var up = new Vector3(0, 1, 0);  // Вектор "вверх"
            var right = Forward.Cross(up);  // Вектор "вправо"

            var move = new Vector3(0, 0, 0);

            // Обрабатываем нажатые клавиши для движения
            if (KeyController.pressedKeys.Contains(Keys.W)) move += Forward;  // Вперед
            if (KeyController.pressedKeys.Contains(Keys.S)) move -= Forward;  // Назад
            if (KeyController.pressedKeys.Contains(Keys.A)) move -= right;     // Влево
            if (KeyController.pressedKeys.Contains(Keys.D)) move += right;     // Вправо
            if (KeyController.pressedKeys.Contains(Keys.Q)) move += up;       // Вверх
            if (KeyController.pressedKeys.Contains(Keys.E)) move -= up;        // Вниз

            // Если есть движение - обновляем позицию
            if (move.Length() != 0)
                Position += move.Normalize() * speed;

            // Обрабатываем нажатые клавиши для поворота
            if (KeyController.pressedKeys.Contains(Keys.D1)) cameraYaw -= rotationSpeed;  // Поворот влево
            if (KeyController.pressedKeys.Contains(Keys.D2)) cameraYaw += rotationSpeed;  // Поворот вправо
            if (KeyController.pressedKeys.Contains(Keys.D3)) cameraPitch -= rotationSpeed; // Поворот вверх
            if (KeyController.pressedKeys.Contains(Keys.D4)) cameraPitch += rotationSpeed; // Поворот вниз

            // Ограничиваем угол pitch чтобы камера не переворачивалась
            cameraPitch = Math.Clamp(cameraPitch, -MathF.PI / 2 + 0.01f, MathF.PI / 2 - 0.01f);

            // Вычисляем направление взгляда из углов yaw и pitch
            Forward = new Vector3(
                MathF.Cos(cameraPitch) * MathF.Sin(cameraYaw),
                MathF.Sin(cameraPitch),
                MathF.Cos(cameraPitch) * MathF.Cos(cameraYaw)
            ).Normalize();
        }

        // Вычисляет направление луча для заданного пикселя экрана
        // x, y - координаты пикселя
        // width, height - размеры экрана
        // Возвращает нормализованный вектор направления луча
        public Vector3 GetRay(int x, int y, int width, int height)
        {
            // Преобразуем координаты пикселя в координаты виртуального экрана
            float px = (x - width / 2.0f) * vWidth / width;
            float py = -(y - height / 2.0f) * vHeight / height;

            // Вычисляем направление луча в локальной системе координат камеры
            Vector3 rayLocalCamera = new Vector3(px, py, Dist).Normalize();

            // Вычисляем базис камеры (право, вверх, вперед)
            var up = new Vector3(0, 1, 0);
            var right = Forward.Cross(up).Normalize();
            up = right.Cross(Forward).Normalize();

            // Преобразуем луч из локальной системы координат в глобальную
            Vector3 rayGlobal =
                (right * rayLocalCamera.X +
                up * rayLocalCamera.Y +
                Forward * rayLocalCamera.Z).Normalize();

            return rayGlobal;
        }
    }
}

