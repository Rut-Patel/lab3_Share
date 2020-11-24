/*
 Rut Patel
 Refernce form Week 7 Lab3 video
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class share
    {
        //Data members
        protected string buyerName;
        protected string buyDate;
        protected int number;
        protected string type;
       
       


        /// <summary>
        /// Getter and setter for the buyerName.
        /// </summary>
        public string Buyer
        {
            get { return this.buyerName; }
            set { this.buyerName = value;  }
        }
        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        public string BuyDate
        {
            
            get { return this.buyDate; }
            set { this.buyDate = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public share(string buyerName, string buyerDate,int number,string type)
        {
            this.buyerName = buyerName;
            this.buyDate = buyerDate;
            this.number = number;
            this.type = type;
        }

    }
}
