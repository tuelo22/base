using System.Linq.Expressions;

namespace Base.Domain.Base.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntidade, TId>
           where TEntidade : class
           where TId : struct
    {
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] includeProperties);

        IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade ObterPor(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties);

        bool Existe(Func<TEntidade, bool> where);

        IQueryable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] includeProperties);

        IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade ObterPorId(TId id, params Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade Adicionar(TEntidade entidade);

        TEntidade Editar(TEntidade entidade);

        void Remover(TEntidade entidade);

        void RemoverLista(IEnumerable<TEntidade> entidade);

        IEnumerable<TEntidade> AdicionarLista(IEnumerable<TEntidade> entidades);

        Task<IQueryable<TEntidade>> ListarPorAsync(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] includeProperties);

        Task<IQueryable<TEntidade>> ListarEOrdenadosPorAsync<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties);

        Task<TEntidade> ObterPorAsync(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties);

        Task<bool> ExisteAsync(Func<TEntidade, bool> where);

        Task<IQueryable<TEntidade>> ListarAsync(params Expression<Func<TEntidade, object>>[] includeProperties);

        Task<IQueryable<TEntidade>> ListarOrdenadosPorAsync<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties);

        Task<TEntidade> ObterPorIdAsync(TId id, params Expression<Func<TEntidade, object>>[] includeProperties);

        Task<TEntidade> AdicionarAsync(TEntidade entidade);

        Task<TEntidade> EditarAsync(TEntidade entidade);

        void RemoverAsync(TEntidade entidade);

        void RemoverListaAsync(IEnumerable<TEntidade> entidade);

        Task<IEnumerable<TEntidade>> AdicionarListaAsync(IEnumerable<TEntidade> entidades);
    }
}
