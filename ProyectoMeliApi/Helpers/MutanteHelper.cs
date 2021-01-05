using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Helpers
{
    public class Direccion
    {
        public int IncrFila { get; set; }
        public int IncrCol { get; set; }
        public Direccion(int incrFila, int incrCol)
        {
            IncrFila = incrFila;
            IncrCol = incrCol;
        }
    }

    public static class MutanteHelper
    {
        private static readonly IList<Direccion> Direcciones = new List<Direccion>(){
            new Direccion(0, 1),    //DERECHA
            new Direccion(1, 0),    //ABAJO
            new Direccion(1, 1),    //DIAGONAL DERECHA
            new Direccion(1, -1),   //DIAGONAL IZQUIERDA
         };

        private static readonly int LongitudNecesaria = 4;

        private static readonly IList<char> CaracteresValidos = new List<char>() { 'A', 'C', 'G', 'T' };

        // aca va toda la logica 
        public static bool IsMutant(string[] adn)
        {
            char[][] matriz = adn.Select(x => x.ToCharArray()).ToArray();
            for (var i = 0; i < matriz.Length; i++)
                for (var j = 0; j < matriz[i].Length; j++)
                    for (var pos = 0; pos < Direcciones.Count; pos++)
                    {
                        if (VerificarLongitud(matriz, i, j, Direcciones[pos], LongitudNecesaria))
                            return true;
                    }

            return false;
        }

        // Valida que partiendo de una posicion, y siguiendo la direccion indicada se llegue a una 
        // cadena de al menos @longitud de largo con el mismo caracter que al principio.
        private static bool VerificarLongitud(char[][] matriz, int fila, int col, Direccion dir, int longitud)
        {
            if (longitud <= 1) return true;

            int filaSig = fila + dir.IncrFila;
            int colSig = col + dir.IncrCol;
            char? actual = ValueOf(matriz, fila, col);
            char? siguiente = ValueOf(matriz, filaSig, colSig);

            return actual.HasValue && siguiente.HasValue && actual.Value == siguiente.Value
                ? VerificarLongitud(matriz, filaSig, colSig, dir, longitud - 1)
                : false;
        }

        // Si es valida la posicion, devuelve el caracter
        private static char? ValueOf(char[][] matriz, int fila, int col)
        {
            if (fila < 0 || col < 0 || (fila >= matriz.Length || col >= matriz[fila].Length || !CaracteresValidos.Contains(matriz[fila][col])))
                return null;

            return matriz[fila][col];
        }
    }
}
