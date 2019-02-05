using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTest
{
    public abstract class GestionDates
    {
        /// <summary>
        /// Fonction qui permet d'optimser le code
        /// Elle fait une opération sur la date passée en paramètre
        /// et la transcrypte en chaine de caractères
        /// </summary>
        /// <param name="date">une date au format DateTime</param>
        /// <param name="opAFaire">valeur du calcul à faire</param>
        /// <returns>mois : le mois sous la forme de deux chiffres</returns>
        public static String optimisation(DateTime date, int opAFaire)
        {
            String mois = date.AddMonths(opAFaire).ToString("MM");
            return mois;
        }

        /// <summary>
        /// Méthode qui retourne le numéro du mois précedent
        /// </summary>
        /// <returns>Retourne le numéro du mois précédent de celui passé en paramètre
        /// dans la surcharge de la méthode appellée</returns>
        public static String getMoisPrecedent()
        {
            //Appel à la surcharge de la méthode getMoisPrecedent(), qui cette fois, attend une date en paramètre
            return getMoisPrecedent(DateTime.Today);
        }


        /// <summary>
        /// Surcharge de la méthode getMoisPrecedent()
        /// Méthode qui retourne le numéro du mois précedent sous la forme de deux chiffres
        /// </summary>
        /// <param name="date">date passée en paramètre sous la forme DateTime</param>
        /// <returns>mois : le mois sous la forme de 2 chiffres</returns>
        public static String getMoisPrecedent(DateTime date)
        {
            String mois = optimisation(DateTime.Today, -1); //Récupération du mois
            return mois;
        }

        /// <summary>
        /// Méthode qui retourne le num du mois suivant
        /// </summary>
        /// <returns>Retourne le numéro du mois précédent de celui passé en paramètre
        /// dans la surcharge de la méthode appellée</returns>
        public static String getMoisSuivant()
        {
            return getMoisSuivant(DateTime.Today);
        }

        /// <summary>
        /// Surcharge de la méthode getMoisSuivant()
        /// Méthode qui retourne le numéro du mois suivantsous la forme de deux chiffres
        /// </summary>
        /// <param name="date">date passée en paramètre sous la forme DateTime</param>
        /// <returns>mois : le mois sous la forme de deux chiffres</returns>
        public static String getMoisSuivant(DateTime date)
        {
            String mois = optimisation(DateTime.Today, 1);
            return mois;
        }

        /// <summary>
        /// Méthode qui permet de retourner l'année qui correspond au mois précedent 
        /// </summary>
        /// <param name="date">date passée en paramètre sous la forme DateTime</param>
        /// <returns>annee : l'année qui correspond au mois précédent celui passé en paramètre </returns>
        public static String getAnnee(DateTime date)
        {
            date = date.AddMonths(-1);
            String annee = date.ToString("yyyy");
            return annee;
        }

        /// <summary>
        /// Méthode qui vérifie si le jour passé en paramètre est entre deux jours précis
        /// </summary>
        /// <param name="jour1">première borne de l'interval à vérifier</param>
        /// <param name="jour2">deuxième borne de l'interval à vérifier</param>
        /// <returns>vrai si la date du jour est entre les deux bornes, sinon faux</returns>
        public static Boolean entre(int jour1, int jour2)
        {
            return entre(jour1, jour2, DateTime.Today);
        }

        /// <summary>
        /// Surcharge de la méthode entre() avec une date passée en plus en paramètre
        /// </summary>
        /// <param name="jour1">première borne de l'interval à vérifier</param>
        /// <param name="jour2">deuxième borne de l'interval à vérifier</param>
        /// <param name="date">date à vérifier, au format DateTime</param>
        /// <returns>vrai si la date du jour est entre les deux bornes, sinon faux</returns>
        public static Boolean entre(int jour1, int jour2, DateTime date)
        {
            int jour = date.Day; //Récupération du numéro d'aujourd'hui 
            if (jour >= jour1 && jour <= jour2)
            {
                return true;
            }
            return false;

        }

    }
}
