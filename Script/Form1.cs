using Script.Componentes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Script
{
    public partial class Main : Form
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private bool OrbwalkActivado = false;

        public Main()
        {
            InitializeComponent();
        }

        public void InicializarKeyboardHook()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += handlePulsarTecla;
        }

        private void handlePulsarTecla(object sender, GlobalKeyboardHookEventArgs e)
        {
            if(e.KeyboardData.VirtualCode == GlobalKeyboardHook.VkF1 && e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                OrbwalkActivado = !OrbwalkActivado;

                switch (OrbwalkActivado)
                {
                    case true:
                        labelInformativo.Text = "Orbwalk iniciado, pulsa F1 para iniciar o detener. Manten Espacio para ejecutar.";
                        labelInformativo.ForeColor = Color.Green;
                        break;
                    case false:
                        labelInformativo.Text = "Orbwalk no iniciado, pulsa F1 para iniciar o detener.";
                        labelInformativo.ForeColor = Color.Red;
                        break;
                }

                e.Handled = true;
            }

            if (OrbwalkActivado)
            {

            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            InicializarKeyboardHook();
        }
    }
}
