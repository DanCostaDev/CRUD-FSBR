﻿namespace CRUD_FSBR.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco {  get; set; }
        public int QuantidadeEstoque {  get; set; }
    }
}
