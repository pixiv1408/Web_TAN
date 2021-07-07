using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using testing.Models;

namespace testing.ViewModels
{
   

    public class GioHang
    {
        public int FoodID { get; set; }

        [StringLength(30)]
        public string FName { get; set; }

        [StringLength(255)]
        public string Fimage { get; set; }

        public int FPrice { get; set; }

        public int DAmount { get; set; }

        public int BTotal
        {
            get { return DAmount * FPrice; }
        }

        Database data = new Database();

        public GioHang(int foodID)
        {
            FoodID = foodID;
            Food fd = data.Foods.SingleOrDefault(x => x.FoodID == FoodID);
            FName = fd.FName;
            Fimage = fd.Fimage;
            FPrice = int.Parse(fd.FPrice.ToString());
            DAmount = 1;
        }
    }

}