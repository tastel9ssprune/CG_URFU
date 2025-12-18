using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenTK.Mathematics;

namespace Lab7.Controllers
{
    // Класс для управления камерой в 3D пространстве
    // Позволяет перемещаться и поворачивать камеру с помощью клавиатуры и мыши
    public class CameraController
    {
        // Позиция камеры в 3D пространстве
        public Vector3 Position = new Vector3(200, 200, 150);
        
        // Точка, на которую смотрит камера (центр сцены, где находится дерево)
        public Vector3 Target = new Vector3(100, 0, 50);

        // Множество нажатых клавиш
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();
        
        // Скорость движения камеры
        private float speed = 5f;

        // Углы поворота камеры
        private float yaw = -45f;     // Поворот влево-вправо (настроено чтобы смотреть на дерево)
        private float pitch = -20f;   // Поворот вверх-вниз (немного сверху)
        
        // Флаг первого движения мыши (чтобы не было резкого скачка)
        private bool firstMouse = true;
        private float lastX, lastY;
        
        // Чувствительность мыши
        private float sensitivity = 0.2f;

        // Конструктор - инициализирует камеру
        public CameraController()
        {
            // Устанавливаем начальное направление взгляда
            UpdateTarget();
        }

        // Обработчик нажатия клавиши
        public void KeyDown(Keys key) => pressedKeys.Add(key);
        
        // Обработчик отпускания клавиши
        public void KeyUp(Keys key) => pressedKeys.Remove(key);

        // Обработчик движения мыши
        // Изменяет направление взгляда камеры
        public void MouseMove(float x, float y)
        {
            // Если левая кнопка мыши не нажата - сбрасываем флаг
            if ((Control.MouseButtons & MouseButtons.Left) == 0)
            {
                firstMouse = true;
                return;
            }
            
            // При первом движении сохраняем текущую позицию мыши
            if (firstMouse)
            {
                lastX = x;
                lastY = y;
                firstMouse = false;
                return;
            }

            // Вычисляем смещение мыши
            float offsetX = (x - lastX) * sensitivity;
            float offsetY = (lastY - y) * sensitivity;  // Инвертируем Y

            lastX = x;
            lastY = y;

            // Обновляем углы поворота
            yaw += offsetX;
            pitch += offsetY;
            
            // Ограничиваем угол pitch чтобы камера не переворачивалась
            if (pitch > 89.0f) pitch = 89.0f;
            if (pitch < -89.0f) pitch = -89.0f;

            // Обновляем направление взгляда
            UpdateTarget();
        }

        // Обновляет точку, на которую смотрит камера
        // Использует углы yaw и pitch для вычисления направления
        private void UpdateTarget()
        {
            // Вычисляем направление взгляда используя сферические координаты
            float yawRad = yaw * (float)(Math.PI / 180.0);
            float pitchRad = pitch * (float)(Math.PI / 180.0);
            var direction = new Vector3(
                (float)(Math.Cos(yawRad) * Math.Cos(pitchRad)),
                (float)(Math.Sin(pitchRad)),
                (float)(Math.Sin(yawRad) * Math.Cos(pitchRad))
            );
            
            // Точка, на которую смотрим = позиция + направление
            Target = Position + direction;
        }

        // Обновляет позицию камеры в зависимости от нажатых клавиш
        public void Update()
        {
            // Вычисляем векторы направления
            var forward = Target - Position;  // Вперед
            forward.Normalize();
            
            var right = Vector3.Cross(forward, Vector3.UnitY);  // Вправо
            right.Normalize();
            
            var move = Vector3.Zero;

            // Обрабатываем нажатые клавиши
            if (pressedKeys.Contains(Keys.W)) move += forward;  // Вперед
            if (pressedKeys.Contains(Keys.S)) move -= forward;  // Назад
            if (pressedKeys.Contains(Keys.A)) move -= right;   // Влево
            if (pressedKeys.Contains(Keys.D)) move += right;   // Вправо
            if (pressedKeys.Contains(Keys.Q)) move += Vector3.UnitY;  // Вверх
            if (pressedKeys.Contains(Keys.E)) move -= Vector3.UnitY;  // Вниз

            // Если есть движение - обновляем позицию камеры и цели
            if (move.LengthSquared > 0)
            {
                move.Normalize();
                Position += move * speed;
                Target += move * speed;
            }
        }
    }
}

