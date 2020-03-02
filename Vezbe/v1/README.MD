# STRUKTURA PROGRAMA

![image](https://user-images.githubusercontent.com/49925421/75669216-f5556000-5c7a-11ea-85c2-0d173f4846b6.png)

# FORMIRANJE GRAFA
![image](https://user-images.githubusercontent.com/49925421/75668631-e1f5c500-5c79-11ea-85f6-bc7efde271da.png)

Potrebno je implementirati metode za formiranje grafa, metodu koja odredjuje moguca sledeca stanja u koja se moze preci iz trenutnog stanja, algoritme za pretragu (***DFS, BFS, iterativni DFS***), nakon pronalaska resenja u konzolu je potrebno ispisati ukupnu duzinu puta koji je predjen. 

# BFS
![image](https://user-images.githubusercontent.com/49925421/75670002-42860180-5c7c-11ea-9e4e-9475e3fa5210.png)

# DFS
![image](https://user-images.githubusercontent.com/49925421/75670101-7103dc80-5c7c-11ea-93d7-1bbbfa608f83.png)

## Algoritmi pretrage
Potrebno je uociti razliku izmedju **BFS-a** i **DFS-a** i kada je koji optimalniji za upotrebu.
***BFS (Breadth First Search)*** je tehnika zasnovana na cvorovima i sluzi za pronalazenje najkrace putanje u grafu. Ona koristi ***Queue*** kao strukturu podataka koja funkcionise na principu first in first out (***FIFO***), algoritam je ***sporiji*** od DFS-a. Algoritam je dosta dobar kada se cvor koji trazimo nalazi ***blizu***. 
![image](https://user-images.githubusercontent.com/49925421/75671214-84b04280-5c7e-11ea-8c4c-a3a7cf5a215e.png)
***DFS (Depth First Search)*** je tehnika koja se bazira na granama. Koristi ***Stack*** kao srtrukturu podataka (***LIFO***) . Algoritam je dosta dobar kada je resenje dosta udaljeno.

![BFS vs DFS](https://user-images.githubusercontent.com/49925421/75672251-a6122e00-5c80-11ea-9009-17f0ae46a401.png)

## Razlika
U ovom konktretnom primeru implementacije bitno je primetiti da se za modelovanje struktura ***Queue*** i ***Stack*** koristi Lista, glavna razlika je u nacinu dodavanja u listu, kod ***BFS-a*** dodajemo element ***na kraj*** a pri obradi uzimamo sa pocetka, na taj nacin simuliramo **FIFO**, kod ***DFS-a*** dodajemo **na pocetak** i uzimamo sa pocetka na taj nacin simuliramo **Stack** tj ***LIFO***.
