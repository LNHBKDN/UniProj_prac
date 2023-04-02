using _3Layer_MS.DAL;
using _3Layer_MS.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Layer_MS.BLL
{
    internal class QLSVBLL
    {
        public List<CBBItem> getCBB()
        {
            List<CBBItem> res = new List<CBBItem>();
            res.Add(new CBBItem()
            {
                Value = 0,
                Text = "All"
            });
            QLSVDAL dal = new QLSVDAL();
            foreach(LSH i in dal.getAllLSH())
            {
                res.Add(new CBBItem
                {
                    Value = i.ID_Lop,
                    Text = i.LopName
                });
            }
            return res;
        }
        public List<CBBItem> getColumnNameBll()
        {
            List<CBBItem> res = new List<CBBItem>();
            QLSVDAL dal = new QLSVDAL();
            foreach(DataRow row in dal.getColumnNameDAL().Rows)
            {
                res.Add(new CBBItem
                {
                    Text = row["COLUMN_NAME"].ToString()
                });
            }
            return res;
        }
        public DataTable getSVbyIDlop(int id)
        {
            DataTable dt = new DataTable();
            QLSVDAL dal = new QLSVDAL();
            if(id == 0)
            {
                dt = dal.setDataDAL();
            }
            else
            {
                dt = dal.getSVbyIDLopDAL(id);
            }
            return dt;
        }
        public void addSVBLL(SV1 sv)
        {
            QLSVDAL dal = new QLSVDAL();
            dal.addSVDAL(sv);
        }
        public void delSVBLL(string mssv)
        {
            QLSVDAL dal = new QLSVDAL();
            dal.delSVDAL(mssv);
        }
        public DataTable setDataBLL()
        {
            QLSVDAL dal = new QLSVDAL();
            return dal.setDataDAL();
        }
        public DataTable getSVByNameBLL(string name,string txtlop)
        {
            QLSVDAL dal= new QLSVDAL();
            return dal.getSVbyNameDAL(name,txtlop);
        }
        public DataTable SortBLL(string srt,string lop)
        {
            QLSVDAL dal = new QLSVDAL();
            return dal.getSortRecord(srt,lop);
        }
    }
}
