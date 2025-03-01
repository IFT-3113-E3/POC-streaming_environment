# [POC] Streaming et Tile Mapping

## **Introduction**

Ce projet a pour objectif de réaliser des prototypes de streaming et de tile mapping en 3D pour tester leur faisabilité et leur efficacité dans le cadre du développement de jeux vidéo. Les prototypes visent à explorer différentes approches pour assurer une transition fluide entre les niveaux et créer des environnements de jeu modulaires et évolutifs.

## **Comment exécuter le projet ?**

Pour exécuter le projet, vous aurez besoin de Unity 2019.4.28f1 ou d'une version ultérieure. Voici les étapes à suivre :

1. Clonez le dépôt Git sur votre machine locale.
2. Ouvrez Unity Hub et ajoutez le projet en cliquant sur le bouton "Add" -> "Add project from disk" et en sélectionnant le dossier `Source/StreamingEnvironment` depuis la racine du projet.
3. Sélectionnez le projet dans Unity Hub et ouvrez-le dans Unity.
4. Une fois le projet ouvert, vous pouvez exécuter les prototypes en cliquant sur le bouton "Play" dans l'éditeur Unity.

## **Défis**

### **Défi Prototype – Streaming Environment**

#### **Objectif :**
L’objectif de ce prototype est d’implémenter un système de transition fluide entre différents niveaux sans temps de chargement apparent. Inspiré des anciennes routes dans Pokémon, le joueur traverse une zone de transition (un tunnel, un couloir, un passage) qui sert de tampon pour charger le prochain niveau en arrière-plan. Une fois cette transition terminée, le nouveau niveau est activé et la scène précédente est déchargée, garantissant une continuité immersive.

#### **Description :**
Le joueur évolue dans un environnement composé de plusieurs niveaux connectés par des zones de transition. Lorsqu’il atteint l’extrémité d’un niveau (ex. une sortie de route ou une porte), une scène intermédiaire se charge immédiatement pour masquer le chargement du prochain niveau principal. Le joueur mettra légèrement plus de temps à traverser cette transition que nécessaire, laissant au moteur le temps de charger les ressources du prochain environnement. Une fois cette phase terminée, la transition est désactivée et le joueur entre dans le nouveau niveau sans coupure visible.

#### **Éléments techniques à explorer :**
- **Gestion des scènes dans Unity :** Chargement asynchrone des scènes avec SceneManager.LoadSceneAsync().
- **Optimisation des ressources :** Chargement différé des assets 3D, textures et sons pour améliorer la fluidité.
- **Transition musicale et sonore :** Assurer une continuité sonore sans interruption pour renforcer l’immersion.

#### **Critères de réussite :**
Le prototype doit garantir une transition fluide entre les niveaux sans écran de chargement apparent ni interruption notable. Le passage par une zone de transition doit masquer le chargement en arrière-plan, tout en maintenant une continuité sonore et des performances stables pour préserver l’immersion du joueur.

### **Défi Prototype – Système de Tile Mapping en 3D**

#### **Objectif :**
Ce prototype vise à créer un système flexible de tile mapping en 3D permettant de concevoir des niveaux facilement. L’objectif est de tester la modularité du système et d’assurer une fluidité optimale, notamment lors des transitions entre différentes zones.

#### **Description :**
Le monde sera construit à partir d’un système de tiles en 3D disposés sur une grille. Chaque tile représentera une portion du décor (sol, mur, éléments interactifs) et pourra être agencé de manière à former des niveaux variés. Bien que le terrain ne soit pas modifiable par le joueur dans ce prototype, l’outil devra permettre aux développeurs de créer des environnements rapidement et efficacement.

#### **Éléments techniques à explorer :**
- **Structure de la grille** : Utilisation d’un système de coordonnées pour organiser les tiles en 3D (grille fixe ou octree si nécessaire).
- **Optimisation du rendu** : Réduction du nombre de draw calls en fusionnant les meshes adjacents ou en utilisant des chunks de tiles.
- **Streaming des tiles** : Chargement dynamique des portions du niveau selon la position du joueur, combiné avec la technique de transition étudiée dans le défi précédent.

#### **Critères de réussite :**
Le prototype doit permettre de concevoir des niveaux en assemblant des tiles 3D de manière intuitive, tout en maintenant un rendu fluide. La gestion du streaming et des performances sera essentielle pour éviter les chutes de framerate. Les transitions entre les zones de la grille devront être transparentes, sans coupure ni latence perceptible pour le joueur.
