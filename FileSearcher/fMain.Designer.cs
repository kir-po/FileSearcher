
namespace FileSearcher
{
    partial class fMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxFileRegexp = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.groupBoxSearchParameters = new System.Windows.Forms.GroupBox();
            this.buttonCancelSearch = new System.Windows.Forms.Button();
            this.labelSearchedFiles = new System.Windows.Forms.Label();
            this.buttonStartFolderExplore = new System.Windows.Forms.Button();
            this.labelStartFolder = new System.Windows.Forms.Label();
            this.textBoxStartFolder = new System.Windows.Forms.TextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBoxSearchResults = new System.Windows.Forms.GroupBox();
            this.statusStripSearchStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFoundedFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCurrentDirectory = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxSearchParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.statusStripSearchStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFileRegexp
            // 
            this.textBoxFileRegexp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileRegexp.Location = new System.Drawing.Point(9, 75);
            this.textBoxFileRegexp.Name = "textBoxFileRegexp";
            this.textBoxFileRegexp.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxFileRegexp.Size = new System.Drawing.Size(536, 20);
            this.textBoxFileRegexp.TabIndex = 0;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(551, 72);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Поиск";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // groupBoxSearchParameters
            // 
            this.groupBoxSearchParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSearchParameters.Controls.Add(this.buttonCancelSearch);
            this.groupBoxSearchParameters.Controls.Add(this.labelSearchedFiles);
            this.groupBoxSearchParameters.Controls.Add(this.buttonStartFolderExplore);
            this.groupBoxSearchParameters.Controls.Add(this.labelStartFolder);
            this.groupBoxSearchParameters.Controls.Add(this.textBoxStartFolder);
            this.groupBoxSearchParameters.Controls.Add(this.buttonSearch);
            this.groupBoxSearchParameters.Controls.Add(this.textBoxFileRegexp);
            this.groupBoxSearchParameters.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSearchParameters.Name = "groupBoxSearchParameters";
            this.groupBoxSearchParameters.Size = new System.Drawing.Size(713, 104);
            this.groupBoxSearchParameters.TabIndex = 1;
            this.groupBoxSearchParameters.TabStop = false;
            this.groupBoxSearchParameters.Text = "Параметры поиска";
            // 
            // buttonCancelSearch
            // 
            this.buttonCancelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancelSearch.Enabled = false;
            this.buttonCancelSearch.Location = new System.Drawing.Point(632, 72);
            this.buttonCancelSearch.Name = "buttonCancelSearch";
            this.buttonCancelSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelSearch.TabIndex = 6;
            this.buttonCancelSearch.Text = "Отмена";
            this.buttonCancelSearch.UseVisualStyleBackColor = true;
            this.buttonCancelSearch.Click += new System.EventHandler(this.buttonCanselSearch_Click);
            // 
            // labelSearchedFiles
            // 
            this.labelSearchedFiles.AutoSize = true;
            this.labelSearchedFiles.Location = new System.Drawing.Point(6, 55);
            this.labelSearchedFiles.Name = "labelSearchedFiles";
            this.labelSearchedFiles.Size = new System.Drawing.Size(231, 13);
            this.labelSearchedFiles.TabIndex = 5;
            this.labelSearchedFiles.Text = "Регулярное выражение для поиска файлов:";
            // 
            // buttonStartFolderExplore
            // 
            this.buttonStartFolderExplore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartFolderExplore.Location = new System.Drawing.Point(632, 17);
            this.buttonStartFolderExplore.Name = "buttonStartFolderExplore";
            this.buttonStartFolderExplore.Size = new System.Drawing.Size(75, 23);
            this.buttonStartFolderExplore.TabIndex = 4;
            this.buttonStartFolderExplore.Text = "Обзор...";
            this.buttonStartFolderExplore.UseVisualStyleBackColor = true;
            this.buttonStartFolderExplore.Click += new System.EventHandler(this.buttonStartFolderExplore_Click);
            // 
            // labelStartFolder
            // 
            this.labelStartFolder.AutoSize = true;
            this.labelStartFolder.Location = new System.Drawing.Point(6, 22);
            this.labelStartFolder.Name = "labelStartFolder";
            this.labelStartFolder.Size = new System.Drawing.Size(96, 13);
            this.labelStartFolder.TabIndex = 3;
            this.labelStartFolder.Text = "Стартовая папка:";
            // 
            // textBoxStartFolder
            // 
            this.textBoxStartFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStartFolder.Location = new System.Drawing.Point(108, 19);
            this.textBoxStartFolder.Name = "textBoxStartFolder";
            this.textBoxStartFolder.Size = new System.Drawing.Size(518, 20);
            this.textBoxStartFolder.TabIndex = 2;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 12);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.groupBoxSearchParameters);
            this.splitContainer.Panel1MinSize = 108;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.groupBoxSearchResults);
            this.splitContainer.Size = new System.Drawing.Size(719, 592);
            this.splitContainer.SplitterDistance = 110;
            this.splitContainer.TabIndex = 3;
            // 
            // groupBoxSearchResults
            // 
            this.groupBoxSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSearchResults.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSearchResults.Name = "groupBoxSearchResults";
            this.groupBoxSearchResults.Size = new System.Drawing.Size(713, 472);
            this.groupBoxSearchResults.TabIndex = 0;
            this.groupBoxSearchResults.TabStop = false;
            this.groupBoxSearchResults.Text = "Результаты поиска:";
            // 
            // statusStripSearchStatus
            // 
            this.statusStripSearchStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelFoundedFiles,
            this.toolStripStatusLabelCurrentDirectory,
            this.toolStripStatusLabelTimer});
            this.statusStripSearchStatus.Location = new System.Drawing.Point(0, 607);
            this.statusStripSearchStatus.Name = "statusStripSearchStatus";
            this.statusStripSearchStatus.ShowItemToolTips = true;
            this.statusStripSearchStatus.Size = new System.Drawing.Size(743, 22);
            this.statusStripSearchStatus.TabIndex = 4;
            this.statusStripSearchStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabelFoundedFiles
            // 
            this.toolStripStatusLabelFoundedFiles.Name = "toolStripStatusLabelFoundedFiles";
            this.toolStripStatusLabelFoundedFiles.Size = new System.Drawing.Size(129, 17);
            this.toolStripStatusLabelFoundedFiles.Text = "Найдено совпадений: ";
            this.toolStripStatusLabelFoundedFiles.Visible = false;
            // 
            // toolStripStatusLabelCurrentDirectory
            // 
            this.toolStripStatusLabelCurrentDirectory.AutoToolTip = true;
            this.toolStripStatusLabelCurrentDirectory.Name = "toolStripStatusLabelCurrentDirectory";
            this.toolStripStatusLabelCurrentDirectory.Size = new System.Drawing.Size(130, 17);
            this.toolStripStatusLabelCurrentDirectory.Text = "Текущая директория:  ";
            this.toolStripStatusLabelCurrentDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabelCurrentDirectory.Visible = false;
            // 
            // toolStripStatusLabelTimer
            // 
            this.toolStripStatusLabelTimer.Name = "toolStripStatusLabelTimer";
            this.toolStripStatusLabelTimer.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelTimer.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabelTimer.Visible = false;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 629);
            this.Controls.Add(this.statusStripSearchStatus);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(759, 668);
            this.Name = "fMain";
            this.Text = "FileSearcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fMain_FormClosed);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.groupBoxSearchParameters.ResumeLayout(false);
            this.groupBoxSearchParameters.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.statusStripSearchStatus.ResumeLayout(false);
            this.statusStripSearchStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxFileRegexp;
        private System.Windows.Forms.GroupBox groupBoxSearchParameters;
        private System.Windows.Forms.Button buttonStartFolderExplore;
        private System.Windows.Forms.Label labelStartFolder;
        private System.Windows.Forms.TextBox textBoxStartFolder;
        private System.Windows.Forms.Label labelSearchedFiles;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.StatusStrip statusStripSearchStatus;
        private System.Windows.Forms.GroupBox groupBoxSearchResults;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentDirectory;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFoundedFiles;
        private System.Windows.Forms.Button buttonCancelSearch;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTimer;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

