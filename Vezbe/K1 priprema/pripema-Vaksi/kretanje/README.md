<h2> Opšti način za definisanje načina kretanja </h2>

<h4> Figure: </h4>
<ul>
<li> Top: int [,] top = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } }; </li>
<li> Lovac: int[,] lovac = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } }; </li>
<li> Kralj: int [,] kralj = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } }; </li>
<li> Kraljica: int [,] kraljica = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } }; </li>
<li> Konj: int [,] konj = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } }; </li>
</ul>

### Kordinatni sistem
<pre>

             ----------------------> J kordinata
             |
             |
             |
             |
             |
             I kordinata
              
             * To znaci da bi isao desno, povesavas J kordinatiu
             * Da bi isao levo, smanjujes J kordinatu
             * Da bi isao gore, SMANJUJES I kordinatu
             * Da bi isao dole, POVECAVAS I kordinatu
             
             
</pre>

### Algoritam hesiranja

Moja ideja je da hesiram tako sto cu igru podeliti na frekvencije. U zavisnosti od frekvencije, dobija se odredjeni hes, samim tim, omogucavam u zavisnosti stanja igre da mozemo ili ne mozemo proci kroz odredjena polja.

<pre>
    hash = 10 * markI + markJ;            // ultra niza frekvencija: [0-99] 
    hash = 100 + 10 * markI + markJ;      // niska frekvencija: [100-199]
    hash = 200 + 10 * markI + markJ;      // srednja niza frekvencija: [200-299]
    hash = 300 + 10 * markI + markJ;      // srednja frekvencija: [300-399]
    hash = 400 + 10 * markI + markJ;      // visoka frekvencija: [400-499]
    hash = 500 + 10 * markI + markJ;      // ultra visoka frekvencija: [500-599]
</pre>

#### Obrazlozenje ideje
Posto je tabla 10 * 10 a krecemo indeksiranje od 0, to znaci da markI moze biti u opsegu od 0 do 9 a tako i markJ.

Na ovaj nacin obezbedjujem da nece doci do mesanja frekvencije, samim tim kada pokupimo recimo jednu kutiju, prelazimo na visu frekvenciju, i onda mozemo svako od tih polja opet obici zasigurno jer smo na jednom stepenu vece frekvencije.

Jedino pitanje je kada se prelazi na veci nivo frekvencije, ali to se odredjuje u kodu od zadatka do zadatka.


### Kretanje
<ul>
<li> Izabrati korake u odnosu na tekst zadatka. </li>
<li> Promenljivo bool <i> oneStep </i> postaviti na true za figure koje u jednom potezu prelaze jedno polje - kralj i konj. 
Za ostale figure postaviti na vrednost false. </li>
<li>
<pre>
for (int i = 0; i < koraci.GetLength(0); ++i)
{               
    int j = 1; 
    while (true)
    {
        int nextI = markI + j * koraci[i, 0];
        int nextJ = markJ + j * koraci[i, 1];
        ++j;
        //proveri da li je polje validno, tj. u granicama lavirinta
        if (nextI < 0 || nextI >= Main.brojKolona || nextJ < 0 || nextJ >= Main.brojVrsta)
        {
            break; //izasli smo iz granica lavirinta
        }
        //proveriti da li je polje sivo - ne moze se preci na njega
        if (lavirint[nextI, nextJ] == 1)
        {
            break; //polje je sivo
        }
        //dodaje se novo stanje
        State novi = sledeceStanje(nextI, nextJ);
        rez.Add(novi);
        //za figure koje prelaze samo jedno polje po potezu
        if (oneStep == true)
            break;                    
    }
}
</pre>
</li>
</ul>
