
<h1 align="center"> Reinforcement Learning </h1>

<p align="center">

  <img width="1000" height="400" src="https://user-images.githubusercontent.com/45834270/77358482-34db0d80-6d4a-11ea-8132-67b6b7f8e5cd.png">

</p>

<p align="center">

  <img width="900" height="380" src="https://user-images.githubusercontent.com/45834270/77358646-808db700-6d4a-11ea-950f-bc016222e26f.png">

</p>

<p align="center">

  <img width="900" height="380" src="https://user-images.githubusercontent.com/45834270/77358883-fdb92c00-6d4a-11ea-8106-43d1502aa4d8.png">

</p>

Dva nacina za Reinforcement Learning su ***Model-Based*** Learning i ***Model-Free*** Learning

<br>
<br>
<h1 align="center"> Model-Based Learning </h1>

<p align="center">

  <img width="800" height="341" src="https://user-images.githubusercontent.com/45834270/77361062-d6fcf480-6d4e-11ea-98af-59ac871e13aa.png">

</p>

<p align="center">

  <img width="800" height="361" src="https://user-images.githubusercontent.com/45834270/77362504-4f64b500-6d51-11ea-8fa4-60cb844112f5.png">

</p>


<br><br>
<h1 align="center"> Model-Free Learning </h1>

Model-Free learning cemo podeliti u dve faze, ***Passive*** Reinforcement Learning i ***Active*** Reinforcement Learning

<p align="center">

  <img width="800" height="361" src="https://user-images.githubusercontent.com/45834270/77364669-09115500-6d55-11ea-8ac7-88f0d15e7588.png">

</p>

Passive dalje delimo u **Direktnu evaluaciju** i **Indirektnu**

<br>

## Direktna evaluacija

<p align="center">

  <img width="800" height="283" src="https://user-images.githubusercontent.com/45834270/77366387-33b0dd00-6d58-11ea-904c-c7b15a1a4764.png">

</p>

<p align="center">

  <img width="800" height="333" src="https://user-images.githubusercontent.com/45834270/77366873-44158780-6d59-11ea-92b1-8905475830ac.png">

</p>


<br>

<p align="center">

  <img width="840" height="421" src="https://user-images.githubusercontent.com/45834270/77367567-bf2b6d80-6d5a-11ea-9380-379387dff38d.png">

</p>

<br>

<p align="center">

  <img width="840" height="481" src="https://user-images.githubusercontent.com/45834270/77367937-7b853380-6d5b-11ea-9a62-0acfbd8d9a60.png">

</p>

<br>

<p align="center">

  <img width="800" height="333" src="https://user-images.githubusercontent.com/45834270/77369321-a3c26180-6d5e-11ea-9764-944d88d9f09f.png">

</p>

<p align="center">

  <img width="800" height="333" src="https://user-images.githubusercontent.com/45834270/77369764-a96c7700-6d5f-11ea-9f4e-67a8c9821958.png">

</p>

Problem sa **TDL**-om je sto ne mozemo da pretvorimo rezultat TDL-a(*vrednosti za svako stanje*) u politiku zato sto nam trebaju T i R, zbog toga cemo da ucimo ***Q-values*** a ne V vrednosti.

<h1 align="center"> Active Reinforcement Learning </h1>

Aktivno skupljamo podatke dok ucimo Q-vrednosti. Q-vrednosti cemo uciti slicno kao i V-vrednosti samo sa malom razlikom.

<p align="center">

  <img width="840" height="481" src="https://user-images.githubusercontent.com/45834270/77370525-627f8100-6d61-11ea-839d-a8aa85982a9f.png">

</p>

<p align="center">

  <img width="840" height="481" src="https://user-images.githubusercontent.com/45834270/77370846-0ec16780-6d62-11ea-9575-50868e9b995f.png">

</p>

<p align="center">

  <img width="840" height="333" src="https://user-images.githubusercontent.com/45834270/77370863-197bfc80-6d62-11ea-8167-c843883f7617.png">

</p>

<br><br><br><br><br>
<br><br>

<p align="center">

  <img width="840" height="481" src="https://user-images.githubusercontent.com/45834270/77374140-39172300-6d6a-11ea-844d-b265811dbef4.png">

</p>

<p align="center">

  <img width="840" height="481" src="https://user-images.githubusercontent.com/45834270/77426082-13266880-6dd4-11ea-94d7-fb1baa0986ac.png">

</p>

<br><br>

## Approximate Q-Learning

Posto za realne probleme nije izvodljivo popunjavati cele matrice(sa Q-vrednostima), ulazimo u fundamentalnu ideju **Masinskog ucenja** a to je da naucimo mali broj Q-vrednosti, i onda kada se nadjemo u slicnoj situaciji, da znamo da delujemo na osnovu generalizovanog ucenja.

<p align="center">

  <img width="840" height="431" src="https://user-images.githubusercontent.com/45834270/77431072-7b794800-6ddc-11ea-9f48-fafad26588d2.png">

</p>

<p align="center">

  <img width="840" height="431" src="https://user-images.githubusercontent.com/45834270/77431407-f3477280-6ddc-11ea-936d-686b415a6b18.png">

</p>

<p align="center">

  <img width="840" height="481" src="https://user-images.githubusercontent.com/45834270/77432084-e0816d80-6ddd-11ea-8a25-c6954421cf5d.png">

</p>

<p align="center">

  <img width="840" height="481" src="https://user-images.githubusercontent.com/45834270/77437218-b2535c00-6de4-11ea-97d8-95d711872dc4.png">

</p>

<p align="center">

  <img width="840" height="431" src="https://user-images.githubusercontent.com/45834270/77437969-900e0e00-6de5-11ea-9501-7f0908316c51.png">

</p>

<p align="center">

  <img width="840" height="431" src="https://user-images.githubusercontent.com/45834270/77438136-be8be900-6de5-11ea-93c9-18239f58fe8b.png">

</p>

<p align="center">

  <img width="840" height="431" src="https://user-images.githubusercontent.com/45834270/77438431-16c2eb00-6de6-11ea-8a5f-e0805cd39c9e.png">

</p>

<p align="center">

  <img width="840" height="431" src="https://user-images.githubusercontent.com/45834270/77438806-92249c80-6de6-11ea-9e55-2206394c5a4b.png">

</p>

<p align="center">

  <img width="840" height="281" src="https://user-images.githubusercontent.com/45834270/77439488-635af600-6de7-11ea-9e6e-d9c8e25b5acc.png">

</p>

