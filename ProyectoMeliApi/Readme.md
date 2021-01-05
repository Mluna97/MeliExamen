#Meli Examen Martin Luna

Para el hosteo de APIs decidí utilizar Amazon AWS, con sus funciones Lambda, que permiten el hosteo de APIs.

El link del endpoint es https://z8rpaf5gol.execute-api.sa-east-1.amazonaws.com/Prod/

Dejando así las siguientes urls de acceso a las APIs

Nivel 2: https://z8rpaf5gol.execute-api.sa-east-1.amazonaws.com/Prod/api/Mutante/mutant

recibiendo un HTTP POST con un Json el cual tenga el siguiente formato:
POST → /mutant/
{
	"dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]
}

Nivel 3: https://z8rpaf5gol.execute-api.sa-east-1.amazonaws.com/Prod/api/Mutante/stats

Para la base de datos opté por SQL Server en el servicio RDS (Relational Database Service) de Amazon AWS

Repositorio de Git: https://github.com/Mluna97/MeliExamen