using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSearcher
{
    public partial class fMain : Form
    {
        ManualResetEvent mre = new ManualResetEvent(true);      // Экземпляр класса ManualResetEvent для приостановки потока поиска
        Search search;                                          // Экземпляр класса Search для осуществления поиска
        BackgroundWorker worker;                                // Экземпляр класса BackgroundWorker для запуска поиска в отдельном потоке
        ModifiedTreeView mtv;                                   // Экземпляр модифицированного класса TreeView для исключения мерцания при обновлении

        // Поля для работы таймера
        DateTime dt;
        TimeSpan tsPaused;
        DateTime dtPauseStart;
        DateTime dtPauseCancel;

        public fMain()
        {
            InitializeComponent();

            buttonSearch.Tag = "search";                        // Установка тега для кнопки поиск для определения ее текущего назначения

            // Инициализация ModifiedTreeView
            mtv = new ModifiedTreeView();
            mtv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            mtv.Location = new System.Drawing.Point(6, 19);
            mtv.Name = "treeViewSearchResults";
            mtv.Size = new System.Drawing.Size(groupBoxSearchResults.Width - 12, 445);
            mtv.TabIndex = 0;
            groupBoxSearchResults.Controls.Add(mtv);
        }

        //
        // Обработчики событий
        //

        // Считывание файла конфигурации при загрузке формы для установки стартовой директории поиска и регулярного выражения
        private void fMain_Load(object sender, EventArgs e)
        {
            using (FileStream fstream = new FileStream(@".\settings.txt", FileMode.OpenOrCreate))
            {
                byte[] byteArray = new byte[fstream.Length];
                fstream.Read(byteArray, 0, byteArray.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(byteArray);
                string[] stringArray = textFromFile.Split('\n');
                if (stringArray.Length > 0) textBoxStartFolder.Text = stringArray[0].Trim();
                if (stringArray.Length > 1) textBoxFileRegexp.Text = stringArray[1];
            }
        }

        // Запись файла конфигурации при закрытии формы для установки стартовой директории поиска и регулярного выражения после перезапуска приложения
        private void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (FileStream fstream = new FileStream(@".\settings.txt", FileMode.Truncate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(textBoxStartFolder.Text + "\r\n" + textBoxFileRegexp.Text);
                fstream.Write(array, 0, array.Length);
            }
        }

        // Выбор стартовой директории через FolderBrowserDialog
        private void buttonStartFolderExplore_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxStartFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if ((string)(sender as Button).Tag == "pause")
            {
                // Определение текущего назначения кнопки buttonSearch по ее тегу и установка соответствующего текста
                (sender as Button).Tag = "continue";
                (sender as Button).Text = ">";

                dtPauseStart = DateTime.Now;                                // Сохранение времени постановки на паузу для определения продолжительности паузы
                timer.Stop();                                               // Остановка таймера при постановке на паузу

                mre.Reset();                                                // Установка ManualResetEvent в несигнальное состояние
            }
            else if ((string)(sender as Button).Tag == "continue")
            {
                // Определение текущего назначения кнопки buttonSearch по ее тегу и установка соответствующего текста
                (sender as Button).Tag = "pause";
                (sender as Button).Text = "| |";

                dtPauseCancel = DateTime.Now;                               // Сохранение времени снятия с паузы для определения продолжительности паузы
                tsPaused += dtPauseCancel - dtPauseStart;                   // Определение продолжительности паузы (после очередной паузы складывается с предыдущим значением)
                timer.Start();                                              // Запуск таймера после снятия с паузы

                mre.Set();                                                  // Установка ManualResetEvent в сигнальное состояние
            }
            else if ((string)(sender as Button).Tag == "search")
            {
                // Инициализация экземпляра BackgroundWorker
                worker = new BackgroundWorker();
                // Инициализация экземпляра Search
                search = new Search(textBoxStartFolder.Text, textBoxFileRegexp.Text, mre, worker);

                dt = DateTime.Now;                                          // Сохранение времени запуска поиска
                tsPaused = new TimeSpan();                                  // Инициализация экземпляра TimeSpan для сохранения продолжительности паузы
                timer.Start();                                              // Запуск таймера

                // Определение текущего назначения кнопки buttonSearch по ее тегу и установка соответствующего текста
                (sender as Button).Tag = "pause";
                (sender as Button).Text = "| |";

                // Включение кнопки "Отмена" и установка тега в false для определения причины остановки поиска
                buttonCancelSearch.Tag = false;
                buttonCancelSearch.Enabled = true;

                mre.Set();                                                  // Установка ManualResetEvent в сигнальное состояние
                mtv.Nodes.Clear();                                          // Очистка узлов ModifiedTreeView после предыдущего поиска

                // Инициализация BacgroundWorker
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }

            // Включение видимости toolStripStatusLabel'ов
            toolStripStatusLabelFoundedFiles.Visible = true;
            toolStripStatusLabelCurrentDirectory.Visible = true;
            toolStripStatusLabelTimer.Visible = true;
        }

        private void buttonCanselSearch_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();                                           // Запрос остановки BackgroundWorker
                                                                            // (проверка CancellationPendingToken осуществляется в классе Search)

            mre.Set();                                                      // Установка ManualResetEvent в сигнальное состояние для того, чтобы событие
                                                                            // окончания поиска произошло сразу, даже в случае, если поиск сейчас на паузе

            buttonCancelSearch.Tag = true;                                  // Установка тега кнопки отмена в true для определения причины остановки
                                                                            // поиска как "отмена"

            // Отключение видимости toolStripStatusLabel'ов
            toolStripStatusLabelFoundedFiles.Visible = false;
            toolStripStatusLabelCurrentDirectory.Visible = false;
            toolStripStatusLabelTimer.Visible = false;
        }

        // Запуск поиска в отдельном потоке
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            search.GoSearch();
        }

        // Отслеживание прогресса поиска
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Сохранение промежуточных результатов поиска
            SearchResults r = (SearchResults)e.UserState;

            // Если в промежуточных результатах не содержится сведений о возникновении исключения
            if (r.Exception == null)
            {
                // Если в промежуточных результатах содержится сведения о найденном файле - вызов метода treeViewBuilder,
                // создающего узел дерева в соответствии с переданным путем
                if (r.CurrentFoundFilePath != null) treeViewBuilder(r.CurrentFoundFilePath.Split('\\'), 0, null, mtv);

                // Установка значений toolStripStatusLabel'ов в соответствии с общим количеством файлов и количеством найденных совпадений
                toolStripStatusLabelFoundedFiles.Text = "Найдено совпадений: " + r.FoundFiles + " из " + r.FilesCount;

                // Если в промежуточных результатах есть сведения о текущем каталоге поиска
                if (r.CurrentDirectory != null)
                {
                    // Установка подсказки для отображения пути к текущему каталогу поиска
                    toolStripStatusLabelCurrentDirectory.ToolTipText = r.CurrentDirectory;

                    // Установка значения Text toolStripStatusLabel для отображения текущего каталога в зависимости от длины пути
                    if (r.CurrentDirectory.Length > 35) toolStripStatusLabelCurrentDirectory.Text = "Текущая директория: " + r.CurrentDirectory.Remove(35) + "...";
                    else toolStripStatusLabelCurrentDirectory.Text = "Текущая директория: " + r.CurrentDirectory;
                }
            }
            // Если в промежуточных результатах есть сведения о возникновении исключения
            else
            {
                MessageBox.Show(r.Exception.Message);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Stop();

            // Проверять причину завершения работы BackgroundWorker не получается, потому что цикл выполняется не в обработчике DoWork,
            // поэтому не получается установить статус Cancelled

            // Проверяем отменена ли работа BackgroundWorker через проверку buttonCancelSearch.Tag
            if ((bool)buttonCancelSearch.Tag == true)
            {
                buttonCancelSearch.Tag = false;
                MessageBox.Show("Поиск отменен!");
            }
            // Если поиск был завершен не в результате отмены
            else
            {
                MessageBox.Show("Поиск закончен!");
            }

            // Переключение назначения кнопки buttonSearch в "Поиск"
            buttonSearch.Tag = "search";
            buttonSearch.Text = "Поиск";

            // Отключение кнопки "Отмена"
            buttonCancelSearch.Enabled = false;
        }

        // Обработка очередного тика таймера
        private void timer_Tick(object sender, EventArgs e)
        {
            // Вычисление продолжительности поиска (исключается суммарная продолжительность пауз)
            string timeToShow = ((DateTime.Now - dt) - tsPaused).ToString(@"hh\:mm\:ss");

            // Показ прошедшего времени с начала поиска
            toolStripStatusLabelTimer.Text = timeToShow;
        }

        private void treeViewBuilder(string[] splitPath, int treeLevel, TreeNode node, TreeView tree)
        {
            // Текущий обрабатываемый узел дерева
            TreeNode currentNode;

            if (treeLevel < splitPath.Length)                                       // Проверка текущего обрабатываемого уровня дерева на превышение длины массива splitPath
            {
                if (node == null)                                                   // Если текущий передаваемый узел дерева равен нулю (при первой итерации построения дерева)
                {
                    // Если в дереве не содержится коренвого узла
                    // splitPath[0] - добавляем его
                    if (!(tree.Nodes.ContainsKey(splitPath[0]))) tree.Nodes.Add(splitPath[0], splitPath[0]);

                    // Сохранение корневого узла в currentNode
                    currentNode = tree.Nodes[splitPath[0]];

                    // Инкрементация текущего обрабатываемого уровня дерева
                    treeLevel++;

                    // Рекурсивный вызов метода treeViewBuilder с передачей
                    // инкрементированного уровня дерева и корневого узла
                    treeViewBuilder(splitPath, treeLevel, currentNode, tree);
                }
                else                                                                // Если происходит не первая итерация построения дерева
                                                                                    // (в метод поступил ненулевой параметр node)
                {
                    // Если в дереве не содержится текущего обрабатываемого
                    // узла splitPath[0] - добавляем его
                    if (!(node.Nodes.ContainsKey(splitPath[treeLevel]))) node.Nodes.Add(splitPath[treeLevel], splitPath[treeLevel]);

                    // Сохранение текущего узла в currentNode
                    currentNode = node.Nodes[splitPath[treeLevel]];

                    // Инкрементация текущего обрабатываемого уровня дерева
                    treeLevel++;

                    // Рекурсивный вызов метода treeViewBuilder с передачей
                    // инкрементированного уровня дерева и текущего узла
                    treeViewBuilder(splitPath, treeLevel, currentNode, tree);
                }
            }
        }
    }
}
