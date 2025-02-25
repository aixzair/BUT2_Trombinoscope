using DllbddPersonnels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppTrombinoscope
{
    /// <summary>
    /// Logique d'interaction pour Vue_ParametresBDD.xaml
    /// </summary>
    public partial class Vue_ParametresBDD : Window
    {
        public Vue_ParametresBDD()
        {
            InitializeComponent();


            tb_ip.Text = Properties.Settings.Default.ip;
            tb_port.Text = Properties.Settings.Default.port;
            tb_utilisateur.Text = Properties.Settings.Default.utilisateur;
            tb_mdp.Password = Properties.Settings.Default.mdp;
        }

        private void onAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onEnregistrer(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ip = tb_ip.Text;
            Properties.Settings.Default.port = tb_port.Text;
            Properties.Settings.Default.utilisateur = tb_utilisateur.Text;
            Properties.Settings.Default.mdp = tb_mdp.Password;

            this.Close();
        }
    }
}
