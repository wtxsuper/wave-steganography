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
                MessageBox.Show("����������, �������� �������� ���� ����������� �������!", "������������ ����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        throw new Exception("�� ������� ��������� ���������");
                    }
                    maxLength = audio.samplesCount / 8 - 16;
                    filenameLabel.Text = string.Format("������ ���� \"{0}\"", audioFilePath);
                    logListBox.Items.Add(string.Format("������ �������� ���� \"{0}\"", Path.GetFileName(audioFilePath)));
                    ioRichTextBox.Text = string.Format("����������, ������� ����� ��������� ��� �����������.\n������������ ����� ���������: {0} ��������\n---\n��� ������� ������ \"�������������\", ����� ������� ������� ���������.", maxLength);
                    ioRichTextBox.ScrollToCaret();
                }
                catch (Exception)
                {
                    MessageBox.Show("� �������� ������ ����� �������� ����������� ������!", "����������� ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logListBox.Items.Add("�������� ����������� ������ ������ �����");
                    audioFilePath = "";
                }
            }
        }

        private void encodeButton_Click(object sender, EventArgs e)
        {
            if (audioFilePath == "")
            {
                MessageBox.Show("��� ������ ����������� �������� ����!", "���� �� ������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ioRichTextBox.TextLength > maxLength)
            {
                MessageBox.Show(string.Format("������������ ������ ��� ���������� ����� {0} ��������.", maxLength, ioRichTextBox.TextLength), "�������� ������ ���������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ioRichTextBox.AppendText(string.Format("\n---\n{0} ��������", ioRichTextBox.TextLength));
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
                    MessageBox.Show("����������, ��� ���������� ��������������� ����� �������� ���������� ��� ����� � ����������� .wav!", "������������ ��� �����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    resultSaveFileDialog.FileName = "";
                    if (resultSaveFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
                var savePath = resultSaveFileDialog.FileName;
                resultSaveFileDialog.FileName = "";
                WavWriter.Write(savePath, audio.headerBytes, eDataBytes);
                logListBox.Items.Add(string.Format("��������� ����������� {0} ��������", ioRichTextBox.TextLength));
                MessageBox.Show(string.Format("� ���� \"{1}\" ������� ������������ ��������� �� {0} ��������.", ioRichTextBox.TextLength, Path.GetFileName(savePath)), "�����!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                savePath = "";
                audioFilePath = "";
                filenameLabel.Text = "����������, �������� ���� ��� ������ ������";
                ioRichTextBox.Text = "������� � ��� ���� �����, ������� ��������� ������������\n���\n�������� ���� � ������� ������ \"�������������\", ����� �������� ����������";
            }
            catch (Exception)
            {
                MessageBox.Show("� �������� ����������� �������� ����������� ������!", "����������� ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logListBox.Items.Add("�������� ����������� ������ �����������");
            }
        }

        private void decodeButton_Click(object sender, EventArgs e)
        {
            if (audioFilePath == "")
            {
                MessageBox.Show("��� ������ ������������� �������� ����!", "���� �� ������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var decodedMessage = Steganography.Decode(audio.dataBits, audio.bitsPerSample);
                ioRichTextBox.Text = decodedMessage;
                MessageBox.Show(string.Format("�� ����� \"{1}\" ������� ������������ ��������� �� {0} ��������.", ioRichTextBox.TextLength, Path.GetFileName(audioFilePath)), "�����!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logListBox.Items.Add(string.Format("��������� ������������� {0} ��������", ioRichTextBox.TextLength));
                audioFilePath = "";
                filenameLabel.Text = "����������, �������� ���� ��� ������ ������";
            }
            catch (Exception)
            {
                MessageBox.Show("� �������� ������������� �������� ����������� ������! ��������, � ���� ����� ��� �������� ���������.", "����������� ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logListBox.Items.Add("�������� ����������� ������ �������������");
            }
        }
    }
}