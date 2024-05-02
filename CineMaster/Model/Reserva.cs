using System;
using CineMaster.DTO; // Asegúrate de importar el espacio de nombres correcto

namespace CineMaster.Model
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Genero { get; set; } // Suponiendo que el género de la película se almacena como una cadena de texto
        // Otras propiedades de la reserva, como cliente, película, etc.

        // Relación con la película (suponiendo que una reserva está asociada a una película)
        public Pelicula Pelicula { get; set; }
    }
}
