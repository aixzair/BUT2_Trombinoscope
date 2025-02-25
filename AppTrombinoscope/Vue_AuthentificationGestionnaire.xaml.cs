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
    /// Logique d'interaction pour Vue_AuthentificationGestionnaire.xaml
    /// </summary>
    public partial class Vue_AuthentificationGestionnaire : Window
    {
        private MainWindow pere;

        public Vue_AuthentificationGestionnaire(MainWindow pere)
        {
            this.pere = pere;
            InitializeComponent();
        }

        private void onAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onConnexion(object sender, RoutedEventArgs e)
        {
            if (tb_utilisateur.Text == "admin" && pb_mdp.Password == "Password1234@")
            {
                pere.Gestionnaire = true;
                this.Close();
            }
            else
            {
                pb_mdp.Password = "";
            }
        }
    }
}
