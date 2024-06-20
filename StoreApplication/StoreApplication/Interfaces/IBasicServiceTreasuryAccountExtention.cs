using Store.Interfaces;
using StoreApplication.Models;

namespace StoreApplication.Interfaces
{
	public interface IBasicServiceTreasuryAccountExtention <TreasuryAccount>: IBasicServices<TreasuryAccount>
	{
		public short UpdateTreasuryAccountBalance(int treasuryId, int transactionId);
	}
}
