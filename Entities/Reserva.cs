namespace PousadaIdentity.Entities
{
    public class Reserva
    {

        public int ReservaId { get; set; }

        public string? DataReserva { get; set; }

        public string? DataQuandoReservada { get; set; }

        public string? Estado { get; set; }

        public decimal? ValorTotalReserva { get; set; }

        public string? Token { get; set; }

        public int? PessoaId { get; set; }

        public Pessoa? Pessoa { get; set; }

    }
}
