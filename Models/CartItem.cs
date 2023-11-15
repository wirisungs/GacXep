using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GacXep.Models
{
    public class CartItem
    {
        DBGacXepBookstoreEntities db = new DBGacXepBookstoreEntities();

        public int ProID { get; set; }

        public string ProImage { get; set; }
        public string ProName { get; set; }


        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Decimal FinalPrice()
        {
            return Quantity * Price;
        }

        public CartItem(int ProID)
        {
            this.ProID = ProID;
            var bookDB = db.Books.Single(s => s.ProID == this.ProID);
            var detailDB = db.BookDetails.Single(s => s.ProID ==  this.ProID);
            this.ProImage = bookDB.ProImage;
            this.ProName = bookDB.ProName;
            this.Quantity = 1;
            this.Price = (decimal)detailDB.Price;

        }
    }
}