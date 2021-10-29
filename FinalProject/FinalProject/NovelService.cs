using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public interface NovelService
    {
        IList<Novel> SearchNovel(string keyword, string NoVol);
        Novel LoadById(long id);
        void UpdateNovel(Novel novel);
        void DeleteById(int id);
    }
}
