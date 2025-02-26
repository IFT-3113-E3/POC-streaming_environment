# POC-streaming_environnment

L’objectif de ce prototype est d’implémenter un système de transition fluide entre différents niveaux sans temps de chargement apparent. Inspiré des anciennes routes dans Pokémon, le joueur traverse une zone de transition (un tunnel, un couloir, un passage) qui sert de tampon pour charger le prochain niveau en arrière-plan. Une fois cette transition terminée, le nouveau niveau est activé et la scène précédente est déchargée, garantissant une continuité immersive.

Le joueur évolue dans un environnement composé de plusieurs niveaux connectés par des zones de transition. Lorsqu’il atteint l’extrémité d’un niveau (ex. une sortie de route ou une porte), une scène intermédiaire se charge immédiatement pour masquer le chargement du prochain niveau principal. Le joueur mettra légèrement plus de temps à traverser cette transition que nécessaire, laissant au moteur le temps de charger les ressources du prochain environnement. Une fois cette phase terminée, la transition est désactivée et le joueur entre dans le nouveau niveau sans coupure visible.

Éléments techniques à explorer :
- **Gestion des scènes dans Unity :** Chargement asynchrone des scènes avec SceneManager.LoadSceneAsync().
- **Optimisation des ressources :** Chargement différé des assets 3D, textures et sons pour améliorer la fluidité.
- **Transition musicale et sonore :** Assurer une continuité sonore sans interruption pour renforcer l’immersion.

Le prototype doit garantir une transition fluide entre les niveaux sans écran de chargement apparent ni interruption notable. Le passage par une zone de transition doit masquer le chargement en arrière-plan, tout en maintenant une continuité sonore et des performances stables pour préserver l’immersion du joueur.
