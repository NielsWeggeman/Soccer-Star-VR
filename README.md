# Soccer Star VR

Fit VR-game gebouwd voor de sr. challenge


*---- Welcome to... Soccer Star VR! ----*

Deze VR-voetbalgame is gebouwd om mijn ervaring in Unity tot dusver te laten zien. 
Het is een speelse en steeds uitdagender wordende game, die spelers uitnodigt om 
in een speelse setting te sporten.

Soccer Star VR plaatst je in een professioneel voetbalstadion waar je de voetballen moet stoppen
en de bommetjes moet ontwijken. Het spel bevat (nu) 6 levels, waarbij de ballen onder steeds 
moeilijkere hoeken, met hogere snelheden en met meer spin worden aangeschoten.


*---- Design Keuzes ----*

De gegeven uitdaging was om een applicatie te bouwen waarmee patienten met chronische pijn 
of long-covid klachten uitgedaagd kunnen worden om meer te bewegen. Uit ervaring weet ik dat
een hoop van dit soort spellen werken op basis van 'slechts' het maken van bepaalde bewegingen 
of reiken naar bepaalde doelen. Zelf vind ik dat nog al saai, en weinig uitdaging.

Vandaar dat ik ervoor heb gekozen een Wii Sports-achtig spel te maken dat de speler uitnodigt
om te bewegen, door hem/haar in een meer game-achtige omgeving te zetten in plaats van het doen 
van droge oefeningen.

Het pel begint vrij kalmpjes aan, maar de levels worden steeds lastiger. Dit helpt de speler
het spel leren begrijpen, maar bouwt daarmee ook rustig op, om de patiënt de tijd te geven
te wennen aan de exercitie. De speler wordt 'beloond' door het overzicht met de gescoorde punten
aan het uiteinde van het stadium en krijgt ook feedback d.m.v. de geluiden.

De game is ontworpen om te worden gespeeld op de Oculus Quest 2

Bekijk zeker ook even een opname van het spel op https://www.youtube.com/watch?v=Nn5rcXA3vUY&ab_channel=NielsWeggeman
of download de 'Soccer Star VR.apk' en installeer deze op de Quest 2 om het spel te spelen.

![alt text](https://github.com/NielsWeggeman/Soccer-Star-VR/blob/main/Soccer%20Star%20VR%20Main%20menu%20view.jpg)
![alt text](https://github.com/NielsWeggeman/Soccer-Star-VR/blob/main/Soccer%20Star%20VR%20Game%20view.jpg)


*---- Verbeterpunten voor het spel ---- *

Deze app is binnen twee dagen gebouwd. Als ik hier meer tijd aan zou besteden
spel, zou ik aanraden:

- Meer niveaus aan het spel toevoegen.
- Meer voetbalmodellen aan het spel toevoegen.


*---- Groei sinds vorige opdracht ----*

In het afgelopen jaar heb ik in totaal ongeveer een maand gewerkt met Unity,
bovenop de ervaring die ik al had van een paar jaar terug, de 'intern challenge'
daarin meegenomen.

Mijn belangrijkste inzichten wbt werken met Unity tot dusver zijn:

- Dat ik er met de volle 100% geniet om ermee te werken.
- Volgende keer denk ik vooraf na over een flowchart voor de logica van de app, 
- want ik ben nu 4 uur langer bezig geweest met herstructureren om de laatste bug te vinden.
- Ik weet ondertussen hoeveel sneller ik kan werken en hoeveel meer ik voor elkaar krijg
  door een goede tutorial of de juiste comment op StackOverflow te zoeken.
- Ik focus voortaan op zo weinig mogelijk vertices en assets en slechts de broodnodige 
  berekeningen om prestaties optimaal te krijgen.

Na het doen van de intern challenge besloot ik deze keer veel meer van de code zelf 
te schrijven, in plaats van te leunen op tutorials. 95% van de code in dit spel is 
zelf geschreven, waarbij ik slechts kleine trucjes leen uit tutorials en artikelen.

Dit maakt de code iets rommeliger dan bij de intern challenge, hoewel ik de code waar
mogelijk heb opgeschoond. De volgende keer zou ik eerst een logic flowchart opstellen 
van het spel opstellen voordat ik ga bouwen.

Ik ben vooral trots op het algoritme dat bepaalt hoe de bal zo geschoten kan worden, 
dat deze altijd dicht bij de speler landt (te zien in de kickBall.cs bestand), 
evenals op het zelf uitvinden hoe ik een effectief level-systeem kan bouwen.

Van deze uitdaging heb ik met name meer geleerd over:
- Structureren en debuggen van code
- Verschillende niveaus in een spel instellen
- Werken met het unity UI-systeem
- Eenmalig afspelen audiobronnen instellen
- Werken met het deeltjessysteem
- Werken met verlichting & fakkels

In de toekomst zou het nuttig kunnen zijn om meer te weten te komen over:
- Meer intuïtieve gebruikersinterface en controle-ervaringen bouwen
- Goede leerervaringen creëren
- Werken met menselijke avatars voor instructies
- Multiplayer-interacties opzetten in VR
- Realistischere materialen en verlichting creëren in VR
- GameObjects spawnen en vernietigen
- Optimalisatie van ontwerp en code om de rekenbelasting te minimaliseren


*---- IP ----*

Ik heb gratis audiosamples assets gebruikt om de game een energieke, game-achtige sfeer te geven.
Dit project zou niet mogelijk zijn geweest zonder de audio van:
- 'ROYALTY FREE Breaking News Music / News Intro Music Royalty Free / News Opener Music Royalty Free' van 'Music for Video Library'
  op https://www.youtube.com/watch?v=y1clZNYIl4Y&t=2s&ab_channel=MusicforVideoLibrary
- 'Royalty Free Sports Music | Sports Background Music' door 'Alex Grohl - Background Rock Music'
  op https://www.youtube.com/watch?v=nPW3INoPK8c&ab_channel=AlexGrohl-BackgroundRockMusic

En de volgende gratis assets:
- 'Arena das Dunas Free 3D model' door 'marco-aurelio-o-m' op CGTrader https://www.cgtrader.com/free-3d-models/architectural/engineering/arena-das-dunas
- 'Free Crowd Cheering Sounds' door 'Gregor Quendel' op de Unity Asset Store
- 'Outdoor Ground Textures' door 'A dog's life software' op de Unity Asset Store
- 'SRP Lens Flare (for URP)' door 'ALIyerEdon' op de Unity Asset Store
- 'Procedural fire' door 'HOVL Studio' op de Unity Asset Store
