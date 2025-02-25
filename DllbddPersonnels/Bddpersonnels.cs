using BddpersonnelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllbddPersonnels
{
    public class Bddpersonnels
    {
        private BddpersonnelDataContext bdd;

        public Bddpersonnels(string utilisateur, string mdp, string ip, string port)
        {
            bdd = new BddpersonnelDataContext(
                "User Id=" + utilisateur
                + ";Password=" + mdp
                + ";Host=" + ip
                + ";Port=" + port
                + ";Database=bddpersonnels;Persist Security Info=True"
            );
        }

        public bool addPersonne(Personnel personne)
        {
            try
            {
                bdd.Personnels.InsertOnSubmit(personne);
                bdd.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public List<Personnel> getAllPersonnel()
        {
            return bdd.Personnels.ToList();
        }

        public List<Personnel> getPersonnelsWhithFonctionAndService(Fonction fonction, Service service)
        {
            if (fonction != null && service != null)
            {
                return bdd.Personnels.Where(personnel => personnel.Fonction.Id == fonction.Id).Where(personnel => personnel.Service.Id == service.Id).ToList();
            }
            else if (fonction == null && service == null)
            {
                return new List<Personnel>();
            }
            else if (fonction == null)
            {
                return bdd.Personnels.Where(personnel => personnel.Service.Id == service.Id).ToList();
            }
            else if (service == null)
            {
                return bdd.Personnels.Where(personnel => personnel.Fonction.Id == fonction.Id).ToList();
            }

            return new List<Personnel>();
        }

        public Personnel getPersonnelWhithNom(string nom)
        {
            return bdd.Personnels.Where(personnel => (personnel.Prenom + " " + personnel.Nom) == nom).ToList().Last();
        }

        public List<Personnel> getPersonnelContainsNom(string nom)
        {
            return bdd.Personnels.Where(personnel => personnel.Nom.Contains(nom)).ToList();
        }

        public List<Personnel> getPersonnelContainsPrenom(string prenom)
        {
            return bdd.Personnels.Where(personnel => personnel.Prenom.Contains(prenom)).ToList();
        }

        public List<Personnel> getPersonnelContainsNomAndPrenom(string nom, string prenom)
        {
            return bdd.Personnels
                .Where(personnel => personnel.Nom.Contains(nom))
                .Where(personnel => personnel.Prenom.Contains(prenom))
                .ToList();
        }


        public bool deletePersonnel(Personnel personnel)
        {
            try
            {
                bdd.Personnels.DeleteOnSubmit(personnel);
                bdd.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool addFonction(string intitule)
        {
            Fonction fonction = new Fonction
            {
                Intitule = intitule
            };

            try
            {
                bdd.Fonctions.InsertOnSubmit(fonction);
                bdd.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool updateFonction(Fonction fonction)
        {
            if (fonction == null)
            {
                return false;
            }

            try
            {
                Fonction fonctionCourant = getFonction(fonction.Id);
                fonctionCourant.Intitule = fonction.Intitule;

                bdd.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteFonction(Fonction fonction)
        {
            if (fonction == null)
            {
                return false;
            }

            try
            {
                bdd.Fonctions.DeleteOnSubmit(fonction);
                bdd.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }


        public List<Fonction> getAllFonction()
        {
            return bdd.Fonctions.ToList();
        }

        public Fonction getFonction(int id)
        {
            return bdd.Fonctions.Where(fonction => fonction.Id == id).ToList().Last();
        }

        public Fonction getFonctionWhithIntitule(string intitule)
        {
            foreach (Fonction fonction in getAllFonction())
            {
                if (fonction.Intitule == intitule)
                {
                    return fonction;
                }
            }

            return null;
        }

        public bool addSerivce(string intitule)
        {
            Service service = new Service
            {
                Intitule = intitule
            };

            try
            {
                bdd.Services.InsertOnSubmit(service);
                bdd.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public List<Service> getAllService()
        {
            return bdd.Services.ToList();
        }

        public bool updateService(Service service)
        {
            if (service == null)
            {
                return false;
            }

            try
            {
                Service serviceCourant = getService(service.Id);
                serviceCourant.Intitule = service.Intitule;

                bdd.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteService(Service service)
        {
            if (service == null)
            {
                return false;
            }

            try
            {
                bdd.Services.DeleteOnSubmit(service);
                bdd.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Service getServiceWhithIntitule(string intitule)
        {
            foreach (Service service in getAllService())
            {
                if (service.Intitule == intitule)
                {
                    return service;
                }
            }

            return null;
        }

        public Service getService(int id)
        {
             return bdd.Services.Where(service => service.Id == id).ToList().Last();
        }
    }
}
