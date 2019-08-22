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


namespace operacionesConArchivos
{
    public partial class Form1 : Form
    {
        public string nombreArchivo;
        FileStream archivo;
        public Form1()
        {
            InitializeComponent();
        }

        private void labelArchEditandose_Click(object sender, EventArgs e)
        {

        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)//crea nuevo archivo 
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                nombreArchivo = saveDialog.FileName;
                archivo = new FileStream(nombreArchivo + ".BIN", FileMode.CreateNew,FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(archivo);
                long cab = -1;
                bw.Write("-1");
                archivo.Close();


              /*  archivo = new FileStream(nombreArchivo + ".BIN", FileMode.Open, FileAccess.Read); //abrimos el archivo para que este listo para editarlo                 
                labelArchEditandose.Text = "Abierto Archivo: " + nombreArchivo; //actualizmaos el label para saber que archivo esta abierto!
                archivo.Close();*/


            }             
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)//Boton Abrir 
        {
            OpenFileDialog d = new OpenFileDialog();

            d.Filter = "Archivos de binarios|*.BIN";
            if (d.ShowDialog() != DialogResult.OK) return;

            nombreArchivo = d.FileName; //recuperamos el nombre desde la ventana de dialogo

            archivo = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read); //abrimos el archivo 
            

            labelArchEditandose.Text = "Abierto Archivo: "+ d.FileName; //actualizmaos el label para saber que archivo esta abierto!

            archivo.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);
            long tamañoArch = archivo.Length;
            char[] leidos = br.ReadChars((int)tamañoArch);
            string sLeidos = new string(leidos);
            textBoxTest.Text = sLeidos;
            archivo.Close();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e) //Guardar
        {
            if(labelArchEditandose.Text == "Ningun Archivo Abierto")
            {        
                MessageBox.Show("No se ha abierto ningun archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                archivo = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write); //abrimos el archivo 
                BinaryWriter bw = new BinaryWriter(archivo);
                archivo.Seek(0, SeekOrigin.Begin);
                bw.Write(textBoxTest.Text);
                archivo.Close();
            }
        }

        private void cambiarNombreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (labelArchEditandose.Text == "Ningun Archivo Abierto")
            {
                MessageBox.Show("No se ha abierto ningun archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream nuevo = new FileStream(saveDialog.FileName + ".BIN", FileMode.CreateNew, FileAccess.ReadWrite);
                    archivo = new FileStream(nombreArchivo, FileMode.Open, FileAccess.ReadWrite);

                    archivo.CopyTo(nuevo);
                    archivo.Close();
                    nuevo.Close();
                    File.Delete(nombreArchivo);

                    nombreArchivo = saveDialog.FileName; //Actualizamos el nuevo nombre del archivo
                    labelArchEditandose.Text = "Abierto Archivo: " + nombreArchivo; //y el label
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (labelArchEditandose.Text == "Ningun Archivo Abierto")
            {
                MessageBox.Show("No se ha abierto ningun archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Esta seguro que desea eliminar el archivo", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(nombreArchivo);
                    labelArchEditandose.Text = "Ningun Archivo Abierto";
                    textBoxTest.Clear();
                }
            }             
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
