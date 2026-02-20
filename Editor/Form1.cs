using Microsoft.VisualBasic.Logging;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
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
                    xml();
                }// le voy a mandar al archuvo lo que tengo escrito en el rtb
                

            }
        }
        private void xml()
        {
            // ftdEditor (FontDialog)
            // solo es una ventana para elegir

            //cldEditor(ColorDialog)
            //solo es una ventana para seleccionar color

           // rtbEditor
           // es el que realmente tiene aplicados los cambio
            XElement xml = new XElement("Caracteristicas");
            xml.Add(
            new XElement("Fuente",
                new XAttribute("Tipo", rtbEditor.Font.Name),// lo separo asi si quiero tener las caracteristicas por separado
                new XAttribute("Tamaño", rtbEditor.Font.Size),
                new XAttribute("Estilo", rtbEditor.Font.Style.ToString())),

            new XElement("Color",
                    new XAttribute("Fondo", rtbEditor.BackColor.ToString()),
                    new XAttribute("Fuente", rtbEditor.ForeColor.ToString())));


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
        public void cargarXML() {
            if (File.Exists("Archivo.xml")) { // comprobar si es xml
                XElement xml = XElement.Load("Archivo.xml"); //lo convierto en objeto para poder cceder a sus nodos
                String nombre = xml.Element("Fuente").Attribute("nombre").Value;
                int tamaño = int.Parse(xml.Element("Fuente").Attribute("tamaño").Value);
                String estilo = xml.Element("Fuente").Attribute("estilo").Value;
                int colorTexto = int.Parse(xml.Element("Color").Attribute("Fuente").Value);
                int colorFondo = int.Parse(xml.Element("Color").Attribute("Fondo").Value);

                rtbEditor.Font = new Font(nombre, tamaño, estilo);
                rtbEditor.ForeColor = Color.FromArgb(colorTexto);
                rtbEditor.BackColor=Color.FromArgb(colorFondo);
            }
        }
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofpEditor.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(ofpEditor.FileName))
                { // si la ruta seleccionada existe
                    rtbEditor.Text = File.ReadAllText(ofpEditor.FileName);
                    cargarXML();
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
