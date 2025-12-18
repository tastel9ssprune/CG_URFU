using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab8.Controllers
{
    // Класс для отслеживания нажатых клавиш
    // Хранит множество нажатых клавиш для управления камерой
    public static class KeyController
    {
        // Множество нажатых клавиш
        public static HashSet<Keys> pressedKeys = new HashSet<Keys>();

        // Добавляет клавишу в множество нажатых
        public static void KeyDown(Keys key) => pressedKeys.Add(key);
        
        // Удаляет клавишу из множества нажатых
        public static void KeyUp(Keys key) => pressedKeys.Remove(key);
    }
}

