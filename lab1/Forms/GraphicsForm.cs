using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GraphicsLab
{
    // Главная форма приложения
    // Содержит интерфейс пользователя и управляет отрисовкой фигур
    public class GraphicsForm : Form
    {
        // Элементы интерфейса - группы для организации элементов
        private GroupBox _controlGroup;      // Группа с кнопками управления
        private GroupBox _canvasGroup;       // Группа с областью рисования
        private GroupBox _optionsGroup;      // Группа с настройками фигур
        
        // Элементы управления
        private ComboBox _shapeSelector;     // Выпадающий список для выбора типа фигуры
        private Panel _drawingArea;          // Панель для рисования
        private DrawingCanvas _canvas;       // Объект для управления рисованием
        
        // Элементы для работы с произвольным многоугольником
        private TextBox _coordX;             // Поле ввода координаты X
        private TextBox _coordY;             // Поле ввода координаты Y
        private Button _addVertexButton;     // Кнопка добавления вершины
        private ListBox _vertexList;         // Список вершин многоугольника
        
        // Кнопки управления
        private Button _activateButton;       // Кнопка активации графики
        private Button _renderButton;         // Кнопка рисования фигуры
        private Button _deleteButton;        // Кнопка удаления фигуры
        private Button _resetButton;         // Кнопка очистки области
        
        // Текущая активная фигура
        private IShape _activeShape;

        // Конструктор формы
        // Инициализирует форму и все элементы интерфейса
        public GraphicsForm()
        {
            InitializeForm();
            SetupControls();
            LayoutControls();
        }

        // Инициализация основных параметров формы
        private void InitializeForm()
        {
            Text = "Графический редактор - Лабораторная 1";
            Size = new Size(850, 650);
            StartPosition = FormStartPosition.CenterScreen;
        }

        // Настройка элементов управления
        // Создает все необходимые элементы интерфейса
        private void SetupControls()
        {
            // Создаем группы для организации элементов
            _controlGroup = new GroupBox { Text = "Панель управления" };
            _canvasGroup = new GroupBox { Text = "Область рисования" };
            _optionsGroup = new GroupBox { Text = "Параметры" };
            
            // Настраиваем выпадающий список типов фигур
            _shapeSelector = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Отрезок", "Овал", "Правильный многоугольник", "Произвольный многоугольник" }
            };
            _shapeSelector.SelectedIndex = 0;
            _shapeSelector.SelectedIndexChanged += OnShapeTypeChanged;
            
            // Создаем панель для рисования
            _drawingArea = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            
            // Создаем кнопки управления через вспомогательный класс
            _activateButton = UIHelper.CreateControlButton("Активировать графику", 0, 0, handler: OnActivateClick);
            _renderButton = UIHelper.CreateControlButton("Нарисовать фигуру", 0, 0, handler: OnRenderClick);
            _deleteButton = UIHelper.CreateControlButton("Удалить фигуру", 0, 0, handler: OnDeleteClick);
            _resetButton = UIHelper.CreateControlButton("Очистить всё", 0, 0, handler: OnResetClick);
            
            // Настраиваем элементы для произвольного многоугольника
            SetupPolygonControls();
        }

        // Настройка элементов для ввода вершин произвольного многоугольника
        private void SetupPolygonControls()
        {
            _coordX = new TextBox { Width = 80 };
            _coordY = new TextBox { Width = 80 };
            _addVertexButton = UIHelper.CreateControlButton("Добавить вершину", 0, 0, 170, 30, OnAddVertexClick);
            _vertexList = new ListBox { Height = 150 };
        }

        // Размещение элементов на форме
        // Устанавливает позиции и размеры всех элементов
        private void LayoutControls()
        {
            // Размещаем группу управления слева
            _controlGroup.Location = new Point(10, 10);
            _controlGroup.Size = new Size(200, 240);
            
            // Размещаем элементы внутри группы управления
            int buttonY = 25;
            _shapeSelector.Location = new Point(10, buttonY);
            _shapeSelector.Width = 180;
            buttonY += 35;
            
            _activateButton.Location = new Point(10, buttonY);
            buttonY += 35;
            _renderButton.Location = new Point(10, buttonY);
            buttonY += 35;
            _deleteButton.Location = new Point(10, buttonY);
            buttonY += 35;
            _resetButton.Location = new Point(10, buttonY);
            
            _controlGroup.Controls.AddRange(new Control[] 
            { 
                _shapeSelector, _activateButton, _renderButton, _deleteButton, _resetButton 
            });
            
            // Размещаем группу с областью рисования по центру
            _canvasGroup.Location = new Point(220, 10);
            _canvasGroup.Size = new Size(600, 520);
            
            _drawingArea.Location = new Point(10, 20);
            _drawingArea.Size = new Size(580, 490);
            _canvasGroup.Controls.Add(_drawingArea);
            
            // Инициализируем canvas после установки размеров панели
            // Это важно, чтобы canvas знал правильные размеры области рисования
            _canvas = new DrawingCanvas(_drawingArea);
            
            // Размещаем группу с настройками внизу слева
            _optionsGroup.Location = new Point(10, 260);
            _optionsGroup.Size = new Size(200, 270);
            UpdateOptionsPanel();
            
            // Добавляем все группы на форму
            Controls.AddRange(new Control[] { _controlGroup, _canvasGroup, _optionsGroup });
        }

        // Обновление панели настроек в зависимости от выбранного типа фигуры
        // Для произвольного многоугольника показываем поля ввода координат
        private void UpdateOptionsPanel()
        {
            _optionsGroup.Controls.Clear();
            
            // Если выбран произвольный многоугольник, показываем элементы ввода
            if (_shapeSelector.SelectedIndex == 3)
            {
                _optionsGroup.Text = "Координаты вершин";
                _coordX.Location = new Point(10, 20);
                _coordY.Location = new Point(100, 20);
                _addVertexButton.Location = new Point(10, 50);
                _vertexList.Location = new Point(10, 85);
                _vertexList.Width = 180;
                
                _optionsGroup.Controls.AddRange(new Control[] 
                { 
                    _coordX, _coordY, _addVertexButton, _vertexList 
                });
            }
            else
            {
                _optionsGroup.Text = "Параметры недоступны";
            }
        }

        // Обработчик изменения типа фигуры
        // Обновляет панель настроек и сбрасывает активную фигуру
        private void OnShapeTypeChanged(object sender, EventArgs e)
        {
            UpdateOptionsPanel();
            _activeShape = null;
        }

        // Обработчик нажатия кнопки активации графики
        // В текущей реализации не выполняет действий
        private void OnActivateClick(object sender, EventArgs e)
        {
            // Функция активации графики (можно добавить логику)
        }

        // Обработчик нажатия кнопки рисования фигуры
        // Создает фигуру выбранного типа и добавляет её на canvas
        private void OnRenderClick(object sender, EventArgs e)
        {
            int selectedIndex = _shapeSelector.SelectedIndex;
            
            // В зависимости от выбранного типа создаем соответствующую фигуру
            switch (selectedIndex)
            {
                case 0: // Отрезок
                    _activeShape = ShapeFactory.CreateLine(
                        new Point(50, 50), 
                        new Point(200, 200), 
                        3, 
                        Color.Black, 
                        Color.White
                    );
                    break;
                    
                case 1: // Овал
                    _activeShape = ShapeFactory.CreateEllipse(
                        new Point(150, 150), 
                        80, 50, 
                        Color.Red, 
                        Color.Yellow, 
                        _drawingArea.BackColor, 
                        "Эллипс", 
                        Color.Black
                    );
                    break;
                    
                case 2: // Правильный многоугольник
                    // Создаем правильный пятиугольник
                    _activeShape = ShapeFactory.CreateRegularPolygon(
                        new Point(200, 200), 
                        5, 80, 
                        2, 
                        Color.Green, 
                        Color.LightGreen, 
                        _drawingArea.BackColor
                    );
                    break;
                    
                case 3: // Произвольный многоугольник
                    // Проверяем, что добавлено минимум 3 вершины
                    if (_vertexList.Items.Count < 3)
                    {
                        MessageBox.Show("Необходимо минимум 3 вершины", "Ошибка", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // Преобразуем строки из списка в массив точек
                    var vertices = new PointF[_vertexList.Items.Count];
                    for (int i = 0; i < _vertexList.Items.Count; i++)
                    {
                        // Разбиваем строку "x y" на координаты
                        string[] parts = _vertexList.Items[i].ToString().Split(' ');
                        vertices[i] = new PointF(
                            Convert.ToSingle(parts[0]), 
                            Convert.ToSingle(parts[1])
                        );
                    }
                    
                    // Создаем многоугольник по заданным точкам
                    _activeShape = ShapeFactory.CreateCustomPolygon(
                        vertices, 
                        3, 
                        Color.Black, 
                        Color.Blue, 
                        Color.White
                    );
                    break;
            }
            
            // Если фигура создана успешно, добавляем её на canvas
            if (_activeShape != null)
            {
                _canvas.AddShape(_activeShape);
            }
        }

        // Обработчик нажатия кнопки удаления фигуры
        // Удаляет последнюю добавленную фигуру
        private void OnDeleteClick(object sender, EventArgs e)
        {
            _canvas.RemoveLastShape();
            _activeShape = null;
        }

        // Обработчик нажатия кнопки очистки
        // Удаляет все фигуры и очищает область рисования
        private void OnResetClick(object sender, EventArgs e)
        {
            _canvas.ClearShapes();
            _canvas.Clear(_drawingArea.BackColor);
            _activeShape = null;
        }

        // Обработчик нажатия кнопки добавления вершины
        // Добавляет новую вершину в список для произвольного многоугольника
        private void OnAddVertexClick(object sender, EventArgs e)
        {
            // Пытаемся распарсить координаты из текстовых полей
            if (int.TryParse(_coordX.Text, out int x) && int.TryParse(_coordY.Text, out int y))
            {
                // Добавляем вершину в список в формате "x y"
                _vertexList.Items.Add($"{x} {y}");
                // Очищаем поля ввода для следующей вершины
                _coordX.Clear();
                _coordY.Clear();
            }
            else
            {
                // Если координаты некорректны, показываем сообщение об ошибке
                MessageBox.Show("Введите корректные числовые координаты", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Точка входа в приложение
        [STAThread]
        public static void Main()
        {
            // Включаем визуальные стили Windows
            Application.EnableVisualStyles();
            // Отключаем совместимость со старым рендерингом текста
            Application.SetCompatibleTextRenderingDefault(false);
            // Запускаем главную форму
            Application.Run(new GraphicsForm());
        }
    }
}
