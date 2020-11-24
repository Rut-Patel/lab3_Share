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
    class CommonShare : share
    {
        protected int price = 42;
        protected int votingPower = 1;


        public int Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public int VotingPower
        {
            get { return this.votingPower; }
            set { this.votingPower = value; }
        }

        public CommonShare(string buyerName, string buyerDate,int number,string type): base( buyerName, buyerDate,number,type)
        {
            this.buyerName = base.buyerName;
            this.buyDate = base.buyDate;
            this.number = base.number;
            this.type = base.type;
        }
    
    }
}
