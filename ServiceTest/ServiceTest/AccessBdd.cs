using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTest
{
    class AccessBdd
    {
        /// <summary>
        /// propriétés nécessaires à la classe
        /// </summary>
        private bool finCurseur = true; // fin du curseur atteinte
        private MySqlConnection connection; // chaine de connexion
        private MySqlCommand command; // commande d'envoi de la requête à la base de données
        private MySqlDataReader reader; // commande de gestion du curseur

        /// <summary>
        /// Constructeur
        /// </summary>
        public AccessBdd()
        {
            String chaineConnection = "server=localhost; database=gsb_frais; user id=userGsb; pwd=secret";
            this.connection = new MySqlConnection(chaineConnection); //Création de l'objet connection
        }


        /// <summary>
        /// Fonction qui permet l'exécution d'une requête SELECT
        /// </summary>
        /// <param name="chaineRequete">requête à éxécuter</param>
        public void reqSelect(string chaineRequete)
        {
            this.connection.Open(); //Ouverture de la connection
            this.command = new MySqlCommand(chaineRequete, this.connection); //création de l'objet command
            this.reader = this.command.ExecuteReader(); // appel du curseur
            this.finCurseur = false; //pour que le curseur tourne, et ne s'arrête pas tout de suite : éxécution de la requête
            this.suivant(); //Passage à la ligne suivante
            this.connection.Close();
        }



        /// <summary>
        /// Fonction qui permet l'exécution d'une requête UPDATE
        /// </summary>
        /// <param name="chaineRequete">requête à exécuter</param>        
        public void reqUpdate(string chaineRequete)
        {
            this.connection.Open(); //Ouverture de la connection
            this.command = new MySqlCommand(chaineRequete, this.connection); //création de l'objet command
            this.command.ExecuteNonQuery(); //exécution de la requête  
            this.finCurseur = true; //fermeture du curseur
            this.connection.Close();
        }


        /// <summary>
        /// Fonction qui permet de récupérer un champ
        /// </summary>
        /// <param name="nomChamp">nom du champ à récupérer</param>
        /// <returns>retourne le contenu du champ passé en paramètre</returns>
        public Object champ(string nomChamp)
        {
            return this.reader[nomChamp];
        }

        /// <summary>
        /// Fonction qui permet de passer à la ligne suivante du curseur
        /// </summary>
        public void suivant()
        {
            if (!this.finCurseur)
            {
                this.finCurseur = !this.reader.Read();
            }
        }

        /// <summary>
        /// Fonction qui test la fin du curseur
        /// </summary>
        /// <returns>vrai pour "fin du curseur"</returns>
        public Boolean fin()
        {
            return this.finCurseur;
        }

        /// <summary>
        /// Méthode qui permet de fermer la connexion
        /// </summary>
        public void close()
        {
            this.connection.Close();
        }
    }
}
