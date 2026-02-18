using System.Xml.Linq;
using System.Xml;
namespace Editor
{
    public partial class frmEditor : Form
    {
        bool saved = false;
        string path = "";
        string texto = "";
        public frmEditor()
        {
            InitializeComponent();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cldEditor.ShowDialog() == DialogResult.OK)
            {
                rtbEditor.ForeColor = cldEditor.Color;
            }
        }

        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ftdEditor.ShowDialog() == DialogResult.OK)
            {
                rtbEditor.Font = ftdEditor.Font;
            }
        }

        private void rtbEditor_TextChanged(object sender, EventArgs e)
        {
            texto = rtbEditor.Text;
            string[] palabras = texto.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);


            tssStatus.Text = palabras.Length.ToString() + " Palabras";
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbEditor.Clear();
            
            path = "";//ruta
            saved = false;
        }
        private void guardar()
        {
            if (sfdEditor.ShowDialog() == DialogResult.OK)
            {
                path = sfdEditor.FileName;
                using (StreamWriter archivo = new StreamWriter(path)) // la ruta del archivo que voy a guardar
                { archivo.Write(rtbEditor.Text);
                    archivo.Write(rtbEditor.Font);
                    archivo.Write(rtbEditor.ForeColor);
                    archivo.Write(rtbEditor.BackColor);
                }// le voy a mandar al archuvo lo que tengo escrito en el rtb
                xml();

            }
        }
        private void xml()
        {
            XElement xml = new XElement("Caracteristicas");
            xml.Add(
            new XElement("Color", cldEditor.Color),
                    new XElement("Fuente", ftdEditor.Font));


            try
            {
                xml.Save("Archivo.xml");
                MessageBox.Show("Guardado", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }


        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved == false)
            {
                guardar();
                saved = true;
            }
            else
            {
                using (StreamWriter archivo = new StreamWriter(path)) // la ruta del archivo que voy a guardar
                    archivo.Write(rtbEditor.Text);
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofpEditor.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(ofpEditor.FileName))
                { // si la ruta seleccionada existe
                    rtbEditor.Text = File.ReadAllText(ofpEditor.FileName);
                    


                }
            }
        }

        private void tssStatus_Click(object sender, EventArgs e)
        {
            texto = rtbEditor.Text;
            string[] palabras = texto.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] parrafos = texto.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            MessageBox.Show("Estadisticas : \n\nPalabras: " + palabras.Length.ToString() + "\nLetras: " + texto.Length.ToString() + "\nParrafos: " + parrafos.Length.ToString(), "Contador de Palabras");

        }

        private void fondoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cldEditor.ShowDialog() == DialogResult.OK)
            {
                rtbEditor.BackColor= cldEditor.Color;
            }
        }
    }
}
