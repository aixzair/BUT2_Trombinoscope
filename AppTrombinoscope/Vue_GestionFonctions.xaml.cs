using BddpersonnelContext;
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
    /// Logique d'interaction pour Vue_GestionFonctions.xaml
    /// </summary>
    public partial class Vue_GestionFonctions : Window
    {
        private MainWindow pere;

        public Vue_GestionFonctions(MainWindow pere)
        {
            this.pere = pere;

            InitializeComponent();
            updateFonctions();
        }

        private void updateFonctions()
        {
            lb_fonctions.Items.Clear();

            foreach (Fonction fonction in pere.Bdd.getAllFonction())
            {
                lb_fonctions.Items.Add(fonction.Intitule);
            }
            pere.refresh();
        }

        private void onAjouter(object sender, RoutedEventArgs e)
        {
            if (tb_fonction.Text != "" && pere.Bdd.addFonction(tb_fonction.Text))
            {
                tb_fonction.Text = "";
                updateFonctions();
            }
        }

        private void onAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onModifier(object sender, RoutedEventArgs e)
        {
            if (tb_fonction.Text.Equals(""))
            {
                lb_etat.Content = "Vous devez inscire le nouveau nom du service";
                return;
            }
            else if (lb_fonctions.SelectedItem == null)
            {
                lb_etat.Content = "Vous devez sélectionnr un élément";
            }

            Fonction fonction = pere.Bdd.getFonctionWhithIntitule(lb_fonctions.SelectedItem.ToString());
            fonction.Intitule = tb_fonction.Text;

            if (pere.Bdd.updateFonction(fonction))
            {
                lb_etat.Content = "Modification réussi !";
                tb_fonction.Text = "";
                updateFonctions();
            }
            else
            {
                lb_etat.Content = "Un problème est survenue !";
            }

        }

        private void onSupprimer(object sender, RoutedEventArgs e)
        {
            if (lb_fonctions.SelectedItem == null)
            {
                return;
            }

            Fonction fonction = pere.Bdd.getFonctionWhithIntitule(lb_fonctions.SelectedItem.ToString());

            if (fonction == null)
            {
                return;
            }

            if (pere.Bdd.deleteFonction(fonction))
            {
                lb_etat.Content = "Supréssion réussi !";
                updateFonctions();
            }
            else
            {
                lb_etat.Content = "Vous ne pouvez pas supprimer un service affecté !";
                pere.onConnexionBdd(null, null);
            }
        }
    }
}
