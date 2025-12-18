using System;
using System.Drawing;
using System.Windows.Forms;
using Lab8.Renderers;

namespace Lab8.Forms
{
    // Частичный класс для UI элементов камеры
    // Разделен на отдельный файл для удобства
    public partial class MainForm
    {
        // Поля ввода для отображения позиции и направления камеры
        private TextBox tX, tY, tZ, tdirX, tdirY, tdirZ;

        // Добавляет UI элементы для отображения информации о камере
        private void AddCameraUI()
        {
            // Создаем метки для позиции камеры
            var labelX = new Label() { Text = "X:", Left = 820, Top = 20, Width = 20 };
            var labelY = new Label() { Text = "Y:", Left = 820, Top = 50, Width = 20 };
            var labelZ = new Label() { Text = "Z:", Left = 820, Top = 80, Width = 20 };

            // Создаем поля ввода для позиции (только для чтения)
            tX = new TextBox() { Left = 850, Top = 20, Width = 100, ReadOnly = true };
            tY = new TextBox() { Left = 850, Top = 50, Width = 100, ReadOnly = true };
            tZ = new TextBox() { Left = 850, Top = 80, Width = 100, ReadOnly = true };

            // Отключаем Tab для этих полей
            tX.TabStop = false;
            tY.TabStop = false;
            tZ.TabStop = false;

            // Добавляем элементы на форму
            this.Controls.Add(labelX);
            this.Controls.Add(labelY);
            this.Controls.Add(labelZ);
            this.Controls.Add(tX);
            this.Controls.Add(tY);
            this.Controls.Add(tZ);

            // Создаем поля ввода для направления камеры
            tdirX = new TextBox() { Left = 950, Top = 20, Width = 100, ReadOnly = true };
            tdirY = new TextBox() { Left = 950, Top = 50, Width = 100, ReadOnly = true };
            tdirZ = new TextBox() { Left = 950, Top = 80, Width = 100, ReadOnly = true };

            // Отключаем Tab для этих полей
            tdirX.TabStop = false;
            tdirY.TabStop = false;
            tdirZ.TabStop = false;

            // Добавляем элементы на форму
            this.Controls.Add(tdirX);
            this.Controls.Add(tdirY);
            this.Controls.Add(tdirZ);
        }

        // Обновляет текстовые поля с информацией о камере
        private void UpdateCameraText()
        {
            // Получаем позицию камеры
            var pos = render.camera.Position;
            tX.Text = pos.X.ToString("F2");
            tY.Text = pos.Y.ToString("F2");
            tZ.Text = pos.Z.ToString("F2");

            // Получаем направление камеры
            var dir = render.camera.Forward;
            tdirX.Text = dir.X.ToString("F2");
            tdirY.Text = dir.Y.ToString("F2");
            tdirZ.Text = dir.Z.ToString("F2");
        }
    }
}

