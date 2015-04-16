using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Duplicator
{
    public partial class Duplicator : Form
    {
        public Duplicator()
        {
            InitializeComponent();
        }

        private void btnWithDelimiter_Click(object sender, EventArgs e)
        {
            string lst = txtList.Text;
            string find = txtFind.Text;
            string textS = txtText.Text;
            string delimiter = txtDelimiter.Text;

            string outt = String.Empty;
            string[] lstitemS;
            string temp = "";

            var findS = find.Split(new string[] { delimiter }, StringSplitOptions.None);
            var listS = lst.Trim().Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; (i <= (listS.Length - 1)); i++)
            {
                lstitemS = listS[i].Trim().Split(new string[] { delimiter }, StringSplitOptions.None);
                temp = textS;
                for (int j = 0; (j <= (lstitemS.Length - 1)); j++)
                {
                    temp = temp.Replace(findS[j], lstitemS[j]);
                }
                outt = (outt + (temp + "\r\n"));
            }
            Clipboard.SetText(outt);
            txtOut.Text = outt;
        }
    }
}
