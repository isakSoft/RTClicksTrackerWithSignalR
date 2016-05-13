using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.ai.ext.upwork.test1.Models
{
    interface IRepository
    {
        IEnumerable<ClicksTracker> ClicksTrackers { get; }
        int SaveClicksTracker(ClicksTracker item);
        ClicksTracker DeleteClicksTracker(int itemId);
    }
}