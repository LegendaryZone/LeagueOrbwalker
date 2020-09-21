using Script.Componentes;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace Script
{
    public partial class Main : Form
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private bool OrbwalkActivado = false;
        private TesseractEngine tEngine;
        private string velocidadDeAtaqueDetectada = String.Empty;

        public Main()
        {
            InitializeComponent();
        }

        public void InicializarKeyboardHook()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += handlePulsarTecla;
        }

        private async void handlePulsarTecla(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.VirtualCode == Convert.ToInt32(labelTeclaIniciarDetener.Text) && e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                OrbwalkActivado = !OrbwalkActivado;
                switch (OrbwalkActivado)
                {
                    case true:
                        labelInformativo.Text = "Orbwalk iniciado.";
                        labelInformativo.ForeColor = Color.Green;
                        break;
                    case false:
                        labelInformativo.Text = "Orbwalk no iniciado.";
                        labelInformativo.ForeColor = Color.Red;
                        break;
                }
                e.Handled = true;
            }

            if (OrbwalkActivado)
            {
                if (e.KeyboardData.VirtualCode == Convert.ToInt32(labelTeclaEjecutarOrbwalk.Text) && e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
                {
                    await ProcesarOrdenOrbwalk();
                    e.Handled = true;
                }
            }
        }

        private Task ProcesarOrdenOrbwalk()
        {
            //Thread.Sleep(5000);

            //TODO

            return Task.CompletedTask;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            InicializarKeyboardHook();
        }

        private void tomarImagen(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(Convert.ToInt32(rectX.Text), Convert.ToInt32(rectY.Text), Convert.ToInt32(rectW.Text), Convert.ToInt32(rectH.Text));
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            Bitmap resized = new Bitmap(bmp, new Size(Convert.ToInt32(resizeW.Text), Convert.ToInt32(resizeH.Text)));
            

            using (tEngine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var img = PixConverter.ToPix(resized))
                {
                    using(var page = tEngine.Process(resized))
                    {
                        textBox1.Text = page.GetText() + " s";

                       

                        if(textBox1.Text.Contains("I"))
                        {
                            textBox1.Text = textBox1.Text.Replace("I", "1");
                        }

                        if (textBox1.Text.Contains("L"))
                        {
                            textBox1.Text = textBox1.Text.Replace("L", "1.");
                        }

                        if (textBox1.Text.Contains(" "))
                        {
                            textBox1.Text = textBox1.Text.Replace(" ", "");
                        }

                        if (textBox1.Text.Equals(" s"))
                        {
                            textBox1.Text = "Error";
                        }

                        pictureBox1.Image = resized;
                    }
                }
            }
        }
    }
}
