﻿namespace WSMovimientos.Entidades.DTOS.Entrada
{
    public class EntradaConsultaMovimiento
    {
        public long? IdMovimiento { get; set; } = null;
        public long? IdCuenta { get; set; } = null;
        public string? FechaInicio { get; set; } = null;
        public string? FechaFin { get; set; } = null;

    }
}