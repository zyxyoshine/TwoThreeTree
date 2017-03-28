using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoThreeTree
{
    public partial class form : Form
    {
        public form()
        {
            InitializeComponent();
        }

        private void insertb_Click(object sender, EventArgs e)
        {
            //Node tt = new Node(insert);
            int val = 0;
            try
            {
                val = Int32.Parse(insert.Text);
            }
            catch
            { }
            TwoThreeTree.add(val,info);
            info.Text += Environment.NewLine;
            show();
        }

        private void clrb_Click(object sender, EventArgs e)
        {
            info.Clear();
        }

        private void delb_Click(object sender, EventArgs e)
        {
            int val = 0;
            try
            {
                val = Int32.Parse(del.Text);
            }
            catch
            { }
            TwoThreeTree.remove(val, info);
            info.Text += Environment.NewLine;
            show();
        }

        private void qub_Click(object sender, EventArgs e)
        {
            show();
            int val = 0;
            try
            {
                val = Int32.Parse(qu.Text);
            }
            catch
            { }
            Node q = TwoThreeTree.findNode(TwoThreeTree.root, val, info);
            if (q == null)
                info.Text += "找不到" + val + Environment.NewLine;
            else
            {
                TwoThreeTree.showNode(q, info);
                q.leftVal = -q.leftVal;
                show();
                q.leftVal = -q.leftVal;
            }
                info.Text += Environment.NewLine;
        }
        public static bool hua(Node x,TextBox n)
        {
            n.Clear();
            if (x == null)
                return false;
            if (x.isTwoNode())
            {
                n.Text += "  " + x.val();
            }else
            {
                n.Text += x.leftVal + " " + x.rightVal;
            }
            return true;
        }
        public void show()
        {
                if (hua(TwoThreeTree.root, n11))
                {
                    if (hua(TwoThreeTree.root.leftChild, n21))
                    {
                        hua(TwoThreeTree.root.leftChild.leftChild, n31);
                        hua(TwoThreeTree.root.leftChild.middleChild, n32);
                        hua(TwoThreeTree.root.leftChild.rightChild, n33);
                    }
                    if (hua(TwoThreeTree.root.middleChild, n22))
                    {
                        hua(TwoThreeTree.root.middleChild.leftChild, n34);
                        hua(TwoThreeTree.root.middleChild.middleChild, n35);
                        hua(TwoThreeTree.root.middleChild.rightChild, n36);
                    }
                    if (hua(TwoThreeTree.root.rightChild, n23))
                    {
                        hua(TwoThreeTree.root.rightChild.leftChild, n37);
                        hua(TwoThreeTree.root.rightChild.middleChild, n38);
                        hua(TwoThreeTree.root.rightChild.rightChild, n39);
                    }
                }
           
        }
    }
}
