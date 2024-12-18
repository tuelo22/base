﻿using Base.Domain.Notification.Entities;
using Base.Domain.Resources;

namespace Base.Domain.Base.ValueObjects
{
    public class Descricao : Notificavel
    {
        public String Valor { get; private set; } = String.Empty;

        /// <summary>
        /// Ef Core.
        /// </summary>
        protected Descricao()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="valor"></param>
        public Descricao(String valor)
        {
            Valor = valor;

            if(!SeObrigatorioNaoInformado(String.IsNullOrEmpty(valor), Textos.Descricao))
            {
                SeInadequadoTamanhoMinimoMaximo(Textos.Descricao, valor, 5, 255);
            }            
        }

        public override string ToString()
        {
            return Valor;
        }

        public static implicit operator string(Descricao descricao) => descricao.Valor;

        public static implicit operator Descricao(string descricao) => new(descricao);
    }
}
