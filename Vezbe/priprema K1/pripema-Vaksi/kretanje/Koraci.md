<h2> Opšti način za definisanje načina kretanja </h2>

<h4> Figure: </h4>
<ul>
<li> Top: int [,] top = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } }; </li>
<li> Lovac: int[,] lovac = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } }; </li>
<li> Kralj: int [,] kralj = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } }; </li>
<li> Kraljica: int [,] kraljica = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } }; </li>
<li> Konj: int [,] konj = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } }; </li>
</ul>

<h4> Kretanje: </h4>
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
