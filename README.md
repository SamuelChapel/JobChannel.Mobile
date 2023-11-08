# JobChannel : Solutions pour l’Insertion Professionnelle

## Introduction
Le service d'insertion de l'association AMIO gère les offres d'emploi pour les stages, l'alternance et les emplois. 

Ces offres proviennent de diverses sources et sont ensuite transmises par e-mail aux stagiaires. 

Pour améliorer la mise à disposition des offres d'emploi et impliquer davantage les stagiaires dans leurs recherches, 
nous allons développer une solution avec une architecture en couches.

## Architecture Globale
JobChannel adopte une architecture en couches pour séparer les responsabilités et faciliter la maintenance. 

Voici les applications de cette solution :

1. **Partie Serveur**:
    - Base de données relationnelle : Utilisation de **SQL Server** comme SGBDR pour stocker les données des offres d'emploi.
    - **API REST** avec le **Framework ASP.NET 6**. Cette API servira d'intermédiaire entre la base de données et les applications clientes.

2. **Partie Client**:
    - **Application Desktop pour les Conseillers d'Insertion**:
        - Application **WinForms** pour permettre aux conseillers d'insertion de gérer les offres d'emploi mises à disposition des stagiaires.
    - **Application Mobile pour les Stagiaires**:
        - Pour les stagiaires, application **UWP** (Universal Windows Platform) avec une architecture **MVVM**. Cette application permettra aux stagiaires de consulter les offres d'emplois.

## Tests
Les test ont étés éffectués tout au long du développement en utilisant des **tests unitaires** et des **tests d'intégration** avec le framework **xUnit**.

## Exemple d'écran de l'application mobile

![image](https://github.com/SamuelChapel/JobChannel.Mobile/assets/86355019/a5bb0c2d-56e0-4730-9f3a-b646365750ce)
