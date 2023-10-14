using System.Numerics;

namespace PousadaIdentity.Entities
{
    public class Reserva
    {

        public int ReservaId { get; set; }

        public string? CheckIn { get; set; }

        public string? CheckUp { get; set; }

        public string? DataReservada { get; set; }

        public string? Estado { get; set; } 

        public decimal? ValorTotalReserva { get; set; }

        public int QuartoID { get; set; }

        public Quarto? Quarto { get; set; }

        public string? Token { get; set; }

        public int? PessoaId { get; set; }

        public Pessoa? Pessoa { get; set; }

    }
}
