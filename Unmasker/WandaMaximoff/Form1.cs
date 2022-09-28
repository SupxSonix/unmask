using System;
using System.Windows.Forms;

namespace WandaMaximoff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox1.Text = RemoveStringMask(StringValue.Text);
        }

        public static string RemoveStringMask(string pMaskedString)
        {
            if (pMaskedString != null)
            {
                int ctr = 0, ctrLen = 1;
                string SearchStr = null, UnmaskValue = null;
                string[] StringToInt = new string[28] { "V", "O", "N", "E", "A", "T", "U", "K", "Z", "Ñ", "F", "B", "M", "L", "S", "C", "I", "R", "J", "H", "W", "P", "Q", "X", "D", "Y", "G", " " };
                while (ctrLen <= pMaskedString.Length)
                {
                    SearchStr = pMaskedString.Substring(ctrLen - 1, 3); ctr = 0;
                    while (ctr < 28)
                    {
                        if (int.TryParse(SearchStr.Trim(), out int OutNum))
                        {
                            int.TryParse(SearchStr.Substring(1, 2).Trim(), out int Indx);
                            if (SearchStr.Substring(0, 1) == "1")
                                UnmaskValue = UnmaskValue + StringToInt[Indx];
                            else if (SearchStr.Substring(0, 1) == "0")
                                UnmaskValue = UnmaskValue + StringToInt[Indx].ToLower();
                            ctr = 100;
                        }
                        else if (SearchStr.Substring(0, 1) == StringToInt[ctr])
                        {
                            UnmaskValue = UnmaskValue + ctr; ctr = 100;
                        }
                        ctr++;
                    }
                    if (ctr == 28) UnmaskValue = UnmaskValue + SearchStr.Substring(2, 1);
                    ctrLen = ctrLen + 3;
                }
                return UnmaskValue;
            }
            else return null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox1.Text = generateStringMask(StringValue.Text);
        }

        public static string generateStringMask(string pStringToMask)
        {
            if (pStringToMask != null)
            {
                int ctr = 0, ctrLen = 1;
                string SearchStr = null, MaskValue = null;
                string[] StringToInt = new string[28] { "V", "O", "N", "E", "A", "T", "U", "K", "Z", "Ñ", "F", "B", "M", "L", "S", "C", "I", "R", "J", "H", "W", "P", "Q", "X", "D", "Y", "G", " " };
                while (ctrLen <= pStringToMask.Length)
                {
                    SearchStr = pStringToMask.Substring(ctrLen - 1, 1); ctr = 0;
                    while (ctr < 28)
                    {
                        string CheckThis = StringToInt[ctr];
                        if (int.TryParse(SearchStr.Trim(), out int OutNum))
                        {
                            if (OutNum >= 27) OutNum = OutNum - 27;
                            MaskValue = MaskValue + StringToInt[OutNum] + StringToInt[OutNum] + StringToInt[OutNum];
                            ctr = 100;
                        }
                        else if (SearchStr.ToUpper() == StringToInt[ctr])
                        {
                            string SubMask = null;
                            if (SearchStr == StringToInt[ctr])
                            {
                                if (ctr < 10) SubMask = "10" + ctr;
                                else SubMask = "1" + ctr;
                                MaskValue = MaskValue + SubMask;
                            }
                            else if (SearchStr != StringToInt[ctr])
                            {
                                if (ctr < 10) SubMask = "00" + ctr;
                                else SubMask = "0" + ctr;
                                MaskValue = MaskValue + SubMask;
                            }
                            ctr = 100;
                        }
                        ctr++;
                    }
                    if (ctr == 28)
                        MaskValue = MaskValue + "//" + SearchStr;
                    ctrLen++;
                }
                return MaskValue;
            }
            else return null;
        }
    }
}
