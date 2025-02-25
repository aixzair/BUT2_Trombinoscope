using BddpersonnelContext;
using DllbddPersonnels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppTrombinoscope
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bddpersonnels bdd;
        private bool gestionnaire;
        private bool aAfficherMembre;

        public Bddpersonnels Bdd {
            get { return bdd; }
            set { bdd = value; }
        }

        public bool Gestionnaire {
            get
            {
                return gestionnaire;
            }
            set
            {
                gestionnaire = value;
                updateGestionnaire();
            }
        }

        public MainWindow()
        {
            this.bdd = null;
            this.gestionnaire = false;
            this.aAfficherMembre = false;
            InitializeComponent();
            updateGestionnaire();
        }

        public void onConnexionBdd(object sender, RoutedEventArgs e)
        {
            bdd = new Bddpersonnels(
                Properties.Settings.Default.utilisateur,
                Properties.Settings.Default.mdp,
                Properties.Settings.Default.ip,
                Properties.Settings.Default.port
            );

            refresh();
            lb_statut.Content = "";
        }

        public void refresh(object sender = null, RoutedEventArgs e = null)
        {
            liste_fonction.Items.Clear();
            liste_service.Items.Clear();
            liste_membre.Items.Clear();

            foreach (Fonction fonction in bdd.getAllFonction())
            {
                liste_fonction.Items.Add(fonction.Intitule);
            }

            foreach (Service service in bdd.getAllService())
            {
                liste_service.Items.Add(service.Intitule);
            }
        }

        private void updateGestionnaire()
        {
            Visibility visibility = (gestionnaire)
                ? Visibility.Visible
                : Visibility.Hidden;

            bt_gestionServices.Visibility = visibility; 
            bt_gestionFonctions.Visibility = visibility;
            bt_gestionPersonnels.Visibility = visibility;
        }

        private void onParametreBdd(object sender, RoutedEventArgs e)
        {
            (new Vue_ParametresBDD()).Show();
        }

        private void onAuthentificationGestionnaire(object sender, RoutedEventArgs e)
        {
            if (bdd == null)
            {
                lb_statut.Content = "Veuillez vous connecter !";
                return;
            }

            lb_statut.Content = "";

            (new Vue_AuthentificationGestionnaire(this)).Show();
        }

        private void onGestionFonctions(object sender, RoutedEventArgs e)
        {
            if (bdd == null)
            {
                lb_statut.Content = "Veuillez vous connecter !";
                return;
            }

            lb_statut.Content = "";

            (new Vue_GestionFonctions(this)).Show();
        }

        private void onGestionServices(object sender, RoutedEventArgs e)
        {
            if (bdd == null)
            {
                lb_statut.Content = "Veuillez vous connecter !";
                return;
            }

            lb_statut.Content = "";

            (new Vue_gestionServices(this)).Show();
        }

        private void onGestionPersonnels(object sender, RoutedEventArgs e)
        {
            if (bdd == null)
            {
                lb_statut.Content = "Veuillez vous connecter !";
                return;
            }

            lb_statut.Content = "";

            (new Vue_GestionPersonnels(this)).Show();
        }

        private void onListePersonnels(object sender, RoutedEventArgs e)
        {
            if (bdd == null)
            {
                lb_statut.Content = "Veuillez vous connecter !";
                return;
            }

            lb_statut.Content = "";

            (new Vue_ListePersonnels(this)).Show();
        }

        private void onTrouverMembres(object sender, SelectionChangedEventArgs e)
        {
            if (bdd == null)
            {
                return;
            }

            Fonction fonction = null;
            Service service = null;
            List<Personnel> personnels;

            if (liste_fonction.SelectedItem != null)
            {
                fonction = Bdd.getFonctionWhithIntitule(liste_fonction.SelectedItem.ToString());
            }
            if (liste_service.SelectedItem != null)
            {
                service = Bdd.getServiceWhithIntitule(liste_service.SelectedItem.ToString());
            }

            liste_membre.Items.Clear();
            foreach (Personnel personne in Bdd.getPersonnelsWhithFonctionAndService(fonction, service))
            {
                liste_membre.Items.Add(personne.Prenom + " " + personne.Nom);
            }
        }

        private void afficherMembre(object sender, SelectionChangedEventArgs e)
        {
            if (bdd == null)
            {
                return;
            }

            Personnel personne = null;
            this.aAfficherMembre = true;

            if (liste_membre.SelectedItem != null)
            {
                personne = Bdd.getPersonnelWhithNom(liste_membre.SelectedItem.ToString());
            }
            
            if (personne == null)
            {
                lb_nom.Text = "";
                lb_prenom.Content = "";
                lb_service.Content = "";
                lb_fonction.Content = "";
                lb_telephone.Content = "";
                op_photo.Source = new BitmapImage();

                return;
            }

            lb_nom.Text = personne.Nom;
            lb_prenom.Content = personne.Prenom;
            lb_service.Content = personne.Service.Intitule;
            lb_fonction.Content = personne.Fonction.Intitule;
            lb_telephone.Content = personne.Telephone;

            BitmapImage image = new BitmapImage();

            if (personne.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(personne.Photo))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }

                op_photo.Source = image as ImageSource;
            }
            else
            {
                op_photo.Source = new BitmapImage();

            }
        }

        private void onRechercherNom(object sender, TextChangedEventArgs e)
        {
            if (bdd == null)
            {
                return;
            }

            if (aAfficherMembre)
            {
                aAfficherMembre = false;
                return;
            }

            liste_membre.Items.Clear();
            foreach (Personnel personne in Bdd.getPersonnelContainsNom(lb_nom.Text))
            {
                liste_membre.Items.Add(personne.Prenom + " " + personne.Nom);
            }
        }
    }
}
