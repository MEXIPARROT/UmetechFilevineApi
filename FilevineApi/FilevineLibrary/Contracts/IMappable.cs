using System.Data;

namespace PCLawData.Contracts
{
    public interface IMappable
    {
        void MapFromDataRow(DataRow row);
    }
}