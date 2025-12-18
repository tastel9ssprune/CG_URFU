using System;
using System.Drawing;
using System.Windows.Forms;
using Lab5.Processors;

namespace Lab5.Forms
{
    // Главная форма приложения для работы с изображениями
    public class MainForm : Form
    {
        // Кнопки управления
        private Button buttonLoadImage;
        private Button buttonGetPixel;
        private Button buttonBrighten;
        
        // Компоненты для отображения изображений
        private PictureBox pictureBoxOriginal;
        private PictureBox pictureBoxModified;
        
        // Диалог для выбора файла
        private OpenFileDialog openFileDialog;
        
        // Поля ввода
        private TextBox textBoxX;
        private TextBox textBoxY;
        private TextBox textBoxBrightness;
        
        // Метки для подписей
        private Label labelResult;
        private Label labelX;
        private Label labelY;
        private Label labelBright;
        
        // Изображения
        private Bitmap loadedImage;
        private Bitmap modifiedImage;

        // Конструктор - создает форму и все элементы интерфейса
        public MainForm()
        {
            // Настраиваем форму
            this.Text = "Лабораторная работа 5 - Работа с изображениями";
            this.Width = 1000;
            this.Height = 720;

            // Создаем кнопки
            buttonLoadImage = new Button 
            {
                Left = 400, 
                Top = 10, 
                Width = 180, 
                Height = 30, 
                Text = "Загрузить изображение"
            };
            
            buttonGetPixel = new Button 
            {
                Left = 10, 
                Top = 575, 
                Width = 180, 
                Height = 30, 
                Text = "Проверить пиксель"
            };
            
            buttonBrighten = new Button 
            {
                Left = 500, 
                Top = 575, 
                Width = 200, 
                Height = 30, 
                Text = "Добавить яркость"
            };

            // Подключаем обработчики событий
            buttonLoadImage.Click += ButtonLoadImage_Click;
            buttonGetPixel.Click += ButtonGetPixel_Click;
            buttonBrighten.Click += ButtonBrighten_Click;

            // Добавляем кнопки на форму
            this.Controls.Add(buttonLoadImage);
            this.Controls.Add(buttonGetPixel);
            this.Controls.Add(buttonBrighten);

            // Создаем компоненты для отображения изображений
            pictureBoxOriginal = new PictureBox 
            {
                Top = 50, 
                Left = 10, 
                Width = 460, 
                Height = 500, 
                BorderStyle = BorderStyle.Fixed3D, 
                SizeMode = PictureBoxSizeMode.Zoom
            };
            
            pictureBoxModified = new PictureBox 
            {
                Top = 50, 
                Left = 500, 
                Width = 460, 
                Height = 500, 
                BorderStyle = BorderStyle.Fixed3D, 
                SizeMode = PictureBoxSizeMode.Zoom
            };
            
            this.Controls.Add(pictureBoxOriginal);
            this.Controls.Add(pictureBoxModified);

            // Создаем метки и поля ввода для координат
            labelX = new Label { Top = 580, Left = 210, Width = 20, Text = "X:" };
            labelY = new Label { Top = 580, Left = 290, Width = 20, Text = "Y:" };
            textBoxX = new TextBox { Top = 580, Left = 230, Width = 50 };
            textBoxY = new TextBox { Top = 580, Left = 310, Width = 50 };

            // Создаем метку и поле ввода для яркости
            labelBright = new Label { Top = 580, Left = 730, Width = 130, Text = "Прибавка яркости:" };
            textBoxBrightness = new TextBox { Top = 580, Left = 860, Width = 40 };

            // Создаем метку для вывода результата
            labelResult = new Label { Top = 620, Left = 10, Width = 800, Height = 30 };

            // Добавляем все элементы на форму
            this.Controls.Add(labelX);
            this.Controls.Add(textBoxX);
            this.Controls.Add(labelY);
            this.Controls.Add(textBoxY);
            this.Controls.Add(labelBright);
            this.Controls.Add(textBoxBrightness);
            this.Controls.Add(labelResult);

            // Настраиваем диалог выбора файла
            openFileDialog = new OpenFileDialog 
            { 
                Filter = "Изображения|*.bmp;*.jpg;*.jpeg;*.png" 
            };
        }

        // Обработчик нажатия на кнопку "Загрузить изображение"
        private void ButtonLoadImage_Click(object sender, EventArgs e)
        {
            // Показываем диалог выбора файла
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Загружаем изображение из выбранного файла
                loadedImage = new Bitmap(openFileDialog.FileName);
                
                // Отображаем исходное изображение
                pictureBoxOriginal.Image = loadedImage;
                
                // Очищаем модифицированное изображение
                pictureBoxModified.Image = null;
            }
        }

        // Обработчик нажатия на кнопку "Проверить пиксель"
        private void ButtonGetPixel_Click(object sender, EventArgs e)
        {
            // Проверяем что изображение загружено
            if (loadedImage == null)
            {
                MessageBox.Show("Загрузите изображение!!!!");
                return;
            }

            // Пытаемся распарсить координаты
            if (!int.TryParse(textBoxX.Text, out int x) || !int.TryParse(textBoxY.Text, out int y))
            {
                MessageBox.Show("Введите корректные числа!!");
                return;
            }

            // Получаем информацию о пикселе и выводим ее
            string info = ImageProcessor.GetPixelInfo(loadedImage, x, y);
            labelResult.Text = info;
            
            // Если координаты невалидны, показываем сообщение
            if (info == "Координаты вне изображения")
            {
                MessageBox.Show("Координаты вне изображения");
            }
        }

        // Обработчик нажатия на кнопку "Добавить яркость"
        private void ButtonBrighten_Click(object sender, EventArgs e)
        {
            // Проверяем что изображение загружено
            if (loadedImage == null)
            {
                MessageBox.Show("Загрузите изображение!!!!");
                return;
            }

            // Пытаемся распарсить значение яркости
            if (!int.TryParse(textBoxBrightness.Text, out int brightness))
            {
                MessageBox.Show("Введите корректные числа!!");
                return;
            }

            // Обрабатываем изображение - изменяем яркость
            modifiedImage = ImageProcessor.AdjustBrightness(loadedImage, brightness);
            
            // Отображаем модифицированное изображение
            pictureBoxModified.Image = modifiedImage;
        }
    }
}

