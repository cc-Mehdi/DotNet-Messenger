namespace DataLayer.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IUsersRepository UsersRepository { get; set; }
        public IMessagesRepository MessagesRepository { get; set; }
        void Save();
    }
}
