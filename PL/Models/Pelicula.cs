﻿namespace PL.Models
{
    public class Pelicula
    {
        internal dynamic Nombre;
        internal dynamic Descripcion;
        internal dynamic Fecha;
        internal dynamic Imagen;

        public List<object> Peliculas { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public decimal popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public IEnumerable<object> results { get; internal set; }
        public int IdMovie { get; internal set; }
    }
}