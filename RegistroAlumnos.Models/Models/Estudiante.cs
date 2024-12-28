﻿namespace RegistroAlumnos.Models.Models
{
    public class Estudiante
    {
        public string? Id { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? CorreoElectronico { get; set; }
        public int? Edad { get; set; }
        public DateTime? FechaInsripcion { get; set; }
        public Curso? Curso { get; set; }
        public bool? Estado { get; set; }
        public string? NombreCompleto => $"{PrimerNombre} {PrimerApellido}";

        public string EstadoTexto => Estado.HasValue ? (Estado.Value ? "Activo" : "Inactivo") : "Estado Desconocido";
    }
}
