using System.Linq.Expressions;

namespace Base.Domain.Base.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntidade, TId>
           where TEntidade : class
           where TId : struct
    {
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] includeProperties);

        IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade? ObterPor(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties);

        bool Existe(Func<TEntidade, bool> where);

        IQueryable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] includeProperties);

        IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade? ObterPorId(TId id, params Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade Adicionar(TEntidade entidade);

        TEntidade Editar(TEntidade entidade);

        void Remover(TEntidade entidade);

        void RemoverLista(IEnumerable<TEntidade> entidade);

        IEnumerable<TEntidade> AdicionarLista(IEnumerable<TEntidade> entidades);

        Task<TEntidade?> ObterPorAsync(Func<TEntidade, bool> where, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties);

        Task<TEntidade?> ObterPorIdAsync(TId id, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties);

        Task<bool> ExisteAsync(Func<TEntidade, bool> where, CancellationToken cancellationToken = default);

        Task<TEntidade> AdicionarAsync(TEntidade entidade);
    }
}
