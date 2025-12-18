using System.Drawing;

namespace GraphicsLab
{
    // Интерфейс для всех графических фигур
    // Определяет базовые методы для рисования и удаления фигур
    public interface IShape
    {
        // Метод для отрисовки фигуры на графическом контексте
        void Render(Graphics graphics);
        
        // Метод для удаления фигуры (закрашивание цветом фона)
        void Remove(Graphics graphics);
    }
}
