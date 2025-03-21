Music Market Space (Angular / C# / Docker)
========================

L'application web **Music Market Space** sert de mise en relation entre professionnels et artistes dans le domaine musical.

Docker
------------

- <u>Pré requis</u> :
    - Téléchargez Docker via cette url : https://www.docker.com/ (Download For Windows - AMD64)
    - Lancez l'installation ...
    - Vérifiez la présence de docker et de docker compose via ces commandes :

```bash
$ docker --version
```

```bash
$ docker-compose --version
```

- <u>Préparation du lancement du docker-compose</u> :
    - Placez-vous dans le dossier <strong>"./docker/"</strong> 
    - Créez un fichier <strong>".env"</strong> avec les informations suivantes (par défaut) :
```
MYSQL_ROOT_PASSWORD=ToUpDaTePaSsWoRd
MYSQL_DATABASE=ToUpDaTeDaTaBaSe
MYSQL_USER=ToUpDaTeUsEr
MYSQL_PASSWORD=ToUpDaTePaSsWoRd
``` 
  - <u>Lancement du docker-compose </u> : 
    - Une fois positionné dans le dossier <strong>"./docker/"</strong>, vous pouvez lancer le docker compose via cette commande :
```bash
$ docker-compose up
```

- <u>Ouverture de PhpMyAdmin</u> : 
  - Allez sur votre navigateur sur l'url ci-dessous en vous connectant via les informations <strong>"User"</strong> et <strong>"Password"</strong> ci-dessous :
```
Url: http://localhost:8080/
User: root
Password: ToUpDaTePaSsWoRd
```
- <u>Accès à la base de données</u> : 
  - Accédez à la base de données nommée ci-dessous. Si vous ne la voyez pas, créez-là à la main :
```
Nom de la base de données: ToUpDaTeDaTaBaSe
```

Git
------------

- <u>Récupération du projet (distant vers local)</u> :
    - Placez-vous dans un dossier de votre choix sur votre ordinateur
    - Clonez le repository distant via cette commande : 

```bash
$ git clone https://github.com/osscoco/MusicMarketSpace.git
```

- <u>Commandes utiles</u> :

Initialisez un repository local git :
```bash
$ git init
```
Ajoutez votre prénom et nom pour signer le repository local git :
```bash
$ git config --global user.name "votre prénom et votre nom"
```
Vérifiez que la commande précédente a fonctionné :
```bash
$ git config user.name
```
Ajoutez votre email à la signature du repository local git :
```bash
$ git config --global user.email "votre email"
```
Vérifiez que la commande précédente a fonctionné :
```bash
$ git config user.email
```
Liez votre repository local git avec ce repository distant :
```bash
$ git remote add origin "https://github.com/osscoco/MusicMarketSpace.git"
```
Récupérez le contenu du repository distant vers votre repository local git :
```bash
$ git clone "https://github.com/osscoco/MusicMarketSpace.git"
```

<strong>*Notez que si vous avez l'autorisation d'ajouter du contenu au repository distant, voici les commandes utiles :*</strong>

Si vous n'êtes pas sur la branche <strong>"develop"</strong>, positionnez-vous dessus :
```bash
$ git checkout develop
```
Créez une branche à l'intérieur de la branche <strong>"develop"</strong> :
```bash
$ git checkout -b features/nom-de-votre-fonctionnalité develop
```
Si vous avez besoin de renommer votre branche :
```bash
$ git branch -m features/nom-de-votre-fonctionnalité features/nouveau-nom-de-votre-fonctionnalité
```
Après avoir fait vos modifications de code, vérifiez tous les fichiers détectés qui seront envoyés vers le repository distant au prochain commit que vous allez effectuer :
```bash
$ git status
```
Sélectionnez tous les fichiers qui sont dans la liste affichée par la commande précédente :
```bash
$ git add .
```
Commitez cet envoi avec un message clair :
```bash
$ git commit -m "votre message"
```
Envoyez ce commit vers la zone des <strong>"PR"</strong> (pull request) :
```bash
$ git push -u origin features/nom-de-votre-fonctionnalité
```
*Gérez la suite sur le dashboard du projet sur github avec un membre qui vérifiera si votre <strong>"PR"</strong> est prête à être fusionnée vers la branche <strong>"develop"</strong> ou si vous devez modifier votre code. Si tout est bon, votre <strong>"PR"</strong> sera validée et vous (ou le membre qui a vérifié votre <strong>"PR"</strong>) pourra fusionner cette branche vers la branche <strong>"develop"</strong>. Si des modifications sont demandées, vous devrez effectuer les commandes suivantes :*

Si la branche <strong>"develop"</strong> du repository distant n'est pas à jour avec la branche 'develop' de votre repository local :
```bash
$ git pull origin develop
```
Si vous n'avez plus la branche de votre <strong>"PR"</strong> en local :
```bash
$ git fetch
```
Positionnez-vous dans la branche de votre <strong>"PR"</strong> :
```bash
$ git checkout features/nom-de-votre-fonctionnalité
```
Effectuez les modifications demandées sur votre <strong>"PR"</strong>. Vous pourrez ensuite effectuer les commandes suivantes :
```bash
$ git add .
$ git commit -m "votre message de modification - V1"
$ git push -u origin features/nom-de-votre-fonctionnalité
```

Markdown Preview Editor
------------

- Installer dans les extensions Visual Studio Code <strong>"Markdown Preview Editor"</strong> et redemarrez Visual Studio Code si besoin.

- Une fois l'extension installée et active, ouvrez le preview avec la combinaison clavier suivante : ctrl-k + v

Envs (dotnet)
------------

- <u>Ajout de la connectionString dans EFCore</u> :
    - Positionnez-vous dans le dossier <strong>"./back/EFCore"</strong> et créez un fichier <strong>"appsettings.json"</strong> :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;User id=root;Password=ToUpDaTePaSsWoRd;Database=ToUpDaTeDaTaBaSe;Persistsecurityinfo=True"
  }
}
```

- <u>Ajout de la connectionString dans Api</u> :
    - Positionnez-vous dans le dossier <strong>"./back/Api"</strong> et créez un fichier <strong>"appsettings.json"</strong> :

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

- <u>Lancement des migrations vers la base de données (PhpMyAdmin)</u> :
    - Placez-vous dans le dossier <strong>"./back/EFCore"</strong>
    - Executez (via le terminal de commande) la commande suivante : 

- <u>Si jamais dotnet-ef n'est pas installé, lancer la commande</u> :

```bash
$ dotnet tool install --global dotnet-ef
```

- <u>Si jamais le fichier "InitCreateTables" n'existe pas dans les migrations, lancer la commande</u> :

```bash
$ dotnet ef migrations add InitCreateTables
```

- <u>Envoyer les migrations vers la base de données via cette commande</u>

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

Application (angular)
------------

```
  Si vous souhaitez modifier le host et/ou le numéro de port de votre application web angular, vous pouvez modifier le fichier "./front/package.json" comme ceci :
```

```json
{
  "scripts": {
    "ng": "ng",
    "build": "ng build",
    "watch": "ng build --watch --configuration=local --host=localhost --port=4200",
    "test": "ng test",  
    "start:local": "ng serve --configuration=local --host=localhost --port=4200",
    "start:preproduction": "ng serve --configuration=preproduction --host=mms-preprod.example.com",
    "start:production": "ng serve --configuration=production --host=mms-prod.example.com"
  }
}
```

- Lancement de l'application :

- Placez vous dans le dossier (./front/)

- Executez (via le terminal de commande) la commande suivante (serveur local): 

```bash
$ npm run start:local
```

Envs (angular)
------------

- Ajouter le dossier "environments" dans "./front/src/" et créer les fichiers suivants : 
      
- ./front/src/environments/environment.ts

```ts
export const environment = {
    production: false,
    apiUrl: 'http://localhost:5123',
    debug: true
};
```

- ./front/src/environments/environment.local.ts

```ts
export const environment = {
    production: false,
    apiUrl: 'http://localhost:5123',
    debug: true
};
```

- ./front/src/environments/environment.preproduction.ts

```ts
export const environment = {
    production: false,
    apiUrl: 'https://api-preprod.example.com',
    debug: false
};
```

- ./front/src/environments/environment.production.ts

```ts
export const environment = {
    production: true,
    apiUrl: 'https://api-prod.example.com',
    debug: false
};
```

```
  Notez ici que "apiUrl" correspond à l'url de l'application dotnet.
```