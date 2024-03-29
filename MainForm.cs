namespace AudioSteganography_Winforms
{
    public partial class MainForm : Form
    {
        private string audioFilePath = "";
        private WavReader audio;
        private long maxLength;

        public MainForm()
        {
            InitializeComponent();
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            selectOpenFileDialog.ShowDialog();
        }

        private void selectOpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Path.GetExtension(selectOpenFileDialog.FileName) != ".wav")
            {
                selectOpenFileDialog.FileName = "";
                MessageBox.Show("Пожалуйста, выберите звуковой файл подходящего формата!", "Некорректный файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    audioFilePath = selectOpenFileDialog.FileName;
                    selectOpenFileDialog.FileName = "";
                    audio = new WavReader(audioFilePath);
                    if (audio.samplesCount < 24)
                    {
                        throw new Exception("Не удалось прочитать заголовок");
                    }
                    maxLength = audio.samplesCount / 8 - 16;
                    filenameLabel.Text = string.Format("Выбран файл \"{0}\"", audioFilePath);
                    logListBox.Items.Add(string.Format("Выбран звуковой файл \"{0}\"", Path.GetFileName(audioFilePath)));
                    ioRichTextBox.Text = string.Format("Пожалуйста, введите текст сообщения для кодирования.\nМаксимальная длина сообщения: {0} символов\n---\nили нажмите кнопку \"Раскодировать\", чтобы вывести скрытое сообщение.", maxLength);
                    ioRichTextBox.ScrollToCaret();
                }
                catch (Exception)
                {
                    MessageBox.Show("В процессе чтения файла возникла неизвестная ошибка!", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logListBox.Items.Add("Возникла неизвестная ошибка чтения файла");
                    audioFilePath = "";
                }
            }
        }

        private void encodeButton_Click(object sender, EventArgs e)
        {
            if (audioFilePath == "")
            {
                MessageBox.Show("Для начала кодирования выберите файл!", "Файл не выбран", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ioRichTextBox.TextLength > maxLength)
            {
                MessageBox.Show(string.Format("Максимальный размер для выбранного файла {0} символов.", maxLength, ioRichTextBox.TextLength), "Превышен размер сообщения!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ioRichTextBox.AppendText(string.Format("\n---\n{0} символов", ioRichTextBox.TextLength));
                ioRichTextBox.ScrollToCaret();
                return;
            }
            try
            {
                ioRichTextBox.Text = ioRichTextBox.Text.ReplaceLineEndings("\n");
                var encodedData = Steganography.Encode(audio.dataBits, ioRichTextBox.Text, audio.bitsPerSample);
                byte[] eDataBytes = new byte[(encodedData.Count + 7) / 8];
                encodedData.CopyTo(eDataBytes, 0);
                if (resultSaveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                while (!(resultSaveFileDialog.FileName != "" && Path.GetExtension(resultSaveFileDialog.FileName) == ".wav"))
                {
                    MessageBox.Show("Пожалуйста, для сохранения закодированного файла напишите корректное имя файла с расширением .wav!", "Некорректное имя файла", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    resultSaveFileDialog.FileName = "";
                    if (resultSaveFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
                var savePath = resultSaveFileDialog.FileName;
                resultSaveFileDialog.FileName = "";
                WavWriter.Write(savePath, audio.headerBytes, eDataBytes);
                logListBox.Items.Add(string.Format("Выполнено кодирование {0} символов", ioRichTextBox.TextLength));
                MessageBox.Show(string.Format("В файл \"{1}\" успешно закодировано сообщение из {0} символов.", ioRichTextBox.TextLength, Path.GetFileName(savePath)), "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                savePath = "";
                audioFilePath = "";
                filenameLabel.Text = "Пожалуйста, выберите файл для начала работы";
                ioRichTextBox.Text = "Введите в это поле текст, который требуется закодировать\nили\nвыберите файл и нажмите кнопку \"Раскодировать\", чтобы получить информацию";
            }
            catch (Exception)
            {
                MessageBox.Show("В процессе кодирования возникла неизвестная ошибка!", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logListBox.Items.Add("Возникла неизвестная ошибка кодирования");
            }
        }

        private void decodeButton_Click(object sender, EventArgs e)
        {
            if (audioFilePath == "")
            {
                MessageBox.Show("Для начала декодирования выберите файл!", "Файл не выбран", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var decodedMessage = Steganography.Decode(audio.dataBits, audio.bitsPerSample);
                ioRichTextBox.Text = decodedMessage;
                MessageBox.Show(string.Format("Из файла \"{1}\" успешно декодировано сообщение из {0} символов.", ioRichTextBox.TextLength, Path.GetFileName(audioFilePath)), "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logListBox.Items.Add(string.Format("Выполнено декодирование {0} символов", ioRichTextBox.TextLength));
                audioFilePath = "";
                filenameLabel.Text = "Пожалуйста, выберите файл для начала работы";
            }
            catch (Exception)
            {
                MessageBox.Show("В процессе декодирования возникла неизвестная ошибка! Возможно, в этом файле нет скрытого сообщения.", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logListBox.Items.Add("Возникла неизвестная ошибка декодирования");
            }
        }
    }
}