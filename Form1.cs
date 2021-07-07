using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EditorTexto
{
    public partial class Form1 : Form
    {
        StringReader reader = null;
        public Form1()
        {
            InitializeComponent();
        }

        // File (new, open, save, print Out)
        private void newFile()
        {
            richTextBox1.Clear();
            richTextBox1.Focus();
        }
        private void openFile()
        {
            // Não permitindo que seja selecionado + de 1 arquivo
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.Title = "Open File.";
            openFileDialog1.InitialDirectory = @"C:\Users\Administrador\Desktop";
            openFileDialog1.Filter = "(*.TXT)|*.TXT";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream file = new FileStream(
                        openFileDialog1.FileName,
                        FileMode.Open,
                        FileAccess.Read
                        );
                    StreamReader streamReader = new StreamReader(file);
                    streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    this.richTextBox1.Text = "";
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        this.richTextBox1.Text += line + "\n";
                        line = streamReader.ReadLine();
                    }

                    streamReader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        "Read error: " + e.Message,
                        "Read Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
            }
        }
        private void saveFile()
        {
            try
            {
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Criando o arquivo a ser salvo
                    FileStream file = new FileStream(
                        saveFileDialog1.FileName,
                        FileMode.OpenOrCreate,
                        FileAccess.Write
                        );
                    // Escritor que irá escrever no arquivo
                    StreamWriter streamWriter = new StreamWriter(file);
                    streamWriter.Flush();
                    streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                    streamWriter.Write(this.richTextBox1.Text);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Error saving file: " + e.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                throw;
            }
        }
        private void printOut()
        {
            printDialog1.Document = printDocument1;
            string text = this.richTextBox1.Text;
            reader = new StringReader(text);

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
        }

        // Copy / Paste
        private void textCopy()
        {
            // Se existe conteudo selecionado
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }
        private void textPaste()
        {
            richTextBox1.Paste();
        }

        // Font Styles
        private void textBold()
        {
            string fontName = null;
            bool bold, italic, underline = false;
            float fontSize = 0;

            fontName = richTextBox1.Font.Name;
            fontSize = richTextBox1.Font.Size;
            bold = richTextBox1.SelectionFont.Bold;
            italic = richTextBox1.SelectionFont.Italic;
            underline = richTextBox1.SelectionFont.Underline;

            richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Regular);
            
            if (!bold)
            {
                if (italic && underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
                else if (!italic && underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Bold | FontStyle.Underline);
                else if (italic && !underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Bold | FontStyle.Italic);
                else if (!italic && !underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Bold);
            }
            else
            {
                if (italic && underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Italic | FontStyle.Underline);
                else if (!italic && underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Underline);
                else if (italic && !underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Italic);
            }
        }
        private void textItalic()
        {
            string fontName = null;
            bool italic, bold, underline = false;
            float fontSize = 0;

            fontName = richTextBox1.Font.Name;
            fontSize = richTextBox1.Font.Size;

            bold = richTextBox1.SelectionFont.Bold;
            italic = richTextBox1.SelectionFont.Italic;
            underline = richTextBox1.SelectionFont.Underline;

            richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Regular);

            if (!italic)
            {
                if (bold && underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Italic | FontStyle.Bold | FontStyle.Underline);
                else if (!bold && underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Italic | FontStyle.Underline);
                else if (bold && !underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Italic | FontStyle.Bold);
                else if (!bold && !underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Italic);
            }
            else
            {
                if (bold && underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Bold | FontStyle.Underline);
                else if (!bold && underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Underline);
                else if (bold && !underline)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Bold);
            }
        }
        private void textUnderline()
        {
            string fontName = null;
            bool underline, bold, italic = false;
            float fontSize = 0;

            fontName = richTextBox1.Font.Name;
            fontSize = richTextBox1.Font.Size;
            bold = richTextBox1.SelectionFont.Bold;
            italic = richTextBox1.SelectionFont.Italic;
            underline = richTextBox1.SelectionFont.Underline;

            richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Regular);

            if (!underline)
            {
                if (bold && italic)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Underline | FontStyle.Bold | FontStyle.Italic);
                else if (!bold && italic)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Underline | FontStyle.Italic);
                else if (bold && !italic)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Underline | FontStyle.Bold);
                else if (!bold && !italic)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Underline);
            }
            else
            {
                if (bold && italic)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Bold | FontStyle.Italic);
                else if (!bold && italic)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Italic);
                else if (bold && !italic)
                    richTextBox1.SelectionFont = new Font(fontName, fontSize, FontStyle.Bold);
            }
        }

        // Alinhamentos
        private void leftAlignment()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void rightAlignment()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
        private void centerAlignment()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            rightAlignment();
        }

        private void btn_center_Click(object sender, EventArgs e)
        {
            centerAlignment();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newFile();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            newFile();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            textCopy();
        }

        private void btn_paste_Click(object sender, EventArgs e)
        {
            textPaste();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textCopy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textPaste();
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBold();
        }

        private void italizToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textItalic();
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textUnderline();
        }

        private void btn_bold_Click(object sender, EventArgs e)
        {
            textBold();
        }

        private void btn_italic_Click(object sender, EventArgs e)
        {
            textItalic();
        }

        private void btn_underline_Click(object sender, EventArgs e)
        {
            textUnderline();
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            leftAlignment();
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            centerAlignment();
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftAlignment();
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightAlignment();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linesPage = 0;
            float posY = 0;
            int count = 0;

            float leftMargin = e.MarginBounds.Left - 50;
            float topMargin = e.MarginBounds.Top - 50;
            if (leftMargin < 5)
                leftMargin = 20;
            if (topMargin < 5)
                topMargin = 20;

            string line = null;
            Font font = this.richTextBox1.Font;
            SolidBrush pencil = new SolidBrush(Color.Black);
            // calculo de linhas por página (usando margens como referencia)
            linesPage = e.MarginBounds.Height / font.GetHeight(e.Graphics);
            line = reader.ReadLine();

            while (count < linesPage)
            {
                posY = (topMargin + (count * font.GetHeight(e.Graphics)));
                e.Graphics.DrawString(line, font, pencil, leftMargin, posY, new StringFormat());
                count++;
                line = reader.ReadLine();
            }
            if (line != null)
            {
                // Imprimir em outra pagina
                e.HasMorePages = true;
            }
            else
                e.HasMorePages = false;
            pencil.Dispose();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
