using VideoShare.DAL.Entities;
using System;

namespace VideoShare.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
