using System;
using System.Security.Cryptography;
using System.Text;
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
            if (!string.IsNullOrEmpty(StringValue.Text))
            {
                textBox1.Text = String.Empty;
                textBox1.Text = RemoveStringMask(StringValue.Text);
            } 
            else
                MessageBox.Show("Please provide string to proceed", "Can't do that");
        }

        public static string RemoveStringMask(string pMaskedString)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Can't do that");
                return null;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StringValue.Text))
            {
                textBox1.Text = String.Empty;
                textBox1.Text = generateStringMask(StringValue.Text);
            }
            else
                MessageBox.Show("Please provide string to proceed", "Can't do that");

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
        string Password = "j1n15y5admin";
        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordTxtbox.Text))
            {
                //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                /*byte[] _bytesToHash = Encoding.Unicode.GetBytes(generateStringMask(PasswordTxtbox.Text));
                string _mPassword = Convert.ToBase64String(md5.ComputeHash(_bytesToHash));
                PasswordResult.Text = String.Empty;
                PasswordResult.Text = _mPassword;*/
                PasswordResult.Text = Hasher.Encrypt(PasswordTxtbox.Text, Password);

            }
            else 
                MessageBox.Show("Can't hash when empty", "Please input password");
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PasswordResult.Text = string.Empty;
            PasswordTxtbox.Text = string.Empty;
            textBox1.Text = string.Empty;
            StringValue.Text = string.Empty;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordTxtbox.Text))
            {
                /*MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] _bytesToHash = Encoding.Unicode.GetBytes(RemoveStringMask(PasswordTxtbox.Text));
                string _mPassword = Convert.ToBase64String(_bytesToHash);
                PasswordResult.Text = String.Empty;
                PasswordResult.Text = _mPassword;*/
                PasswordResult.Text = Hasher.Decrypt(PasswordTxtbox.Text, Password);
            }
            else
                MessageBox.Show("Can't hash when empty", "Please input password");
        }
    }
}
