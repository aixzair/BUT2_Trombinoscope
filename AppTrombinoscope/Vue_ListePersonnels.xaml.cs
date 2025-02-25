using BddpersonnelContext;
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
    /// Logique d'interaction pour Vue_ListePersonnels.xaml
    /// </summary>
    public partial class Vue_ListePersonnels : Window
    {
        private MainWindow pere;
        private ObservableCollection<Personnel> personnels;

        public Vue_ListePersonnels(MainWindow pere)
        {
            InitializeComponent();

            this.pere = pere;
            personnels = new ObservableCollection<Personnel>();

            foreach (Personnel personnel in pere.Bdd.getAllPersonnel())
            {
                personnels.Add(personnel);
            }

            datagrid_personnels.ItemsSource = personnels;
        }

        private void updatePersonnels(object sender, TextChangedEventArgs e)
        {
            string nom = tb_nom.Text;
            string prenom = tb_prenom.Text;
            List<Personnel> resultats = new List<Personnel>();

            if (nom == "" && prenom == "")
            {
                resultats = pere.Bdd.getAllPersonnel();
            }
            else if (nom == "")
            {
                resultats = pere.Bdd.getPersonnelContainsPrenom(prenom);
            }
            else if (prenom == "")
            {
                resultats = pere.Bdd.getPersonnelContainsNom(nom);
            }
            else
            {
                resultats = pere.Bdd.getPersonnelContainsNomAndPrenom(nom, prenom);
            }

            personnels.Clear();
            foreach (Personnel personnel in resultats)
            {
                personnels.Add(personnel);
            }
        }
    }
}
