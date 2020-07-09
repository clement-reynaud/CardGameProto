GUIDE A LA CREATION DE CARTE:
1) Ajouter le Scriptble Object de la carte (et remplir ses données)
2) Copier son nom et le coller tel que dans un des "cases" de la fonction "ReadCard()" de "CardReader.cs"
2.EX) case "Nom de la carte":

	  break;

3) Rajouter l'effet de la Carte
3.EX) case "Nom de la carte":
	  //Effet de la carte
	  break;
NB) Les fonctions DamageEnemy(cible,nb,element,cout), DamageEnemy(cible,nb,cout) et UseCard(cout) sont trés utile pour 
    la création de nouvelle carte



GUIDE A LA CREATION D'ENNEMI:
1) Ajouter le Scriptble Object de l'ennemi (et remplir ses données)

2) Copier son nom et le coller tel que dans un des "cases" de la fonction "EnemyAction()" de "CardReader.cs"
2.EX) case "Nom de l'Ennemi":

	  break;

3) Remplir ce case avec un créateur d'aléatoire
3.EX) case "Nom de l'Ennemi":
	selector = Data.rng.Next(0,max) + 1;
	break;

4) Faire un if statements dépendant de ce selector pour créer les sorts
4.EX) case "Nom de l'Ennemi":
	selector = Data.rng.Next(0,max) + 1;
	if(selector <= 50)
	{

	}
	else
	{

	}
	break;

5) Remplir les effets de chaque sorts.
5.EX) case "Nom de l'Ennemi":
	selector = Data.rng.Next(0,max) + 1;
	if(selector <= 50)
	{
		ActionName = "Nom du Sort 1"; //OBLIGATOIRE POUR LE LOG
		ActionDescription = "Description du Sort 1"; //Se remplit tout seul si soin ou dmg simple
		ActionDescriptionBuffer = "Suite de la desc"; //Si la description est automatique (voir au dessus) mais qu'il faut rajouter qqc
		//Effet du sort
	}
	else
	{
		ActionName = "Nom du Sort 2"; //OBLIGATOIRE POUR LE LOG
		//Effet du sort
	}
	break;

