using BT6_EnityFramework.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT6_EnityFramework.BLL
{
    internal class BLL_QLSV
    {
        public List<CBBItems> setDataCBBLSH()
        {
            List<CBBItems> cb = new List<CBBItems>();
            cb.Add(new CBBItems
            {
                Value = 0,
                Text = "All"
            });
            using (LNHEntities1 Eni = new LNHEntities1())
            {
                foreach(LSH i in Eni.LSH)
                {
                    cb.Add(new CBBItems
                    {
                        Value = i.ID_LOP,
                        Text = i.NAMELOP
                    });
                }
            }
            return cb;
        }
        public List<string> getColName()
        {
            List<string> res = new List<string>();
            using(LNHEntities1 eni = new LNHEntities1())
            {
                var li = eni.SV1.Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.GENDER, p.DTB })
                    .First().GetType().GetProperties().Select(p => p.Name).ToList();
                res = li;
            }
            return res;
        }
    }
}
