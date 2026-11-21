namespace Base.Domain.Base.Interfaces.Transactions
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();

        /// <summary>
        /// OPCIONAL: roda <paramref name="acao"/> dentro de uma transação do banco. O fluxo comum continua sendo o de sempre (o serviço muda
        /// as entidades em memória, avisa erro por mensagem e o controller grava no fim com <see cref="CommitAsync"/>); use este método só
        /// quando a operação faz gravações diretas no banco (por exemplo UPDATE condicional) que precisam valer juntas, ou quando precisa
        /// de uma trava de linha, que só existe dentro de uma transação.
        /// </summary>
        /// <param name="acao">
        /// O trabalho. Devolva <c>true</c> para confirmar: o que estiver pendente no contexto é gravado e a transação confirmada. Devolva
        /// <c>false</c> para desfazer tudo. Se lançar exceção, desfaz e relança.
        /// </param>
        /// <returns><c>true</c> se confirmou; <c>false</c> se a ação pediu para desfazer.</returns>
        Task<bool> ExecutarEmTransacaoAsync(Func<Task<bool>> acao);
    }
}
