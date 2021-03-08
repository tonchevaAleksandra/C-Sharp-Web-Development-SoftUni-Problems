using System.Collections.Generic;
using Panda.ViewModels;

namespace Panda.Services
{
    public interface IReceiptsService
    {
        void Create(string packageId, string recipientId);

        ICollection<ReceiptViewModel> GetAllReceipts(string userId);
    }
}
