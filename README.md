# Experimentální repozitář pro koncept Teacher Control

Teacher Control má být webová aplikace, kde bude možno 
hodnotit své učitele. Hodnocení by mělo být reprezentování jednou
až pěti hvězdičkami a textem formou recenzí.
---

## Drbárna

Další funkce této webové aplikace by měla být drbárna. Živý chat
s možností poslat školní memes. Tyto memes by pravděpodobně 
musely být omezeny a buď schvalovány moderátory, nebo čistě omezeny
na selekci od moderátorů.
---

## Abstence

Pro zábavu by mohli studenti zapisovat pozdní příchody učitelů 
na hodiny. Pozdní příchod by se zadával v minutách a učitelé
by mohli dostávat odznáčky na základě jejich celkového času.

S tím by mohl být nějak spojený "Nálada metr".

---

## Král/královna dne

Bingo jako například [zde](http://kubicekm23.epsilon.spstrutnov.cz),
kde ten, kdo by jako první dosáhl binga by se stal králem/královnou.
Jak přesně by toto bingo mělo však fungovat není specifikováno.

Můj nápad je, že by se to dělilo na učitele. Tam je ale problém,
že by název král/královna moc neseděl. To je však jediný, který
mě s touto cestou napadá.

---

## Hlasování pro učitele

Hlasování registrovaných uživatelů pro učitele v různých kategoriích.
Pár kategoriích, které mě napadají, jsou například *nejlepší hlášky*, 
*nejsprostější* a *nejvíc sexy*.

---

## Celkové hodnocení učitelů

Finální hodnocení učitele by mělo vycházet jak z výsledků
hlasování, tak recenzí studentů a nedochvilnosti.

---

## Podmínky užívání webové aplikace

Vše toto by muselo být pod jednou podmínkou. Existuje moderátor,
který bude banovat uživatele za nevhodné zprávy a recenze. Recenze
by však měli být anonymní, ale stále navázány na účet, aby mohl být
zablokován.

---

*Následující text jsou instrukce a dokumentace, 
která je součástí použitého template.*

## Project structure.

Entities folder — contains the models for the database.

Models folder — a class for passing errors.

Data folder — houses the appDbContext and seeder for default data. There you can set the default admin credentials.

The rest should be self-explanatory.

## Running the project in a development environment.

The project is configured so that it is easy to run in a development environment as well as in a production environment.
In the development environment, the project is configured to use a local PostgreSQL database via docker compose.
For production, you change the connection string in the .env file.

1. Have Docker installed.
2. Run the docker compose file. (docker-compose up --build)
3. Run and edit the project using your IDE or editor of choice.