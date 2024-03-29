namespace AudioSteganography_Winforms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ioRichTextBox = new RichTextBox();
            selectFileButton = new Button();
            encodeButton = new Button();
            decodeButton = new Button();
            filenameLabel = new Label();
            logListBox = new ListBox();
            selectOpenFileDialog = new OpenFileDialog();
            resultSaveFileDialog = new SaveFileDialog();
            SuspendLayout();
            // 
            // ioRichTextBox
            // 
            ioRichTextBox.Location = new Point(11, 12);
            ioRichTextBox.Name = "ioRichTextBox";
            ioRichTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            ioRichTextBox.Size = new Size(361, 433);
            ioRichTextBox.TabIndex = 0;
            ioRichTextBox.Text = "Программа \"Стеганография-Аудио\" позволяет кодировать и декодировать скрытые сообщения в звуковых файлах. Для начала работы с программой выберите файл и следуйте дальнейшим инструкциям.";
            // 
            // selectFileButton
            // 
            selectFileButton.Location = new Point(12, 515);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(360, 34);
            selectFileButton.TabIndex = 1;
            selectFileButton.Text = "Выбрать звуковой файл";
            selectFileButton.UseVisualStyleBackColor = true;
            selectFileButton.Click += selectFileButton_Click;
            // 
            // encodeButton
            // 
            encodeButton.Location = new Point(11, 451);
            encodeButton.Name = "encodeButton";
            encodeButton.Size = new Size(168, 36);
            encodeButton.TabIndex = 2;
            encodeButton.Text = "Закодировать";
            encodeButton.UseVisualStyleBackColor = true;
            encodeButton.Click += encodeButton_Click;
            // 
            // decodeButton
            // 
            decodeButton.Location = new Point(204, 451);
            decodeButton.Name = "decodeButton";
            decodeButton.Size = new Size(168, 36);
            decodeButton.TabIndex = 3;
            decodeButton.Text = "Раскодировать";
            decodeButton.UseVisualStyleBackColor = true;
            decodeButton.Click += decodeButton_Click;
            // 
            // filenameLabel
            // 
            filenameLabel.AutoSize = true;
            filenameLabel.Location = new Point(12, 497);
            filenameLabel.Name = "filenameLabel";
            filenameLabel.Size = new Size(274, 15);
            filenameLabel.TabIndex = 4;
            filenameLabel.Text = "Пожалуйста, выберите файл для начала работы";
            filenameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // logListBox
            // 
            logListBox.FormattingEnabled = true;
            logListBox.HorizontalScrollbar = true;
            logListBox.ItemHeight = 15;
            logListBox.Items.AddRange(new object[] { "Программа запущена" });
            logListBox.Location = new Point(388, 12);
            logListBox.Name = "logListBox";
            logListBox.Size = new Size(244, 529);
            logListBox.TabIndex = 5;
            // 
            // selectOpenFileDialog
            // 
            selectOpenFileDialog.DefaultExt = "wav";
            selectOpenFileDialog.Filter = "WAV-файл|*.wav";
            selectOpenFileDialog.InitialDirectory = "AppContext.BaseDirectory";
            selectOpenFileDialog.FileOk += selectOpenFileDialog_FileOk;
            // 
            // resultSaveFileDialog
            // 
            resultSaveFileDialog.DefaultExt = "wav";
            resultSaveFileDialog.Filter = "WAV-файл|*.wav";
            resultSaveFileDialog.OkRequiresInteraction = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(644, 561);
            Controls.Add(logListBox);
            Controls.Add(filenameLabel);
            Controls.Add(decodeButton);
            Controls.Add(encodeButton);
            Controls.Add(selectFileButton);
            Controls.Add(ioRichTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimumSize = new Size(660, 600);
            Name = "MainForm";
            Text = "Стеганография-Аудио";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox ioRichTextBox;
        private Button selectFileButton;
        private Button encodeButton;
        private Button decodeButton;
        private Label filenameLabel;
        private ListBox logListBox;
        private OpenFileDialog selectOpenFileDialog;
        private SaveFileDialog resultSaveFileDialog;
    }
}