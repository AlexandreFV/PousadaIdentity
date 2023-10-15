﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace PousadaIdentity.Entities
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public DateTime DataReservada { get; set; }

        public string? Estado { get; set; } 

        public decimal? ValorTotalReserva { get; set; }

        [ForeignKey("Quarto")]
        public int QuartoID { get; set; }

        public Quarto? Quarto { get; set; }

        [ForeignKey("Pessoa")]
        public int? PessoaId { get; set; }

        public Pessoa? Pessoa { get; set; }

    }
}
