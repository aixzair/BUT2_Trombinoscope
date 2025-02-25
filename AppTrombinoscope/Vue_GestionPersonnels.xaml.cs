using BddpersonnelContext;
using DllbddPersonnels;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace AppTrombinoscope
{
    /// <summary>
    /// Logique d'interaction pour Vue_GestionPersonnels.xaml
    /// </summary>
    public partial class Vue_GestionPersonnels : Window
    {
        private class Show
        {
            public int Id { get; set; }
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public byte[] Photo { get; set; }
            public string Service { get; set; }
            public string Fonction { get; set; }
            public string Telephone { get; set; }

        }


        private MainWindow pere;
        private byte[] photo;

        public Vue_GestionPersonnels(MainWindow pere)
        {
            this.pere = pere;
            this.photo = new byte[0];

            InitializeComponent();

            foreach (Service service in pere.Bdd.getAllService())
            {
                liste_services.Items.Add(service.Intitule);
            }

            foreach (Fonction fonction in pere.Bdd.getAllFonction())
            {
                liste_fonctions.Items.Add(fonction.Intitule);
            }
        }

        private void onAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onEnregistrer(object sender, RoutedEventArgs e)
        {
            string nom = tb_nom.Text;
            string prenom = tb_prenom.Text;
            string tel = tb_tel.Text;

            try
            {
                Int32.Parse(tb_tel.Text);
            }
            catch
            {
                return;
            }

            Fonction fonction = pere.Bdd.getFonctionWhithIntitule(liste_fonctions.SelectedItem.ToString());
            Service service = pere.Bdd.getServiceWhithIntitule(liste_services.SelectedItem.ToString());

            if (nom == "" || prenom == "" || fonction == null || service == null)
            {
                return;
            }

            Personnel personne = new Personnel
            {
                Nom = nom,
                Prenom = prenom,
                Telephone = tel,
                IdFonction = fonction.Id,
                IdService = service.Id,
                Photo = photo
            };

            if (!pere.Bdd.addPersonne(personne))
            {
                return;
            }

            pere.refresh();

            photo = null;
            image_profile.Source = null;
            tb_nom.Text = "";
            tb_prenom.Text = "";
            tb_tel.Text = "";
            liste_fonctions.SelectedItem = null;
            liste_services.SelectedItem = null;
        }

        private void onChoisirPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialogue = new OpenFileDialog
            {
                FilterIndex = 1,
                Title = "Choisissez une photo",
                Filter = "All supported graphics| *.jpg;*.jpeg;*.png|"
                    + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
                    + "Portable Network (*.png)|*.png"
            };

            if (dialogue.ShowDialog() == true)
            {
                ImageSource image = new BitmapImage(new Uri(dialogue.FileName));
                BitmapImage image_bm = image as BitmapImage;
                image_profile.Source = image;

                using (MemoryStream ms = new MemoryStream()) {
                    BitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image_bm));
                    encoder.Save(ms);

                    photo = ms.ToArray();
                }
            }
        }
    }
}
