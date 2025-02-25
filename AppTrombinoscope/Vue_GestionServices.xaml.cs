using BddpersonnelContext;
using DllbddPersonnels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour Vue_gestionServices.xaml
    /// </summary>
    public partial class Vue_gestionServices : Window
    {
        private MainWindow pere;

        public Vue_gestionServices(MainWindow pere)
        {
            this.pere = pere;

            InitializeComponent();
            updateServices();
        }

        private void updateServices()
        {
            lb_services.Items.Clear();

            foreach (Service service in pere.Bdd.getAllService())
            {
                lb_services.Items.Add(service);
            }

            pere.refresh();
        }

        private void onAjouter(object sender, RoutedEventArgs e)
        {
            if (tb_service.Text != "" && pere.Bdd.addSerivce(tb_service.Text))
            {
                tb_service.Text = "";
                tb_etat.Content = "Ajout effectué";
                updateServices();
            }
        }

        private void onAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onModifier(object sender, RoutedEventArgs e)
        {
            if (tb_service.Text.Equals(""))
            {
                tb_etat.Content = "Vous devez inscire le nouveau nom du service";
                return;
            }
            else if (lb_services.SelectedItem == null)
            {
                tb_etat.Content = "Vous devez sélectionnr un élément";
            }

            Service service = pere.Bdd.getServiceWhithIntitule(lb_services.SelectedItem.ToString());
            service.Intitule = tb_service.Text;

            if (pere.Bdd.updateService(service))
            {
                tb_etat.Content = "Modification réussi !";
                tb_service.Text = "";
                updateServices();
            }
            else
            {
                tb_etat.Content = "Un problème est survenue !";
            }
            
        }

        private void onSupprimer(object sender, RoutedEventArgs e)
        {
            if (lb_services.SelectedItem == null)
            {
                return;
            }

            Service service = pere.Bdd.getServiceWhithIntitule(lb_services.SelectedItem.ToString());

            if (service == null)
            {
                return;
            }

            if (pere.Bdd.deleteService(service))
            {
                tb_etat.Content = "Supréssion réussi !";
                updateServices();
            }
            else
            {
                tb_etat.Content = "Vous ne pouvez pas supprimer un service affecté !";
                pere.onConnexionBdd(null, null);
            }
        }
    }
}
