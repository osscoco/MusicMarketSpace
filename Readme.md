Music Market Space (Angular / C# / Docker)
========================

L'application web **Music Market Space** sert de mise en relation entre professionnels et artistes domaine le domaine musical.

Docker
------------

- <u>Pré requis</u> :
    - Téléchargement : https://www.docker.com/ (Download For Windows - AMD64)
    - Installation ...
    - Vérification de la présence de docker et de docker compose :

```bash
$ docker --version
```

```bash
$ docker-compose --version
```

- <u>Préparation du lancement du docker-compose</u> :
    - Placement dans le dossier "./docker/" du fichier "docker-compose.yml" 
    - Créer un fichier ".env" avec les informations suivantes (par défaut) :
```
MYSQL_ROOT_PASSWORD=ToUpDaTePaSsWoRd
MYSQL_DATABASE=ToUpDaTeDaTaBaSe
MYSQL_USER=ToUpDaTeUsEr
MYSQL_PASSWORD=ToUpDaTePaSsWoRd
``` 
  - <u>Lancement du docker-compose </u> : 

```bash
$ docker-compose up
```

- <u>Ouverture de PhpMyAdmin</u> : 
```
Url: http://localhost:8080/
User: root
Password: ToUpDaTePaSsWoRd
```
- <u>Accès à la base de données</u> : 
```
Nom de la base de données: ToUpDaTeDaTaBaSe
```
*Créer la base de données à la main si elle n'existe pas*


Git
------------

- <u>Récupération du projet (remote vers local)</u> :
    - Placement dans le dossier du fichier (./)
    - Execution (via le terminal de commande) de la commande suivante : 

```bash
$ git clone https://github.com/osscoco/MusicMarketSpace.git
```

- <u>Commandes à connaitre</u> :
    - Configuration :
    ```
        git init
        git config --global user.name "first-name last-name"
        git config user.name
        git config --global user.email "email"
        git config user.email
        git remote add origin "remote-repository-link"
    ```
    - Récupération projet distant → repository local :
    ``` 
        git clone "remote-repository-link"
        git pull → git fetch + git merge
        git pull "origin" "master"
    ```
    - Gestion des branches dans le repository local :
    ```
        git checkout "branch-name" "upper-branch"
        git checkout -b "branch-name" "upper-branch"
    ```
    - Envoi du repository local → projet distant :
    ```
        git status
        git add "file-name"
        git add .
        git commit -m "message"
        git push -u origin "branch-name"
    ```

Markdown Preview Editor
------------

- Ouvrir le preview : ctrl-k + v (sous visual studio code avec l'extension activée)


Envs (dotnet)
------------

- Ajout de la connectionString :
    - Ouverture du fichier "appsettings.json" dans le dossier (./back/Api/EFCore/)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;User id=root;Password=ToUpDaTePaSsWoRd;Database=ToUpDaTeDaTaBaSe;Persistsecurityinfo=True"
  }
}
```

- Ajout de la connectionString :
    - Ouverture du fichier "appsettings.json" dans le dossier (./back/Api/)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;User id=root;Password=ToUpDaTePaSsWoRd;Database=ToUpDaTeDaTaBaSe;Persistsecurityinfo=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "...",
    "Issuer": "http://localhost:5123",
    "Audience": "http://localhost:5123",
    "Subject": "JwtSubject"
  },
  "Smtp": {
    "Host": "smtp.office365.com",
    "Port": 587,
    "Username": "...",
    "Password": "..."
  }
}
```

Migrations (dotnet)
------------

- Lancement des migrations vers la base de données (PhpMyAdmin) :
    - Placement dans le dossier (./back/Api/EFCore/)
    - Execution (via le terminal de commande) de la commande suivante : 

- Si jamais dotnet-ef n'est pas installé, lancer la commande :

```bash
$ dotnet tool install --global dotnet-ef
```

- Si jamais le fichier "InitCreateTables" n'existe pas dans les migrations, lancer la commande :

```bash
$ dotnet ef migrations add InitCreateTables
```

- Envoyer les migrations vers la base de données

```bash
$ dotnet ef database update
```

Api Web (dotnet)
------------

- Lancement de l'API WEB :
    - Placement dans le dossier (./back/Api/)
    - Execution (via le terminal de commande) de la commande suivante : 

```bash
$ dotnet run
```

Swagger (dotnet)
------------

- Utilisation de l'interface SWAGGER : 
    - Url : http://localhost:8080/swagger/index.html
------------