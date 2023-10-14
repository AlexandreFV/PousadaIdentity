﻿namespace PousadaIdentity.Entities
{
    public class Quarto
    {

        public int QuartoId { get; set; }

        public string? Disponibilidade { get; set; }

        public string? Numero { get; set; }

        public string? Tipo { get; set; }

        public string? ArCondicionado { get; set; }

        public string? ValorTotalQuarto { get; set;}

        public int? ReservaID { get; set; }

        public Reserva? Reserva { get; set; }
    }
}
