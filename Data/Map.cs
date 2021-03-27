﻿using System;

/* Map.cs  *************************************************************************************************
 **********     @Authors :                                             Date : 01 Avril 2020       **********
 **********                 * Josue Lubaki                                                        **********
 **********                 * Ismael Gansonre                                                     **********
 **********                 * Jordan Kuibia                                                       **********
 **********                 * Jonathan Kanyinda                                                   **********
 **********                 * Edgard Koffi                                                        **********
 ***********************************************************************************************************/
/*░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
 * Map.cs
 * ======
 *      Cette Classe permet de defini la Base sur laquelle on placera le Noeud et liaison formé.
 *      D'où (Noeud + Liaison + Map) = Labyrinthe
 *      
 *░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░*/
namespace TP1_INF1008.Data
{
    public partial class Map
    {
        /**
        * Compteur d'operation d'initialisation Map
        */
        private static int nbOperationMap = 0;

        /**
        * Données brutes de liaison.
        * - Première dimenssion :
        * Chaque noeud de gauche à droite puis de haut en bas
        * - Seconde dimension :
        * [0] -> valeur de la liaison vers le voisin de bas
        * [1] -> valeur de la liaison vers le voisin de droite
        * Valeur indéterminée si aucun
        */
        private int[,] map;

        /**
        * La longueur (horizontale) de la map.
        * Doit être supérieure à 0.
        */
        private int longueur;

        /**
        * La largeur (verticale) de la map.
        * Doit être supérieure à 0.
        */
        private int largeur;


        /**
         * Crée une instance de la classe {@link Map} avec pour
         * dimension x (horizontale) {@code longueur} et
         * y (verticale) {@code largeur}.
         *
         * @param longueur Longeur de la map.
         * @param largeur   Largeur de la map.
         * @throws IllegalArgumentException Si la longueur ou la largeur est inférieure ou égale à zéro.
         */
        public Map(int longueur, int largeur)
        {
            this.longueur = longueur;
            this.largeur = largeur;
            if (longueur <= 0 || largeur <= 0)
                throw new ArgumentException("La longueur et la largeur de la carte doient être supérieur à zéro.");

            map = new int[longueur * largeur, 2];
        }

        public int GetLongueur
        {
            get { return longueur; }
        }

        public int GetLargeur
        {
            get { return largeur; }
        }

        /**
        * Vérifie si la case de coordonées {@code x} et {@code y} est dans la carte.
        *
        * @param x Coordonnées x (horizontale) de la case à vérifier.
        * @param y Coordonnées y (verticale) de la case à vérifier.
        * @throws IllegalArgumentException Si au moins une des coordonées x ou y est invalide.
        */
        public void isValideXY(int x, int y)
        {
            if (x < 0)
                throw new ArgumentException($"La valeur x ne doit pas être inférieure à zéro ! x = {x}");
            else if (y < 0)
                throw new ArgumentException($"La valeur y ne doit pas être inférieure à zéro ! y = {y}");
            else if (x >= longueur)
                throw new ArgumentException($"La valeur x ne doit pas supérieur ou égale a la longueur ! x = {x}");
            else if (y >= largeur)
                throw new ArgumentException($"La valeur y ne doit pas supérieur ou égale a la largeur ! y  = {y}");
        }



        /**
     * Retourne vrai si la case de coordonées {@code x} et {@code y}
     * à un voisin de droite.
     *
     * @param x Coordonnées x (horizontale) de la case à vérifier.
     * @param y Coordonnées y (verticale) de la case à vérifier.
     * @return Vrai si la case à un voisin de droite.
     * @throws IllegalArgumentException Si au moins une des coordonées x ou y est invalide.
     */
        private bool aUnVoisinDeDroite(int x, int y)
        {
            isValideXY(x, y);
            return x < longueur - 1;
        }

        /**
        * Retourne vrai si la case de coordonées {@code x} et {@code y}
        * à un voisin du bas.
        *
        * @param x Coordonnées x (horizontale) de la case à vérifier.
        * @param y Coordonnées y (verticale) de la case à vérifier.
        * @return Vrai si la case à un voisin du bas.
        * @throws IllegalArgumentException Si au moins une des coordonées x ou y est invalide.
        */
        private bool aUnVoisinDuBas(int x, int y)
        {
            isValideXY(x, y);
            return y < largeur - 1;
        }


     /**
     * Met des valeurs aléatoires aux liaisons bornées entre {@code min} (inclue)
     * et {@code max}
     * @param min Minimum des Poids aléatoires
     * @param max Maximum des Poids aléatoires
     * @return Map
     */
        public Map PoidsAleatoires(int min, int max)
        {
            nbOperationMap = 0;
            // x et y sont des coordonées au niveau du tableau map
            int x, y;
            Random rand = new Random();
            for (x = 0; x < longueur * largeur; x++)
            {
                for (y = 0; y < 2; y++)
                {
                    map[x, y] = rand.Next(min, max) + 1;
                    nbOperationMap += 1;
                }
            }
            return this;
        }

        /**
         * Methode qui permet d'affecter les poids Manuellement (non aléatoire)
         * en deux directions (toRight = DROITE) et (!toRight = GAUCHE)
         */
        public void AffectationPoids(int x, int poids, bool toRight) {

            if (toRight)
                map[x, 1] = poids;
            else
                map[x, 0] = poids;

            nbOperationMap += 1;
        }

        // Getter et setter du nombre d'opération effectuée dans la Map (Initialisation)
        public int NbreOperation{
            get { return nbOperationMap; }
            set { nbOperationMap = value; }
        }

       
        /**
            * Cette fonction permet de d'obtenir la valeur de la liaison
            * {@code versLiaisonDroite} en fonction des coordonées d'une
            * case de coordonées {@code x} et {@code y}
            *
            * @param x                 Coordonnées x (horizontale) de la valeur à prendre.
            * @param y                 Coordonnées y (verticale) de la valeur à prendre.
            * @param versLiaisonDroite Vrai si c'est vers la liaison de droite,
            *                          faux si c'est vers celle du bas.
            * @return La valeur de la liaison.
            * @throws IllegalArgumentException Si au moins une des coordonées x ou y est invalide.
            * @throws IllegalArgumentException Si la case n'a pas de voisin de droite ou
            *                                  du bas en fonction de {@code versLiaisonDroite}.
        */
        public int GetPoidsLiaison(int positionX, int positionY, bool toRightDirection)
        {
            isValideXY(positionX, positionY);

            // Si on se dirige vers la droite et qu'il n'existe aucune case
            if (toRightDirection && !aUnVoisinDeDroite(positionX, positionY))
                throw new ArgumentOutOfRangeException("Cette case n'a pas de voisin de droite.");

            // Si nous sommes à la fin du map
            if (!toRightDirection && !aUnVoisinDuBas(positionX, positionY))
                throw new ArgumentOutOfRangeException("Cette case n'a pas de voisin du Bas.");

            return map[positionX + positionY * longueur, toRightDirection ? 1 : 0];
        }

        public override string ToString()
        {
            return $"La map fait une taille de {longueur} X {largeur}";
        }
    }
}
